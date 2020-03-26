using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteSheeter
{
	public static class ImageHelper
	{
		public static System.Drawing.Bitmap Convert (SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32> image)
		{
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap (image.Width, image.Height);

			for (int y = 0; y < image.Height; ++y)
			{
				for (int x = 0; x < image.Width; ++x)
				{
					var currentColor = image [x, y];
					bitmap.SetPixel (x, y, System.Drawing.Color.FromArgb (currentColor.A, currentColor.R, currentColor.G, currentColor.B));
				}
			}

			return bitmap;
		}
	}
}
