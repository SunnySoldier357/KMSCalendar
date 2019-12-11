using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using KMSCalendar.Views;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KMSCalendar
{
    public partial class App : Application
    {
        //* Static Properties
        public static string AzureBackendUrl = "https://kmscalendar.azurewebsites.net";

        public static bool UseMockDataStore = false;

        //* Public Properties
        public User SignedInUser { get; set; }

        //* Constructors
        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
            {
                DependencyService.Register<MockDataStore<Assignment>>();
                //DependencyService.Register<MockDataStore<Class>>();
                DependencyService.Register<MockDataStore<User>>();
            }
            else
            {
                DependencyService.Register<AzureDataStore<Assignment>>();
                //DependencyService.Register<AzureDataStore<Class>>();
                DependencyService.Register<AzureDataStore<User>>();
            }
        }

        //* Public Methods
        public void PullEnrolledClasses()
        {
            var enrolledClassList = Services.ClassManager.LoadEnrolledClasses(SignedInUser.Id);
            SignedInUser.EnrolledClasses = enrolledClassList;
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

        //* Overridden Methods
        protected override async void OnStart()
        {
            Settings settings = Settings.DefaultInstance;
            UpdateColorResources(settings.Theme);
            settings.PropertyChanged += ThemeChanged;

            if (string.IsNullOrWhiteSpace(settings.SignedInUserId))
                MainPage = new LoginPage();
            else
            {
                try
                {
                    SignedInUser = await DependencyService.Get<IDataStore<User>>()
                        .GetItemAsync(settings.SignedInUserId);

                    // Sets signedInUser model's SchoolId what it is in the db
                    SignedInUser.SchoolId = Services.DataManagers.UserManager.LoadSchoolId(settings.SignedInUserId);


                } catch (Exception e)
                {
                    //todo: toast a message as to why it didn't work
                }

                if (SignedInUser != null)
                    MainPage = new MainPage();
                else
                    MainPage = new LoginPage();
            }
        }

        //* Event Handlers
        private void ThemeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.Theme))
                UpdateColorResources(Settings.DefaultInstance.Theme);
        }

        //* Accessors
        public int GetSchoolId()
        {
            return SignedInUser.SchoolId;
        }
    }
}