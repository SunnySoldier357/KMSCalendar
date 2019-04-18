using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models;
using KMSCalendar.Services;
using KMSCalendar.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KMSCalendar
{
    public partial class App : Application
    {
        //* Static Properties
        public static string AzureBackendUrl = "https://kmscalendar.azurewebsites.net/";

        public static bool UseMockDataStore = false;

        //* Constructors
        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            Settings settings = Settings.DefaultInstance;
            UpdateColorResources(settings.Theme);
            settings.PropertyChanged += ThemeChanged;

            MainPage = new MainPage();
        }

        //* Public Methods
        public void UpdateColorResources(Theme theme)
        {
            // Light Theme => White
            // Dark Theme => Black
            Color NavigationBackground = Color.FromHex(theme == Theme.Light ? "#FFF" : "#000");

            // Dark Blue
            Color NavigationPrimary = Color.FromHex("#154360");

            // Brown
            Color NavigationSecondary = Color.FromHex("#603215");

            // Orange
            Color NavigationTertiary = Color.FromHex("#C9682C");

            // Light Theme => Light Gray
            // Dark Theme => Dark Gray
            Color LightText = Color.FromHex(theme == Theme.Light ? "#999" : "#666");

            // Light Theme => Black
            // Dark Theme => White
            Color Text = Color.FromHex(theme == Theme.Light ? "#000" : "#FFF");

            Resources[nameof(NavigationBackground)] = NavigationBackground;
            Resources[nameof(NavigationPrimary)] = NavigationPrimary;
            Resources[nameof(NavigationSecondary)] = NavigationSecondary;
            Resources[nameof(NavigationTertiary)] = NavigationTertiary;

            Resources[nameof(LightText)] = LightText;
            Resources[nameof(Text)] = Text;
        }

        //* Event Handlers
        private void ThemeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.Theme))
                UpdateColorResources(Settings.DefaultInstance.Theme);
        }
    }
}