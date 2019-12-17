using System.Collections.Generic;

using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Data
{
    public static class AssignmentManager
    {
        //* Public Methods
        public static List<Assignment> LoadAssignments(Class @class)
        {
            string sql = @"SELECT * FROM dbo.Assignments
                WHERE ClassID = @Id AND Period = @Period";

            return AzureDataStore.LoadDataWithParam<Assignment, Class>(sql, @class);
        }

        public static int PutInAssignment(Assignment assignment)
        {
            string sql = @"INSERT INTO dbo.Assignments (Id, DueDate, Description, Name, ClassId, UserId, Period) 
                VALUES (@Id, @DueDate, @Description, @Name, @ClassId, @UserId, @Period)";

            return AzureDataStore.SaveData(sql, assignment);
        }

        public static int RemoveAssignment(Assignment assignment)
        {
            string sql = @"DELETE FROM dbo.Assignments
                WHERE Id = @Id";

            return AzureDataStore.DeleteData(sql, assignment);
        }
    }
}