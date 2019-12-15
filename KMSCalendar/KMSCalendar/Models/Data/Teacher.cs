using System.Collections.Generic;

namespace KMSCalendar.Models.Data
{
    public class Teacher
    {
        //* Public Properties
        public int Id { get; set; }
        public int SchoolId { get; set; }

        public List<Class> Classes { get; set; }

        public string Name { get; set; }

        //* Constructors
        public Teacher() { }

        public Teacher(string name) => Name = name;
    }
}