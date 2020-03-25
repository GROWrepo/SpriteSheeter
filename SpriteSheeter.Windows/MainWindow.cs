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
			if(OpenFileDialogSprite.ShowDialog () == DialogResult.Cancel)
				return;

			foreach (var filename in OpenFileDialogSprite.FileNames)
			{
				string name = spriteSheet.AddSprite (filename);
				var item = ListViewSprites.Items.Add (name);
				ListViewSprites.SmallImageList.Images.Add (name, ImageHelper.Convert (spriteSheet [name].Sprite));
				item.ImageKey = name;
			}

			spriteSheet.Refresh ();
			var spriteSheetImage = spriteSheet.GenerateSpriteSheet ();
			PictureBoxPreview.Image = ImageHelper.Convert (spriteSheetImage);
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
			foreach (int index in ListViewSprites.SelectedIndices)
			{
				var area = spriteSheet.Items [index].SheetArea;
				e.Graphics.DrawRectangle (SelectedItemBlackPen, area);
				e.Graphics.DrawString (area.ToString (), TextFont, TextBrush, new Point (area.X, area.Y + area.Height));
			}
		}
	}
}
