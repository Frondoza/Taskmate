using MySqlConnector;
using System.Data;

namespace TaskMate.Utilities;

internal static class Database
{
    private const string Server = "localhost";
    private const uint Port = 3306;
    private const string DatabaseName = "taskmate_db";
    private const string User = "root";
    private const string Password = "Fr0ndFr0nd@1010";

    private static string ServerConnectionString =>
        $"Server={Server};Port={Port};User ID={User};Password={Password};";

    private static string ConnectionString =>
        $"{ServerConnectionString}Database={DatabaseName};";

    public static MySqlConnection Open()
    {
        var connection = new MySqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    public static void Initialize()
    {
        using (var server = new MySqlConnection(ServerConnectionString))
        {
            server.Open();
            using var createDb = new MySqlCommand(
                $"CREATE DATABASE IF NOT EXISTS `{DatabaseName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;", server);
            createDb.ExecuteNonQuery();
        }

        using var connection = Open();
        string[] statements =
        {
            """
            CREATE TABLE IF NOT EXISTS users (
                user_id INT PRIMARY KEY AUTO_INCREMENT,
                name VARCHAR(100) NOT NULL,
                email VARCHAR(150) NULL
            )
            """,
            """
            CREATE TABLE IF NOT EXISTS categories (
                category_id INT PRIMARY KEY AUTO_INCREMENT,
                user_id INT NOT NULL,
                category_name VARCHAR(80) NOT NULL,
                description VARCHAR(255) NULL,
                UNIQUE KEY uq_category_user_name (user_id, category_name),
                CONSTRAINT fk_categories_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
            )
            """,
            """
            CREATE TABLE IF NOT EXISTS tasks (
                task_id INT PRIMARY KEY AUTO_INCREMENT,
                user_id INT NOT NULL,
                category_id INT NULL,
                title VARCHAR(150) NOT NULL,
                description VARCHAR(1000) NULL,
                priority VARCHAR(20) NOT NULL DEFAULT 'Medium',
                status VARCHAR(20) NOT NULL DEFAULT 'Pending',
                start_date DATETIME NULL,
                due_date DATETIME NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                CONSTRAINT fk_tasks_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
                CONSTRAINT fk_tasks_category FOREIGN KEY (category_id) REFERENCES categories(category_id) ON DELETE SET NULL
            )
            """,
            """
            CREATE TABLE IF NOT EXISTS schedules (
                schedule_id INT PRIMARY KEY AUTO_INCREMENT,
                user_id INT NOT NULL,
                title VARCHAR(150) NOT NULL,
                description VARCHAR(1000) NULL,
                start_time DATETIME NOT NULL,
                end_time DATETIME NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                CONSTRAINT fk_schedules_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
            )
            """,
            """
            CREATE TABLE IF NOT EXISTS announcements (
                announcement_id INT PRIMARY KEY AUTO_INCREMENT,
                user_id INT NOT NULL,
                title VARCHAR(150) NOT NULL,
                message TEXT NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                CONSTRAINT fk_announcements_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
            )
            """,
            """
            CREATE TABLE IF NOT EXISTS notifications (
                notification_id INT PRIMARY KEY AUTO_INCREMENT,
                user_id INT NOT NULL,
                message VARCHAR(500) NOT NULL,
                created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                is_read BOOLEAN NOT NULL DEFAULT FALSE,
                CONSTRAINT fk_notifications_user FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
            )
            """,
            """
            CREATE TABLE IF NOT EXISTS reminders (
                reminder_id INT PRIMARY KEY AUTO_INCREMENT,
                task_id INT NOT NULL,
                reminder_date DATETIME NOT NULL,
                message VARCHAR(255) NULL,
                CONSTRAINT fk_reminders_task FOREIGN KEY (task_id) REFERENCES tasks(task_id) ON DELETE CASCADE
            )
            """
        };

        foreach (var sql in statements)
        {
            using var command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        // Upgrade installations created from the original, smaller SQL script.
        EnsureColumn(connection, "tasks", "category_id", "INT NULL");
        EnsureColumn(connection, "tasks", "start_date", "DATETIME NULL");
        EnsureColumn(connection, "tasks", "created_at", "DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP");

        using var seed = new MySqlCommand(
            "INSERT INTO users (user_id, name, email) VALUES (1, 'TaskMate User', NULL) ON DUPLICATE KEY UPDATE name = VALUES(name);", connection);
        seed.ExecuteNonQuery();

        using var defaultCategory = new MySqlCommand(
            "INSERT IGNORE INTO categories (user_id, category_name, description) VALUES (1, 'General', 'Default category');", connection);
        defaultCategory.ExecuteNonQuery();
    }


    private static void EnsureColumn(MySqlConnection connection, string table, string column, string definition)
    {
        using var check = new MySqlCommand(
            "SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@db AND TABLE_NAME=@table AND COLUMN_NAME=@column;", connection);
        check.Parameters.AddWithValue("@db", DatabaseName);
        check.Parameters.AddWithValue("@table", table);
        check.Parameters.AddWithValue("@column", column);
        if (Convert.ToInt32(check.ExecuteScalar()) == 0)
        {
            using var alter = new MySqlCommand($"ALTER TABLE `{table}` ADD COLUMN `{column}` {definition};", connection);
            alter.ExecuteNonQuery();
        }
    }

    public static DataTable Query(string sql, params MySqlParameter[] parameters)
    {
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddRange(parameters);
        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static int Execute(string sql, params MySqlParameter[] parameters)
    {
        using var connection = Open();
        using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddRange(parameters);
        return command.ExecuteNonQuery();
    }

    public static MySqlParameter P(string name, object? value) =>
        new(name, value ?? DBNull.Value);
}
