using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using Microsoft.AppCenter.Crashes;

namespace KMSCalendar.Models.Settings
{
	public class ConnectionStringInfo
	{
		//* Public Properties
		public int? ConnectionTimeout { get; private set; }
		public int? Port { get; private set; }

		public string DatabaseName { get; private set; }
		public string Password { get; private set; }
		public string ServerHost { get; private set; }
		public string UserName { get; private set; }

		//* Static Methods
		public static ConnectionStringInfo UpdateSettings(ConnectionStringInfo @this,
			ConnectionStringInfo other)
		{
			if (AppSettings.IsInitialized)
			{
				throw new InvalidOperationException(
					$"{nameof(ConnectionStringInfo)} cannot be modified after initialization!");
			}

			if (@this == null)
				@this = new ConnectionStringInfo();

			@this.ConnectionTimeout = other.ConnectionTimeout ?? @this.ConnectionTimeout;
			@this.Port = other.Port ?? @this.Port;

			@this.DatabaseName = other.DatabaseName ?? @this.DatabaseName;
			@this.Password = other.Password ?? @this.Password;
			@this.ServerHost = other.ServerHost ?? @this.ServerHost;
			@this.UserName = other.UserName ?? @this.UserName;

			return @this;
		}

		//* Public Methods
		public string GenerateConnectionString()
		{
			try
			{
				var builder = new SqlConnectionStringBuilder()
				{
					DataSource = $"tcp:{ServerHost},{(int) Port}",
					InitialCatalog = DatabaseName,
					PersistSecurityInfo = false,
					UserID = UserName,
					Password = Password,
					MultipleActiveResultSets = false,
					Encrypt = true,
					TrustServerCertificate = false,
					ConnectTimeout = (int) ConnectionTimeout
				};

				return builder.ToString();
			}
			catch (InvalidOperationException e)
			{
				// Check which property is null
				string propertyName = GetType().GetProperties()
					.Where(p => p.GetValue(this) == null)
					.First()?
					.ToString();

				Crashes.TrackError(e, new Dictionary<string, string>
				{
					{ "PropertyName", propertyName },
					{ "Source", nameof(ConnectionStringInfo) }
				});

				throw new Exception($"The property '{ propertyName }' of ConnectionStringInfo is " +
					"null. It requires a value in the appropriate appsettings.json file.");
			}
			catch (Exception e)
			{
				Crashes.TrackError(e, new Dictionary<string, string>
				{
					{ "Source", nameof(ConnectionStringInfo) }
				});

				throw e;
			}
		}
	}
}