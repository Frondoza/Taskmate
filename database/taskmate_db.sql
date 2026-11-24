
CREATE DATABASE IF NOT EXISTS taskmate_db;
USE taskmate_db;

CREATE TABLE IF NOT EXISTS users (
    user_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100),
    email VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS tasks (
    task_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    title VARCHAR(100),
    description VARCHAR(255),
    due_date DATE,
    priority VARCHAR(20),
    category VARCHAR(50),
    status VARCHAR(20),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

CREATE TABLE IF NOT EXISTS reminders (
    reminder_id INT PRIMARY KEY AUTO_INCREMENT,
    task_id INT,
    reminder_date DATE,
    message VARCHAR(100),
    FOREIGN KEY (task_id) REFERENCES tasks(task_id)
);
