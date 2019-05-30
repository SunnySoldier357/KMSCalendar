using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using KMSCalendar.MobileAppService.Models.Entities;

namespace KMSCalendar.MobileAppService.Controllers.API
{
    [Route("api/[controller]")]
    public abstract class BaseController<T> : Controller where T : TableData
    {
        //* Private Properties
        private readonly CalendarDbDataContext db;

        private readonly DbSet<T> items;

        //* Constructors
        public BaseController(CalendarDbDataContext db)
        {
            this.db = db;
            items = (DbSet<T>) db.GetTable<T>();
        }

        //* Public Methods
        [HttpGet]
        public async Task<IActionResult> List() =>
            Ok(await items.ToArrayAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(string id)
        {
            T item = await items.FirstOrDefaultAsync(t => t.Id == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] T item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                    return BadRequest("Invalid State");

                item.Id = Guid.NewGuid().ToString();

                items.Add(item);
                db.SaveChanges();

                return Ok(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] T item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                    return BadRequest("Invalid State");

                T find = await items.FirstOrDefaultAsync(t => t.Id == item.Id);

                if (find == null)
                    return NotFound();

                find.Update(item);

                await db.SaveChangesAsync();

                return Ok(find);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            T item = await items.FirstOrDefaultAsync(t => t.Id == id);

            if (item == null)
                return NotFound();

            items.Remove(item);
            await db.SaveChangesAsync();

            return Ok(item);
        }
    }
}