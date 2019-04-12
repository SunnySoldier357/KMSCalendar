using System;
using Microsoft.AspNetCore.Mvc;

using KMSCalendar.MobileAppService.Models;

namespace KMSCalendar.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    public class AssignmentController : Controller
    {
        //* Private Properties
        private readonly IAssignmentRepository assignmentRepository;

        //* Constructors
        public AssignmentController(IAssignmentRepository assignmentRepository) =>
            this.assignmentRepository = assignmentRepository;

        //* Public Properties
        [HttpGet]
        public IActionResult List() =>
            Ok(assignmentRepository.GetAll());

        [HttpGet("{id}")]
        public Assignment GetItem(string id) => assignmentRepository.Get(id);

        [HttpPost]
        public IActionResult Create([FromBody] Assignment assignment)
        {
            try
            {
                if (assignment == null || !ModelState.IsValid)
                    return BadRequest("Invalid State");

                assignmentRepository.Add(assignment);
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

                assignmentRepository.Update(assignment);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id) => assignmentRepository.Remove(id);
    }
}