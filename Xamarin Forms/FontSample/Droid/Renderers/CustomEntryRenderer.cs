using System;
using Android.Graphics;
using Android.Widget;
using FontSample.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(CustomEntryRenderer))]
namespace FontSample.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
				return;

            if (!String.IsNullOrEmpty(Element.FontFamily))
            {
                Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, Element.FontFamily + ".ttf");

            }
		}
	}
}
