using System.Windows.Input;

using Xamarin.Forms;

using KMSCalendar.Models;
using KMSCalendar.Views;

namespace KMSCalendar.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        //* Private Properties
        private Settings settings = Settings.DefaultInstance;

        //* Public Properties
        public bool IsDarkThemeEnabled
        {
            get => settings.Theme == Theme.Dark;
            set
            {
                settings.Theme = value ? Theme.Dark : Theme.Light;
                OnPropertyChanged();
            }
        }
        public bool ShowCalendarDays
        {
            get => settings.ShowCalendarDays;
            set
            {
                settings.ShowCalendarDays = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogOutCommand { get; set; }

        //* Constructors
        public SettingsViewModel()
        {
            Title = "Settings";

            LogOutCommand = new Command(() => ExecuteLogOutCommand());
        }

        //* Public Methods
        public void ExecuteLogOutCommand()
        {
            App app = Application.Current as App;

            settings.SignedInUserId = null;
            app.SignedInUser = null;

            app.MainPage = new LoginPage();
        }
    }
}