using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Onova;
using Onova.Services;
using Exception = System.Exception;
using FileInfo = System.IO.FileInfo;

namespace imgdanke
{
	public partial class MainForm : Form
	{
		private const string TEMP_FOLDER_NAME = "imgdanke/";
		private static readonly bool IS_UNIX = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
		private static readonly string MAGICK_FILENAME = IS_UNIX ? "magick" : "magick.exe";
		private static readonly string PINGO_FILENAME = IS_UNIX ? "pingo" : "pingo.exe";
		private static readonly Regex INVALID_FILENAME_CHARS_REGEX = new("[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex INVALID_PATH_CHARS_REGEX = new("[" + Regex.Escape(new string(Path.GetInvalidPathChars())) + "]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Regex PINGO_OPTIMIZE_OPTIONS_REGEX = new("-s[0-9,a,b]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		private static readonly Color MENU_COLOR_OPTION = Color.FromArgb(216, 216, 216);
		private static readonly Color MENU_COLOR_OPTION_HIGHLIGHTED = Color.FromArgb(100, 100, 100);
		private static readonly Color COLOR_BACKGROUND = Color.FromArgb(55, 55, 55);
		//private static readonly Color COLOR_FOREGROUND = Color.FromArgb(216, 216, 216);
		internal static readonly UserConfig CONFIG = UserConfig.LoadConfig(IS_UNIX);

		private static readonly BindingList<KeyValuePair<string, string>> PNG_PALETTE_ITEMS = new()
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
		private static List<FileInfoWithSubpath> FilesInSourceFolderList;
		private static List<FileInfoWithSubpath> FilesInSourceFolderListDataSource;

		private static readonly OutputSettingsForm OUTPUT_SETTINGS_FORM = new();

		private class ImgInfo
		{
			internal FileInfo NewInfo { get; set; }
			internal FileInfo OrigInfo { get; }

			internal ImgInfo(FileInfo orig)
			{
				OrigInfo = orig;
				NewInfo = new FileInfo(orig.FullName);
			}

			internal bool IsOrigPSD() => IsPSD(OrigInfo);
			internal bool AreFilesTheSame() => NewInfo.FullName == OrigInfo.FullName;
		}

		private static bool IsPSD(FileInfo file) => file.Extension.ToLowerInvariant() == ".psd";

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

			ShouldOutputToNewFolderToolStripMenuItem.Checked = CONFIG.ShouldOutputToNewFolder;
			UseSourceDirAsOutputDirToolStripMenuItem.Checked = CONFIG.ShouldUseSourceFolderAsOutputFolder;
			AddTagsToFilenamesToolStripMenuItem.Checked = CONFIG.ShouldAddTagsToFilenames;
			AddTagsToNewFolderToolStripMenuItem.Checked = CONFIG.ShouldAddTagsToOutputFolder;
			CheckForUpdatesOnStartupToolStripMenuItem.Checked = CONFIG.ShouldCheckForUpdatesOnStartup;
			DisableFailedToCheckMessageToolStripMenuItem.Checked = CONFIG.ShouldDisableFailedToCheckForUpdatesMessage;

			FilesInSourceFolderListBox.DisplayMember = "Subpath";
			FilesInSourceFolderListBox.ValueMember = "Subpath";
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

			using UpdateManager updateManager = new(
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
			InitializeOutputExtension();
			InitializePresetSetting();
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

			OutputFolderPathButton.Enabled = !CONFIG.ShouldReplaceOriginals && !CONFIG.ShouldUseSourceFolderAsOutputFolder;
			OutputFolderPathTextBox.Enabled = !CONFIG.ShouldReplaceOriginals && !CONFIG.ShouldUseSourceFolderAsOutputFolder;
		}

		private void InitializeOutputExtension()
		{
			if ( VerifyOutputExtensionIsValid() )
			{
				UpdateOutputExtensionComboBoxItems();
			}
			else
			{
				CONFIG.OutputExtension = ".png";
			}
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

		private void InitializeShouldIncludeSubfolders()
		{
			IncludeSubfoldersCheckBox.Checked = CONFIG.ShouldIncludeSubfolders;
			MaintainFolderStructureCheckBox.Visible = !CONFIG.ShouldReplaceOriginals && CONFIG.ShouldIncludeSubfolders;
			MaintainFolderStructureCheckBox.Checked = CONFIG.ShouldMaintainFolderStructure;
		}

		private void InitializeShouldIncludePSDs()
		{
			IncludePSDsCheckBox.Checked = CONFIG.ValidInputExtensions.Contains(".psd");
		}

		private void InitializeMagickCommandString()
		{
			if ( VerifyMagickCommandIsValid() )
			{
				MagickCommandTextBox.Text = CONFIG.MagickCommandString;
			}
			else
			{
				CONFIG.MagickCommandString = ConstructMagickCommandString();
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
				CONFIG.PingoCommandString = ConstructPingoCommandString();
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

		private void MainForm_ResizeEnd(object sender, EventArgs e)
		{
			CONFIG.LastWindowSize = Size;
			CONFIG.LastWindowLocation = Location;
			CONFIG.ShouldStartMaximized = WindowState == FormWindowState.Maximized;
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
			SourceFolderPathTextBox.Text = IS_UNIX ? OpenFolderDialogUnix() : OpenFolderDialogWindows();
		}

		private void SourceFolderPathTextBox_TextChanged(object sender, EventArgs e)
		{
			CONFIG.SourceFolderPath = SourceFolderPathTextBox.Text;

			if ( FileOps.DoesDirectoryExist(SourceFolderPathTextBox.Text) )
			{
				BuildFilesInSourceFolderList();
			}
			else
			{
				ClearFilesInSourceFolderList();
			}

			if ( (CONFIG.ShouldUseSourceFolderAsOutputFolder || CONFIG.ShouldReplaceOriginals) && CONFIG.OutputFolderPath != SourceFolderPathTextBox.Text )
			{
				OutputFolderPathTextBox.Text = SourceFolderPathTextBox.Text;
			}

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
			OutputFolderPathTextBox.Text = IS_UNIX ? OpenFolderDialogUnix() : OpenFolderDialogWindows();
		}

		private void OutputFolderPathTextBox_TextChanged(object sender, EventArgs e)
		{
			OutputFolderPathButton.Enabled = !CONFIG.ShouldUseSourceFolderAsOutputFolder;
			OutputFolderPathTextBox.Enabled = !CONFIG.ShouldUseSourceFolderAsOutputFolder;

			if ( CONFIG.ShouldUseSourceFolderAsOutputFolder && CONFIG.OutputFolderPath != SourceFolderPathTextBox.Text )
			{
				OutputFolderPathTextBox.Text = SourceFolderPathTextBox.Text;
			}

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

			if ( CONFIG.ShouldReplaceOriginals )
			{
				OutputFolderPathTextBox.Text = SourceFolderPathTextBox.Text;
			}

			OutputFolderPathButton.Enabled = !CONFIG.ShouldReplaceOriginals && !CONFIG.ShouldUseSourceFolderAsOutputFolder;
			OutputFolderPathTextBox.Enabled = !CONFIG.ShouldReplaceOriginals && !CONFIG.ShouldUseSourceFolderAsOutputFolder;
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

		private static string OpenFolderDialogUnix()
		{
			using FolderBrowserDialog folderBrowserDialog = new();

			return folderBrowserDialog.ShowDialog() == DialogResult.OK ? folderBrowserDialog.SelectedPath : "";
		}

		private static string OpenFolderDialogWindows()
		{
			using CommonOpenFileDialog folderBrowserDialog = new()
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
			PingoStripCheckBox.Checked = !IS_UNIX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			UpdateShouldAvoidMagickPNGCompression();
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
			MagickCommandTextBox.Text = CONFIG.MagickCommandString;
			PingoCommandTextBox.Text = CONFIG.PingoCommandString;
			UpdateShouldAvoidMagickPNGCompression();
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
			PingoStripCheckBox.Checked = !IS_UNIX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			UpdateShouldAvoidMagickPNGCompression();
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
			PingoStripCheckBox.Checked = !IS_UNIX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			UpdateShouldAvoidMagickPNGCompression();
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
			PingoStripCheckBox.Checked = !IS_UNIX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			UpdateShouldAvoidMagickPNGCompression();
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
			PingoStripCheckBox.Checked = !IS_UNIX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			UpdateShouldAvoidMagickPNGCompression();
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
			PingoStripCheckBox.Checked = !IS_UNIX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			UpdateShouldAvoidMagickPNGCompression();
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
			PingoStripCheckBox.Checked = !IS_UNIX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			UpdateShouldAvoidMagickPNGCompression();
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
			PingoStripCheckBox.Checked = !IS_UNIX;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
			UpdateShouldAvoidMagickPNGCompression();
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
				UpdateShouldAvoidMagickPNGCompression();
			}
		}

		private void UpdateShouldAvoidMagickPNGCompression()
		{
			bool previousValue = CONFIG.ShouldAvoidMagickPNGCompression;

			// Magick's PNG compression can be avoided in any case where we plan on using pingo
			CONFIG.ShouldAvoidMagickPNGCompression = IsPingoPNGOptimizationGoingToBeUsed();

			if ( CONFIG.ShouldAvoidMagickPNGCompression != previousValue )
			{
				MagickCommandTextBox.Text = ConstructMagickCommandString();
			}
		}

		private bool IsPingoPNGOptimizationGoingToBeUsed()
		{
			return !string.IsNullOrWhiteSpace(PingoCommandTextBox.Text)
					&& CONFIG.OutputExtension.ToLowerInvariant() == ".png"
					&& !string.IsNullOrWhiteSpace(CONFIG.PingoOptimizeLevel)
					&& PINGO_OPTIMIZE_OPTIONS_REGEX.IsMatch(PingoCommandTextBox.Text);
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
			listBox.BeginUpdate();
			listBox.ClearSelected();
			listBox.TopIndex = previousTopIndex;
			listBox.EndUpdate();
		}

		private static void SelectAllInListBox(ListBox listBox)
		{
			int previousTopIndex = listBox.TopIndex;
			listBox.BeginUpdate();
			listBox.Select();
			SendKeys.Send("{HOME}+{END}");
			SendKeys.Flush();
			listBox.EndUpdate();
			listBox.TopIndex = previousTopIndex;
		}

		private void RefreshFileListButton_Click(object sender, EventArgs e)
		{
			BuildFilesInSourceFolderList();
		}

		private void OutputExtensionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			CONFIG.OutputExtension = OutputExtensionComboBox.SelectedItem?.ToString().ToLowerInvariant();
			UpdateShouldAvoidMagickPNGCompression();
			StartButton.Enabled = VerifyReadyToApply();
		}

		private void IncludeSubfoldersCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldIncludeSubfolders = IncludeSubfoldersCheckBox.Checked;
			MaintainFolderStructureCheckBox.Visible = !CONFIG.ShouldReplaceOriginals && IncludeSubfoldersCheckBox.Checked;
			BuildFilesInSourceFolderList();
		}

		private void IncludePSDsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			if ( IncludePSDsCheckBox.Checked )
			{
				if ( !CONFIG.ValidInputExtensions.Contains(".psd") )
				{
					CONFIG.ValidInputExtensions.Add(".psd");
				}
			}
			else
			{
				CONFIG.ValidInputExtensions.Remove(".psd");
			}

			CONFIG.SaveConfig();
			BuildFilesInSourceFolderList();
		}

		private void FilesListSearchTextBox_TextChanged(object sender, EventArgs e)
		{
			FilesInSourceFolderListDataSource = new List<FileInfoWithSubpath>(FilesInSourceFolderList);
			RemoveNonMatchingFilesInSourceFolderListBox();
			UpdateFilesInSourceFolderListDataSource();
		}

		private void RemoveNonMatchingFilesInSourceFolderListBox()
		{
			if ( FilesListSearchTextBox.Text.Contains('*') )
			{
				try
				{
					Regex regex = new(Regex.Escape(FilesListSearchTextBox.Text).Replace(@"\*", ".*").Replace(@"\?", "."));

					for ( int i = FilesInSourceFolderListDataSource.Count - 1; i >= 0; --i )
					{
						if ( !regex.IsMatch(FilesInSourceFolderListDataSource[i].ImageInfo.Name) )
						{
							FilesInSourceFolderListDataSource.RemoveAt(i);
						}
					}

					return;
				}
				catch ( Exception )
				{
					// ignored
				}
			}

			for ( int i = FilesInSourceFolderListDataSource.Count - 1; i >= 0; --i )
			{
				if ( !FilesInSourceFolderListDataSource[i].ImageInfo.Name.Contains(FilesListSearchTextBox.Text) )
				{
					FilesInSourceFolderListDataSource.RemoveAt(i);
				}
			}
		}

		private void BuildFilesInSourceFolderList()
		{
			if ( !FileOps.DoesDirectoryExist(SourceFolderPathTextBox.Text) )
			{
				ClearFilesInSourceFolderList();
			}
			else
			{
				FilesInSourceFolderList = GetImageFilesList(SourceFolderPathTextBox.Text);
				FilesInSourceFolderListDataSource = FilesInSourceFolderList;
				UpdateFilesInSourceFolderListDataSource();
			}

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}

		private void ClearFilesInSourceFolderList()
		{
			if ( FilesInSourceFolderListDataSource == null )
			{
				return;
			}

			FilesInSourceFolderListDataSource.Clear();
			UpdateFilesInSourceFolderListDataSource();
		}

		private void UpdateFilesInSourceFolderListDataSource()
		{
			FilesInSourceFolderListBox.BeginUpdate();
			FilesInSourceFolderListBox.DataSource = FilesInSourceFolderListDataSource;
			FilesInSourceFolderListBox.SelectedIndex = -1;
			FilesInSourceFolderListBox.EndUpdate();
		}

		private static List<FileInfoWithSubpath> GetImageFilesList(string imageFilesFullPath)
		{
			List<FileInfoWithSubpath> imageFilesWithSubpaths = new();

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
				.Where(s => CONFIG.ValidInputExtensions.Any(x => string.Equals(x, s.Extension, StringComparison.OrdinalIgnoreCase)));
		}

		private static string GetSubpathFromFileInfo(FileInfo fileInfo, string workingPath)
		{
			string subpath = fileInfo.FullName.Substring(workingPath.Length);
			return (subpath[0] == '\\' || subpath[0] == '/') ? subpath.Substring(1) : subpath;
		}

		private void FilesInSourceFolderListBox_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
		}

		private void FilesInSourceFolderListBox_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

			if ( !files.Any() )
			{
				return;
			}

			string firstFile = files[0];

			if ( FileOps.DoesDirectoryExist(firstFile) )
			{
				SourceFolderPathTextBox.Text = firstFile;
			}
			else if ( FileOps.DoesFileExist(firstFile) )
			{
				SourceFolderPathTextBox.Text = new FileInfo(firstFile).DirectoryName;

				FilesInSourceFolderListBox.BeginUpdate();
				UnselectAllItemsInListBox(FilesInSourceFolderListBox);
				Array.Sort(files);

				foreach ( FileInfo filename in files.Select(f => new FileInfo(f)) )
				{
					FilesInSourceFolderListBox.SetSelected(FilesInSourceFolderListBox.FindStringExact(filename.Name), true);
				}

				FilesInSourceFolderListBox.EndUpdate();
			}
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
			FileOps.OpenPathToFileInExplorer(fileInfo.DirectoryName, fileInfo.Name);
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

		private static string GetStringWithOnlyDigits(string str) => new(str.Where(char.IsDigit).ToArray());

		#endregion

		#region CommandsUI

		private string ConstructMagickCommandString()
		{
			EnsureMagickConfigValuesAreUpdated();

			StringBuilder commandBuilder = new(244);
			commandBuilder.Append("mogrify -format " + CONFIG.OutputExtension.Substring(1) + " -path \"%1\" ");

			if ( CONFIG.ShouldAvoidMagickPNGCompression )
			{
				commandBuilder.Append("-quality 10 ");
			}

			if ( !string.IsNullOrWhiteSpace(CONFIG.MagickDither) )
			{
				commandBuilder.Append(CONFIG.MagickDither + " ");
			}

			if ( !string.IsNullOrWhiteSpace(CONFIG.MagickColorspace) )
			{
				commandBuilder.Append(CONFIG.MagickColorspace + " ");
			}

			if ( CONFIG.MagickColorsValue > 0 )
			{
				commandBuilder.Append("-colors " + CONFIG.MagickColorsValue + " ");
			}

			if ( CONFIG.MagickDepthValue > 0 )
			{
				commandBuilder.Append("-depth " + CONFIG.MagickDepthValue + " ");
			}

			if ( CONFIG.MagickPosterizeValue > 0 )
			{
				commandBuilder.Append("-posterize " + CONFIG.MagickPosterizeValue + " ");
			}

			if ( CONFIG.ShouldUseMagickNormalize )
			{
				commandBuilder.Append("-normalize ");
			}

			if ( CONFIG.ShouldUseMagickContrastStretch )
			{
				commandBuilder.Append("-contrast-stretch 0%x0% ");
			}

			if ( CONFIG.ShouldUseMagickAutoLevel )
			{
				commandBuilder.Append("-auto-level ");
			}

			commandBuilder.Append("*" + CONFIG.OutputExtension);

			return commandBuilder.ToString();
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
			EnsurePingoConfigValuesAreUpdated();

			StringBuilder commandBuilder = new(IS_UNIX ? 52 : 44);

			if ( IS_UNIX )
			{
				commandBuilder.Append("-c 'pingo ");
			}

			if ( CONFIG.PingoPNGPaletteValue > 0 && CONFIG.PingoPNGPaletteValue <= 100 )
			{
				commandBuilder.Append("-pngpalette=" + CONFIG.PingoPNGPaletteValue + " ");

				if ( CONFIG.ShouldUsePingoNoDithering )
				{
					commandBuilder.Append("-nodithering ");
				}
			}

			commandBuilder.Append(CONFIG.PingoOptimizeLevel + " ");

			if ( CONFIG.ShouldUsePingoStrip )
			{
				commandBuilder.Append("-strip ");
			}

			commandBuilder.Append(IS_UNIX ? "%1" : "\"%1\"");

			if ( IS_UNIX )
			{
				commandBuilder.Append("'");
			}

			return commandBuilder.ToString();
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
			CONFIG.MagickCommandString = string.IsNullOrWhiteSpace(MagickCommandTextBox.Text) ? ConstructMagickCommandString() : MagickCommandTextBox.Text;
			StartButton.Enabled = VerifyReadyToApply();
		}

		private void PingoCommandTextBox_TextChanged(object sender, EventArgs e)
		{
			CONFIG.PingoCommandString = string.IsNullOrWhiteSpace(PingoCommandTextBox.Text) ? ConstructPingoCommandString() : PingoCommandTextBox.Text;
			UpdateShouldAvoidMagickPNGCompression();
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
			List<ImgInfo> imgFiles = FilesInSourceFolderListBox.SelectedItems.Cast<FileInfoWithSubpath>().Select(f => new ImgInfo(f.ImageInfo)).ToList();

			if ( !imgFiles.Any() )
			{
				StatusMessageLabel.Text = "No valid files in the folder selected.";
				ToggleUI(true);
				ShouldCancelProcessing = false;
				return;
			}

			string tempFolderPath = Path.GetTempPath() + TEMP_FOLDER_NAME;
			Stopwatch stopwatch = Stopwatch.StartNew();
			InitializeProcessing(imgFiles, tempFolderPath);
			ProcessFiles(ref imgFiles, tempFolderPath);
			FinalizeProcessing(imgFiles, tempFolderPath);
			stopwatch.Stop();

			UpdateStatusMessageForEndProcessing(stopwatch.ElapsedMilliseconds, tempFolderPath);
			BuildFilesInSourceFolderList();
			ToggleUI(true);
			ShouldCancelProcessing = false;
		}

		private void ToggleUI(bool isActive)
		{
			MagickCommandTextBox.Enabled = isActive;
			PingoCommandTextBox.Enabled = isActive;
			SourceFolderPathTextBox.Enabled = isActive;
			SourceFolderPathButton.Enabled = isActive;
			OutputFolderPathTextBox.Enabled = !CONFIG.ShouldReplaceOriginals && !CONFIG.ShouldUseSourceFolderAsOutputFolder;
			OutputFolderPathButton.Enabled = !CONFIG.ShouldReplaceOriginals && !CONFIG.ShouldUseSourceFolderAsOutputFolder;
			DeleteOriginalsAfterCheckBox.Enabled = isActive;
			ReplaceOriginalsCheckBox.Enabled = isActive;
			MaintainFolderStructureCheckBox.Enabled = isActive;
			MaintainFolderStructureCheckBox.Visible = !CONFIG.ShouldReplaceOriginals && CONFIG.ShouldIncludeSubfolders;
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

		private void UpdateStatusMessageForEndProcessing(long elapsedMilliseconds, string tempFolderPath)
		{
			if ( ShouldCancelProcessing )
			{
				if ( MessageBox.Show("Since processing was canceled, would you like to open the path to the temporary folder to view the files?",
					"Open path to temporary folder?",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question) == DialogResult.Yes )
				{
					FileOps.OpenFolderPathInExplorer(Directory.Exists(tempFolderPath) ? tempFolderPath : Path.GetTempPath());
				}

				StatusMessageLabel.Text = "Command(s) canceled. Some files may have already been processed, or may be in an invalid state.";
			}
			else
			{
				StatusMessageLabel.Text = "Command(s) completed. Total time elapsed: " + TimeSpan.FromMilliseconds(elapsedMilliseconds).ToString(@"hh\:mm\:ss");
			}
		}

		#region Processing Initialization

		private void InitializeProcessing(List<ImgInfo> imgFiles, string tempFolderPath)
		{
			if ( ShouldCancelProcessing )
			{
				return;
			}

			ProcessingProgressBar.Maximum = (imgFiles.Count * 3) + imgFiles.Where(f => f.IsOrigPSD()).ToList().Count;
			InitializeTagsStrings();

			if ( !ShouldCancelProcessing && !FileOps.DeleteFilesInFolder(tempFolderPath) )
			{
				ShouldCancelProcessing = true;
			}
		}

		private static void InitializeTagsStrings()
		{
			if ( ShouldCancelProcessing )
			{
				return;
			}

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

			if ( CONFIG.ShouldOutputToNewFolder && !CONFIG.ShouldReplaceOriginals )
			{
				string fullPath = CONFIG.OutputFolderPath + "/" + CONFIG.NewOutputFolderBaseName;

				if ( CONFIG.ShouldAddTagsToOutputFolder )
				{
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

		#endregion

		#region Main Processing

		private void ProcessFiles(ref List<ImgInfo> imgFiles, string tempFolderPath)
		{
			if ( ShouldCancelProcessing )
			{
				return;
			}

			MagickCopyFilesToTempFolder(imgFiles, tempFolderPath);
			UpdateFileInfosToTempFolder(ref imgFiles, tempFolderPath, out long previousTotalFilesize);
			RenameCopiedFiles(ref imgFiles);
			ProcessingProgressBar.Value += imgFiles.Count;

			if ( !ShouldCancelProcessing )
			{
				PreviousSizeLabel.Text = "Prev Size: " + GetBytesAsReadableString(previousTotalFilesize);
			}

			ProcessMagickCommand(tempFolderPath);
			ProcessingProgressBar.Value += imgFiles.Count;
			ProcessPingoCommand(tempFolderPath);
			ProcessingProgressBar.Value += imgFiles.Count;

			if ( !ShouldCancelProcessing )
			{
				long newTotalFilesize = 0;
				NewSizeLabel.Text = "New Size: " + GetTotalSizeOfFiles(imgFiles, ref newTotalFilesize);
				long filesizeDiff = -(newTotalFilesize - previousTotalFilesize);
				TotalSavingsLabel.Text = "Total Savings: " + GetBytesAsReadableString(filesizeDiff) + " or " + GetTotalSavingsPercentage(previousTotalFilesize, filesizeDiff);
			}
		}

		private void MagickCopyFilesToTempFolder(List<ImgInfo> imgFiles, string tempFolderPath)
		{
			if ( ShouldCancelProcessing )
			{
				return;
			}

			StatusMessageLabel.Text = "Copying file(s)/creating hardlink(s) to temp working folder...";
			List<FileInfo> filesToHardLink = new();
			List<FileInfo> filesToCopy = new();

			foreach ( ImgInfo img in imgFiles )
			{
				if ( ShouldFileBeCopied(img.OrigInfo, tempFolderPath) )
				{
					filesToCopy.Add(img.OrigInfo);
				}
				else
				{
					filesToHardLink.Add(img.OrigInfo);
				}
			}

			if ( ShouldCancelProcessing )
			{
				return;
			}

			if ( filesToHardLink.Any() )
			{
				if ( !FileOps.CreateHardLinksToFiles(filesToHardLink, tempFolderPath) )
				{
					ShouldCancelProcessing = true;
					return;
				}
			}

			if ( ShouldCancelProcessing || !filesToCopy.Any() )
			{
				return;
			}

			StartAndWaitForProcess(IS_UNIX ? CONFIG.ImagemagickPathToExe : "magick.exe", CONFIG.SourceFolderPath, "mogrify -format " + CONFIG.OutputExtension.Substring(1) + " -path \"" + tempFolderPath + "\" " + GetOriginalImagePaths(filesToCopy));
		}

		private bool ShouldFileBeCopied(FileInfo img, string tempFolderPath)
		{
			return !string.IsNullOrWhiteSpace(PrependToOutputTextBox.Text)
					|| !string.IsNullOrWhiteSpace(AppendToOutputTextBox.Text)
					|| (CONFIG.ShouldAddTagsToFilenames && !string.IsNullOrWhiteSpace(CONFIG.TagsStringToAppendToFilenames))
					|| img.Extension.ToLowerInvariant() != CONFIG.OutputExtension
					|| Path.GetPathRoot(img.FullName) != Path.GetPathRoot(tempFolderPath);
		}

		private string GetOriginalImagePaths(List<FileInfo> imgFiles)
		{
			StringBuilder argumentBuilder = new((imgFiles.Max(f => f.FullName).Length * imgFiles.Count) + (imgFiles.Count * 6));

			foreach ( FileInfo img in imgFiles )
			{
				argumentBuilder.Append("\"");
				argumentBuilder.Append(img.FullName);

				if ( IsPSD(img) )
				{
					argumentBuilder.Append("[0]");
				}

				argumentBuilder.Append("\" ");
			}

			--argumentBuilder.Length; // Remove the last space character
			return argumentBuilder.ToString();
		}

		private void UpdateFileInfosToTempFolder(ref List<ImgInfo> imgFiles, string tempFolderPath, out long previousTotalFilesize)
		{
			previousTotalFilesize = 0;

			if ( ShouldCancelProcessing )
			{
				return;
			}

			// Update the FileInfos to the new output location in the temp folder (and new file extensions if applicable)
			foreach ( ImgInfo imgInfo in imgFiles )
			{
				FileInfo img = imgInfo.OrigInfo;
				bool isOriginallyPSD = imgInfo.IsOrigPSD();

				if ( !isOriginallyPSD )
				{
					previousTotalFilesize += img.Length;
				}

				imgInfo.NewInfo = new FileInfo(tempFolderPath + (img.Extension == CONFIG.OutputExtension
											? img.Name : img.Name.Replace(img.Extension, CONFIG.OutputExtension)));

				if ( isOriginallyPSD )
				{
					previousTotalFilesize += imgInfo.NewInfo.Length;
				}
			}
		}

		private void RenameCopiedFiles(ref List<ImgInfo> imgFiles)
		{
			if ( ShouldCancelProcessing || !ShouldRenameFiles() )
			{
				return;
			}

			StatusMessageLabel.Text = "Renaming file(s) to final filename(s)...";
			string appendString = AppendToOutputTextBox.Text + (CONFIG.ShouldAddTagsToFilenames ? CONFIG.TagsStringToAppendToFilenames : "");

			foreach ( ImgInfo imgInfo in imgFiles )
			{
				FileInfo img = imgInfo.NewInfo;
				string newName = PrependToOutputTextBox.Text + img.Name.Replace(img.Extension, "") + appendString + img.Extension;
				FileOps.RenameFile(img, newName);
				imgInfo.NewInfo = new FileInfo(img.DirectoryName + "/" + newName);
			}
		}

		private bool ShouldRenameFiles()
		{
			return !string.IsNullOrWhiteSpace(PrependToOutputTextBox.Text)
					|| !string.IsNullOrWhiteSpace(AppendToOutputTextBox.Text)
					|| CONFIG.ShouldAddTagsToFilenames && !string.IsNullOrWhiteSpace(CONFIG.TagsStringToAppendToFilenames);
		}

		private void ProcessMagickCommand(string tempFolderPath)
		{
			if ( ShouldCancelProcessing || !VerifyMagickCommandIsReadyAndValid() || IsDefaultMagickCommand(CONFIG.MagickCommandString) )
			{
				return;
			}

			StatusMessageLabel.Text = "Processing magick command on file(s)...";
			StartAndWaitForProcess(IS_UNIX ? CONFIG.ImagemagickPathToExe : "magick.exe", tempFolderPath, CONFIG.MagickCommandString.Replace("%1", tempFolderPath));
		}

		private static bool IsDefaultMagickCommand(string commandString)
		{
			const string DEFAULT_COMMAND = "convert \"%1\" \"%2\"";
			const string DEFAULT_COMMAND_WITH_PINGO = "convert \"%1\" -quality 10 \"%2\"";

			return commandString == DEFAULT_COMMAND_WITH_PINGO || commandString == DEFAULT_COMMAND;
		}

		private void ProcessPingoCommand(string tempFolderPath)
		{
			if ( ShouldCancelProcessing || string.IsNullOrWhiteSpace(CONFIG.PingoPathToExe) || !VerifyPingoCommandIsReadyAndValid() )
			{
				return;
			}

			StatusMessageLabel.Text = "Processing pingo command on file(s)...";
			StartAndWaitForProcess(IS_UNIX ? "sh" : "pingo.exe", tempFolderPath, CONFIG.PingoCommandString.Replace("%1", IS_UNIX ? "*" + CONFIG.OutputExtension : tempFolderPath));
		}

		private void FinalizeProcessing(List<ImgInfo> imgFiles, string tempFolderPath)
		{
			if ( ShouldCancelProcessing )
			{
				return;
			}

			MoveFilesToFinalLocation(imgFiles);
			ProcessDeletingOriginalFiles(imgFiles);

			if ( ShouldCancelProcessing )
			{
				return;
			}

			FileOps.DeleteFolder(tempFolderPath, true);
		}

		private void MoveFilesToFinalLocation(List<ImgInfo> imgFiles)
		{
			if ( ShouldCancelProcessing )
			{
				return;
			}

			StatusMessageLabel.Text = "Moving file(s) to final location(s)...";

			foreach ( ImgInfo imgInfo in imgFiles )
			{
				if ( ShouldCancelProcessing )
				{
					return;
				}

				string newFilepath = DetermineOutputFilepath(imgInfo);
				ShouldCancelProcessing = !FileOps.Move(imgInfo.NewInfo.FullName, newFilepath);

				if ( !ShouldCancelProcessing )
				{
					imgInfo.NewInfo = new FileInfo(newFilepath);
				}
			}
		}

		private void ProcessDeletingOriginalFiles(List<ImgInfo> imgFiles)
		{
			if ( ShouldCancelProcessing || !CONFIG.ShouldDeleteOriginals )
			{
				return;
			}

			StatusMessageLabel.Text = "Deleting original file(s)...";

			foreach ( ImgInfo imgInfo in imgFiles )
			{
				if ( ShouldCancelProcessing )
				{
					break;
				}

				imgInfo.OrigInfo.Refresh();
				imgInfo.NewInfo.Refresh();

				if ( !imgInfo.IsOrigPSD() && FileOps.DoesFileExist(imgInfo.OrigInfo.FullName) && !imgInfo.AreFilesTheSame() )
				{
					FileOps.DeleteFile(imgInfo.OrigInfo.FullName);
					break;
				}
			}
		}

		#endregion

		#region Processing Utility Funcs

		private static void StartAndWaitForProcess(string executableName, string workingDirPath, string arguments)
		{
			if ( ShouldCancelProcessing )
			{
				return;
			}

			using Process process = Process.Start(new ProcessStartInfo
			{
				FileName = executableName,
				UseShellExecute = false,
				CreateNoWindow = true,
				WorkingDirectory = workingDirPath,
				Arguments = arguments
			});

			if ( process == null )
			{
				if ( DisplayProcessIsNullError(true) )
				{
					ShouldCancelProcessing = true;
				}

				return;
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
			}
		}

		private static string DetermineOutputFilepath(ImgInfo imgInfo)
		{
			FileInfo img = imgInfo.NewInfo;

			if ( CONFIG.ShouldReplaceOriginals )
			{
				return imgInfo.OrigInfo.DirectoryName + "/" + img.Name;
			}

			if ( CONFIG.ShouldMaintainFolderStructure && CONFIG.ShouldIncludeSubfolders )
			{
				return DetermineSubfolderPath(imgInfo.OrigInfo) + "/" + img.Name;
			}

			if ( CONFIG.ShouldOutputToNewFolder )
			{
				return CONFIG.NewOutputFolderPath + img.Name;
			}

			return CONFIG.OutputFolderPath + "/" + img.Name;
		}

		private static string DetermineSubfolderPath(FileInfo fileInfo)
		{
			string outputFolderName = CONFIG.OutputFolderPath + fileInfo.DirectoryName?.Replace(CONFIG.SourceFolderPath, "");
			Directory.CreateDirectory(outputFolderName);

			return outputFolderName;
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

		private static string GetTotalSizeOfFiles(List<ImgInfo> imgFiles, ref long totalFilesizeInBytes)
		{
			foreach ( ImgInfo file in imgFiles )
			{
				file.NewInfo.Refresh(); // Required to ensure the FileInfo is up to date at this moment
				totalFilesizeInBytes += file.NewInfo.Length;
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

		#endregion

		#region DarkRenderer

		private class DarkContextMenuRenderer : ToolStripProfessionalRenderer
		{
			public DarkContextMenuRenderer() : base(new DarkContextMenuColors())
			{
			}

			protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
			{
				Rectangle rect = new(Point.Empty, e.Item.Size);
				using SolidBrush brush = new(MENU_COLOR_OPTION);
				e.Graphics.FillRectangle(brush, rect);
				using Pen pen = new(Color.Black);
				int y = rect.Height / 2;
				e.Graphics.DrawLine(pen, rect.Left, y, rect.Right - 1, y);
			}

			protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
			{
				Rectangle rect = new(Point.Empty, e.Item.Size);
				Color color = e.Item.Selected ? MENU_COLOR_OPTION_HIGHLIGHTED : MENU_COLOR_OPTION;

				using SolidBrush brush = new(color);
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
			FileOps.OpenFileInDefaultApplication(UserConfig.PathToFile);
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

		private void EditValidInputExtensionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using EditListItemsDialog dialog = new(CONFIG.ValidInputExtensions, Location);

			if ( dialog.ShowDialog() == DialogResult.OK )
			{
				CONFIG.ValidInputExtensions = new List<string>(dialog.ListItems);
			}
		}

		private void EditValidOutputExtensionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using EditListItemsDialog dialog = new(CONFIG.ValidOutputExtensions, Location);

			if ( dialog.ShowDialog() == DialogResult.OK )
			{
				CONFIG.ValidOutputExtensions = new List<string>(dialog.ListItems);
				UpdateOutputExtensionComboBoxItems();
			}
		}

		private void UpdateOutputExtensionComboBoxItems()
		{
			OutputExtensionComboBox.BeginUpdate();
			OutputExtensionComboBox.Items.Clear();
			OutputExtensionComboBox.Items.AddRange(CONFIG.ValidOutputExtensions.ToArray());
			OutputExtensionComboBox.SelectedIndex = GetIndexOfStringInComboBox(OutputExtensionComboBox, CONFIG.OutputExtension);
			OutputExtensionComboBox.EndUpdate();
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

		private void UseSourceDirAsOutputDirToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CONFIG.ShouldUseSourceFolderAsOutputFolder = UseSourceDirAsOutputDirToolStripMenuItem.Checked;
			OutputFolderPathButton.Enabled = !CONFIG.ShouldUseSourceFolderAsOutputFolder && !CONFIG.ShouldReplaceOriginals;
			OutputFolderPathTextBox.Enabled = !CONFIG.ShouldUseSourceFolderAsOutputFolder && !CONFIG.ShouldReplaceOriginals;

			if ( CONFIG.ShouldUseSourceFolderAsOutputFolder )
			{
				OutputFolderPathTextBox.Text = SourceFolderPathTextBox.Text;
			}
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
