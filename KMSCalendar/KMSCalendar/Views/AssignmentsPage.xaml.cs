using ModernXamarinCalendar;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models;
using KMSCalendar.Models.Entities;
using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentsPage : ContentPage
    {
        //* Private Properties
        private AssignmentViewModel viewModel;

        public Settings settings = Settings.DefaultInstance;

        //* Constructors
        public AssignmentsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AssignmentViewModel();

            CalendarWeekControl.BindingContext = settings;

            // Subscribe to the event
            CalendarWeekControl.DataSelectedChanged += DateSelectedChanged; //dateselectedchanged is the name of the event

            ListTitleDate.BindingContext = CalendarWeekControl;
        }

        //* Overridden Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Assignments.Count == 0)
                viewModel.LoadAssignmentsCommand.Execute(null);
        }

        //* Event Handlers
        public async void AddAssignment_Clicked(object sender, EventArgs e) =>
            await Navigation.PushModalAsync(new NavigationPage(
                new NewAssignmentPage(CalendarWeekControl.DateSelected)));

        public void DateSelectedChanged(object sender, EventArgs e)
        {
            WeekControl control = sender as WeekControl;
            viewModel.FilterAssignments(control.DateSelected);
        }
        
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is Assignment assignment))
                return;

            await Navigation.PushAsync(new AssignmentDetailPage(
                new AssignmentDetailViewModel(assignment)));

            // Manually deselect item.
            AssignmentsListView.SelectedItem = null;
        }
    }
}