﻿using System.Collections.Generic;
using System.Linq;

namespace KMSCalendar.Models.Entities
{
    public class User : TableData
    {
        //* Public Properties
        public List<Class> EnrolledClasses { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
    }
}