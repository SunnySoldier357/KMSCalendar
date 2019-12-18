﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KMSCalendar.Models.Settings
{
    public class AppSettings
    {
        //* Constanst
        public const string CONFIG_FILE = "appsettings.json";

        //* Static Properties
        private static readonly JsonSerializerSettings JsonSerializerSettings 
            = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new SettingsReaderContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        //* Private Properties
        private bool isInitialized = false;

        private string connectionString = null;

        //* Public Properties
        public bool UseMockDataStore { get; private set; }

        public ConnectionStringInfo ConnectionStringInfo { get; private set; }

        public string AzureBackendUrl { get; private set; }
        public string ConnectionString
        {
            get
            {
                if (connectionString == null && ConnectionStringInfo != null)
                    connectionString = ConnectionStringInfo.GenerateConnectionString();

                return connectionString;
            }
        }

        //* Public Methods
        public void Initialize()
        {
            if (!isInitialized)
            {
                if (!File.Exists(CONFIG_FILE))
                    throw new FileNotFoundException("The appsettings.json file must be " +
                        "created from the appsettings-TEMPLATE.json file!", CONFIG_FILE);

                string json = File.ReadAllText(CONFIG_FILE);

                AppSettings settings = JsonConvert.DeserializeObject<AppSettings>(json,
                    JsonSerializerSettings);

                updateInstance(settings);

                isInitialized = true;
            }
        }

        //* Private Methods
        private void updateInstance(AppSettings settings)
        {
            if (settings != null)
            {
                UseMockDataStore = settings.UseMockDataStore;

                AzureBackendUrl = settings.AzureBackendUrl;
                ConnectionStringInfo = settings.ConnectionStringInfo;
            }
        }

        //* Private Class

        /// <summary>
        /// Class enables AppSettings' properties to be assigned to as they have
        /// a private modifier on their setter. This also is required to prevent
        /// any external code changing the app settings.
        /// </summary>
        private class SettingsReaderContractResolver : DefaultContractResolver
        {
            //* Overriden Methods
            protected override IList<JsonProperty> CreateProperties(Type type,
                MemberSerialization memberSerialization)
            {
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(p => CreateProperty(p, memberSerialization))
                    .ToList();

                properties.ForEach(p =>
                {
                    p.Writable = true;
                    p.Readable = true;
                });

                return properties;
            }
        }
    }
}