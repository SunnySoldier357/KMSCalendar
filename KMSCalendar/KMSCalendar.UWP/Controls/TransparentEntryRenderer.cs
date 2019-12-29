using KMSCalendar.Controls;
using KMSCalendar.UWP.Controls;

using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TransparentEntry), typeof(TransparentEntryRenderer))]
namespace KMSCalendar.UWP.Controls
{
	public class TransparentEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
				Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
		}
	}
}