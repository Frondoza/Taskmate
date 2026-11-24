namespace TaskMate.ViewModel
{
    partial class TaskView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            NoTaskLbl = new Label();
            panel2 = new Panel();
            AddScheduleBtn = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(822, 13);
            label1.Name = "label1";
            label1.Size = new Size(91, 28);
            label1.TabIndex = 2;
            label1.Text = "Schedule";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F);
            label2.Location = new Point(18, 13);
            label2.Name = "label2";
            label2.Size = new Size(89, 28);
            label2.TabIndex = 3;
            label2.Text = "My Tasks";
            label2.Click += label2_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(NoTaskLbl);
            panel1.Location = new Point(0, 63);
            panel1.Name = "panel1";
            panel1.Size = new Size(934, 500);
            panel1.TabIndex = 4;
            // 
            // NoTaskLbl
            // 
            NoTaskLbl.AutoSize = true;
            NoTaskLbl.Font = new Font("Segoe UI", 13F);
            NoTaskLbl.Location = new Point(453, 221);
            NoTaskLbl.Name = "NoTaskLbl";
            NoTaskLbl.Size = new Size(74, 25);
            NoTaskLbl.TabIndex = 0;
            NoTaskLbl.Text = "No Task";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(AddScheduleBtn);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(934, 57);
            panel2.TabIndex = 5;
            // 
            // AddScheduleBtn
            // 
            AddScheduleBtn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            AddScheduleBtn.Location = new Point(786, 17);
            AddScheduleBtn.Name = "AddScheduleBtn";
            AddScheduleBtn.Size = new Size(30, 28);
            AddScheduleBtn.TabIndex = 4;
            AddScheduleBtn.Text = "+";
            AddScheduleBtn.UseVisualStyleBackColor = true;
            AddScheduleBtn.Click += AddScheduleBtn_Click;
            // 
            // TaskView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "TaskView";
            Size = new Size(933, 566);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Panel panel2;
        private Label NoTaskLbl;
        private Button AddScheduleBtn;
    }
}