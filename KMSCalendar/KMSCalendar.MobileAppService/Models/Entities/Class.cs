using System.Collections.Generic;

namespace KMSCalendar.MobileAppService.Models.Entities
{
    public class Class : TableData
    {
        //* Public Properties
        public int Period { get; set; }

        public List<Assignment> Assignments { get; set; }

        public string Name { get; set; }

        public Teacher Teacher { get; set; }

        //* Overridden Methods
        public override void Update(TableData td)
        {
            if (td is Class)
            {
                Class other = (Class) td;

                Period = other.Period;
                Assignments = other.Assignments;
                Name = other.Name;
                Teacher = other.Teacher;
            }
        }
    }
}