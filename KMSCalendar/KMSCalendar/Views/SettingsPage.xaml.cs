using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        //* Private Properties
        public Settings settings = Settings.DefaultInstance;

        //* Constructor
        public SettingsPage()
        {
            InitializeComponent();

            ThemeSwitch.IsToggled = settings.Theme == Theme.Dark;
            CalendarDaySwitch.IsToggled = settings.ShowCalendarDays;

            BindingContext = (Application.Current as App).SignedInUser;
        }

        //* Event Handlers
        private void CalendarDaySwitch_Toggled(object sender, ToggledEventArgs e) =>
            settings.ShowCalendarDays = CalendarDaySwitch.IsToggled;

        private void LogOutButton_Clicked(object sender, System.EventArgs e)
        {
            App app = Application.Current as App;

            settings.SignedInUserId = null;
            app.SignedInUser = null;

            app.MainPage = new LoginPage();
        }

        private void ThemeSwitch_Toggled(object sender, ToggledEventArgs e) => 
            settings.Theme = ThemeSwitch.IsToggled ? Theme.Dark : Theme.Light;
    }
}