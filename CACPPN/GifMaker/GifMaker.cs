using System;
using System.IO;
using System.Linq;
using System.Windows;
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

		public static void MakeGif(IntPtr[] images, string gifPath)
		{
			GifBitmapEncoder gEnc = new GifBitmapEncoder();
			foreach(IntPtr pointer in images)
			{
				var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
										pointer,
										IntPtr.Zero,
										Int32Rect.Empty,
										BitmapSizeOptions.FromEmptyOptions());
				var frame = BitmapFrame.Create(src);
				gEnc.Frames.Add(frame);
			}
			SaveGif(gEnc, gifPath);
		}
		public static void MakeMagickGif(string[] imagePaths, string gifPath, int frameDuration)
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
		private static void SaveGif(GifBitmapEncoder gEnc, string path)
		{
			using(FileStream fs = new FileStream(path, FileMode.Create))
				gEnc.Save(fs);
		}
	}
}
