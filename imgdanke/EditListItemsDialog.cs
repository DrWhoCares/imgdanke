using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace imgdanke
{

	internal partial class EditListItemsDialog : Form
	{
		private int LastSelectedItemIndex;
		private readonly Point OriginalLocation;
		private static readonly Regex INVALID_FILENAME_CHARS_REGEX = new Regex("[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		internal readonly List<string> ListItems;

		public sealed override string Text
		{
			get => base.Text;
			set => base.Text = value;
		}

		internal EditListItemsDialog(List<string> listItems, Point mainWindowLocation)
		{
			InitializeComponent();
			Text = "imgdanke - v" + typeof(MainForm).Assembly.GetName().Version + " - Output Settings";
			OriginalLocation = mainWindowLocation;
			ListItems = new List<string>();
			InitializeTextBox(listItems);
			ListItemsListBox.Controls.Add(EditItemTextBox);
		}

		private void InitializeTextBox(List<string> listItems)
		{
			ListItemsListBox.BeginUpdate();
			ListItemsListBox.Items.AddRange(listItems.ToArray());
			ListItemsListBox.Focus();
			ListItemsListBox.Select();
			ListItemsListBox.SelectedIndex = 0;
			ListItemsListBox.EndUpdate();
		}

		private void EditListItemsDialog_Shown(object sender, System.EventArgs e)
		{
			Location = OriginalLocation;
		}

		private void ListItemsListBox_DoubleClick(object sender, System.EventArgs e)
		{
			EditItemInListBox();
		}

		private void ListItemsListBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == 13 ) // Enter key
			{
				EditItemInListBox();
			}
		}

		private void EditItemInListBox()
		{
			LastSelectedItemIndex = ListItemsListBox.SelectedIndex;
			EditItemTextBox.Text = ListItemsListBox.Items[LastSelectedItemIndex].ToString();
			Rectangle rect = ListItemsListBox.GetItemRectangle(LastSelectedItemIndex);
			EditItemTextBox.Location = new Point(rect.X, rect.Y);
			EditItemTextBox.Size = new Size(rect.Width, rect.Height);
			EditItemTextBox.Show();
			EditItemTextBox.Focus();
			EditItemTextBox.SelectAll();
		}

		private void EditItemTextBox_Leave(object sender, System.EventArgs e)
		{
			EndEditingItemInListBox();
		}

		private void EditItemTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ( !char.IsControl(e.KeyChar) && INVALID_FILENAME_CHARS_REGEX.IsMatch(e.KeyChar.ToString()) )
			{
				e.Handled = true;
			}
			else if ( e.KeyChar == 13 ) // Enter key
			{
				EndEditingItemInListBox();
				e.Handled = true;
			}
		}

		private void EndEditingItemInListBox()
		{
			if ( string.IsNullOrWhiteSpace(EditItemTextBox.Text) )
			{
				ListItemsListBox.Items.RemoveAt(LastSelectedItemIndex);
			}
			else
			{
				if ( EditItemTextBox.Text[0] != '.' )
				{
					EditItemTextBox.Text = EditItemTextBox.Text.Insert(0, ".");
				}

				ListItemsListBox.Items[LastSelectedItemIndex] = EditItemTextBox.Text;
			}

			EditItemTextBox.Hide();
		}

		private void AddRowButton_Click(object sender, System.EventArgs e)
		{
			ListItemsListBox.Items.Add("");
			ListItemsListBox.Focus();
			ListItemsListBox.Select();
			ListItemsListBox.ClearSelected();
			ListItemsListBox.SelectedIndex = ListItemsListBox.Items.Count - 1;
		}

		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			foreach ( string item in ListItemsListBox.Items )
			{
				if ( string.IsNullOrWhiteSpace(item) || item[0] != '.' )
				{
					continue;
				}

				ListItems.Add(item);
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void CancelEditingListItemsButton_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

	}
}
