using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using KMSCalendar.Models.Data;
using KMSCalendar.ViewModels;

namespace KMSCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentsPage : ContentPage
    {
        //* Constructors
        public AssignmentsPage()
        {
            InitializeComponent();

            // Initializes a list of all of the user's classes in the SignedInUser's object
            (Application.Current as App).PullEnrolledClasses();

            // TODO: SUNNY Update WeekControl to have a Command & Command Property XAML Attribute
            EventHandler eventHandler = (sender, args) =>
            {
                (BindingContext as AssignmentsViewModel)
                    .ExecuteFilterAssignmentsCommand(CalendarWeekControl.DateSelected);

                (BindingContext as AssignmentsViewModel)
                    .DateChoosen = CalendarWeekControl.DateSelected;
            };
            CalendarWeekControl.DataSelectedChanged += eventHandler;
        }

        //* Event Handlers
        public async void AddAssignment_Clicked(object sender, EventArgs e) =>
            await Navigation.PushModalAsync(new NavigationPage(
                new NewAssignmentPage(CalendarWeekControl.DateSelected)));
        
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Assignment assignment)
            {
                await Navigation.PushAsync(new AssignmentDetailPage(assignment));

                // Manually deselect item.
                AssignmentsListView.SelectedItem = null;
            }
        }
    }
}