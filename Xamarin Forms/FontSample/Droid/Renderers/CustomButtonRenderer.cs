using System;
using Android.Graphics;
using Android.Widget;
using FontSample.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(CustomButtonRenderer))]
namespace FontSample.Droid
{
    public class CustomButtonRenderer: ButtonRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
				return;

			if (!String.IsNullOrEmpty(Element.FontFamily))
                Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, Element.FontFamily + ".ttf");
		}
    }
}
