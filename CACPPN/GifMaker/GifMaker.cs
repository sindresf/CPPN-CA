using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using ImageMagick;

namespace Gif
{
	static public class GifMaker
	{
		public static string[] GetImagesInDirectory(string folderPath)
		{
			if(!Directory.Exists(folderPath)) throw new ArgumentException("no Folder at that path! path:" + folderPath);

			var filteredFiles = Directory
				.EnumerateFiles(folderPath)
				.Where(file => file.ToLower().EndsWith("png") || file.ToLower().EndsWith("jpg"))
				.ToList();
			return filteredFiles.ToArray();
		}

		public static void MakeGif(string[] imagePaths, string gifPath, int frameDuration)
		{
			using(MagickImageCollection collection = new MagickImageCollection())
			{
				int indexCount = 0;
				foreach(string imgPath in imagePaths)
				{
					collection.Add(imgPath);
					collection[indexCount++].AnimationDelay = frameDuration;
				}
				collection.Write(gifPath);
			}
		}
		public static void MakeDirectoryGifs(string[] dirPaths, string GifDirPath, int frameDuration)
		{
			int gifNameAdd = 1;
			string gifPath;
			foreach(string dirPath in dirPaths)
			{
				gifPath = Path.Combine(GifDirPath, "Dank.gif".Insert(4, "" + gifNameAdd++));
				var images = GetImagesInDirectory(dirPath);
				MakeGif(images, gifPath, frameDuration);
			}
		}
		private static void SaveGif(GifBitmapEncoder gEnc, string path)
		{
			using(FileStream fs = new FileStream(path, FileMode.Create))
				gEnc.Save(fs);
		}
	}
}
