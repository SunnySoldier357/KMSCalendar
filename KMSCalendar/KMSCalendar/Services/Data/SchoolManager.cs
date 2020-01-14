using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using KMSCalendar.Models.Data;

using Microsoft.AppCenter.Analytics;

namespace KMSCalendar.Services.Data
{
	public static class SchoolManager
	{
		//* Public Methods
		public static int AddSchool(School school)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"INSERT INTO dbo.Schools (Id, Name, ZipCode)
                VALUES (@Id, @Name, @ZipCode)";

			int output = AzureDataStore.SaveData(sql, school);

			watch.Stop();
			Analytics.TrackEvent(nameof(AddSchool), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}

		/// <summary>
		/// Returns the name of a school, given it's Id.
		/// </summary>
		/// <param name="schoolId">The Id of the school. (Guid)</param>
		/// <returns>The name of the school. (string)</returns>
		public static string GetSchoolName(Guid schoolId)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"SELECT Name FROM dbo.Schools
                WHERE Id = @Id";

			string output = AzureDataStore.LoadDataWithGuid<string>(sql, schoolId)
				.FirstOrDefault();

			watch.Stop();
			Analytics.TrackEvent(nameof(GetSchoolName), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}

		public static List<School> LoadSchools()
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"SELECT * FROM dbo.Schools";

			List<School> output = AzureDataStore.LoadData<School>(sql);

			watch.Stop();
			Analytics.TrackEvent(nameof(LoadSchools), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}
	}
}