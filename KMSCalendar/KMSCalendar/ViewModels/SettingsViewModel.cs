using System.Windows.Input;

using Xamarin.Forms;

using KMSCalendar.Models.Settings;
using KMSCalendar.Views;
using System;

namespace KMSCalendar.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        //* Private Properties
        private App app = (Application.Current as App);

        private UserSettings settings = UserSettings.DefaultInstance;

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

        public string Email => app.SignedInUser?.Email;
        public string UserName => app.SignedInUser?.UserName;

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

            settings.SignedInUserId = Guid.Empty;
            app.SignedInUser = null;

            app.MainPage = new LoginPage();
        }
    }
}