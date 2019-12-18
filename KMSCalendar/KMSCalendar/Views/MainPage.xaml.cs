using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Navigation;
 
namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        //* Public Properties
        public Dictionary<int, NavigationPage> MenuPages =
            new Dictionary<int, NavigationPage>();

        // This is a static public property that allows downstream pages to get a handle to the MainPage instance 
        // in order to call methods that are in this class. Hide() is called from MenuPage.xaml.cs 
        public static MainPage Current;

        //* Constructor
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            // The default page to load
            MenuPages.Add((int) MenuItemType.Calendar, (NavigationPage) Detail);

            Current = this;
        }

        // Public Methods
        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int) MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;

                    case (int) MenuItemType.Calendar:
                        MenuPages.Add(id, new NavigationPage(new AssignmentsPage()));
                        break;

                    case (int) MenuItemType.Search:
                        MenuPages.Add(id, new NavigationPage(new ClassSearchPage()));
                        break;

                    case (int)MenuItemType.EnrolledClasses:
                        MenuPages.Add(id, new NavigationPage(new EnrolledClassesPage()));
                        break;

                    case (int) MenuItemType.Settings:
                        MenuPages.Add(id, new NavigationPage(new SettingsPage()));
                        break;
                }
            }

            NavigationPage newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        /// <summary>
        /// Hides the hamburger menu navigation drawer so when the user goes to the modal ClassSearchPage.xaml.cs page
        /// </summary>
        public void Hide() =>
            IsPresented = false;
    }
}