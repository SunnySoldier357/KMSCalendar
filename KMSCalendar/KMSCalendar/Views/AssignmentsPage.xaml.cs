﻿using System;
using KMSCalendar.Models;
using KMSCalendar.Models.Data;
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
		public AssignmentsPage()
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
		}
		public void DateSelectedChanged(object sender, EventArgs e)
		{
			var calendar = sender as WeekControl;

			ViewModel.FilterAssignmentsCommand.Execute(calendar.SelectedDate);
		}

		private void TodayButton_Clicked(object sender, EventArgs e)
		{
			CalendarWeekControl.OverrideSelectedDate(DateTime.Today);
		}
	}
}