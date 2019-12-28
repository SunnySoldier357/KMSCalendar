using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Models.Data
{
    public class School : TableData
    {
        public string Name { get; set; }

        public string State { get; set; }  //i.e. WA

        public string ZipCode { get; set; }
    }
}
