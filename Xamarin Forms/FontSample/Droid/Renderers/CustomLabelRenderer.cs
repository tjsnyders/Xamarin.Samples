using System;
using Android.Graphics;
using FontSample.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace FontSample.Droid
{
    public class CustomLabelRenderer: LabelRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

            if (Control == null)
                return;
            
			if (!String.IsNullOrEmpty(Element.FontFamily))
				Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, Element.FontFamily + ".ttf");
		}
    }
}
