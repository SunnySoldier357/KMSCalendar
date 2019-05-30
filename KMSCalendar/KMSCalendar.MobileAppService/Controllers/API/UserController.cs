using KMSCalendar.MobileAppService.Models.Entities;

namespace KMSCalendar.MobileAppService.Controllers.API
{
    public class UserController : BaseController<User>
    {
        //* Constructors */
        public UserController(CalendarDbDataContext db) : base(db) { }
    }
}