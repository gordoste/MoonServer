namespace ProblemExport
{
    partial class ProblemExportForm
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
            this.exportBtn = new System.Windows.Forms.Button();
            this.folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.gradeCombo = new System.Windows.Forms.ComboBox();
            this.gradeLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ratingCombo = new System.Windows.Forms.ComboBox();
            this.benchmarkCombo = new System.Windows.Forms.ComboBox();
            this.repeatsCombo = new System.Windows.Forms.ComboBox();
            this.autoExportBtn = new System.Windows.Forms.Button();
            this.StatusTextBox = new System.Windows.Forms.TextBox();
            this.exportListsBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exportBtn
            // 
            this.exportBtn.Location = new System.Drawing.Point(39, 163);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(75, 23);
            this.exportBtn.TabIndex = 0;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.ExportBtn_Click);
            // 
            // gradeCombo
            // 
            this.gradeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gradeCombo.FormattingEnabled = true;
            this.gradeCombo.Location = new System.Drawing.Point(128, 27);
            this.gradeCombo.Name = "gradeCombo";
            this.gradeCombo.Size = new System.Drawing.Size(121, 21);
            this.gradeCombo.TabIndex = 1;
            // 
            // gradeLbl
            // 
            this.gradeLbl.AutoSize = true;
            this.gradeLbl.Location = new System.Drawing.Point(33, 34);
            this.gradeLbl.Name = "gradeLbl";
            this.gradeLbl.Size = new System.Drawing.Size(39, 13);
            this.gradeLbl.TabIndex = 2;
            this.gradeLbl.Text = "Grade:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rating:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Benchmark?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Repeats:";
            // 
            // ratingCombo
            // 
            this.ratingCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ratingCombo.FormattingEnabled = true;
            this.ratingCombo.Location = new System.Drawing.Point(128, 57);
            this.ratingCombo.Name = "ratingCombo";
            this.ratingCombo.Size = new System.Drawing.Size(121, 21);
            this.ratingCombo.TabIndex = 6;
            // 
            // benchmarkCombo
            // 
            this.benchmarkCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.benchmarkCombo.FormattingEnabled = true;
            this.benchmarkCombo.Location = new System.Drawing.Point(128, 85);
            this.benchmarkCombo.Name = "benchmarkCombo";
            this.benchmarkCombo.Size = new System.Drawing.Size(121, 21);
            this.benchmarkCombo.TabIndex = 7;
            // 
            // repeatsCombo
            // 
            this.repeatsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.repeatsCombo.FormattingEnabled = true;
            this.repeatsCombo.Location = new System.Drawing.Point(128, 115);
            this.repeatsCombo.Name = "repeatsCombo";
            this.repeatsCombo.Size = new System.Drawing.Size(121, 21);
            this.repeatsCombo.TabIndex = 8;
            // 
            // autoExportBtn
            // 
            this.autoExportBtn.Location = new System.Drawing.Point(158, 163);
            this.autoExportBtn.Name = "autoExportBtn";
            this.autoExportBtn.Size = new System.Drawing.Size(75, 23);
            this.autoExportBtn.TabIndex = 9;
            this.autoExportBtn.Text = "Auto-Export";
            this.autoExportBtn.UseVisualStyleBackColor = true;
            this.autoExportBtn.Click += new System.EventHandler(this.autoExportBtn_Click);
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.Location = new System.Drawing.Point(12, 205);
            this.StatusTextBox.Multiline = true;
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ReadOnly = true;
            this.StatusTextBox.Size = new System.Drawing.Size(353, 122);
            this.StatusTextBox.TabIndex = 10;
            // 
            // exportListsBtn
            // 
            this.exportListsBtn.Location = new System.Drawing.Point(269, 163);
            this.exportListsBtn.Name = "exportListsBtn";
            this.exportListsBtn.Size = new System.Drawing.Size(75, 23);
            this.exportListsBtn.TabIndex = 11;
            this.exportListsBtn.Text = "Export Lists";
            this.exportListsBtn.UseVisualStyleBackColor = true;
            this.exportListsBtn.Click += new System.EventHandler(this.exportListsBtn_Click);
            // 
            // ProblemExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 339);
            this.Controls.Add(this.exportListsBtn);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.autoExportBtn);
            this.Controls.Add(this.repeatsCombo);
            this.Controls.Add(this.benchmarkCombo);
            this.Controls.Add(this.ratingCombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gradeLbl);
            this.Controls.Add(this.gradeCombo);
            this.Controls.Add(this.exportBtn);
            this.Name = "ProblemExportForm";
            this.Text = "Problem Exporter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.FolderBrowserDialog folderDlg;
        private System.Windows.Forms.ComboBox gradeCombo;
        private System.Windows.Forms.Label gradeLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ratingCombo;
        private System.Windows.Forms.ComboBox benchmarkCombo;
        private System.Windows.Forms.ComboBox repeatsCombo;
        private System.Windows.Forms.Button autoExportBtn;
        private System.Windows.Forms.TextBox StatusTextBox;
        private System.Windows.Forms.Button exportListsBtn;
    }
}

