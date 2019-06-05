using System;
using System.Collections.Generic;

using KMSCalendar.Models.Entities;

namespace KMSCalendar.ViewModels
{
    public class NewAssignmentViewModel : BaseViewModel
    {
        //* Private Properties
        private Assignment assignment;

        private List<Class> subscribedClasses;

        //* Public Properties
        public Assignment Assignment
        {
            get => assignment;
            set => setProperty(ref assignment, value);
        }

        public List<Class> SubscribedClasses
        {
            get => subscribedClasses;
            set => setProperty(ref subscribedClasses, value);
        }

        //* Constructors
        public NewAssignmentViewModel(DateTime dateTime)
        {
            Title = "New Assignment";

            Assignment = new Assignment
            {
                Name = "Assignment name",
                Description = "This is an item description",
                DueDate = dateTime
            };

            SubscribedClasses = new List<Class>
            {
                 new Class
                 {
                     Name = "Dystopian Fiction",
                     Period = 1
                 },
                new Class
                {
                    Name = "Econ",
                    Period = 2
                },
                new Class
                {
                    Name = "Graphic Design",
                    Period = 3
                },
                new Class
                {
                    Name = "Physics",
                    Period = 4
                },
                new Class
                {
                    Name = "Math",
                    Period = 5
                },
                new Class
                {
                    Name = "Comp Sci",
                    Period = 6
                }
            };
        }
    }
}