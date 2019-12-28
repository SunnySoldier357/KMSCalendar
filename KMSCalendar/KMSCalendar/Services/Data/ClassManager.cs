using System;
using System.Collections.Generic;

using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Data
{
    public static class ClassManager
    {
        //* Public Methods

        /// <summary>
        /// Adds a class to db.Classes, and calls PutInClassPeriod() to add
        /// its period to db.Class_Periods.
        /// </summary>
        /// <param name="@class">The class to add to the db.</param>
        public static bool AddClass(Class @class)
        {
            string sql = @"
                INSERT INTO dbo.Classes (Id, Period, Name, TeacherId, UserId, SchoolId)
                VALUES (@Id, @Period, @Name, @TeacherId, @UserId, @SchoolId)";

            // Saves the new class to the db
            var rowsAffected = AzureDataStore.SaveData(sql, @class);

            return PeriodManager.AddPeriod(@class);
        }

        /// <summary>
        /// Enrolls a user into a class via inserting the class, user, and period
        /// into dbo.Class_Users. The user will not be enrolled into the same class
        /// and period, but can be enrolled into a different period.
        /// </summary>
        /// <param name="class">
        /// The class the user wants to join. Id, UserId, Period, cannot be null
        /// </param>
        /// <returns>
        /// true if the user is enrolled, false if he is not enrolled.
        /// </returns>
        public static bool EnrollUserInClass(Class @class)
        {
            string sql = @"
                IF NOT EXISTS 
                (
                    SELECT 1 FROM dbo.Class_Users
                    WHERE ClassId = @Id AND UserId = @UserId AND Period = @Period
                ) 
                INSERT INTO dbo.Class_Users (ClassId, UserId, Period)
                VALUES (@Id, @UserId, @Period)";

            return AzureDataStore.SaveData(sql, @class) == 1;
        }

        /// <summary>
        /// Loads a list of all of the classes belonging to a particular school.
        /// </summary>
        /// <param name="schoolId">
        /// The Id of the school the current user belongs to.
        /// </param>
        /// <returns>The list of classes.</returns>
        public static List<Class> LoadClasses(Guid schoolId)
        {
            string sql = @"SELECT * FROM dbo.Classes
                WHERE SchoolId = @Id";

            return AzureDataStore.LoadDataWithGuid<Class>(sql, schoolId);
        }

        /// <summary>
        /// Returns a list of classes that a user is enrolled in, given the
        /// user's Id.
        /// </summary>
        /// <param name="userId">The user's Id (Guid)</param>
        /// <returns>List of classes that the user is enrolled in.</returns>
        public static List<Class> LoadEnrolledClasses(Guid userId)
        {
            string sql = @"
                SELECT dbo.Classes.Id, dbo.Class_Users.Period, dbo.Classes.Name, dbo.Classes.TeacherId
                FROM dbo.Classes
                JOIN dbo.Class_Users
                ON dbo.Classes.Id = dbo.Class_Users.ClassId 
                WHERE dbo.Class_Users.UserId = @Id";

            return AzureDataStore.LoadDataWithGuid<Class>(sql, userId);
        }

        /// <summary>
        /// Removes a user from enrollment in a class.
        /// </summary>
        /// <param name="class">
        /// The class you wich the user to unenroll from.
        /// Id, UserID, and Period must be non-null.
        /// </param>
        /// <returns>The number of rows deleted from the db.</returns>
        public static int UnenrollUserFromClass(Class @class)
        {
            string sql = @"DELETE FROM dbo.Class_Users 
                WHERE ClassId = @Id AND UserId = @UserId AND Period = @Period";

            return AzureDataStore.DeleteData(sql, @class);
        }
    }
}