using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KMSCalendar.Models.Entities
{
    public abstract class TableData
    {
        //* Public Properties

        /// <summary>The unique ID of the entity.</summary>
        public string Id { get; set; }

        //* Static Methods

        /// <summary>
        /// A method used to seed values if the MockDataStore is being used.
        /// </summary>
        /// <typeparam name="T">A subclass of TableData.</typeparam>
        /// <returns>An IEnumerable of the seeded instances of TableData.</returns>
        public static IEnumerable Seed<T>() where T : TableData
        {
            Type temp = typeof(T);

            if (temp.Equals(typeof(Assignment)))
            {
                return new List<Assignment>
                {
                    new Assignment
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "First item",
                        Description ="This is an item description.",
                        DueDate = DateTime.Today
                    },
                    new Assignment
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Second item",
                        Description ="This is an item description.",
                        DueDate = DateTime.Today
                    },
                    new Assignment
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Third item",
                        Description="This is an item description.",
                        DueDate = DateTime.Today
                    },
                    new Assignment
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Fourth item",
                        Description="This is an item description.",
                        DueDate = DateTime.Today
                    },
                    new Assignment
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Fifth item",
                        Description="This is an item description.",
                        DueDate = DateTime.Today
                    },
                    new Assignment
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Sixth item",
                        Description="This is an item description.",
                        DueDate = DateTime.Today
                    }
                };
            }
            else if (temp.Equals(typeof(Class)))
            {
                return new List<Class>
                {
                    new Class
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "IB Math HL 2"
                    },
                    new Class
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "IB Physics HL 2",
                        Period = 5
                    },
                    new Class
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "IB Comp Sci HL",
                        Period = 6
                    },
                    new Class
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Dystopian Fiction",
                        Period = 1
                    },
                    new Class
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Graphic Design 1",
                        Period = 3
                    },
                    new Class
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "IB Economics SL",
                        Period = 2
                    },
                };
            }

            else
                return Enumerable.Empty<T>();
        }
    }
}