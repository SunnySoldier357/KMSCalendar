﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            assignments = new List<Assignment>();
        }

        //* Interface Implementations
        public async Task<IEnumerable<Assignment>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                string json = await client.GetStringAsync($"api/item");
                assignments = await Task.Run(() =>
                    JsonConvert.DeserializeObject<IEnumerable<Assignment>>(json));
            }

            return assignments;
        }

        public async Task<Assignment> GetItemAsync(string id)
        {
            if (id != null)
            {
                string json = await client.GetStringAsync($"api/item/{id}");
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

            var response = await client.PostAsync($"api/item",
                new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Assignment assignment)
        {
            if (assignment == null || assignment.Id == null)
                return false;

            string serializedItem = JsonConvert.SerializeObject(assignment);
            byte[] buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{assignment.Id}"),
                byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}