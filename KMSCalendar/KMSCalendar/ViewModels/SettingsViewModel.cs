using System;
using System.Windows.Input;

using Autofac;

using KMSCalendar.Models.Settings;
using KMSCalendar.Views;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        //* Private Properties
        private App app = (Application.Current as App);

        private readonly UserSettings userSettings;

        //* Public Properties
        public bool IsDarkThemeEnabled
        {
            get => userSettings.Theme == Theme.Dark;
            set
            {
                userSettings.Theme = value ? Theme.Dark : Theme.Light;
                OnPropertyChanged();
            }
        }
        public bool ShowCalendarDays
        {
            get => userSettings.ShowCalendarDays;
            set
            {
                userSettings.ShowCalendarDays = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogOutCommand { get; set; }

        public string Email => app.SignedInUser?.Email;
        public string UserName => app.SignedInUser?.UserName;

        //* Constructors
        public SettingsViewModel() :
            this(AppContainer.Container.Resolve<UserSettings>()) { }

        public SettingsViewModel(UserSettings userSettings)
        {
            Title = "Settings";
            this.userSettings = userSettings;

            LogOutCommand = new Command(() => ExecuteLogOutCommand());
        }

        //* Public Methods
        public void ExecuteLogOutCommand()
        {
            App app = Application.Current as App;

            userSettings.SignedInUserId = Guid.Empty;
            app.SignedInUser = null;

            app.MainPage = new LoginPage();
        }
    }
}