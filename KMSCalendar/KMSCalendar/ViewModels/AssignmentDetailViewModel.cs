using System.Windows.Input;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;

using Xamarin.Forms;

namespace KMSCalendar.ViewModels
{
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

			DeleteAssignmentCommand = new Command(() =>
				AssignmentManager.DeleteAssignment(Assignment));
		}
	}
}