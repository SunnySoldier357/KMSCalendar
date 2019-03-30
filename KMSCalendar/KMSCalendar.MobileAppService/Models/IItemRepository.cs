using System.Collections.Generic;

namespace KMSCalendar.Models
{
    public interface IItemRepository
    {
        //* Methods
        void Add(Item item);
        void Update(Item item);
        Item Remove(string key);
        Item Get(string id);
        IEnumerable<Item> GetAll();
    }
}
