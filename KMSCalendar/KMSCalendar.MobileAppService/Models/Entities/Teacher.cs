using System.Collections.Generic;

namespace KMSCalendar.MobileAppService.Models.Entities
{
    public class Teacher : TableData
    {
        //* Public Properties
        public List<Class> Classes { get; set; }

        public string Name { get; set; }

        //* Public Methods
        public override void Update(TableData td)
        {
            if (td is Teacher)
            {
                Teacher other = (Teacher) td;

                Classes = other.Classes;
                Name = other.Name;
            }
        }
    }
}