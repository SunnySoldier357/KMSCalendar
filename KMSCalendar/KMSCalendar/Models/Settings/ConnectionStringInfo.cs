using System.Data.SqlClient;

namespace KMSCalendar.Models.Settings
{
    public class ConnectionStringInfo
    {
        //* Public Properties
        public int ConnectionTimeout { get; private set; }
        public int Port { get; private set; }

        public string DatabaseName { get; private set; }
        public string Password { get; private set; }
        public string ServerHost { get; private set; }
        public string UserName { get; private set; }

        //* Public Methods
        public string GenerateConnectionString()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = $"tcp:{ServerHost},{Port}",
                InitialCatalog = DatabaseName,
                PersistSecurityInfo = false,
                UserID = UserName,
                Password = Password,
                MultipleActiveResultSets = false,
                Encrypt = true,
                TrustServerCertificate = false,
                ConnectTimeout = ConnectionTimeout
            };

            return builder.ToString();
        }
    }
}