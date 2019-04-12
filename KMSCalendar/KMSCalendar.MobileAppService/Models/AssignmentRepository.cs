using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace KMSCalendar.MobileAppService.Models
{
    public class AssignmentRepository : IAssignmentRepository
    {
        //* Static Properties
        private static ConcurrentDictionary<string, Assignment> assignments =
            new ConcurrentDictionary<string, Assignment>();
        
        //* Constructors
        public AssignmentRepository()
        {
            Add(new Assignment
            {
                Id = Guid.NewGuid().ToString(),
                DueDate = DateTime.Today,
                Name = "Item 1",
                Description = "This is an item description."
            });
            Add(new Assignment
            {
                Id = Guid.NewGuid().ToString(),
                DueDate = DateTime.Today,
                Name = "Item 2",
                Description = "This is an item description."
            });
            Add(new Assignment
            {
                Id = Guid.NewGuid().ToString(),
                DueDate = DateTime.Today,
                Name = "Item 3",
                Description = "This is an item description."
            });
        }

        //* Public Methods
        public Assignment Find(string id)
        {
            Assignment assignment;
            assignments.TryGetValue(id, out assignment);

            return assignment;
        }

        //* Interface Implementations
        public Assignment Get(string id) => assignments[id];

        public IEnumerable<Assignment> GetAll() => assignments.Values;

        public void Add(Assignment assignment)
        {
            assignment.Id = Guid.NewGuid().ToString();
            assignments[assignment.Id] = assignment;
        }

        public Assignment Remove(string key)
        {
            Assignment assignment;
            assignments.TryRemove(key, out assignment);

            return assignment;
        }

        public void Update(Assignment assignment) =>
            assignments[assignment.Id] = assignment;
    }
}