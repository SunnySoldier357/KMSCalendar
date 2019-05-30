namespace KMSCalendar.MobileAppService.Models.Entities
{
    public abstract class TableData
    {
        //* Public Properties
        public string Id { get; set; }

        //* Public Method
        public abstract void Update(TableData td);
    }
}