using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models;
 
namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        //* Public Properties
        public Dictionary<int, NavigationPage> MenuPages =
            new Dictionary<int, NavigationPage>();

        //* Constructor
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            // The default page to load
            MenuPages.Add((int) MenuItemType.Calendar, (NavigationPage) Detail);
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

                    case (int) MenuItemType.ClassSearch:
                        MenuPages.Add(id, new NavigationPage(new ClassSearchPage()));
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
    }
}