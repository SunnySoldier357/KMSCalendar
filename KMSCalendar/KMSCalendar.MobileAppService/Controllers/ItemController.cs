using System;
using Microsoft.AspNetCore.Mvc;

using KMSCalendar.Models;

namespace KMSCalendar.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        //* Private Properties
        private readonly IItemRepository ItemRepository;

        //* Constructors
        public ItemController(IItemRepository itemRepository)
        {
            ItemRepository = itemRepository;
        }

        //* Public Properties
        [HttpGet]
        public IActionResult List() =>
            Ok(ItemRepository.GetAll());

        [HttpGet("{id}")]
        public Item GetItem(string id) => ItemRepository.Get(id);

        [HttpPost]
        public IActionResult Create([FromBody] Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                    return BadRequest("Invalid State");

                ItemRepository.Add(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }

            return Ok(item);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                    return BadRequest("Invalid State");

                ItemRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id) => ItemRepository.Remove(id);
    }
}
