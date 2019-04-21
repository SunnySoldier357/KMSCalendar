using Android.Content;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using KMSCalendar.Droid;

[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
namespace KMSCalendar.Droid
{
    public class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.Background = null;
        }
    }
}