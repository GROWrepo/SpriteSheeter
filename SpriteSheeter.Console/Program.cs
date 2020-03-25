using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;

namespace SpriteSheeter
{
	class Program
	{
		static void PrintHelp ()
		{
			Console.WriteLine ("Usage:");
			Console.WriteLine ("  ssc [--output-dir=<Output Directory>] [--output-filename=<Output Filename>] [--maximum-size=<Maximum Image Size>] <files>");
		}

		static void Main (string[] args)
		{
			if(args.Length == 0 || args[0] == "--help")
			{
				PrintHelp ();
				return;
			}

			SpriteSheet spriteSheet = new SpriteSheet ();
			string outputDir = "";
			string outputFilename = "output";

			foreach(var file in args)
			{
				if (file.StartsWith ("--output-dir="))
				{
					outputDir = file.Substring ("--output-dir=".Length);
					continue;
				}
				else if (file.StartsWith ("--output-filename="))
				{
					outputFilename = file.Substring ("--output-filename=".Length);
					continue;
				}
				else if (file.StartsWith ("--maximum-size="))
				{
					spriteSheet.MaximumSize = int.Parse (file.Substring ("--maximum-size=".Length));
					continue;
				}

				if(!File.Exists (file))
				{
					Console.WriteLine ($"ERROR: File not Found: {file}");
					continue;
				}

				try
				{
					spriteSheet.AddSprite (file);
				}
				catch (Exception ex)
				{
					Console.WriteLine ($"ERROR: Cannot load Image: {file} because: {ex.Message}");
					continue;
				}
			}

			using (Image generated = spriteSheet.GenerateSpriteSheet ())
			{
				generated.Save (Path.Combine (outputDir, $"{outputFilename}.png"), new SixLabors.ImageSharp.Formats.Png.PngEncoder ()
				{
					BitDepth = SixLabors.ImageSharp.Formats.Png.PngBitDepth.Bit8,
					ColorType = SixLabors.ImageSharp.Formats.Png.PngColorType.RgbWithAlpha,
					FilterMethod = SixLabors.ImageSharp.Formats.Png.PngFilterMethod.Adaptive,
					InterlaceMethod = SixLabors.ImageSharp.Formats.Png.PngInterlaceMode.None,
				});
			}

			using (Stream outputStream = new FileStream (Path.Combine (outputDir, $"{outputFilename}.json"), FileMode.Create))
			{
				using (StreamWriter writer = new StreamWriter (outputStream, Encoding.UTF8))
				{
					writer.WriteLine ("{");
					foreach (var item in spriteSheet.Items)
					{
						var area = item.SheetArea;
						var message = $"INFO: {item.Name}: {area}";
						Console.WriteLine (message);
						Debug.WriteLine (message);
						writer.WriteLine ($"\t\"{item.Name}\" : \"{area.X},{area.Y},{area.Width},{area.Height}\"{((item.Name != spriteSheet.Items[spriteSheet.Items.Count - 1].Name) ? "," : "")}");
					}

					writer.WriteLine ("}");
					writer.Flush ();
				}
			}
		}
	}
}
