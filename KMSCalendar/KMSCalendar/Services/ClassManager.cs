using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services
{
    public static class ClassManager
    {
        public static int PutInClass(Class c)
        {
            string sql = @"INSERT INTO dbo.Classes (Period, Name, TeacherId, UserId, SchoolId) VALUES (@Period, @Name, @TeacherId, @UserId, @SchoolId)";
            
            return SqlAccess.SaveData(sql, c);
        }

        public static List<Class> LoadClasses(int schoolId)
        {
            string sql = @"SELECT * FROM dbo.Classes WHERE SchoolId = @Id";

            return SqlAccess.LoadSingularData<Class>(sql, schoolId.ToString());
        }
    }
}
