using KMSCalendar.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KMSCalendar.Services.Data
{
    public static class SchoolManager
    {
        public static List<School> LoadSchools()
        {
            string sql = @"SELECT * FROM dbo.Schools";

            return AzureDataStore.LoadData<School>(sql);
        }

        /// <summary>
        /// Returns the name of a school, given it's Id.
        /// </summary>
        /// <param name="schoolId">The Id of the school. (Guid)</param>
        /// <returns>The name of the school. (string)</returns>
        public static string GetSchoolName(Guid schoolId)
        {
            string sql = @"SELECT Name FROM dbo.Schools
                WHERE Id = @Id";

            return AzureDataStore.LoadDataWithGuid<string>(sql, schoolId).FirstOrDefault();
        }

        public static int PutInSchool(School school)
        {
            string sql = @"INSERT INTO dbo.Schools (Id, Name, ZipCode) VALUES (@Id, @Name, @ZipCode)";

            return AzureDataStore.SaveData(sql, school);
        }
    }
}