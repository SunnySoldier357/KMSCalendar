using System.Collections.Generic;

namespace KMSCalendar.Models.Data
{
    public class Teacher : TableData
    {
        //* Public Properties
        public List<Class> Classes { get; set; }

        public string Name { get; set; }

        //* Overridden Methods
        public override bool Equals(object obj)
        {
            if (obj is Teacher teacher)
                return Id == teacher.Id;

            return false;
        }
    }
}