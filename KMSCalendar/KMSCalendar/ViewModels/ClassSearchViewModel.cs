using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

using KMSCalendar.Extensions;
using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;

using PropertyChanged;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	[AddINotifyPropertyChangedInterface]
	public class ClassSearchViewModel : BaseViewModel
	{
		//* Private Properties
		private List<Class> classes;

		private UIState currentUIState { get; set; } = UIState.ClassSearchView;

		//* Public Properties
		public bool ClassSearchViewVisibility => currentUIState == UIState.ClassSearchView;
		public bool PeriodSelectViewVisiblity => currentUIState == UIState.PeriodSelectView;

		public Class SelectedClass { get; set; }

		public ICommand AddPeriodCommand { get; }
		public ICommand FilterClassesCommand { get; }
		public ICommand GoBackwardCommand { get; }
		public ICommand GoToNewClassCommand { get; }
		public ICommand ShowPeriodsCommand { get; }
		public ICommand SubscribeUserToClassCommand { get; }

		public List<Class> FilteredClasses { get; set; }

		public List<int> FilteredPeriods { get; set; }

		public ThemeImageSource SearchImageSource { get; }

		//* Constructors
		public ClassSearchViewModel()
		{
			AddPeriodCommand = new Command<int?>(period => addPeriod(period));
			FilterClassesCommand = new Command<string>(searchInput =>
				filterClasses(searchInput));
			GoBackwardCommand = new Command(() => goBackward());
			GoToNewClassCommand = new Command(() =>
				MessagingCenter.Send(this, MessagingEvent.GoToNewClassPage));
			ShowPeriodsCommand = new Command<Class>(@class => showPeriods(@class));
			SubscribeUserToClassCommand = new Command<int>(period =>
				subscribeUserToClass(period));

			SearchImageSource = new ThemeImageSource("search_blue.png", "search_white.png",
				"Shared");

			// This is so that when the new class page closes,
			// the class list will update
			MessagingCenter.Subscribe<NewClassViewModel>(this, "LoadClasses",
				(sender) => loadClasses());

			loadClasses();
		}

		//* Private Methods
		private void addPeriod(int? newPeriod)
		{
			if (newPeriod is int period && !FilteredPeriods.Contains(period))
			{
				// Add the period to the db
				SelectedClass.Period = period;
				if (DataOperation.ConnectToBackend(PeriodManager.AddPeriod, SelectedClass))
					loadPeriods();
			}
		}

		private void filterClasses(string searchInput)
		{
			if (string.IsNullOrWhiteSpace(searchInput))
				FilteredClasses = new List<Class>(classes);
			else
			{
				var result =
					from @class in classes.AsParallel()
					where @class.Name.ToLower().Contains(searchInput.ToLower())
					select @class;

				FilteredClasses = result.ToList();
			}
		}

		private void filterPeriods(List<int> periods)
		{
			var result =
				from period in periods.AsParallel()
				let userPeriods =
					from @class in App.SignedInUser.EnrolledClasses
					where @class.Id == SelectedClass.Id
					select @class.Period
				where !userPeriods.Contains(period)
				orderby period
				select period;

			FilteredPeriods = result.ToList();
		}

		private void goBackward()
		{
			if (--currentUIState < 0)
				MessagingCenter.Send(this, MessagingEvent.GoBackToCalendar);
		}

		/// <summary>
		/// Loads a list of classes from the DB so the user can add one to their account.
		/// </summary>
		private void loadClasses()
		{
			// TODO: MATEO get this to work so a user doesn't have duplicate classes.
			List<Class> classList = DataOperation.ConnectToBackend(ClassManager.LoadClasses, App.SignedInUser.SchoolId) ??
				new List<Class>();

			foreach (Class @class in classList)
			{
				string name = DataOperation.ConnectToBackend(TeacherManager.LoadTeacherNameFromId, @class.TeacherId);
				@class.Teacher = new Teacher(name);
			}

			classes = classList;
			FilteredClasses = classes;
		}

		private void loadPeriods()
		{
			Guid classId = SelectedClass.Id;

			List<int> periods = DataOperation.ConnectToBackend(PeriodManager.LoadPeriods, classId) ??
				new List<int>();
			filterPeriods(periods);
		}

		private void showPeriods(Class @class)
		{
			SelectedClass = @class;
			loadPeriods();

			currentUIState++;

			MessagingCenter.Send(this, MessagingEvent.ClassesListViewDeselectItem);
		}

		private void subscribeUserToClass(int period)
		{
			SelectedClass.UserId = App.SignedInUser.Id;
			SelectedClass.Period = period;

			DataOperation.ConnectToBackend(ClassManager.EnrollUserInClass, SelectedClass);

			App.PullEnrolledClasses();

			MessagingCenter.Send(this, "LoadAssignments");
			MessagingCenter.Send(this, "LoadClassesForNewAssignmentPage");
			MessagingCenter.Send(this, "UpdateClasses");

			MessagingCenter.Send(this, MessagingEvent.GoBackToCalendar);
		}

		//* Private Enumerations
		private enum UIState
		{
			ClassSearchView,
			PeriodSelectView
		}
	}
}