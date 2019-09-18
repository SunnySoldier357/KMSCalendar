using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Email
{
    public interface IEmailService
    {
        //* Interface Methods
        void SendResetPasswordEmail(User recipient);
    }
}