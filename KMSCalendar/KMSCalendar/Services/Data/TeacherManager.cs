using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using KMSCalendar.Models.Data;

using Microsoft.AppCenter.Analytics;

namespace KMSCalendar.Services.Data
{
	public static class TeacherManager
	{
		//* Public Methods

		/// <summary>
		/// Inserts a teacher into the db.
		/// </summary>
		/// <param name="teacher">
		/// The teacher to insert into the db. 
		/// Id, Name, and SchoolId must be non-null.
		/// </param>
		/// <returns>
		/// true if the teacher was inserted, false if not.
		/// </returns>
		public static bool AddTeacher(Teacher teacher)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"INSERT INTO dbo.Teachers (Id, Name, SchoolId)
                VALUES (@Id, @Name, @SchoolId)";

			bool output = AzureDataStore.SaveData(sql, teacher) == 1;

			watch.Stop();
			Analytics.TrackEvent(nameof(AddTeacher), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}

		/// <summary>
		/// Returns a list of teachers from a given school.
		/// </summary>
		/// <param name="schoolId">The id of the school. (Guid)</param>
		/// <returns>A list of teachers from a given school.</returns>
		public static List<Teacher> LoadAllTeachers(Guid schoolId)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"SELECT * FROM dbo.Teachers
                WHERE SchoolId = @Id";

			List<Teacher> output = AzureDataStore.LoadDataWithGuid<Teacher>(sql, schoolId);

			watch.Stop();
			Analytics.TrackEvent(nameof(LoadAllTeachers), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}

		/// <summary>
		/// Returns the name of a teacher, give it's id.
		/// </summary>
		/// <param name="teacherId">The Id of the Teacher. (Guid)</param>
		/// <returns>The name of the teacher. (string)</returns>
		public static string LoadTeacherNameFromId(Guid teacherId)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"SELECT Name FROM dbo.Teachers
                WHERE Id = @Id";

			string output = AzureDataStore.LoadDataWithGuid<string>(sql, teacherId)
				.FirstOrDefault();

			watch.Stop();
			Analytics.TrackEvent(nameof(LoadTeacherNameFromId), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}
	}
}