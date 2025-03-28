namespace WinFormsApp_No10
{
    partial class Form1
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
            label1 = new Label();
            username = new TextBox();
            button1 = new Button();
            lblTimer = new Label();
            lblQuestion = new Label();
            prevButton = new Button();
            nextButton = new Button();
            label2 = new Label();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(59, 21);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 0;
            label1.Text = "ชื่อ - นามสกุล";
            // 
            // username
            // 
            username.Location = new Point(134, 18);
            username.Name = "username";
            username.Size = new Size(216, 23);
            username.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(51, 359);
            button1.Name = "button1";
            button1.Size = new Size(90, 23);
            button1.TabIndex = 3;
            button1.Text = "เริ่มทำข้อสอบ";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.BackColor = SystemColors.ActiveCaption;
            lblTimer.Location = new Point(599, 21);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(93, 15);
            lblTimer.TabIndex = 4;
            lblTimer.Text = "จับเวลา : 00:00:00 ";
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.Location = new Point(51, 82);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(0, 15);
            lblQuestion.TabIndex = 5;
            // 
            // prevButton
            // 
            prevButton.Location = new Point(185, 307);
            prevButton.Name = "prevButton";
            prevButton.Size = new Size(75, 23);
            prevButton.TabIndex = 6;
            prevButton.Text = "ย้อนกลับ";
            prevButton.UseVisualStyleBackColor = true;
            prevButton.Click += prevButton_Click;
            // 
            // nextButton
            // 
            nextButton.Location = new Point(325, 307);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(75, 23);
            nextButton.TabIndex = 7;
            nextButton.Text = "ถัดไป";
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += nextButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.InactiveCaption;
            label2.Location = new Point(409, 359);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 8;
            label2.Text = "คุณ ---  ได้คะแนน: -/-";
            // 
            // panel1
            // 
            panel1.Location = new Point(33, 58);
            panel1.Name = "panel1";
            panel1.Size = new Size(478, 243);
            panel1.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 413);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(nextButton);
            Controls.Add(prevButton);
            Controls.Add(lblQuestion);
            Controls.Add(lblTimer);
            Controls.Add(button1);
            Controls.Add(username);
            Controls.Add(label1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Good Quiz IT-10 By Witsawa";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox username;
        private Button button1;
        private Label lblTimer;
        private Label lblQuestion;
        private Button button2;
        private Button button3;
        private Button nextButton;
        private Button prevButton;
        private Label label2;
        private Panel panel1;
    }
}
