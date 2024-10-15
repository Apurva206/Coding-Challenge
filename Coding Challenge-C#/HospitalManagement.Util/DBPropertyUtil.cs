using System;
using System.Configuration;

namespace HospitalManagementSystem.Util
{
    public static class DBPropertyUtil
    {
        public static string GetConnectionString()
        {
            // Retrieve the connection string from the app.config
            string connectionString = ConfigurationManager.ConnectionStrings["HospitalDB"]?.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is not configured.");
            }

            return connectionString;
        }
    }
}

