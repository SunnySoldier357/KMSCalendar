using Microsoft.AspNetCore.Mvc;

namespace KMSCalendar.MobileAppService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Error(int? statusCode = null) =>
            View(model: statusCode?.ToString() + " " ?? "");
    }
}