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

		public override string ToString () => $"{{Name: {Name}, Area: {SheetArea}}}";
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

		public bool IsDelayedRefresh { get; set; } = false;

		public SpriteSheet ()
		{

		}

		public string AddSprite (string filepath)
		{
			var item = new SheetItem (filepath);
			items.Add (item);
			if (!IsDelayedRefresh)
				Refresh ();
			return item.Name;
		}

		public void ReplaceSprite (string filename, string filepath)
		{
			var item = items.Find (i => i.Name == filename);
			if (item == null)
				return;
			item.Set (filepath);
			if (!IsDelayedRefresh)
				Refresh ();
		}

		public void RemoveSprite (string filename)
		{
			var item = items.Find (i => i.Name == filename);
			if (item == null)
				return;
			items.Remove (item);
			if (!IsDelayedRefresh)
				Refresh ();
		}

		public void Clear ()
		{
			items.Clear ();
			if (!IsDelayedRefresh)
				Refresh ();
		}

		public SheetItem FindItem (string name)
		{
			foreach (var item in items)
				if (item.Name == name)
					return item;
			return null;
		}

		public SheetItem this[string name]
		{
			get => FindItem (name);
		}

		public void Refresh ()
		{
			Refresh (128);
		}


		private static readonly System.Drawing.Point NOT_BATCHED = new System.Drawing.Point(-1, -1);
		private IEnumerable<SheetItem> GetBatchedItems () =>
			from item in items where item.SheetArea.Location != NOT_BATCHED select item;
		private IEnumerable<SheetItem> GetNotBatchedItems () =>
			from item in items where item.SheetArea.Location == NOT_BATCHED select item;

		private void Refresh (int targetSize)
		{
			foreach (var item in items)
				item.SheetArea.Location = NOT_BATCHED;

			foreach (var item in items)
			{
				bool isBatched = true;

				if (GetBatchedItems ().Count () == 0)
				{
					item.SheetArea.Location = new System.Drawing.Point (0, 0);
					continue;
				}

				foreach (var batched in GetBatchedItems ())
				{
					if (item == batched)
						continue;

					isBatched = true;
					item.SheetArea.Location = new System.Drawing.Point (batched.SheetArea.X + batched.SheetArea.Width, batched.SheetArea.Y);
					if ((item.SheetArea.X + item.SheetArea.Width) <= targetSize
						&& (item.SheetArea.Y + item.SheetArea.Height) <= targetSize)
					{
						foreach (var batchedCompare in GetBatchedItems ())
						{
							if (batched == batchedCompare || item == batchedCompare)
								continue;
							if (item.SheetArea.IntersectsWith (batchedCompare.SheetArea))
							{
								item.SheetArea.Location = NOT_BATCHED;
								isBatched = false;
								break;
							}
						}
					}
					else
					{
						item.SheetArea.Location = NOT_BATCHED;
						isBatched = false;
					}

					if (!isBatched)
					{
						isBatched = true;
						item.SheetArea.Location = new System.Drawing.Point (batched.SheetArea.X, batched.SheetArea.Y + batched.SheetArea.Height);
						if ((item.SheetArea.X + item.SheetArea.Width) <= targetSize
							&& (item.SheetArea.Y + item.SheetArea.Height) <= targetSize)
						{
							foreach (var batchedCompare in GetBatchedItems ())
							{
								if (batched == batchedCompare || item == batchedCompare)
									continue;
								if (item.SheetArea.IntersectsWith (batchedCompare.SheetArea))
								{
									item.SheetArea.Location = NOT_BATCHED;
									isBatched = false;
									break;
								}
							}
						}
						else
						{
							item.SheetArea.Location = NOT_BATCHED;
							isBatched = false;
						}
					}

					if (isBatched)
						break;
				}

				if (!isBatched)
				{
					item.SheetArea.Location = NOT_BATCHED;
				}
			}

			if (GetNotBatchedItems ().Count () != 0)
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
				if (item.SheetArea.Location == NOT_BATCHED)
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
