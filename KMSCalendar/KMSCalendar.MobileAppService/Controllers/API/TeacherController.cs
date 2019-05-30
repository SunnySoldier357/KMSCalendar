using KMSCalendar.MobileAppService.Models.Entities;

namespace KMSCalendar.MobileAppService.Controllers.API
{
    public class TeacherController : BaseController<Teacher>
    {
        //* Constructors
        public TeacherController(CalendarDbDataContext db) : base(db) { }
    }
}