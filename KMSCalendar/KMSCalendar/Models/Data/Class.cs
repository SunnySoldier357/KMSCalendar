using System.Collections.Generic;

namespace KMSCalendar.Models.Data
{
    public class Class
    {
        //* Public Properties
        public int Id { get; set; }
        public int Period { get; set; }
        public int SchoolId { get; set; }
        public int TeacherId { get; set; }

        public List<Assignment> Assignments { get; set; }

        public string DisplayName => $"{Name} (Per {Period})";
        public string Name { get; set; }
        public string UserId { get; set; }

        public Teacher Teacher { get; set; }
    }
}