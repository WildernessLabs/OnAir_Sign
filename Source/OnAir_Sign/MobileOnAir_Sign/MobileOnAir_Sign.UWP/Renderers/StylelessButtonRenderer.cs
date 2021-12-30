﻿using MobileOnAir_Sign.UWP.Renderers;
using Windows.UI.Xaml;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(StylelessButtonRenderer))]
namespace MobileOnAir_Sign.UWP.Renderers
{
    public class StylelessButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var style = Application.Current.Resources["ButtonStyle"] as Style;
                Control.Style = style;
            }
        }
    }
}