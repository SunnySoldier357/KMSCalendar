using System;

namespace KMSCalendar.Models.Data
{
    /// <summary>A class representing a particular assignment.</summary>
    public class Assignment : TableData
    {
        //* Public Properties
        public Class Class { get; set; }

        /// <summary>The Due Date of the assignment.</summary>
        public DateTime DueDate { get; set; }

        /// <summary>The description of the assignment.</summary>
        public string Description { get; set; }
        /// <summary>The name of the assignment.</summary>
        public string Name { get; set; }
    }
}