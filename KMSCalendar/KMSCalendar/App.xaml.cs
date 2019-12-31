using System;
using System.Collections.Generic;
using System.ComponentModel;

using Autofac;

using KMSCalendar.Models.Data;
using KMSCalendar.Models.Settings;
using KMSCalendar.Services.Data;
using KMSCalendar.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KMSCalendar
{
	public partial class App : Application
	{
		//* Private Properties
		private readonly AppSettings appSettings;
		private DataOperation dataOperation = new DataOperation();
		private readonly UserSettings userSettings;

		//* Public Properties
		public User SignedInUser { get; set; }
		public string SchoolName { get; set; }

		//* Constructors
		public App(AppSetup setup = null)
		{
			InitializeComponent();

			if (setup == null)
				setup = new AppSetup();

			AppContainer.Container = setup.CreateContainer();

			using (var scope = AppContainer.Container.BeginLifetimeScope())
			{
				userSettings = scope.Resolve<UserSettings>();
				appSettings = scope.Resolve<AppSettings>();

				UpdateColorResources();
				userSettings.PropertyChanged += ThemeChanged;

				if (userSettings.SignedInUserId.Equals(Guid.Empty))
					MainPage = new LogInPage();
				else
				{
					try
					{
						SignedInUser = dataOperation.ConnectToBackend(UserManager.LoadUserFromId, userSettings.SignedInUserId);
					}
					catch (Exception)
					{
						// TODO: Toast a message as to why it didn't work
					}

					if (SignedInUser != null)
						MainPage = new MainPage();
					else
						MainPage = new LogInPage();
				}
			}
		}

		//* Public Methods
		public void PullEnrolledClasses()
		{
			SignedInUser.EnrolledClasses = dataOperation.ConnectToBackend(
				ClassManager.LoadEnrolledClasses, SignedInUser.Id) ??
				new List<Class>();
		}

		public void PullEnrolledTeachers()
		{
			foreach (Class @class in SignedInUser.EnrolledClasses)
			{
				string name = dataOperation.ConnectToBackend(TeacherManager.LoadTeacherNameFromId, @class.TeacherId);
				@class.Teacher = new Teacher(name);
			}
		}

		public void PullSchoolName()
		{
			if (SignedInUser != null)
				SchoolName = dataOperation.ConnectToBackend(SchoolManager.GetSchoolName, SignedInUser.SchoolId);
		}

		public void UpdateColorResources()
		{
			foreach (ColorResource item in appSettings.ColorPalette)
				Resources[item.Name] = item.CurrentThemeColor;
		}

		//* Event Handlers
		private void ThemeChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(UserSettings.Theme))
				UpdateColorResources();
		}
	}
}