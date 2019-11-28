using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;


namespace KMSCalendar.Services
{
    public static class TeacherManager
    {
        public static int PutInTeacher(Teacher teacher)
        {
            string sql = @"INSERT INTO dbo.Teachers (Name, SchoolId) VALUES (@Name, @SchoolId)";

            return SqlAccess.SaveData(sql, teacher);
        }

        public static List<Teacher> LoadAllTeachers()
        {
            string sql = @"SELECT * FROM dbo.Teachers";

            return SqlAccess.LoadData<Teacher>(sql);
        }

    }
}
