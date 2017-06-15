using System;
using System.Collections.Generic;
using SelectMultiIpleImagesApp.Services;
using Xamarin.Forms;

namespace SelectMultiIpleImagesApp
{
	public partial class MainPage : ContentPage
	{
		List<string> _images = new List<string>();
		public MainPage()
		{
			InitializeComponent();

			//Remember to remove the temporal files 
			//DependencyService.Get<IMediaService>().ClearFiles(_images);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			MessagingCenter.Subscribe<App, List<string>> ((App)Xamarin.Forms.Application.Current, "ImagesSelected", (s, images) =>
		    {
				listItems.FlowItemsSource = images;
				_images = images;
		    });
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			MessagingCenter.Unsubscribe<App, List<string>>(this, "ImagesSelected");
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await DependencyService.Get<IMediaService>().OpenGallery();
		}

	}
}
