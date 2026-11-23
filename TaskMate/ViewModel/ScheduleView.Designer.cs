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
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(158, 55);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 0;
            label1.Text = "Title";
            // 
            // TitleTxtBox
            // 
            TitleTxtBox.Location = new Point(158, 73);
            TitleTxtBox.Name = "TitleTxtBox";
            TitleTxtBox.Size = new Size(446, 23);
            TitleTxtBox.TabIndex = 1;
            // 
            // Create
            // 
            Create.Location = new Point(251, 366);
            Create.Name = "Create";
            Create.Size = new Size(117, 36);
            Create.TabIndex = 2;
            Create.Text = "Create";
            Create.UseVisualStyleBackColor = true;
            // 
            // DescTxtBox
            // 
            DescTxtBox.Location = new Point(158, 156);
            DescTxtBox.Multiline = true;
            DescTxtBox.Name = "DescTxtBox";
            DescTxtBox.Size = new Size(446, 95);
            DescTxtBox.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(158, 138);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 3;
            label2.Text = "Description";
            // 
            // StartDateTime
            // 
            StartDateTime.Location = new Point(167, 304);
            StartDateTime.Name = "StartDateTime";
            StartDateTime.Size = new Size(200, 23);
            StartDateTime.TabIndex = 5;
            // 
            // EndDateTime
            // 
            EndDateTime.Location = new Point(391, 304);
            EndDateTime.Name = "EndDateTime";
            EndDateTime.Size = new Size(200, 23);
            EndDateTime.TabIndex = 6;
            // 
            // Cancel
            // 
            Cancel.Location = new Point(391, 366);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(121, 36);
            Cancel.TabIndex = 7;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(167, 286);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 8;
            label3.Text = "Creation Date";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(391, 286);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 9;
            label4.Text = "Due Date";
            // 
            // ScheduleView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(Cancel);
            Controls.Add(EndDateTime);
            Controls.Add(StartDateTime);
            Controls.Add(DescTxtBox);
            Controls.Add(label2);
            Controls.Add(Create);
            Controls.Add(TitleTxtBox);
            Controls.Add(label1);
            Name = "ScheduleView";
            Size = new Size(784, 462);
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
    }
}
