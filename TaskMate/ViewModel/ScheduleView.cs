using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TaskMate.ViewModel
{
    public partial class ScheduleView : UserControl
    {
        public ScheduleView()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
