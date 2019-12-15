using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services.Data
{
    public static class AssignmentManager
    {
        public static int PutInAssignment(Assignment a)
        {
            string sql = @"INSERT INTO dbo.Assignments (DueDate, Description, Name, ClassId, UserId, Period) 
                            VALUES (@DueDate, @Description, @Name, @ClassId, @UserId, @Period)";

            return SqlAccess.SaveData(sql, a);
        }

        public static List<Assignment> LoadAssignments(Class c)
        {
            string sql = @"SELECT * FROM dbo.Assignments WHERE ClassID = @Id AND Period = @Period";

            return SqlAccess.LoadDataWithParam<Assignment, Class>(sql, c);
        }

        public static int RemoveAssignment(Assignment a)
        {
            string sql = @"DELETE FROM dbo.Assignments WHERE Id = @Id";

            return SqlAccess.DeleteData(sql, a);
        }

    }
}
