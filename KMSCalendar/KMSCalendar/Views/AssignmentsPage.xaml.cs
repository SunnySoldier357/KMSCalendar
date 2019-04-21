using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Controls;
using KMSCalendar.Models;
using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentsPage : ContentPage
    {
        //* Private Properties
        private AssignmentViewModel viewModel;
        
        //* Constructors
        public AssignmentsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AssignmentViewModel();

            // Subscrible to the event
            CalendarWeekControl.DataSelectedChanged += DateSelectedChanged;

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
            await Navigation.PushModalAsync(new NavigationPage(new NewAssignmentPage()));
        
        public void DateSelectedChanged(object sender, EventArgs e)
        {
            WeekControl control = sender as WeekControl;
            viewModel.FilterAssignments(control.DateSelected);
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Assignment assignment = e.SelectedItem as Assignment;
            if (assignment == null)
                return;

            await Navigation.PushAsync(new AssignmentDetailPage(
                new AssignmentDetailViewModel(assignment)));

            // Manually deselect item.
            AssignmentsListView.SelectedItem = null;
        }
    }
}