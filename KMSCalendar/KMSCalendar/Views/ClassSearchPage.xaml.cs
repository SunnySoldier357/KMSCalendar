using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;
using KMSCalendar.ViewModels;


namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassSearchPage : ContentPage
    {
        //* Private Properties
        private App app => (Application.Current as App);

        //* Public Properties
        public ClassSearchViewModel ViewModel;

        public MainPage RootPage => Application.Current.MainPage as MainPage;

        //* Constructors
        public ClassSearchPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = new ClassSearchViewModel();

            // Event Handlers for the SearchBar text changing or for the SearchButton pressing.
            ClassSearchBar.SearchButtonPressed += (sender, args) => ViewModel.FilterClasses(ClassSearchBar.Text);
            ClassSearchBar.TextChanged += (sender, args) => ViewModel.FilterClasses(ClassSearchBar.Text);
        }

        //* Public Methods

        /// <summary>
        /// This method goes to the calendar once the user has selected a class
        /// and a period
        /// </summary>
        /// <param name="periodChosen">Period that the user is in</param>
        public async Task GoToCalendarAsync(int periodChosen)
        {
            // TODO: periodChosen not used??? remove if not needed
            Class selectedClass = ViewModel.SelectedClass;
            selectedClass.UserId = app.SignedInUser.Id;

            ClassManager.EnrollUserInClass(selectedClass);

            app.PullEnrolledClasses();

            MessagingCenter.Send(this, "LoadClasses");
            MessagingCenter.Send(this, "LoadAssignments");
            MessagingCenter.Send(this, "LoadClassesForNewAssignmentPage");

            // Closes the page and goes to the last one on the stack
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
        private void AddNewPeriodButton_Clicked(object sender, EventArgs e)
        {
            int newPeriod = int.Parse(NewPeriodLabel.Text);

            if(ViewModel.AddNewPeriod(newPeriod));
                ViewModel.LoadPeriods();
        }

        private void BackButton_Clicked(object sender, EventArgs e) =>
            Swap();

        /// <summary>
        /// Invoked when the user selects a class, then shows the selectPeriodView
        /// where the user can select a period.
        /// </summary>
        private void ClassesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectedClass = ClassesListView.SelectedItem as Class;
            ViewModel.LoadPeriods();
            Swap();
        }

        private async void DoneButton_Clicked(object sender, EventArgs e)
        {
            if (PeriodsListView.SelectedItem != null)
            {
                int periodChosen = (int)PeriodsListView.SelectedItem;

                await GoToCalendarAsync(periodChosen);
            }
        }

        private void ExitButton_Clicked(object sender, EventArgs e) =>
            Navigation.PopModalAsync();

        private void GoToNewClassButton_Clicked(object sender, EventArgs e) =>
            Navigation.PushModalAsync(new NewClassPage(this));

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.SelectedClass != null)
            {
                ViewModel.LoadPeriods();
                Swap();
            }
        }
    }
}