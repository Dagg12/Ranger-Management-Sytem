using System.Configuration;
using MySql.Data.MySqlClient;

namespace TheRanger
{
    public static class Database
    {
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["SafariManagementConnection"].ConnectionString);
        }
    }
}
