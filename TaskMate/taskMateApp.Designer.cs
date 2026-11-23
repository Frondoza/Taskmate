namespace TaskMate
{
    partial class taskMateApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Announcement = new Button();
            Calendar = new Button();
            Overview = new Button();
            Task = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            ViewPanel = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Announcement
            // 
            Announcement.Dock = DockStyle.Top;
            Announcement.Location = new Point(0, 144);
            Announcement.Name = "Announcement";
            Announcement.Size = new Size(143, 48);
            Announcement.TabIndex = 0;
            Announcement.Text = "Announcement";
            Announcement.UseVisualStyleBackColor = true;
            Announcement.Click += Overview_Click;
            // 
            // Calendar
            // 
            Calendar.Dock = DockStyle.Top;
            Calendar.Location = new Point(0, 96);
            Calendar.Name = "Calendar";
            Calendar.Size = new Size(143, 48);
            Calendar.TabIndex = 1;
            Calendar.Text = "Calendar";
            Calendar.UseVisualStyleBackColor = true;
            Calendar.Click += Calendar_Click;
            // 
            // Overview
            // 
            Overview.Dock = DockStyle.Top;
            Overview.Location = new Point(0, 0);
            Overview.Name = "Overview";
            Overview.Size = new Size(143, 48);
            Overview.TabIndex = 2;
            Overview.Text = "Overview";
            Overview.UseVisualStyleBackColor = true;
            Overview.Click += Announcement_Click;
            // 
            // Task
            // 
            Task.Dock = DockStyle.Top;
            Task.Location = new Point(0, 48);
            Task.Name = "Task";
            Task.Size = new Size(143, 48);
            Task.TabIndex = 3;
            Task.Text = "Task";
            Task.UseVisualStyleBackColor = true;
            Task.Click += Task_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(Announcement);
            panel1.Controls.Add(Calendar);
            panel1.Controls.Add(Task);
            panel1.Controls.Add(Overview);
            panel1.Location = new Point(0, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(143, 592);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Location = new Point(149, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(721, 504);
            panel2.TabIndex = 5;
            // 
            // ViewPanel
            // 
            ViewPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ViewPanel.Location = new Point(149, 1);
            ViewPanel.Name = "ViewPanel";
            ViewPanel.Size = new Size(813, 592);
            ViewPanel.TabIndex = 5;
            // 
            // taskMateApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 592);
            Controls.Add(ViewPanel);
            Controls.Add(panel1);
            Name = "taskMateApp";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button Overview;
        private Button Task;
        private Button Calendar;
        private Button Announcement;
        private Panel panel1;
        private Panel panel2;
        private Panel ViewPanel;
    }
}
