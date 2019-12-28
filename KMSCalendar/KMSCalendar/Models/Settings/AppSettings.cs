using System;
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
        public const string MAIN_FILE = "appsettings.json";

#if DEBUG
        public const string SECONDARY_FILE = "appsettings.Development.json";
#else
        public const string SECONDARY_FILE = "appsettings.Production.json";
#endif

        //* Static Properties
        public static bool IsInitialized = false;

        private static readonly JsonSerializerSettings jsonSerializerSettings 
            = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new SettingsReaderContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        //* Private Properties
        private string connectionString = null;

        //* Public Properties
        public bool? UseMockDataStore { get; private set; }

        public List<ColorResource> ColorPalette { get; private set; }

        public ConnectionStringInfo ConnectionStringInfo { get; private set; }

        public EmailInfo EmailInfo { get; private set; }

        public string ConnectionString
        {
            get
            {
                if (connectionString == null && ConnectionStringInfo != null)
                    connectionString = ConnectionStringInfo.GenerateConnectionString();

                return connectionString;
            }
        }

        //* Constructors
        private AppSettings() { }

        private AppSettings(AppSettings settings)
        {
            if (settings != null)
            {
                UseMockDataStore = settings.UseMockDataStore;

                ColorPalette = new List<ColorResource>(settings.ColorPalette);

                ConnectionStringInfo = settings.ConnectionStringInfo;
                EmailInfo = settings.EmailInfo;
            }
        }

        //* Public Methods
        public static AppSettings InitSingleton()
        {
            if (!IsInitialized)
            {
                if (!File.Exists(MAIN_FILE))
                    throw new FileNotFoundException("The appsettings.json file must be " +
                        "created from the appsettings-TEMPLATE.json file!", MAIN_FILE);

#if DEBUG
                if (!File.Exists(SECONDARY_FILE))
                    throw new FileNotFoundException("The appsettings.Development.json file " +
                        "must be created from the appsettings.Development-TEMPLATE.json file!", 
                        SECONDARY_FILE);
#else
                if (!File.Exists(SECONDARY_FILE))
                    throw new FileNotFoundException("The appsettings.Production.json file " +
                        "must be created from the appsettings.Production-TEMPLATE.json file!", 
                        SECONDARY_FILE);
#endif

                string mainJson = File.ReadAllText(MAIN_FILE);
                AppSettings mainSettings = JsonConvert.DeserializeObject<AppSettings>(mainJson,
                    jsonSerializerSettings);

                string secJson = File.ReadAllText(SECONDARY_FILE);
                AppSettings secSettings = JsonConvert.DeserializeObject<AppSettings>(secJson,
                    jsonSerializerSettings);

                mainSettings.updateSettings(secSettings);

                IsInitialized = true;

                return new AppSettings(mainSettings);
            }

            throw new Exception($"A Singleton Instance of {nameof(AppSettings)} has already " +
                "been initialized.");
        }

        //* Private Methods
        private void updateSettings(AppSettings settings)
        {
            UseMockDataStore = settings.UseMockDataStore ?? UseMockDataStore;

            if (settings.ColorPalette != null)
            {
                if (ColorPalette == null)
                    ColorPalette = new List<ColorResource>(settings.ColorPalette);
                else
                {
                    ColorPalette = new List<ColorResource>(
                        ColorPalette.Union(settings.ColorPalette));
                }
            }

            ConnectionStringInfo = ConnectionStringInfo.UpdateSettings(ConnectionStringInfo, 
                settings.ConnectionStringInfo);
            EmailInfo = EmailInfo.UpdateSettings(EmailInfo, settings.EmailInfo);
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