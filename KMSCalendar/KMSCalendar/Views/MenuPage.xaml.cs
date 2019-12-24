using System.Collections.Generic;
using System.Threading.Tasks;

using KMSCalendar.Extensions;
using KMSCalendar.Models.Navigation;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
                    Source = new ThemeImageSource("calendar_blue.png", "calendar_white.png",
                        nameof(MenuPage))
                },
                new HomeMenuItem
                {
                    Id = MenuItemType.Search,
                    Title = "Add New Class",
                    Source = new ThemeImageSource("plus_blue.png", "plus_white.png",
                        nameof(MenuPage))
                },
                new HomeMenuItem
                {
                    Id = MenuItemType.EnrolledClasses,
                    Title ="My Enrolled Classes",
                    Source = new ThemeImageSource("edit_blue.png", "edit_white.png",
                        folderName: nameof(MenuPage))
                },
                new HomeMenuItem
                {
                    Id = MenuItemType.About,
                    Title ="About",
                    Source = new ThemeImageSource("book_blue.png", "book_white.png",
                        folderName: nameof(MenuPage))
                },
                new HomeMenuItem
                {
                    Id = MenuItemType.Settings,
                    Title = "Settings",
                    Source = new ThemeImageSource("settings_blue.png", "settings_white.png",
                        folderName: nameof(MenuPage))
                },
            };

            MenuListView.ItemsSource = menuItems;

            MenuListView.SelectedItem = menuItems[0];

            MenuListView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                if (((HomeMenuItem)e.SelectedItem).Id == MenuItemType.Search)
                    await GoToClassSearchPage();
                else
                {
                    int id = (int) ((HomeMenuItem) e.SelectedItem).Id;
                    await RootPage.NavigateFromMenu(id);
                }
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