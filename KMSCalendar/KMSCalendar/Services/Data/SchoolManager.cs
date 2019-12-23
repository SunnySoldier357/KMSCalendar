using System;
using System.Collections.Generic;
using System.Linq;

namespace KMSCalendar.Services.Data
{
    public static class SchoolManager
    {
        /// <summary>
        /// Returns the name of a school, given it's Id.
        /// </summary>
        /// <param name="schoolId">The Id of the school. (Guid)</param>
        /// <returns>The name of the school. (string)</returns>
        public static string GetSchoolName(Guid schoolId)
        {
            string sql = @"SELECT Name FROM dbo.Schools
                WHERE Id = @Id";

            return AzureDataStore.LoadDataWithGuid<string>(sql, schoolId).FirstOrDefault();
        }
    }
}