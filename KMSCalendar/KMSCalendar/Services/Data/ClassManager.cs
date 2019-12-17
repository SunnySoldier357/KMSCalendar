using System;
using System.Collections.Generic;

using KMSCalendar.Models.Data;

namespace KMSCalendar.Services.Data
{
    public static class ClassManager
    {
        //* Public Methods

        public static int EnrollUserInClass(Class @class)
        {
            string sql = @"INSERT INTO dbo.Class_Users (ClassId, UserId, Period)
                VALUES (@Id, @UserId, @Period)";

            return AzureDataStore.SaveData(sql, @class);
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

        public static List<Class> LoadEnrolledClasses(Guid userId)
        {
            // Get the ClassId's from db.Class_Users; then get all the classes
            // with those classes. HINT: USE INNER JOIN
            string sql = @"SELECT dbo.Classes.Id, dbo.Class_Users.Period, dbo.Classes.Name, dbo.Classes.TeacherId
                FROM dbo.Classes
                JOIN dbo.Class_Users
                ON dbo.Classes.Id = dbo.Class_Users.ClassId 
                WHERE dbo.Class_Users.UserId = @Id";

            return AzureDataStore.LoadDataWithGuid<Class>(sql, userId);
        }

        /// <summary>
        /// Adds a class to db.Classes, and calls PutInClassPeriod() to add
        /// its period to db.Class_Periods.
        /// </summary>
        /// <param name="@class">The class to add to the db.</param>
        /// <returns>
        /// Whether or not adding the period to the db was successful.
        /// </returns>
        public static int PutInClass(Class @class)
        {
            string sql = @"INSERT INTO dbo.Classes (Id, Period, Name, TeacherId, UserId, SchoolId)
                VALUES (@Id, @Period, @Name, @TeacherId, @UserId, @SchoolId)";

            // Saves the new class to the db
            var rowsAffected = AzureDataStore.SaveData(sql, @class);

            return PeriodManager.PutInClassPeriod(@class);
        }
    }
}