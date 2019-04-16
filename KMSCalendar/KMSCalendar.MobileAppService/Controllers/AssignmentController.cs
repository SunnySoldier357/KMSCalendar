using System;
using Microsoft.AspNetCore.Mvc;

using KMSCalendar.MobileAppService.Models;

namespace KMSCalendar.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    public class AssignmentController : BaseController<Assignment>
    {
        //* Constructors
        public AssignmentController()
        {
            if (items.Count == 0)
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
    }
}