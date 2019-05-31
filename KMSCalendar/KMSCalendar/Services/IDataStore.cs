using System.Collections.Generic;
using System.Threading.Tasks;

using KMSCalendar.Models.Entities;

namespace KMSCalendar.Services
{
    public interface IDataStore<T> where T : TableData
    {
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<T> GetItemAsync(string id);
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
    }
}