using System.Collections.Generic;

namespace KMSCalendar.MobileAppService.Models.Entities
{
    public class Class : TableData
    {
        //* Public Properties
        public int Period { get; set; }

        public List<Assignment> Assignments { get; set; }

        public string Name { get; set; }

        public Teacher teacher { get; set; }
    }
}