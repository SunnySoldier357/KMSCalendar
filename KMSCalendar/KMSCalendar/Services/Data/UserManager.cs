using System;

using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Data
{
    public static class UserManager
    {
        //* Public Methods
        public static bool CheckForUser(string email)
        {
            string sql = @"SELECT 1 FROM dbo.Users
                WHERE Email = @Id";

            var result = AzureDataStore.LoadDataWithString<User>(sql, email);

            return (result.Count > 0) ? true : false;
        }

        public static User LoadUserFromEmail(string email)
        {
            string sql = @"SELECT Id, Email, Username, Password, SchoolId FROM dbo.Users
                WHERE Email = @Id";

            var users = AzureDataStore.LoadSingularData<User>(sql, email);

            return (users.Count == 1) ? users[0] : null;
        }

        public static User LoadUserFromId(Guid userId)
        {
            string sql = @"SELECT * FROM dbo.Users
                WHERE Id = @Id";

            return AzureDataStore.LoadDataWithGuid<User>(sql, userId)[0];
        }

        public static int PutInUser(User user)
        {
            string sql = @"INSERT INTO dbo.Users (Id, Email, Username, Password, SchoolId)
                VALUES (@Id, @Email, @Username, @Password, @SchoolId)";

            return AzureDataStore.SaveData(sql, user);
        }
    }
}