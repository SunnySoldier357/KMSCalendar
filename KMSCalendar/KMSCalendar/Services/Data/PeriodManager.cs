using System;
using System.Collections.Generic;
using System.Diagnostics;

using KMSCalendar.Models.Data;

using Microsoft.AppCenter.Analytics;

namespace KMSCalendar.Services.Data
{
	public static class PeriodManager
	{
		//* Public Methods

		/// <summary>
		/// Adds period to db.Class_Periods with it's respective classId
		/// </summary>
		/// <param name="@class">
		/// The class that the period belongs to. Must have Id and Period non-null.
		/// </param>
		/// <returns>
		/// Whether or not adding the period to the db was successful.
		/// </returns>
		public static bool AddPeriod(Class @class)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"
                IF NOT EXISTS 
                (
                    SELECT 1 FROM dbo.Class_Periods
                    WHERE ClassId = @Id AND Period = @Period
                ) 
                INSERT INTO dbo.Class_Periods (ClassId, Period)
                VALUES (@Id, @Period)";

			bool output = AzureDataStore.SaveData(sql, @class) == 1;

			watch.Stop();
			Analytics.TrackEvent(nameof(AddPeriod), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}

		/// <summary>
		/// Returns a list of periods that a class has.
		/// </summary>
		/// <param name="classId">The Id of the class. (Guid)</param>
		/// <returns>List of periods that a class has.</returns>
		public static List<int> LoadPeriods(Guid classId)
		{
			Stopwatch watch = Stopwatch.StartNew();

			string sql = @"SELECT (Period) FROM dbo.Class_Periods
                WHERE ClassId = (@Id)";

			List<int> output = AzureDataStore.LoadDataWithGuid<int>(sql, classId);

			watch.Stop();
			Analytics.TrackEvent(nameof(LoadPeriods), new Dictionary<string, string>
			{
				{ "ElapsedTime", $"{ watch.ElapsedMilliseconds } ms" }
			});

			return output;
		}
	}
}