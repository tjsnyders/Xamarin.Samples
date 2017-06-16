using System;
namespace ImageFilterSample.Droid.Helpers
{
	public static class FileUtils
	{
		public static Java.IO.File GetDirectoryForPictures(string collectionName)
		{
			var fileDir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), collectionName);
			if (!fileDir.Exists())
			{
				fileDir.Mkdirs();
			}

			return fileDir;

		}

		public static Java.IO.File GetDirectoryForVideos(string collectionName)
		{
			var fileDir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMovies), collectionName);
			if (!fileDir.Exists())
			{
				fileDir.Mkdirs();
			}

			return fileDir;
		}
	}
}
