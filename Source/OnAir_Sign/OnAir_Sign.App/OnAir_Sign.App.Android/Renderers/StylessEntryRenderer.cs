using Android.Content;
using OnAir_Sign.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(StylessEntryRenderer))]
namespace OnAir_Sign.App.Droid.Renderers
{
    public class StylessEntryRenderer : EntryRenderer
    {
        public StylessEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                Control.SetBackgroundColor(Color.Transparent.ToAndroid());
                SetPadding(0, 0, 0, 0);
            }
        }
    }
}