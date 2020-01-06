using System.Windows.Input;

using KMSCalendar.Models;
using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;

using PropertyChanged;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
	[DoNotNotify]
	public class AssignmentDetailViewModel : BaseViewModel
	{
		//* Public Properties
		public Assignment Assignment { get; set; }

		public ICommand DeleteAssignmentCommand { get; }

		public string ClassDetail => string.Format("{0} (Per {1})",
			Assignment.Class.Name, Assignment.Class.Period);

		//* Constructors
		public AssignmentDetailViewModel(Assignment assignment)
		{
			Title = assignment?.Name;
			Assignment = assignment;

			DeleteAssignmentCommand = new Command(() => deleteAssignment());
		}

		//* Private Properties
		private void deleteAssignment()
		{
			DataOperation.ConnectToBackend(AssignmentManager.DeleteAssignment, Assignment);

			MessagingCenter.Send(this, MessagingEvent.GoBackToAssignmentsPage);
		}
	}
}