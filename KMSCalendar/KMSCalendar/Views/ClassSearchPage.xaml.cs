using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Controls;
using KMSCalendar.Models.Data;
using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClassSearchPage : ContentPage
	{
        //* Public Properties
        public ClassSearchViewModel ViewModel;

        public MainPage RootPage => Application.Current.MainPage as MainPage;

        //* Events
        public event EventHandler SelectPeriodControlLoaded;

        //* Constructors
        public ClassSearchPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = new ClassSearchViewModel();

            SelectPeriodControl.ParentPage = this;
            SelectPeriodControlLoaded += SelectPeriodControl.OnLoaded;

            // Event Handlers for the SearchBar text changing or for the SearchButton pressing.
            ClassSearchBar.TextChanged += (sender, args) => ViewModel.FilterClasses(ClassSearchBar.Text);
            ClassSearchBar.SearchButtonPressed += (sender, args) => ViewModel.FilterClasses(ClassSearchBar.Text);
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

        /// <summary>
        /// Swaps from the class search view to the period selection view.
        /// </summary>
        public void Swap()
        {
            SearchAreaStackLayout.IsVisible = !SearchAreaStackLayout.IsVisible;
            PopUpGrid.IsVisible = !PopUpGrid.IsVisible;
        }

        //* Event Handlers
        /// <summary>
        /// Invoked when the user selects a class, then shows the selectPeriodView where the user can select a period.
        /// </summary>
        private void ClassesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectedClass = ClassesListView.SelectedItem as Class;

            SelectPeriodControlLoaded.Invoke(this, new EventArgs());
            Swap();
        }
        private void GoToNewClassButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewClassPage(this));
        }
    }
}