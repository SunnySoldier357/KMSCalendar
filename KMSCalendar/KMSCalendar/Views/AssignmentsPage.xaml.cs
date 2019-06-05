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

            // TODO SUNNY Update WeekControl to have a Command & Command Property XAML Attribute
            CalendarWeekControl.DataSelectedChanged += (sender, args) =>
                (BindingContext as AssignmentViewModel)
                    .ExecuteFilterAssignmentsCommand(CalendarWeekControl.DateSelected);
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