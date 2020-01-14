using System;

namespace KMSCalendar.Models.Settings
{
	public class AppSecrets
	{
		//* Public Properties
		public string Android { get; private set; }
		public string IOS { get; private set; }

		//* Static Methods
		public static AppSecrets UpdateSettings(AppSecrets @this, AppSecrets other)
		{
			if (AppSettings.IsInitialized)
			{
				throw new InvalidOperationException(
					$"{nameof(AppSecrets)} cannot be modified after initialization!");
			}

			if (@this == null)
				@this = new AppSecrets();

			@this.Android = other.Android ?? @this.Android;
			@this.IOS = other.IOS ?? @this.IOS;

			return @this;
		}
	}
}