using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMSCalendar.MobileAppService.Models
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
        public string AuthenticationToken { get; set; }

    }
}
