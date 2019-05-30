namespace KMSCalendar.MobileAppService.Models.Environment
{
    public class ConnectionStringConfiguration
    {
        //* Public Properties
        public int Port { get; set; }

        public string ConnectionString
        {
            get
            {
                if (Startup.isDevelopment)
                {
                    return $"Server=tcp:{ServerHost},{Port};Initial Catalog={DatabaseName};" +
                        $"Persist Security Info=False;User ID={UserName};Password={Password};" +
                        "MultipleActiveResultSets=False;Encrypt=True;" +
                        "TrustServerCertificate=False;Connection Timeout=30;";
                }
                else
                    return AzureConnectionString;
            }
        }
        public string DatabaseName { get; set; }
        public string AzureConnectionString { get; set; }
        public string Password { get; set; }
        public string ServerHost { get; set; }
        public string UserName { get; set; }
    }
}