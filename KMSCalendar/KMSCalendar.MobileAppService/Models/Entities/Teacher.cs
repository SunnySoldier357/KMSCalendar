using System.Collections.Generic;

namespace KMSCalendar.MobileAppService.Models.Entities
{
    public class Teacher : TableData
    {
        //* Public Properties
        public List<Class> Classes { get; set; }

        public string Name { get; set; }
    }
}