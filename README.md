# TaskMate

TaskMate is a Windows Forms (.NET 10) desktop app backed by MySQL.

## Requirements
- Windows
- Visual Studio 2022 with the .NET 10 SDK / Windows Forms workload
- MySQL Server 8.0+ (or a compatible MariaDB server)

## Database setup
1. Start your MySQL server.
2. Open `TaskMate/Utilities/Database.cs`.
3. Update `Server`, `Port`, `User`, and `Password` to match your MySQL installation.
4. Run the application. TaskMate creates `taskmate_db` and its tables automatically.
5. The app seeds a local demo user with `user_id = 1` and a `General` category.

The SQL setup script is also included at `database/taskmate_db.sql`.

## Features
- Tasks: create, edit, delete, mark complete, priority/status, dates, category assignment.
- Categories: create, edit, delete.
- Schedules: create, edit, delete with start/end date and time.
- Calendar: monthly view showing scheduled events and task due dates.
- Announcements: publish, view, and delete announcements.

## Notes
- The app currently uses a single local demo account (`user_id = 1`); authentication and multi-user account selection are not implemented.
- Tasks and schedules are shown in the calendar by due date / start date.
- The project uses the `MySqlConnector` NuGet package. Visual Studio will restore it when the project is opened.
