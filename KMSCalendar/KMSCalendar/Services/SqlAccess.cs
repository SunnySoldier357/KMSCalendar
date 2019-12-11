using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace KMSCalendar.Services
{
    public static class SqlAccess
    {
        private static string connectionString = Connections.GetConnectionString();

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                return cnn.Query<T>(sql).AsList();
            }
        }

        public static List<T> LoadDataWithId<T>(string sql, string id)   //currrent is the Id
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))     //grabs the connection string from the method above.
            {
                return cnn.Query<T>(sql, new { Id = id }).AsList();      //returns a list of generics from the database
            }
        }

        public static List<TOne> LoadDataWithParam<TOne, TTwo>(string sql, TTwo data)   //currrent is the Id
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))     //grabs the connection string from the method above.
            {
                return cnn.Query<TOne>(sql, data).AsList();      //returns a list of generics from the database
            }
        }

        public static List<T> LoadSingularData<T>(string sql, string id)   //currrent is the Id
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))     //grabs the connection string from the method above.
            {
                return cnn.Query<T>(sql, new { Id = id }).AsList();      //returns a list of generics from the database
            }
        }

        public static int SaveItemReturnId<T>(string sql, T data)   //currrent is the Id
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))     //grabs the connection string from the method above.
            {
                return cnn.Query<int>(sql, data).Single();
            }
        }



    }
}
