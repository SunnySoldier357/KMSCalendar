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

		public string Email => App.SignedInUser?.Email;
		public string UserName => App.SignedInUser?.UserName;

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
			userSettings.SignedInUserId = Guid.Empty;
			App.SignedInUser = null;

			App.MainPage = new LogInPage();
		}
	}
}