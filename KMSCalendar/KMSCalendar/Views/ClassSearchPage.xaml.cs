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
            var selectedClass = ViewModel.SelectedClass;

            // TODO: MATEO add this to the sample data
            
            // TODO: SUNNY add the period selected and class selected to the database.

            //Closes the page and goes to the last one on the stack
            await Navigation.PopModalAsync();
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
        /// Invoked when the user selects a class, then shows the selectPeriodView where
        /// the user can select a period.
        /// </summary>
        private void ClassesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectedClass = ClassesListView.SelectedItem as Class;

            SelectPeriodControlLoaded.Invoke(this, new EventArgs());
            Swap();
        }

        private void GoToNewClassButton_Clicked(object sender, EventArgs e) => 
            Navigation.PushModalAsync(new NewClassPage(this));

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.SelectedClass != null)
            {
                SelectPeriodControlLoaded.Invoke(this, new EventArgs());
                Swap();
            }
        }
    }
}