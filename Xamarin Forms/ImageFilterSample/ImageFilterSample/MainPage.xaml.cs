using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ImageFilterSample.ViewModels;

namespace ImageFilterSample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			BindingContext = new MainPageViewModel();

		}
	}
}
