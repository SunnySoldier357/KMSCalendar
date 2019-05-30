using System.Collections.Generic;

namespace KMSCalendar.MobileAppService.Models.Entities
{
    public class User : TableData
    {
        //* Public Properties
        public List<Class> EnrolledClasses { get; set; }

        public string Name { get; set; }

        //* Public Methods
        public override void Update(TableData td)
        {
            if (td is User)
            {
                User other = (User) td;

                EnrolledClasses = other.EnrolledClasses;
                Name = other.Name;
            }
        }
    }
}