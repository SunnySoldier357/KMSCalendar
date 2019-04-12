using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KMSCalendar.Models;

namespace KMSCalendar.Services
{
    public class MockDataStore : IDataStore<Assignment>
    {
        //* Private Properties
        private List<Assignment> assignments;

        //* Contructors
        public MockDataStore()
        {
            assignments = new List<Assignment>();
            var mockAssignments = new List<Assignment>
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

            foreach (var assignment in mockAssignments)
                assignments.Add(assignment);
        }

        //* Interface Implementations
        public async Task<bool> AddItemAsync(Assignment assignment)
        {
            assignments.Add(assignment);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Assignment assignment)
        {
            var oldItem = assignments
                .Where(a => a.Id == assignment.Id)
                .FirstOrDefault();
            assignments.Remove(oldItem);
            assignments.Add(assignment);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = assignments
                .Where(a => a.Id == id)
                .FirstOrDefault();
            assignments.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Assignment> GetItemAsync(string id) =>
            await Task.FromResult(assignments.FirstOrDefault(a => a.Id == id));

        public async Task<IEnumerable<Assignment>> GetItemsAsync(bool forceRefresh = false) =>
            await Task.FromResult(assignments);
    }
}