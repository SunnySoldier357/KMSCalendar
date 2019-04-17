using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Services;
using KMSCalendar.Views;
using KMSCalendar.Models;
using System.Diagnostics;

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

            MainPage = new MainPage();
        }
    }
}