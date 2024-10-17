using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Util
{
    public static class DBPropertyUtil
    {
        public static string GetConnectionString(string ProjectManagementDB)
        {
            try
            {
                // Retrieves the connection string from the app.config
                string connectionString = ConfigurationManager.ConnectionStrings[ProjectManagementDB]?.ConnectionString;

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException("Connection string not found for the property: " + ProjectManagementDB);
                }

                return connectionString;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading the connection string: " + ex.Message);
            }
        }
    }
}