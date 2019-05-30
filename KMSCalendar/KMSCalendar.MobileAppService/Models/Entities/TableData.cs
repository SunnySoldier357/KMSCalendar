using System.ComponentModel.DataAnnotations;

namespace KMSCalendar.MobileAppService.Models.Entities
{
    public abstract class TableData
    {
        //* Public Properties
        [Required]
        public string Id { get; set; }

        //* Public Method
        public abstract void Update(TableData td);
    }
}