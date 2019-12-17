using System;
using System.Collections.Generic;

namespace KMSCalendar.Services.Data
{
    public static class SchoolManager
    {
        public static List<string> GetSchoolName(Guid schoolId)
        {
            string sql = @"SELECT Name FROM dbo.Schools
                WHERE Id = @Id";

            return AzureDataStore.LoadDataWithGuid<string>(sql, schoolId);
        }
    }
}