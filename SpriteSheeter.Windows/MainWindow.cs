using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
		bool isDirty;
		string savepath;

		public bool IsSaved
		{
			get
			{
				if (isDirty)
				{
					switch (MessageBox.Show ("저장하시겠습니까?", "스프라이트 시터", MessageBoxButtons.YesNoCancel))
					{
						case DialogResult.Yes: return Save ();
						case DialogResult.No: return true;
						case DialogResult.Cancel: return false;
					}
				}
				return true;
			}
		}

		public class FileItem
		{
			[JsonPropertyName ("name")]
			public string Name { get; set; }
			[JsonPropertyName ("image")]
			public string ImageBase64 { get; set; }
		}

		public void Open ()
		{
			if (OpenFileDialogSpriteSheet.ShowDialog (this) == DialogResult.Cancel)
				return;

			spriteSheet.Clear ();
			ListViewSprites.Items.Clear ();

			string input = File.ReadAllText (OpenFileDialogSpriteSheet.FileName);
			List<FileItem> fileItems = JsonSerializer.Deserialize<List<FileItem>> (input);
			foreach (var fileItem in fileItems)
			{
				SheetItem item = new SheetItem ();
				item.Name = fileItem.Name;
				byte [] imageData = Convert.FromBase64String (fileItem.ImageBase64);
				using (MemoryStream stream = new MemoryStream (imageData))
				{
					stream.Position = 0;
					item.Sprite = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32> (stream);
				}
				item.ResetArea ();

				string name = spriteSheet.AddSprite (item);
				var listViewItem = ListViewSprites.Items.Add (name);
				ListViewSprites.SmallImageList.Images.Add (name, ImageHelper.Convert (spriteSheet [name].Sprite));
				listViewItem.ImageKey = name;
			}

			isDirty = false;
			savepath = OpenFileDialogSpriteSheet.FileName;

			spriteSheet.Refresh ();
			var spriteSheetImage = spriteSheet.GenerateSpriteSheet ();
			PictureBoxPreview.Image = ImageHelper.Convert (spriteSheetImage);
		}

		public bool Save (bool force = false)
		{
			if (isDirty || force)
			{
				if (string.IsNullOrEmpty (savepath) || force)
				{
					if (SaveFileDialogSpriteSheet.ShowDialog (this) == DialogResult.Cancel)
						return false;
					savepath = SaveFileDialogSpriteSheet.FileName;
				}

				List<FileItem> fileItems = new List<FileItem> ();
				foreach (var item in spriteSheet.Items)
				{
					MemoryStream imageData = new MemoryStream ();
					item.Sprite.Save (imageData, new SixLabors.ImageSharp.Formats.Png.PngEncoder ()
					{
						BitDepth = SixLabors.ImageSharp.Formats.Png.PngBitDepth.Bit8,
						ColorType = SixLabors.ImageSharp.Formats.Png.PngColorType.RgbWithAlpha,
						FilterMethod = SixLabors.ImageSharp.Formats.Png.PngFilterMethod.Adaptive,
						InterlaceMethod = SixLabors.ImageSharp.Formats.Png.PngInterlaceMode.None,
					});

					var fileItem = new FileItem ();
					fileItem.Name = item.Name;
					fileItem.ImageBase64 = Convert.ToBase64String (imageData.ToArray ());

					fileItems.Add (fileItem);
				}

				string output = JsonSerializer.Serialize (fileItems);
				using (Stream fileStream = new FileStream (savepath, FileMode.Create, FileAccess.Write))
				{
					using (TextWriter writer = new StreamWriter (fileStream))
					{
						writer.Write (output);
						writer.Flush ();
					}
				}

				isDirty = false;
			}
			return true;
		}

		public MainWindow ()
		{
			InitializeComponent ();

			spriteSheet = new SpriteSheet ();
			spriteSheet.IsDelayedRefresh = true;
			isDirty = false;

			ComboBoxMaximumSize.Text = spriteSheet.MaximumSize.ToString ();
			ListViewSprites.SmallImageList = new ImageList ();
		}

		private void AddSprite (string filename)
		{
			string name = spriteSheet.AddSprite (filename);
			var item = ListViewSprites.Items.Add (name);
			ListViewSprites.SmallImageList.Images.Add (name, ImageHelper.Convert (spriteSheet [name].Sprite));
			item.ImageKey = name;
			isDirty = true;
		}

		private void MenuItemNewSpriteSheet_Click (object sender, EventArgs e)
		{
			if (!IsSaved)
				return;

			spriteSheet.Clear ();
			ListViewSprites.Items.Clear ();

			isDirty = false;
			savepath = null;

			spriteSheet.Refresh ();
			var spriteSheetImage = spriteSheet.GenerateSpriteSheet ();
			PictureBoxPreview.Image = ImageHelper.Convert (spriteSheetImage);
		}

		private void MenuItemOpen_Click (object sender, EventArgs e)
		{
			if (!IsSaved)
				return;
			Open ();
		}

		private void MenuItemSave_Click (object sender, EventArgs e)
		{
			Save ();
		}

		private void MenuItemSaveAs_Click (object sender, EventArgs e)
		{
			Save (true);
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

			isDirty = true;

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

			isDirty = true;

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
