using System;
using System.Collections.Generic;

namespace KMSCalendar.Models.Data
{
    public class Teacher : TableData
    {
        //* Public Properties
        public Guid SchoolId { get; set; }
        public string SchoolName { get; set; }

        public List<Class> Classes { get; set; }

        public string Name { get; set; }

        //* Constructors
        public Teacher() { }

        public Teacher(string name) => Name = name;
    }
}