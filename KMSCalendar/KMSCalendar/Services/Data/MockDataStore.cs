using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Data
{
    public class MockDataStore<T> : IDataStore<T> where T : TableData
    {
        //* Private Properties
        private List<T> items;

        //* Contructors
        public MockDataStore()
        {
            items = new List<T>();
            List<T> mockItems = (List<T>) TableData.Seed<T>();

            foreach (T item in mockItems)
                items.Add(item);
        }

        //* Interface Implementations
        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false) =>
            await Task.FromResult(items);

        public async Task<T> GetItemAsync(string id) =>
            await Task.FromResult(items.FirstOrDefault(i => i.Id == id));

        public async Task<T> AddItemAsync(T item)
        {
            items.Add(item);

            return await Task.FromResult(item);
        }

        public async Task<T> UpdateItemAsync(T item)
        {
            T oldItem = items
                .Where(i => i.Id == item.Id)
                .FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(item);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            T oldItem = items
                .Where(i => i.Id == id)
                .FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }
    }
}