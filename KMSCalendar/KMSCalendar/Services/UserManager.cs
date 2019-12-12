using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services
{
    public static class UserManager
    {
        public static int PutInUser(User user)
        {
            string sql = @"INSERT INTO dbo.Users (Id, Email, Username, Password, SchoolId) VALUES (@Id, @Email, @Username, @Password, @SchoolId)";

            return SqlAccess.SaveData(sql, user);
        }

        public static User LoadUser(string userId)
        {
            string sql = @"SELECT * FROM dbo.Users WHERE Id = @Id";

            return SqlAccess.LoadSingularData<User>(sql, userId)[0];
        }
    }
}
