using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

using KMSCalendar.MobileAppService.Models;

namespace KMSCalendar.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController<T> : Controller where T : TableData
    {
        //* Static Properties
        protected static ConcurrentDictionary<string, T> items =
            new ConcurrentDictionary<string, T>();

        //* Protected Methods
        protected void add(T item)
        {
            item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
        }

        //* Public Methods
        [HttpGet]
        public IActionResult List() =>
            Ok(items.Values);

        [HttpGet("{id}")]
        public T GetItem(string id) => items[id];

        [HttpPost]
        public IActionResult Create([FromBody] T item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                    return BadRequest("Invalid State");

                add(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }

            return Ok(item);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] T item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                    return BadRequest("Invalid State");

                items[item.Id] = item;
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id) => items.TryRemove(id, out T item);
    }
}