﻿using System.Collections.Generic;

using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Data
{
    public static class TeacherManager
    {
        //* Public Methods
        public static List<Teacher> LoadAllTeachers()
        {
            string sql = @"SELECT * FROM dbo.Teachers";

            return SqlAccess.LoadData<Teacher>(sql);
        }

        public static List<int> LoadId()
        {
            string sql = @"SELECT * FROM dbo.Teachers
                WHERE Id = SCOPE_IDENTITY()";

            return SqlAccess.LoadData<int>(sql);
        }

        public static string LoadTeacherNameFromId(int teacherId)
        {
            string sql = @"SELECT Name FROM dbo.Teachers
                WHERE Id = @Id";

            return SqlAccess.LoadSingularData<string>(sql, teacherId.ToString())[0];
        }

        public static int PutInTeacher(Teacher teacher)
        {
            string sql = @"INSERT INTO dbo.Teachers (Name, SchoolId)
                OUTPUT (Inserted.Id) 
                VALUES (@Name, @SchoolId)";

            return SqlAccess.SaveItemReturnId(sql, teacher);
        }
    }
}