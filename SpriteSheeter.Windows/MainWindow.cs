using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteSheeter
{
	public partial class MainWindow : Form
	{
		static readonly Pen SelectedItemBlackPen = new Pen (Color.Red, 2);
		static readonly Font TextFont = new Font ("Gulim", 8);
		static readonly Brush TextBrush = new SolidBrush (Color.Black);
		static readonly Brush TransparentWhiteBrush = new SolidBrush (Color.White);
		static readonly Brush TransparentGrayBrush = new SolidBrush (Color.LightGray);

		SpriteSheet spriteSheet;

		public MainWindow ()
		{
			InitializeComponent ();

			spriteSheet = new SpriteSheet ();
			spriteSheet.IsDelayedRefresh = true;

			ComboBoxMaximumSize.Text = spriteSheet.MaximumSize.ToString ();
			ListViewSprites.SmallImageList = new ImageList ();
		}

		private void AddSprite (string filename)
		{
			string name = spriteSheet.AddSprite (filename);
			var item = ListViewSprites.Items.Add (name);
			ListViewSprites.SmallImageList.Images.Add (name, ImageHelper.Convert (spriteSheet [name].Sprite));
			item.ImageKey = name;
		}

		private void MenuItemNewSpriteSheet_Click (object sender, EventArgs e)
		{

		}

		private void MenuItemOpen_Click (object sender, EventArgs e)
		{

		}

		private void MenuItemSave_Click (object sender, EventArgs e)
		{

		}

		private void MenuItemSaveAs_Click (object sender, EventArgs e)
		{

		}

		private void MenuItemExport_Click (object sender, EventArgs e)
		{

		}

		private void MenuItemExit_Click (object sender, EventArgs e)
		{
			Close ();
		}

		private void MenuItemSpriteAdd_Click (object sender, EventArgs e)
		{
			OpenFileDialogSprite.Multiselect = true;
			if(OpenFileDialogSprite.ShowDialog (this) == DialogResult.Cancel)
				return;

			foreach (var filename in OpenFileDialogSprite.FileNames)
				AddSprite (filename);

			spriteSheet.Refresh ();
			var spriteSheetImage = spriteSheet.GenerateSpriteSheet ();
			PictureBoxPreview.Image = ImageHelper.Convert (spriteSheetImage);
		}

		private void MenuItemSpriteRemove_Click (object sender, EventArgs e)
		{
			List<SheetItem> selectedItems = new List<SheetItem> ();
			List<ListViewItem> selectedListViewItems = new List<ListViewItem> ();
			foreach (int index in ListViewSprites.SelectedIndices)
			{
				selectedItems.Add (spriteSheet.Items [index]);
				selectedListViewItems.Add (ListViewSprites.Items [index]);
			}

			foreach (var item in selectedListViewItems)
				ListViewSprites.Items.Remove (item);
			foreach (var item in selectedItems)
				spriteSheet.RemoveSprite (item.Name);

			spriteSheet.Refresh ();
			var spriteSheetImage = spriteSheet.GenerateSpriteSheet ();
			PictureBoxPreview.Image = ImageHelper.Convert (spriteSheetImage);
		}

		private void MenuItemSpriteReplace_Click (object sender, EventArgs e)
		{
			if (ListViewSprites.SelectedIndices.Count == 0)
				return;

			OpenFileDialogSprite.Multiselect = true;
			if (OpenFileDialogSprite.ShowDialog (this) == DialogResult.Cancel)
				return;

			spriteSheet.Items [ListViewSprites.SelectedIndices [0]].Set (OpenFileDialogSprite.FileName, false);

			spriteSheet.Refresh ();
			var spriteSheetImage = spriteSheet.GenerateSpriteSheet ();
			PictureBoxPreview.Image = ImageHelper.Convert (spriteSheetImage);
		}

		private void ListViewSprites_DoubleClick (object sender, EventArgs e)
		{
			MenuItemSpriteReplace_Click (sender, e);
		}

		private void ComboBoxMaximumSize_SelectedIndexChanged (object sender, EventArgs e)
		{
			spriteSheet.MaximumSize = int.Parse (ComboBoxMaximumSize.Text);
			spriteSheet.Refresh ();
			var spriteSheetImage = spriteSheet.GenerateSpriteSheet ();
			PictureBoxPreview.Image = ImageHelper.Convert (spriteSheetImage);
		}

		private void ListViewSprites_SelectedIndexChanged (object sender, EventArgs e)
		{
			PictureBoxPreview.Refresh ();
		}

		private void ListViewSprites_DragEnter (object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent (DataFormats.FileDrop))
				e.Effect = DragDropEffects.Link;
			else
				e.Effect = DragDropEffects.None;
		}

		private void ListViewSprites_DragDrop (object sender, DragEventArgs e)
		{
			foreach (string filename in e.Data.GetData (DataFormats.FileDrop) as string [])
				AddSprite (filename);

			spriteSheet.Refresh ();
			var spriteSheetImage = spriteSheet.GenerateSpriteSheet ();
			PictureBoxPreview.Image = ImageHelper.Convert (spriteSheetImage);
		}

		private void PictureBoxPreview_Paint (object sender, PaintEventArgs e)
		{
			for (int y = 0; y < PictureBoxPreview.Height; y += 8)
			{
				for (int x = 0; x < PictureBoxPreview.Width; x += 8)
				{
					Brush drawingBrush = null;
					if ((y / 8) % 2 == 0)
						drawingBrush = (x / 8) % 2 == 0 ? TransparentGrayBrush : TransparentWhiteBrush;
					else
						drawingBrush = (x / 8) % 2 == 0 ? TransparentWhiteBrush : TransparentGrayBrush;
					e.Graphics.FillRectangle (drawingBrush, new Rectangle (x, y, 8, 8));
				}
			}
			e.Graphics.DrawImage (PictureBoxPreview.Image, new Point ());
			ToolTipSelectedDetails.Hide (PictureBoxPreview);
			foreach (int index in ListViewSprites.SelectedIndices)
			{
				if (spriteSheet.Items.Count <= index)
					continue;
				var area = spriteSheet.Items [index].SheetArea;
				if (area.Location == SpriteSheet.NOT_BATCHED)
					continue;
				e.Graphics.DrawRectangle (SelectedItemBlackPen, area);
				if (ListViewSprites.SelectedIndices.Count == 1)
					ToolTipSelectedDetails.Show ($"{area.X}, {area.Y}, {area.Width}, {area.Height}", PictureBoxPreview,
						new Point (area.X, area.Y + area.Height), 3000);
			}
		}

		private void PictureBoxPreview_MouseUp (object sender, MouseEventArgs e)
		{
			var mousePosition = new Rectangle (e.X, e.Y, 1, 1);
			for (int i = 0; i < spriteSheet.Items.Count; ++i)
			{
				if (spriteSheet.Items [i].SheetArea.IntersectsWith (mousePosition))
				{
					ListViewSprites.SelectedIndices.Clear ();
					ListViewSprites.SelectedIndices.Add (i);
					break;
				}
			}
		}
	}
}
