using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using KMSCalendar.Controls;
using KMSCalendar.iOS;

[assembly: ExportRenderer(typeof(TransparentEntry), typeof(TransparentEntryRenderer))]
namespace KMSCalendar.iOS
{
    class TransparentEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}