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
            // TODO: only show teachers for the user's school
            string sql = @"SELECT * FROM dbo.Teachers
                WHERE SchoolId = @Id";

            return AzureDataStore.LoadDataWithGuid<Teacher>(sql, schoolId);
        }

        public static string LoadTeacherNameFromId(Guid teacherId)
        {
            string sql = @"SELECT Name FROM dbo.Teachers
                WHERE Id = @Id";

            return AzureDataStore.LoadDataWithGuid<string>(sql, teacherId)[0];
        }

        public static int PutInTeacher(Teacher teacher)
        {
            string sql = @"INSERT INTO dbo.Teachers (Id, Name, SchoolId)
                VALUES (@Id, @Name, @SchoolId)";

            return AzureDataStore.SaveData(sql, teacher);
        }
    }
}