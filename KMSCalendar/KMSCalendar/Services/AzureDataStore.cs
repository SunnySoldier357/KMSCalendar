using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using KMSCalendar.Models;

namespace KMSCalendar.Services
{
    public class AzureDataStore : IDataStore<Assignment>
    {
        //* Private Properties
        private HttpClient client;

        private IEnumerable<Assignment> assignments;

        //* Constructors
        public AzureDataStore()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.AzureBackendUrl}/")
            };

            assignments = new List<Assignment>();
        }

        //* Interface Implementations
        public async Task<IEnumerable<Assignment>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                string json = await client.GetStringAsync($"api/{nameof(Assignment)}");
                assignments = await Task.Run(() =>
                    JsonConvert.DeserializeObject<IEnumerable<Assignment>>(json));
            }

            return assignments;
        }

        public async Task<Assignment> GetItemAsync(string id)
        {
            if (id != null)
            {
                string json = await client.GetStringAsync($"api/{nameof(Assignment)}/{id}");
                return await Task.Run(() =>
                    JsonConvert.DeserializeObject<Assignment>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Assignment assignment)
        {
            if (assignment == null)
                return false;

            string serializedItem = JsonConvert.SerializeObject(assignment);

            HttpResponseMessage response = await client.PostAsync($"api/{nameof(Assignment)}",
                new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Assignment assignment)
        {
            if (assignment == null || assignment.Id == null)
                return false;

            string serializedItem = JsonConvert.SerializeObject(assignment);
            byte[] buffer = Encoding.UTF8.GetBytes(serializedItem);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);

            HttpResponseMessage response = await client.PutAsync(new Uri($"api/{nameof(Assignment)}/{assignment.Id}"),
                byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            HttpResponseMessage response = await client.DeleteAsync($"api/{nameof(Assignment)}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}