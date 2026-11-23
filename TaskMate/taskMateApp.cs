using TaskMate.ViewModel;

namespace TaskMate
{
    public partial class taskMateApp : Form
    {
        public taskMateApp()
        {
            InitializeComponent();
        }

        private void Overview_Click(object sender, EventArgs e)
        {

        }

        private void Task_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();

            TaskView task = new TaskView();

            task.Dock = DockStyle.Fill;

            ViewPanel.Controls.Add(task);

        }

        private void Calendar_Click(object sender, EventArgs e)
        {

        }

        private void Announcement_Click(object sender, EventArgs e)
        {

        }
    }
}
