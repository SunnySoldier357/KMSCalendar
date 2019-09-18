using Microsoft.AspNetCore.Mvc;

using KMSCalendar.MobileAppService.ViewModels;
using System.Diagnostics;

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
            return View(new ResetPasswordViewModel(authToken));
        }

        [HttpPost]
        [Route("auth/reset/{authToken}")]
        //[ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            Debug.Write("here");

            if (ModelState.IsValid)
            {
                //TODO: SUNNY verify the data

                return RedirectToAction("ResetPasswordConfirmation");
            }

            return View();
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}