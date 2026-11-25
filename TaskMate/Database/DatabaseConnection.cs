using MySql.Data.MySqlClient;

namespace TaskMate.Database
{
    public class DatabaseConnection
    {
        private readonly string connectionString =
            "Server=localhost;Port=3306;Database=taskmate_db;Uid=root;Pwd=;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}