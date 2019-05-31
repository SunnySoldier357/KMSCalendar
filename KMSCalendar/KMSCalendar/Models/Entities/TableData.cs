using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KMSCalendar.Models.Entities
{
    public abstract class TableData
    {
        //* Public Properties

        /// <summary>The unique Id of the entity.</summary>
        public string Id { get; set; }

        public static IEnumerable Seed<T>()
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
            else
                return Enumerable.Empty<T>();
        }
    }
}