using FFImageLoading.Forms.Touch;
using FFImageLoading.Forms;
using Xamarin.Forms;
using ImageFilterSample.Controls;
using ImageFilterSample.iOS.Renderers;
using ImageFilterSample.Helpers;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using System.Threading.Tasks;
using CoreImage;
using ImageFilterSample.iOS.Helpers;
using System.IO;
using System;
using CoreGraphics;
using GPUImage.Filters.ColorProcessing;

[assembly: ExportRenderer(typeof(FilterImage), typeof(FilterImageRenderer))]
namespace ImageFilterSample.iOS.Renderers
{
	public class FilterImageRenderer : CachedImageRenderer
	{
		FilterType currentFilterType = FilterType.NoFilter;
		UIImage filteredImage = null;
		private static IImageSourceHandler GetHandler(ImageSource source)
		{
			IImageSourceHandler returnValue = null;
			if (source is UriImageSource)
			{
				returnValue = new ImageLoaderSourceHandler();
			}
			else if (source is FileImageSource)
			{
				returnValue = new FileImageSourceHandler();
			}
			else if (source is StreamImageSource)
			{
				returnValue = new StreamImagesourceHandler();
			}
			return returnValue;
		}


		private async static Task SetImageAsync(ImageSource source, UIImageView targetImageView)
		{
			var handler = GetHandler(source);
			using (UIImage image = await handler.LoadImageAsync(source))
			{
				targetImageView.Image = image;

			}
		}

		protected override async void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == FilterImage.SourceProperty.PropertyName)
			{
				await SetImageAsync(Element.Source, Control);
			}

		}

		protected override void OnElementChanged(ElementChangedEventArgs<CachedImage> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
			}

			if (e.NewElement != null)
			{

				var filterImage = e.NewElement as FilterImage;
				filterImage.SelectFilterCommand = new Command<FilterType>((fType) => ApplyFilter(fType), (b) => (filterImage.OriginalSource != null));
				filterImage.ApplyFilterCommand = new Command(async () =>
				{
					if (filteredImage == null)
					{
						var handler = GetHandler(filterImage.OriginalSource);
						filteredImage = await handler.LoadImageAsync(filterImage.OriginalSource);
					}

					string path = FileUtils.SaveToDisk(filteredImage, "Sample");
					var fInfo = new FileInfo(path);
					filterImage?.OnFilterApplied(this, new Tuple<string, byte[], long>(path, File.ReadAllBytes(path), fInfo.Length));
				}, () => (filterImage.OriginalSource != null));
			}
		}

		//Filters
		async void ApplyFilter(FilterType filterType)
		{
			if (currentFilterType == filterType)
				return;

			var filterImage = Element as FilterImage;
			var handler = GetHandler(filterImage.OriginalSource);
			using (UIImage originalImage = await handler.LoadImageAsync(filterImage.OriginalSource))
			{
				switch (filterType)
				{
					case FilterType.NoFilter:
						filteredImage = UIImage.FromImage(originalImage.CGImage, originalImage.CurrentScale, originalImage.Orientation);
						break;
					case FilterType.BlackAndWhite:
						var blackAndWhiteEffect = new CIPhotoEffectMono()
						{
							Image = new CoreImage.CIImage(originalImage)
						};
						filteredImage = ApplyEffect(blackAndWhiteEffect, originalImage);
						break;
					case FilterType.Hifi:
						var hifiControls = new CIColorControls()
						{
							Image = new CoreImage.CIImage(originalImage),
							Brightness = .5F, // Min: 0 Max: 2
							Saturation = 1.2F, // Min: -1 Max: 1
							Contrast = 2.5F // Min: 0 Max: 4
						};

						filteredImage = ApplyEffect(hifiControls, originalImage);
						break;
					case FilterType.Vintage:
						var vintageEffect = new CIPhotoEffectChrome()
						{
							Image = new CoreImage.CIImage(originalImage)
						};

						filteredImage = ApplyEffect(vintageEffect, originalImage);
						break;
					case FilterType.Saturated:
						var saturatedEffect = new GPUImageSaturationFilter();
						saturatedEffect.Saturation = 2.5f;
						var imageFiltered = saturatedEffect.CreateFilteredImage(originalImage);
						filteredImage = imageFiltered;
						break;
				}
				Control.Image = filteredImage;
				currentFilterType = filterType;

			}



		}



		UIImage ApplyEffect(CIFilter effect, UIImage originalImage)
		{

			var holder = effect.OutputImage;
			CGRect extent = holder.Extent;
			var context = CIContext.FromOptions(null);
			var cgImage = context.CreateCGImage(holder, extent);
			var fixedImage = UIImage.FromImage(cgImage, originalImage.CurrentScale, originalImage.Orientation);
			return fixedImage;

			//cgImage.Dispose();
		}
	}
}
