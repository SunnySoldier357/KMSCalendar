using System;

namespace KMSCalendar.Models.Data
{
    /// <summary>A class representing a particular assignment.</summary>
    public class Assignment
    {
        //* Public Properties
        public Class Class { get; set; }

        /// <summary>The Due Date of the assignment.</summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// The id of the class that the asignment belongs to. This is
        /// repetitive so that Dapper can put it in the db
        /// </summary>
        public Guid ClassId { get; set; }
        public Guid Id { get; set; }
        /// <summary>The period of the class that the user belongs to.
        /// This is repetitive so that Dapper can put it in the db.
        /// </summary>
        public int Period { get; set; }

        /// <summary>The description of the assignment.</summary>
        public string Description { get; set; }
        /// <summary>The name of the assignment.</summary>
        public string Name { get; set; }
        /// <summary>The user id of the assignment creator</summary>
        public string UserId { get; set; }

        //* Public Methods
        public void SetClassId() => ClassId = Class.Id;
        
        public void SetPeriod() => Period = Class.Period;
    }
}