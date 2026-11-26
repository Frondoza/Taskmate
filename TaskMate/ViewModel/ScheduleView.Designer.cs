namespace TaskMate.ViewModel
{
    partial class ScheduleView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            TitleTxtBox = new TextBox();
            Create = new Button();
            DescTxtBox = new TextBox();
            label2 = new Label();
            StartDateTime = new DateTimePicker();
            EndDateTime = new DateTimePicker();
            Cancel = new Button();
            label3 = new Label();
            label4 = new Label();
            CloseBtn = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(227, 25);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 0;
            label1.Text = "Title";
            // 
            // TitleTxtBox
            // 
            TitleTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TitleTxtBox.Location = new Point(227, 43);
            TitleTxtBox.Name = "TitleTxtBox";
            TitleTxtBox.Size = new Size(297, 23);
            TitleTxtBox.TabIndex = 1;
            // 
            // Create
            // 
            Create.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            Create.Location = new Point(243, 337);
            Create.Name = "Create";
            Create.Size = new Size(117, 36);
            Create.TabIndex = 2;
            Create.Text = "Create";
            Create.UseVisualStyleBackColor = true;
            // 
            // DescTxtBox
            // 
            DescTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            DescTxtBox.Location = new Point(227, 126);
            DescTxtBox.Multiline = true;
            DescTxtBox.Name = "DescTxtBox";
            DescTxtBox.Size = new Size(297, 73);
            DescTxtBox.TabIndex = 4;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.ImageAlign = ContentAlignment.BottomLeft;
            label2.Location = new Point(227, 108);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 3;
            label2.Text = "Description";
            // 
            // StartDateTime
            // 
            StartDateTime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            StartDateTime.Location = new Point(159, 274);
            StartDateTime.Name = "StartDateTime";
            StartDateTime.Size = new Size(200, 23);
            StartDateTime.TabIndex = 5;
            // 
            // EndDateTime
            // 
            EndDateTime.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            EndDateTime.Location = new Point(383, 274);
            EndDateTime.Name = "EndDateTime";
            EndDateTime.Size = new Size(200, 23);
            EndDateTime.TabIndex = 6;
            // 
            // Cancel
            // 
            Cancel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Cancel.Location = new Point(383, 337);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(121, 36);
            Cancel.TabIndex = 7;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(159, 256);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 8;
            label3.Text = "Creation Date";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(383, 256);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 9;
            label4.Text = "Due Date";
            // 
            // CloseBtn
            // 
            CloseBtn.Location = new Point(19, 15);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new Size(72, 33);
            CloseBtn.TabIndex = 10;
            CloseBtn.Text = "Close";
            CloseBtn.UseVisualStyleBackColor = true;
            CloseBtn.Click += CloseBtn_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.AutoSize = true;
            panel1.Controls.Add(TitleTxtBox);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(Create);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(Cancel);
            panel1.Controls.Add(DescTxtBox);
            panel1.Controls.Add(EndDateTime);
            panel1.Controls.Add(StartDateTime);
            panel1.Location = new Point(0, 57);
            panel1.Name = "panel1";
            panel1.Size = new Size(757, 435);
            panel1.TabIndex = 11;
            // 
            // panel2
            // 
            panel2.Controls.Add(CloseBtn);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(756, 51);
            panel2.TabIndex = 12;
            // 
            // ScheduleView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "ScheduleView";
            Size = new Size(756, 491);
            Load += ScheduleView_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox TitleTxtBox;
        private Button Create;
        private TextBox DescTxtBox;
        private Label label2;
        private DateTimePicker StartDateTime;
        private DateTimePicker EndDateTime;
        private Button Cancel;
        private Label label3;
        private Label label4;
        private Button CloseBtn;
        private Panel panel1;
        private Panel panel2;
    }
}
