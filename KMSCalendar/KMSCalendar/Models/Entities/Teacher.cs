﻿using System.Collections.Generic;

namespace KMSCalendar.Models.Entities
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
                return Name == teacher.Name &&
                    Id == teacher.Id;

            return false;
        }
    }
}