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

            var result = SqlAccess.LoadDataWithId<User>(sql, email);

            return (result.Count > 0) ? true : false;
        }

        public static User LoadUserFromEmail(string email)
        {
            string sql = @"SELECT * FROM dbo.Users
                WHERE Email = @Id";

            var users = SqlAccess.LoadSingularData<User>(sql, email);

            return (users.Count == 1) ? users[0] : null;
        }

        public static User LoadUserFromId(string userId)
        {
            string sql = @"SELECT * FROM dbo.Users
                WHERE Id = @Id";

            return SqlAccess.LoadSingularData<User>(sql, userId)[0];
        }

        public static int PutInUser(User user)
        {
            string sql = @"INSERT INTO dbo.Users (Id, Email, Username, Password, SchoolId)
                VALUES (@Id, @Email, @Username, @Password, @SchoolId)";

            return SqlAccess.SaveData(sql, user);
        }
    }
}