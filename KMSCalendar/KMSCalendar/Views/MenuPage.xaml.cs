using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Navigation;
using System.Threading.Tasks;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        //* Public Properties
        public MainPage RootPage => Application.Current.MainPage as MainPage;

        //* Private Properties
        private List<HomeMenuItem> menuItems;

        //* Constructors
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem
                {
                    Id = MenuItemType.Calendar,
                    Title = "Calendar",
                    Icon = "calendar.png"
                },
                new HomeMenuItem
                {
                    Id = MenuItemType.Search,
                    Title = "Classes",
                    Icon = "search.png"
                },
                new HomeMenuItem
                {
                    Id = MenuItemType.About,
                    Title ="About",
                    Icon = "file.png"
                },
                new HomeMenuItem
                {
                    Id = MenuItemType.Settings,
                    Title = "Settings",
                    Icon = "gear.png"
                },
            };

            MenuListView.ItemsSource = menuItems;

            MenuListView.SelectedItem = menuItems[0];

            MenuListView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                int id = (int) ((HomeMenuItem) e.SelectedItem).Id;

                if (((HomeMenuItem)e.SelectedItem).Id == MenuItemType.Search)
                    await GoToClassSearchPage();
                else
                    await RootPage.NavigateFromMenu(id);
            };

            UserNameLabel.BindingContext = (Application.Current as App).SignedInUser.UserName;
        }

        private async Task GoToClassSearchPage()
        {
            await Navigation.PushModalAsync(new ClassSearchPage());

            MenuListView.SelectedItem = menuItems[0];

            // Hides the hamburger menu navigation drawer.
            MainPage.Current.Hide();
        }
    }
}