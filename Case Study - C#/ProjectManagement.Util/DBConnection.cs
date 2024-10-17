using System;
using System.Data.SqlClient;
using System.Configuration;

namespace ProjectManagementSystem.Util
{
    public static class DBConnection
    {
        private static SqlConnection _connection;

        public static SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ProjectManagementDB"].ConnectionString;
                _connection = new SqlConnection(connectionString);

            }
            return _connection;
        }

    }
}
