using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TaskMate.ViewModel
{
    public partial class TaskView : UserControl
    {
        public TaskView()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AddScheduleBtn_Click(object sender, EventArgs e)
        {
            ScheduleView view = new ScheduleView();
            view.Dock = DockStyle.Fill;
            this.Parent.Controls.Add(view);
            view.BringToFront();
        }
    }
}
