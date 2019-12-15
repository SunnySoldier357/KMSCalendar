using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using KMSCalendar.Models.Data;
using KMSCalendar.Services.Data;

namespace KMSCalendar.ViewModels
{
    public class AssignmentDetailViewModel : BaseViewModel
    {
        //* Public Properties
        public Assignment Assignment { get; set; }

        public ICommand DeleteAssignmentCommand { get; set; }

        public string ClassDetail => string.Format("{0} (Per {1})",
            Assignment.Class.Name, Assignment.Class.Period);

        //* Constructors
        public AssignmentDetailViewModel(Assignment assignment)
        {
            Title = assignment?.Name;
            Assignment = assignment;

            DeleteAssignmentCommand = new Command(async () =>
                await ExecuteDeleteAssignmentCommandAssignment());
        }

        //* Private Methods
        public async Task ExecuteDeleteAssignmentCommandAssignment()
        {
            AssignmentManager.RemoveAssignment(Assignment);
        }
    }
}