using System.Collections.Generic;

namespace KMSCalendar.MobileAppService.Models
{
    public class User : TableData
    {
        //* Public Properties
        public List<Class> EnrolledClasses { get; set; }

        public string Name { get; set; }
    }
}