using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services
{
    public class Connections
    {
        private static string ConnectionString = "<Insert Connection String Here>";

        public static string GetConnectionString() => ConnectionString;
    }
}
