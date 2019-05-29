using System;

namespace KMSCalendar.MobileAppService.Models.Entities
{
    public class Assignment : TableData
    {
        //* Public Properties
        public DateTime DueDate { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }
    }
}