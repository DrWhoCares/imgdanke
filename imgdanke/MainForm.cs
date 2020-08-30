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

namespace imgdanke
{
	public partial class MainForm : Form
	{

		private static string MAGICK_FILENAME = "magick.exe";
		private static string PINGO_FILENAME = "pingo.exe";

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
			new KeyValuePair<string, string>("136c", "64"),
			new KeyValuePair<string, string>("160c", "69"),
			new KeyValuePair<string, string>("208c", "74"),
			new KeyValuePair<string, string>("184c", "79"),
			new KeyValuePair<string, string>("208c", "84"),
			new KeyValuePair<string, string>("232c", "89"),
			new KeyValuePair<string, string>("256c", "100")
		};

		private static readonly bool IS_LINUX = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
		private static readonly UserConfig CONFIG = UserConfig.LoadConfig();
		private static readonly Regex INVALID_FILENAME_CHARS_REGEX = new Regex("[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]");
		private static bool ShouldCancelProcessing;
		private static bool IsInitializing;
		private static bool ShouldDelayUpdatingCommands;
		private static bool EnableFormLevelDoubleBuffering = true;
		private static int OriginalExStyle = -1;

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
			InitializeWindowSettings();
			InitializeWithCommandLineArgs(args);
			CheckForProgramUpdates();
			InitializeBinaryFilenames();
			InitializePingoPNGPaletteComboBox();
			InitializeWithConfigValues();
			IsInitializing = false;
		}

		#region Initialization

		#region InitFuncs

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

		private static async void CheckForProgramUpdates()
		{
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

				DialogResult result = MessageBox.Show("There is a new version (" + check.LastVersion + ") available for download. Would you like to download and install it?", "New Version Update", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

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
				MessageBox.Show("Checking for updates threw an exception:\n\"" + e.Message + "\"\n\nYou may not be able to access api.github.com. You can safely continue using this offline.", "Error Checking For Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private static void InitializeBinaryFilenames()
		{
			if ( IS_LINUX )
			{
				MAGICK_FILENAME = "magick";
				PINGO_FILENAME = "pingo";
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

		#endregion

		#region Verification

		private static bool VerifyImagemagickPathIsValid()
		{
			return File.Exists(CONFIG.ImagemagickPathToExe) && CONFIG.ImagemagickPathToExe.Contains(MAGICK_FILENAME);
		}

		private static bool VerifyPingoPathIsValid()
		{
			return File.Exists(CONFIG.PingoPathToExe) && CONFIG.PingoPathToExe.Contains(PINGO_FILENAME);
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

				if ( File.Exists(fullPath) )
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

			if ( File.Exists(fullPath) )
			{
				return fullPath;
			}

			foreach ( string path in new DirectoryInfo(localPath).EnumerateDirectories("*", SearchOption.AllDirectories).Select(d => d.FullName) )
			{
				fullPath = Path.Combine(path, filename);

				if ( File.Exists(fullPath) )
				{
					return fullPath;
				}
			}

			return "";
		}

		private static bool VerifySourceFolderPathIsValid()
		{
			return Directory.Exists(CONFIG.SourceFolderPath);
		}

		private static bool VerifyOutputFolderPathIsValid()
		{
			return Directory.Exists(CONFIG.OutputFolderPath);
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

		private static bool VerifyReadyToApply()
		{
			return VerifyOutputSettingsAreValid()
				&& (VerifyMagickCommandIsReadyAndValid() || VerifyPingoCommandIsReadyAndValid());
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
			CONFIG.SourceFolderPath = SourceFolderPathTextBox.Text;
			BuildFilesInSourceFolderList();
			ApplyButton.Enabled = VerifyReadyToApply();
		}

		private void FilesInSourceFolderListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
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
			ApplyButton.Enabled = VerifyReadyToApply();
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

			if ( Directory.Exists(files.First()) )
			{
				((TextBox)sender).Text = files.First();
			}
			else if ( File.Exists(files.First()) )
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
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
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
			PingoPNGPaletteComboBox.SelectedIndex = PNG_PALETTE_ITEMS.ToList().FindIndex(i => i.Value == CONFIG.PingoPNGPaletteValue.ToString());
			PingoNoDitheringCheckBox.Checked = CONFIG.ShouldUsePingoNoDithering;
			PingoSBRadioButton.Checked = CONFIG.PingoAdditionalChecks == PingoAdditionalChecks.sb;
			PingoSARadioButton.Checked = CONFIG.PingoAdditionalChecks == PingoAdditionalChecks.sa;
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
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
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
			MagickNormalizeCheckBox.Checked = true;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
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
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
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
			MagickNormalizeCheckBox.Checked = true;
			PingoPNGPaletteComboBox.SelectedIndex = PNG_PALETTE_ITEMS.ToList().FindIndex(i => i.Value == "24");
			PingoNoDitheringCheckBox.Enabled = true;
			PingoNoDitheringCheckBox.Checked = false;
			PingoSBRadioButton.Enabled = true;
			PingoSBRadioButton.Checked = true;
			PingoSARadioButton.Enabled = true;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
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
			MagickNormalizeCheckBox.Checked = true;
			PingoPNGPaletteComboBox.SelectedIndex = PingoPNGPaletteComboBox.Items.Count - 1;
			PingoNoDitheringCheckBox.Enabled = true;
			PingoNoDitheringCheckBox.Checked = true;
			PingoSBRadioButton.Enabled = true;
			PingoSBRadioButton.Checked = true;
			PingoSARadioButton.Enabled = true;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
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
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
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
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoNoDitheringCheckBox.Checked = false;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
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
			PingoSBRadioButton.Enabled = value > 0;
			PingoSARadioButton.Enabled = value > 0;
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

		private void PingoSBRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing || !PingoSBRadioButton.Checked )
			{
				return;
			}

			CONFIG.PingoAdditionalChecks = PingoAdditionalChecks.sb;

			if ( !ShouldDelayUpdatingCommands )
			{
				PingoCommandTextBox.Text = ConstructPingoCommandString();
			}
		}

		private void PingoSARadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing || !PingoSARadioButton.Checked )
			{
				return;
			}

			CONFIG.PingoAdditionalChecks = PingoAdditionalChecks.sa;

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
			CONFIG.OutputExtension = OutputExtensionTextBox.Text;
			ApplyButton.Enabled = VerifyReadyToApply();
		}

		private void IncludeSubfoldersCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldIncludeSubfolders = IncludeSubfoldersCheckBox.Checked;
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
			if ( !Directory.Exists(SourceFolderPathTextBox.Text) )
			{
				return;
			}

			FilesInSourceFolderListBox.DataSource = GetImageFilesList(SourceFolderPathTextBox.Text);
			FilesInSourceFolderListBox.DisplayMember = "Subpath";
			FilesInSourceFolderListBox.ValueMember = "Subpath";
			FilesInSourceFolderListBox.SelectedIndex = -1;

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
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
			const string MAGICK_COMMAND_PREFIX = "magick convert \"%1\" ";
			const string MAGICK_COMMAND_PREFIX_LINUX = "convert \"%1\" ";
			const string MAGICK_COMMAND_SUFFIX = "\"%2\"";
			string command = IS_LINUX ? MAGICK_COMMAND_PREFIX_LINUX : MAGICK_COMMAND_PREFIX;

			EnsureMagickConfigValuesAreUpdated();

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
			const string PINGO_COMMAND_PREFIX = "pingo ";
			const string PINGO_COMMAND_SUFFIX = "\"%1\"";
			string command = IS_LINUX ? "" : PINGO_COMMAND_PREFIX;

			EnsurePingoConfigValuesAreUpdated();

			if ( CONFIG.PingoPNGPaletteValue > 0 && CONFIG.PingoPNGPaletteValue <= 100 )
			{
				command += "-pngpalette=" + CONFIG.PingoPNGPaletteValue + " ";

				if ( CONFIG.ShouldUsePingoNoDithering )
				{
					command += "-nodithering ";
				}

				switch ( CONFIG.PingoAdditionalChecks )
				{
					case PingoAdditionalChecks.None:
						break;
					case PingoAdditionalChecks.sb:
						command += "-sb ";
						break;
					case PingoAdditionalChecks.sa:
						command += "-sa ";
						break;

					default: throw new ArgumentOutOfRangeException();
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

			if ( PingoSBRadioButton.Checked )
			{
				CONFIG.PingoAdditionalChecks = PingoAdditionalChecks.sb;
			} 
			else if ( PingoSARadioButton.Checked )
			{
				CONFIG.PingoAdditionalChecks = PingoAdditionalChecks.sa;
			}
			else
			{
				CONFIG.PingoAdditionalChecks = PingoAdditionalChecks.None;
			}

			CONFIG.PingoOptimizeLevel = PingoOptimizationLevelComboBox.SelectedItem == null ? "" : PingoOptimizationLevelComboBox.SelectedItem.ToString();
			CONFIG.ShouldUsePingoStrip = PingoStripCheckBox.Checked;
		}

		private void MagickCommandTextBox_TextChanged(object sender, EventArgs e)
		{
			CONFIG.MagickCommandString = string.IsNullOrWhiteSpace(MagickCommandTextBox.Text) ? "" : MagickCommandTextBox.Text;
			ApplyButton.Enabled = VerifyReadyToApply();
		}

		private void PingoCommandTextBox_TextChanged(object sender, EventArgs e)
		{
			CONFIG.PingoCommandString = string.IsNullOrWhiteSpace(PingoCommandTextBox.Text) ? "" : PingoCommandTextBox.Text;
			ApplyButton.Enabled = VerifyReadyToApply();
		}

		private void ProcessingCancelButton_Click(object sender, EventArgs e)
		{
			ShouldCancelProcessing = true;
		}

		#endregion

		#endregion

		#region Processing

		private void ApplyButton_Click(object sender, EventArgs e)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			ToggleUI(false);
			List<FileInfo> imgFiles = FilesInSourceFolderListBox.SelectedItems.Cast<FileInfoWithSubpath>().Select(f => f.ImageInfo).ToList();

			if ( !imgFiles.Any() )
			{
				StatusMessageLabel.Text = "No valid files in the folder selected.";
				ToggleUI(true);
				return;
			}

			long previousTotalFilesize = 0;
			long newTotalFilesize = 0;
			ProcessingProgressBar.Maximum = (imgFiles.Count * 2) + imgFiles.Where(f => f.Extension == ".psd").ToList().Count;

			List<FileInfo> origFiles = CONFIG.ShouldDeleteOriginals ? imgFiles : new List<FileInfo>();

			if ( !ShouldCancelProcessing && CONFIG.ShouldIncludePSDs )
			{
				imgFiles = ConvertAnyPSDs(imgFiles, StatusMessageLabel, ProcessingProgressBar);
			}

			if ( !ShouldCancelProcessing )
			{
				PreviousSizeLabel.Text = "Prev Size: " + GetTotalSizeOfFiles(imgFiles, ref previousTotalFilesize);
			}

			if ( !ShouldCancelProcessing && VerifyMagickCommandIsReadyAndValid() )
			{
				imgFiles = CallMagickCommand(imgFiles, MagickCommandTextBox.Text, PrependToOutputTextBox.Text, AppendToOutputTextBox.Text, StatusMessageLabel, ProcessingProgressBar);
			}

			if ( !string.IsNullOrWhiteSpace(CONFIG.PingoPathToExe) && !ShouldCancelProcessing && VerifyPingoCommandIsReadyAndValid() )
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
				foreach ( FileInfo origFile in origFiles )
				{
					if ( ShouldCancelProcessing )
					{
						break;
					}

					if ( CONFIG.ShouldReplaceOriginals )
					{
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
					}

					if ( File.Exists(origFile.FullName) && origFile.Extension != ".psd" )
					{
						File.Delete(origFile.FullName);
					}
				}
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
			OutputFolderPathTextBox.Enabled = isActive;
			OutputFolderPathButton.Enabled = isActive;
			DeleteOriginalsAfterCheckBox.Enabled = isActive;
			ReplaceOriginalsCheckBox.Enabled = isActive;
			PresetSettingsGroupBox.Enabled = isActive;
			ImagemagickSettingsGroupBox.Enabled = isActive;
			PingoSettingsGroupBox.Enabled = isActive;
			FilesInSourceFolderGroupBox.Enabled = isActive;
			ApplyButton.Enabled = isActive;
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

		private static List<FileInfo> ConvertAnyPSDs(List<FileInfo> originalImgFiles, Label statusLabel, ProgressBar progressBar)
		{
			List<FileInfo> psdFiles = originalImgFiles.Where(f => f.Extension == ".psd").ToList();

			if ( !psdFiles.Any() )
			{
				return originalImgFiles;
			}

			List<FileInfo> newImgFiles = originalImgFiles.Where(f => f.Extension != ".psd").ToList();

			foreach ( FileInfo psdFile in psdFiles )
			{
				ProcessStartInfo startInfo = new ProcessStartInfo
				{
					FileName = IS_LINUX ? CONFIG.ImagemagickPathToExe : "cmd.exe",
					UseShellExecute = false,
					CreateNoWindow = true,
					WorkingDirectory = CONFIG.SourceFolderPath
				};

				string outputFilename = (CONFIG.ShouldReplaceOriginals ? psdFile.DirectoryName : CONFIG.OutputFolderPath) + "/" + psdFile.Name.Replace(psdFile.Extension, "") + CONFIG.OutputExtension;
				startInfo.Arguments = (IS_LINUX ? "" : "/C magick") + " convert \"" + psdFile.FullName + "[0]\" \"" + outputFilename + "\"";
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

				process.Start();

				while ( !process.HasExited )
				{
					Application.DoEvents();

					if ( ShouldCancelProcessing )
					{
						process.Close();
						break;
					}
				}

				if ( ShouldCancelProcessing )
				{
					return new List<FileInfo>();
				}

				newImgFiles.Add(new FileInfo(outputFilename));
				++progressBar.Value;
			}

			return newImgFiles;
		}

		private static List<FileInfo> CallMagickCommand(List<FileInfo> imgFiles, string commandString, string prependString, string appendString, Label statusLabel, ProgressBar progressBar)
		{
			const string DEFAULT_COMMAND = "magick convert \"%1\" \"%2\"";
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = IS_LINUX ? CONFIG.ImagemagickPathToExe : "cmd.exe",
				UseShellExecute = false,
				CreateNoWindow = true,
				WorkingDirectory = CONFIG.SourceFolderPath
			};

			List<FileInfo> newImgFiles = new List<FileInfo>();

			foreach ( FileInfo img in imgFiles )
			{
				if ( commandString == DEFAULT_COMMAND && img.Extension == CONFIG.OutputExtension && img.DirectoryName == CONFIG.OutputFolderPath && string.IsNullOrWhiteSpace(prependString) && string.IsNullOrWhiteSpace(appendString) )
				{
					// Avoid processing the magick command if it won't actually do anything. Still need to process it if the extension would change
					newImgFiles.Add(new FileInfo(img.FullName));
					++progressBar.Value;
					continue;
				}

				string tempFilename = (CONFIG.ShouldReplaceOriginals ? img.DirectoryName : CONFIG.OutputFolderPath) + "/" + prependString + img.Name.Replace(img.Extension, "") + appendString + ".tmp" + CONFIG.OutputExtension;
				startInfo.Arguments = (IS_LINUX ? "" : "/C ") + commandString;
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

				process.Start();

				while ( !process.HasExited )
				{
					Application.DoEvents();

					if ( ShouldCancelProcessing )
					{
						process.Close();
						break;
					}
				}

				if ( ShouldCancelProcessing )
				{
					return new List<FileInfo>();
				}

				while ( !IsFileReady(tempFilename) )
				{
					Application.DoEvents();
				}

				string newLocation = tempFilename.Replace(".tmp", "");

				if ( File.Exists(newLocation) )
				{
					File.Delete(newLocation);
				}

				File.Move(tempFilename, newLocation);
				newImgFiles.Add(new FileInfo(newLocation));
				++progressBar.Value;
			}

			return newImgFiles;
		}

		private static bool IsFileReady(string filename)
		{
			// If the file can be opened for exclusive access it means that the file is no longer locked by another process
			try
			{
				using FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None);
				return inputStream.Length > 0;
			}
			catch ( Exception )
			{
				return false;
			}
		}

		private static void CallPingoCommand(List<FileInfo> imgFiles, string commandString, Label statusLabel, ProgressBar progressBar)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = IS_LINUX ? CONFIG.PingoPathToExe : "cmd.exe",
				UseShellExecute = false,
				CreateNoWindow = true,
				WorkingDirectory = CONFIG.OutputFolderPath
			};

			foreach ( FileInfo img in imgFiles )
			{
				startInfo.Arguments = (IS_LINUX ? "" : "/C ") + commandString;
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

				process.Start();

				while ( !process.HasExited )
				{
					Application.DoEvents();

					if ( ShouldCancelProcessing )
					{
						process.Close();
						break;
					}
				}

				++progressBar.Value;
			}
		}

		private static bool DisplayProcessIsNullError(bool isMagickProcess)
		{
			return MessageBox.Show("Unable to start the process for '" + (isMagickProcess ? MAGICK_FILENAME : PINGO_FILENAME) + "'. Process returned is null.\nDo you want to cancel further processing?",
				"Process could not be started",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation) == DialogResult.Yes;
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
