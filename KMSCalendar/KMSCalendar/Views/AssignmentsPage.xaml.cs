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

        public Settings settings = Settings.DefaultInstance;

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
                await Navigation.PushAsync(new AssignmentDetailPage(
                    new AssignmentDetailViewModel(assignment)));

                // Manually deselect item.
                AssignmentsListView.SelectedItem = null;
            }
        }
    }
}