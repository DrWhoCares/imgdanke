using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Onova;
using Onova.Services;

namespace imgdanke
{
	public partial class MainForm : Form
	{
		private const string MAGICK_FILENAME = "magick.exe";
		private const string PINGO_FILENAME = "pingo.exe";

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

		private static readonly UserConfig CONFIG = UserConfig.LoadConfig();
		private static bool ShouldCancelProcessing = false;
		private static bool IsInitializing = false;

		public MainForm()
		{
			InitializeComponent();
			Text = "imgdanke - v" + typeof(MainForm).Assembly.GetName().Version;
			CheckForProgramUpdates();
			IsInitializing = true;
			InitializePingoPNGPaletteComboBox();
			InitializeWithConfigValues();
			IsInitializing = false;
		}

		public sealed override string Text
		{
			get => base.Text;
			set => base.Text = value;
		}

		#region Initialization

		private async void CheckForProgramUpdates()
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
			InitializePresetSetting();
			InitializeOutputExtension();
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
				MessageBox.Show("The Config's path to the (" + MAGICK_FILENAME + ") file is invalid or is not found on your PATH. Please ensure it is downloaded, or edit the Config to the correct path.\nYou can download it here: https://imagemagick.org/script/download.php", MAGICK_FILENAME + " Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				CONFIG.ImagemagickPathToExe = exePathFound;
			}
		}

		private static void InitializePingoPathToExe()
		{
			if ( VerifyPingoPathIsValid() )
			{
				return;
			}

			string exePathFound = CheckSystemPathForExe(PINGO_FILENAME);

			if ( string.IsNullOrWhiteSpace(exePathFound) )
			{
				MessageBox.Show("The Config's path to the (" + PINGO_FILENAME + ") file is invalid or is not found on your PATH. Please ensure it is downloaded, or edit the Config to the correct path.\nYou can download it here: https://css-ig.net/pingo", PINGO_FILENAME + " Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				CONFIG.PingoPathToExe = exePathFound;
			}
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
				case PresetSettings.Color4Bpp:
					FourBppColorPresetRadioButton.Checked = true;
					break;
				case PresetSettings.MagickColor8Bpp:
					MagickEightBppColorPresetRadioButton.Checked = true;
					break;
				case PresetSettings.PingoColor8Bpp:
					PingoEightBppColorPresetRadioButton.Checked = true;
					break;

				default: break;
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

		#endregion

		#region UIEvents

		#region PathsUI

		private void SourceFolderPathButton_Click(object sender, EventArgs e)
		{
			using CommonOpenFileDialog folderBrowserDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			};

			if ( folderBrowserDialog.ShowDialog() == CommonFileDialogResult.Ok )
			{
				SourceFolderPathTextBox.Text = folderBrowserDialog.FileName;
			}
		}

		private void SourceFolderPathTextBox_TextChanged(object sender, EventArgs e)
		{
			if ( string.IsNullOrWhiteSpace(SourceFolderPathTextBox.Text) )
			{
				CONFIG.SourceFolderPath = "";
				return;
			}

			CONFIG.SourceFolderPath = SourceFolderPathTextBox.Text;
			BuildFilesInSourceFolderList();
		}

		private void BuildFilesInSourceFolderList()
		{
			FilesInSourceFolderListBox.DataSource = GetImageFiles(SourceFolderPathTextBox.Text).OrderByAlphaNumeric(DirectoryOrderer.GetFileName).ToList();
			FilesInSourceFolderListBox.DisplayMember = "Name";
			FilesInSourceFolderListBox.ValueMember = "Name";
			SelectAllInListBox(FilesInSourceFolderListBox);
		}

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

		private void FilesInSourceFolderListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( FilesInSourceFolderListBox.Items.Count == 0 )
			{
				MassFileSelectorButton.Enabled = false;
				return;
			}

			if ( MassFileSelectorButton.Enabled )
			{
				return;
			}

			MassFileSelectorButton.Enabled = true;
			MassFileSelectorButton.Text = FilesInSourceFolderListBox.SelectedIndices.Count > 0 ? "Unselect All" : "Select All";
		}

		private void OutputFolderPathButton_Click(object sender, EventArgs e)
		{
			using CommonOpenFileDialog folderBrowserDialog = new CommonOpenFileDialog
			{
				IsFolderPicker = true
			};

			if ( folderBrowserDialog.ShowDialog() == CommonFileDialogResult.Ok )
			{
				OutputFolderPathTextBox.Text = folderBrowserDialog.FileName;
			}
		}

