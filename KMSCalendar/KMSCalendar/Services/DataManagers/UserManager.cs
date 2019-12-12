using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services.DataManagers
{
    public static class UserManager
    {
        public static int LoadSchoolId(string id)
        {
            string sql = @"SELECT SchoolId FROM dbo.Users WHERE Id = @Id";

            return SqlAccess.LoadSingularData<int>(sql, id)[0];
        }

    }
}
