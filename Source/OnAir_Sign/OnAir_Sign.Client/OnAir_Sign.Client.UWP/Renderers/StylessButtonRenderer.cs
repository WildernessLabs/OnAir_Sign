using OnAir_Sign.App.UWP.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Button), typeof(StylessButtonRenderer))]
namespace OnAir_Sign.App.UWP.Renderers
{
    public class StylessButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Resources["ButtonBackgroundPointerOver"] = Control.BackgroundColor;
                Resources["ButtonBackgroundPressed"] = Control.BackgroundColor;
            }
        }
    }
}