		private void OutputFolderPathTextBox_TextChanged(object sender, EventArgs e)
		{
			if ( string.IsNullOrWhiteSpace(OutputFolderPathTextBox.Text) )
			{
				CONFIG.OutputFolderPath = "";
				return;
			}

			CONFIG.OutputFolderPath = OutputFolderPathTextBox.Text;
		}

		private void OutputExtensionTextBox_TextChanged(object sender, EventArgs e)
		{
			if ( string.IsNullOrWhiteSpace(OutputExtensionTextBox.Text) )
			{
				CONFIG.OutputExtension = "";
				return;
			}

			CONFIG.OutputExtension = OutputExtensionTextBox.Text;
		}

		#endregion

		#region PresetsUI

		private void NoPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !NoPresetRadioButton.Checked )
			{
				return;
			}

			CONFIG.PresetSetting = PresetSettings.None;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.Invalid;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.Invalid;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "";
			MagickNormalizeCheckBox.Checked = false;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
			PingoStripCheckBox.Checked = true;
		}

		private void CustomPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !CustomPresetRadioButton.Checked )
			{
				return;
			}

			CONFIG.PresetSetting = PresetSettings.Custom;
			MagickDitherComboBox.SelectedIndex = GetIndexOfStringInComboBox(MagickDitherComboBox, CONFIG.MagickDither);
			MagickColorspaceComboBox.SelectedIndex = GetIndexOfStringInComboBox(MagickColorspaceComboBox, CONFIG.MagickColorspace);
			MagickColorsTextBox.Text = CONFIG.MagickColorsValue > 0 ? CONFIG.MagickColorsValue.ToString() : "";
			MagickDepthTextBox.Text = CONFIG.MagickDepthValue > 0 ? CONFIG.MagickDepthValue.ToString() : "";
			MagickPosterizeTextBox.Text = CONFIG.MagickPosterizeValue > 0 ? CONFIG.MagickPosterizeValue.ToString() : "";
			MagickNormalizeCheckBox.Checked = CONFIG.ShouldUseMagickNormalize;
			PingoPNGPaletteComboBox.SelectedIndex = PNG_PALETTE_ITEMS.ToList().FindIndex(i => i.Value == CONFIG.PingoPNGPaletteValue.ToString());
			PingoSBRadioButton.Checked = CONFIG.PingoAdditionalChecks == PingoAdditionalChecks.sb;
			PingoSARadioButton.Checked = CONFIG.PingoAdditionalChecks == PingoAdditionalChecks.sa;
			PingoOptimizationLevelComboBox.SelectedIndex = GetIndexOfStringInComboBox(PingoOptimizationLevelComboBox, CONFIG.PingoOptimizeLevel);
			PingoStripCheckBox.Checked = CONFIG.ShouldUsePingoStrip;
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

			CONFIG.PresetSetting = PresetSettings.Gray1Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.None;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.Gray;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "2";
			MagickNormalizeCheckBox.Checked = true;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
			PingoStripCheckBox.Checked = true;
		}

		private void FourBppGrayPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !FourBppGrayPresetRadioButton.Checked )
			{
				return;
			}

			CONFIG.PresetSetting = PresetSettings.Gray4Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.None;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.Gray;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "16";
			MagickNormalizeCheckBox.Checked = true;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
			PingoStripCheckBox.Checked = true;
		}

		private void EightBppGrayPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !EightBppGrayPresetRadioButton.Checked )
			{
				return;
			}

			CONFIG.PresetSetting = PresetSettings.Gray8Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.None;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.Gray;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "";
			MagickNormalizeCheckBox.Checked = true;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
			PingoStripCheckBox.Checked = true;
		}

		private void FourBppColorPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !FourBppColorPresetRadioButton.Checked )
			{
				return;
			}

			CONFIG.PresetSetting = PresetSettings.Color4Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.None;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.sRGB;
			MagickColorsTextBox.Text = "16";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "256";
			MagickNormalizeCheckBox.Checked = true;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
			PingoStripCheckBox.Checked = true;
		}

		private void MagickEightBppColorPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !MagickEightBppColorPresetRadioButton.Checked )
			{
				return;
			}

			CONFIG.PresetSetting = PresetSettings.MagickColor8Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.None;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.sRGB;
			MagickColorsTextBox.Text = "256";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "256";
			MagickNormalizeCheckBox.Checked = true;
			PingoPNGPaletteComboBox.SelectedIndex = 0;
			PingoSBRadioButton.Checked = false;
			PingoSARadioButton.Checked = false;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
			PingoStripCheckBox.Checked = true;
		}

		private void PingoEightBppColorPresetRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( !PingoEightBppColorPresetRadioButton.Checked )
			{
				return;
			}

			CONFIG.PresetSetting = PresetSettings.PingoColor8Bpp;
			MagickDitherComboBox.SelectedIndex = (int)MagickDitherOptions.Invalid;
			MagickColorspaceComboBox.SelectedIndex = (int)MagickColorspaceOptions.Invalid;
			MagickColorsTextBox.Text = "";
			MagickDepthTextBox.Text = "";
			MagickPosterizeTextBox.Text = "";
			MagickNormalizeCheckBox.Checked = true;
			PingoPNGPaletteComboBox.SelectedIndex = PingoPNGPaletteComboBox.Items.Count - 1;
			PingoSBRadioButton.Checked = true;
			PingoOptimizationLevelComboBox.SelectedIndex = (int)PingoOptimizationLevels.Max;
			PingoStripCheckBox.Checked = true;
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
			MagickCommandTextBox.Text = ConstructMagickCommandString();
		}

		private void MagickColorspaceComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.MagickColorspace = MagickColorspaceComboBox.SelectedItem == null ? "" : MagickColorspaceComboBox.SelectedItem.ToString();
			MagickCommandTextBox.Text = ConstructMagickCommandString();
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
			MagickCommandTextBox.Text = ConstructMagickCommandString();
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
			MagickCommandTextBox.Text = ConstructMagickCommandString();
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
			MagickCommandTextBox.Text = ConstructMagickCommandString();
		}

		private void MagickNormalizeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldUseMagickNormalize = MagickNormalizeCheckBox.Checked;
			MagickCommandTextBox.Text = ConstructMagickCommandString();
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

			PingoSBRadioButton.Enabled = value > 0;
			PingoSARadioButton.Enabled = value > 0;
			CONFIG.PingoPNGPaletteValue = value;
			PingoCommandTextBox.Text = ConstructPingoCommandString();
		}

		private void PingoSBRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing || !PingoSBRadioButton.Checked )
			{
				return;
			}

			CONFIG.PingoAdditionalChecks = PingoAdditionalChecks.sb;
			PingoCommandTextBox.Text = ConstructPingoCommandString();
		}

		private void PingoSARadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing || !PingoSARadioButton.Checked )
			{
				return;
			}

			CONFIG.PingoAdditionalChecks = PingoAdditionalChecks.sa;
			PingoCommandTextBox.Text = ConstructPingoCommandString();
		}

		private void PingoOptimizationLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.PingoOptimizeLevel = PingoOptimizationLevelComboBox.SelectedItem == null ? "" : PingoOptimizationLevelComboBox.SelectedItem.ToString();
			PingoCommandTextBox.Text = ConstructPingoCommandString();
		}

		private void PingoStripCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ( IsInitializing )
			{
				return;
			}

			CONFIG.ShouldUsePingoStrip = PingoStripCheckBox.Checked;
			PingoCommandTextBox.Text = ConstructPingoCommandString();
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

		private static bool IsAllDigits(string str) => str.All(char.IsDigit);

		private static string GetStringWithOnlyDigits(string str) => new string(str.Where(char.IsDigit).ToArray());

		#endregion

		#region CommandsUI

		private string ConstructMagickCommandString()
		{
			const string MAGICK_COMMAND_PREFIX = "magick convert \"%1\" ";
			const string MAGICK_COMMAND_SUFFIX = "\"%2\"";
			string command = MAGICK_COMMAND_PREFIX;

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
			string command = PINGO_COMMAND_PREFIX;

			EnsurePingoConfigValuesAreUpdated();

			if ( CONFIG.PingoPNGPaletteValue > 0 && CONFIG.PingoPNGPaletteValue <= 100 )
			{
				command += "-pngpalette=" + CONFIG.PingoPNGPaletteValue + " ";

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
			if ( string.IsNullOrWhiteSpace(MagickCommandTextBox.Text) )
			{
				CONFIG.MagickCommandString = "";
				return;
			}

			CONFIG.MagickCommandString = MagickCommandTextBox.Text;
			ApplyButton.Enabled = VerifyReadyToApply();
		}

		private void PingoCommandTextBox_TextChanged(object sender, EventArgs e)
		{
			if ( string.IsNullOrWhiteSpace(PingoCommandTextBox.Text) )
			{
				CONFIG.PingoCommandString = "";
				return;
			}

			CONFIG.PingoCommandString = PingoCommandTextBox.Text;
			ApplyButton.Enabled = VerifyReadyToApply();
		}

		private void ProcessingCancelButton_Click(object sender, EventArgs e)
		{
			ShouldCancelProcessing = true;
		}

		#endregion

		#endregion

		#region Processing

		private bool VerifyReadyToApply()
		{
			return !string.IsNullOrEmpty(MagickCommandTextBox.Text) && Directory.Exists(SourceFolderPathTextBox.Text);
		}

		private void ApplyButton_Click(object sender, EventArgs e)
		{
			ToggleUI(false);
			List<FileInfo> imgFiles = FilesInSourceFolderListBox.SelectedItems.Cast<FileInfo>().ToList();

			if ( !imgFiles.Any() )
			{
				StatusMessageLabel.Text = "No valid files in the folder selected.";
				ToggleUI(true);
				return;
			}

			CallCommandOnFiles(imgFiles, MagickCommandTextBox.Text, true, StatusMessageLabel);
			
			if ( !ShouldCancelProcessing )
			{
				CallCommandOnFiles(imgFiles, PingoCommandTextBox.Text, false, StatusMessageLabel);
			}

			BuildFilesInSourceFolderList();
			ToggleUI(true);
			ShouldCancelProcessing = false;
			StatusMessageLabel.Text = "Use %1 as a placeholder for the input filename and %2 for the output filename.";
		}

		private void ToggleUI(bool isActive)
		{
			MagickCommandTextBox.Enabled = isActive;
			PingoCommandTextBox.Enabled = isActive;
			SourceFolderPathTextBox.Enabled = isActive;
			SourceFolderPathButton.Enabled = isActive;
			OutputFolderPathTextBox.Enabled = isActive;
			OutputFolderPathButton.Enabled = isActive;
			PresetSettingsGroupBox.Enabled = isActive;
			ImagemagickSettingsGroupBox.Enabled = isActive;
			PingoSettingsGroupBox.Enabled = isActive;
			FilesInSourceFolderGroupBox.Enabled = isActive;
			ApplyButton.Enabled = isActive;
			FilesInSourceFolderListBox.Enabled = isActive;
			MassFileSelectorButton.Enabled = isActive;
			RefreshFileListButton.Enabled = isActive;
			ProcessingCancelButton.Enabled = !isActive;
			ProcessingCancelButton.Visible = !isActive;
		}

		private static IEnumerable<FileInfo> GetImageFiles(string imageFilesFullPath)
		{
			return new DirectoryInfo(imageFilesFullPath)
				.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
				.Where(s => s.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
								|| s.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
								|| s.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase));
		}

		private static void CallCommandOnFiles(List<FileInfo> imgFiles, string commandString, bool isMagickCommand, Label statusLabel)
		{
			if ( string.IsNullOrWhiteSpace(commandString) )
			{
				return;
			}

			bool doesCommandSpecifyOutput = commandString.Contains("%2");

			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = "CMD.exe",
				UseShellExecute = false,
				CreateNoWindow = true
			};

			foreach ( FileInfo img in imgFiles )
			{
				startInfo.Arguments = "/C " + commandString;
				string originalFilename = img.FullName;
				string tempFilename = "";
				startInfo.Arguments = startInfo.Arguments.Replace("%1", img.FullName);

				if ( doesCommandSpecifyOutput )
				{
					tempFilename = img.FullName.Replace(img.Extension, "") + ".tmp" + CONFIG.OutputExtension;
					startInfo.Arguments = startInfo.Arguments.Replace("%2", tempFilename);
				}

				string commandType = isMagickCommand ? "magick" : "pingo";
				statusLabel.Text = "Processing " + commandType + " command on \"" + img.FullName + "\".";
				Process process = Process.Start(startInfo);
				process.Start();

				while ( !process.HasExited )
				{
					Application.DoEvents();

					if ( ShouldCancelProcessing )
					{
						process.Close();
						process.Dispose();
						break;
					}
				}

				if ( ShouldCancelProcessing )
				{
					return;
				}

				if ( isMagickCommand )
				{
					if ( doesCommandSpecifyOutput )
					{
						File.Delete(originalFilename);

						while ( !IsFileReady(tempFilename) )
						{
							Application.DoEvents();
						}

						File.Move(tempFilename, tempFilename.Replace(".tmp", ""));
					}
				}
			}
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

		#endregion

	}

	internal static class DirectoryOrderer
	{
		public static string GetDirectoryName(DirectoryInfo dirInfo) => dirInfo.Name;

		public static string GetFileName(FileInfo fileInfo) => fileInfo.Name;

		public static IOrderedEnumerable<T> OrderByAlphaNumeric<T>(this IEnumerable<T> source, Func<T, string> selector)
		{
			var enumerable = source.ToList();
			int max = enumerable.SelectMany(i => Regex.Matches(selector(i), @"\d+").Cast<Match>().Select(m => m.Value.Length)).DefaultIfEmpty().Max();
			return enumerable.OrderBy(i => Regex.Replace(selector(i), @"\d+", m => m.Value.PadLeft(max, '0')));
		}
	}
}
