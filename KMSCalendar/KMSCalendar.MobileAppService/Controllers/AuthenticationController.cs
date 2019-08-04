using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KMSCalendar.MobileAppService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KMSCalendar.MobileAppService.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ForgotPassword()
        {
            ForgotPasswordModel forgotPassword = new ForgotPasswordModel();
            return View(forgotPassword);
        }

        //public IActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        //{
        //    return View();
        //}

    }
}
