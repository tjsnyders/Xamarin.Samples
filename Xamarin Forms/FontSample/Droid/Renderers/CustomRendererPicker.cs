using System;
using Android.Graphics;
using FontSample.Droid;
using FontSample.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomRendererPicker))]
namespace FontSample.Droid.Renderers
{
    public class CustomRendererPicker : PickerRenderer
	{
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
				return;
         
				Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets,  "orangejuice.ttf");
		}
	}
}