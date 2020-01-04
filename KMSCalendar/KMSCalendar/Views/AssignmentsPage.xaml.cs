using System;

using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssignmentsPage : ContentPage
	{
		//* Constructors
		public AssignmentsPage()
		{
			InitializeComponent();

			MessagingCenter.Subscribe<AssignmentsViewModel, DateTime>(this,
				MessagingEvent.AddAssignment,
				async (sender, dateSelected) => await Navigation.PushModalAsync(new NavigationPage(
					new NewAssignmentPage(dateSelected))));

			MessagingCenter.Subscribe<AssignmentsViewModel, Assignment>(this,
				MessagingEvent.CalendarWeekControlItemSelected,
				async (sender, assignment) =>
				{
					await Navigation.PushAsync(new AssignmentDetailPage(assignment));

					// Manually deselect item.
					AssignmentsListView.SelectedItem = null;
				});
		}
	}
}