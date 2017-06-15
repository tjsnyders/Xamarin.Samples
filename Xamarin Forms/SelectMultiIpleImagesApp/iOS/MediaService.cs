using System;
using CoreGraphics;
using AssetsLibrary;
using UIKit;
using Foundation;
using ObjCRuntime;
using System.Collections.Generic;
using Xamarin.Forms;
using SelectMultiIpleImagesApp.iOS;
using SelectMultiIpleImagesApp.Services;
using ELCImagePicker;
using System.Threading.Tasks;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace SelectMultiIpleImagesApp.iOS
{
	public class MediaService : IMediaService, IMessanger
	{
		public async Task OpenGallery()
		{
			var picker = ELCImagePickerViewController.Create(15);
			picker.MaximumImagesCount = 15;

			var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
			while (topController.PresentedViewController != null)
			{
				topController = topController.PresentedViewController;
			}
			topController.PresentViewController(picker, true, null);

			await picker.Completion.ContinueWith(t =>
			{
				picker.BeginInvokeOnMainThread(() =>
				{
							//dismiss the picker
							picker.DismissViewController(true, null);

					if (t.IsCanceled || t.Exception != null)
					{
					}
					else
					{
						List<string> images = new List<string>();

						var items = t.Result as List<AssetResult>;
						foreach (var item in items)
						{
							var path = Save(item.Image, item.Name);
							images.Add(path);
							//CleanPath(path);
						}

						MessagingCenter.Send<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", images);
					}
				});
			});
		}

		string Save(UIImage image, string name)
		{
			var documentsDirectory = Environment.GetFolderPath
								  (Environment.SpecialFolder.Personal);
			string jpgFilename = System.IO.Path.Combine(documentsDirectory, name); // hardcoded filename, overwritten each time
			NSData imgData = image.AsJPEG();
			NSError err = null;
			if (imgData.Save(jpgFilename, false, out err))
			{
				return jpgFilename;
			}
			else
			{
				Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
				return null;
			}
		}

		void IMediaService.ClearFiles(List<string> filePaths)
		{
			var documentsDirectory = Environment.GetFolderPath
							  (Environment.SpecialFolder.Personal);

			if (Directory.Exists(documentsDirectory))
			{
				foreach (var p in filePaths)
				{
					File.Delete(p);
				}
			}
		}
	}
}