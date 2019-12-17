using System;
using System.Collections.Generic;

using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Data
{
    public static class TeacherManager
    {
        //* Public Methods
        public static List<Teacher> LoadAllTeachers(Guid schoolId)
        {
            string sql = @"SELECT * FROM dbo.Teachers WHERE SchoolId = @Id";     //Todo: only show teachers for the user's school

            return SqlAccess.LoadDataWithGuid<Teacher>(sql, schoolId);
        }

        public static string LoadTeacherNameFromId(Guid teacherId)
        {
            string sql = @"SELECT Name FROM dbo.Teachers
                WHERE Id = @Id";

            return SqlAccess.LoadDataWithGuid<string>(sql, teacherId)[0];
        }

        public static int PutInTeacher(Teacher teacher)
        {
            string sql = @"INSERT INTO dbo.Teachers (Id, Name, SchoolId)
                VALUES (@Id, @Name, @SchoolId)";

            return SqlAccess.SaveData(sql, teacher);
        }
    }
}