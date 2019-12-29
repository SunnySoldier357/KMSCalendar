using Android.Content;

using KMSCalendar.Controls;
using KMSCalendar.Droid.Controls;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TransparentEntry), typeof(TransparentEntryRenderer))]
namespace KMSCalendar.Droid.Controls
{
	public class TransparentEntryRenderer : EntryRenderer
	{
		public TransparentEntryRenderer(Context context) : base(context) { }

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
				Control.Background = null;
		}
	}
}