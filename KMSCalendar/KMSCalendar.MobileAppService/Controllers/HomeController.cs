using Microsoft.AspNetCore.Mvc;

namespace KMSCalendar.MobileAppService.Controllers
{
    public class HomeController : Controller
    {
        //* Public Methods
        public IActionResult Index() => View();

        [Route("{controller}/{action}/{statusCode?}")]
        public IActionResult Error(int? statusCode = null) =>
            View(model: statusCode?.ToString() + " " ?? "");
    }
}