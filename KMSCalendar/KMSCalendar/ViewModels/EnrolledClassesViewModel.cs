using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	public class EnrolledClassesViewModel : BaseViewModel
	{
		//* Private Properties
		private App app => Application.Current as App;

		private DataOperation dataOperation = new DataOperation();

		private List<Class> classes;

		//* Public Properties
		public ICommand UnsubscribeClassCommand { get; set; }

		public List<Class> Classes
		{
			get => classes;
			set => setProperty(ref classes, value);
		}

		//* Constructors
		public EnrolledClassesViewModel()
		{
			updateData();

			UnsubscribeClassCommand = new Command<Class>(async @class => await unsubscribeClassAsync(@class));

			MessagingCenter.Subscribe<ClassSearchViewModel>(this, "UpdateClasses",
				(sender) => updateData());
		}

		//* Private Methods
		private void updateData()
		{
			app.PullEnrolledTeachers();
			Classes = app.SignedInUser.EnrolledClasses;
		}

		private async Task unsubscribeClassAsync(Class @class)
		{
			@class.UserId = app.SignedInUser.Id;
			await Task.Run(() => dataOperation.ConnectToBackend(ClassManager.UnenrollUserFromClass, @class));

			app.PullEnrolledClasses();
			updateData();

			MessagingCenter.Send(this, "LoadAssignments");

			// TODO: If there are no other users in the class, delete the class
		}
	}
}