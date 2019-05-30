using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KMSCalendar.MobileAppService.Models.Entities
{
    public class CalendarDbDataContext : DbContext
    {
        //* Public Properties
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }

        //* Constructors
        public CalendarDbDataContext(DbContextOptions<CalendarDbDataContext> options)
            : base(options) { }

        //* Public Methods
        public IQueryable GetTable<T>() where T : TableData
        {
            Type type = typeof(T);

            if (type.Equals(typeof(Assignment)))
                return Assignments;
            else if (type.Equals(typeof(Class)))
                return Classes;
            else if (type.Equals(typeof(Teacher)))
                return Teachers;
            else if (type.Equals(typeof(User)))
                return Users;

            return null;
        }
    }
}