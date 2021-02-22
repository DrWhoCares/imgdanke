
namespace imgdanke
{
	partial class EditListItemsDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditListItemsDialog));
			this.ListItemsListBox = new System.Windows.Forms.ListBox();
			this.ListItemsAndFormControlsSplitContainer = new System.Windows.Forms.SplitContainer();
			this.EditItemTextBox = new System.Windows.Forms.TextBox();
			this.AddRowButton = new System.Windows.Forms.Button();
			this.SaveButton = new System.Windows.Forms.Button();
			this.CancelEditingListItemsButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.ListItemsAndFormControlsSplitContainer)).BeginInit();
			this.ListItemsAndFormControlsSplitContainer.Panel1.SuspendLayout();
			this.ListItemsAndFormControlsSplitContainer.Panel2.SuspendLayout();
			this.ListItemsAndFormControlsSplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// ListItemsListBox
			// 
			this.ListItemsListBox.AllowDrop = true;
			this.ListItemsListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
			this.ListItemsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ListItemsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListItemsListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.ListItemsListBox.FormattingEnabled = true;
			this.ListItemsListBox.HorizontalScrollbar = true;
			this.ListItemsListBox.Location = new System.Drawing.Point(0, 0);
			this.ListItemsListBox.Name = "ListItemsListBox";
			this.ListItemsListBox.Size = new System.Drawing.Size(270, 119);
			this.ListItemsListBox.TabIndex = 5;
			this.ListItemsListBox.TabStop = false;
			this.ListItemsListBox.DoubleClick += new System.EventHandler(this.ListItemsListBox_DoubleClick);
			this.ListItemsListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListItemsListBox_KeyPress);
			// 
			// ListItemsAndFormControlsSplitContainer
			// 
			this.ListItemsAndFormControlsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListItemsAndFormControlsSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.ListItemsAndFormControlsSplitContainer.IsSplitterFixed = true;
			this.ListItemsAndFormControlsSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.ListItemsAndFormControlsSplitContainer.Name = "ListItemsAndFormControlsSplitContainer";
			this.ListItemsAndFormControlsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// ListItemsAndFormControlsSplitContainer.Panel1
			// 
			this.ListItemsAndFormControlsSplitContainer.Panel1.Controls.Add(this.EditItemTextBox);
			this.ListItemsAndFormControlsSplitContainer.Panel1.Controls.Add(this.ListItemsListBox);
			this.ListItemsAndFormControlsSplitContainer.Panel1MinSize = 119;
			// 
			// ListItemsAndFormControlsSplitContainer.Panel2
			// 
			this.ListItemsAndFormControlsSplitContainer.Panel2.Controls.Add(this.AddRowButton);
			this.ListItemsAndFormControlsSplitContainer.Panel2.Controls.Add(this.SaveButton);
			this.ListItemsAndFormControlsSplitContainer.Panel2.Controls.Add(this.CancelEditingListItemsButton);
			this.ListItemsAndFormControlsSplitContainer.Size = new System.Drawing.Size(270, 167);
			this.ListItemsAndFormControlsSplitContainer.SplitterDistance = 119;
			this.ListItemsAndFormControlsSplitContainer.TabIndex = 30;
			this.ListItemsAndFormControlsSplitContainer.TabStop = false;
			// 
			// EditItemTextBox
			// 
			this.EditItemTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.EditItemTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.EditItemTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.EditItemTextBox.Location = new System.Drawing.Point(3, 3);
			this.EditItemTextBox.Name = "EditItemTextBox";
			this.EditItemTextBox.Size = new System.Drawing.Size(34, 20);
			this.EditItemTextBox.TabIndex = 11;
			this.EditItemTextBox.TabStop = false;
			this.EditItemTextBox.Visible = false;
			this.EditItemTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditItemTextBox_KeyPress);
			this.EditItemTextBox.Leave += new System.EventHandler(this.EditItemTextBox_Leave);
			// 
			// AddRowButton
			// 
			this.AddRowButton.AutoSize = true;
			this.AddRowButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.AddRowButton.Location = new System.Drawing.Point(12, 11);
			this.AddRowButton.Name = "AddRowButton";
			this.AddRowButton.Size = new System.Drawing.Size(57, 23);
			this.AddRowButton.TabIndex = 1;
			this.AddRowButton.Text = "Add";
			this.AddRowButton.UseMnemonic = false;
			this.AddRowButton.UseVisualStyleBackColor = true;
			this.AddRowButton.Click += new System.EventHandler(this.AddRowButton_Click);
			// 
			// SaveButton
			// 
			this.SaveButton.AutoSize = true;
			this.SaveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.SaveButton.Location = new System.Drawing.Point(138, 11);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(57, 23);
			this.SaveButton.TabIndex = 3;
			this.SaveButton.Text = "Save";
			this.SaveButton.UseMnemonic = false;
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// CancelEditingListItemsButton
			// 
			this.CancelEditingListItemsButton.AutoSize = true;
			this.CancelEditingListItemsButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.CancelEditingListItemsButton.Location = new System.Drawing.Point(201, 11);
			this.CancelEditingListItemsButton.Name = "CancelEditingListItemsButton";
			this.CancelEditingListItemsButton.Size = new System.Drawing.Size(57, 23);
			this.CancelEditingListItemsButton.TabIndex = 4;
			this.CancelEditingListItemsButton.Text = "Cancel";
			this.CancelEditingListItemsButton.UseMnemonic = false;
			this.CancelEditingListItemsButton.UseVisualStyleBackColor = true;
			this.CancelEditingListItemsButton.Click += new System.EventHandler(this.CancelEditingListItemsButton_Click);
			// 
			// EditListItemsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
			this.ClientSize = new System.Drawing.Size(270, 167);
			this.Controls.Add(this.ListItemsAndFormControlsSplitContainer);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EditListItemsDialog";
			this.Text = "EditListItemsDialog";
			this.Shown += new System.EventHandler(this.EditListItemsDialog_Shown);
			this.ListItemsAndFormControlsSplitContainer.Panel1.ResumeLayout(false);
			this.ListItemsAndFormControlsSplitContainer.Panel1.PerformLayout();
			this.ListItemsAndFormControlsSplitContainer.Panel2.ResumeLayout(false);
			this.ListItemsAndFormControlsSplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ListItemsAndFormControlsSplitContainer)).EndInit();
			this.ListItemsAndFormControlsSplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox ListItemsListBox;
		private System.Windows.Forms.SplitContainer ListItemsAndFormControlsSplitContainer;
		private System.Windows.Forms.Button AddRowButton;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Button CancelEditingListItemsButton;
		private System.Windows.Forms.TextBox EditItemTextBox;
	}
}