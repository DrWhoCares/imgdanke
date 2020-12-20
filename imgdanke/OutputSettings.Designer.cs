
namespace imgdanke
{
	partial class OutputSettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputSettingsForm));
			this.OutputToNewFolderFolderNameTextBox = new System.Windows.Forms.TextBox();
			this.OutputToNewFolderCheckBox = new System.Windows.Forms.CheckBox();
			this.OutputToNewFolderNameLabel = new System.Windows.Forms.Label();
			this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.FilenamesAndOutputFolderSplitContainer = new System.Windows.Forms.SplitContainer();
			this.TagsToFilenameGroupBox = new System.Windows.Forms.GroupBox();
			this.FilenamesPingoSettingsCheckBox = new System.Windows.Forms.CheckBox();
			this.FilenamesMagickSettingsCheckBox = new System.Windows.Forms.CheckBox();
			this.FilenamesPresetCheckBox = new System.Windows.Forms.CheckBox();
			this.AddTagsToFilenamesCheckBox = new System.Windows.Forms.CheckBox();
			this.TagsToOutputFolderGroupBox = new System.Windows.Forms.GroupBox();
			this.OutputFolderPingoSettingsCheckBox = new System.Windows.Forms.CheckBox();
			this.OutputFolderMagickSettingsCheckBox = new System.Windows.Forms.CheckBox();
			this.OutputFolderPresetCheckBox = new System.Windows.Forms.CheckBox();
			this.AddTagsToOutputFolderCheckBox = new System.Windows.Forms.CheckBox();
			this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
			this.MainSplitContainer.Panel1.SuspendLayout();
			this.MainSplitContainer.Panel2.SuspendLayout();
			this.MainSplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FilenamesAndOutputFolderSplitContainer)).BeginInit();
			this.FilenamesAndOutputFolderSplitContainer.Panel1.SuspendLayout();
			this.FilenamesAndOutputFolderSplitContainer.Panel2.SuspendLayout();
			this.FilenamesAndOutputFolderSplitContainer.SuspendLayout();
			this.TagsToFilenameGroupBox.SuspendLayout();
			this.TagsToOutputFolderGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// OutputToNewFolderFolderNameTextBox
			// 
			this.OutputToNewFolderFolderNameTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.OutputToNewFolderFolderNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OutputToNewFolderFolderNameTextBox.Enabled = false;
			this.OutputToNewFolderFolderNameTextBox.Location = new System.Drawing.Point(237, 9);
			this.OutputToNewFolderFolderNameTextBox.Name = "OutputToNewFolderFolderNameTextBox";
			this.OutputToNewFolderFolderNameTextBox.Size = new System.Drawing.Size(252, 20);
			this.OutputToNewFolderFolderNameTextBox.TabIndex = 17;
			this.MainToolTip.SetToolTip(this.OutputToNewFolderFolderNameTextBox, "The name of the new folder to be created if Output To New Folder is checked. Tags" +
        " will be appened to this if also checked.");
			this.OutputToNewFolderFolderNameTextBox.TextChanged += new System.EventHandler(this.OutputToNewFolderFolderNameTextBox_TextChanged);
			this.OutputToNewFolderFolderNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OutputToNewFolderFolderNameTextBox_KeyPress);
			// 
			// OutputToNewFolderCheckBox
			// 
			this.OutputToNewFolderCheckBox.AutoSize = true;
			this.OutputToNewFolderCheckBox.Location = new System.Drawing.Point(12, 12);
			this.OutputToNewFolderCheckBox.Name = "OutputToNewFolderCheckBox";
			this.OutputToNewFolderCheckBox.Size = new System.Drawing.Size(131, 17);
			this.OutputToNewFolderCheckBox.TabIndex = 18;
			this.OutputToNewFolderCheckBox.Text = "Output To New Folder";
			this.MainToolTip.SetToolTip(this.OutputToNewFolderCheckBox, "Create a new folder at Output Folder Path with an optional base name, and/or opti" +
        "onal tags");
			this.OutputToNewFolderCheckBox.UseVisualStyleBackColor = true;
			this.OutputToNewFolderCheckBox.CheckedChanged += new System.EventHandler(this.OutputToNewFolderCheckBox_CheckedChanged);
			// 
			// OutputToNewFolderNameLabel
			// 
			this.OutputToNewFolderNameLabel.AutoSize = true;
			this.OutputToNewFolderNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OutputToNewFolderNameLabel.Location = new System.Drawing.Point(149, 13);
			this.OutputToNewFolderNameLabel.Name = "OutputToNewFolderNameLabel";
			this.OutputToNewFolderNameLabel.Size = new System.Drawing.Size(82, 13);
			this.OutputToNewFolderNameLabel.TabIndex = 19;
			this.OutputToNewFolderNameLabel.Text = "Folder Name:";
			// 
			// MainSplitContainer
			// 
			this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.MainSplitContainer.IsSplitterFixed = true;
			this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.MainSplitContainer.Name = "MainSplitContainer";
			this.MainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// MainSplitContainer.Panel1
			// 
			this.MainSplitContainer.Panel1.Controls.Add(this.OutputToNewFolderCheckBox);
			this.MainSplitContainer.Panel1.Controls.Add(this.OutputToNewFolderNameLabel);
			this.MainSplitContainer.Panel1.Controls.Add(this.OutputToNewFolderFolderNameTextBox);
			// 
			// MainSplitContainer.Panel2
			// 
			this.MainSplitContainer.Panel2.Controls.Add(this.FilenamesAndOutputFolderSplitContainer);
			this.MainSplitContainer.Size = new System.Drawing.Size(501, 152);
			this.MainSplitContainer.SplitterDistance = 31;
			this.MainSplitContainer.TabIndex = 20;
			// 
			// FilenamesAndOutputFolderSplitContainer
			// 
			this.FilenamesAndOutputFolderSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FilenamesAndOutputFolderSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.FilenamesAndOutputFolderSplitContainer.Name = "FilenamesAndOutputFolderSplitContainer";
			// 
			// FilenamesAndOutputFolderSplitContainer.Panel1
			// 
			this.FilenamesAndOutputFolderSplitContainer.Panel1.Controls.Add(this.TagsToFilenameGroupBox);
			// 
			// FilenamesAndOutputFolderSplitContainer.Panel2
			// 
			this.FilenamesAndOutputFolderSplitContainer.Panel2.Controls.Add(this.TagsToOutputFolderGroupBox);
			this.FilenamesAndOutputFolderSplitContainer.Size = new System.Drawing.Size(501, 117);
			this.FilenamesAndOutputFolderSplitContainer.SplitterDistance = 225;
			this.FilenamesAndOutputFolderSplitContainer.TabIndex = 8;
			// 
			// TagsToFilenameGroupBox
			// 
			this.TagsToFilenameGroupBox.AutoSize = true;
			this.TagsToFilenameGroupBox.Controls.Add(this.FilenamesPingoSettingsCheckBox);
			this.TagsToFilenameGroupBox.Controls.Add(this.FilenamesMagickSettingsCheckBox);
			this.TagsToFilenameGroupBox.Controls.Add(this.FilenamesPresetCheckBox);
			this.TagsToFilenameGroupBox.Controls.Add(this.AddTagsToFilenamesCheckBox);
			this.TagsToFilenameGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TagsToFilenameGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.TagsToFilenameGroupBox.Location = new System.Drawing.Point(0, 0);
			this.TagsToFilenameGroupBox.Name = "TagsToFilenameGroupBox";
			this.TagsToFilenameGroupBox.Size = new System.Drawing.Size(225, 117);
			this.TagsToFilenameGroupBox.TabIndex = 8;
			this.TagsToFilenameGroupBox.TabStop = false;
			this.TagsToFilenameGroupBox.Text = "Tags To Append To Filename(s)";
			// 
			// FilenamesPingoSettingsCheckBox
			// 
			this.FilenamesPingoSettingsCheckBox.AutoSize = true;
			this.FilenamesPingoSettingsCheckBox.Location = new System.Drawing.Point(26, 88);
			this.FilenamesPingoSettingsCheckBox.Name = "FilenamesPingoSettingsCheckBox";
			this.FilenamesPingoSettingsCheckBox.Size = new System.Drawing.Size(131, 17);
			this.FilenamesPingoSettingsCheckBox.TabIndex = 3;
			this.FilenamesPingoSettingsCheckBox.Text = "Include pingo Settings";
			this.MainToolTip.SetToolTip(this.FilenamesPingoSettingsCheckBox, "Appends the pingo settings, for example:\r\n\"(pngpal24)\"\r\n\"(-sb)\"\r\n\"(-s9)\"\r\n\"(-stri" +
        "p)\"");
			this.FilenamesPingoSettingsCheckBox.UseVisualStyleBackColor = true;
			this.FilenamesPingoSettingsCheckBox.CheckedChanged += new System.EventHandler(this.FilenamesPingoSettingsCheckBox_CheckedChanged);
			// 
			// FilenamesMagickSettingsCheckBox
			// 
			this.FilenamesMagickSettingsCheckBox.AutoSize = true;
			this.FilenamesMagickSettingsCheckBox.Location = new System.Drawing.Point(26, 65);
			this.FilenamesMagickSettingsCheckBox.Name = "FilenamesMagickSettingsCheckBox";
			this.FilenamesMagickSettingsCheckBox.Size = new System.Drawing.Size(139, 17);
			this.FilenamesMagickSettingsCheckBox.TabIndex = 2;
			this.FilenamesMagickSettingsCheckBox.Text = "Include magick Settings";
			this.MainToolTip.SetToolTip(this.FilenamesMagickSettingsCheckBox, "Appends the Magick settings, for example:\r\n\"(-dither None)\"\r\n\"(-colorspace Gray)\"" +
        "\r\n\"(pos16)\"\r\n\"(norm)\"");
			this.FilenamesMagickSettingsCheckBox.UseVisualStyleBackColor = true;
			this.FilenamesMagickSettingsCheckBox.CheckedChanged += new System.EventHandler(this.FilenamesMagickSettingsCheckBox_CheckedChanged);
			// 
			// FilenamesPresetCheckBox
			// 
			this.FilenamesPresetCheckBox.AutoSize = true;
			this.FilenamesPresetCheckBox.Location = new System.Drawing.Point(26, 42);
			this.FilenamesPresetCheckBox.Name = "FilenamesPresetCheckBox";
			this.FilenamesPresetCheckBox.Size = new System.Drawing.Size(94, 17);
			this.FilenamesPresetCheckBox.TabIndex = 1;
			this.FilenamesPresetCheckBox.Text = "Include Preset";
			this.MainToolTip.SetToolTip(this.FilenamesPresetCheckBox, "Appends the Preset name, for example:\r\n\"(NoPreset)\"\r\n\"(Gray8bpp)\"\r\n\"(PingoColor4b" +
        "pp)\"");
			this.FilenamesPresetCheckBox.UseVisualStyleBackColor = true;
			this.FilenamesPresetCheckBox.CheckedChanged += new System.EventHandler(this.FilenamesPresetCheckBox_CheckedChanged);
			// 
			// AddTagsToFilenamesCheckBox
			// 
			this.AddTagsToFilenamesCheckBox.AutoSize = true;
			this.AddTagsToFilenamesCheckBox.Location = new System.Drawing.Point(12, 19);
			this.AddTagsToFilenamesCheckBox.Name = "AddTagsToFilenamesCheckBox";
			this.AddTagsToFilenamesCheckBox.Size = new System.Drawing.Size(180, 17);
			this.AddTagsToFilenamesCheckBox.TabIndex = 0;
			this.AddTagsToFilenamesCheckBox.Text = "Should Add Tags To Filename(s)";
			this.MainToolTip.SetToolTip(this.AddTagsToFilenamesCheckBox, "Toggles whether or not the images should have tags appended to their filename,\r\na" +
        "fter the Append option on main interface");
			this.AddTagsToFilenamesCheckBox.UseVisualStyleBackColor = true;
			this.AddTagsToFilenamesCheckBox.CheckedChanged += new System.EventHandler(this.AddTagsToFilenamesCheckBox_CheckedChanged);
			// 
			// TagsToOutputFolderGroupBox
			// 
			this.TagsToOutputFolderGroupBox.AutoSize = true;
			this.TagsToOutputFolderGroupBox.Controls.Add(this.OutputFolderPingoSettingsCheckBox);
			this.TagsToOutputFolderGroupBox.Controls.Add(this.OutputFolderMagickSettingsCheckBox);
			this.TagsToOutputFolderGroupBox.Controls.Add(this.OutputFolderPresetCheckBox);
			this.TagsToOutputFolderGroupBox.Controls.Add(this.AddTagsToOutputFolderCheckBox);
			this.TagsToOutputFolderGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TagsToOutputFolderGroupBox.Enabled = false;
			this.TagsToOutputFolderGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.TagsToOutputFolderGroupBox.Location = new System.Drawing.Point(0, 0);
			this.TagsToOutputFolderGroupBox.Name = "TagsToOutputFolderGroupBox";
			this.TagsToOutputFolderGroupBox.Size = new System.Drawing.Size(272, 117);
			this.TagsToOutputFolderGroupBox.TabIndex = 9;
			this.TagsToOutputFolderGroupBox.TabStop = false;
			this.TagsToOutputFolderGroupBox.Text = "Tags To Append To New Output Folder";
			// 
			// OutputFolderPingoSettingsCheckBox
			// 
			this.OutputFolderPingoSettingsCheckBox.AutoSize = true;
			this.OutputFolderPingoSettingsCheckBox.Location = new System.Drawing.Point(26, 88);
			this.OutputFolderPingoSettingsCheckBox.Name = "OutputFolderPingoSettingsCheckBox";
			this.OutputFolderPingoSettingsCheckBox.Size = new System.Drawing.Size(131, 17);
			this.OutputFolderPingoSettingsCheckBox.TabIndex = 6;
			this.OutputFolderPingoSettingsCheckBox.Text = "Include pingo Settings";
			this.MainToolTip.SetToolTip(this.OutputFolderPingoSettingsCheckBox, "Appends the pingo settings, for example:\r\n\"(pngpal24)\"\r\n\"(-sb)\"\r\n\"(-s9)\"\r\n\"(-stri" +
        "p)\"");
			this.OutputFolderPingoSettingsCheckBox.UseVisualStyleBackColor = true;
			this.OutputFolderPingoSettingsCheckBox.CheckedChanged += new System.EventHandler(this.OutputFolderPingoSettingsCheckBox_CheckedChanged);
			// 
			// OutputFolderMagickSettingsCheckBox
			// 
			this.OutputFolderMagickSettingsCheckBox.AutoSize = true;
			this.OutputFolderMagickSettingsCheckBox.Location = new System.Drawing.Point(26, 65);
			this.OutputFolderMagickSettingsCheckBox.Name = "OutputFolderMagickSettingsCheckBox";
			this.OutputFolderMagickSettingsCheckBox.Size = new System.Drawing.Size(139, 17);
			this.OutputFolderMagickSettingsCheckBox.TabIndex = 5;
			this.OutputFolderMagickSettingsCheckBox.Text = "Include magick Settings";
			this.MainToolTip.SetToolTip(this.OutputFolderMagickSettingsCheckBox, "Appends the Magick settings, for example:\r\n\"(-dither None)\"\r\n\"(-colorspace Gray)\"" +
        "\r\n\"(pos16)\"\r\n\"(norm)\"");
			this.OutputFolderMagickSettingsCheckBox.UseVisualStyleBackColor = true;
			this.OutputFolderMagickSettingsCheckBox.CheckedChanged += new System.EventHandler(this.OutputFolderMagickSettingsCheckBox_CheckedChanged);
			// 
			// OutputFolderPresetCheckBox
			// 
			this.OutputFolderPresetCheckBox.AutoSize = true;
			this.OutputFolderPresetCheckBox.Location = new System.Drawing.Point(26, 42);
			this.OutputFolderPresetCheckBox.Name = "OutputFolderPresetCheckBox";
			this.OutputFolderPresetCheckBox.Size = new System.Drawing.Size(94, 17);
			this.OutputFolderPresetCheckBox.TabIndex = 4;
			this.OutputFolderPresetCheckBox.Text = "Include Preset";
			this.MainToolTip.SetToolTip(this.OutputFolderPresetCheckBox, "Appends the Preset name, for example:\r\n\"(NoPreset)\"\r\n\"(Gray8bpp)\"\r\n\"(PingoColor4b" +
        "pp)\"");
			this.OutputFolderPresetCheckBox.UseVisualStyleBackColor = true;
			this.OutputFolderPresetCheckBox.CheckedChanged += new System.EventHandler(this.OutputFolderPresetCheckBox_CheckedChanged);
			// 
			// AddTagsToOutputFolderCheckBox
			// 
			this.AddTagsToOutputFolderCheckBox.AutoSize = true;
			this.AddTagsToOutputFolderCheckBox.Location = new System.Drawing.Point(12, 19);
			this.AddTagsToOutputFolderCheckBox.Name = "AddTagsToOutputFolderCheckBox";
			this.AddTagsToOutputFolderCheckBox.Size = new System.Drawing.Size(216, 17);
			this.AddTagsToOutputFolderCheckBox.TabIndex = 0;
			this.AddTagsToOutputFolderCheckBox.Text = "Should Add Tags To New Output Folder";
			this.MainToolTip.SetToolTip(this.AddTagsToOutputFolderCheckBox, "Toggles whether or not the folder added by Output To New Folder\r\nshould have tags" +
        " appended to its name");
			this.AddTagsToOutputFolderCheckBox.UseVisualStyleBackColor = true;
			this.AddTagsToOutputFolderCheckBox.CheckedChanged += new System.EventHandler(this.AddTagsToOutputFolderCheckBox_CheckedChanged);
			// 
			// MainToolTip
			// 
			this.MainToolTip.BackColor = System.Drawing.SystemColors.GrayText;
			this.MainToolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			// 
			// OutputSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
			this.ClientSize = new System.Drawing.Size(501, 152);
			this.Controls.Add(this.MainSplitContainer);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OutputSettingsForm";
			this.Text = "imgdanke - Output Settings";
			this.VisibleChanged += new System.EventHandler(this.OutputSettingsForm_VisibleChanged);
			this.MainSplitContainer.Panel1.ResumeLayout(false);
			this.MainSplitContainer.Panel1.PerformLayout();
			this.MainSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
			this.MainSplitContainer.ResumeLayout(false);
			this.FilenamesAndOutputFolderSplitContainer.Panel1.ResumeLayout(false);
			this.FilenamesAndOutputFolderSplitContainer.Panel1.PerformLayout();
			this.FilenamesAndOutputFolderSplitContainer.Panel2.ResumeLayout(false);
			this.FilenamesAndOutputFolderSplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.FilenamesAndOutputFolderSplitContainer)).EndInit();
			this.FilenamesAndOutputFolderSplitContainer.ResumeLayout(false);
			this.TagsToFilenameGroupBox.ResumeLayout(false);
			this.TagsToFilenameGroupBox.PerformLayout();
			this.TagsToOutputFolderGroupBox.ResumeLayout(false);
			this.TagsToOutputFolderGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox OutputToNewFolderFolderNameTextBox;
		private System.Windows.Forms.CheckBox OutputToNewFolderCheckBox;
		private System.Windows.Forms.Label OutputToNewFolderNameLabel;
		private System.Windows.Forms.SplitContainer MainSplitContainer;
		private System.Windows.Forms.SplitContainer FilenamesAndOutputFolderSplitContainer;
		private System.Windows.Forms.GroupBox TagsToFilenameGroupBox;
		private System.Windows.Forms.CheckBox AddTagsToFilenamesCheckBox;
		private System.Windows.Forms.GroupBox TagsToOutputFolderGroupBox;
		private System.Windows.Forms.CheckBox AddTagsToOutputFolderCheckBox;
		private System.Windows.Forms.CheckBox FilenamesPresetCheckBox;
		private System.Windows.Forms.CheckBox FilenamesMagickSettingsCheckBox;
		private System.Windows.Forms.CheckBox FilenamesPingoSettingsCheckBox;
		private System.Windows.Forms.CheckBox OutputFolderPingoSettingsCheckBox;
		private System.Windows.Forms.CheckBox OutputFolderMagickSettingsCheckBox;
		private System.Windows.Forms.CheckBox OutputFolderPresetCheckBox;
		private System.Windows.Forms.ToolTip MainToolTip;
	}
}