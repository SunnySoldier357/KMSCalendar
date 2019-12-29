using System;

using KMSCalendar.Models.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssignmentsPage : ContentPage
	{
		//* Constructors
		public AssignmentsPage() => InitializeComponent();

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