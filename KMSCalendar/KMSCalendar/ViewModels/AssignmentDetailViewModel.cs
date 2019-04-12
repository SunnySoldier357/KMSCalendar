using KMSCalendar.Models;

namespace KMSCalendar.ViewModels
{
    public class AssignmentDetailViewModel : BaseViewModel
    {
        //* Public Properties
        public Assignment Assignment { get; set; }

        //* Constructors
        public AssignmentDetailViewModel(Assignment assignment = null)
        {
            Title = assignment?.Name;
            Assignment = assignment;
        }
    }
}