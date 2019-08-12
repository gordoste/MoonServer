using MoonServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProblemExport
{
    public partial class ProblemExportForm : Form
    {
        private MoonServerDB moonServer = new MoonServerDB();

        public ProblemExportForm()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Users\gordo\source\repos\MoonServer\MoonServer\App_Data");
            MoonServer.Constants.Init(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\MoonServer");
            InitializeComponent();
            gradeCombo.Items.Add("Any");
            gradeCombo.Items.AddRange(moonServer.Grades.ToList().ConvertAll(g => g.AmericanName).ToArray());
            gradeCombo.SelectedItem = "Any";
            ratingCombo.Items.Add("Any");
            ratingCombo.Items.AddRange(MoonServer.Constants.GetFilter("rating").Categories.ToArray());
            ratingCombo.SelectedItem = "Any";
            repeatsCombo.Items.Add("Any");
            repeatsCombo.Items.AddRange(MoonServer.Constants.GetFilter("repeats").Categories.ToArray());
            repeatsCombo.SelectedItem = "Any";
            benchmarkCombo.Items.Add("Any");
            benchmarkCombo.Items.Add("Yes");
            benchmarkCombo.SelectedItem = "Any";
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (folderDlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            ExportProblems(gradeCombo.Text, ratingCombo.Text, repeatsCombo.Text, benchmarkCombo.Text, folderDlg.SelectedPath, true);
        }

        private void autoExportBtn_Click(object sender, EventArgs e)
        {
            List<Control> controlList = new List<Control>(new Control[] { autoExportBtn, exportBtn, gradeCombo, ratingCombo, repeatsCombo, benchmarkCombo });
            foreach (Control c in controlList) { c.Enabled = false; }

            if (folderDlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            ExportProblems("V4", "2", "20", "Any", folderDlg.SelectedPath, false);
            ExportProblems("V5", "2", "20", "Any", folderDlg.SelectedPath, false);
            ExportProblems("V6", "2", "20", "Any", folderDlg.SelectedPath, false);
            ExportProblems("V4", "3", "Any", "Any", folderDlg.SelectedPath, false);
            ExportProblems("V5", "3", "Any", "Any", folderDlg.SelectedPath, false);
            ExportProblems("V6", "3", "Any", "Any", folderDlg.SelectedPath, false);

            foreach (Control c in controlList) { c.Enabled = true; }
        }

        protected void ExportProblems(string grade, string rating, string repeats, string benchmark, string folder, bool askOverwrite)
        {
            StatusTextBox.AppendText("Exporting " + String.Join("/", grade, rating, repeats, benchmark + "..."));
            string fileName = String.Format(@"{0}\{1}.dat", folder, String.Join("_", grade, rating, repeats, benchmark));
            if (askOverwrite && File.Exists(fileName))
            {
                DialogResult owrite = MessageBox.Show("File " + fileName + " already exists. Overwrite?",
                    "File Exists", MessageBoxButtons.YesNo);
                if (owrite == DialogResult.No) { return; }
            }
            int ratingChoice = 0;
            if (rating != "Any") { ratingChoice = int.Parse(rating); }
            int repeatsChoice = 0;
            if (repeats != "Any") { repeatsChoice = int.Parse(repeats); }
            StreamWriter f = new StreamWriter(fileName);
            foreach (Problem p in moonServer.Problems.Where(prb =>
                (grade.Equals("Any") || prb.Grade.AmericanName.Equals(grade)) &&
                (rating.Equals("Any") || prb.Rating >= ratingChoice) &&
                (repeats.Equals("Any") || prb.Repeats >= repeatsChoice) &&
                (benchmark.Equals("Any") || prb.IsBenchmark)
            ))
            {
                MoonServer.PositionStrings ps = new MoonServer.PositionStrings(p);
                f.WriteLine(String.Join(":",
                    p.Name,
                    p.Grade.AmericanName,
                    p.Rating,
                    p.Repeats,
                    p.IsBenchmark ? "Y" : "N",
                    String.Join(" ", ps.Bottom.ToArray()),
                    String.Join(" ", ps.Middle.ToArray()),
                    String.Join(" ", ps.Top.ToArray())
                ));
            }
            f.Close();
            StatusTextBox.AppendText("Done\n");
        }
    }
}
