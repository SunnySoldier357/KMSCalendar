using Microsoft.AspNetCore.Mvc;

using KMSCalendar.MobileAppService.Models.Entities;

namespace KMSCalendar.MobileAppService.Controllers.API
{
    [Route("api/[controller]")]
    public class AssignmentController : BaseController<Assignment>
    {
        //* Constructors
        public AssignmentController(CalendarDbDataContext db) : base(db) { }
    }
}