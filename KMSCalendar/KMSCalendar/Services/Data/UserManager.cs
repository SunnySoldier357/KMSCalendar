using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using KMSCalendar.Models.Data;

using Microsoft.AppCenter.Analytics;

namespace KMSCalendar.Services.Data
{
	public static class UserManager
	{
		//* Public Methods

		/// <summary>
		/// Inserts a new user into the database.
		/// </summary>
		/// <param name="user">
		/// The user to insert into db.
		/// The Id, Email, Username, Password, and SchoolId must be non-null.
		/// </param>
		/// <returns>true if the user was saved, false if failed.</returns>
		public static bool AddUser(User user)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"INSERT INTO dbo.Users (Id, Email, Username, Password, SchoolId)
                VALUES (@Id, @Email, @Username, @Password, @SchoolId)";

			bool output = AzureDataStore.SaveData(sql, user) == 1;

			watch.Stop();
			Analytics.TrackEvent(nameof(AddUser), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}

		/// <summary>
		/// Checks whether an email has an account.
		/// </summary>
		/// <param name="email">The email to check in the db.s</param>
		/// <returns>Whether the email has an account. (bool)</returns>
		public static bool CheckForUser(string email)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"SELECT 1 FROM dbo.Users
                WHERE Email = @Id";

			List<User> result = AzureDataStore.LoadDataWithString<User>(sql, email);

			bool output = result.Count > 0;

			watch.Stop();
			Analytics.TrackEvent(nameof(CheckForUser), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}

		/// <summary>
		/// Returns a user from the db, given the user's email
		/// </summary>
		/// <param name="email">The email of the user. (string)</param>
		/// <returns>The user.</returns>
		public static User LoadUserFromEmail(string email)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"SELECT Id, Email, Username, Password, SchoolId FROM dbo.Users
                WHERE Email = @Id";

			List<User> users = AzureDataStore.LoadDataWithString<User>(sql, email);

			User output = users.FirstOrDefault();

			watch.Stop();
			Analytics.TrackEvent(nameof(LoadUserFromEmail), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}

		/// <summary>
		/// Returns a User object, given it's Id.
		/// </summary>
		/// <param name="userId">The id of the user. (Guid)</param>
		/// <returns>The user.</returns>
		public static User LoadUserFromId(Guid userId)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"SELECT * FROM dbo.Users
                WHERE Id = @Id";

			User output = AzureDataStore.LoadDataWithGuid<User>(sql, userId)
				.FirstOrDefault();

			watch.Stop();
			Analytics.TrackEvent(nameof(LoadUserFromId), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}

		/// <summary>
		/// Updates the user's password hash with the new one/.
		/// </summary>
		/// <param name="user">
		/// User to update in the db. Id and Password must be non-null
		/// </param>
		/// <returns>true if successful, false if failed.</returns>
		public static bool UpdateUser(User user)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"UPDATE dbo.Users
                SET Password = @Password
                WHERE Id = @Id";

			bool output = AzureDataStore.SaveData(sql, user) == 1;

			watch.Stop();
			Analytics.TrackEvent(nameof(UpdateUser), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}
	}
}