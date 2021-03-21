using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace imgdanke
{
	public partial class OutputSettingsForm : Form
	{
		private static readonly Regex INVALID_FILENAME_CHARS_REGEX = new("[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]");

		public sealed override string Text
		{
			get => base.Text;
			set => base.Text = value;
		}

		public OutputSettingsForm()
		{
			InitializeComponent();
			Text = "imgdanke - v" + typeof(MainForm).Assembly.GetName().Version + " - Output Settings";
			InitializeSettings();
		}

		#region Initialization

		private void InitializeSettings()
		{
			OutputToNewFolderCheckBox.Checked = MainForm.CONFIG.ShouldOutputToNewFolder;
			OutputToNewFolderFolderNameTextBox.Text = MainForm.CONFIG.NewOutputFolderBaseName;
			AddTagsToFilenamesCheckBox.Checked = MainForm.CONFIG.ShouldAddTagsToFilenames;
			FilenamesPresetCheckBox.Checked = MainForm.CONFIG.ShouldAddPresetToFilenames;
			FilenamesMagickSettingsCheckBox.Checked = MainForm.CONFIG.ShouldAddMagickSettingsToFilenames;
			FilenamesPingoSettingsCheckBox.Checked = MainForm.CONFIG.ShouldAddPingoSettingsToFilenames;
			AddTagsToOutputFolderCheckBox.Checked = MainForm.CONFIG.ShouldAddTagsToOutputFolder;
			OutputFolderPresetCheckBox.Checked = MainForm.CONFIG.ShouldAddPresetToOutputFolder;
			OutputFolderMagickSettingsCheckBox.Checked = MainForm.CONFIG.ShouldAddMagickSettingsToOutputFolder;
			OutputFolderPingoSettingsCheckBox.Checked = MainForm.CONFIG.ShouldAddPingoSettingsToOutputFolder;
			ToggleOutputToNewFolderUI(OutputToNewFolderCheckBox.Checked);
			ToggleAddTagsToFilenamesUI(AddTagsToFilenamesCheckBox.Checked);
			ToggleAddTagsToOutputFolderUI(AddTagsToOutputFolderCheckBox.Checked);
		}

		private void OutputSettingsForm_VisibleChanged(object sender, System.EventArgs e)
		{
			OutputToNewFolderCheckBox.Checked = MainForm.CONFIG.ShouldOutputToNewFolder;
			AddTagsToFilenamesCheckBox.Checked = MainForm.CONFIG.ShouldAddTagsToFilenames;
			AddTagsToOutputFolderCheckBox.Checked = MainForm.CONFIG.ShouldAddTagsToOutputFolder;
		}

		private void OutputSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			HandleSettingOutputToNewFolderToDefaultValue();
		}

		#endregion

		#region FolderSettings

		private void OutputToNewFolderCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			MainForm.CONFIG.ShouldOutputToNewFolder = OutputToNewFolderCheckBox.Checked;
			ToggleOutputToNewFolderUI(OutputToNewFolderCheckBox.Checked);
		}

		private void ToggleOutputToNewFolderUI(bool isActive)
		{
			OutputToNewFolderFolderNameTextBox.Enabled = isActive;
			TagsToOutputFolderGroupBox.Enabled = isActive;
		}

		private void OutputToNewFolderFolderNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ( !char.IsControl(e.KeyChar) && INVALID_FILENAME_CHARS_REGEX.IsMatch(e.KeyChar.ToString()) )
			{
				e.Handled = true;
			}
		}
		
		private void OutputToNewFolderFolderNameTextBox_TextChanged(object sender, System.EventArgs e)
		{
			MainForm.CONFIG.NewOutputFolderBaseName = string.IsNullOrWhiteSpace(OutputToNewFolderFolderNameTextBox.Text) ? "" : OutputToNewFolderFolderNameTextBox.Text;
		}

		private void OutputToNewFolderFolderNameTextBox_Leave(object sender, System.EventArgs e)
		{
			HandleSettingOutputToNewFolderToDefaultValue();
		}

		private void HandleSettingOutputToNewFolderToDefaultValue()
		{
			if ( string.IsNullOrWhiteSpace(OutputToNewFolderFolderNameTextBox.Text) )
			{
				OutputToNewFolderFolderNameTextBox.Text = "_danke";
			}
		}

		#endregion

		#region TagsToFilenames

		private void AddTagsToFilenamesCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			MainForm.CONFIG.ShouldAddTagsToFilenames = AddTagsToFilenamesCheckBox.Checked;
			ToggleAddTagsToFilenamesUI(AddTagsToFilenamesCheckBox.Checked);
		}

		private void ToggleAddTagsToFilenamesUI(bool isActive)
		{
			FilenamesPresetCheckBox.Enabled = isActive;
			FilenamesMagickSettingsCheckBox.Enabled = isActive;
			FilenamesPingoSettingsCheckBox.Enabled = isActive;
		}

		private void FilenamesPresetCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			MainForm.CONFIG.ShouldAddPresetToFilenames = FilenamesPresetCheckBox.Checked;
		}

		private void FilenamesMagickSettingsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			MainForm.CONFIG.ShouldAddMagickSettingsToFilenames = FilenamesMagickSettingsCheckBox.Checked;
		}

		private void FilenamesPingoSettingsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			MainForm.CONFIG.ShouldAddPingoSettingsToFilenames = FilenamesPingoSettingsCheckBox.Checked;
		}

		#endregion

		#region TagsToOutputFolder

		private void AddTagsToOutputFolderCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			MainForm.CONFIG.ShouldAddTagsToOutputFolder = AddTagsToOutputFolderCheckBox.Checked;
			ToggleAddTagsToOutputFolderUI(AddTagsToOutputFolderCheckBox.Checked);
		}

		private void ToggleAddTagsToOutputFolderUI(bool isActive)
		{
			OutputFolderPresetCheckBox.Enabled = isActive;
			OutputFolderMagickSettingsCheckBox.Enabled = isActive;
			OutputFolderPingoSettingsCheckBox.Enabled = isActive;
		}

		private void OutputFolderPresetCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			MainForm.CONFIG.ShouldAddPresetToOutputFolder = OutputFolderPresetCheckBox.Checked;
		}

		private void OutputFolderMagickSettingsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			MainForm.CONFIG.ShouldAddMagickSettingsToOutputFolder = OutputFolderMagickSettingsCheckBox.Checked;
		}

		private void OutputFolderPingoSettingsCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			MainForm.CONFIG.ShouldAddPingoSettingsToOutputFolder = OutputFolderPingoSettingsCheckBox.Checked;
		}

		#endregion

	}
}
