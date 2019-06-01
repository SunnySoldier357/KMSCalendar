using System;

namespace KMSCalendar.Models.Entities
{
    /// <summary>A class representing a particular assignment.</summary>
    public class Assignment : TableData
    {
        //* Public Properties

        /// <summary>The Due Date of the assignment.</summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// The Due Date of the assignment in the following format:
        /// Wednesday, May 16, 2001
        /// </summary>
        public string Date => DueDate.ToLongDateString();
        /// <summary>The description of the assignment.</summary>
        public string Description { get; set; }
        /// <summary>The name of the assignment.</summary>
        public string Name { get; set; }
        /// <summary>
        /// The Due Date of the assignment in the following format:
        /// 5/16/2001
        /// </summary>
        public string ShortDate => DueDate.ToShortDateString();
    }
}