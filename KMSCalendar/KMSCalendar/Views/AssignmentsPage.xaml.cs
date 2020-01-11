using System;

using Autofac;

using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Models.Settings;
using KMSCalendar.ViewModels;

using ModernXamarinCalendar;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KMSCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssignmentsPage : ContentPage
	{
		//* Constructors
		public AssignmentsPage() :
			this(AppContainer.Container.Resolve<UserSettings>()) { }

		public AssignmentsPage(UserSettings userSettings)
		{
			InitializeComponent();

			CalendarWeekControl.SelectedDateChanged += DateSelectedChanged;

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

			CalendarWeekControl.ShowDayName = userSettings.ShowCalendarDays;
			userSettings.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(UserSettings.ShowCalendarDays))
					CalendarWeekControl.ShowDayName = userSettings.ShowCalendarDays;
			};
		}
		public void DateSelectedChanged(object sender, EventArgs e)
		{
			var calendar = sender as WeekControl;

			ViewModel.FilterAssignmentsCommand.Execute(calendar.SelectedDate);
		}
	}
}