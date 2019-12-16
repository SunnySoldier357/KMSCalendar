using System.Collections.Generic;

using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Data
{
    public static class PeriodManager
    {
        //* Public Methods
        public static List<int> LoadPeriods(int classId)
        {
            string sql = @"SELECT (Period) FROM dbo.Class_Periods
                WHERE ClassId = (@Id)";

            return SqlAccess.LoadDataWithId<int>(sql, classId.ToString());
        }

        /// <summary>
        /// Adds period to db.Class_Periods with it's respective classId
        /// </summary>
        /// <param name="@class">
        /// The class that the period belongs to. Must have Id and Period non-null.
        /// </param>
        /// <returns>
        /// Whether or not adding the period to the db was successful.
        /// </returns>
        public static int PutInClassPeriod(Class @class)
        {
            string sql = @"IF NOT EXISTS (SELECT 1 FROM dbo.CLass_Periods WHERE Period = @Period) INSERT INTO dbo.Class_Periods (ClassId, Period)
                VALUES (@Id, @Period)";

            return SqlAccess.SaveData<Class>(sql, @class);
        }
    }
}