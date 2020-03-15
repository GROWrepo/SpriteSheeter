using System;
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
				using (Stream outputStream = new FileStream (Path.Combine (outputDir, $"{outputFilename}.png"), FileMode.Create))
				{
					generated.SaveAsPng (outputStream);
				}
			}

			using (Stream outputStream = new FileStream (Path.Combine (outputDir, $"{outputFilename}.json"), FileMode.Create))
			{
				using (StreamWriter writer = new StreamWriter (outputStream, Encoding.UTF8))
				{
					writer.WriteLine ("{");
					foreach (var filename in spriteSheet.Filenames)
					{
						var position = spriteSheet.Positions[filename];
						var sprite = spriteSheet.Sprites[filename];
						var size = new System.Drawing.Size (sprite.Width, sprite.Height);
						Console.WriteLine ($"INFO: {filename}: {new System.Drawing.Rectangle (position, size)}");
						writer.WriteLine ($"\t\"{filename}\" : \"{position.X},{position.Y},{size.Width},{size.Height}\"");
					}
					writer.WriteLine ("}");
					writer.Flush ();
				}
			}
		}
	}
}
