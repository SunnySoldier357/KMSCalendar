using KMSCalendar.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		//* Constructor
		public SettingsPage()
		{
			InitializeComponent();

			UserImageSource = new ThemeImageSource("user_blue.png", "user_white.png",
				nameof(SettingsPage));

			UserIconImage.BindingContext = this;
		}

		//* Public Properties
		public ThemeImageSource UserImageSource { get; }
	}
}