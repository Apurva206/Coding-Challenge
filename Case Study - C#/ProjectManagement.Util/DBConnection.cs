using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace ProjectManagementSystem.Util
{

    public static class DBConnection
    {
        private static SqlConnection _connection;

        public static SqlConnection GetConnection(string connectionString)
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(connectionString);
            }
            return _connection;
        }
    }
}