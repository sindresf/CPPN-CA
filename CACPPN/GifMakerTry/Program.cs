using System.IO;
using Gif;

namespace GifMakerTry
{
	class Program
	{
		static void Main(string[] args)
		{
			string folderPath = @"C:\Users\sindr\Pictures\GifTest";
			string gifPath = Path.Combine(folderPath,"Dank.gif");
			var images = GifMaker.GetImagesInDirectory(folderPath);
			int frameDuration = 20;
			GifMaker.MakeMagickGif(images, gifPath, frameDuration);
		}
	}
}
