using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Onova;
using Onova.Services;
using Exception = System.Exception;

namespace imgdanke
{
	public partial class MainForm : Form
	{
		private static readonly bool IS_LINUX = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
		private static readonly string MAGICK_FILENAME = IS_LINUX ? "magick" : "magick.exe";
		private static readonly string PINGO_FILENAME = IS_LINUX ? "pingo" : "pingo.exe";
		private static readonly Regex INVALID_FILENAME_CHARS_REGEX = new Regex("[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex INVALID_PATH_CHARS_REGEX = new Regex("[" + Regex.Escape(new string(Path.GetInvalidPathChars())) + "]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Color MENU_COLOR_OPTION = Color.FromArgb(216, 216, 216);
		private static readonly Color MENU_COLOR_OPTION_HIGHLIGHTED = Color.FromArgb(100, 100, 100);
		private static readonly Color COLOR_BACKGROUND = Color.FromArgb(55, 55, 55);
		//private static readonly Color COLOR_FOREGROUND = Color.FromArgb(216, 216, 216);
		internal static readonly UserConfig CONFIG = UserConfig.LoadConfig();

		private static readonly BindingList<KeyValuePair<string, string>> PNG_PALETTE_ITEMS = new BindingList<KeyValuePair<string, string>>
		{
			new KeyValuePair<string, string>("", ""),
			new KeyValuePair<string, string>("2c", "9"),
			new KeyValuePair<string, string>("4c", "14"),
			new KeyValuePair<string, string>("8c", "19"),
			new KeyValuePair<string, string>("16c", "24"),
			new KeyValuePair<string, string>("24c", "29"),
			new KeyValuePair<string, string>("32c", "34"),
			new KeyValuePair<string, string>("40c", "39"),
			new KeyValuePair<string, string>("48c", "44"),
			new KeyValuePair<string, string>("64c", "49"),
			new KeyValuePair<string, string>("88c", "54"),
			new KeyValuePair<string, string>("112c", "59"),
			new KeyValuePair<string, string>("124c", "64"),
			new KeyValuePair<string, string>("136c", "69"),
			new KeyValuePair<string, string>("160c", "74"),
			new KeyValuePair<string, string>("184c", "79"),
			new KeyValuePair<string, string>("208c", "84"),
			new KeyValuePair<string, string>("232c", "89"),
			new KeyValuePair<string, string>("256c", "100")
		};

		private static bool ShouldCancelProcessing;
		private static bool IsInitializing;
		private static bool ShouldDelayUpdatingCommands;
		private static bool EnableFormLevelDoubleBuffering = true;
		private static int OriginalExStyle = -1;

		private static readonly OutputSettingsForm OUTPUT_SETTINGS_FORM = new OutputSettingsForm();

		public sealed override string Text
		{
			get => base.Text;
			set => base.Text = value;
		}

		protected override CreateParams CreateParams
		{
			get
			{
				if ( OriginalExStyle == -1 )
				{
					OriginalExStyle = base.CreateParams.ExStyle;
				}

				CreateParams cp = base.CreateParams;

				if ( EnableFormLevelDoubleBuffering )
				{
					cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
				}
				else
				{
					cp.ExStyle = OriginalExStyle;
				}

				return cp;
			}
		}

		public MainForm(IReadOnlyList<string> args)
		{
			InitializeComponent();
			IsInitializing = true;
			InitializeDesignerOptions();
			InitializeWindowSettings();
			InitializeWithCommandLineArgs(args);
			InitializePingoPNGPaletteComboBox();
			InitializeWithConfigValues();
			IsInitializing = false;
		}

		#region Initialization

		#region InitFuncs

		private void InitializeDesignerOptions()
		{
			OptionsMenuStrip.Renderer = new DarkContextMenuRenderer();
			FilesContextMenuStrip.Renderer = new DarkContextMenuRenderer();
			PreferencesToolStripMenuItem.DropDown.Closing += DropDown_Closing;
			FilesInSourceFolderListBox.ContextMenuStrip = FilesContextMenuStrip;
		}
		private void InitializeWindowSettings()
		{
			//SetControlStyleFlagsViaReflection();

			Text = "imgdanke - v" + typeof(MainForm).Assembly.GetName().Version;

			if ( CONFIG.LastWindowLocation != Point.Empty )
			{
				Location = CONFIG.LastWindowLocation;
			}

			if ( CONFIG.ShouldStartMaximized )
			{
				WindowState = FormWindowState.Maximized;
			}
			else if ( CONFIG.LastWindowSize != Size.Empty )
			{
				Size = CONFIG.LastWindowSize;
			}

			DisableFailedToCheckMessageToolStripMenuItem.Checked = CONFIG.ShouldDisableFailedToCheckForUpdatesMessage;
			CheckForUpdatesOnStartupToolStripMenuItem.Checked = CONFIG.ShouldCheckForUpdatesOnStartup;

			EnsureWindowIsWithinBounds();
		}

		//private void SetControlStyleFlagsViaReflection()
		//{
		//	const ControlStyles FLAGS_FOR_OFF = ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw;
		//	const ControlStyles FLAGS_FOR_ON = ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque;
		//	const string FILE_LIST_BOX_NAME = nameof(FilesInSourceFolderListBox);

		//	foreach ( Control control in Controls )
		//	{
		//		if ( control.Name == FILE_LIST_BOX_NAME )
		//		{
		//			continue;
		//		}

		//		SetControlStyleViaReflection(control, FLAGS_FOR_OFF, false);
		//		SetControlStyleViaReflection(control, FLAGS_FOR_ON, true);
		//	}
		//}

		//private static void SetControlStyleViaReflection(Control control, ControlStyles flags, bool value)
		//{
		//	Type type = control.GetType();
		//	MethodInfo method = type.GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
		//	object[] param = { flags, value };
		//	method?.Invoke(control, param);
		//}

		private void EnsureWindowIsWithinBounds()
		{
			if ( Screen.AllScreens.Any(s => s.WorkingArea.Contains(DesktopBounds)) )
			{
				return;
			}

			Location = Point.Empty;
			Size = MinimumSize;
		}

		private void InitializeWithCommandLineArgs(IReadOnlyList<string> args)
		{
			if ( !args.Any() )
			{
				return;
			}

			string sourcePath = args[0];
			SourceFolderPathTextBox.Text = sourcePath;

			if ( args.Count == 2 )
			{
				string outputPath = args[1];
				OutputFolderPathTextBox.Text = outputPath;
			}
		}

		private static async void CheckForProgramUpdates(bool isManualCheck = false)
		{
			if ( !isManualCheck && !CONFIG.ShouldCheckForUpdatesOnStartup )
			{
				return;
			}

			using UpdateManager updateManager = new UpdateManager(
				new GithubPackageResolver("DrWhoCares", "imgdanke", "imgdanke_*.zip"),
				new ZipPackageExtractor()
			);

			// Check for updates
			try
			{
				var check = await updateManager.CheckForUpdatesAsync();

				// If there are no updates, continue on silently
				if ( !check.CanUpdate )
				{
					return;
				}

				DialogResult result = MessageBox.Show("There is a new version (" + check.LastVersion + ") available for download. Would you like to download and install it? Click the Help button to open the changelog.", "New Version Update", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, 0, "https://github.com/DrWhoCares/imgdanke/releases/tag/" + check.LastVersion);

				if ( result != DialogResult.Yes )
				{
					return;
				}

				// Prepare the latest update
				await updateManager.PrepareUpdateAsync(check.LastVersion ?? throw new InvalidOperationException());

				// Launch updater and exit
				updateManager.LaunchUpdater(check.LastVersion);
				Application.Exit();
			}
			catch ( Exception e )
			{
				if ( !CONFIG.ShouldDisableFailedToCheckForUpdatesMessage )
				{
					if ( MessageBox.Show("Checking for updates threw an exception:\n\"" + e.Message + "\"\n\nYou may not be able to access api.github.com. You can safely continue using this offline.\n\nWould you like to disable this message for future checks? (Can be re-enabled in Preferences Menu.)", "Error Checking For Update", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes )
					{
						CONFIG.ShouldDisableFailedToCheckForUpdatesMessage = true;
					}
				}
			}
		}

		private void InitializePingoPNGPaletteComboBox()
		{
			PingoPNGPaletteComboBox.DataSource = PNG_PALETTE_ITEMS;
			PingoPNGPaletteComboBox.DisplayMember = "Key";
			PingoPNGPaletteComboBox.ValueMember = "Value";
		}

		private void InitializeWithConfigValues()
		{
			InitializeImagemagickPathToExe();
			InitializePingoPathToExe();
			InitializeSourceFolderPath();
			InitializeOutputFolderPath();
			InitializeDeleteOriginals();
			InitializeReplaceOriginals();
			InitializePresetSetting();
			InitializeOutputExtension();
			InitializeShouldIncludeSubfolders();
			InitializeShouldIncludePSDs();
			InitializeMagickCommandString();
			InitializePingoCommandString();
			InitializeMenuStripItems();
		}

		private static void InitializeImagemagickPathToExe()
		{
			if ( VerifyImagemagickPathIsValid() )
			{
				return;
			}

			string exePathFound = CheckSystemPathForExe(MAGICK_FILENAME);

			if ( string.IsNullOrWhiteSpace(exePathFound) )
			{
				exePathFound = CheckLocalPathForExe(MAGICK_FILENAME);
			}

			if ( string.IsNullOrWhiteSpace(exePathFound) )
			{
				MessageBox.Show("The Config's path to the (" + MAGICK_FILENAME + ") file is invalid or is not found on your PATH or within the directory that this exe is in. Please ensure it is downloaded, or edit the Config to the correct path.\nYou can download it here: https://imagemagick.org/script/download.php", MAGICK_FILENAME + " Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				CONFIG.ImagemagickPathToExe = exePathFound;
			}
		}

		private void InitializePingoPathToExe()
		{
			if ( VerifyPingoPathIsValid() )
			{
				return;
			}

			string exePathFound = CheckSystemPathForExe(PINGO_FILENAME);

			if ( string.IsNullOrWhiteSpace(exePathFound) )
			{
				exePathFound = CheckLocalPathForExe(PINGO_FILENAME);
			}

			if ( string.IsNullOrWhiteSpace(exePathFound) )
			{
				if ( !CONFIG.ShouldSuppressPingoNotFoundWarning )
				{
					CONFIG.ShouldSuppressPingoNotFoundWarning = MessageBox.Show("The Config's path to the (" + PINGO_FILENAME + ") file is invalid or is not found on your PATH or within the directory that this exe is in. Please ensure it is downloaded, or edit the Config to the correct path.\nYou can download it here: https://css-ig.net/pingo\n\nDo you want to ignore this warning in the future?", PINGO_FILENAME + " Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
				}

				DisablePingoGUIElements();
			}
			else
			{
				CONFIG.PingoPathToExe = exePathFound;
			}
		}

		private void DisablePingoGUIElements()
		{
			PingoSettingsGroupBox.Enabled = false;
			PingoCommandTextBox.Enabled = false;
			PingoFourBppColorPresetRadioButton.Enabled = false;
			PingoEightBppColorPresetRadioButton.Enabled = false;
		}

		private void InitializeSourceFolderPath()
		{
			if ( VerifySourceFolderPathIsValid() )
			{
				SourceFolderPathTextBox.Text = CONFIG.SourceFolderPath;
			}
			else
			{
				CONFIG.SourceFolderPath = "";
			}
		}

		private void InitializeOutputFolderPath()
		{
			if ( VerifyOutputFolderPathIsValid() )
			{
				OutputFolderPathTextBox.Text = CONFIG.OutputFolderPath;
			}
			else
			{
				CONFIG.OutputFolderPath = "";
			}
		}

		private void InitializeDeleteOriginals()
		{
			DeleteOriginalsAfterCheckBox.Checked = CONFIG.ShouldDeleteOriginals;
		}

		private void InitializeReplaceOriginals()
		{
			ReplaceOriginalsCheckBox.Checked = CONFIG.ShouldReplaceOriginals;

			OutputFolderPathButton.Enabled = !CONFIG.ShouldReplaceOriginals;
			OutputFolderPathTextBox.Enabled = !CONFIG.ShouldReplaceOriginals;
		}

		private void InitializePresetSetting()
		{
			switch ( CONFIG.PresetSetting )
			{
				case PresetSettings.None:
					NoPresetRadioButton.Checked = true;
					break;
				case PresetSettings.Custom:
					CustomPresetRadioButton.Checked = true;
					break;
				case PresetSettings.Gray1Bpp:
					OneBppGrayPresetRadioButton.Checked = true;
					break;
				case PresetSettings.Gray4Bpp:
					FourBppGrayPresetRadioButton.Checked = true;
					break;
				case PresetSettings.Gray8Bpp:
					EightBppGrayPresetRadioButton.Checked = true;
					break;
				case PresetSettings.PingoColor4Bpp:
					PingoFourBppColorPresetRadioButton.Checked = true;
					break;
				case PresetSettings.PingoColor8Bpp:
					PingoEightBppColorPresetRadioButton.Checked = true;
					break;
				case PresetSettings.MagickColor4Bpp:
					MagickFourBppColorPresetRadioButton.Checked = true;
					break;
				case PresetSettings.MagickColor8Bpp:
					MagickEightBppColorPresetRadioButton.Checked = true;
					break;
			}
		}

		private void InitializeOutputExtension()
		{
			if ( VerifyOutputExtensionIsValid() )
			{
				OutputExtensionTextBox.Text = CONFIG.OutputExtension;
			}
			else
			{
				CONFIG.OutputExtension = "";
			}
		}

		private void InitializeShouldIncludeSubfolders()
		{
			IncludeSubfoldersCheckBox.Checked = CONFIG.ShouldIncludeSubfolders;
			MaintainFolderStructureCheckBox.Visible = CONFIG.ShouldIncludeSubfolders;
			MaintainFolderStructureCheckBox.Checked = CONFIG.ShouldMaintainFolderStructure;
		}

		private void InitializeShouldIncludePSDs()
		{
			IncludePSDsCheckBox.Checked = CONFIG.ShouldIncludePSDs;
		}

		private void InitializeMagickCommandString()
		{
			if ( VerifyMagickCommandIsValid() )
			{
				MagickCommandTextBox.Text = CONFIG.MagickCommandString;
			}
			else
			{
				CONFIG.MagickCommandString = "";
			}
		}

		private void InitializePingoCommandString()
		{
			if ( VerifyPingoCommandIsValid() )
			{
				PingoCommandTextBox.Text = CONFIG.PingoCommandString;
			}
			else
			{
				CONFIG.PingoCommandString = "";
			}
		}

		private void InitializeMenuStripItems()
		{
			ShouldOutputToNewFolderToolStripMenuItem.Checked = CONFIG.ShouldOutputToNewFolder;
			AddTagsToFilenamesToolStripMenuItem.Checked = CONFIG.ShouldAddTagsToFilenames;
			AddTagsToNewFolderToolStripMenuItem.Checked = CONFIG.ShouldAddTagsToOutputFolder;
		}

		#endregion

		#region Verification

		private static bool VerifyImagemagickPathIsValid()
		{
			return FileOps.DoesFileExist(CONFIG.ImagemagickPathToExe) && CONFIG.ImagemagickPathToExe.Contains(MAGICK_FILENAME);
		}

		private static bool VerifyPingoPathIsValid()
		{
			return FileOps.DoesFileExist(CONFIG.PingoPathToExe) && CONFIG.PingoPathToExe.Contains(PINGO_FILENAME);
		}

		private static string CheckSystemPathForExe(string filename)
		{
			string values = Environment.GetEnvironmentVariable("PATH");

			if ( values == null )
			{
				return "";
			}

			foreach ( string path in values.Split(Path.PathSeparator) )
			{
				string fullPath = Path.Combine(path, filename);

				if ( FileOps.DoesFileExist(fullPath) )
				{
					return fullPath;
				}
			}

			return "";
		}

		private static string CheckLocalPathForExe(string filename)
		{
			string localPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			localPath ??= "";
			string fullPath = Path.Combine(localPath, filename);

			if ( FileOps.DoesFileExist(fullPath) )
			{
				return fullPath;
			}

			foreach ( string path in new DirectoryInfo(localPath).EnumerateDirectories("*", SearchOption.AllDirectories).Select(d => d.FullName) )
			{
				fullPath = Path.Combine(path, filename);

				if ( FileOps.DoesFileExist(fullPath) )
				{
					return fullPath;
				}
			}

			return "";
		}

		private static bool VerifySourceFolderPathIsValid()
		{
			return FileOps.DoesDirectoryExist(CONFIG.SourceFolderPath);
		}

		private static bool VerifyOutputFolderPathIsValid()
		{
			return FileOps.DoesDirectoryExist(CONFIG.OutputFolderPath) || CONFIG.ShouldReplaceOriginals;
		}

		private static bool VerifyOutputExtensionIsValid()
		{
			return !string.IsNullOrWhiteSpace(CONFIG.OutputExtension) && CONFIG.OutputExtension[0] == '.';
		}

		private static bool VerifyMagickCommandIsValid()
		{
			return !string.IsNullOrWhiteSpace(CONFIG.MagickCommandString);
		}

		private static bool VerifyPingoCommandIsValid()
		{
			return !string.IsNullOrWhiteSpace(CONFIG.PingoCommandString);
		}

		private bool VerifyReadyToApply()
		{
			return VerifyOutputSettingsAreValid()
				&& (VerifyMagickCommandIsReadyAndValid() || VerifyPingoCommandIsReadyAndValid())
				&& FilesInSourceFolderListBox.SelectedItems.Count > 0;
		}

		private static bool VerifyOutputSettingsAreValid()
		{
			return VerifySourceFolderPathIsValid()
				&& VerifyOutputFolderPathIsValid()
				&& VerifyOutputExtensionIsValid();
		}

		private static bool VerifyMagickCommandIsReadyAndValid()
		{
			return VerifyImagemagickPathIsValid()
				&& VerifyMagickCommandIsValid();
		}

		private static bool VerifyPingoCommandIsReadyAndValid()
		{
			return VerifyPingoPathIsValid()
				&& VerifyPingoCommandIsValid();
		}

		#endregion

		#endregion

		#region UIEvents

		#region FormUI

		private void MainForm_Shown(object sender, EventArgs e)
		{
			CheckForProgramUpdates();
		}

		private void MainForm_LocationChanged(object sender, EventArgs e)
		{
			CONFIG.LastWindowLocation = Location;
		}

		private void MainForm_SizeChanged(object sender, EventArgs e)
		{
			TurnOnFormLevelDoubleBuffering();
			FilesInSourceFolderListBox.BeginUpdate();

			if ( Size.Height >= MainSplitContainer.Panel2MinSize )
			{
				MainSplitContainer.SplitterDistance = Size.Height;
			}

			FilesInSourceFolderListBox.EndUpdate();
			TurnOffFormLevelDoubleBuffering();

			CONFIG.LastWindowSize = Size;
			CONFIG.ShouldStartMaximized = WindowState == FormWindowState.Maximized;
		}

		private void TurnOnFormLevelDoubleBuffering()
		{
			EnableFormLevelDoubleBuffering = true;
			MaximizeBox = true;
		}

		private void TurnOffFormLevelDoubleBuffering()
		{
			EnableFormLevelDoubleBuffering = false;
			MaximizeBox = true;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			ShouldCancelProcessing = true;
		}

		#endregion

		#region PathsUI

		private void SourceFolderPathButton_Click(object sender, EventArgs e)
		{
			SourceFolderPathTextBox.Text = IS_LINUX ? OpenFolderDialogLinux() : OpenFolderDialogWindows();
		}

		private void SourceFolderPathTextBox_TextChanged(object sender, EventArgs e)
		{
			if ( !FileOps.DoesDirectoryExist(SourceFolderPathTextBox.Text) )
			{
				ClearFilesInSourceFolderList();
				return;
			}

			CONFIG.SourceFolderPath = SourceFolderPathTextBox.Text;
			BuildFilesInSourceFolderList();
			StartButton.Enabled = VerifyReadyToApply();
		}

		private void TextBoxRestrictToPathPermittedChars_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ( !char.IsControl(e.KeyChar) && INVALID_PATH_CHARS_REGEX.IsMatch(e.KeyChar.ToString()) )
			{
				e.Handled = true;
			}
		}

		private void FilesInSourceFolderListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			StartButton.Enabled = VerifyReadyToApply();

			if ( FilesInSourceFolderListBox.Items.Count == 0 )
			{
				MassFileSelectorButton.Enabled = false;
				return;
			}

			MassFileSelectorButton.Text = FilesInSourceFolderListBox.SelectedIndices.Count > 0 ? "Unselect All" : "Select All";

			if ( MassFileSelectorButton.Enabled )
			{
				return;
			}

			MassFileSelectorButton.Enabled = true;
		}

		private void OutputFolderPathButton_Click(object sender, EventArgs e)
		{
			OutputFolderPathTextBox.Text = IS_LINUX ? OpenFolderDialogLinux() : OpenFolderDialogWindows();
		}

		private void OutputFolderPathTextBox_TextChanged(object sender, EventArgs e)
		{
			CONFIG.OutputFolderPath = OutputFolderPathTextBox.Text;
			StartButton.Enabled = VerifyReadyToApply();
		}

		private void PathTextBox_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
		}

		private void PathTextBox_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

			if ( !files.Any() )
			{
				return;
			}

			if ( FileOps.DoesDirectoryExist(files.First()) )
			{
				((TextBox)sender).Text = files.First();
			}
			else if ( FileOps.DoesFileExist(files.First()) )
			{
				((TextBox)sender).Text = new FileInfo(files.First()).DirectoryName;
			}
		}

		private void DeleteOriginalsAfterCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.ShouldDeleteOriginals = DeleteOriginalsAfterCheckBox.Checked;
		}

		private void ReplaceOriginalsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			CONFIG.ShouldReplaceOriginals = ReplaceOriginalsCheckBox.Checked;

			OutputFolderPathButton.Enabled = !CONFIG.ShouldReplaceOriginals;
			OutputFolderPathTextBox.Enabled = !CONFIG.ShouldReplaceOriginals;
			MaintainFolderStructureCheckBox.Visible = !CONFIG.ShouldReplaceOriginals && CONFIG.ShouldIncludeSubfolders;

			StartButton.Enabled = VerifyReadyToApply();
		}

		private void MaintainFolderStructureCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldMaintainFolderStructure = MaintainFolderStructureCheckBox.Checked;
		}

		private static string OpenFolderDialogLinux()
		{
			using FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

			return folderBrowserDialog.ShowDialog() == DialogResult.OK ? folderBrowserDialog.SelectedPath : "";
		}

		private static string OpenFolderDialogWindows()
		{
			using CommonOpenFileDialog folderBrowserDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			};

			return folderBrowserDialog.ShowDialog() == CommonFileDialogResult.Ok ? folderBrowserDialog.FileName : "";
		}

		#endregion

		#region PresetsUI

		private void NoPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !NoPresetRadioButton.Checked )
			{
				return;
			}

			ShouldDelayUpdatingCommands = true;
			CONFIG.PresetSetting = PresetSettings.None;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.Invalid;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.Invalid;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "";
			MagickNormalizeCheckBox.Checked = false;
			MagickContrastStretchCheckBox.Checked = false;
			MagickAutoLevelCheckBox.Checked = false;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Best;
			PingoStripCheckBox.Checked = !IS_LINUX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			ShouldDelayUpdatingCommands = false;
		}

		private void CustomPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !CustomPresetRadioButton.Checked )
			{
				return;
			}

			ShouldDelayUpdatingCommands = true;
			CONFIG.PresetSetting = PresetSettings.Custom;
			MagickDitherComboBox.SelectedIndex = GetIndexOfStringInComboBox(MagickDitherComboBox, CONFIG.MagickDither);
			MagickColorspaceComboBox.SelectedIndex = GetIndexOfStringInComboBox(MagickColorspaceComboBox, CONFIG.MagickColorspace);
			MagickColorsTextBox.Text = CONFIG.MagickColorsValue > 0 ? CONFIG.MagickColorsValue.ToString() : "";
			MagickDepthTextBox.Text = CONFIG.MagickDepthValue > 0 ? CONFIG.MagickDepthValue.ToString() : "";
			MagickPosterizeTextBox.Text = CONFIG.MagickPosterizeValue > 0 ? CONFIG.MagickPosterizeValue.ToString() : "";
			MagickNormalizeCheckBox.Checked = CONFIG.ShouldUseMagickNormalize;
			MagickContrastStretchCheckBox.Checked = CONFIG.ShouldUseMagickContrastStretch;
			MagickAutoLevelCheckBox.Checked = CONFIG.ShouldUseMagickAutoLevel;
			PingoPNGPaletteComboBox.SelectedIndex = PNG_PALETTE_ITEMS.ToList().FindIndex(i => i.Value == CONFIG.PingoPNGPaletteValue.ToString());
			PingoNoDitheringCheckBox.Checked = CONFIG.ShouldUsePingoNoDithering;
			PingoOptimizationLevelComboBox.SelectedIndex = GetIndexOfStringInComboBox(PingoOptimizationLevelComboBox, CONFIG.PingoOptimizeLevel);
			PingoStripCheckBox.Checked = CONFIG.ShouldUsePingoStrip;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			ShouldDelayUpdatingCommands = false;
		}

		private static int GetIndexOfStringInComboBox(ComboBox comboBox, string item)
		{
			int result = comboBox.FindStringExact(item);
			return result < 0 ? 0 : result;
		}

		private void OneBppGrayPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !OneBppGrayPresetRadioButton.Checked )
			{
				return;
			}

			ShouldDelayUpdatingCommands = true;
			CONFIG.PresetSetting = PresetSettings.Gray1Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.None;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.Gray;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "2";
			MagickNormalizeCheckBox.Checked = true;
			MagickContrastStretchCheckBox.Checked = true;
			MagickAutoLevelCheckBox.Checked = false;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Best;
			PingoStripCheckBox.Checked = !IS_LINUX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			ShouldDelayUpdatingCommands = false;
		}

		private void FourBppGrayPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !FourBppGrayPresetRadioButton.Checked )
			{
				return;
			}

			ShouldDelayUpdatingCommands = true;
			CONFIG.PresetSetting = PresetSettings.Gray4Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.None;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.Gray;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "16";
			MagickNormalizeCheckBox.Checked = false;
			MagickContrastStretchCheckBox.Checked = false;
			MagickAutoLevelCheckBox.Checked = true;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Best;
			PingoStripCheckBox.Checked = !IS_LINUX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			ShouldDelayUpdatingCommands = false;
		}

		private void EightBppGrayPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !EightBppGrayPresetRadioButton.Checked )
			{
				return;
			}

			ShouldDelayUpdatingCommands = true;
			CONFIG.PresetSetting = PresetSettings.Gray8Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.None;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.Gray;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "";
			MagickNormalizeCheckBox.Checked = true;
			MagickContrastStretchCheckBox.Checked = true;
			MagickAutoLevelCheckBox.Checked = false;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Best;
			PingoStripCheckBox.Checked = !IS_LINUX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			ShouldDelayUpdatingCommands = false;
		}

		private void PingoFourBppColorPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !PingoFourBppColorPresetRadioButton.Checked )
			{
				return;
			}

			ShouldDelayUpdatingCommands = true;
			CONFIG.PresetSetting = PresetSettings.PingoColor4Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.Invalid;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.sRGB;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "";
			MagickNormalizeCheckBox.Checked = false;
			MagickContrastStretchCheckBox.Checked = false;
			MagickAutoLevelCheckBox.Checked = false;
			PingoPNGPaletteComboBox.SelectedIndex = PNG_PALETTE_ITEMS.ToList().FindIndex(i => i.Value == "24");
			PingoNoDitheringCheckBox.Enabled = true;
			PingoNoDitheringCheckBox.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Best;
			PingoStripCheckBox.Checked = !IS_LINUX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			ShouldDelayUpdatingCommands = false;
		}

		private void PingoEightBppColorPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !PingoEightBppColorPresetRadioButton.Checked )
			{
				return;
			}

			ShouldDelayUpdatingCommands = true;
			CONFIG.PresetSetting = PresetSettings.PingoColor8Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.Invalid;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.sRGB;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "";
			MagickNormalizeCheckBox.Checked = false;
			MagickContrastStretchCheckBox.Checked = false;
			MagickAutoLevelCheckBox.Checked = false;
			PingoPNGPaletteComboBox.SelectedIndex = PingoPNGPaletteComboBox.Items.Count - 1;
			PingoNoDitheringCheckBox.Enabled = true;
			PingoNoDitheringCheckBox.Checked = true;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Best;
			PingoStripCheckBox.Checked = !IS_LINUX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			ShouldDelayUpdatingCommands = false;
		}

		private void MagickFourBppColorPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !MagickFourBppColorPresetRadioButton.Checked )
			{
				return;
			}

			ShouldDelayUpdatingCommands = true;
			CONFIG.PresetSetting = PresetSettings.MagickColor4Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.None;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.sRGB;
			MagickColorsTextBox.Text = "16";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "256";
			MagickNormalizeCheckBox.Checked = true;
			MagickContrastStretchCheckBox.Checked = true;
			MagickAutoLevelCheckBox.Checked = false;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Best;
			PingoStripCheckBox.Checked = !IS_LINUX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			ShouldDelayUpdatingCommands = false;
		}

		private void MagickEightBppColorPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !MagickEightBppColorPresetRadioButton.Checked )
			{
				return;
			}

			ShouldDelayUpdatingCommands = true;
			CONFIG.PresetSetting = PresetSettings.MagickColor8Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.None;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.sRGB;
			MagickColorsTextBox.Text = "256";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "256";
			MagickNormalizeCheckBox.Checked = true;
			MagickContrastStretchCheckBox.Checked = true;
			MagickAutoLevelCheckBox.Checked = false;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Best;
			PingoStripCheckBox.Checked = !IS_LINUX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			ShouldDelayUpdatingCommands = false;
		}

		#endregion

		#region MagickUI

		private void MagickDitherComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.MagickDither = MagickDitherComboBox.SelectedItem == null ? "" : MagickDitherComboBox.SelectedItem.ToString();

			if ( !ShouldDelayUpdatingCommands )
			{
				MagickCommandTextBox.Text = ConstructMagickCommandString();
			}
		}

		private void MagickColorspaceComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.MagickColorspace = MagickColorspaceComboBox.SelectedItem == null ? "" : MagickColorspaceComboBox.SelectedItem.ToString();

			if ( !ShouldDelayUpdatingCommands )
			{
				MagickCommandTextBox.Text = ConstructMagickCommandString();
			}
		}

		private void MagickColorsTextBox_TextChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			if ( !IsAllDigits(MagickColorsTextBox.Text) )
			{
				MagickColorsTextBox.Text = GetStringWithOnlyDigits(MagickColorsTextBox.Text);
			}

			CONFIG.MagickColorsValue = string.IsNullOrWhiteSpace(MagickColorsTextBox.Text) ? 0 : int.Parse(MagickColorsTextBox.Text);

			if ( !ShouldDelayUpdatingCommands )
			{
				MagickCommandTextBox.Text = ConstructMagickCommandString();
			}
		}

		private void MagickDepthTextBox_TextChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			if ( !IsAllDigits(MagickDepthTextBox.Text) )
			{
				MagickDepthTextBox.Text = GetStringWithOnlyDigits(MagickDepthTextBox.Text);
			}

			CONFIG.MagickDepthValue = string.IsNullOrWhiteSpace(MagickDepthTextBox.Text) ? 0 : int.Parse(MagickDepthTextBox.Text);

			if ( !ShouldDelayUpdatingCommands )
			{
				MagickCommandTextBox.Text = ConstructMagickCommandString();
			}
		}

		private void MagickPosterizeTextBox_TextChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			if ( !IsAllDigits(MagickPosterizeTextBox.Text) )
			{
				MagickPosterizeTextBox.Text = GetStringWithOnlyDigits(MagickPosterizeTextBox.Text);
			}

			CONFIG.MagickPosterizeValue = string.IsNullOrWhiteSpace(MagickPosterizeTextBox.Text) ? 0 : int.Parse(MagickPosterizeTextBox.Text);

			if ( !ShouldDelayUpdatingCommands )
			{
				MagickCommandTextBox.Text = ConstructMagickCommandString();
			}
		}

		private void MagickNormalizeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldUseMagickNormalize = MagickNormalizeCheckBox.Checked;

			if ( !ShouldDelayUpdatingCommands )
			{
				MagickCommandTextBox.Text = ConstructMagickCommandString();
			}
		}

		private void MagickContrastStretchCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldUseMagickContrastStretch = MagickContrastStretchCheckBox.Checked;

			if ( !ShouldDelayUpdatingCommands )
			{
				MagickCommandTextBox.Text = ConstructMagickCommandString();
			}
		}

		private void MagickAutoLevelCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldUseMagickAutoLevel = MagickAutoLevelCheckBox.Checked;

			if ( !ShouldDelayUpdatingCommands )
			{
				MagickCommandTextBox.Text = ConstructMagickCommandString();
			}
		}

		#endregion

		#region PingoUI

		private void PingoPNGPaletteComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			int value = PingoPNGPaletteComboBox.SelectedItem == null || string.IsNullOrWhiteSpace(PingoPNGPaletteComboBox.SelectedValue.ToString())
				? 0 : int.Parse(PingoPNGPaletteComboBox.SelectedValue.ToString());

			PingoNoDitheringCheckBox.Enabled = value > 0;
			CONFIG.PingoPNGPaletteValue = value;

			if ( !ShouldDelayUpdatingCommands )
			{
				PingoCommandTextBox.Text = ConstructPingoCommandString();
			}
		}

		private void PingoNoDitheringCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldUsePingoNoDithering = PingoNoDitheringCheckBox.Checked;

			if ( !ShouldDelayUpdatingCommands )
			{
				PingoCommandTextBox.Text = ConstructPingoCommandString();
			}
		}

		private void PingoOptimizationLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.PingoOptimizeLevel = PingoOptimizationLevelComboBox.SelectedItem == null ? "" : PingoOptimizationLevelComboBox.SelectedItem.ToString();

			if ( !ShouldDelayUpdatingCommands )
			{
				PingoCommandTextBox.Text = ConstructPingoCommandString();
			}
		}

		private void PingoStripCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldUsePingoStrip = PingoStripCheckBox.Checked;

			if ( !ShouldDelayUpdatingCommands )
			{
				PingoCommandTextBox.Text = ConstructPingoCommandString();
			}
		}

		#endregion

		#region FileUI

		private void MassFileSelectorButton_Click(object sender, EventArgs e)
		{
			if ( FilesInSourceFolderListBox.Items.Count == 0 )
			{
				return;
			}

			if ( FilesInSourceFolderListBox.SelectedIndices.Count > 0 )
			{
				UnselectAllItemsInListBox(FilesInSourceFolderListBox);
				MassFileSelectorButton.Text = "Select All";
			}
			else
			{
				SelectAllInListBox(FilesInSourceFolderListBox);
				MassFileSelectorButton.Text = "Unselect All";
			}
		}

		private static void UnselectAllItemsInListBox(ListBox listBox)
		{
			int previousTopIndex = listBox.TopIndex;
			listBox.ClearSelected();
			listBox.TopIndex = previousTopIndex;
		}

		private static void SelectAllInListBox(ListBox listBox)
		{
			int previousTopIndex = listBox.TopIndex;
			listBox.BeginUpdate();

			for ( int itemIndex = 0; itemIndex < listBox.Items.Count; ++itemIndex )
			{
				listBox.SetSelected(itemIndex, true);
			}

			listBox.EndUpdate();
			listBox.TopIndex = previousTopIndex;
		}

		private void RefreshFileListButton_Click(object sender, EventArgs e)
		{
			BuildFilesInSourceFolderList();
		}

		private void OutputExtensionTextBox_TextChanged(object sender, EventArgs e)
		{
			CONFIG.OutputExtension = OutputExtensionTextBox.Text.ToLowerInvariant();
			StartButton.Enabled = VerifyReadyToApply();
		}

		private void IncludeSubfoldersCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldIncludeSubfolders = IncludeSubfoldersCheckBox.Checked;
			MaintainFolderStructureCheckBox.Visible = IncludeSubfoldersCheckBox.Checked && !CONFIG.ShouldReplaceOriginals;
			BuildFilesInSourceFolderList();
		}

		private void IncludePSDsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldIncludePSDs = IncludePSDsCheckBox.Checked;
			BuildFilesInSourceFolderList();
		}

		private void BuildFilesInSourceFolderList()
		{
			if ( !FileOps.DoesDirectoryExist(SourceFolderPathTextBox.Text) )
			{
				ClearFilesInSourceFolderList();
			}
			else
			{
				FilesInSourceFolderListBox.DataSource = GetImageFilesList(SourceFolderPathTextBox.Text);
				FilesInSourceFolderListBox.DisplayMember = "Subpath";
				FilesInSourceFolderListBox.ValueMember = "Subpath";
				FilesInSourceFolderListBox.SelectedIndex = -1;
			}

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}

		private void ClearFilesInSourceFolderList()
		{
			FilesInSourceFolderListBox.DataSource = null;
		}

		private static List<FileInfoWithSubpath> GetImageFilesList(string imageFilesFullPath)
		{
			List<FileInfoWithSubpath> imageFilesWithSubpaths = new List<FileInfoWithSubpath>();

			foreach ( FileInfo fileInfo in GetImageFiles(imageFilesFullPath).OrderByAlphaNumeric(DirectoryOrderer.GetDirectoryName) )
			{
				imageFilesWithSubpaths.Add(new FileInfoWithSubpath(fileInfo, GetSubpathFromFileInfo(fileInfo, imageFilesFullPath)));
			}

			imageFilesWithSubpaths.Sort(DirectoryOrderer.CompareSubpaths);

			return imageFilesWithSubpaths;
		}

		private static IEnumerable<FileInfo> GetImageFiles(string imageFilesFullPath)
		{
			return new DirectoryInfo(imageFilesFullPath)
				.EnumerateFiles("*.*", CONFIG.ShouldIncludeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
				.Where(s => s.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
						|| s.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
						|| s.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
						|| s.Name.EndsWith(".webp", StringComparison.OrdinalIgnoreCase)
						|| (CONFIG.ShouldIncludePSDs && s.Name.EndsWith(".psd", StringComparison.OrdinalIgnoreCase)));
		}

		private static string GetSubpathFromFileInfo(FileInfo fileInfo, string workingPath)
		{
			string subpath = fileInfo.FullName.Substring(workingPath.Length);
			return (subpath.First() == '\\' || subpath.First() == '/') ? subpath.Substring(1) : subpath;
		}

		private void FilesInSourceFolderListBox_MouseDown(object sender, MouseEventArgs e)
		{
			if ( e.Button != MouseButtons.Right )
			{
				return;
			}

			FilesInSourceFolderListBox.SelectedIndices.Clear();
			FilesInSourceFolderListBox.SelectedIndex = FilesInSourceFolderListBox.IndexFromPoint(e.X, e.Y);
		}

		private void OpenPathToFileContextMenuItem_Click(object sender, EventArgs e)
		{
			FileInfo fileInfo = ((FileInfoWithSubpath)FilesInSourceFolderListBox.SelectedItem).ImageInfo;
			FileOps.OpenPathToFileInExplorer(fileInfo.DirectoryName, fileInfo.Name, IS_LINUX);
		}

		private void OpenImageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FileInfo fileInfo = ((FileInfoWithSubpath)FilesInSourceFolderListBox.SelectedItem).ImageInfo;
			FileOps.OpenFileInDefaultApplication(fileInfo.DirectoryName, fileInfo.Name);
		}

		#endregion

		#region UtilityFunctions

		private void RestrictToNumbersTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ( !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) )
			{
				e.Handled = true;
			}
		}

		private void TextBoxRestrictToFilePermittedChars_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ( !char.IsControl(e.KeyChar) && INVALID_FILENAME_CHARS_REGEX.IsMatch(e.KeyChar.ToString()) )
			{
				e.Handled = true;
			}
		}

		private static bool IsAllDigits(string str) => str.All(char.IsDigit);

		private static string GetStringWithOnlyDigits(string str) => new string(str.Where(char.IsDigit).ToArray());

		#endregion

		#region CommandsUI

		private string ConstructMagickCommandString()
		{
			const string MAGICK_COMMAND_PREFIX = "convert \"%1\" ";
			const string MAGICK_COMMAND_SUFFIX = "\"%2\"";

			EnsureMagickConfigValuesAreUpdated();

			string command = MAGICK_COMMAND_PREFIX;

			if ( !string.IsNullOrWhiteSpace(CONFIG.MagickDither) )
			{
				command += CONFIG.MagickDither + " ";
			}

			if ( !string.IsNullOrWhiteSpace(CONFIG.MagickColorspace) )
			{
				command += CONFIG.MagickColorspace + " ";
			}

			if ( CONFIG.MagickColorsValue > 0 )
			{
				command += "-colors " + CONFIG.MagickColorsValue + " ";
			}

			if ( CONFIG.MagickDepthValue > 0 )
			{
				command += "-depth " + CONFIG.MagickDepthValue + " ";
			}

			if ( CONFIG.MagickPosterizeValue > 0 )
			{
				command += "-posterize " + CONFIG.MagickPosterizeValue + " ";
			}

			if ( CONFIG.ShouldUseMagickNormalize )
			{
				command += "-normalize ";
			}

			if ( CONFIG.ShouldUseMagickContrastStretch )
			{
				command += "-contrast-stretch 0%x0% ";
			}

			if ( CONFIG.ShouldUseMagickAutoLevel )
			{
				command += "-auto-level ";
			}

			command += MAGICK_COMMAND_SUFFIX;

			return command;
		}

		private void EnsureMagickConfigValuesAreUpdated()
		{
			CONFIG.MagickDither = MagickDitherComboBox.SelectedItem == null ? "" : MagickDitherComboBox.SelectedItem.ToString();
			CONFIG.MagickColorspace = MagickColorspaceComboBox.SelectedItem == null ? "" : MagickColorspaceComboBox.SelectedItem.ToString();

			int.TryParse(MagickColorsTextBox.Text, out int colorsValue);
			CONFIG.MagickColorsValue = colorsValue > 0 ? colorsValue : 0;

			int.TryParse(MagickDepthTextBox.Text, out int depthValue);
			CONFIG.MagickDepthValue = depthValue > 0 ? depthValue : 0;

			int.TryParse(MagickPosterizeTextBox.Text, out int posterizeValue);
			CONFIG.MagickPosterizeValue = posterizeValue > 0 ? posterizeValue : 0;

			CONFIG.ShouldUseMagickNormalize = MagickNormalizeCheckBox.Checked;
		}

		private string ConstructPingoCommandString()
		{
			const string PINGO_COMMAND_SUFFIX = "\"%1\"";

			EnsurePingoConfigValuesAreUpdated();

			string command = "";

			if ( CONFIG.PingoPNGPaletteValue > 0 && CONFIG.PingoPNGPaletteValue <= 100 )
			{
				command += "-pngpalette=" + CONFIG.PingoPNGPaletteValue + " ";

				if ( CONFIG.ShouldUsePingoNoDithering )
				{
					command += "-nodithering ";
				}
			}

			command += CONFIG.PingoOptimizeLevel + " ";

			if ( CONFIG.ShouldUsePingoStrip )
			{
				command += "-strip ";
			}

			command += PINGO_COMMAND_SUFFIX;

			return command;
		}

		private void EnsurePingoConfigValuesAreUpdated()
		{
			int pngPaletteValue = 0;

			if ( PingoPNGPaletteComboBox.SelectedItem != null )
			{
				int.TryParse(PingoPNGPaletteComboBox.SelectedValue.ToString(), out pngPaletteValue);
			}

			CONFIG.PingoPNGPaletteValue = pngPaletteValue > 0 && pngPaletteValue <= 100 ? pngPaletteValue : 0;
			CONFIG.ShouldUsePingoNoDithering = PingoNoDitheringCheckBox.Checked;
			CONFIG.PingoOptimizeLevel = PingoOptimizationLevelComboBox.SelectedItem == null ? "" : PingoOptimizationLevelComboBox.SelectedItem.ToString();
			CONFIG.ShouldUsePingoStrip = PingoStripCheckBox.Checked;
		}

		private void MagickCommandTextBox_TextChanged(object sender, EventArgs e)
		{
			CONFIG.MagickCommandString = string.IsNullOrWhiteSpace(MagickCommandTextBox.Text) ? "" : MagickCommandTextBox.Text;
			StartButton.Enabled = VerifyReadyToApply();
		}

		private void PingoCommandTextBox_TextChanged(object sender, EventArgs e)
		{
			CONFIG.PingoCommandString = string.IsNullOrWhiteSpace(PingoCommandTextBox.Text) ? "" : PingoCommandTextBox.Text;
			StartButton.Enabled = VerifyReadyToApply();
		}

		private void ProcessingCancelButton_Click(object sender, EventArgs e)
		{
			ShouldCancelProcessing = true;
		}

		#endregion

		#endregion

		#region Processing

		private void StartButton_Click(object sender, EventArgs e)
		{
			ToggleUI(false);
			List<FileInfo> imgFiles = FilesInSourceFolderListBox.SelectedItems.Cast<FileInfoWithSubpath>().Select(f => f.ImageInfo).ToList();

			if ( !imgFiles.Any() )
			{
				StatusMessageLabel.Text = "No valid files in the folder selected.";
				ToggleUI(true);
				ShouldCancelProcessing = false;
				return;
			}

			Stopwatch stopwatch = Stopwatch.StartNew();
			long previousTotalFilesize = 0;
			long newTotalFilesize = 0;
			ProcessingProgressBar.Maximum = (imgFiles.Count * 2) + imgFiles.Where(f => f.Extension.ToLowerInvariant() == ".psd").ToList().Count;

			InitializeTagsStrings();

			List<FileInfo> origFiles = CONFIG.ShouldDeleteOriginals ? imgFiles : new List<FileInfo>();

			if ( !ShouldCancelProcessing && CONFIG.ShouldIncludePSDs )
			{
				imgFiles = ConvertAnyPSDs(imgFiles, PrependToOutputTextBox.Text, AppendToOutputTextBox.Text, StatusMessageLabel, ProcessingProgressBar);
			}

			if ( !ShouldCancelProcessing )
			{
				PreviousSizeLabel.Text = "Prev Size: " + GetTotalSizeOfFiles(imgFiles, ref previousTotalFilesize);
			}

			if ( !ShouldCancelProcessing && VerifyMagickCommandIsReadyAndValid() )
			{
				imgFiles = CallMagickCommand(imgFiles, MagickCommandTextBox.Text, PrependToOutputTextBox.Text, AppendToOutputTextBox.Text, StatusMessageLabel, ProcessingProgressBar);
			}

			if ( !ShouldCancelProcessing && !string.IsNullOrWhiteSpace(CONFIG.PingoPathToExe) && VerifyPingoCommandIsReadyAndValid() )
			{
				CallPingoCommand(imgFiles, PingoCommandTextBox.Text, StatusMessageLabel, ProcessingProgressBar);
			}

			if ( !ShouldCancelProcessing )
			{
				NewSizeLabel.Text = "New Size: " + GetTotalSizeOfFiles(imgFiles, ref newTotalFilesize);
				long filesizeDiff = -(newTotalFilesize - previousTotalFilesize);
				TotalSavingsLabel.Text = "Total Savings: " + GetBytesAsReadableString(filesizeDiff) + " or " + GetTotalSavingsPercentage(previousTotalFilesize, filesizeDiff);
			}

			if ( !ShouldCancelProcessing && CONFIG.ShouldDeleteOriginals )
			{
				ProcessDeletingOriginalFiles(origFiles, imgFiles);
			}

			BuildFilesInSourceFolderList();
			ToggleUI(true);
			stopwatch.Stop();

			if ( ShouldCancelProcessing )
			{
				StatusMessageLabel.Text = "Command(s) canceled. Some files may have already been processed, or may be in an invalid state.";
			}
			else
			{
				StatusMessageLabel.Text = "Command(s) completed. Total time elapsed: " + TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).ToString(@"hh\:mm\:ss");
			}

			ShouldCancelProcessing = false;
		}

		private void ToggleUI(bool isActive)
		{
			MagickCommandTextBox.Enabled = isActive;
			PingoCommandTextBox.Enabled = isActive;
			SourceFolderPathTextBox.Enabled = isActive;
			SourceFolderPathButton.Enabled = isActive;
			OutputFolderPathTextBox.Enabled = !CONFIG.ShouldReplaceOriginals;
			OutputFolderPathButton.Enabled = !CONFIG.ShouldReplaceOriginals;
			DeleteOriginalsAfterCheckBox.Enabled = isActive;
			ReplaceOriginalsCheckBox.Enabled = isActive;
			MaintainFolderStructureCheckBox.Enabled = isActive;
			PresetSettingsGroupBox.Enabled = isActive;
			ImagemagickSettingsGroupBox.Enabled = isActive;
			PingoSettingsGroupBox.Enabled = isActive;
			FilesInSourceFolderGroupBox.Enabled = isActive;
			StartButton.Enabled = isActive;
			FilesInSourceFolderListBox.Enabled = isActive;
			MassFileSelectorButton.Enabled = isActive;
			RefreshFileListButton.Enabled = isActive;
			PrependToOutputTextBox.Enabled = isActive;
			AppendToOutputTextBox.Enabled = isActive;
			ProcessingProgressBar.Visible = !isActive;
			ProcessingProgressBar.Value = 0;
			ProcessingProgressBar.Maximum = 100;
			ProcessingCancelButton.Enabled = !isActive;
			ProcessingCancelButton.Visible = !isActive;
			OutputSettingsToolStripMenuItem.Enabled = isActive;
			ShouldOutputToNewFolderToolStripMenuItem.Enabled = isActive;
			AddTagsToFilenamesToolStripMenuItem.Enabled = isActive;
			AddTagsToNewFolderToolStripMenuItem.Enabled = isActive;

			if ( string.IsNullOrWhiteSpace(CONFIG.PingoPathToExe) )
			{
				DisablePingoGUIElements();
			}

			if ( !isActive )
			{
				PreviousSizeLabel.Text = "Prev Size: XXX.XXYY";
				NewSizeLabel.Text = "New Size: XXX.XXYY";
				TotalSavingsLabel.Text = "Total Savings: XXX.XXYY or XX.XX%";
			}
		}

		private void InitializeTagsStrings()
		{
			if ( CONFIG.ShouldAddTagsToFilenames )
			{
				string fullString = "";

				if ( CONFIG.ShouldAddPresetToFilenames )
				{
					fullString += GetPresetTagsString();
				}

				if ( CONFIG.ShouldAddMagickSettingsToFilenames )
				{
					fullString += GetMagickSettingsTagsString();
				}

				if ( CONFIG.ShouldAddPingoSettingsToFilenames )
				{
					fullString += GetPingoSettingsTagsString();
				}

				CONFIG.TagsStringToAppendToFilenames = fullString;
			}

			if ( CONFIG.ShouldOutputToNewFolder )
			{
				string fullPath = CONFIG.OutputFolderPath + "/" + CONFIG.NewOutputFolderBaseName;

				if ( CONFIG.ShouldAddPresetToOutputFolder )
				{
					fullPath += GetPresetTagsString();
				}

				if ( CONFIG.ShouldAddMagickSettingsToOutputFolder )
				{
					fullPath += GetMagickSettingsTagsString();
				}

				if ( CONFIG.ShouldAddPingoSettingsToOutputFolder )
				{
					fullPath += GetPingoSettingsTagsString();
				}

				CONFIG.NewOutputFolderPath = fullPath + "/";
				Directory.CreateDirectory(CONFIG.NewOutputFolderPath);
			}
		}

		private static string GetPresetTagsString()
		{
			return CONFIG.PresetSetting switch
			{
				PresetSettings.None => "(NoPreset)",
				PresetSettings.Custom => "(CustomPreset)",
				PresetSettings.Gray1Bpp => "(Gray1bpp)",
				PresetSettings.Gray4Bpp => "(Gray4bpp)",
				PresetSettings.Gray8Bpp => "(Gray8bpp)",
				PresetSettings.PingoColor4Bpp => "(PingoColor4bpp)",
				PresetSettings.PingoColor8Bpp => "(PingoColor8bpp)",
				PresetSettings.MagickColor4Bpp => "(MagickColor4bpp)",
				PresetSettings.MagickColor8Bpp => "(MagickColor8bpp)",
				_ => "(InvalidPreset)"
			};
		}

		private static string GetMagickSettingsTagsString()
		{
			string fullString = "(" + CONFIG.MagickDither + ")";
			fullString += "(" + CONFIG.MagickColorspace + ")";

			if ( CONFIG.MagickColorsValue > 0 )
			{
				fullString += "(colors" + CONFIG.MagickColorsValue + ")";
			}

			if ( CONFIG.MagickDepthValue > 0 )
			{
				fullString += "(depth" + CONFIG.MagickDepthValue + ")";
			}

			if ( CONFIG.MagickPosterizeValue > 0 )
			{
				fullString += "(pos" + CONFIG.MagickPosterizeValue + ")";
			}

			if ( CONFIG.ShouldUseMagickNormalize )
			{
				fullString += "(norm)";
			}

			if ( CONFIG.ShouldUseMagickContrastStretch )
			{
				fullString += "(cs)";
			}

			if ( CONFIG.ShouldUseMagickAutoLevel )
			{
				fullString += "(al)";
			}

			return fullString;
		}

		private static string GetPingoSettingsTagsString()
		{
			string fullString = "";

			if ( CONFIG.PingoPNGPaletteValue > 0 )
			{
				fullString += "(pngpal" + CONFIG.PingoPNGPaletteValue + ")";

				if ( CONFIG.ShouldUsePingoNoDithering )
				{
					fullString += "(nodither)";
				}
			}

			if ( CONFIG.PingoOptimizeLevel != "Off" )
			{
				fullString += "(-s" + CONFIG.PingoOptimizeLevel.Last() + ")";
			}

			if ( CONFIG.ShouldUsePingoStrip )
			{
				fullString += "(-strip)";
			}

			return fullString;
		}

		private static List<FileInfo> ConvertAnyPSDs(List<FileInfo> originalImgFiles, string prependString, string appendString, Label statusLabel, ProgressBar progressBar)
		{
			List<FileInfo> psdFiles = originalImgFiles.Where(f => f.Extension.ToLowerInvariant() == ".psd").ToList();

			if ( !psdFiles.Any() )
			{
				return originalImgFiles;
			}

			List<FileInfo> newImgFiles = originalImgFiles.Where(f => f.Extension.ToLowerInvariant() != ".psd").ToList();

			foreach ( FileInfo psdFile in psdFiles )
			{
				ProcessStartInfo startInfo = new ProcessStartInfo
				{
					FileName = IS_LINUX ? CONFIG.ImagemagickPathToExe : "magick.exe",
					UseShellExecute = false,
					CreateNoWindow = true,
					WorkingDirectory = CONFIG.SourceFolderPath
				};

				string outputFilename = DetermineOutputFilepath(psdFile) + prependString + psdFile.Name.Replace(psdFile.Extension, "")
										+ appendString + (CONFIG.ShouldAddTagsToFilenames ? CONFIG.TagsStringToAppendToFilenames : "") + ".tmp" + CONFIG.OutputExtension;
				startInfo.Arguments = "convert \"" + psdFile.FullName + "[0]\" \"" + outputFilename + "\"";
				statusLabel.Text = "Converting \"" + psdFile.Name + "\" via magick convert.";

				using Process process = Process.Start(startInfo);

				if ( process == null )
				{
					if ( DisplayProcessIsNullError(true) )
					{
						ShouldCancelProcessing = true;
					}

					break;
				}

				while ( !process.HasExited )
				{
					Application.DoEvents();

					if ( ShouldCancelProcessing )
					{
						KillProcessAndWait(process);
						break;
					}
				}

				if ( ShouldCancelProcessing )
				{
					KillProcessAndWait(process);
					return new List<FileInfo>();
				}

				newImgFiles.Add(new FileInfo(outputFilename));
				++progressBar.Value;
			}

			return newImgFiles;
		}

		private static List<FileInfo> CallMagickCommand(List<FileInfo> imgFiles, string commandString, string prependString, string appendString, Label statusLabel, ProgressBar progressBar)
		{
			const string DEFAULT_COMMAND = "convert \"%1\" \"%2\"";
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = IS_LINUX ? CONFIG.ImagemagickPathToExe : "magick.exe",
				UseShellExecute = false,
				CreateNoWindow = true,
				WorkingDirectory = CONFIG.SourceFolderPath
			};

			List<FileInfo> newImgFiles = new List<FileInfo>();

			foreach ( FileInfo img in imgFiles )
			{
				if ( ShouldCancelProcessing )
				{
					return new List<FileInfo>();
				}

				if ( commandString == DEFAULT_COMMAND && img.Extension.ToLowerInvariant() == CONFIG.OutputExtension && img.DirectoryName == CONFIG.OutputFolderPath && string.IsNullOrWhiteSpace(prependString) && string.IsNullOrWhiteSpace(appendString) )
				{
					// Avoid processing the magick command if it won't actually do anything. Still need to process it if the extension would change
					newImgFiles.Add(new FileInfo(img.FullName));
					++progressBar.Value;
					continue;
				}

				bool isFromPSD = img.Name.Contains(".tmp");
				string tempFilename;
				string filenameAlone = img.Name.Replace(img.Extension, "");

				if ( isFromPSD )
				{
					tempFilename = img.FullName;
				}
				else
				{
					if ( CONFIG.ShouldReplaceOriginals )
					{
						tempFilename = DetermineOutputFilepath(img) + filenameAlone + CONFIG.OutputExtension;
					}
					else
					{
						tempFilename = DetermineOutputFilepath(img) + prependString + filenameAlone + appendString
										+ (CONFIG.ShouldAddTagsToFilenames ? CONFIG.TagsStringToAppendToFilenames : "") + ".tmp" + CONFIG.OutputExtension;
					}
				}

				startInfo.Arguments = commandString;
				startInfo.Arguments = startInfo.Arguments.Replace("%1", img.FullName);
				startInfo.Arguments = startInfo.Arguments.Replace("%2", tempFilename);
				statusLabel.Text = "Processing magick command on \"" + img.Name + "\".";

				using Process process = Process.Start(startInfo);

				if ( process == null )
				{
					if ( DisplayProcessIsNullError(true) )
					{
						ShouldCancelProcessing = true;
					}

					break;
				}

				while ( !process.HasExited )
				{
					Application.DoEvents();

					if ( ShouldCancelProcessing )
					{
						KillProcessAndWait(process);
						break;
					}
				}

				if ( ShouldCancelProcessing )
				{
					KillProcessAndWait(process);
					return new List<FileInfo>();
				}

				string newLocation;

				if ( CONFIG.ShouldReplaceOriginals )
				{
					newLocation = tempFilename.Replace(filenameAlone, prependString + filenameAlone.Replace(".tmp", "") + appendString + (CONFIG.ShouldAddTagsToFilenames ? CONFIG.TagsStringToAppendToFilenames : ""));
				}
				else
				{
					newLocation = tempFilename.Replace(".tmp", "");
				}

				while ( !FileOps.IsFileReady(tempFilename) )
				{
					Application.DoEvents();

					if ( ShouldCancelProcessing )
					{
						KillProcessAndWait(process);
						return new List<FileInfo>();
					}
				}

				FileOps.Move(tempFilename, newLocation);

				newImgFiles.Add(new FileInfo(newLocation));
				++progressBar.Value;
			}

			return newImgFiles;
		}

		private static string DetermineOutputFilepath(FileInfo fileInfo)
		{
			if ( CONFIG.ShouldReplaceOriginals )
			{
				return fileInfo.DirectoryName + "/";
			}

			if ( CONFIG.ShouldIncludeSubfolders && CONFIG.ShouldMaintainFolderStructure )
			{
				return DetermineSubfolderPath(fileInfo) + "/";
			}

			if ( CONFIG.ShouldOutputToNewFolder )
			{
				return CONFIG.NewOutputFolderPath;
			}

			return CONFIG.OutputFolderPath + "/";
		}

		private static string DetermineSubfolderPath(FileInfo fileInfo)
		{
			string outputFolderName = CONFIG.OutputFolderPath + (CONFIG.OutputFolderPath == CONFIG.SourceFolderPath ? "/_OUTPUT" : "") + fileInfo.DirectoryName?.Replace(CONFIG.SourceFolderPath, "");
			Directory.CreateDirectory(outputFolderName);

			return outputFolderName;
		}

		private static void CallPingoCommand(List<FileInfo> imgFiles, string commandString, Label statusLabel, ProgressBar progressBar)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = IS_LINUX ? CONFIG.PingoPathToExe : "pingo.exe",
				UseShellExecute = false,
				CreateNoWindow = true,
				WorkingDirectory = CONFIG.OutputFolderPath
			};

			for ( int imgIndex = 0; imgIndex < imgFiles.Count; ++imgIndex )
			{
				FileInfo img = imgFiles[imgIndex];

				if ( ShouldCancelProcessing )
				{
					break;
				}

				startInfo.Arguments = commandString;
				startInfo.Arguments = startInfo.Arguments.Replace("%1", img.FullName);

				statusLabel.Text = "Processing pingo command on \"" + img.Name + "\".";
				using Process process = Process.Start(startInfo);

				if ( process == null )
				{
					if ( DisplayProcessIsNullError(false) )
					{
						ShouldCancelProcessing = true;
					}

					break;
				}

				while ( !process.HasExited )
				{
					Application.DoEvents();

					if ( ShouldCancelProcessing )
					{
						KillProcessAndWait(process);
						break;
					}
				}

				if ( img.Name.Contains(".tmp") )
				{
					while ( !FileOps.IsFileReady(img.FullName) )
					{
						Application.DoEvents();

						if ( ShouldCancelProcessing )
						{
							KillProcessAndWait(process);
							return;
						}
					}

					string newFilepath = img.DirectoryName + "/" + img.Name.Replace(".tmp", "");
					FileOps.Move(img.FullName, newFilepath);
					imgFiles[imgIndex] = new FileInfo(newFilepath);
				}

				++progressBar.Value;
			}
		}

		private static void ProcessDeletingOriginalFiles(List<FileInfo> origFiles, List<FileInfo> imgFiles)
		{
			foreach ( FileInfo origFile in origFiles )
			{
				if ( ShouldCancelProcessing )
				{
					break;
				}

				bool shouldSkipDeletion = false;

				foreach ( FileInfo imgFile in imgFiles )
				{
					if ( origFile.FullName == imgFile.FullName )
					{
						shouldSkipDeletion = true;
						break;
					}
				}

				if ( shouldSkipDeletion )
				{
					continue;
				}

				if ( FileOps.DoesFileExist(origFile.FullName) && origFile.Extension.ToLowerInvariant() != ".psd" )
				{
					FileOps.Delete(origFile.FullName);
				}
			}
		}

		private static bool DisplayProcessIsNullError(bool isMagickProcess)
		{
			return MessageBox.Show("Unable to start the process for '" + (isMagickProcess ? MAGICK_FILENAME : PINGO_FILENAME) + "'. Process returned is null.\nDo you want to cancel further processing?",
				"Process could not be started",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation) == DialogResult.Yes;
		}

		// Returns true if the process has exited, including if the process passed is null, or has already exited
		// This means that it only returns false if it fails to exit after the timeout period
		private static void KillProcessAndWait(Process process)
		{
			if ( process == null || process.HasExited )
			{
				return;
			}

			string processName = process.ProcessName;
			process.Kill();
			
			if ( !process.WaitForExit(10000) )
			{
				MessageBox.Show("Process (" + processName + ") failed to close after the timeout time of 10 seconds. The process may still be running, and you may need to close it manually.", processName + " failed to close", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private static string GetTotalSizeOfFiles(List<FileInfo> imgFiles, ref long totalFilesizeInBytes)
		{
			foreach ( FileInfo file in imgFiles )
			{
				totalFilesizeInBytes += file.Length;
			}

			return GetBytesAsReadableString(totalFilesizeInBytes);
		}

		// Returns the human-readable file size for an arbitrary, 64-bit file size 
		// The default format is "0.### XB", e.g. "4.2 KB" or "1.434 GB"
		// Taken from: https://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net/11124118#11124118
		private static string GetBytesAsReadableString(long totalSize)
		{
			long totalSizeAbsolute = (totalSize < 0 ? -totalSize : totalSize);
			string suffix;
			double readable;

			if ( totalSizeAbsolute >= 0x40000000 ) // Gigabyte
			{
				suffix = "GB";
				readable = (totalSize >> 20);
			}
			else if ( totalSizeAbsolute >= 0x100000 ) // Megabyte
			{
				suffix = "MB";
				readable = (totalSize >> 10);
			}
			else if ( totalSizeAbsolute >= 0x400 ) // Kilobyte
			{
				suffix = "KB";
				readable = totalSize;
			}
			else
			{
				return totalSize.ToString("0B"); // Byte
			}

			readable /= 1024; // Divide by 1024 to get fractional value

			return readable.ToString("0.##") + suffix; // Return formatted number with suffix
		}

		private static string GetTotalSavingsPercentage(long previousTotalFilesize, double filesizeDiffAbs)
		{
			return Math.Round((filesizeDiffAbs / previousTotalFilesize) * 100.0, 2, MidpointRounding.AwayFromZero).ToString("0.##") + "%";
		}

		#endregion

		#region DarkRenderer

		private class DarkContextMenuRenderer : ToolStripProfessionalRenderer
		{
			public DarkContextMenuRenderer() : base(new DarkContextMenuColors())
			{
			}

			protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
			{
				Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
				using SolidBrush brush = new SolidBrush(MENU_COLOR_OPTION);
				e.Graphics.FillRectangle(brush, rect);
				using Pen pen = new Pen(Color.Black);
				int y = rect.Height / 2;
				e.Graphics.DrawLine(pen, rect.Left, y, rect.Right - 1, y);
			}

			protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
			{
				Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
				Color color = e.Item.Selected ? MENU_COLOR_OPTION_HIGHLIGHTED : MENU_COLOR_OPTION;

				using SolidBrush brush = new SolidBrush(color);
				e.Graphics.FillRectangle(brush, rect);
			}
		}

		private class DarkContextMenuColors : ProfessionalColorTable
		{
			//public override Color ButtonCheckedGradientBegin => COLOR_FOREGROUND;
			//public override Color ButtonCheckedGradientEnd => COLOR_FOREGROUND;
			//public override Color ButtonCheckedGradientMiddle => COLOR_FOREGROUND;
			//public override Color ButtonCheckedHighlight => COLOR_FOREGROUND;
			//public override Color ButtonCheckedHighlightBorder => COLOR_FOREGROUND;
			//public override Color ButtonPressedBorder => COLOR_FOREGROUND;
			//public override Color ButtonPressedGradientBegin => COLOR_FOREGROUND;
			//public override Color ButtonPressedGradientEnd => COLOR_FOREGROUND;
			//public override Color ButtonPressedGradientMiddle => COLOR_FOREGROUND;
			//public override Color ButtonPressedHighlight => COLOR_FOREGROUND;
			//public override Color ButtonPressedHighlightBorder => COLOR_FOREGROUND;
			//public override Color ButtonSelectedGradientBegin => COLOR_FOREGROUND;
			//public override Color ButtonSelectedGradientEnd => COLOR_FOREGROUND;
			//public override Color ButtonSelectedGradientMiddle => COLOR_FOREGROUND;
			//public override Color ButtonSelectedHighlight => COLOR_FOREGROUND;
			//public override Color ButtonSelectedHighlightBorder => COLOR_FOREGROUND;
			//public override Color CheckBackground => COLOR_FOREGROUND;
			//public override Color CheckPressedBackground => COLOR_FOREGROUND;
			//public override Color CheckSelectedBackground => COLOR_FOREGROUND;
			//public override Color GripDark => COLOR_FOREGROUND;
			//public override Color GripLight => COLOR_FOREGROUND;
			public override Color ImageMarginGradientBegin => MENU_COLOR_OPTION;
			public override Color ImageMarginGradientEnd => MENU_COLOR_OPTION;
			public override Color ImageMarginGradientMiddle => MENU_COLOR_OPTION;
			public override Color MenuBorder => COLOR_BACKGROUND;
			//public override Color MenuItemPressedGradientBegin => COLOR_FOREGROUND;
			//public override Color MenuItemPressedGradientEnd => COLOR_FOREGROUND;
			//public override Color MenuItemPressedGradientMiddle => COLOR_FOREGROUND;
			//public override Color MenuItemSelected => COLOR_FOREGROUND;
			//public override Color MenuItemSelectedGradientBegin => COLOR_FOREGROUND;
			//public override Color MenuItemSelectedGradientEnd => COLOR_FOREGROUND;
			public override Color MenuStripGradientBegin => MENU_COLOR_OPTION;
			public override Color MenuStripGradientEnd => MENU_COLOR_OPTION;
			//public override Color OverflowButtonGradientBegin => COLOR_FOREGROUND;
			//public override Color OverflowButtonGradientEnd => COLOR_FOREGROUND;
			//public override Color OverflowButtonGradientMiddle => COLOR_FOREGROUND;
			//public override Color RaftingContainerGradientBegin => COLOR_FOREGROUND;
			//public override Color RaftingContainerGradientEnd => COLOR_FOREGROUND;
			public override Color SeparatorDark => COLOR_BACKGROUND;
			public override Color SeparatorLight => COLOR_BACKGROUND;
			//public override Color StatusStripGradientBegin => COLOR_FOREGROUND;
			//public override Color StatusStripGradientEnd => COLOR_FOREGROUND;
			public override Color ToolStripBorder => COLOR_BACKGROUND;
			//public override Color ToolStripContentPanelGradientBegin => COLOR_FOREGROUND;
			//public override Color ToolStripContentPanelGradientEnd => COLOR_FOREGROUND;
			public override Color ToolStripDropDownBackground => MENU_COLOR_OPTION;
			//public override Color ToolStripGradientBegin => COLOR_FOREGROUND;
			//public override Color ToolStripGradientEnd => COLOR_FOREGROUND;
			//public override Color ToolStripGradientMiddle => COLOR_FOREGROUND;
			//public override Color ToolStripPanelGradientBegin => COLOR_FOREGROUND;
			//public override Color ToolStripPanelGradientEnd => COLOR_FOREGROUND;
		}

		#endregion

		#region MenuUI

		#region FileTabUI

		private void UserConfigToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string localPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			localPath ??= "";
			localPath = Path.Combine(localPath, UserConfig.CONFIG_FILENAME);

			FileOps.OpenFileInDefaultApplication(localPath);
		}

		private void SourceFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FileOps.OpenFolderPathInExplorer(SourceFolderPathTextBox.Text);
		}

		private void OutputFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FileOps.OpenFolderPathInExplorer(OutputFolderPathTextBox.Text);
		}

		private void ImgdankeFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string localPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			localPath ??= "";

			FileOps.OpenFolderPathInExplorer(localPath);
		}

		private void SaveCurrentSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CONFIG.SaveConfig();
		}

		private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShouldCancelProcessing = true;
			Application.Exit();
		}

		#endregion

		#region PreferencesTabUI

		private void DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			if ( e.CloseReason == ToolStripDropDownCloseReason.ItemClicked )
			{
				e.Cancel = true;
			}
		}

		private void OutputSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OUTPUT_SETTINGS_FORM.ShowDialog(this);
			ShouldOutputToNewFolderToolStripMenuItem.Checked = CONFIG.ShouldOutputToNewFolder;
			AddTagsToFilenamesToolStripMenuItem.Checked = CONFIG.ShouldAddTagsToFilenames;
			AddTagsToNewFolderToolStripMenuItem.Checked = CONFIG.ShouldAddTagsToOutputFolder;
		}

		private void ShouldOutputToNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CONFIG.ShouldOutputToNewFolder = ShouldOutputToNewFolderToolStripMenuItem.Checked;
		}

		private void AddTagsToFilenamesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CONFIG.ShouldAddTagsToFilenames = AddTagsToFilenamesToolStripMenuItem.Checked;
		}

		private void AddTagsToNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CONFIG.ShouldAddTagsToOutputFolder = AddTagsToNewFolderToolStripMenuItem.Checked;
		}

		private void CheckForUpdatesOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CONFIG.ShouldCheckForUpdatesOnStartup = CheckForUpdatesOnStartupToolStripMenuItem.Checked;
		}

		private void DisableFailedToCheckMessageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CONFIG.ShouldDisableFailedToCheckForUpdatesMessage = DisableFailedToCheckMessageToolStripMenuItem.Checked;
		}

		private void CheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CheckForProgramUpdates(true);
			PreferencesToolStripMenuItem.DropDown.Close();
		}

		#endregion

		#region HelpTabUI

		private void GitHubToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				using Process process = Process.Start("");
			}
			catch ( Exception ex )
			{
				MessageBox.Show("Unable to open `https://github.com/DrWhoCares/imgdanke`. Exception thrown:\n\n`" + ex.Message + "`",
					"Cannot open README.md",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			}
		}

		private void OpenDocumentationFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string localPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			localPath ??= "";

			if ( string.IsNullOrEmpty(localPath) )
			{
				return;
			}

			localPath = Path.Combine(localPath, "README.md");

			try
			{
				using Process process = Process.Start(localPath);
			}
			catch ( Exception ex )
			{
				MessageBox.Show("Unable to open the file at `" + localPath + "`. Exception thrown:\n\n`" + ex.Message + "`",
					"Cannot open README.md",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			}
		}

		private void OpenDocumentationGitHubToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				using Process process = Process.Start("https://github.com/DrWhoCares/imgdanke/blob/master/README.md");
			}
			catch ( Exception ex )
			{
				MessageBox.Show("Unable to open `https://github.com/DrWhoCares/imgdanke/blob/master/README.md`. Exception thrown:\n\n`" + ex.Message + "`",
					"Cannot open README.md",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			}
		}

		private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("v" + typeof(MainForm).Assembly.GetName().Version + "\n- Created by DrWhoCares\nhttps://github.com/DrWhoCares/imgdanke",
				"About imgdanke",
				MessageBoxButtons.OK);
		}

		#endregion

		#endregion

	}

	internal readonly struct FileInfoWithSubpath
	{
		internal FileInfoWithSubpath(FileInfo fileInfo, string subpath)
		{
			ImageInfo = fileInfo;
			Subpath = subpath;
		}

		public FileInfo ImageInfo { get; }
		// ReSharper disable once MemberCanBePrivate.Global
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Subpath { get; }
	}

	internal static class DirectoryOrderer
	{
		internal static string GetDirectoryName(FileInfo fileInfo) => fileInfo.DirectoryName;
		private static string GetFileName(FileInfo fileInfo) => fileInfo.Name;

		internal static IOrderedEnumerable<T> OrderByAlphaNumeric<T>(this IEnumerable<T> source, Func<T, string> selector)
		{
			var enumerable = source.ToList();
			int max = enumerable.SelectMany(i => Regex.Matches(selector(i), @"\d+").Cast<Match>().Select(m => m.Value.Length)).DefaultIfEmpty().Max();
			return enumerable.OrderBy(i => Regex.Replace(selector(i), @"\d+", m => m.Value.PadLeft(max, '0')));
		}

		internal static int CompareSubpaths(FileInfoWithSubpath lhs, FileInfoWithSubpath rhs)
		{
			if ( lhs.ImageInfo.DirectoryName != rhs.ImageInfo.DirectoryName )
			{
				return new List<FileInfo> { lhs.ImageInfo, rhs.ImageInfo }.OrderByAlphaNumeric(GetDirectoryName).First() == lhs.ImageInfo ? -1 : 1;
			}

			return new List<FileInfo> { lhs.ImageInfo, rhs.ImageInfo }.OrderByAlphaNumeric(GetFileName).First() == lhs.ImageInfo ? -1 : 1;
		}
	}
}
