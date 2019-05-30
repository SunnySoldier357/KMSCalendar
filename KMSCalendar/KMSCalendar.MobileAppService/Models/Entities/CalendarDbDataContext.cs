using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KMSCalendar.MobileAppService.Models.Entities
{
    public class CalendarDbDataContext : DbContext
    {
        //* Public Properties
        public DbSet<Assignment> Assignments { get; set; }

        //* Constructors
        public CalendarDbDataContext(DbContextOptions<CalendarDbDataContext> options)
            : base(options) { }

        //* Public Methods
        public IQueryable GetTable<T>() where T : TableData
        {
            if (typeof(T).Equals(typeof(Assignment)))
                return Assignments;

            return null;
        }
    }
}