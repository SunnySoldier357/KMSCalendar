using System.Collections.Generic;

namespace KMSCalendar.Services.Data
{
    public static class SchoolManager
    {
        public static List<string> GetSchoolName(int schoolId)
        {
            string sql = @"SELECT Name FROM dbo.Schools
                WHERE Id = @Id";

            return SqlAccess.LoadSingularData<string>(sql, schoolId.ToString());
        }
    }
}