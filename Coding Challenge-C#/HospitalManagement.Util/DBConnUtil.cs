using System;
using System.Data.SqlClient;

namespace HospitalManagementSystem.Util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = DBPropertyUtil.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}