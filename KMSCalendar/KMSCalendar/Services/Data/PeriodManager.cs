using System;
using System.Collections.Generic;

using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Data
{
    public static class PeriodManager
    {
        //* Public Methods
        /// <summary>
        /// Returns a list of periods that a class has.
        /// </summary>
        /// <param name="classId">The Id of the class. (Guid)</param>
        /// <returns>List of periods that a class has.</returns>
        public static List<int> LoadPeriods(Guid classId)
        {
            string sql = @"SELECT (Period) FROM dbo.Class_Periods
                WHERE ClassId = (@Id)";

            return AzureDataStore.LoadDataWithGuid<int>(sql, classId);
        }

        /// <summary>
        /// Adds period to db.Class_Periods with it's respective classId
        /// </summary>
        /// <param name="@class">
        /// The class that the period belongs to. Must have Id and Period non-null.
        /// </param>
        /// <returns> Whether or not adding the period to the db was successful. </returns>
        public static int PutInClassPeriod(Class @class)
        {
            string sql = @"IF NOT EXISTS 
                (
                    SELECT 1 FROM dbo.Class_Periods
                    WHERE ClassId = @Id AND Period = @Period
                ) 
                INSERT INTO dbo.Class_Periods (ClassId, Period)
                VALUES (@Id, @Period)";

            return AzureDataStore.SaveData(sql, @class);
        }
    }
}