using System;
using System.Collections.Generic;
using System.Drawing;
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
    public class SpriteSheet
    {
        int maximumSize = 4096;
        List<string> filenames = new List<string> ();
        Dictionary<string, Image<Rgba32>> sprites = new Dictionary<string, Image<Rgba32>> ();
        Dictionary<string, System.Drawing.Point> positions = new Dictionary<string, System.Drawing.Point> ();

        public IReadOnlyList<string> Filenames => filenames;
        public IReadOnlyDictionary<string, Image<Rgba32>> Sprites => sprites;
        public IReadOnlyDictionary<string, System.Drawing.Point> Positions => positions;

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
            string filename = System.IO.Path.GetFileName (filepath);

            Image<Rgba32> image = Image.Load<Rgba32> (filepath);
            filenames.Add (filename);
            sprites.Add (filename, image);
            positions.Add (filename, new System.Drawing.Point (-1, -1));

            Refresh ();
            return filename;
        }

        public void ReplaceSprite (string filename, string filepath)
        {
            if (filenames.Contains (filename))
            {
                Image<Rgba32> image = Image.Load<Rgba32> (filepath);
                sprites[filename].Dispose ();
                sprites[filename] = image;

                Refresh ();
            }
        }

        public void RemoveSprite (string filename)
        {
            if (filenames.Contains (filename))
            {
                filenames.Remove (filename);
                sprites[filename].Dispose ();
                sprites.Remove (filename);
                positions.Remove (filename);

                Refresh ();
            }
        }

        public void Clear ()
        {
            filenames.Clear ();
            foreach (var sprite in sprites)
                sprite.Value.Dispose ();
            sprites.Clear ();
            positions.Clear ();
        }

        private void Refresh ()
        {
            Refresh (128);
        }

        private void Refresh (int targetSize)
        {
            int currentHorizontal = 0, currentVertical = 0;
            int currentMaximumHeight = 0;

            foreach (var sprite in sprites)
            {
                int nextHorizontal = currentHorizontal + sprite.Value.Width;

                if (nextHorizontal > targetSize)
                {
                    currentHorizontal = 0;
                    int nextVertical = currentVertical + currentMaximumHeight;
                    if (nextVertical > targetSize)
                    {
                        if (targetSize != MaximumSize)
                        {
                            Refresh (targetSize * 2);
                            return;
                        }
                        positions [sprite.Key] = new System.Drawing.Point (-1, -1);
                        continue;
                    }
                    currentVertical = nextVertical;
                    currentMaximumHeight = sprite.Value.Height;
                }

                positions [sprite.Key] = new System.Drawing.Point (currentHorizontal, currentVertical);
                currentMaximumHeight = (int) Math.Max (currentMaximumHeight, sprite.Value.Height);

                currentHorizontal = nextHorizontal;
            }

            CurrentSize = targetSize;
        }

		public Image GenerateSpriteSheet ()
		{
            Image<Rgba32> spriteSheet = new Image<Rgba32> (CurrentSize, CurrentSize);

            foreach (var filename in filenames)
            {
                var sprite = sprites [filename];
                var position = positions [filename];

                for (int y = 0; y < sprite.Height; ++y)
                {
                    for (int x = 0; x < sprite.Width; ++x)
                    {
                        spriteSheet [x + position.X, y + position.Y] = sprite [x, y];
                    }
                }
            }

            return spriteSheet;
		}
    }
}
