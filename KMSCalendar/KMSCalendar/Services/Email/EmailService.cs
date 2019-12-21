using System.IO;

using MailKit.Net.Smtp;
using MailKit.Security;

using MimeKit;

using KMSCalendar.Models.Data;
using KMSCalendar.Models.Settings;

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

                client.Connect(emailInfo.SmtpServer, emailInfo.Port, SecureSocketOptions.StartTls);

                client.Authenticate(emailInfo.UserName, emailInfo.Password);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}