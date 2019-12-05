using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMSCalendar.Services
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

            return PutInClassPeriod(c);     //adds the period to the db
        }

        /// <summary>
        /// Adds period to db.Class_Periods with it's respective classId
        /// </summary>
        /// <param name="c">The class that the period belongs to. Must have Id and Period non-null</param>
        /// <returns>Whether or not adding the period to the db was successful</returns>
        public static int PutInClassPeriod(Class c)
        {
            string sql = @"INSERT INTO dbo.Class_Periods (ClassId, Period) VALUES (@Id, @Period)";

            return SqlAccess.SaveData<Class>(sql, c);
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
    }
}
