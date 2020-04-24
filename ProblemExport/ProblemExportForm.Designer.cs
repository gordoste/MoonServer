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
            this.folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.gradeLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.autoExportBtn = new System.Windows.Forms.Button();
            this.StatusTextBox = new System.Windows.Forms.TextBox();
            this.exportListsBtn = new System.Windows.Forms.Button();
            this.gradeCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ratingCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.bmarkCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.repeatsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
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
            this.label1.Location = new System.Drawing.Point(229, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rating:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Benchmark?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Repeats:";
            // 
            // autoExportBtn
            // 
            this.autoExportBtn.Location = new System.Drawing.Point(533, 76);
            this.autoExportBtn.Name = "autoExportBtn";
            this.autoExportBtn.Size = new System.Drawing.Size(75, 23);
            this.autoExportBtn.TabIndex = 9;
            this.autoExportBtn.Text = "Auto-Export";
            this.autoExportBtn.UseVisualStyleBackColor = true;
            this.autoExportBtn.Click += new System.EventHandler(this.autoExportBtn_Click);
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.Location = new System.Drawing.Point(416, 163);
            this.StatusTextBox.Multiline = true;
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ReadOnly = true;
            this.StatusTextBox.Size = new System.Drawing.Size(353, 122);
            this.StatusTextBox.TabIndex = 10;
            // 
            // exportListsBtn
            // 
            this.exportListsBtn.Location = new System.Drawing.Point(533, 116);
            this.exportListsBtn.Name = "exportListsBtn";
            this.exportListsBtn.Size = new System.Drawing.Size(75, 23);
            this.exportListsBtn.TabIndex = 11;
            this.exportListsBtn.Text = "Export Lists";
            this.exportListsBtn.UseVisualStyleBackColor = true;
            this.exportListsBtn.Click += new System.EventHandler(this.exportListsBtn_Click);
            // 
            // gradeCheckedListBox
            // 
            this.gradeCheckedListBox.CheckOnClick = true;
            this.gradeCheckedListBox.FormattingEnabled = true;
            this.gradeCheckedListBox.Location = new System.Drawing.Point(92, 34);
            this.gradeCheckedListBox.Name = "gradeCheckedListBox";
            this.gradeCheckedListBox.Size = new System.Drawing.Size(120, 94);
            this.gradeCheckedListBox.TabIndex = 12;
            // 
            // ratingCheckedListBox
            // 
            this.ratingCheckedListBox.CheckOnClick = true;
            this.ratingCheckedListBox.FormattingEnabled = true;
            this.ratingCheckedListBox.Location = new System.Drawing.Point(277, 34);
            this.ratingCheckedListBox.Name = "ratingCheckedListBox";
            this.ratingCheckedListBox.Size = new System.Drawing.Size(120, 94);
            this.ratingCheckedListBox.TabIndex = 13;
            this.ratingCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.ratingCheckedListBox_SelectedIndexChanged);
            // 
            // bmarkCheckedListBox
            // 
            this.bmarkCheckedListBox.CheckOnClick = true;
            this.bmarkCheckedListBox.FormattingEnabled = true;
            this.bmarkCheckedListBox.Location = new System.Drawing.Point(92, 163);
            this.bmarkCheckedListBox.Name = "bmarkCheckedListBox";
            this.bmarkCheckedListBox.Size = new System.Drawing.Size(120, 94);
            this.bmarkCheckedListBox.TabIndex = 14;
            // 
            // repeatsCheckedListBox
            // 
            this.repeatsCheckedListBox.CheckOnClick = true;
            this.repeatsCheckedListBox.FormattingEnabled = true;
            this.repeatsCheckedListBox.Location = new System.Drawing.Point(277, 163);
            this.repeatsCheckedListBox.Name = "repeatsCheckedListBox";
            this.repeatsCheckedListBox.Size = new System.Drawing.Size(120, 94);
            this.repeatsCheckedListBox.TabIndex = 15;
            // 
            // ProblemExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 350);
            this.Controls.Add(this.repeatsCheckedListBox);
            this.Controls.Add(this.bmarkCheckedListBox);
            this.Controls.Add(this.ratingCheckedListBox);
            this.Controls.Add(this.gradeCheckedListBox);
            this.Controls.Add(this.exportListsBtn);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.autoExportBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gradeLbl);
            this.Name = "ProblemExportForm";
            this.Text = "Problem Exporter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderDlg;
        private System.Windows.Forms.Label gradeLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button autoExportBtn;
        private System.Windows.Forms.TextBox StatusTextBox;
        private System.Windows.Forms.Button exportListsBtn;
        private System.Windows.Forms.CheckedListBox gradeCheckedListBox;
        private System.Windows.Forms.CheckedListBox ratingCheckedListBox;
        private System.Windows.Forms.CheckedListBox bmarkCheckedListBox;
        private System.Windows.Forms.CheckedListBox repeatsCheckedListBox;
    }
}

