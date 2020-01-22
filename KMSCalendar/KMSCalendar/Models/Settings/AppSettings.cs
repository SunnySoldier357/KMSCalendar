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

		public AppSecrets AppSecrets { get; private set; }

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

				AppSecrets = settings.AppSecrets;
				ConnectionStringInfo = settings.ConnectionStringInfo;
				EmailInfo = settings.EmailInfo;
			}
		}

		//* Public Methods
		public static AppSettings InitSingleton()
		{
			if (!readFileAsJson(MAIN_FILE, out AppSettings mainSettings))
			{
				throw new FileNotFoundException("The appsettings.json file must be " +
					"created from the appsettings-TEMPLATE.json file!", MAIN_FILE);
			}

			if (readFileAsJson(SECONDARY_FILE, out AppSettings secSettings))
			{
				mainSettings.updateSettings(secSettings);
				secSettings.ColorPalette = mainSettings.ColorPalette;	

				if (!IsInitialized)		//if the app is already initialized, then no need to grab appSettings because sec settings already has the information!
				{
					IsInitialized = true;
					return new AppSettings(mainSettings);
				}

				return new AppSettings(secSettings);
			}

			throw new Exception($"A Singleton Instance of {nameof(AppSettings)} has already " +
				"been initialized.");
		}

		//* Private Methods

		/// <summary>
		/// Reads the file name as it is an embedded resource, meaning
		/// the file itself doesn't exist but is embedded into the current
		/// assembly.
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		/// <see cref="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/data/files?tabs=windows#loading-files-embedded-as-resources"/>
		private static bool readFileAsJson(string fileName, out AppSettings settings)
		{
			settings = null;

			var assembly = typeof(AppSettings).Assembly;
			string resourceId = $"{ assembly.GetName().Name }.{ fileName }";
			Stream stream = assembly.GetManifestResourceStream(resourceId);

			if (stream == null)
				return false;

			string json = "";
			using (var reader = new StreamReader(stream))
				json = reader.ReadToEnd();

			settings = JsonConvert.DeserializeObject<AppSettings>(json,
				jsonSerializerSettings);

			return true;
		}

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

			if(!IsInitialized)
			{
				AppSecrets = AppSecrets.UpdateSettings(AppSecrets, settings.AppSecrets);

				ConnectionStringInfo = ConnectionStringInfo.UpdateSettings(ConnectionStringInfo,
					settings.ConnectionStringInfo);
				EmailInfo = EmailInfo.UpdateSettings(EmailInfo, settings.EmailInfo);
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