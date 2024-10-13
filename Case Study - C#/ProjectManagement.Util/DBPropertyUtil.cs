using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Util
{
    public static class DBPropertyUtil
    {
        public static string GetConnectionString()
        {
            return "Data Source=DESKTOP-5VEHB15;Initial Catalog=PMSystem;Integrated Security=True;TrustServerCertificate=True";
        }
    }
}