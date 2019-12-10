using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services
{
    public static class AssignmentManager
    {
        public static int PutInAssignment(Assignment a)
        {
            string sql = @"INSERT INTO dbo.Assignments (DueDate, Description, Name, ClassId, UserId, Period) 
                            VALUES (@DueDate, @Description, @Name, @ClassId, @UserId, @Period)";

            return SqlAccess.SaveData(sql, a);
        }

        public static List<Assignment> LoadAssignments(int classId)
        {
            string sql = @"SELECT * FROM dbo.Assignments WHERE ClassID = @Id";

            return SqlAccess.LoadDataWithId<Assignment>(sql, classId);
        }

    }
}
