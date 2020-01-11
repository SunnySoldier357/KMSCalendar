using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

using Autofac;

using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Models.Settings;
using KMSCalendar.Services.Data;
using ModernXamarinCalendar;
using PropertyChanged;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	[AddINotifyPropertyChangedInterface]
	public class AssignmentsViewModel : BaseViewModel
	{
		//* Private Properties
		/// <summary>A List of all the Assignments to display.</summary>
		private List<Assignment> assignments;

		private readonly UserSettings userSettings;

		//* Public Properties
		[DoNotNotify]
		public bool ShowCalendarDays => userSettings.ShowCalendarDays;

		public DateTime DateSelected { get; set; }
		public string DateFormatted => DateSelected.ToString("dddd, MMM d");

		public ICommand AddAssignmentCommand { get; }
		public ICommand FilterAssignmentsCommand { get; }
		public ICommand GoToTodayCommand { get; }
		public ICommand GoToTomorrowCommand { get; }
		public ICommand ItemSelectedCommand { get; }
		public ICommand LoadAssignmentsCommand { get; }

		/// <summary>
		/// A filtered set of all the Assignments that are only for the
		/// current day selected
		/// </summary>
		public List<Assignment> FilteredAssignments { get; set; }

		//* Constructors
		public AssignmentsViewModel() :
			this(AppContainer.Container.Resolve<UserSettings>()) { }

		public AssignmentsViewModel(UserSettings userSettings)
		{
			this.userSettings = userSettings;

			DateSelected = DateTime.Today;

			assignments = new List<Assignment>();
			FilteredAssignments = new List<Assignment>();

			AddAssignmentCommand = new Command(() => MessagingCenter.Send(this,
				MessagingEvent.AddAssignment, DateSelected));
			FilterAssignmentsCommand = new Command<DateTime>(selectedDate =>
				filterAssignments(selectedDate));
			ItemSelectedCommand = new Command<object>(selectedItem => itemSelected(selectedItem));
			LoadAssignmentsCommand = new Command(() => loadAssignments());
			GoToTodayCommand = new Command<WeekControl>(weekControl => goToToday(weekControl));
			GoToTomorrowCommand = new Command<WeekControl>(weekControl => goToTomorrow(weekControl));

			MessagingCenter.Subscribe<NewAssignmentViewModel, Assignment>(this,
				"AddAssignment", (page, a) =>
			{
				assignments.Add(DataOperation.ConnectToBackend(AssignmentManager.AddAssignment, a));

				filterAssignments(DateSelected);
			});

			// This is so that when the class search page closes,
			// the assignment page will update it's assignment list
			MessagingCenter.Subscribe<ClassSearchViewModel>(this, "LoadAssignments",
				(sender) => loadAssignments());

			MessagingCenter.Subscribe<EnrolledClassesViewModel>(this, "LoadAssignments",
				(sender) => loadAssignments());

			App.PullEnrolledClasses();
			loadAssignments();

			userSettings.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(UserSettings.ShowCalendarDays))
					OnNotifyPropertyChanged(nameof(ShowCalendarDays));
			};
		}

		//* Private Methods
		private void filterAssignments(DateTime date)
		{
			DateSelected = date;

			var result =
				from assignment in assignments.AsParallel()
				where assignment.DueDate.Date.Equals(date.Date)
				orderby assignment.Name
				select assignment;

			FilteredAssignments = result.ToList();
		}

		private void itemSelected(object selectedItem)
		{
			if (selectedItem is Assignment assignment)
			{
				MessagingCenter.Send(this, MessagingEvent.CalendarWeekControlItemSelected,
					assignment);
			}
		}

		/// <summary>
		/// Loads Assignments from the db.
		/// </summary>
		private void loadAssignments()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				// Loads assignments from the db for each class that the user is in.
				var userAssignments = new List<Assignment>();
				if (App.SignedInUser.EnrolledClasses != null)
				{
					foreach (Class @class in App.SignedInUser.EnrolledClasses)
					{
						@class.Assignments = DataOperation.ConnectToBackend(AssignmentManager.LoadAssignments, @class);
						foreach (Assignment assignment in @class.Assignments)
							assignment.Class = @class;

						userAssignments.AddRange(@class.Assignments);
					}
				}
				assignments = userAssignments;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}

			filterAssignments(DateSelected);
		}

		public void goToToday(WeekControl weekControl)
		{
			weekControl.OverrideSelectedDate(DateTime.Today);
			DateSelected = DateTime.Today;
			filterAssignments(DateSelected);
		}

		public void goToTomorrow(WeekControl weekControl)
		{
			weekControl.OverrideSelectedDate(DateTime.Today.AddDays(1));
			DateSelected = DateTime.Today.AddDays(1);
			filterAssignments(DateSelected);
		}
	}
}