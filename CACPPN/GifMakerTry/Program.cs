using System;
using System.IO;
using Gif;

namespace GifMakerTry
{
	class Program
	{
		static void Main(string[] args)
		{
			string folderPath = @"C:\Users\sindr\Pictures\GifTest";
			string[] folderPaths = new string[] {folderPath,
												 folderPath + " - Copy",
												 folderPath + " - Copy (2)",
												 folderPath + " - Copy (3)"};
			string gifPath = Path.Combine(folderPath,"Dank.gif");
			var images = GifMaker.GetImagesInDirectory(folderPath);
			int frameDuration = 20;
			GifMaker.MakeGif(images, gifPath, frameDuration);

			Console.WriteLine("trying the multiMaker!");
			GifMaker.MakeDirectoryGifs(folderPaths, @"C:\Users\sindr\Pictures\", frameDuration);
		}
	}
}
