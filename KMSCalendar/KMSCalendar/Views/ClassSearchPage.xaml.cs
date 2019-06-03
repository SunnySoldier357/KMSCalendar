using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Controls;
using KMSCalendar.Models.Entities;
using KMSCalendar.Services;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClassSearchPage : ContentPage
	{
        //* Private Properties
        private List<Class> classes;

        //* Public Properties
        public MainPage RootPage => Application.Current.MainPage as MainPage;

        //* Constructors
        public ClassSearchPage()
        {
            InitializeComponent();

            SelectPeriodControl.ParentPage = this;
            PopUpGrid.IsVisible = false;

            loadListAsync();

            // Event Handlers for the SearchBar text changing or for the SearchButton pressing.
            ClassSearchBar.TextChanged += (sender, args) => filterContactsAsync(ClassSearchBar.Text);
            ClassSearchBar.SearchButtonPressed += (sender, args) => filterContactsAsync(ClassSearchBar.Text);
        }

        //* Public Methods

        public async Task GoToCalendarAsync(int periodChosen)
        {
            // TODO: SUNNY add the period selected and class selected to the database.

            // TODO: MATEO get the selected class

            var x = ClassesListView.SelectedItem;

            // TODO: MATEO fix the navigation mess!

            //    Clears all of the pages on the navigation stack
            // var existingPages = Navigation.NavigationStack.ToList();
            // foreach (var page in existingPages)
            // {
            //     Navigation.RemovePage(page);
            // }

            // var MyAppsFirstPage = new AssignmentsPage();
            // Application.Current.MainPage = new NavigationPage(MyAppsFirstPage);

            // await Application.Current.MainPage.Navigation.PushAsync(new AssignmentsPage());
            // await Application.Current.MainPage.Navigation.PopAsync();

            // This navigates to the instance of the calendar page through the menu
            await RootPage.NavigateFromMenu(1);
        }

        public void Swap()
        {
            SearchAreaStackLayout.IsVisible = !SearchAreaStackLayout.IsVisible;
            PopUpGrid.IsVisible = !PopUpGrid.IsVisible;
        }

        //* Private Methods

        /// <summary>
        /// Loads the list with data before the user searches anything.
        /// </summary>
        private async Task loadListAsync()
        {
            var data = DependencyService.Get<IDataStore<Class>>();
            var tempClassList = await data.GetItemsAsync();
            classes = tempClassList.ToList();
            ClassesListView.ItemsSource = classes;

            // _lstContacts = modDatabase.GetContacts(string.Empty); 
            // lvAddressBook.ItemsSource = _lstContacts;
        }

        /// <summary>
        /// Filters the ListView below the SearchBar based off of what the user types in.
        /// </summary>
        /// <param name="filter">The search term that the user enters.</param>
        private async Task filterContactsAsync(string filter)
        {
            if (filter == "hide")
                Swap();

            // lvAddressBook.BeginRefresh();

            if (string.IsNullOrWhiteSpace(filter))
            {
                ClassesListView.ItemsSource = classes;
                // lvAddressBook.ItemsSource = _lstContacts;
            }
            else
            {
                // Filter the data list for only items that contain the search term.
                ClassesListView.ItemsSource = classes.Where(
                    c => c.Name.ToLower().Contains(filter.ToLower()));
            }

            ClassesListView.EndRefresh();
        }

        //* Event Handlers
        private void ClassesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) =>
            Swap();
    }
}