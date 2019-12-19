namespace KMSCalendar.Models.Settings
{
    public class EmailInfo
    {
        //* Public Properties
        public int Port { get; private set; }

        public string DisplayName { get; private set; }
        public string Password { get; private set; }
        public string SmtpServer { get; private set; }
        public string UserName { get; private set; }
    }
}
