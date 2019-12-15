using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services.Data
{
    public static class ClassManager
    {
        /// <summary>
        /// Adds a class to db.Classes, and calls PutInClassPeriod() to add its period to db.Class_Periods
        /// </summary>
        /// <param name="c">The class to add to the db</param>
        /// <returns>Whether or not adding the period to the db was successful</returns>
        public static int PutInClass(Class c)
        {
            string sql = @"INSERT INTO dbo.Classes (Period, Name, TeacherId, UserId, SchoolId) OUTPUT (Inserted.Id) VALUES (@Period, @Name, @TeacherId, @UserId, @SchoolId)";

            int id = SqlAccess.SaveItemReturnId(sql, c);    //saves the new class to the db

            c.Id = id;  //sets the class id to what it is in the db

            return PeriodManager.PutInClassPeriod(c);     //adds the period to the db
        }


        /// <summary>
        /// Loads a list of all of the classes belonging to a particular school
        /// </summary>
        /// <param name="schoolId">The Id of the school the current user belongs to</param>
        /// <returns>the list of classes</returns>
        public static List<Class> LoadClasses(int schoolId)
        {
            string sql = @"SELECT * FROM dbo.Classes WHERE SchoolId = @Id";

            return SqlAccess.LoadSingularData<Class>(sql, schoolId.ToString());
        }


        public static int EnrollUserInClass(Class c)
        {
            string sql = @"INSERT INTO dbo.Class_Users (ClassId, UserId, Period) VALUES (@Id, @UserId, @Period)";

            return SqlAccess.SaveData(sql, c);
        }

        public static List<Class> LoadEnrolledClasses(string userId)
        {

            //get the ClassId's from db.Class_Users; then get all the classes with those classes. HINT: USE INNER JOIN
            string sql = @"SELECT dbo.Classes.Id, dbo.Class_Users.Period, dbo.Classes.Name, dbo.Classes.TeacherId
                                FROM dbo.Classes
                                JOIN dbo.Class_Users
                                ON dbo.Classes.Id = dbo.Class_Users.ClassId 
                                WHERE dbo.Class_Users.UserId = @Id";   

            return SqlAccess.LoadDataWithId<Class>(sql, userId);
        }


    }
}
