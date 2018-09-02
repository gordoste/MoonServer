namespace DataLoader
{
    partial class LoadDataForm
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
            this.ProblemsRadioBtn = new System.Windows.Forms.RadioButton();
            this.HoldPlacementsRadioBtn = new System.Windows.Forms.RadioButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.LoadBtn = new System.Windows.Forms.Button();
            this.TypeGroupBox = new System.Windows.Forms.GroupBox();
            this.PositionsRadioBtn = new System.Windows.Forms.RadioButton();
            this.HoldsRadioBtn = new System.Windows.Forms.RadioButton();
            this.HoldSetupsRadioBtn = new System.Windows.Forms.RadioButton();
            this.GradesRadioBtn = new System.Windows.Forms.RadioButton();
            this.StatusTextBox = new System.Windows.Forms.TextBox();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressLbl = new System.Windows.Forms.Label();
            this.ErrorOnDupCheckBox = new System.Windows.Forms.CheckBox();
            this.ReplaceCheckBox = new System.Windows.Forms.CheckBox();
            this.PageTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Check1Button = new System.Windows.Forms.Button();
            this.TypeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProblemsRadioBtn
            // 
            this.ProblemsRadioBtn.AutoSize = true;
            this.ProblemsRadioBtn.Location = new System.Drawing.Point(6, 32);
            this.ProblemsRadioBtn.Name = "ProblemsRadioBtn";
            this.ProblemsRadioBtn.Size = new System.Drawing.Size(88, 21);
            this.ProblemsRadioBtn.TabIndex = 0;
            this.ProblemsRadioBtn.TabStop = true;
            this.ProblemsRadioBtn.Text = "Problems";
            this.ProblemsRadioBtn.UseVisualStyleBackColor = true;
            this.ProblemsRadioBtn.CheckedChanged += new System.EventHandler(this.ProblemsRadioBtn_CheckedChanged);
            // 
            // HoldPlacementsRadioBtn
            // 
            this.HoldPlacementsRadioBtn.AutoSize = true;
            this.HoldPlacementsRadioBtn.Location = new System.Drawing.Point(6, 59);
            this.HoldPlacementsRadioBtn.Name = "HoldPlacementsRadioBtn";
            this.HoldPlacementsRadioBtn.Size = new System.Drawing.Size(131, 21);
            this.HoldPlacementsRadioBtn.TabIndex = 1;
            this.HoldPlacementsRadioBtn.TabStop = true;
            this.HoldPlacementsRadioBtn.Text = "HoldPlacements";
            this.HoldPlacementsRadioBtn.UseVisualStyleBackColor = true;
            this.HoldPlacementsRadioBtn.CheckedChanged += new System.EventHandler(this.HoldPlacementsRadioBtn_CheckedChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "json";
            this.openFileDialog.Filter = "JSON files|*.json|All files|*.*";
            this.openFileDialog.InitialDirectory = "D:\\Dropbox\\Dropbox\\moonboard\\json";
            // 
            // LoadBtn
            // 
            this.LoadBtn.Enabled = false;
            this.LoadBtn.Location = new System.Drawing.Point(284, 106);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(75, 23);
            this.LoadBtn.TabIndex = 2;
            this.LoadBtn.Text = "Load";
            this.LoadBtn.UseVisualStyleBackColor = true;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // TypeGroupBox
            // 
            this.TypeGroupBox.Controls.Add(this.PositionsRadioBtn);
            this.TypeGroupBox.Controls.Add(this.HoldsRadioBtn);
            this.TypeGroupBox.Controls.Add(this.HoldSetupsRadioBtn);
            this.TypeGroupBox.Controls.Add(this.GradesRadioBtn);
            this.TypeGroupBox.Controls.Add(this.ProblemsRadioBtn);
            this.TypeGroupBox.Controls.Add(this.HoldPlacementsRadioBtn);
            this.TypeGroupBox.Location = new System.Drawing.Point(21, 21);
            this.TypeGroupBox.Name = "TypeGroupBox";
            this.TypeGroupBox.Size = new System.Drawing.Size(200, 241);
            this.TypeGroupBox.TabIndex = 4;
            this.TypeGroupBox.TabStop = false;
            this.TypeGroupBox.Text = "Type";
            // 
            // PositionsRadioBtn
            // 
            this.PositionsRadioBtn.AutoSize = true;
            this.PositionsRadioBtn.Location = new System.Drawing.Point(7, 170);
            this.PositionsRadioBtn.Name = "PositionsRadioBtn";
            this.PositionsRadioBtn.Size = new System.Drawing.Size(86, 21);
            this.PositionsRadioBtn.TabIndex = 5;
            this.PositionsRadioBtn.TabStop = true;
            this.PositionsRadioBtn.Text = "Positions";
            this.PositionsRadioBtn.UseVisualStyleBackColor = true;
            this.PositionsRadioBtn.CheckedChanged += new System.EventHandler(this.PositionsRadioBtn_CheckedChanged);
            // 
            // HoldsRadioBtn
            // 
            this.HoldsRadioBtn.AutoSize = true;
            this.HoldsRadioBtn.Location = new System.Drawing.Point(7, 142);
            this.HoldsRadioBtn.Name = "HoldsRadioBtn";
            this.HoldsRadioBtn.Size = new System.Drawing.Size(65, 21);
            this.HoldsRadioBtn.TabIndex = 4;
            this.HoldsRadioBtn.TabStop = true;
            this.HoldsRadioBtn.Text = "Holds";
            this.HoldsRadioBtn.UseVisualStyleBackColor = true;
            this.HoldsRadioBtn.CheckedChanged += new System.EventHandler(this.HoldsRadioBtn_CheckedChanged);
            // 
            // HoldSetupsRadioBtn
            // 
            this.HoldSetupsRadioBtn.AutoSize = true;
            this.HoldSetupsRadioBtn.Location = new System.Drawing.Point(7, 115);
            this.HoldSetupsRadioBtn.Name = "HoldSetupsRadioBtn";
            this.HoldSetupsRadioBtn.Size = new System.Drawing.Size(102, 21);
            this.HoldSetupsRadioBtn.TabIndex = 3;
            this.HoldSetupsRadioBtn.TabStop = true;
            this.HoldSetupsRadioBtn.Text = "HoldSetups";
            this.HoldSetupsRadioBtn.UseVisualStyleBackColor = true;
            this.HoldSetupsRadioBtn.CheckedChanged += new System.EventHandler(this.HoldSetupsRadioBtn_CheckedChanged);
            // 
            // GradesRadioBtn
            // 
            this.GradesRadioBtn.AutoSize = true;
            this.GradesRadioBtn.Location = new System.Drawing.Point(7, 87);
            this.GradesRadioBtn.Name = "GradesRadioBtn";
            this.GradesRadioBtn.Size = new System.Drawing.Size(76, 21);
            this.GradesRadioBtn.TabIndex = 2;
            this.GradesRadioBtn.TabStop = true;
            this.GradesRadioBtn.Text = "Grades";
            this.GradesRadioBtn.UseVisualStyleBackColor = true;
            this.GradesRadioBtn.CheckedChanged += new System.EventHandler(this.GradesRadioBtn_CheckedChanged);
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.StatusTextBox.Location = new System.Drawing.Point(12, 307);
            this.StatusTextBox.Multiline = true;
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ReadOnly = true;
            this.StatusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusTextBox.Size = new System.Drawing.Size(633, 113);
            this.StatusTextBox.TabIndex = 5;
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(284, 136);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 6;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "json";
            this.saveFileDialog.Filter = "JSON files|*.json|All files|*.*";
            this.saveFileDialog.InitialDirectory = "D:\\Dropbox\\Dropbox\\moonboard\\json";
            this.saveFileDialog.OverwritePrompt = false;
            this.saveFileDialog.ShowHelp = true;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(270, 220);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(259, 23);
            this.ProgressBar.TabIndex = 7;
            this.ProgressBar.Visible = false;
            // 
            // ProgressLbl
            // 
            this.ProgressLbl.AutoSize = true;
            this.ProgressLbl.Location = new System.Drawing.Point(553, 225);
            this.ProgressLbl.MinimumSize = new System.Drawing.Size(75, 0);
            this.ProgressLbl.Name = "ProgressLbl";
            this.ProgressLbl.Size = new System.Drawing.Size(75, 17);
            this.ProgressLbl.TabIndex = 8;
            // 
            // ErrorOnDupCheckBox
            // 
            this.ErrorOnDupCheckBox.AutoSize = true;
            this.ErrorOnDupCheckBox.Location = new System.Drawing.Point(284, 31);
            this.ErrorOnDupCheckBox.Name = "ErrorOnDupCheckBox";
            this.ErrorOnDupCheckBox.Size = new System.Drawing.Size(185, 21);
            this.ErrorOnDupCheckBox.TabIndex = 9;
            this.ErrorOnDupCheckBox.Text = "Throw error on duplicate";
            this.ErrorOnDupCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReplaceCheckBox
            // 
            this.ReplaceCheckBox.AutoSize = true;
            this.ReplaceCheckBox.Location = new System.Drawing.Point(284, 59);
            this.ReplaceCheckBox.Name = "ReplaceCheckBox";
            this.ReplaceCheckBox.Size = new System.Drawing.Size(320, 21);
            this.ReplaceCheckBox.TabIndex = 10;
            this.ReplaceCheckBox.Text = "Replace Problem with same ID, different name";
            this.ReplaceCheckBox.UseVisualStyleBackColor = true;
            // 
            // PageTextBox
            // 
            this.PageTextBox.Location = new System.Drawing.Point(489, 136);
            this.PageTextBox.Name = "PageTextBox";
            this.PageTextBox.Size = new System.Drawing.Size(100, 22);
            this.PageTextBox.TabIndex = 11;
            this.PageTextBox.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(489, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Page # (problems only):";
            // 
            // Check1Button
            // 
            this.Check1Button.Location = new System.Drawing.Point(284, 180);
            this.Check1Button.Name = "Check1Button";
            this.Check1Button.Size = new System.Drawing.Size(75, 23);
            this.Check1Button.TabIndex = 13;
            this.Check1Button.Text = "Check 1";
            this.Check1Button.UseVisualStyleBackColor = true;
            this.Check1Button.Click += new System.EventHandler(this.Check1Button_Click);
            // 
            // LoadDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 432);
            this.Controls.Add(this.Check1Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PageTextBox);
            this.Controls.Add(this.ReplaceCheckBox);
            this.Controls.Add(this.ErrorOnDupCheckBox);
            this.Controls.Add(this.ProgressLbl);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.TypeGroupBox);
            this.Controls.Add(this.LoadBtn);
            this.Name = "LoadDataForm";
            this.Text = "Load Data";
            this.TypeGroupBox.ResumeLayout(false);
            this.TypeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton ProblemsRadioBtn;
        private System.Windows.Forms.RadioButton HoldPlacementsRadioBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.GroupBox TypeGroupBox;
        private System.Windows.Forms.TextBox StatusTextBox;
        private System.Windows.Forms.RadioButton PositionsRadioBtn;
        private System.Windows.Forms.RadioButton HoldsRadioBtn;
        private System.Windows.Forms.RadioButton HoldSetupsRadioBtn;
        private System.Windows.Forms.RadioButton GradesRadioBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label ProgressLbl;
        private System.Windows.Forms.CheckBox ErrorOnDupCheckBox;
        private System.Windows.Forms.CheckBox ReplaceCheckBox;
        private System.Windows.Forms.TextBox PageTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Check1Button;
    }
}

