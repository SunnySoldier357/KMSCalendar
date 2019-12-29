using System;
using System.Globalization;
using System.IO;

using KMSCalendar.Models.Data;
using KMSCalendar.Models.Settings;

using MailKit.Net.Smtp;
using MailKit.Security;

using MimeKit;

namespace KMSCalendar.Services.Email
{
	public interface IEmailService
	{
		//* Interface Methods
		void SendResetPasswordEmail(User recipient, string token);
	}

	public class EmailService : IEmailService
	{
		//* Static Properties
		private static string htmlFile = Path.Combine("Services", "Email",
			"ResetPasswordPage.html");

		//* Private Properties
		private readonly EmailInfo emailInfo;

		//* Constructors
		public EmailService(AppSettings appSettings) => emailInfo = appSettings.EmailInfo;

		//* Public Methods
		public void SendResetPasswordEmail(User recipient, string token)
		{
			string html = File.ReadAllText(htmlFile);

			// Replace variables in html with values
			html = html.Replace("{{token}}", token)
				.Replace("{{currentDate}}", DateTime.Today.ToString("d MMMM yyyy",
					DateTimeFormatInfo.InvariantInfo))
				.Replace("{{currentTime}}", DateTime.Now.ToString("h:mm tt",
					DateTimeFormatInfo.InvariantInfo))
				.Replace("{{currentYear}}", $"{DateTime.Today.Year}");

			var message = new MimeMessage()
			{
				Subject = "KMSCalendar Reset Password",
				Body = new TextPart(MimeKit.Text.TextFormat.Html)
				{
					Text = html
				}
			};

			message.From.Add(new MailboxAddress(emailInfo.DisplayName, emailInfo.UserName));
			message.To.Add(new MailboxAddress(recipient.UserName, recipient.Email));

			using (var client = new SmtpClient())
			{
				client.ServerCertificateValidationCallback = (s, c, h, e) => true;

				client.Connect(emailInfo.SmtpServer, (int) emailInfo.Port, SecureSocketOptions.StartTls);

				client.Authenticate(emailInfo.UserName, emailInfo.Password);

				client.Send(message);
				client.Disconnect(true);
			}
		}
	}
}