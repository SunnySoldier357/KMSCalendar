using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services.DataManagers
{
    public static class UserManager
    {
        public static int LoadSchoolId(string userId)
        {
            string sql = @"SELECT SchoolId FROM dbo.Users WHERE Id = @UserId";

            return SqlAccess.LoadSingularData<int>(sql, userId)[0];
        }

    }
}
