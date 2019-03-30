using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace KMSCalendar.Models
{
    public class ItemRepository : IItemRepository
    {
        //* Static Properties
        private static ConcurrentDictionary<string, Item> items =
            new ConcurrentDictionary<string, Item>();

        //* Constructors
        public ItemRepository()
        {
            Add(new Item
            {
                Id = Guid.NewGuid().ToString(),
                Text = "Item 1",
                Description = "This is an item description."
            });
            Add(new Item
            {
                Id = Guid.NewGuid().ToString(),
                Text = "Item 2",
                Description = "This is an item description."
            });
            Add(new Item
            {
                Id = Guid.NewGuid().ToString(),
                Text = "Item 3",
                Description = "This is an item description."
            });
        }

        //* Public Methods
        public Item Find(string id)
        {
            Item item;
            items.TryGetValue(id, out item);

            return item;
        }

        //* Interface Implementations
        public Item Get(string id) => items[id];

        public IEnumerable<Item> GetAll() => items.Values;

        public void Add(Item item)
        {
            item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
        }

        public Item Remove(string id)
        {
            Item item;
            items.TryRemove(id, out item);

            return item;
        }

        public void Update(Item item) =>
            items[item.Id] = item;
    }
}
