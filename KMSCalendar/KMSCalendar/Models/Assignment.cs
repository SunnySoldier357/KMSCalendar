using System;

namespace KMSCalendar.Models
{
    public class Assignment
    {
        //* Public Properties
        public DateTime DueDate { get; set; }

        public string Date => DueDate.ToString("MMMM dd, yyyy");
        public string Description { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortDate => DueDate.ToShortDateString();
    }
}