using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

using KMSCalendar.MobileAppService.Models;

namespace KMSCalendar.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    public class AssignmentController : Controller
    {
        //* Static Properties
        private static ConcurrentDictionary<string, Assignment> assignments =
            new ConcurrentDictionary<string, Assignment>();

        //* Constructors
        public AssignmentController()
        {
            if (assignments.Count == 0)
            {
                add(new Assignment
                {
                    Name = "Item 1",
                    Description = "This is an item description.",
                    DueDate = DateTime.Today
                });
                add(new Assignment
                {
                    Name = "Item 2",
                    Description = "This is an item description.",
                    DueDate = DateTime.Today
                });
                add(new Assignment
                {
                    Name = "Item 3",
                    Description = "This is an item description.",
                    DueDate = DateTime.Today
                });
            }
        }

        //* Public Methods
        [HttpGet]
        public IActionResult List() =>
            Ok(assignments.Values);

        [HttpGet("{id}")]
        public Assignment GetItem(string id) => assignments[id];

        [HttpPost]
        public IActionResult Create([FromBody] Assignment assignment)
        {
            try
            {
                if (assignment == null || !ModelState.IsValid)
                    return BadRequest("Invalid State");

               add(assignment);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }

            return Ok(assignment);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Assignment assignment)
        {
            try
            {
                if (assignment == null || !ModelState.IsValid)
                    return BadRequest("Invalid State");

                assignments[assignment.Id] = assignment;
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Assignment assignment;
            assignments.TryRemove(id, out assignment);
        }

        //* Private Methods
        private void add(Assignment assignment)
        {
            assignment.Id = Guid.NewGuid().ToString();
            assignments[assignment.Id] = assignment;
        }
    }
}