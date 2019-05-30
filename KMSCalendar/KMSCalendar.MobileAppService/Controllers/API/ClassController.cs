using KMSCalendar.MobileAppService.Models.Entities;

namespace KMSCalendar.MobileAppService.Controllers.API
{
    public class ClassController : BaseController<Class>
    {
        //* Constructors
        public ClassController(CalendarDbDataContext db) : base(db) { }
    }
}