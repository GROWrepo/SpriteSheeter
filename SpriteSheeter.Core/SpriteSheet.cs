using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.Metadata;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.ColorSpaces;

namespace SpriteSheeter
{
	public class SheetItem
	{
		public string Name;
		public Image<Rgba32> Sprite;
		public Rectangle SheetArea;

		public SheetItem (string path)
		{
			Set (path);
		}

		public void Set (string path)
		{
			Name = System.IO.Path.GetFileName (path);
			Sprite = SixLabors.ImageSharp.Image.Load<Rgba32> (path);
			ResetArea ();
		}

		public void ResetArea ()
		{
			SheetArea = new Rectangle (-1, -1, Sprite.Width, Sprite.Height);
		}
	}

	public class SpriteSheet
	{
		int maximumSize = 4096;
		List<SheetItem> items = new List<SheetItem> ();

		public IReadOnlyList<SheetItem> Items => items;

		public int MaximumSize
		{
			get => maximumSize;
			set
			{
				maximumSize = value;
				Refresh ();
			}
		}

		public int CurrentSize { get; private set; } = 128;

		public SpriteSheet ()
		{

		}

		public string AddSprite (string filepath)
		{
			var item = new SheetItem (filepath);
			items.Add (item);
			Refresh ();
			return item.Name;
		}

		public void ReplaceSprite (string filename, string filepath)
		{
			var item = items.Find (i => i.Name == filename);
			if (item == null)
				return;
			item.Set (filepath);
			Refresh ();
		}

		public void RemoveSprite (string filename)
		{
			var item = items.Find (i => i.Name == filename);
			if (item == null)
				return;
			items.Remove (item);
			Refresh ();
		}

		public void Clear ()
		{
			items.Clear ();
			Refresh ();
		}

		private void Refresh ()
		{
			Refresh (128);
		}

		private void Refresh (int targetSize)
		{
			foreach (var item in items)
				item.SheetArea.Location = new System.Drawing.Point (-1, -1);

			foreach (var item in items)
			{
				bool isBatched = true;
				bool isHorizontalCompare = true;

				if ((from i in items where i.SheetArea.Location != new System.Drawing.Point (-1, -1) select i).Count () == 0)
				{
					item.SheetArea.Location = new System.Drawing.Point (0, 0);
					continue;
				}

				for (int compare = 0; compare < 2; ++compare)
				{
					isBatched = true;

					Rectangle current = new Rectangle (0, 0, item.Sprite.Width, item.Sprite.Height);
					foreach (var batchedItem in from i in items where i.SheetArea.Location != new System.Drawing.Point (-1, -1) select i)
					{
						if (batchedItem.SheetArea.IntersectsWith (current))
							isBatched = false;
						else
						{
							if ((isHorizontalCompare && (current.X + current.Width > targetSize)) || (!isHorizontalCompare && (current.Y + current.Height > targetSize)))
								isBatched = false;
							else
								isBatched = true;
						}

						if (!isBatched)
						{
							if (isHorizontalCompare)
							{
								current.X = batchedItem.SheetArea.X + batchedItem.SheetArea.Width;
								current.Y = batchedItem.SheetArea.Y;
							}
							else
							{
								current.X = batchedItem.SheetArea.X;
								current.Y = batchedItem.SheetArea.Y + batchedItem.SheetArea.Height;
							}

							if ((isHorizontalCompare && (current.X + current.Width > targetSize)) || (!isHorizontalCompare && (current.Y + current.Height > targetSize)))
								isBatched = false;
							else
								isBatched = true;
						}
					}

					if (isBatched)
					{
						item.SheetArea = current;
						break;
					}
					if (isHorizontalCompare)
						isHorizontalCompare = false;
				}
			}

			if ((from i in items where i.SheetArea.Location == new System.Drawing.Point (-1, -1) select i).Count () != 0)
			{
				if (targetSize < maximumSize)
				{
					Refresh (targetSize * 2);
					return;
				}
			}

			CurrentSize = targetSize;
		}

		public Image<Rgba32> GenerateSpriteSheet ()
		{
			Image<Rgba32> spriteSheet = new Image<Rgba32> (CurrentSize, CurrentSize);

			foreach (var item in items)
			{
				if (item.SheetArea.Location == new System.Drawing.Point (-1, -1))
					continue;
				for (int y = 0; y < item.SheetArea.Height; ++y)
				{
					for (int x = 0; x < item.SheetArea.Width; ++x)
					{
						spriteSheet [x + item.SheetArea.X, y + item.SheetArea.Y] = item.Sprite [x, y];
					}
				}
			}

			return spriteSheet;
		}
	}
}
