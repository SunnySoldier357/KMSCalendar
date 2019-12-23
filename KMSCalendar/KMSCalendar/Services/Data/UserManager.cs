using System;
using System.Linq;
using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Data
{
    public static class UserManager
    {
        //* Public Methods
        /// <summary>
        /// Checks whether an email has an account.
        /// </summary>
        /// <param name="email">The email to check in the db.s</param>
        /// <returns>Whether the email has an account. (bool)</returns>
        public static bool CheckForUser(string email)
        {
            string sql = @"SELECT 1 FROM dbo.Users
                WHERE Email = @Id";

            var result = AzureDataStore.LoadDataWithString<User>(sql, email);

            return (result.Count > 0) ? true : false;
        }

        /// <summary>
        /// Returns a user from the db, given the user's email
        /// </summary>
        /// <param name="email">The email of the user. (string)</param>
        /// <returns>The user.</returns>
        public static User LoadUserFromEmail(string email)
        {
            string sql = @"SELECT Id, Email, Username, Password, SchoolId FROM dbo.Users
                WHERE Email = @Id";

            var users = AzureDataStore.LoadDataWithString<User>(sql, email);

            return (users.Count == 1) ? users[0] : null;
        }

        /// <summary>
        /// Returns a User object, given it's Id.
        /// </summary>
        /// <param name="userId">The id of the user. (Guid)</param>
        /// <returns>The user.</returns>
        public static User LoadUserFromId(Guid userId)
        {
            string sql = @"SELECT * FROM dbo.Users
                WHERE Id = @Id";

            return AzureDataStore.LoadDataWithGuid<User>(sql, userId).FirstOrDefault();
        }

        /// <summary>
        /// Inserts a new user into the database.
        /// </summary>
        /// <param name="user">
        /// The user to insert into db.
        /// The Id, Email, Username, Password, and SchoolId must be non-null.
        /// </param>
        /// <returns>1 if the user was saved, 0 if failed.</returns>
        public static int PutInUser(User user)
        {
            string sql = @"INSERT INTO dbo.Users (Id, Email, Username, Password, SchoolId)
                VALUES (@Id, @Email, @Username, @Password, @SchoolId)";

            return AzureDataStore.SaveData(sql, user);
        }

        /// <summary>
        /// Updates the user's password hash with the new one/.
        /// </summary>
        /// <param name="user">User to update in the db. Id and Password must be non-null</param>
        /// <returns> 1 if successful, 0 if failed.</returns>
        public static int UpdateUser(User user)
        {
            string sql = "UPDATE dbo.Users SET Password = @Password WHERE Id = @Id";

            return AzureDataStore.SaveData<User>(sql, user);
        }
    }
}