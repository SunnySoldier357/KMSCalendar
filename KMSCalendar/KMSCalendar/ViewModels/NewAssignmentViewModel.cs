using System;
using System.Collections.Generic;

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

		public List<Class> SubscribedClasses
		{
			get => subscribedClasses;
			set => setProperty(ref subscribedClasses, value);
		}

		//* Constructors
		public NewAssignmentViewModel(DateTime dateTime)
		{
			Title = "New Assignment";

			Assignment = new Assignment
			{
				Name = "",
				Description = "",
				DueDate = dateTime
			};

			LoadSubscribedClasses();
		}

		public void LoadSubscribedClasses() =>
			SubscribedClasses = App.SignedInUser.EnrolledClasses;
	}
}