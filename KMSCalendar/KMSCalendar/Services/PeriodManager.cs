using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services
{
    public static class PeriodManager
    {

        /// <summary>
        /// Adds period to db.Class_Periods with it's respective classId
        /// </summary>
        /// <param name="c">The class that the period belongs to. Must have Id and Period non-null</param>
        /// <returns>Whether or not adding the period to the db was successful</returns>
        public static int PutInClassPeriod(Class c)
        {
            string sql = @"INSERT INTO dbo.Class_Periods (ClassId, Period) VALUES (@Id, @Period)";

            return SqlAccess.SaveData<Class>(sql, c);
        }


        public static List<int> LoadPeriods(int classId)
        {
            string sql = @"SELECT (Period) FROM dbo.Class_Periods WHERE ClassId = (@Id)";

            return SqlAccess.LoadDataWithId<int>(sql, classId);
        }

    }
}
