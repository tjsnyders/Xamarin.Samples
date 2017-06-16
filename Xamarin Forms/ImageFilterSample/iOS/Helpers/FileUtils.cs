using System;
using System.Diagnostics;
using System.IO;
using Foundation;
using UIKit;
namespace ImageFilterSample.iOS.Helpers
{
	public static class FileUtils
	{
		public static string GetOutputPath(string directoryName, string bundleName, string name)
		{
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), directoryName);
			Directory.CreateDirectory(path);

			if (string.IsNullOrWhiteSpace(name))
			{
				string timestamp = DateTime.Now.ToString("yyyMMdd_HHmmss");

				name = $"{bundleName}_{timestamp}.jpg";
			}



			return Path.Combine(path, GetUniquePath(path, name));
		}

		public static string GetUniquePath(string path, string name)
		{

			string ext = Path.GetExtension(name);
			if (ext == String.Empty)
				ext = ".jpg";

			name = Path.GetFileNameWithoutExtension(name);

			string nname = name + ext;
			int i = 1;
			while (File.Exists(Path.Combine(path, nname)))
				nname = name + "_" + (i++) + ext;


			return Path.Combine(path, nname);


		}

		public static string SaveToDisk(UIImage image, string bundleName, string fileName = null, string directoryName = null)
		{


			NSError err = null;
			string path = GetOutputPath(directoryName ?? bundleName, bundleName, fileName);

			if (!File.Exists(path))
			{

				if (image.AsJPEG().Save(path, true, out err))
				{
					Debug.WriteLine("saved as " + path);
					Console.WriteLine("saved as " + path);
				}
				else
				{
					Debug.WriteLine("NOT saved as " + path +
						" because" + err.LocalizedDescription);
					Console.WriteLine("NOT saved as " + path +
						" because" + err.LocalizedDescription);
				}
			}

			return path;
		}
	}
}
