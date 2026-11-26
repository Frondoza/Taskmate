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
            panel1 = new Panel();
            panel3 = new Panel();
            label2 = new Label();
            panel2 = new Panel();
            ViewPanel = new Panel();
            AddScheduleBtn = new Button();
            TaskPnl = new Panel();
            label3 = new Label();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            ViewPanel.SuspendLayout();
            TaskPnl.SuspendLayout();
            SuspendLayout();
            // 
            // Announcement
            // 
            Announcement.Location = new Point(0, 103);
            Announcement.Name = "Announcement";
            Announcement.Size = new Size(143, 48);
            Announcement.TabIndex = 0;
            Announcement.Text = "Announcement";
            Announcement.UseVisualStyleBackColor = true;
            Announcement.Click += Overview_Click;
            // 
            // Calendar
            // 
            Calendar.Location = new Point(0, 55);
            Calendar.Name = "Calendar";
            Calendar.Size = new Size(143, 48);
            Calendar.TabIndex = 1;
            Calendar.Text = "Calendar";
            Calendar.UseVisualStyleBackColor = true;
            Calendar.Click += Calendar_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(Announcement);
            panel1.Controls.Add(Calendar);
            panel1.Location = new Point(0, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(143, 559);
            panel1.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Controls.Add(label2);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(143, 49);
            panel3.TabIndex = 8;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F);
            label2.Location = new Point(26, 8);
            label2.Name = "label2";
            label2.Size = new Size(89, 28);
            label2.TabIndex = 6;
            label2.Text = "My Tasks";
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
            ViewPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ViewPanel.Controls.Add(AddScheduleBtn);
            ViewPanel.Location = new Point(149, 1);
            ViewPanel.Name = "ViewPanel";
            ViewPanel.Size = new Size(755, 49);
            ViewPanel.TabIndex = 5;
            // 
            // AddScheduleBtn
            // 
            AddScheduleBtn.Dock = DockStyle.Right;
            AddScheduleBtn.Font = new Font("Segoe UI", 11F);
            AddScheduleBtn.Location = new Point(617, 0);
            AddScheduleBtn.Name = "AddScheduleBtn";
            AddScheduleBtn.Size = new Size(138, 49);
            AddScheduleBtn.TabIndex = 7;
            AddScheduleBtn.Text = "Add Schedule";
            AddScheduleBtn.UseVisualStyleBackColor = true;
            AddScheduleBtn.Click += AddScheduleBtn_Click;
            // 
            // TaskPnl
            // 
            TaskPnl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TaskPnl.Controls.Add(label3);
            TaskPnl.Location = new Point(149, 56);
            TaskPnl.Name = "TaskPnl";
            TaskPnl.Size = new Size(755, 504);
            TaskPnl.TabIndex = 6;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13F);
            label3.Location = new Point(340, 240);
            label3.Name = "label3";
            label3.Size = new Size(74, 25);
            label3.TabIndex = 1;
            label3.Text = "No Task";
            // 
            // taskMateApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(903, 559);
            Controls.Add(TaskPnl);
            Controls.Add(ViewPanel);
            Controls.Add(panel1);
            Name = "taskMateApp";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ViewPanel.ResumeLayout(false);
            TaskPnl.ResumeLayout(false);
            TaskPnl.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button Calendar;
        private Button Announcement;
        private Panel panel1;
        private Panel panel2;
        private Panel ViewPanel;
        private Button AddScheduleBtn;
        private Label label2;
        private Panel panel3;
        private Panel TaskPnl;
        private Label label3;
    }
}
