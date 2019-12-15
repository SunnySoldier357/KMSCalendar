using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Dapper;

namespace KMSCalendar.Services.Data
{
    public static class SqlAccess
    {
        //* Static Properties
        private static string connectionString = Connections.GetConnectionString();

        //* Public Methods
        public static int DeleteData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Execute(sql, data);
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Query<T>(sql).AsList();
        }

        public static List<T> LoadDataWithId<T>(string sql, string id)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Query<T>(sql, new { Id = id }).AsList();
        }

        public static List<T1> LoadDataWithParam<T1, T2>(string sql, T2 param)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Query<T1>(sql, param).AsList();
        }

        // TODO: This returns a List, not a singular data item & looks like
        //       a duplicate of LoadDataWithId
        public static List<T> LoadSingularData<T>(string sql, string id)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Query<T>(sql, new { Id = id }).AsList();
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Execute(sql, data);
        }

        public static int SaveItemReturnId<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
                return cnn.Query<int>(sql, data).Single();
        }
    }
}