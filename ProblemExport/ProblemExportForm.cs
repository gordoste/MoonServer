using MoonServer.Models;
using System;
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
            AppDomain.CurrentDomain.SetData("DataDirectory", "C:\\Users\\gordo\\source\\repos\\MoonServer\\MoonServer\\App_Data");
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
            string fileName = folderDlg.SelectedPath + @"\" + String.Join("_", gradeCombo.Text, ratingCombo.Text, repeatsCombo.Text, benchmarkCombo.Text) + ".dat";
            if (File.Exists(fileName))
            {
                DialogResult owrite = MessageBox.Show("File " + fileName + " already exists. Overwrite?",
                    "File Exists", MessageBoxButtons.YesNo);
                if (owrite == DialogResult.No) { return; }
            }
            StreamWriter f = new StreamWriter(fileName);
            int ratingChoice = 0;
            if (ratingCombo.Text != "Any") { ratingChoice = int.Parse(ratingCombo.Text); }
            int repeatsChoice = 0;
            if (repeatsCombo.Text != "Any") { repeatsChoice = int.Parse(repeatsCombo.Text); }
            foreach (Problem p in moonServer.Problems.Where(prb =>
                (gradeCombo.Text.Equals("Any") || prb.Grade.AmericanName.Equals(gradeCombo.Text)) &&
                (ratingCombo.Text.Equals("Any") || prb.Rating >= ratingChoice) &&
                (repeatsCombo.Text.Equals("Any") || prb.Repeats >= repeatsChoice) &&
                (benchmarkCombo.Text.Equals("Any") || prb.IsBenchmark)
            ))
            {
                f.WriteLine(String.Format("{0}:{1}",
                    p.Name,
                    String.Join(",", p.ProblemPositions.ToList().ConvertAll(pp => pp.Position.Name))
                ));
            }
            f.Close();
        }
    }
}
