using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

using KMSCalendar.UWP;

[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
namespace KMSCalendar.UWP
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
        }
    }
}