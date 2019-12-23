using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Autofac;

using Dapper;

using KMSCalendar.Models.Settings;

namespace KMSCalendar.Services.Data
{
    public static class AzureDataStore
    {
        //* Static Properties
        private static string connectionString = AppContainer.Container
            .Resolve<AppSettings>().ConnectionString;

        //* Public Methods
        /// <summary>
        /// Loads a list of data from the db given an sql statement.
        /// </summary>
        /// <typeparam name="T">The return type of which the list should be.</typeparam>
        /// <param name="sql">The sql query command string.</param>
        /// <returns>List of Objcets of type T from the db.</returns>
        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Query<T>(sql).AsList();
        }

        /// <summary>
        /// Loads a list of data from the db given an sql statement and a Guid.
        /// </summary>
        /// <typeparam name="T">The return type of which the list should be.</typeparam>
        /// <param name="sql">The sql query command string.</param>
        /// <param name="id">The Guid used as an identifier in the sql statement.</param>
        /// <returns>List of Objcets of type T from the db.</returns>
        public static List<T> LoadDataWithGuid<T>(string sql, Guid id)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Query<T>(sql, new { Id = id }).AsList();
        }

        /// <summary>
        /// Loads a list of data from the db given an sql statement and a string.
        /// </summary>
        /// <typeparam name="T">The return type of which the list should be.</typeparam>
        /// <param name="sql">The sql query command string.</param>
        /// <param name="id">The string used as an identifier in the sql statement.</param>
        /// <returns>List of Objcets of type T from the db.</returns>
        public static List<T> LoadDataWithString<T>(string sql, string id)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Query<T>(sql, new { Id = id }).AsList();
        }

        /// <summary>
        /// Loads a list of data from the db given an sql statement and an object.
        /// </summary>
        /// <typeparam name="T1">The return type of which the list should be.</typeparam>
        /// <typeparam name="T2">The type of object passed into the function as the second parameter.</typeparam>
        /// <param name="sql">The sql query command string.</param>
        /// <param name="param">Data object used by the sql query.</param>
        /// <returns>List of Objcets of type T1 from the db.</returns>
        public static List<T1> LoadDataWithParam<T1, T2>(string sql, T2 param)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Query<T1>(sql, param).AsList();
        }

        /// <summary>
        /// Inserts data from object T into the database.
        /// </summary>
        /// <typeparam name="T">The object which holds data to pass into the db.</typeparam>
        /// <param name="sql">The sql query command string.</param>
        /// <param name="data">Data object used by the sql query.</param>
        /// <returns>The number of rows affected in the db.</returns>
        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Execute(sql, data);
        }

        /// <summary>
        /// Deletes data from the database.
        /// </summary>
        /// <typeparam name="T">The object type of the second parameter "data".</typeparam>
        /// <param name="sql">The sql query command string.</param>
        /// <param name="data">Data object used by the sql query.</param>
        /// <returns>The number of rows affected in the db.</returns>
        public static int DeleteData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Execute(sql, data);
        }
    }
}