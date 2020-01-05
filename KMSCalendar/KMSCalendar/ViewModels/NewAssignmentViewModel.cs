using System;
using System.Collections.Generic;
using System.Windows.Input;

using KMSCalendar.Models;
using KMSCalendar.Models.Data;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	public class NewAssignmentViewModel : BaseViewModel
	{
		//* Private Properties
		private Assignment assignment;

		private List<Class> subscribedClasses;

		//* Public Properties
		public Assignment Assignment
		{
			get => assignment;
			set => setProperty(ref assignment, value);
		}

		public ICommand CancelAssignmentCommand { get; }
		public ICommand GoToClassSearchCommand { get; }
		public ICommand SaveAssignmentCommand { get; }

		public List<Class> SubscribedClasses
		{
			get => subscribedClasses;
			set => setProperty(ref subscribedClasses, value);
		}

		//* Constructors
		public NewAssignmentViewModel(DateTime dateTime)
		{
			Assignment = new Assignment
			{
				Name = "",
				Description = "",
				DueDate = dateTime
			};

			CancelAssignmentCommand = new Command(() =>
				MessagingCenter.Send(this, MessagingEvent.GoBack));
			GoToClassSearchCommand = new Command(() =>
				MessagingCenter.Send(this, MessagingEvent.GoToClassSearchPage));
			SaveAssignmentCommand = new Command<object>(
				selectedItem => saveAssignment(selectedItem));

			MessagingCenter.Subscribe<ClassSearchViewModel>(this, "LoadClassesForNewAssignmentPage",
				(sender) => loadSubscribedClasses());

			loadSubscribedClasses();
		}

		//* Private Methods
		private void loadSubscribedClasses() =>
			SubscribedClasses = App.SignedInUser.EnrolledClasses;

		private void saveAssignment(object selectedItem)
		{
			if (selectedItem != null && Assignment.Name != "" &&
				Assignment.Description != null)
			{
				// Sets the viewModel's assignment to the class selected from the picker
				Assignment.Class = selectedItem as Class;

				MessagingCenter.Send(this, "AddAssignment", Assignment);
				MessagingCenter.Send(this, MessagingEvent.GoBack);
			}
		}
	}
}