using Microsoft.AspNetCore.Mvc;

using KMSCalendar.MobileAppService.ViewModels;

namespace KMSCalendar.MobileAppService.Controllers
{
    public class AuthenticationController : Controller
    {
        //* Public Methods
        [Route("auth")]
        public IActionResult Index() => View();

        [Route("auth/reset/{authToken}")]
        public IActionResult ResetPassword(string authToken)
        {
            var viewModel = new ResetPasswordViewModel(authToken);

            return View(viewModel);
        }
    }
}