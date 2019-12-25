using System.Collections.Generic;

using KMSCalendar.Models.Data;
using Xamarin.Forms;

namespace KMSCalendar.Services.Data
{
    public static class AssignmentManager
    {
        //* Public Methods

        /// <summary>
        /// Inserts an assignment into the db.
        /// </summary>
        /// <param name="assignment">
        /// The assignment you want to put into the db.
        /// Must have a non-null Id, DueDate, Description, UserId, and Period
        /// </param>
        public static Assignment AddAssignment(Assignment assignment)
        {
            assignment.UserId = (Application.Current as App).SignedInUser.Id;
            assignment.SetClassId();
            assignment.SetPeriod();

            string sql = @"
                INSERT INTO dbo.Assignments (Id, DueDate, Description, Name, ClassId, UserId, Period) 
                VALUES (@Id, @DueDate, @Description, @Name, @ClassId, @UserId, @Period)";

            int result = AzureDataStore.SaveData(sql, assignment);

            return result == 1 ? assignment : null;
        }

        /// <summary>
        /// Deletes an assignment from the db.
        /// </summary>
        /// <param name="assignment">The assignment you want to delete. Must have non-null Id.</param>
        /// <returns>The number of rows deleted from the db.</returns>
        public static int DeleteAssignment(Assignment assignment)
        {
            string sql = @"DELETE FROM dbo.Assignments
                WHERE Id = @Id";

            return AzureDataStore.DeleteData(sql, assignment);
        }

        /// <summary>
        /// Returns a list of all assignments of a given class and period.
        /// </summary>
        /// <param name="class">The class you want the assignments from. Must have non-null Id and Period.</param>
        /// <returns>List of all assignments of a given class and period.</returns>
        public static List<Assignment> LoadAssignments(Class @class)
        {
            string sql = @"SELECT * FROM dbo.Assignments
                WHERE ClassID = @Id AND Period = @Period";

            return AzureDataStore.LoadDataWithParam<Assignment, Class>(sql, @class);
        }
    }
}