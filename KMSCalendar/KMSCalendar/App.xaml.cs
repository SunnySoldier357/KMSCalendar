using System;
using System.ComponentModel;

using Autofac;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Data;
using KMSCalendar.Models.Settings;
using KMSCalendar.Services.Data;
using KMSCalendar.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KMSCalendar
{
    public partial class App : Application
    {
        //* Private Properties
        private UserSettings userSettings;

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
                UpdateColorResources(userSettings.Theme);
                userSettings.PropertyChanged += ThemeChanged;

                if (userSettings.SignedInUserId.Equals(Guid.Empty))
                    MainPage = new LogInPage();
                else
                {
                    try
                    {
                        SignedInUser = UserManager.LoadUserFromId(userSettings.SignedInUserId);
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
        public void PullEnrolledClasses() =>
            SignedInUser.EnrolledClasses = ClassManager.LoadEnrolledClasses(SignedInUser.Id);

        public void PullEnrolledTeachers()
        {
            foreach (Class @class in SignedInUser.EnrolledClasses)
            {
                string name = TeacherManager.LoadTeacherNameFromId(@class.TeacherId);
                @class.Teacher = new Teacher(name);
            }
        }

        public void PullSchoolName()
        {
            if(SignedInUser != null)
                SchoolName = SchoolManager.GetSchoolName(SignedInUser.SchoolId);
        }


        public void UpdateColorResources(Theme theme)
        {
            var items = new[]
            {
                // Light Theme => White
                // Dark Theme => Black
                new
                {
                    Name = "NavigationBackground",
                    Color = Color.FromHex(theme == Theme.Light ? "#FFF" : "#000")
                },

                // Light Theme => White
                // Dark Theme => Black
                new
                {
                    Name = "NavigationBackgroundLight",
                    Color = Color.FromHex(theme == Theme.Light ? "#E6E7E8" : "#191817")
                },

                // Dark Blue
                new
                {
                    Name = "NavigationPrimary",
                    Color = Color.FromHex("#154360")
                },

                // Brown
                new
                {
                    Name = "NavigationSecondary",
                    Color = Color.FromHex("#603215")
                },

                // Orange
                new
                {
                    Name = "NavigationTertiary",
                    Color = Color.FromHex("#C9682C")
                },

                // Light Theme => Light Gray
                // Dark Theme => Dark Gray
                new
                {
                    Name = "LightText",
                    Color = Color.FromHex(theme == Theme.Light ? "#999" : "#666")
                },

                // Light Theme => Black
                // Dark Theme => White
                new
                {
                    Name = "Text",
                    Color = Color.FromHex(theme == Theme.Light ? "#000" : "#FFF")
                }
            };

            foreach (var item in items)
                Resources[item.Name] = item.Color;
        }

        //* Event Handlers
        private void ThemeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserSettings.Theme))
                UpdateColorResources(userSettings.Theme);
        }
    }
}