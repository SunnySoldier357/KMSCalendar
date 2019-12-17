using System;
using System.Collections.Generic;

namespace KMSCalendar.Models.Data
{
    public class Class
    {
        //* Public Properties
        public Guid Id { get; set; }
        public int Period { get; set; }
        public Guid SchoolId { get; set; }
        public Guid TeacherId { get; set; }

        public List<Assignment> Assignments { get; set; }

        public string DisplayName => $"{Name} (Per {Period})";
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public Teacher Teacher { get; set; }
    }
}