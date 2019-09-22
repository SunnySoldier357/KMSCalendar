using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using KMSCalendar.Models.Data;
using System.Diagnostics;

namespace KMSCalendar.Services.Data
{
    public class AzureDataStore<T> : IDataStore<T> where T : TableData
    {
        //* Private Properties
        private HttpClient client;

        private IEnumerable<T> items;

        //* Constructors
        public AzureDataStore()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.AzureBackendUrl}/")
            };

            items = new List<T>();
        }

        //* Interface Implementations
        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                Debug.WriteLine("woho");
                string json = await client.GetStringAsync($"api/{typeof(T).Name}");     //TODO: Fix this line it is causing frozen app / crash
                items = await Task.Run(() =>
                    JsonConvert.DeserializeObject<IEnumerable<T>>(json));
            }

            return items;
        }

        public async Task<T> GetItemAsync(string id)
        {
            if (id != null)
            {
                string json = await client.GetStringAsync($"api/{typeof(T).Name}/{id}");
                return await Task.Run(() =>
                    JsonConvert.DeserializeObject<T>(json));
            }

            return null;
        }

        public async Task<T> AddItemAsync(T item)
        {
            if (item == null)
                return null;

            string serializedItem = JsonConvert.SerializeObject(item);

            HttpResponseMessage response = await client.PostAsync($"api/{typeof(T).Name}",
                new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return await Task.Run(async () =>
                JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()));
        }

        public async Task<T> UpdateItemAsync(T item)
        {
            if (item == null || item.Id == null)
                return null;

            string serializedItem = JsonConvert.SerializeObject(item);
            byte[] buffer = Encoding.UTF8.GetBytes(serializedItem);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);

            HttpResponseMessage response = await client.PutAsync(
                new Uri($"api/{typeof(T).Name}/{item.Id}"),
                byteContent);

            return await Task.Run(async () =>
                JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()));
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            HttpResponseMessage response = await client.DeleteAsync(
                $"api/{typeof(T).Name}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}