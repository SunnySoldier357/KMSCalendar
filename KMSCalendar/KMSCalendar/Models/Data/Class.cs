using System.Collections.Generic;

namespace KMSCalendar.Models.Data
{
    public class Class : TableData
    {
        //* Public Properties
        public int Period { get; set; }

        public List<Assignment> Assignments { get; set; }

        public string DisplayName => $"{Name} (Per {Period})";
        public string Name { get; set; }
        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public string UserId { get; set; }
        public int SchoolId { get; set; }
    }
}