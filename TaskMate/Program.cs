using TaskMate.Utilities;

namespace TaskMate
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            try
            {
                Database.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "TaskMate could not connect to MySQL or initialize its database.\n\n" +
                    "Check that MySQL Server is running and update the Server, User, and Password values in Utilities/Database.cs.\n\n" +
                    ex.Message,
                    "Database connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new taskMateApp());
        }
    }
}
