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
        }

        //* Event Handlers
        private void CalendarDaySwitch_Toggled(object sender, ToggledEventArgs e) =>
            settings.ShowCalendarDays = CalendarDaySwitch.IsToggled;

        private void ThemeSwitch_Toggled(object sender, ToggledEventArgs e) => 
            settings.Theme = ThemeSwitch.IsToggled ? Theme.Dark : Theme.Light;
    }
}