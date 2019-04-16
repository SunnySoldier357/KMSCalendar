using System.Collections.Generic;

namespace KMSCalendar.MobileAppService.Models
{
    public class User : TableData
    {
        public string Name { get; set; }

        public List<Class> EnrolledClasses { get; set; }
    }
}