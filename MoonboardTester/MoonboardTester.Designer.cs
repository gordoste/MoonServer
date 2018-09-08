namespace MoonboardTester
{
    partial class MoonboardTester
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
            this.ProblemTextBox = new System.Windows.Forms.TextBox();
            this.ChooseBtn = new System.Windows.Forms.Button();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ProblemTextBox
            // 
            this.ProblemTextBox.Location = new System.Drawing.Point(75, 46);
            this.ProblemTextBox.Name = "ProblemTextBox";
            this.ProblemTextBox.Size = new System.Drawing.Size(100, 22);
            this.ProblemTextBox.TabIndex = 0;
            // 
            // ChooseBtn
            // 
            this.ChooseBtn.Location = new System.Drawing.Point(232, 44);
            this.ChooseBtn.Name = "ChooseBtn";
            this.ChooseBtn.Size = new System.Drawing.Size(75, 23);
            this.ChooseBtn.TabIndex = 1;
            this.ChooseBtn.Text = "Choose";
            this.ChooseBtn.UseVisualStyleBackColor = true;
            this.ChooseBtn.Click += new System.EventHandler(this.ChooseBtn_Click);
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(47, 183);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(722, 255);
            this.LogTextBox.TabIndex = 2;
            // 
            // MoonboardTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.ChooseBtn);
            this.Controls.Add(this.ProblemTextBox);
            this.Name = "MoonboardTester";
            this.Text = "Moonboard Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ProblemTextBox;
        private System.Windows.Forms.Button ChooseBtn;
        private System.Windows.Forms.TextBox LogTextBox;
    }
}

