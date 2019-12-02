using System.Collections.Generic;

namespace KMSCalendar.Models.Data
{
    public class Teacher
    {
        //* Public Properties
        public int Id { get; set; }

        public List<Class> Classes { get; set; }

        public string Name { get; set; }

        public int SchoolId { get; set; }
    }
}