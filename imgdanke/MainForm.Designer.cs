using System.Windows.Forms;

namespace imgdanke
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if ( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.MagickCommandTextBox = new System.Windows.Forms.TextBox();
			this.MagickCommandLabel = new System.Windows.Forms.Label();
			this.SourceFolderPathButton = new System.Windows.Forms.Button();
			this.SourceFolderPathTextBox = new System.Windows.Forms.TextBox();
			this.StartButton = new System.Windows.Forms.Button();
			this.StatusMessageLabel = new System.Windows.Forms.Label();
			this.OutputFolderPathTextBox = new System.Windows.Forms.TextBox();
			this.OutputFolderPathButton = new System.Windows.Forms.Button();
			this.SourceFolderPathLabel = new System.Windows.Forms.Label();
			this.OutputFolderPathLabel = new System.Windows.Forms.Label();
			this.ImagemagickSettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.MagickContrastStretchCheckBox = new System.Windows.Forms.CheckBox();
			this.MagickDepthTextBox = new System.Windows.Forms.TextBox();
			this.MagickDepthLabel = new System.Windows.Forms.Label();
			this.MagickColorsTextBox = new System.Windows.Forms.TextBox();
			this.MagickColorsLabel = new System.Windows.Forms.Label();
			this.MagickPosterizeTextBox = new System.Windows.Forms.TextBox();
			this.MagickPosterizeLabel = new System.Windows.Forms.Label();
			this.MagickNormalizeCheckBox = new System.Windows.Forms.CheckBox();
			this.MagickColorspaceComboBox = new System.Windows.Forms.ComboBox();
			this.MagickDitherComboBox = new System.Windows.Forms.ComboBox();
			this.PingoSettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.PingoNoDitheringCheckBox = new System.Windows.Forms.CheckBox();
			this.PingoOptimizationLevelLabel = new System.Windows.Forms.Label();
			this.PingoPNGPaletteComboBox = new System.Windows.Forms.ComboBox();
			this.PingoStripCheckBox = new System.Windows.Forms.CheckBox();
			this.PingoPNGPaletteLabel = new System.Windows.Forms.Label();
			this.PingoOptimizationLevelComboBox = new System.Windows.Forms.ComboBox();
			this.PresetSettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.PingoFourBppColorPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.PingoEightBppColorPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.CustomPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.MagickEightBppColorPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.ColorPresetsLabel = new System.Windows.Forms.Label();
			this.MagickFourBppColorPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.GrayscalePresetsLabel = new System.Windows.Forms.Label();
			this.OneBppGrayPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.NoPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.EightBppGrayPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.FourBppGrayPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.FilesInSourceFolderGroupBox = new System.Windows.Forms.GroupBox();
			this.FileUIAndFileListSplitContainer = new System.Windows.Forms.SplitContainer();
			this.IncludePSDsCheckBox = new System.Windows.Forms.CheckBox();
			this.IncludeSubfoldersCheckBox = new System.Windows.Forms.CheckBox();
			this.MassFileSelectorButton = new System.Windows.Forms.Button();
			this.OutputExtensionLabel = new System.Windows.Forms.Label();
			this.RefreshFileListButton = new System.Windows.Forms.Button();
			this.OutputExtensionTextBox = new System.Windows.Forms.TextBox();
			this.FilesInSourceFolderListBox = new System.Windows.Forms.ListBox();
			this.PingoCommandLabel = new System.Windows.Forms.Label();
			this.PingoCommandTextBox = new System.Windows.Forms.TextBox();
			this.ProcessingCancelButton = new System.Windows.Forms.Button();
			this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.OutputAndSettingsSplitContainer = new System.Windows.Forms.SplitContainer();
			this.MaintainFolderStructureCheckBox = new System.Windows.Forms.CheckBox();
			this.DeleteOriginalsAfterCheckBox = new System.Windows.Forms.CheckBox();
			this.ReplaceOriginalsCheckBox = new System.Windows.Forms.CheckBox();
			this.SettingsAndFilesSplitContainer = new System.Windows.Forms.SplitContainer();
			this.PresetsAndSettingsSplitContainer = new System.Windows.Forms.SplitContainer();
			this.MagickAndPingoSplitContainer = new System.Windows.Forms.SplitContainer();
			this.TotalSavingsLabel = new System.Windows.Forms.Label();
			this.NewSizeLabel = new System.Windows.Forms.Label();
			this.PreviousSizeLabel = new System.Windows.Forms.Label();
			this.AppendToOutputLabel = new System.Windows.Forms.Label();
			this.AppendToOutputTextBox = new System.Windows.Forms.TextBox();
			this.PrependToOutputLabel = new System.Windows.Forms.Label();
			this.PrependToOutputTextBox = new System.Windows.Forms.TextBox();
			this.ProcessingProgressBar = new System.Windows.Forms.ProgressBar();
			this.OptionsMenuStrip = new System.Windows.Forms.MenuStrip();
			this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenUserConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveCurrentSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OutputSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ShouldOutputToNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AddTagsToFilenamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AddTagsToNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CheckForUpdatesToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.CheckForUpdatesOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DisableFailedToCheckMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CheckForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenDocumentationFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenDocumentationGitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.ImagemagickSettingsGroupBox.SuspendLayout();
			this.PingoSettingsGroupBox.SuspendLayout();
			this.PresetSettingsGroupBox.SuspendLayout();
			this.FilesInSourceFolderGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FileUIAndFileListSplitContainer)).BeginInit();
			this.FileUIAndFileListSplitContainer.Panel1.SuspendLayout();
			this.FileUIAndFileListSplitContainer.Panel2.SuspendLayout();
			this.FileUIAndFileListSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
			this.MainSplitContainer.Panel1.SuspendLayout();
			this.MainSplitContainer.Panel2.SuspendLayout();
			this.MainSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.OutputAndSettingsSplitContainer)).BeginInit();
			this.OutputAndSettingsSplitContainer.Panel1.SuspendLayout();
			this.OutputAndSettingsSplitContainer.Panel2.SuspendLayout();
			this.OutputAndSettingsSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SettingsAndFilesSplitContainer)).BeginInit();
			this.SettingsAndFilesSplitContainer.Panel1.SuspendLayout();
			this.SettingsAndFilesSplitContainer.Panel2.SuspendLayout();
			this.SettingsAndFilesSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PresetsAndSettingsSplitContainer)).BeginInit();
			this.PresetsAndSettingsSplitContainer.Panel1.SuspendLayout();
			this.PresetsAndSettingsSplitContainer.Panel2.SuspendLayout();
			this.PresetsAndSettingsSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MagickAndPingoSplitContainer)).BeginInit();
			this.MagickAndPingoSplitContainer.Panel1.SuspendLayout();
			this.MagickAndPingoSplitContainer.Panel2.SuspendLayout();
			this.MagickAndPingoSplitContainer.SuspendLayout();
			this.OptionsMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// MagickCommandTextBox
			// 
			this.MagickCommandTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MagickCommandTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.MagickCommandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MagickCommandTextBox.Location = new System.Drawing.Point(6, 30);
			this.MagickCommandTextBox.Name = "MagickCommandTextBox";
			this.MagickCommandTextBox.Size = new System.Drawing.Size(1077, 20);
			this.MagickCommandTextBox.TabIndex = 7;
			this.MainToolTip.SetToolTip(this.MagickCommandTextBox, "The imagemagick command to be run, %1 and %2 getting replaced by the input and ou" +
        "tput files, respectively.\r\nCommand is skipped if nothing will happen.");
			this.MagickCommandTextBox.TextChanged += new System.EventHandler(this.MagickCommandTextBox_TextChanged);
			// 
			// MagickCommandLabel
			// 
			this.MagickCommandLabel.AutoSize = true;
			this.MagickCommandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MagickCommandLabel.Location = new System.Drawing.Point(7, 7);
			this.MagickCommandLabel.Name = "MagickCommandLabel";
			this.MagickCommandLabel.Size = new System.Drawing.Size(155, 20);
			this.MagickCommandLabel.TabIndex = 1;
			this.MagickCommandLabel.Text = "Magick Command:";
			// 
			// SourceFolderPathButton
			// 
			this.SourceFolderPathButton.AutoSize = true;
			this.SourceFolderPathButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.SourceFolderPathButton.Location = new System.Drawing.Point(2, 21);
			this.SourceFolderPathButton.Name = "SourceFolderPathButton";
			this.SourceFolderPathButton.Size = new System.Drawing.Size(32, 23);
			this.SourceFolderPathButton.TabIndex = 0;
			this.SourceFolderPathButton.Text = "...";
			this.MainToolTip.SetToolTip(this.SourceFolderPathButton, "Select a folder to use as the Source Folder Path.");
			this.SourceFolderPathButton.UseMnemonic = false;
			this.SourceFolderPathButton.UseVisualStyleBackColor = true;
			this.SourceFolderPathButton.Click += new System.EventHandler(this.SourceFolderPathButton_Click);
			// 
			// SourceFolderPathTextBox
			// 
			this.SourceFolderPathTextBox.AllowDrop = true;
			this.SourceFolderPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SourceFolderPathTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.SourceFolderPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SourceFolderPathTextBox.Location = new System.Drawing.Point(40, 23);
			this.SourceFolderPathTextBox.Name = "SourceFolderPathTextBox";
			this.SourceFolderPathTextBox.Size = new System.Drawing.Size(1042, 20);
			this.SourceFolderPathTextBox.TabIndex = 1;
			this.MainToolTip.SetToolTip(this.SourceFolderPathTextBox, "The path to the source images.");
			this.SourceFolderPathTextBox.TextChanged += new System.EventHandler(this.SourceFolderPathTextBox_TextChanged);
			this.SourceFolderPathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.PathTextBox_DragDrop);
			this.SourceFolderPathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.PathTextBox_DragEnter);
			this.SourceFolderPathTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxRestrictToPathPermittedChars_KeyPress);
			// 
			// StartButton
			// 
			this.StartButton.AutoSize = true;
			this.StartButton.Enabled = false;
			this.StartButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.StartButton.Location = new System.Drawing.Point(6, 131);
			this.StartButton.Name = "StartButton";
			this.StartButton.Size = new System.Drawing.Size(75, 23);
			this.StartButton.TabIndex = 8;
			this.StartButton.Text = "Start";
			this.MainToolTip.SetToolTip(this.StartButton, "Start processing the file(s).");
			this.StartButton.UseMnemonic = false;
			this.StartButton.UseVisualStyleBackColor = true;
			this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// StatusMessageLabel
			// 
			this.StatusMessageLabel.AutoSize = true;
			this.StatusMessageLabel.Location = new System.Drawing.Point(168, 136);
			this.StatusMessageLabel.Name = "StatusMessageLabel";
			this.StatusMessageLabel.Size = new System.Drawing.Size(374, 13);
			this.StatusMessageLabel.TabIndex = 5;
			this.StatusMessageLabel.Text = "Use %1 as a placeholder for the input filename and %2 for the output filename.";
			// 
			// OutputFolderPathTextBox
			// 
			this.OutputFolderPathTextBox.AllowDrop = true;
			this.OutputFolderPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OutputFolderPathTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.OutputFolderPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OutputFolderPathTextBox.Location = new System.Drawing.Point(40, 71);
			this.OutputFolderPathTextBox.Name = "OutputFolderPathTextBox";
			this.OutputFolderPathTextBox.Size = new System.Drawing.Size(1042, 20);
			this.OutputFolderPathTextBox.TabIndex = 3;
			this.MainToolTip.SetToolTip(this.OutputFolderPathTextBox, "The path to where the processed images will be output.");
			this.OutputFolderPathTextBox.TextChanged += new System.EventHandler(this.OutputFolderPathTextBox_TextChanged);
			this.OutputFolderPathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.PathTextBox_DragDrop);
			this.OutputFolderPathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.PathTextBox_DragEnter);
			this.OutputFolderPathTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxRestrictToPathPermittedChars_KeyPress);
			// 
			// OutputFolderPathButton
			// 
			this.OutputFolderPathButton.AutoSize = true;
			this.OutputFolderPathButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.OutputFolderPathButton.Location = new System.Drawing.Point(2, 68);
			this.OutputFolderPathButton.Name = "OutputFolderPathButton";
			this.OutputFolderPathButton.Size = new System.Drawing.Size(32, 23);
			this.OutputFolderPathButton.TabIndex = 2;
			this.OutputFolderPathButton.Text = "...";
			this.MainToolTip.SetToolTip(this.OutputFolderPathButton, "Select a folder to use as the Output Folder Path.");
			this.OutputFolderPathButton.UseMnemonic = false;
			this.OutputFolderPathButton.UseVisualStyleBackColor = true;
			this.OutputFolderPathButton.Click += new System.EventHandler(this.OutputFolderPathButton_Click);
			// 
			// SourceFolderPathLabel
			// 
			this.SourceFolderPathLabel.AutoSize = true;
			this.SourceFolderPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SourceFolderPathLabel.Location = new System.Drawing.Point(3, 0);
			this.SourceFolderPathLabel.Name = "SourceFolderPathLabel";
			this.SourceFolderPathLabel.Size = new System.Drawing.Size(169, 20);
			this.SourceFolderPathLabel.TabIndex = 8;
			this.SourceFolderPathLabel.Text = "Source Folder Path:";
			// 
			// OutputFolderPathLabel
			// 
			this.OutputFolderPathLabel.AutoSize = true;
			this.OutputFolderPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OutputFolderPathLabel.Location = new System.Drawing.Point(3, 47);
			this.OutputFolderPathLabel.Name = "OutputFolderPathLabel";
			this.OutputFolderPathLabel.Size = new System.Drawing.Size(167, 20);
			this.OutputFolderPathLabel.TabIndex = 9;
			this.OutputFolderPathLabel.Text = "Output Folder Path:";
			// 
			// ImagemagickSettingsGroupBox
			// 
			this.ImagemagickSettingsGroupBox.AutoSize = true;
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickContrastStretchCheckBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickDepthTextBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickDepthLabel);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickColorsTextBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickColorsLabel);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickPosterizeTextBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickPosterizeLabel);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickNormalizeCheckBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickColorspaceComboBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickDitherComboBox);
			this.ImagemagickSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ImagemagickSettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.ImagemagickSettingsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.ImagemagickSettingsGroupBox.Name = "ImagemagickSettingsGroupBox";
			this.ImagemagickSettingsGroupBox.Size = new System.Drawing.Size(138, 249);
			this.ImagemagickSettingsGroupBox.TabIndex = 5;
			this.ImagemagickSettingsGroupBox.TabStop = false;
			this.ImagemagickSettingsGroupBox.Text = "Imagemagick Settings";
			// 
			// MagickContrastStretchCheckBox
			// 
			this.MagickContrastStretchCheckBox.AutoSize = true;
			this.MagickContrastStretchCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MagickContrastStretchCheckBox.Location = new System.Drawing.Point(2, 174);
			this.MagickContrastStretchCheckBox.Name = "MagickContrastStretchCheckBox";
			this.MagickContrastStretchCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.MagickContrastStretchCheckBox.Size = new System.Drawing.Size(119, 17);
			this.MagickContrastStretchCheckBox.TabIndex = 10;
			this.MagickContrastStretchCheckBox.Text = "contrast-stretch-";
			this.MainToolTip.SetToolTip(this.MagickContrastStretchCheckBox, "Similar to -normalize, this is used to help achieve #000 blacks and #FFF whites f" +
        "or grayscale images.");
			this.MagickContrastStretchCheckBox.UseMnemonic = false;
			this.MagickContrastStretchCheckBox.UseVisualStyleBackColor = false;
			this.MagickContrastStretchCheckBox.CheckedChanged += new System.EventHandler(this.MagickContrastStretchCheckBox_CheckedChanged);
			// 
			// MagickDepthTextBox
			// 
			this.MagickDepthTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.MagickDepthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MagickDepthTextBox.Location = new System.Drawing.Point(52, 99);
			this.MagickDepthTextBox.Name = "MagickDepthTextBox";
			this.MagickDepthTextBox.Size = new System.Drawing.Size(77, 20);
			this.MagickDepthTextBox.TabIndex = 9;
			this.MainToolTip.SetToolTip(this.MagickDepthTextBox, "Limits the depth to the specified amount.\r\nThis is a lossy operation.");
			this.MagickDepthTextBox.TextChanged += new System.EventHandler(this.MagickDepthTextBox_TextChanged);
			this.MagickDepthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RestrictToNumbersTextBox_KeyPress);
			// 
			// MagickDepthLabel
			// 
			this.MagickDepthLabel.AutoSize = true;
			this.MagickDepthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MagickDepthLabel.Location = new System.Drawing.Point(3, 101);
			this.MagickDepthLabel.Name = "MagickDepthLabel";
			this.MagickDepthLabel.Size = new System.Drawing.Size(43, 13);
			this.MagickDepthLabel.TabIndex = 8;
			this.MagickDepthLabel.Text = "-depth";
			// 
			// MagickColorsTextBox
			// 
			this.MagickColorsTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.MagickColorsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MagickColorsTextBox.Location = new System.Drawing.Point(54, 73);
			this.MagickColorsTextBox.Name = "MagickColorsTextBox";
			this.MagickColorsTextBox.Size = new System.Drawing.Size(75, 20);
			this.MagickColorsTextBox.TabIndex = 7;
			this.MainToolTip.SetToolTip(this.MagickColorsTextBox, "Reduces the number of colors to the provided amount, or less.\r\nThis is a lossy op" +
        "eration, and -posterize often does a better job.");
			this.MagickColorsTextBox.TextChanged += new System.EventHandler(this.MagickColorsTextBox_TextChanged);
			this.MagickColorsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RestrictToNumbersTextBox_KeyPress);
			// 
			// MagickColorsLabel
			// 
			this.MagickColorsLabel.AutoSize = true;
			this.MagickColorsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MagickColorsLabel.Location = new System.Drawing.Point(3, 75);
			this.MagickColorsLabel.Name = "MagickColorsLabel";
			this.MagickColorsLabel.Size = new System.Drawing.Size(45, 13);
			this.MagickColorsLabel.TabIndex = 6;
			this.MagickColorsLabel.Text = "-colors";
			// 
			// MagickPosterizeTextBox
			// 
			this.MagickPosterizeTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.MagickPosterizeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MagickPosterizeTextBox.Location = new System.Drawing.Point(71, 125);
			this.MagickPosterizeTextBox.Name = "MagickPosterizeTextBox";
			this.MagickPosterizeTextBox.Size = new System.Drawing.Size(58, 20);
			this.MagickPosterizeTextBox.TabIndex = 5;
			this.MainToolTip.SetToolTip(this.MagickPosterizeTextBox, resources.GetString("MagickPosterizeTextBox.ToolTip"));
			this.MagickPosterizeTextBox.TextChanged += new System.EventHandler(this.MagickPosterizeTextBox_TextChanged);
			this.MagickPosterizeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RestrictToNumbersTextBox_KeyPress);
			// 
			// MagickPosterizeLabel
			// 
			this.MagickPosterizeLabel.AutoSize = true;
			this.MagickPosterizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MagickPosterizeLabel.Location = new System.Drawing.Point(3, 127);
			this.MagickPosterizeLabel.Name = "MagickPosterizeLabel";
			this.MagickPosterizeLabel.Size = new System.Drawing.Size(62, 13);
			this.MagickPosterizeLabel.TabIndex = 4;
			this.MagickPosterizeLabel.Text = "-posterize";
			// 
			// MagickNormalizeCheckBox
			// 
			this.MagickNormalizeCheckBox.AutoSize = true;
			this.MagickNormalizeCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MagickNormalizeCheckBox.Location = new System.Drawing.Point(2, 151);
			this.MagickNormalizeCheckBox.Name = "MagickNormalizeCheckBox";
			this.MagickNormalizeCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.MagickNormalizeCheckBox.Size = new System.Drawing.Size(83, 17);
			this.MagickNormalizeCheckBox.TabIndex = 3;
			this.MagickNormalizeCheckBox.Text = "normalize-";
			this.MainToolTip.SetToolTip(this.MagickNormalizeCheckBox, "Normalizes the colors, disable if you would prefer the original colors.");
			this.MagickNormalizeCheckBox.UseMnemonic = false;
			this.MagickNormalizeCheckBox.UseVisualStyleBackColor = false;
			this.MagickNormalizeCheckBox.CheckedChanged += new System.EventHandler(this.MagickNormalizeCheckBox_CheckedChanged);
			// 
			// MagickColorspaceComboBox
			// 
			this.MagickColorspaceComboBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.MagickColorspaceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MagickColorspaceComboBox.FormattingEnabled = true;
			this.MagickColorspaceComboBox.Items.AddRange(new object[] {
            "",
            "-colorspace Gray",
            "-colorspace sRGB"});
			this.MagickColorspaceComboBox.Location = new System.Drawing.Point(6, 46);
			this.MagickColorspaceComboBox.Name = "MagickColorspaceComboBox";
			this.MagickColorspaceComboBox.Size = new System.Drawing.Size(123, 21);
			this.MagickColorspaceComboBox.TabIndex = 1;
			this.MainToolTip.SetToolTip(this.MagickColorspaceComboBox, "The colorspace to output the files as:\r\n\"Gray\" for grayscale images.\r\n\"sRGB\" for " +
        "color images.");
			this.MagickColorspaceComboBox.SelectedIndexChanged += new System.EventHandler(this.MagickColorspaceComboBox_SelectedIndexChanged);
			// 
			// MagickDitherComboBox
			// 
			this.MagickDitherComboBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.MagickDitherComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MagickDitherComboBox.FormattingEnabled = true;
			this.MagickDitherComboBox.Items.AddRange(new object[] {
            "",
            "-dither None"});
			this.MagickDitherComboBox.Location = new System.Drawing.Point(6, 19);
			this.MagickDitherComboBox.Name = "MagickDitherComboBox";
			this.MagickDitherComboBox.Size = new System.Drawing.Size(123, 21);
			this.MagickDitherComboBox.TabIndex = 0;
			this.MainToolTip.SetToolTip(this.MagickDitherComboBox, "The Imagemagick -dither option to use, default is usually \"None\".");
			this.MagickDitherComboBox.SelectedIndexChanged += new System.EventHandler(this.MagickDitherComboBox_SelectedIndexChanged);
			// 
			// PingoSettingsGroupBox
			// 
			this.PingoSettingsGroupBox.AutoSize = true;
			this.PingoSettingsGroupBox.Controls.Add(this.PingoNoDitheringCheckBox);
			this.PingoSettingsGroupBox.Controls.Add(this.PingoOptimizationLevelLabel);
			this.PingoSettingsGroupBox.Controls.Add(this.PingoPNGPaletteComboBox);
			this.PingoSettingsGroupBox.Controls.Add(this.PingoStripCheckBox);
			this.PingoSettingsGroupBox.Controls.Add(this.PingoPNGPaletteLabel);
			this.PingoSettingsGroupBox.Controls.Add(this.PingoOptimizationLevelComboBox);
			this.PingoSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PingoSettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.PingoSettingsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.PingoSettingsGroupBox.Name = "PingoSettingsGroupBox";
			this.PingoSettingsGroupBox.Size = new System.Drawing.Size(155, 249);
			this.PingoSettingsGroupBox.TabIndex = 6;
			this.PingoSettingsGroupBox.TabStop = false;
			this.PingoSettingsGroupBox.Text = "pingo Settings";
			// 
			// PingoNoDitheringCheckBox
			// 
			this.PingoNoDitheringCheckBox.AutoSize = true;
			this.PingoNoDitheringCheckBox.Enabled = false;
			this.PingoNoDitheringCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PingoNoDitheringCheckBox.Location = new System.Drawing.Point(6, 46);
			this.PingoNoDitheringCheckBox.Name = "PingoNoDitheringCheckBox";
			this.PingoNoDitheringCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.PingoNoDitheringCheckBox.Size = new System.Drawing.Size(93, 17);
			this.PingoNoDitheringCheckBox.TabIndex = 16;
			this.PingoNoDitheringCheckBox.Text = "nodithering-";
			this.MainToolTip.SetToolTip(this.PingoNoDitheringCheckBox, "Turns off dithering if -pngpalette is used.\r\nOff by default for pingo 8bpp color " +
        "preset.\r\nOn by default for pingo 4bpp color preset.");
			this.PingoNoDitheringCheckBox.UseMnemonic = false;
			this.PingoNoDitheringCheckBox.UseVisualStyleBackColor = false;
			this.PingoNoDitheringCheckBox.CheckedChanged += new System.EventHandler(this.PingoNoDitheringCheckBox_CheckedChanged);
			// 
			// PingoOptimizationLevelLabel
			// 
			this.PingoOptimizationLevelLabel.AutoSize = true;
			this.PingoOptimizationLevelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PingoOptimizationLevelLabel.Location = new System.Drawing.Point(6, 73);
			this.PingoOptimizationLevelLabel.Name = "PingoOptimizationLevelLabel";
			this.PingoOptimizationLevelLabel.Size = new System.Drawing.Size(17, 13);
			this.PingoOptimizationLevelLabel.TabIndex = 10;
			this.PingoOptimizationLevelLabel.Text = "-s";
			// 
			// PingoPNGPaletteComboBox
			// 
			this.PingoPNGPaletteComboBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.PingoPNGPaletteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PingoPNGPaletteComboBox.FormattingEnabled = true;
			this.PingoPNGPaletteComboBox.Location = new System.Drawing.Point(83, 19);
			this.PingoPNGPaletteComboBox.Name = "PingoPNGPaletteComboBox";
			this.PingoPNGPaletteComboBox.Size = new System.Drawing.Size(55, 21);
			this.PingoPNGPaletteComboBox.TabIndex = 10;
			this.MainToolTip.SetToolTip(this.PingoPNGPaletteComboBox, "Reduces the number of colors to one of the bins listed\r\nThis is a lossy operation" +
        ".");
			this.PingoPNGPaletteComboBox.SelectedIndexChanged += new System.EventHandler(this.PingoPNGPaletteComboBox_SelectedIndexChanged);
			// 
			// PingoStripCheckBox
			// 
			this.PingoStripCheckBox.AutoSize = true;
			this.PingoStripCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PingoStripCheckBox.Location = new System.Drawing.Point(4, 96);
			this.PingoStripCheckBox.Name = "PingoStripCheckBox";
			this.PingoStripCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.PingoStripCheckBox.Size = new System.Drawing.Size(54, 17);
			this.PingoStripCheckBox.TabIndex = 7;
			this.PingoStripCheckBox.Text = "strip-";
			this.MainToolTip.SetToolTip(this.PingoStripCheckBox, "Strips metadata from the file, slightly reducing the filesize.\r\nDoes not modify t" +
        "he image at all.\r\nTurn off if you want to save the metadata.");
			this.PingoStripCheckBox.UseMnemonic = false;
			this.PingoStripCheckBox.UseVisualStyleBackColor = false;
			this.PingoStripCheckBox.CheckedChanged += new System.EventHandler(this.PingoStripCheckBox_CheckedChanged);
			// 
			// PingoPNGPaletteLabel
			// 
			this.PingoPNGPaletteLabel.AutoSize = true;
			this.PingoPNGPaletteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PingoPNGPaletteLabel.Location = new System.Drawing.Point(6, 24);
			this.PingoPNGPaletteLabel.Name = "PingoPNGPaletteLabel";
			this.PingoPNGPaletteLabel.Size = new System.Drawing.Size(71, 13);
			this.PingoPNGPaletteLabel.TabIndex = 6;
			this.PingoPNGPaletteLabel.Text = "-pngpalette";
			// 
			// PingoOptimizationLevelComboBox
			// 
			this.PingoOptimizationLevelComboBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.PingoOptimizationLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PingoOptimizationLevelComboBox.FormattingEnabled = true;
			this.PingoOptimizationLevelComboBox.Items.AddRange(new object[] {
            "",
            "-s0",
            "-s1",
            "-s2",
            "-s3",
            "-s4",
            "-s5",
            "-s6",
            "-s7",
            "-s8",
            "-s9",
            "-sa",
            "-sb"});
			this.PingoOptimizationLevelComboBox.Location = new System.Drawing.Point(28, 69);
			this.PingoOptimizationLevelComboBox.Name = "PingoOptimizationLevelComboBox";
			this.PingoOptimizationLevelComboBox.Size = new System.Drawing.Size(47, 21);
			this.PingoOptimizationLevelComboBox.TabIndex = 6;
			this.MainToolTip.SetToolTip(this.PingoOptimizationLevelComboBox, "The loseless optimization option, higher is better quality/smaller filesize, but " +
        "more CPU/time to process.");
			this.PingoOptimizationLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.PingoOptimizationLevelComboBox_SelectedIndexChanged);
			// 
			// PresetSettingsGroupBox
			// 
			this.PresetSettingsGroupBox.AutoSize = true;
			this.PresetSettingsGroupBox.Controls.Add(this.PingoFourBppColorPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.PingoEightBppColorPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.CustomPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.MagickEightBppColorPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.ColorPresetsLabel);
			this.PresetSettingsGroupBox.Controls.Add(this.MagickFourBppColorPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.GrayscalePresetsLabel);
			this.PresetSettingsGroupBox.Controls.Add(this.OneBppGrayPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.NoPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.EightBppGrayPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.FourBppGrayPresetRadioButton);
			this.PresetSettingsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PresetSettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.PresetSettingsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.PresetSettingsGroupBox.Name = "PresetSettingsGroupBox";
			this.PresetSettingsGroupBox.Size = new System.Drawing.Size(157, 249);
			this.PresetSettingsGroupBox.TabIndex = 4;
			this.PresetSettingsGroupBox.TabStop = false;
			this.PresetSettingsGroupBox.Text = "Preset Settings";
			// 
			// PingoFourBppColorPresetRadioButton
			// 
			this.PingoFourBppColorPresetRadioButton.AutoSize = true;
			this.PingoFourBppColorPresetRadioButton.Location = new System.Drawing.Point(5, 158);
			this.PingoFourBppColorPresetRadioButton.Name = "PingoFourBppColorPresetRadioButton";
			this.PingoFourBppColorPresetRadioButton.Size = new System.Drawing.Size(136, 17);
			this.PingoFourBppColorPresetRadioButton.TabIndex = 15;
			this.PingoFourBppColorPresetRadioButton.Text = "4 bpp (16 colors, pingo)";
			this.MainToolTip.SetToolTip(this.PingoFourBppColorPresetRadioButton, "Makes a 4bpp sRGB image with only 16 colors, using pingo\'s pngpalette with dither" +
        "ing by default.");
			this.PingoFourBppColorPresetRadioButton.UseMnemonic = false;
			this.PingoFourBppColorPresetRadioButton.UseVisualStyleBackColor = false;
			this.PingoFourBppColorPresetRadioButton.CheckedChanged += new System.EventHandler(this.PingoFourBppColorPresetRadioButton_CheckedChanged);
			// 
			// PingoEightBppColorPresetRadioButton
			// 
			this.PingoEightBppColorPresetRadioButton.AutoSize = true;
			this.PingoEightBppColorPresetRadioButton.Location = new System.Drawing.Point(5, 180);
			this.PingoEightBppColorPresetRadioButton.Name = "PingoEightBppColorPresetRadioButton";
			this.PingoEightBppColorPresetRadioButton.Size = new System.Drawing.Size(142, 17);
			this.PingoEightBppColorPresetRadioButton.TabIndex = 14;
			this.PingoEightBppColorPresetRadioButton.Text = "8 bpp (256 colors, pingo)";
			this.MainToolTip.SetToolTip(this.PingoEightBppColorPresetRadioButton, "Makes an 8bpp sRGB image with only 256 colors, using pingo\'s pngpalette with no d" +
        "ithering by default.");
			this.PingoEightBppColorPresetRadioButton.UseMnemonic = false;
			this.PingoEightBppColorPresetRadioButton.UseVisualStyleBackColor = false;
			this.PingoEightBppColorPresetRadioButton.CheckedChanged += new System.EventHandler(this.PingoEightBppColorPresetRadioButton_CheckedChanged);
			// 
			// CustomPresetRadioButton
			// 
			this.CustomPresetRadioButton.AutoSize = true;
			this.CustomPresetRadioButton.Location = new System.Drawing.Point(5, 42);
			this.CustomPresetRadioButton.Name = "CustomPresetRadioButton";
			this.CustomPresetRadioButton.Size = new System.Drawing.Size(135, 17);
			this.CustomPresetRadioButton.TabIndex = 1;
			this.CustomPresetRadioButton.Text = "Custom (Save Settings)";
			this.MainToolTip.SetToolTip(this.CustomPresetRadioButton, "Whatever current settings you have will continue to be used next time you open th" +
        "e application.");
			this.CustomPresetRadioButton.UseMnemonic = false;
			this.CustomPresetRadioButton.UseVisualStyleBackColor = false;
			this.CustomPresetRadioButton.CheckedChanged += new System.EventHandler(this.CustomPresetRadioButton_CheckedChanged);
			// 
			// MagickEightBppColorPresetRadioButton
			// 
			this.MagickEightBppColorPresetRadioButton.AutoSize = true;
			this.MagickEightBppColorPresetRadioButton.Location = new System.Drawing.Point(5, 226);
			this.MagickEightBppColorPresetRadioButton.Name = "MagickEightBppColorPresetRadioButton";
			this.MagickEightBppColorPresetRadioButton.Size = new System.Drawing.Size(150, 17);
			this.MagickEightBppColorPresetRadioButton.TabIndex = 6;
			this.MagickEightBppColorPresetRadioButton.Text = "8 bpp (256 colors, magick)";
			this.MainToolTip.SetToolTip(this.MagickEightBppColorPresetRadioButton, "Makes an 8bpp sRGB image with only 256 colors, using imagemagick\'s posterization " +
        "with no dithering.");
			this.MagickEightBppColorPresetRadioButton.UseMnemonic = false;
			this.MagickEightBppColorPresetRadioButton.UseVisualStyleBackColor = false;
			this.MagickEightBppColorPresetRadioButton.CheckedChanged += new System.EventHandler(this.MagickEightBppColorPresetRadioButton_CheckedChanged);
			// 
			// ColorPresetsLabel
			// 
			this.ColorPresetsLabel.AutoSize = true;
			this.ColorPresetsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ColorPresetsLabel.Location = new System.Drawing.Point(2, 142);
			this.ColorPresetsLabel.Name = "ColorPresetsLabel";
			this.ColorPresetsLabel.Size = new System.Drawing.Size(86, 13);
			this.ColorPresetsLabel.TabIndex = 13;
			this.ColorPresetsLabel.Text = "Color Presets:";
			// 
			// MagickFourBppColorPresetRadioButton
			// 
			this.MagickFourBppColorPresetRadioButton.AutoSize = true;
			this.MagickFourBppColorPresetRadioButton.Location = new System.Drawing.Point(5, 203);
			this.MagickFourBppColorPresetRadioButton.Name = "MagickFourBppColorPresetRadioButton";
			this.MagickFourBppColorPresetRadioButton.Size = new System.Drawing.Size(144, 17);
			this.MagickFourBppColorPresetRadioButton.TabIndex = 5;
			this.MagickFourBppColorPresetRadioButton.Text = "4 bpp (16 colors, magick)";
			this.MainToolTip.SetToolTip(this.MagickFourBppColorPresetRadioButton, "Makes a 4bpp sRGB image with only 16 colors, using imagemagick\'s posterization wi" +
        "th no dithering.");
			this.MagickFourBppColorPresetRadioButton.UseMnemonic = false;
			this.MagickFourBppColorPresetRadioButton.UseVisualStyleBackColor = false;
			this.MagickFourBppColorPresetRadioButton.CheckedChanged += new System.EventHandler(this.MagickFourBppColorPresetRadioButton_CheckedChanged);
			// 
			// GrayscalePresetsLabel
			// 
			this.GrayscalePresetsLabel.AutoSize = true;
			this.GrayscalePresetsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GrayscalePresetsLabel.Location = new System.Drawing.Point(2, 62);
			this.GrayscalePresetsLabel.Name = "GrayscalePresetsLabel";
			this.GrayscalePresetsLabel.Size = new System.Drawing.Size(113, 13);
			this.GrayscalePresetsLabel.TabIndex = 6;
			this.GrayscalePresetsLabel.Text = "Grayscale Presets:";
			// 
			// OneBppGrayPresetRadioButton
			// 
			this.OneBppGrayPresetRadioButton.AutoSize = true;
			this.OneBppGrayPresetRadioButton.Location = new System.Drawing.Point(5, 76);
			this.OneBppGrayPresetRadioButton.Name = "OneBppGrayPresetRadioButton";
			this.OneBppGrayPresetRadioButton.Size = new System.Drawing.Size(98, 17);
			this.OneBppGrayPresetRadioButton.TabIndex = 2;
			this.OneBppGrayPresetRadioButton.Text = "1 bpp (2 colors)";
			this.MainToolTip.SetToolTip(this.OneBppGrayPresetRadioButton, "Makes a 1bpp grayscale image with only two colors.");
			this.OneBppGrayPresetRadioButton.UseMnemonic = false;
			this.OneBppGrayPresetRadioButton.UseVisualStyleBackColor = false;
			this.OneBppGrayPresetRadioButton.CheckedChanged += new System.EventHandler(this.OneBppGrayPresetRadioButton_CheckedChanged);
			// 
			// NoPresetRadioButton
			// 
			this.NoPresetRadioButton.AutoSize = true;
			this.NoPresetRadioButton.Checked = true;
			this.NoPresetRadioButton.Location = new System.Drawing.Point(5, 19);
			this.NoPresetRadioButton.Name = "NoPresetRadioButton";
			this.NoPresetRadioButton.Size = new System.Drawing.Size(72, 17);
			this.NoPresetRadioButton.TabIndex = 0;
			this.NoPresetRadioButton.TabStop = true;
			this.NoPresetRadioButton.Text = "No Preset";
			this.MainToolTip.SetToolTip(this.NoPresetRadioButton, "Clears all options, only leaving the basic lossless pingo optimizations.");
			this.NoPresetRadioButton.UseMnemonic = false;
			this.NoPresetRadioButton.UseVisualStyleBackColor = false;
			this.NoPresetRadioButton.CheckedChanged += new System.EventHandler(this.NoPresetRadioButton_CheckedChanged);
			// 
			// EightBppGrayPresetRadioButton
			// 
			this.EightBppGrayPresetRadioButton.AutoSize = true;
			this.EightBppGrayPresetRadioButton.Location = new System.Drawing.Point(5, 122);
			this.EightBppGrayPresetRadioButton.Name = "EightBppGrayPresetRadioButton";
			this.EightBppGrayPresetRadioButton.Size = new System.Drawing.Size(110, 17);
			this.EightBppGrayPresetRadioButton.TabIndex = 4;
			this.EightBppGrayPresetRadioButton.Text = "8 bpp (256 colors)";
			this.MainToolTip.SetToolTip(this.EightBppGrayPresetRadioButton, "Makes an 8bpp grayscale image with only 256 colors.");
			this.EightBppGrayPresetRadioButton.UseMnemonic = false;
			this.EightBppGrayPresetRadioButton.UseVisualStyleBackColor = false;
			this.EightBppGrayPresetRadioButton.CheckedChanged += new System.EventHandler(this.EightBppGrayPresetRadioButton_CheckedChanged);
			// 
			// FourBppGrayPresetRadioButton
			// 
			this.FourBppGrayPresetRadioButton.AutoSize = true;
			this.FourBppGrayPresetRadioButton.Location = new System.Drawing.Point(5, 99);
			this.FourBppGrayPresetRadioButton.Name = "FourBppGrayPresetRadioButton";
			this.FourBppGrayPresetRadioButton.Size = new System.Drawing.Size(104, 17);
			this.FourBppGrayPresetRadioButton.TabIndex = 3;
			this.FourBppGrayPresetRadioButton.Text = "4 bpp (16 colors)";
			this.MainToolTip.SetToolTip(this.FourBppGrayPresetRadioButton, "Makes a 4bpp grayscale image with only 16 colors.");
			this.FourBppGrayPresetRadioButton.UseMnemonic = false;
			this.FourBppGrayPresetRadioButton.UseVisualStyleBackColor = false;
			this.FourBppGrayPresetRadioButton.CheckedChanged += new System.EventHandler(this.FourBppGrayPresetRadioButton_CheckedChanged);
			// 
			// FilesInSourceFolderGroupBox
			// 
			this.FilesInSourceFolderGroupBox.AutoSize = true;
			this.FilesInSourceFolderGroupBox.Controls.Add(this.FileUIAndFileListSplitContainer);
			this.FilesInSourceFolderGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FilesInSourceFolderGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.FilesInSourceFolderGroupBox.Location = new System.Drawing.Point(0, 0);
			this.FilesInSourceFolderGroupBox.Name = "FilesInSourceFolderGroupBox";
			this.FilesInSourceFolderGroupBox.Size = new System.Drawing.Size(502, 249);
			this.FilesInSourceFolderGroupBox.TabIndex = 6;
			this.FilesInSourceFolderGroupBox.TabStop = false;
			this.FilesInSourceFolderGroupBox.Text = "Files in Source Folder";
			// 
			// FileUIAndFileListSplitContainer
			// 
			this.FileUIAndFileListSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FileUIAndFileListSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.FileUIAndFileListSplitContainer.IsSplitterFixed = true;
			this.FileUIAndFileListSplitContainer.Location = new System.Drawing.Point(3, 16);
			this.FileUIAndFileListSplitContainer.Name = "FileUIAndFileListSplitContainer";
			this.FileUIAndFileListSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// FileUIAndFileListSplitContainer.Panel1
			// 
			this.FileUIAndFileListSplitContainer.Panel1.Controls.Add(this.IncludePSDsCheckBox);
			this.FileUIAndFileListSplitContainer.Panel1.Controls.Add(this.IncludeSubfoldersCheckBox);
			this.FileUIAndFileListSplitContainer.Panel1.Controls.Add(this.MassFileSelectorButton);
			this.FileUIAndFileListSplitContainer.Panel1.Controls.Add(this.OutputExtensionLabel);
			this.FileUIAndFileListSplitContainer.Panel1.Controls.Add(this.RefreshFileListButton);
			this.FileUIAndFileListSplitContainer.Panel1.Controls.Add(this.OutputExtensionTextBox);
			// 
			// FileUIAndFileListSplitContainer.Panel2
			// 
			this.FileUIAndFileListSplitContainer.Panel2.Controls.Add(this.FilesInSourceFolderListBox);
			this.FileUIAndFileListSplitContainer.Panel2MinSize = 201;
			this.FileUIAndFileListSplitContainer.Size = new System.Drawing.Size(496, 230);
			this.FileUIAndFileListSplitContainer.SplitterDistance = 25;
			this.FileUIAndFileListSplitContainer.TabIndex = 14;
			// 
			// IncludePSDsCheckBox
			// 
			this.IncludePSDsCheckBox.AutoSize = true;
			this.IncludePSDsCheckBox.Checked = true;
			this.IncludePSDsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.IncludePSDsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.IncludePSDsCheckBox.Location = new System.Drawing.Point(391, 7);
			this.IncludePSDsCheckBox.Name = "IncludePSDsCheckBox";
			this.IncludePSDsCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.IncludePSDsCheckBox.Size = new System.Drawing.Size(86, 17);
			this.IncludePSDsCheckBox.TabIndex = 15;
			this.IncludePSDsCheckBox.Text = "Incl. PSDs";
			this.MainToolTip.SetToolTip(this.IncludePSDsCheckBox, resources.GetString("IncludePSDsCheckBox.ToolTip"));
			this.IncludePSDsCheckBox.UseMnemonic = false;
			this.IncludePSDsCheckBox.UseVisualStyleBackColor = false;
			this.IncludePSDsCheckBox.CheckedChanged += new System.EventHandler(this.IncludePSDsCheckBox_CheckedChanged);
			// 
			// IncludeSubfoldersCheckBox
			// 
			this.IncludeSubfoldersCheckBox.AutoSize = true;
			this.IncludeSubfoldersCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.IncludeSubfoldersCheckBox.Location = new System.Drawing.Point(270, 7);
			this.IncludeSubfoldersCheckBox.Name = "IncludeSubfoldersCheckBox";
			this.IncludeSubfoldersCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.IncludeSubfoldersCheckBox.Size = new System.Drawing.Size(115, 17);
			this.IncludeSubfoldersCheckBox.TabIndex = 14;
			this.IncludeSubfoldersCheckBox.Text = "Incl. Subfolders";
			this.MainToolTip.SetToolTip(this.IncludeSubfoldersCheckBox, "Toggles searching through subfolders in the Source Folder Path for files.");
			this.IncludeSubfoldersCheckBox.UseMnemonic = false;
			this.IncludeSubfoldersCheckBox.UseVisualStyleBackColor = false;
			this.IncludeSubfoldersCheckBox.CheckedChanged += new System.EventHandler(this.IncludeSubfoldersCheckBox_CheckedChanged);
			// 
			// MassFileSelectorButton
			// 
			this.MassFileSelectorButton.AutoSize = true;
			this.MassFileSelectorButton.Enabled = false;
			this.MassFileSelectorButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.MassFileSelectorButton.Location = new System.Drawing.Point(3, 1);
			this.MassFileSelectorButton.Name = "MassFileSelectorButton";
			this.MassFileSelectorButton.Size = new System.Drawing.Size(71, 23);
			this.MassFileSelectorButton.TabIndex = 12;
			this.MassFileSelectorButton.Text = "Select All";
			this.MainToolTip.SetToolTip(this.MassFileSelectorButton, "Select all (or unselect all) files in the Files in Source Folder file list.");
			this.MassFileSelectorButton.UseMnemonic = false;
			this.MassFileSelectorButton.UseVisualStyleBackColor = true;
			this.MassFileSelectorButton.Click += new System.EventHandler(this.MassFileSelectorButton_Click);
			// 
			// OutputExtensionLabel
			// 
			this.OutputExtensionLabel.AutoSize = true;
			this.OutputExtensionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OutputExtensionLabel.Location = new System.Drawing.Point(157, 8);
			this.OutputExtensionLabel.Name = "OutputExtensionLabel";
			this.OutputExtensionLabel.Size = new System.Drawing.Size(71, 13);
			this.OutputExtensionLabel.TabIndex = 10;
			this.OutputExtensionLabel.Text = "Output Ext:";
			// 
			// RefreshFileListButton
			// 
			this.RefreshFileListButton.AutoSize = true;
			this.RefreshFileListButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.RefreshFileListButton.Location = new System.Drawing.Point(80, 1);
			this.RefreshFileListButton.Name = "RefreshFileListButton";
			this.RefreshFileListButton.Size = new System.Drawing.Size(73, 23);
			this.RefreshFileListButton.TabIndex = 13;
			this.RefreshFileListButton.Text = "Refresh List";
			this.MainToolTip.SetToolTip(this.RefreshFileListButton, "Refresh the Files In Source Folder file list.");
			this.RefreshFileListButton.UseMnemonic = false;
			this.RefreshFileListButton.UseVisualStyleBackColor = true;
			this.RefreshFileListButton.Click += new System.EventHandler(this.RefreshFileListButton_Click);
			// 
			// OutputExtensionTextBox
			// 
			this.OutputExtensionTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.OutputExtensionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OutputExtensionTextBox.Location = new System.Drawing.Point(230, 4);
			this.OutputExtensionTextBox.Name = "OutputExtensionTextBox";
			this.OutputExtensionTextBox.Size = new System.Drawing.Size(34, 20);
			this.OutputExtensionTextBox.TabIndex = 10;
			this.MainToolTip.SetToolTip(this.OutputExtensionTextBox, "The output file\'s extension, requires a period at the beginning.");
			this.OutputExtensionTextBox.TextChanged += new System.EventHandler(this.OutputExtensionTextBox_TextChanged);
			// 
			// FilesInSourceFolderListBox
			// 
			this.FilesInSourceFolderListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.FilesInSourceFolderListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FilesInSourceFolderListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FilesInSourceFolderListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.FilesInSourceFolderListBox.FormattingEnabled = true;
			this.FilesInSourceFolderListBox.HorizontalScrollbar = true;
			this.FilesInSourceFolderListBox.Location = new System.Drawing.Point(0, 0);
			this.FilesInSourceFolderListBox.Name = "FilesInSourceFolderListBox";
			this.FilesInSourceFolderListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.FilesInSourceFolderListBox.Size = new System.Drawing.Size(496, 201);
			this.FilesInSourceFolderListBox.TabIndex = 0;
			this.FilesInSourceFolderListBox.TabStop = false;
			this.FilesInSourceFolderListBox.SelectedIndexChanged += new System.EventHandler(this.FilesInSourceFolderListBox_SelectedIndexChanged);
			// 
			// PingoCommandLabel
			// 
			this.PingoCommandLabel.AutoSize = true;
			this.PingoCommandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PingoCommandLabel.Location = new System.Drawing.Point(7, 53);
			this.PingoCommandLabel.Name = "PingoCommandLabel";
			this.PingoCommandLabel.Size = new System.Drawing.Size(144, 20);
			this.PingoCommandLabel.TabIndex = 10;
			this.PingoCommandLabel.Text = "Pingo Command:";
			// 
			// PingoCommandTextBox
			// 
			this.PingoCommandTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PingoCommandTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.PingoCommandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PingoCommandTextBox.Location = new System.Drawing.Point(6, 76);
			this.PingoCommandTextBox.Name = "PingoCommandTextBox";
			this.PingoCommandTextBox.Size = new System.Drawing.Size(1077, 20);
			this.PingoCommandTextBox.TabIndex = 11;
			this.MainToolTip.SetToolTip(this.PingoCommandTextBox, "The pingo command to be run, %1 gets replaced by the input file, as pingo only mo" +
        "difies in-place.\r\nCommand will be skipped if textbox is empty.");
			this.PingoCommandTextBox.TextChanged += new System.EventHandler(this.PingoCommandTextBox_TextChanged);
			// 
			// ProcessingCancelButton
			// 
			this.ProcessingCancelButton.AutoSize = true;
			this.ProcessingCancelButton.Enabled = false;
			this.ProcessingCancelButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ProcessingCancelButton.Location = new System.Drawing.Point(87, 131);
			this.ProcessingCancelButton.Name = "ProcessingCancelButton";
			this.ProcessingCancelButton.Size = new System.Drawing.Size(75, 23);
			this.ProcessingCancelButton.TabIndex = 12;
			this.ProcessingCancelButton.Text = "Cancel";
			this.MainToolTip.SetToolTip(this.ProcessingCancelButton, "Cancel processing the file(s), may mean that your files will be in an unfinished " +
        "state.");
			this.ProcessingCancelButton.UseMnemonic = false;
			this.ProcessingCancelButton.UseVisualStyleBackColor = true;
			this.ProcessingCancelButton.Visible = false;
			this.ProcessingCancelButton.Click += new System.EventHandler(this.ProcessingCancelButton_Click);
			// 
			// MainSplitContainer
			// 
			this.MainSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.MainSplitContainer.IsSplitterFixed = true;
			this.MainSplitContainer.Location = new System.Drawing.Point(0, 24);
			this.MainSplitContainer.Name = "MainSplitContainer";
			this.MainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// MainSplitContainer.Panel1
			// 
			this.MainSplitContainer.Panel1.Controls.Add(this.OutputAndSettingsSplitContainer);
			this.MainSplitContainer.Panel1MinSize = 346;
			// 
			// MainSplitContainer.Panel2
			// 
			this.MainSplitContainer.Panel2.Controls.Add(this.TotalSavingsLabel);
			this.MainSplitContainer.Panel2.Controls.Add(this.NewSizeLabel);
			this.MainSplitContainer.Panel2.Controls.Add(this.PreviousSizeLabel);
			this.MainSplitContainer.Panel2.Controls.Add(this.AppendToOutputLabel);
			this.MainSplitContainer.Panel2.Controls.Add(this.AppendToOutputTextBox);
			this.MainSplitContainer.Panel2.Controls.Add(this.PrependToOutputLabel);
			this.MainSplitContainer.Panel2.Controls.Add(this.PrependToOutputTextBox);
			this.MainSplitContainer.Panel2.Controls.Add(this.ProcessingProgressBar);
			this.MainSplitContainer.Panel2.Controls.Add(this.MagickCommandLabel);
			this.MainSplitContainer.Panel2.Controls.Add(this.ProcessingCancelButton);
			this.MainSplitContainer.Panel2.Controls.Add(this.MagickCommandTextBox);
			this.MainSplitContainer.Panel2.Controls.Add(this.PingoCommandLabel);
			this.MainSplitContainer.Panel2.Controls.Add(this.StartButton);
			this.MainSplitContainer.Panel2.Controls.Add(this.PingoCommandTextBox);
			this.MainSplitContainer.Panel2.Controls.Add(this.StatusMessageLabel);
			this.MainSplitContainer.Panel2MinSize = 168;
			this.MainSplitContainer.Size = new System.Drawing.Size(966, 527);
			this.MainSplitContainer.SplitterDistance = 346;
			this.MainSplitContainer.TabIndex = 13;
			// 
			// OutputAndSettingsSplitContainer
			// 
			this.OutputAndSettingsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OutputAndSettingsSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.OutputAndSettingsSplitContainer.IsSplitterFixed = true;
			this.OutputAndSettingsSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.OutputAndSettingsSplitContainer.Name = "OutputAndSettingsSplitContainer";
			this.OutputAndSettingsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// OutputAndSettingsSplitContainer.Panel1
			// 
			this.OutputAndSettingsSplitContainer.Panel1.Controls.Add(this.MaintainFolderStructureCheckBox);
			this.OutputAndSettingsSplitContainer.Panel1.Controls.Add(this.DeleteOriginalsAfterCheckBox);
			this.OutputAndSettingsSplitContainer.Panel1.Controls.Add(this.ReplaceOriginalsCheckBox);
			this.OutputAndSettingsSplitContainer.Panel1.Controls.Add(this.SourceFolderPathLabel);
			this.OutputAndSettingsSplitContainer.Panel1.Controls.Add(this.OutputFolderPathLabel);
			this.OutputAndSettingsSplitContainer.Panel1.Controls.Add(this.SourceFolderPathButton);
			this.OutputAndSettingsSplitContainer.Panel1.Controls.Add(this.OutputFolderPathTextBox);
			this.OutputAndSettingsSplitContainer.Panel1.Controls.Add(this.SourceFolderPathTextBox);
			this.OutputAndSettingsSplitContainer.Panel1.Controls.Add(this.OutputFolderPathButton);
			this.OutputAndSettingsSplitContainer.Panel1MinSize = 91;
			// 
			// OutputAndSettingsSplitContainer.Panel2
			// 
			this.OutputAndSettingsSplitContainer.Panel2.Controls.Add(this.SettingsAndFilesSplitContainer);
			this.OutputAndSettingsSplitContainer.Panel2MinSize = 249;
			this.OutputAndSettingsSplitContainer.Size = new System.Drawing.Size(964, 344);
			this.OutputAndSettingsSplitContainer.SplitterDistance = 91;
			this.OutputAndSettingsSplitContainer.TabIndex = 10;
			// 
			// MaintainFolderStructureCheckBox
			// 
			this.MaintainFolderStructureCheckBox.AutoSize = true;
			this.MaintainFolderStructureCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MaintainFolderStructureCheckBox.Location = new System.Drawing.Point(378, 50);
			this.MaintainFolderStructureCheckBox.Name = "MaintainFolderStructureCheckBox";
			this.MaintainFolderStructureCheckBox.Size = new System.Drawing.Size(169, 17);
			this.MaintainFolderStructureCheckBox.TabIndex = 18;
			this.MaintainFolderStructureCheckBox.Text = "Maintain Folder Structure";
			this.MainToolTip.SetToolTip(this.MaintainFolderStructureCheckBox, "If using Include Subfolders, and your Output Folder Path is different from your S" +
        "ource Folder Path,\r\nthen re-creates the same folder setup at the Output Folder P" +
        "ath.");
			this.MaintainFolderStructureCheckBox.UseMnemonic = false;
			this.MaintainFolderStructureCheckBox.UseVisualStyleBackColor = false;
			this.MaintainFolderStructureCheckBox.Visible = false;
			this.MaintainFolderStructureCheckBox.CheckedChanged += new System.EventHandler(this.MaintainFolderStructureCheckBox_CheckedChanged);
			// 
			// DeleteOriginalsAfterCheckBox
			// 
			this.DeleteOriginalsAfterCheckBox.AutoSize = true;
			this.DeleteOriginalsAfterCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DeleteOriginalsAfterCheckBox.Location = new System.Drawing.Point(171, 3);
			this.DeleteOriginalsAfterCheckBox.Name = "DeleteOriginalsAfterCheckBox";
			this.DeleteOriginalsAfterCheckBox.Size = new System.Drawing.Size(222, 17);
			this.DeleteOriginalsAfterCheckBox.TabIndex = 17;
			this.DeleteOriginalsAfterCheckBox.Text = "Delete Original(s) After (Not PSDs)";
			this.MainToolTip.SetToolTip(this.DeleteOriginalsAfterCheckBox, "Deletes the original files after processing, unless those files are being modifie" +
        "d in-place, never deletes PSDs.");
			this.DeleteOriginalsAfterCheckBox.UseMnemonic = false;
			this.DeleteOriginalsAfterCheckBox.UseVisualStyleBackColor = false;
			this.DeleteOriginalsAfterCheckBox.CheckedChanged += new System.EventHandler(this.DeleteOriginalsAfterCheckBox_CheckedChanged);
			// 
			// ReplaceOriginalsCheckBox
			// 
			this.ReplaceOriginalsCheckBox.AutoSize = true;
			this.ReplaceOriginalsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ReplaceOriginalsCheckBox.Location = new System.Drawing.Point(171, 50);
			this.ReplaceOriginalsCheckBox.Name = "ReplaceOriginalsCheckBox";
			this.ReplaceOriginalsCheckBox.Size = new System.Drawing.Size(201, 17);
			this.ReplaceOriginalsCheckBox.TabIndex = 16;
			this.ReplaceOriginalsCheckBox.Text = "Replace Original(s) (Not PSDs)";
			this.MainToolTip.SetToolTip(this.ReplaceOriginalsCheckBox, "Replaces the original images, modifying them in-place. Deletes originals if filen" +
        "ame or type is changing, never deletes PSDs.");
			this.ReplaceOriginalsCheckBox.UseMnemonic = false;
			this.ReplaceOriginalsCheckBox.UseVisualStyleBackColor = false;
			this.ReplaceOriginalsCheckBox.CheckedChanged += new System.EventHandler(this.ReplaceOriginalsCheckBox_CheckedChanged);
			// 
			// SettingsAndFilesSplitContainer
			// 
			this.SettingsAndFilesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SettingsAndFilesSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.SettingsAndFilesSplitContainer.IsSplitterFixed = true;
			this.SettingsAndFilesSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.SettingsAndFilesSplitContainer.Name = "SettingsAndFilesSplitContainer";
			// 
			// SettingsAndFilesSplitContainer.Panel1
			// 
			this.SettingsAndFilesSplitContainer.Panel1.Controls.Add(this.PresetsAndSettingsSplitContainer);
			this.SettingsAndFilesSplitContainer.Panel1MinSize = 458;
			// 
			// SettingsAndFilesSplitContainer.Panel2
			// 
			this.SettingsAndFilesSplitContainer.Panel2.Controls.Add(this.FilesInSourceFolderGroupBox);
			this.SettingsAndFilesSplitContainer.Panel2MinSize = 336;
			this.SettingsAndFilesSplitContainer.Size = new System.Drawing.Size(964, 249);
			this.SettingsAndFilesSplitContainer.SplitterDistance = 458;
			this.SettingsAndFilesSplitContainer.TabIndex = 7;
			// 
			// PresetsAndSettingsSplitContainer
			// 
			this.PresetsAndSettingsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PresetsAndSettingsSplitContainer.IsSplitterFixed = true;
			this.PresetsAndSettingsSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.PresetsAndSettingsSplitContainer.Name = "PresetsAndSettingsSplitContainer";
			// 
			// PresetsAndSettingsSplitContainer.Panel1
			// 
			this.PresetsAndSettingsSplitContainer.Panel1.Controls.Add(this.PresetSettingsGroupBox);
			this.PresetsAndSettingsSplitContainer.Panel1MinSize = 157;
			// 
			// PresetsAndSettingsSplitContainer.Panel2
			// 
			this.PresetsAndSettingsSplitContainer.Panel2.Controls.Add(this.MagickAndPingoSplitContainer);
			this.PresetsAndSettingsSplitContainer.Panel2MinSize = 297;
			this.PresetsAndSettingsSplitContainer.Size = new System.Drawing.Size(458, 249);
			this.PresetsAndSettingsSplitContainer.SplitterDistance = 157;
			this.PresetsAndSettingsSplitContainer.TabIndex = 7;
			// 
			// MagickAndPingoSplitContainer
			// 
			this.MagickAndPingoSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MagickAndPingoSplitContainer.IsSplitterFixed = true;
			this.MagickAndPingoSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.MagickAndPingoSplitContainer.Name = "MagickAndPingoSplitContainer";
			// 
			// MagickAndPingoSplitContainer.Panel1
			// 
			this.MagickAndPingoSplitContainer.Panel1.Controls.Add(this.ImagemagickSettingsGroupBox);
			this.MagickAndPingoSplitContainer.Panel1MinSize = 138;
			// 
			// MagickAndPingoSplitContainer.Panel2
			// 
			this.MagickAndPingoSplitContainer.Panel2.Controls.Add(this.PingoSettingsGroupBox);
			this.MagickAndPingoSplitContainer.Panel2MinSize = 155;
			this.MagickAndPingoSplitContainer.Size = new System.Drawing.Size(297, 249);
			this.MagickAndPingoSplitContainer.SplitterDistance = 138;
			this.MagickAndPingoSplitContainer.TabIndex = 7;
			// 
			// TotalSavingsLabel
			// 
			this.TotalSavingsLabel.AutoSize = true;
			this.TotalSavingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TotalSavingsLabel.Location = new System.Drawing.Point(708, 108);
			this.TotalSavingsLabel.Name = "TotalSavingsLabel";
			this.TotalSavingsLabel.Size = new System.Drawing.Size(217, 13);
			this.TotalSavingsLabel.TabIndex = 20;
			this.TotalSavingsLabel.Text = "Total Savings: XXX.XXYY or XX.XX%";
			// 
			// NewSizeLabel
			// 
			this.NewSizeLabel.AutoSize = true;
			this.NewSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.NewSizeLabel.Location = new System.Drawing.Point(574, 108);
			this.NewSizeLabel.Name = "NewSizeLabel";
			this.NewSizeLabel.Size = new System.Drawing.Size(128, 13);
			this.NewSizeLabel.TabIndex = 19;
			this.NewSizeLabel.Text = "New Size: XXX.XXYY";
			// 
			// PreviousSizeLabel
			// 
			this.PreviousSizeLabel.AutoSize = true;
			this.PreviousSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PreviousSizeLabel.Location = new System.Drawing.Point(439, 108);
			this.PreviousSizeLabel.Name = "PreviousSizeLabel";
			this.PreviousSizeLabel.Size = new System.Drawing.Size(129, 13);
			this.PreviousSizeLabel.TabIndex = 18;
			this.PreviousSizeLabel.Text = "Prev Size: XXX.XXYY";
			// 
			// AppendToOutputLabel
			// 
			this.AppendToOutputLabel.AutoSize = true;
			this.AppendToOutputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AppendToOutputLabel.Location = new System.Drawing.Point(306, 108);
			this.AppendToOutputLabel.Name = "AppendToOutputLabel";
			this.AppendToOutputLabel.Size = new System.Drawing.Size(54, 13);
			this.AppendToOutputLabel.TabIndex = 16;
			this.AppendToOutputLabel.Text = "Append:";
			// 
			// AppendToOutputTextBox
			// 
			this.AppendToOutputTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.AppendToOutputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AppendToOutputTextBox.Location = new System.Drawing.Point(366, 104);
			this.AppendToOutputTextBox.Name = "AppendToOutputTextBox";
			this.AppendToOutputTextBox.Size = new System.Drawing.Size(67, 20);
			this.AppendToOutputTextBox.TabIndex = 17;
			this.MainToolTip.SetToolTip(this.AppendToOutputTextBox, "String to be appended to the front of the existing filename.");
			this.AppendToOutputTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxRestrictToFilePermittedChars_KeyPress);
			// 
			// PrependToOutputLabel
			// 
			this.PrependToOutputLabel.AutoSize = true;
			this.PrependToOutputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PrependToOutputLabel.Location = new System.Drawing.Point(168, 108);
			this.PrependToOutputLabel.Name = "PrependToOutputLabel";
			this.PrependToOutputLabel.Size = new System.Drawing.Size(58, 13);
			this.PrependToOutputLabel.TabIndex = 14;
			this.PrependToOutputLabel.Text = "Prepend:";
			// 
			// PrependToOutputTextBox
			// 
			this.PrependToOutputTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.PrependToOutputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PrependToOutputTextBox.Location = new System.Drawing.Point(232, 104);
			this.PrependToOutputTextBox.Name = "PrependToOutputTextBox";
			this.PrependToOutputTextBox.Size = new System.Drawing.Size(67, 20);
			this.PrependToOutputTextBox.TabIndex = 15;
			this.MainToolTip.SetToolTip(this.PrependToOutputTextBox, "String to be prepended to the front of the existing filename.");
			this.PrependToOutputTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxRestrictToFilePermittedChars_KeyPress);
			// 
			// ProcessingProgressBar
			// 
			this.ProcessingProgressBar.Location = new System.Drawing.Point(6, 102);
			this.ProcessingProgressBar.Name = "ProcessingProgressBar";
			this.ProcessingProgressBar.Size = new System.Drawing.Size(156, 23);
			this.ProcessingProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.ProcessingProgressBar.TabIndex = 13;
			this.ProcessingProgressBar.Visible = false;
			// 
			// OptionsMenuStrip
			// 
			this.OptionsMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.OptionsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.PreferencesToolStripMenuItem,
            this.HelpToolStripMenuItem});
			this.OptionsMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.OptionsMenuStrip.Name = "OptionsMenuStrip";
			this.OptionsMenuStrip.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
			this.OptionsMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.OptionsMenuStrip.Size = new System.Drawing.Size(966, 24);
			this.OptionsMenuStrip.TabIndex = 14;
			// 
			// FileToolStripMenuItem
			// 
			this.FileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenUserConfigToolStripMenuItem,
            this.SaveCurrentSettingsToolStripMenuItem,
            this.CloseToolStripMenuItem});
			this.FileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.FileToolStripMenuItem.Text = "File";
			// 
			// OpenUserConfigToolStripMenuItem
			// 
			this.OpenUserConfigToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.OpenUserConfigToolStripMenuItem.Name = "OpenUserConfigToolStripMenuItem";
			this.OpenUserConfigToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.OpenUserConfigToolStripMenuItem.Text = "Open User Config";
			this.OpenUserConfigToolStripMenuItem.ToolTipText = "Opens the user config file.";
			this.OpenUserConfigToolStripMenuItem.Click += new System.EventHandler(this.OpenUserConfigToolStripMenuItem_Click);
			// 
			// SaveCurrentSettingsToolStripMenuItem
			// 
			this.SaveCurrentSettingsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.SaveCurrentSettingsToolStripMenuItem.Name = "SaveCurrentSettingsToolStripMenuItem";
			this.SaveCurrentSettingsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.SaveCurrentSettingsToolStripMenuItem.Text = "Save Current Settings";
			this.SaveCurrentSettingsToolStripMenuItem.ToolTipText = "A manual option to save the current settings to the user config file.\r\nSettings a" +
    "re saved any time any setting is changed, so this option is\r\nsomewhat redundant." +
    "";
			this.SaveCurrentSettingsToolStripMenuItem.Click += new System.EventHandler(this.SaveCurrentSettingsToolStripMenuItem_Click);
			// 
			// CloseToolStripMenuItem
			// 
			this.CloseToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
			this.CloseToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.CloseToolStripMenuItem.Text = "Exit";
			this.CloseToolStripMenuItem.ToolTipText = "Closes the program, canceling any current processing.";
			this.CloseToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
			// 
			// PreferencesToolStripMenuItem
			// 
			this.PreferencesToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.PreferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OutputSettingsToolStripMenuItem,
            this.ShouldOutputToNewFolderToolStripMenuItem,
            this.AddTagsToFilenamesToolStripMenuItem,
            this.AddTagsToNewFolderToolStripMenuItem,
            this.CheckForUpdatesToolStripSeparator,
            this.CheckForUpdatesOnStartupToolStripMenuItem,
            this.DisableFailedToCheckMessageToolStripMenuItem,
            this.CheckForUpdatesToolStripMenuItem});
			this.PreferencesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
			this.PreferencesToolStripMenuItem.Name = "PreferencesToolStripMenuItem";
			this.PreferencesToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
			this.PreferencesToolStripMenuItem.Text = "Preferences";
			// 
			// OutputSettingsToolStripMenuItem
			// 
			this.OutputSettingsToolStripMenuItem.Name = "OutputSettingsToolStripMenuItem";
			this.OutputSettingsToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.OutputSettingsToolStripMenuItem.Text = "Output Settings...";
			this.OutputSettingsToolStripMenuItem.Click += new System.EventHandler(this.OutputSettingsToolStripMenuItem_Click);
			// 
			// ShouldOutputToNewFolderToolStripMenuItem
			// 
			this.ShouldOutputToNewFolderToolStripMenuItem.CheckOnClick = true;
			this.ShouldOutputToNewFolderToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ShouldOutputToNewFolderToolStripMenuItem.Name = "ShouldOutputToNewFolderToolStripMenuItem";
			this.ShouldOutputToNewFolderToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.ShouldOutputToNewFolderToolStripMenuItem.Text = "Should Output To New Folder";
			this.ShouldOutputToNewFolderToolStripMenuItem.ToolTipText = "Toggles whether or not images should be output to a newly created folder.";
			this.ShouldOutputToNewFolderToolStripMenuItem.Click += new System.EventHandler(this.ShouldOutputToNewFolderToolStripMenuItem_Click);
			// 
			// AddTagsToFilenamesToolStripMenuItem
			// 
			this.AddTagsToFilenamesToolStripMenuItem.CheckOnClick = true;
			this.AddTagsToFilenamesToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.AddTagsToFilenamesToolStripMenuItem.Name = "AddTagsToFilenamesToolStripMenuItem";
			this.AddTagsToFilenamesToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.AddTagsToFilenamesToolStripMenuItem.Text = "Add Tags To Filenames";
			this.AddTagsToFilenamesToolStripMenuItem.ToolTipText = "A quick toggle for adding tags to filenames.\r\nActual settings are set in the Outp" +
    "ut Settings window.";
			this.AddTagsToFilenamesToolStripMenuItem.Click += new System.EventHandler(this.AddTagsToFilenamesToolStripMenuItem_Click);
			// 
			// AddTagsToNewFolderToolStripMenuItem
			// 
			this.AddTagsToNewFolderToolStripMenuItem.CheckOnClick = true;
			this.AddTagsToNewFolderToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.AddTagsToNewFolderToolStripMenuItem.Name = "AddTagsToNewFolderToolStripMenuItem";
			this.AddTagsToNewFolderToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.AddTagsToNewFolderToolStripMenuItem.Text = "Add Tags To New Folder";
			this.AddTagsToNewFolderToolStripMenuItem.ToolTipText = "A quick toggle for adding tags to the new folder created\r\nby the Should Output To" +
    " New Folder option.\r\nActual settings are set in the Output Settings window.";
			this.AddTagsToNewFolderToolStripMenuItem.Click += new System.EventHandler(this.AddTagsToNewFolderToolStripMenuItem_Click);
			// 
			// CheckForUpdatesToolStripSeparator
			// 
			this.CheckForUpdatesToolStripSeparator.Name = "CheckForUpdatesToolStripSeparator";
			this.CheckForUpdatesToolStripSeparator.Size = new System.Drawing.Size(230, 6);
			// 
			// CheckForUpdatesOnStartupToolStripMenuItem
			// 
			this.CheckForUpdatesOnStartupToolStripMenuItem.Checked = true;
			this.CheckForUpdatesOnStartupToolStripMenuItem.CheckOnClick = true;
			this.CheckForUpdatesOnStartupToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CheckForUpdatesOnStartupToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.CheckForUpdatesOnStartupToolStripMenuItem.Name = "CheckForUpdatesOnStartupToolStripMenuItem";
			this.CheckForUpdatesOnStartupToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.CheckForUpdatesOnStartupToolStripMenuItem.Text = "Check For Updates On Startup";
			this.CheckForUpdatesOnStartupToolStripMenuItem.ToolTipText = "Toggle whether or not a check for any updates should be made when starting up img" +
    "danke.";
			this.CheckForUpdatesOnStartupToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesOnStartupToolStripMenuItem_Click);
			// 
			// DisableFailedToCheckMessageToolStripMenuItem
			// 
			this.DisableFailedToCheckMessageToolStripMenuItem.CheckOnClick = true;
			this.DisableFailedToCheckMessageToolStripMenuItem.Name = "DisableFailedToCheckMessageToolStripMenuItem";
			this.DisableFailedToCheckMessageToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.DisableFailedToCheckMessageToolStripMenuItem.Text = "Disable Failed Check Message";
			this.DisableFailedToCheckMessageToolStripMenuItem.ToolTipText = "Toggles disabling the error message if the check for updates fails.";
			this.DisableFailedToCheckMessageToolStripMenuItem.Click += new System.EventHandler(this.DisableFailedToCheckMessageToolStripMenuItem_Click);
			// 
			// CheckForUpdatesToolStripMenuItem
			// 
			this.CheckForUpdatesToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.CheckForUpdatesToolStripMenuItem.Name = "CheckForUpdatesToolStripMenuItem";
			this.CheckForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.CheckForUpdatesToolStripMenuItem.Text = "Check For Updates";
			this.CheckForUpdatesToolStripMenuItem.ToolTipText = "Toggles the check for any updates that happens on startup.";
			this.CheckForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesToolStripMenuItem_Click);
			// 
			// HelpToolStripMenuItem
			// 
			this.HelpToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GitHubToolStripMenuItem,
            this.OpenDocumentationToolStripMenuItem,
            this.AboutToolStripMenuItem});
			this.HelpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
			this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
			this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.HelpToolStripMenuItem.Text = "Help";
			// 
			// GitHubToolStripMenuItem
			// 
			this.GitHubToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.GitHubToolStripMenuItem.Name = "GitHubToolStripMenuItem";
			this.GitHubToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.GitHubToolStripMenuItem.Text = "GitHub";
			this.GitHubToolStripMenuItem.ToolTipText = "Opens the imgdanke GitHub page in the default browser.\r\nThe URL opened is: https:" +
    "//github.com/DrWhoCares/imgdanke";
			this.GitHubToolStripMenuItem.Click += new System.EventHandler(this.GitHubToolStripMenuItem_Click);
			// 
			// OpenDocumentationToolStripMenuItem
			// 
			this.OpenDocumentationToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.OpenDocumentationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenDocumentationFromFileToolStripMenuItem,
            this.OpenDocumentationGitHubToolStripMenuItem});
			this.OpenDocumentationToolStripMenuItem.Name = "OpenDocumentationToolStripMenuItem";
			this.OpenDocumentationToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.OpenDocumentationToolStripMenuItem.Text = "Open Documentation";
			// 
			// OpenDocumentationFromFileToolStripMenuItem
			// 
			this.OpenDocumentationFromFileToolStripMenuItem.Name = "OpenDocumentationFromFileToolStripMenuItem";
			this.OpenDocumentationFromFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.OpenDocumentationFromFileToolStripMenuItem.Text = "From File";
			this.OpenDocumentationFromFileToolStripMenuItem.ToolTipText = "Opens the local README.md file.";
			this.OpenDocumentationFromFileToolStripMenuItem.Click += new System.EventHandler(this.OpenDocumentationFromFileToolStripMenuItem_Click);
			// 
			// OpenDocumentationGitHubToolStripMenuItem
			// 
			this.OpenDocumentationGitHubToolStripMenuItem.Name = "OpenDocumentationGitHubToolStripMenuItem";
			this.OpenDocumentationGitHubToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.OpenDocumentationGitHubToolStripMenuItem.Text = "GitHub";
			this.OpenDocumentationGitHubToolStripMenuItem.ToolTipText = "Opens the README.md file on the GitHub.\r\nThe URL is: https://github.com/DrWhoCare" +
    "s/imgdanke/blob/master/README.md";
			this.OpenDocumentationGitHubToolStripMenuItem.Click += new System.EventHandler(this.OpenDocumentationGitHubToolStripMenuItem_Click);
			// 
			// AboutToolStripMenuItem
			// 
			this.AboutToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
			this.AboutToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.AboutToolStripMenuItem.Text = "About";
			this.AboutToolStripMenuItem.ToolTipText = "Displays some information on the current build.";
			this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
			// 
			// MainToolTip
			// 
			this.MainToolTip.BackColor = System.Drawing.SystemColors.GrayText;
			this.MainToolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
			this.ClientSize = new System.Drawing.Size(966, 551);
			this.Controls.Add(this.MainSplitContainer);
			this.Controls.Add(this.OptionsMenuStrip);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.OptionsMenuStrip;
			this.MinimumSize = new System.Drawing.Size(982, 590);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "imgdanke";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
			this.ImagemagickSettingsGroupBox.ResumeLayout(false);
			this.ImagemagickSettingsGroupBox.PerformLayout();
			this.PingoSettingsGroupBox.ResumeLayout(false);
			this.PingoSettingsGroupBox.PerformLayout();
			this.PresetSettingsGroupBox.ResumeLayout(false);
			this.PresetSettingsGroupBox.PerformLayout();
			this.FilesInSourceFolderGroupBox.ResumeLayout(false);
			this.FileUIAndFileListSplitContainer.Panel1.ResumeLayout(false);
			this.FileUIAndFileListSplitContainer.Panel1.PerformLayout();
			this.FileUIAndFileListSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.FileUIAndFileListSplitContainer)).EndInit();
			this.FileUIAndFileListSplitContainer.ResumeLayout(false);
			this.MainSplitContainer.Panel1.ResumeLayout(false);
			this.MainSplitContainer.Panel2.ResumeLayout(false);
			this.MainSplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
			this.MainSplitContainer.ResumeLayout(false);
			this.OutputAndSettingsSplitContainer.Panel1.ResumeLayout(false);
			this.OutputAndSettingsSplitContainer.Panel1.PerformLayout();
			this.OutputAndSettingsSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.OutputAndSettingsSplitContainer)).EndInit();
			this.OutputAndSettingsSplitContainer.ResumeLayout(false);
			this.SettingsAndFilesSplitContainer.Panel1.ResumeLayout(false);
			this.SettingsAndFilesSplitContainer.Panel2.ResumeLayout(false);
			this.SettingsAndFilesSplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.SettingsAndFilesSplitContainer)).EndInit();
			this.SettingsAndFilesSplitContainer.ResumeLayout(false);
			this.PresetsAndSettingsSplitContainer.Panel1.ResumeLayout(false);
			this.PresetsAndSettingsSplitContainer.Panel1.PerformLayout();
			this.PresetsAndSettingsSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PresetsAndSettingsSplitContainer)).EndInit();
			this.PresetsAndSettingsSplitContainer.ResumeLayout(false);
			this.MagickAndPingoSplitContainer.Panel1.ResumeLayout(false);
			this.MagickAndPingoSplitContainer.Panel1.PerformLayout();
			this.MagickAndPingoSplitContainer.Panel2.ResumeLayout(false);
			this.MagickAndPingoSplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MagickAndPingoSplitContainer)).EndInit();
			this.MagickAndPingoSplitContainer.ResumeLayout(false);
			this.OptionsMenuStrip.ResumeLayout(false);
			this.OptionsMenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox MagickCommandTextBox;
		private System.Windows.Forms.Label MagickCommandLabel;
		private System.Windows.Forms.Button SourceFolderPathButton;
		private System.Windows.Forms.TextBox SourceFolderPathTextBox;
		private System.Windows.Forms.Button StartButton;
		private System.Windows.Forms.Label StatusMessageLabel;
		private System.Windows.Forms.TextBox OutputFolderPathTextBox;
		private System.Windows.Forms.Button OutputFolderPathButton;
		private System.Windows.Forms.Label SourceFolderPathLabel;
		private System.Windows.Forms.Label OutputFolderPathLabel;
		private System.Windows.Forms.GroupBox ImagemagickSettingsGroupBox;
		private System.Windows.Forms.GroupBox PingoSettingsGroupBox;
		private System.Windows.Forms.GroupBox PresetSettingsGroupBox;
		private System.Windows.Forms.RadioButton NoPresetRadioButton;
		private System.Windows.Forms.RadioButton MagickEightBppColorPresetRadioButton;
		private System.Windows.Forms.RadioButton MagickFourBppColorPresetRadioButton;
		private System.Windows.Forms.RadioButton OneBppGrayPresetRadioButton;
		private System.Windows.Forms.RadioButton EightBppGrayPresetRadioButton;
		private System.Windows.Forms.RadioButton FourBppGrayPresetRadioButton;
		private System.Windows.Forms.GroupBox FilesInSourceFolderGroupBox;
		private System.Windows.Forms.ListBox FilesInSourceFolderListBox;
		private System.Windows.Forms.ComboBox MagickDitherComboBox;
		private System.Windows.Forms.ComboBox MagickColorspaceComboBox;
		private System.Windows.Forms.TextBox MagickPosterizeTextBox;
		private System.Windows.Forms.Label MagickPosterizeLabel;
		private System.Windows.Forms.CheckBox MagickNormalizeCheckBox;
		private System.Windows.Forms.Label ColorPresetsLabel;
		private System.Windows.Forms.Label GrayscalePresetsLabel;
		private System.Windows.Forms.ComboBox PingoOptimizationLevelComboBox;
		private System.Windows.Forms.CheckBox PingoStripCheckBox;
		private System.Windows.Forms.TextBox MagickColorsTextBox;
		private System.Windows.Forms.Label MagickColorsLabel;
		private System.Windows.Forms.TextBox MagickDepthTextBox;
		private System.Windows.Forms.Label MagickDepthLabel;
		private System.Windows.Forms.RadioButton CustomPresetRadioButton;
		private System.Windows.Forms.Label PingoCommandLabel;
		private System.Windows.Forms.TextBox PingoCommandTextBox;
		private System.Windows.Forms.Label PingoPNGPaletteLabel;
		private System.Windows.Forms.RadioButton PingoEightBppColorPresetRadioButton;
		private System.Windows.Forms.Button MassFileSelectorButton;
		private System.Windows.Forms.Button RefreshFileListButton;
		private System.Windows.Forms.Label OutputExtensionLabel;
		private System.Windows.Forms.TextBox OutputExtensionTextBox;
		private System.Windows.Forms.Button ProcessingCancelButton;
		private System.Windows.Forms.ComboBox PingoPNGPaletteComboBox;
		private System.Windows.Forms.Label PingoOptimizationLevelLabel;
		private System.Windows.Forms.RadioButton PingoFourBppColorPresetRadioButton;
		private System.Windows.Forms.SplitContainer MainSplitContainer;
		private System.Windows.Forms.SplitContainer OutputAndSettingsSplitContainer;
		private System.Windows.Forms.SplitContainer SettingsAndFilesSplitContainer;
		private System.Windows.Forms.SplitContainer PresetsAndSettingsSplitContainer;
		private System.Windows.Forms.SplitContainer MagickAndPingoSplitContainer;
		private System.Windows.Forms.SplitContainer FileUIAndFileListSplitContainer;
		private System.Windows.Forms.CheckBox PingoNoDitheringCheckBox;
		private System.Windows.Forms.CheckBox IncludePSDsCheckBox;
		private System.Windows.Forms.CheckBox IncludeSubfoldersCheckBox;
		private System.Windows.Forms.ProgressBar ProcessingProgressBar;
		private System.Windows.Forms.Label PrependToOutputLabel;
		private System.Windows.Forms.TextBox PrependToOutputTextBox;
		private System.Windows.Forms.Label AppendToOutputLabel;
		private System.Windows.Forms.TextBox AppendToOutputTextBox;
		private System.Windows.Forms.Label TotalSavingsLabel;
		private System.Windows.Forms.Label NewSizeLabel;
		private System.Windows.Forms.Label PreviousSizeLabel;
		private System.Windows.Forms.CheckBox ReplaceOriginalsCheckBox;
		private System.Windows.Forms.CheckBox DeleteOriginalsAfterCheckBox;
		private System.Windows.Forms.CheckBox MaintainFolderStructureCheckBox;
		private System.Windows.Forms.CheckBox MagickContrastStretchCheckBox;
		private System.Windows.Forms.MenuStrip OptionsMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem PreferencesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenUserConfigToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveCurrentSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem GitHubToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenDocumentationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenDocumentationFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenDocumentationGitHubToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
		private System.Windows.Forms.ToolTip MainToolTip;
		private System.Windows.Forms.ToolStripMenuItem OutputSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ShouldOutputToNewFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AddTagsToFilenamesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AddTagsToNewFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator CheckForUpdatesToolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem CheckForUpdatesOnStartupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CheckForUpdatesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem DisableFailedToCheckMessageToolStripMenuItem;
	}
}

