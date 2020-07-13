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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.MagickCommandTextBox = new System.Windows.Forms.TextBox();
			this.MagickCommandLabel = new System.Windows.Forms.Label();
			this.SourceFolderPathButton = new System.Windows.Forms.Button();
			this.SourceFolderPathTextBox = new System.Windows.Forms.TextBox();
			this.ApplyButton = new System.Windows.Forms.Button();
			this.StatusMessageLabel = new System.Windows.Forms.Label();
			this.OutputFolderPathTextBox = new System.Windows.Forms.TextBox();
			this.OutputFolderPathButton = new System.Windows.Forms.Button();
			this.SourceFolderPathLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ImagemagickSettingsGroupBox = new System.Windows.Forms.GroupBox();
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
			this.PingoPNGPaletteComboBox = new System.Windows.Forms.ComboBox();
			this.PingoStripCheckBox = new System.Windows.Forms.CheckBox();
			this.PingoSARadioButton = new System.Windows.Forms.RadioButton();
			this.PingoSBRadioButton = new System.Windows.Forms.RadioButton();
			this.PingoPNGPaletteLabel = new System.Windows.Forms.Label();
			this.PingoOptimizationLevelComboBox = new System.Windows.Forms.ComboBox();
			this.PresetSettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.PingoEightBppColorPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.CustomPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.MagickEightBppColorPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.ColorPresetsLabel = new System.Windows.Forms.Label();
			this.FourBppColorPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.GrayscalePresetsLabel = new System.Windows.Forms.Label();
			this.OneBppGrayPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.NoPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.EightBppGrayPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.FourBppGrayPresetRadioButton = new System.Windows.Forms.RadioButton();
			this.FilesInSourceFolderGroupBox = new System.Windows.Forms.GroupBox();
			this.OutputExtensionLabel = new System.Windows.Forms.Label();
			this.OutputExtensionTextBox = new System.Windows.Forms.TextBox();
			this.RefreshFileListButton = new System.Windows.Forms.Button();
			this.MassFileSelectorButton = new System.Windows.Forms.Button();
			this.FilesInSourceFolderListBox = new System.Windows.Forms.ListBox();
			this.PingoCommandLabel = new System.Windows.Forms.Label();
			this.PingoCommandTextBox = new System.Windows.Forms.TextBox();
			this.ProcessingCancelButton = new System.Windows.Forms.Button();
			this.ImagemagickSettingsGroupBox.SuspendLayout();
			this.PingoSettingsGroupBox.SuspendLayout();
			this.PresetSettingsGroupBox.SuspendLayout();
			this.FilesInSourceFolderGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// MagickCommandTextBox
			// 
			this.MagickCommandTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.MagickCommandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MagickCommandTextBox.Location = new System.Drawing.Point(12, 374);
			this.MagickCommandTextBox.Name = "MagickCommandTextBox";
			this.MagickCommandTextBox.Size = new System.Drawing.Size(776, 20);
			this.MagickCommandTextBox.TabIndex = 7;
			this.MagickCommandTextBox.TextChanged += new System.EventHandler(this.MagickCommandTextBox_TextChanged);
			// 
			// MagickCommandLabel
			// 
			this.MagickCommandLabel.AutoSize = true;
			this.MagickCommandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MagickCommandLabel.Location = new System.Drawing.Point(13, 351);
			this.MagickCommandLabel.Name = "MagickCommandLabel";
			this.MagickCommandLabel.Size = new System.Drawing.Size(155, 20);
			this.MagickCommandLabel.TabIndex = 1;
			this.MagickCommandLabel.Text = "Magick Command:";
			// 
			// SourceFolderPathButton
			// 
			this.SourceFolderPathButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.SourceFolderPathButton.Location = new System.Drawing.Point(12, 27);
			this.SourceFolderPathButton.Name = "SourceFolderPathButton";
			this.SourceFolderPathButton.Size = new System.Drawing.Size(32, 21);
			this.SourceFolderPathButton.TabIndex = 0;
			this.SourceFolderPathButton.Text = "...";
			this.SourceFolderPathButton.UseVisualStyleBackColor = true;
			this.SourceFolderPathButton.Click += new System.EventHandler(this.SourceFolderPathButton_Click);
			// 
			// SourceFolderPathTextBox
			// 
			this.SourceFolderPathTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.SourceFolderPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SourceFolderPathTextBox.Location = new System.Drawing.Point(50, 27);
			this.SourceFolderPathTextBox.Name = "SourceFolderPathTextBox";
			this.SourceFolderPathTextBox.Size = new System.Drawing.Size(738, 20);
			this.SourceFolderPathTextBox.TabIndex = 1;
			this.SourceFolderPathTextBox.TextChanged += new System.EventHandler(this.SourceFolderPathTextBox_TextChanged);
			// 
			// ApplyButton
			// 
			this.ApplyButton.Enabled = false;
			this.ApplyButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ApplyButton.Location = new System.Drawing.Point(12, 446);
			this.ApplyButton.Name = "ApplyButton";
			this.ApplyButton.Size = new System.Drawing.Size(75, 23);
			this.ApplyButton.TabIndex = 8;
			this.ApplyButton.Text = "Apply";
			this.ApplyButton.UseVisualStyleBackColor = true;
			this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
			// 
			// StatusMessageLabel
			// 
			this.StatusMessageLabel.AutoSize = true;
			this.StatusMessageLabel.Location = new System.Drawing.Point(94, 452);
			this.StatusMessageLabel.Name = "StatusMessageLabel";
			this.StatusMessageLabel.Size = new System.Drawing.Size(374, 13);
			this.StatusMessageLabel.TabIndex = 5;
			this.StatusMessageLabel.Text = "Use %1 as a placeholder for the input filename and %2 for the output filename.";
			// 
			// OutputFolderPathTextBox
			// 
			this.OutputFolderPathTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.OutputFolderPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OutputFolderPathTextBox.Location = new System.Drawing.Point(50, 75);
			this.OutputFolderPathTextBox.Name = "OutputFolderPathTextBox";
			this.OutputFolderPathTextBox.Size = new System.Drawing.Size(738, 20);
			this.OutputFolderPathTextBox.TabIndex = 3;
			this.OutputFolderPathTextBox.TextChanged += new System.EventHandler(this.OutputFolderPathTextBox_TextChanged);
			// 
			// OutputFolderPathButton
			// 
			this.OutputFolderPathButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.OutputFolderPathButton.Location = new System.Drawing.Point(12, 74);
			this.OutputFolderPathButton.Name = "OutputFolderPathButton";
			this.OutputFolderPathButton.Size = new System.Drawing.Size(32, 21);
			this.OutputFolderPathButton.TabIndex = 2;
			this.OutputFolderPathButton.Text = "...";
			this.OutputFolderPathButton.UseVisualStyleBackColor = true;
			this.OutputFolderPathButton.Click += new System.EventHandler(this.OutputFolderPathButton_Click);
			// 
			// SourceFolderPathLabel
			// 
			this.SourceFolderPathLabel.AutoSize = true;
			this.SourceFolderPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SourceFolderPathLabel.Location = new System.Drawing.Point(13, 4);
			this.SourceFolderPathLabel.Name = "SourceFolderPathLabel";
			this.SourceFolderPathLabel.Size = new System.Drawing.Size(169, 20);
			this.SourceFolderPathLabel.TabIndex = 8;
			this.SourceFolderPathLabel.Text = "Source Folder Path:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(167, 20);
			this.label1.TabIndex = 9;
			this.label1.Text = "Output Folder Path:";
			// 
			// ImagemagickSettingsGroupBox
			// 
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickDepthTextBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickDepthLabel);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickColorsTextBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickColorsLabel);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickPosterizeTextBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickPosterizeLabel);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickNormalizeCheckBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickColorspaceComboBox);
			this.ImagemagickSettingsGroupBox.Controls.Add(this.MagickDitherComboBox);
			this.ImagemagickSettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.ImagemagickSettingsGroupBox.Location = new System.Drawing.Point(218, 101);
			this.ImagemagickSettingsGroupBox.Name = "ImagemagickSettingsGroupBox";
			this.ImagemagickSettingsGroupBox.Size = new System.Drawing.Size(136, 247);
			this.ImagemagickSettingsGroupBox.TabIndex = 5;
			this.ImagemagickSettingsGroupBox.TabStop = false;
			this.ImagemagickSettingsGroupBox.Text = "Imagemagick Settings";
			// 
			// MagickDepthTextBox
			// 
			this.MagickDepthTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.MagickDepthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MagickDepthTextBox.Location = new System.Drawing.Point(52, 99);
			this.MagickDepthTextBox.Name = "MagickDepthTextBox";
			this.MagickDepthTextBox.Size = new System.Drawing.Size(77, 20);
			this.MagickDepthTextBox.TabIndex = 9;
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
			this.MagickNormalizeCheckBox.UseVisualStyleBackColor = true;
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
			this.MagickDitherComboBox.SelectedIndexChanged += new System.EventHandler(this.MagickDitherComboBox_SelectedIndexChanged);
			// 
			// PingoSettingsGroupBox
			// 
			this.PingoSettingsGroupBox.Controls.Add(this.PingoPNGPaletteComboBox);
			this.PingoSettingsGroupBox.Controls.Add(this.PingoStripCheckBox);
			this.PingoSettingsGroupBox.Controls.Add(this.PingoSARadioButton);
			this.PingoSettingsGroupBox.Controls.Add(this.PingoSBRadioButton);
			this.PingoSettingsGroupBox.Controls.Add(this.PingoPNGPaletteLabel);
			this.PingoSettingsGroupBox.Controls.Add(this.PingoOptimizationLevelComboBox);
			this.PingoSettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.PingoSettingsGroupBox.Location = new System.Drawing.Point(360, 101);
			this.PingoSettingsGroupBox.Name = "PingoSettingsGroupBox";
			this.PingoSettingsGroupBox.Size = new System.Drawing.Size(145, 247);
			this.PingoSettingsGroupBox.TabIndex = 6;
			this.PingoSettingsGroupBox.TabStop = false;
			this.PingoSettingsGroupBox.Text = "pingo Settings";
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
			this.PingoPNGPaletteComboBox.SelectedIndexChanged += new System.EventHandler(this.PingoPNGPaletteComboBox_SelectedIndexChanged);
			// 
			// PingoStripCheckBox
			// 
			this.PingoStripCheckBox.AutoSize = true;
			this.PingoStripCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PingoStripCheckBox.Location = new System.Drawing.Point(6, 120);
			this.PingoStripCheckBox.Name = "PingoStripCheckBox";
			this.PingoStripCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.PingoStripCheckBox.Size = new System.Drawing.Size(54, 17);
			this.PingoStripCheckBox.TabIndex = 7;
			this.PingoStripCheckBox.Text = "strip-";
			this.PingoStripCheckBox.UseVisualStyleBackColor = true;
			this.PingoStripCheckBox.CheckedChanged += new System.EventHandler(this.PingoStripCheckBox_CheckedChanged);
			// 
			// PingoSARadioButton
			// 
			this.PingoSARadioButton.AutoSize = true;
			this.PingoSARadioButton.Enabled = false;
			this.PingoSARadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PingoSARadioButton.Location = new System.Drawing.Point(6, 71);
			this.PingoSARadioButton.Name = "PingoSARadioButton";
			this.PingoSARadioButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.PingoSARadioButton.Size = new System.Drawing.Size(42, 17);
			this.PingoSARadioButton.TabIndex = 15;
			this.PingoSARadioButton.TabStop = true;
			this.PingoSARadioButton.Text = "sa-";
			this.PingoSARadioButton.UseVisualStyleBackColor = true;
			this.PingoSARadioButton.CheckedChanged += new System.EventHandler(this.PingoSARadioButton_CheckedChanged);
			// 
			// PingoSBRadioButton
			// 
			this.PingoSBRadioButton.AutoSize = true;
			this.PingoSBRadioButton.Enabled = false;
			this.PingoSBRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PingoSBRadioButton.Location = new System.Drawing.Point(6, 48);
			this.PingoSBRadioButton.Name = "PingoSBRadioButton";
			this.PingoSBRadioButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.PingoSBRadioButton.Size = new System.Drawing.Size(42, 17);
			this.PingoSBRadioButton.TabIndex = 14;
			this.PingoSBRadioButton.TabStop = true;
			this.PingoSBRadioButton.Text = "sb-";
			this.PingoSBRadioButton.UseVisualStyleBackColor = true;
			this.PingoSBRadioButton.CheckedChanged += new System.EventHandler(this.PingoSBRadioButton_CheckedChanged);
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
            "-s0",
            "-s1",
            "-s2",
            "-s3",
            "-s4",
            "-s5",
            "-s6",
            "-s7",
            "-s8",
            "-s9"});
			this.PingoOptimizationLevelComboBox.Location = new System.Drawing.Point(6, 93);
			this.PingoOptimizationLevelComboBox.Name = "PingoOptimizationLevelComboBox";
			this.PingoOptimizationLevelComboBox.Size = new System.Drawing.Size(91, 21);
			this.PingoOptimizationLevelComboBox.TabIndex = 6;
			this.PingoOptimizationLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.PingoOptimizationLevelComboBox_SelectedIndexChanged);
			// 
			// PresetSettingsGroupBox
			// 
			this.PresetSettingsGroupBox.Controls.Add(this.PingoEightBppColorPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.CustomPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.MagickEightBppColorPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.ColorPresetsLabel);
			this.PresetSettingsGroupBox.Controls.Add(this.FourBppColorPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.GrayscalePresetsLabel);
			this.PresetSettingsGroupBox.Controls.Add(this.OneBppGrayPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.NoPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.EightBppGrayPresetRadioButton);
			this.PresetSettingsGroupBox.Controls.Add(this.FourBppGrayPresetRadioButton);
			this.PresetSettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.PresetSettingsGroupBox.Location = new System.Drawing.Point(12, 101);
			this.PresetSettingsGroupBox.Name = "PresetSettingsGroupBox";
			this.PresetSettingsGroupBox.Size = new System.Drawing.Size(200, 247);
			this.PresetSettingsGroupBox.TabIndex = 4;
			this.PresetSettingsGroupBox.TabStop = false;
			this.PresetSettingsGroupBox.Text = "Preset Settings";
			// 
			// PingoEightBppColorPresetRadioButton
			// 
			this.PingoEightBppColorPresetRadioButton.AutoSize = true;
			this.PingoEightBppColorPresetRadioButton.Location = new System.Drawing.Point(5, 204);
			this.PingoEightBppColorPresetRadioButton.Name = "PingoEightBppColorPresetRadioButton";
			this.PingoEightBppColorPresetRadioButton.Size = new System.Drawing.Size(145, 17);
			this.PingoEightBppColorPresetRadioButton.TabIndex = 14;
			this.PingoEightBppColorPresetRadioButton.TabStop = true;
			this.PingoEightBppColorPresetRadioButton.Text = "8 bpp (256 colors) (pingo)";
			this.PingoEightBppColorPresetRadioButton.UseVisualStyleBackColor = true;
			this.PingoEightBppColorPresetRadioButton.CheckedChanged += new System.EventHandler(this.PingoEightBppColorPresetRadioButton_CheckedChanged);
			// 
			// CustomPresetRadioButton
			// 
			this.CustomPresetRadioButton.AutoSize = true;
			this.CustomPresetRadioButton.Location = new System.Drawing.Point(5, 42);
			this.CustomPresetRadioButton.Name = "CustomPresetRadioButton";
			this.CustomPresetRadioButton.Size = new System.Drawing.Size(135, 17);
			this.CustomPresetRadioButton.TabIndex = 1;
			this.CustomPresetRadioButton.TabStop = true;
			this.CustomPresetRadioButton.Text = "Custom (Save Settings)";
			this.CustomPresetRadioButton.UseVisualStyleBackColor = true;
			this.CustomPresetRadioButton.CheckedChanged += new System.EventHandler(this.CustomPresetRadioButton_CheckedChanged);
			// 
			// MagickEightBppColorPresetRadioButton
			// 
			this.MagickEightBppColorPresetRadioButton.AutoSize = true;
			this.MagickEightBppColorPresetRadioButton.Location = new System.Drawing.Point(5, 181);
			this.MagickEightBppColorPresetRadioButton.Name = "MagickEightBppColorPresetRadioButton";
			this.MagickEightBppColorPresetRadioButton.Size = new System.Drawing.Size(153, 17);
			this.MagickEightBppColorPresetRadioButton.TabIndex = 6;
			this.MagickEightBppColorPresetRadioButton.TabStop = true;
			this.MagickEightBppColorPresetRadioButton.Text = "8 bpp (256 colors) (magick)";
			this.MagickEightBppColorPresetRadioButton.UseVisualStyleBackColor = true;
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
			// FourBppColorPresetRadioButton
			// 
			this.FourBppColorPresetRadioButton.AutoSize = true;
			this.FourBppColorPresetRadioButton.Location = new System.Drawing.Point(5, 158);
			this.FourBppColorPresetRadioButton.Name = "FourBppColorPresetRadioButton";
			this.FourBppColorPresetRadioButton.Size = new System.Drawing.Size(104, 17);
			this.FourBppColorPresetRadioButton.TabIndex = 5;
			this.FourBppColorPresetRadioButton.TabStop = true;
			this.FourBppColorPresetRadioButton.Text = "4 bpp (16 colors)";
			this.FourBppColorPresetRadioButton.UseVisualStyleBackColor = true;
			this.FourBppColorPresetRadioButton.CheckedChanged += new System.EventHandler(this.FourBppColorPresetRadioButton_CheckedChanged);
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
			this.OneBppGrayPresetRadioButton.TabStop = true;
			this.OneBppGrayPresetRadioButton.Text = "1 bpp (2 colors)";
			this.OneBppGrayPresetRadioButton.UseVisualStyleBackColor = true;
			this.OneBppGrayPresetRadioButton.CheckedChanged += new System.EventHandler(this.OneBppGrayPresetRadioButton_CheckedChanged);
			// 
			// NoPresetRadioButton
			// 
			this.NoPresetRadioButton.AutoSize = true;
			this.NoPresetRadioButton.Location = new System.Drawing.Point(5, 19);
			this.NoPresetRadioButton.Name = "NoPresetRadioButton";
			this.NoPresetRadioButton.Size = new System.Drawing.Size(72, 17);
			this.NoPresetRadioButton.TabIndex = 0;
			this.NoPresetRadioButton.TabStop = true;
			this.NoPresetRadioButton.Text = "No Preset";
			this.NoPresetRadioButton.UseVisualStyleBackColor = true;
			this.NoPresetRadioButton.CheckedChanged += new System.EventHandler(this.NoPresetRadioButton_CheckedChanged);
			// 
			// EightBppGrayPresetRadioButton
			// 
			this.EightBppGrayPresetRadioButton.AutoSize = true;
			this.EightBppGrayPresetRadioButton.Location = new System.Drawing.Point(5, 122);
			this.EightBppGrayPresetRadioButton.Name = "EightBppGrayPresetRadioButton";
			this.EightBppGrayPresetRadioButton.Size = new System.Drawing.Size(110, 17);
			this.EightBppGrayPresetRadioButton.TabIndex = 4;
			this.EightBppGrayPresetRadioButton.TabStop = true;
			this.EightBppGrayPresetRadioButton.Text = "8 bpp (256 colors)";
			this.EightBppGrayPresetRadioButton.UseVisualStyleBackColor = true;
			this.EightBppGrayPresetRadioButton.CheckedChanged += new System.EventHandler(this.EightBppGrayPresetRadioButton_CheckedChanged);
			// 
			// FourBppGrayPresetRadioButton
			// 
			this.FourBppGrayPresetRadioButton.AutoSize = true;
			this.FourBppGrayPresetRadioButton.Location = new System.Drawing.Point(5, 99);
			this.FourBppGrayPresetRadioButton.Name = "FourBppGrayPresetRadioButton";
			this.FourBppGrayPresetRadioButton.Size = new System.Drawing.Size(104, 17);
			this.FourBppGrayPresetRadioButton.TabIndex = 3;
			this.FourBppGrayPresetRadioButton.TabStop = true;
			this.FourBppGrayPresetRadioButton.Text = "4 bpp (16 colors)";
			this.FourBppGrayPresetRadioButton.UseVisualStyleBackColor = true;
			this.FourBppGrayPresetRadioButton.CheckedChanged += new System.EventHandler(this.FourBppGrayPresetRadioButton_CheckedChanged);
			// 
			// FilesInSourceFolderGroupBox
			// 
			this.FilesInSourceFolderGroupBox.Controls.Add(this.OutputExtensionLabel);
			this.FilesInSourceFolderGroupBox.Controls.Add(this.OutputExtensionTextBox);
			this.FilesInSourceFolderGroupBox.Controls.Add(this.RefreshFileListButton);
			this.FilesInSourceFolderGroupBox.Controls.Add(this.MassFileSelectorButton);
			this.FilesInSourceFolderGroupBox.Controls.Add(this.FilesInSourceFolderListBox);
			this.FilesInSourceFolderGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.FilesInSourceFolderGroupBox.Location = new System.Drawing.Point(511, 101);
			this.FilesInSourceFolderGroupBox.Name = "FilesInSourceFolderGroupBox";
			this.FilesInSourceFolderGroupBox.Size = new System.Drawing.Size(277, 242);
			this.FilesInSourceFolderGroupBox.TabIndex = 6;
			this.FilesInSourceFolderGroupBox.TabStop = false;
			this.FilesInSourceFolderGroupBox.Text = "Files in Source Folder";
			// 
			// OutputExtensionLabel
			// 
			this.OutputExtensionLabel.AutoSize = true;
			this.OutputExtensionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OutputExtensionLabel.Location = new System.Drawing.Point(160, 22);
			this.OutputExtensionLabel.Name = "OutputExtensionLabel";
			this.OutputExtensionLabel.Size = new System.Drawing.Size(71, 13);
			this.OutputExtensionLabel.TabIndex = 10;
			this.OutputExtensionLabel.Text = "Output Ext:";
			// 
			// OutputExtensionTextBox
			// 
			this.OutputExtensionTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.OutputExtensionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OutputExtensionTextBox.Location = new System.Drawing.Point(233, 18);
			this.OutputExtensionTextBox.Name = "OutputExtensionTextBox";
			this.OutputExtensionTextBox.Size = new System.Drawing.Size(34, 20);
			this.OutputExtensionTextBox.TabIndex = 10;
			this.OutputExtensionTextBox.TextChanged += new System.EventHandler(this.OutputExtensionTextBox_TextChanged);
			// 
			// RefreshFileListButton
			// 
			this.RefreshFileListButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.RefreshFileListButton.Location = new System.Drawing.Point(83, 17);
			this.RefreshFileListButton.Name = "RefreshFileListButton";
			this.RefreshFileListButton.Size = new System.Drawing.Size(71, 21);
			this.RefreshFileListButton.TabIndex = 13;
			this.RefreshFileListButton.Text = "Refresh List";
			this.RefreshFileListButton.UseVisualStyleBackColor = true;
			this.RefreshFileListButton.Click += new System.EventHandler(this.RefreshFileListButton_Click);
			// 
			// MassFileSelectorButton
			// 
			this.MassFileSelectorButton.Enabled = false;
			this.MassFileSelectorButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.MassFileSelectorButton.Location = new System.Drawing.Point(6, 17);
			this.MassFileSelectorButton.Name = "MassFileSelectorButton";
			this.MassFileSelectorButton.Size = new System.Drawing.Size(71, 21);
			this.MassFileSelectorButton.TabIndex = 12;
			this.MassFileSelectorButton.Text = "Select All";
			this.MassFileSelectorButton.UseVisualStyleBackColor = true;
			this.MassFileSelectorButton.Click += new System.EventHandler(this.MassFileSelectorButton_Click);
			// 
			// FilesInSourceFolderListBox
			// 
			this.FilesInSourceFolderListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.FilesInSourceFolderListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FilesInSourceFolderListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.FilesInSourceFolderListBox.FormattingEnabled = true;
			this.FilesInSourceFolderListBox.HorizontalScrollbar = true;
			this.FilesInSourceFolderListBox.Location = new System.Drawing.Point(0, 45);
			this.FilesInSourceFolderListBox.Name = "FilesInSourceFolderListBox";
			this.FilesInSourceFolderListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.FilesInSourceFolderListBox.Size = new System.Drawing.Size(277, 197);
			this.FilesInSourceFolderListBox.TabIndex = 0;
			this.FilesInSourceFolderListBox.TabStop = false;
			this.FilesInSourceFolderListBox.SelectedIndexChanged += new System.EventHandler(this.FilesInSourceFolderListBox_SelectedIndexChanged);
			// 
			// PingoCommandLabel
			// 
			this.PingoCommandLabel.AutoSize = true;
			this.PingoCommandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PingoCommandLabel.Location = new System.Drawing.Point(13, 397);
			this.PingoCommandLabel.Name = "PingoCommandLabel";
			this.PingoCommandLabel.Size = new System.Drawing.Size(144, 20);
			this.PingoCommandLabel.TabIndex = 10;
			this.PingoCommandLabel.Text = "Pingo Command:";
			// 
			// PingoCommandTextBox
			// 
			this.PingoCommandTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.PingoCommandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PingoCommandTextBox.Location = new System.Drawing.Point(12, 420);
			this.PingoCommandTextBox.Name = "PingoCommandTextBox";
			this.PingoCommandTextBox.Size = new System.Drawing.Size(776, 20);
			this.PingoCommandTextBox.TabIndex = 11;
			this.PingoCommandTextBox.TextChanged += new System.EventHandler(this.PingoCommandTextBox_TextChanged);
			// 
			// ProcessingCancelButton
			// 
			this.ProcessingCancelButton.Enabled = false;
			this.ProcessingCancelButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ProcessingCancelButton.Location = new System.Drawing.Point(713, 446);
			this.ProcessingCancelButton.Name = "ProcessingCancelButton";
			this.ProcessingCancelButton.Size = new System.Drawing.Size(75, 23);
			this.ProcessingCancelButton.TabIndex = 12;
			this.ProcessingCancelButton.Text = "Cancel";
			this.ProcessingCancelButton.UseVisualStyleBackColor = true;
			this.ProcessingCancelButton.Visible = false;
			this.ProcessingCancelButton.Click += new System.EventHandler(this.ProcessingCancelButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.ClientSize = new System.Drawing.Size(800, 481);
			this.Controls.Add(this.ProcessingCancelButton);
			this.Controls.Add(this.PingoCommandLabel);
			this.Controls.Add(this.PingoCommandTextBox);
			this.Controls.Add(this.FilesInSourceFolderGroupBox);
			this.Controls.Add(this.ImagemagickSettingsGroupBox);
			this.Controls.Add(this.PresetSettingsGroupBox);
			this.Controls.Add(this.PingoSettingsGroupBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SourceFolderPathLabel);
			this.Controls.Add(this.OutputFolderPathTextBox);
			this.Controls.Add(this.OutputFolderPathButton);
			this.Controls.Add(this.StatusMessageLabel);
			this.Controls.Add(this.ApplyButton);
			this.Controls.Add(this.SourceFolderPathTextBox);
			this.Controls.Add(this.SourceFolderPathButton);
			this.Controls.Add(this.MagickCommandLabel);
			this.Controls.Add(this.MagickCommandTextBox);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "imgdanke";
			this.ImagemagickSettingsGroupBox.ResumeLayout(false);
			this.ImagemagickSettingsGroupBox.PerformLayout();
			this.PingoSettingsGroupBox.ResumeLayout(false);
			this.PingoSettingsGroupBox.PerformLayout();
			this.PresetSettingsGroupBox.ResumeLayout(false);
			this.PresetSettingsGroupBox.PerformLayout();
			this.FilesInSourceFolderGroupBox.ResumeLayout(false);
			this.FilesInSourceFolderGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox MagickCommandTextBox;
		private System.Windows.Forms.Label MagickCommandLabel;
		private System.Windows.Forms.Button SourceFolderPathButton;
		private System.Windows.Forms.TextBox SourceFolderPathTextBox;
		private System.Windows.Forms.Button ApplyButton;
		private System.Windows.Forms.Label StatusMessageLabel;
		private System.Windows.Forms.TextBox OutputFolderPathTextBox;
		private System.Windows.Forms.Button OutputFolderPathButton;
		private System.Windows.Forms.Label SourceFolderPathLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox ImagemagickSettingsGroupBox;
		private System.Windows.Forms.GroupBox PingoSettingsGroupBox;
		private System.Windows.Forms.GroupBox PresetSettingsGroupBox;
		private System.Windows.Forms.RadioButton NoPresetRadioButton;
		private System.Windows.Forms.RadioButton MagickEightBppColorPresetRadioButton;
		private System.Windows.Forms.RadioButton FourBppColorPresetRadioButton;
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
		private System.Windows.Forms.RadioButton PingoSARadioButton;
		private System.Windows.Forms.RadioButton PingoSBRadioButton;
		private System.Windows.Forms.RadioButton PingoEightBppColorPresetRadioButton;
		private System.Windows.Forms.Button MassFileSelectorButton;
		private System.Windows.Forms.Button RefreshFileListButton;
		private System.Windows.Forms.Label OutputExtensionLabel;
		private System.Windows.Forms.TextBox OutputExtensionTextBox;
		private System.Windows.Forms.Button ProcessingCancelButton;
		private System.Windows.Forms.ComboBox PingoPNGPaletteComboBox;
	}
}

