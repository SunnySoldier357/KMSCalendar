using System;

namespace KMSCalendar.Models.Settings
{
	public class EmailInfo
	{
		//* Public Properties
		public int? Port { get; private set; }

		public string DisplayName { get; private set; }
		public string Password { get; private set; }
		public string SmtpServer { get; private set; }
		public string UserName { get; private set; }

		//* Static Methods
		public static EmailInfo UpdateSettings(EmailInfo @this, EmailInfo other)
		{
			if (AppSettings.IsInitialized)
			{
				throw new InvalidOperationException(
					$"{nameof(EmailInfo)} cannot be modified after initialization!");
			}

			if (@this == null)
				@this = new EmailInfo();

			@this.Port = other.Port ?? @this.Port;

			@this.DisplayName = other.DisplayName ?? @this.DisplayName;
			@this.Password = other.Password ?? @this.Password;
			@this.SmtpServer = other.SmtpServer ?? @this.SmtpServer;
			@this.UserName = other.UserName ?? @this.UserName;

			return @this;
		}
	}
}
