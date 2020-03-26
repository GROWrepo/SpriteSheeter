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

			SpriteSheet spriteSheet = new SpriteSheet ()
			{
				IsDelayedRefresh = true
			};
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

			spriteSheet.Refresh ();
			spriteSheet.Export (outputDir, outputFilename);
		}
	}
}
