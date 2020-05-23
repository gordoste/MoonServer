using MoonServer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProblemExport
{
    public partial class ProblemExportForm : Form
    {
        private MoonServerDB moonServer = new MoonServerDB();
        private string BMARK_YES = "Yes";
        private string BMARK_NO = "No";

        private string ProblemAsString(Problem p)
        {
            MoonServer.PositionStrings ps = new MoonServer.PositionStrings(p);
            return String.Join("|",
                p.Name,
                p.Grade.AmericanName,
                p.Rating,
                p.Repeats,
                p.IsBenchmark ? "Y" : "N",
                String.Join(" ", ps.Bottom.ToArray()),
                String.Join(" ", ps.Middle.ToArray()),
                String.Join(" ", ps.Top.ToArray())
            );
        }

        public ProblemExportForm()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Users\gordo\source\repos\MoonServer\MoonServer\App_Data");
            MoonServer.Constants.Init(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\MoonServer");
            InitializeComponent();
            gradeCheckedListBox.Items.Add("Any", true);
            gradeCheckedListBox.Items.AddRange(MoonServer.Constants.GetFilter("grade").Categories.ToArray());
            ratingCheckedListBox.Items.Add("Any", true);
            ratingCheckedListBox.Items.AddRange(MoonServer.Constants.GetFilter("rating").Categories.ToArray());
            repeatsCheckedListBox.Items.Add("Any", true);
            repeatsCheckedListBox.Items.AddRange(MoonServer.Constants.GetFilter("repeats").Categories.ToArray());
            bmarkCheckedListBox.Items.Add("Any", true);
            bmarkCheckedListBox.Items.Add(BMARK_YES);
            bmarkCheckedListBox.Items.Add(BMARK_NO);
        }

        private void autoExportBtn_Click(object sender, EventArgs e)
        {
            List<Control> controlList = new List<Control>(new Control[] { autoExportBtn, gradeCheckedListBox, ratingCheckedListBox, repeatsCheckedListBox, bmarkCheckedListBox });
            foreach (Control c in controlList) { c.Enabled = false; }

            if (folderDlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (string iGrade in gradeCheckedListBox.CheckedItems)
            {
                foreach (string iRating in ratingCheckedListBox.CheckedItems)
                {
                    foreach (string iRepeats in repeatsCheckedListBox.CheckedItems)
                    {
                        foreach (string iBmark in bmarkCheckedListBox.CheckedItems)
                        {
                            ExportProblems(iGrade, iRating, iRepeats, iBmark, folderDlg.SelectedPath, false);
                        }
                    }
                }
            }
            foreach (Control c in controlList) { c.Enabled = true; }
        }

        protected void ExportProblems(string grade, string rating, string repeats, string benchmark, string folder, bool askOverwrite)
        {
            StatusTextBox.AppendText("Exporting " + String.Join("/", grade, rating, repeats, benchmark + "..."));
            string bmarkStr = "Any";
            if (benchmark.Equals(BMARK_YES)) bmarkStr = "Y";
            if (benchmark.Equals(BMARK_NO)) bmarkStr = "N";
            string filterName = String.Join("_", grade, rating, repeats, bmarkStr);

            int ratingChoice = 0;
            if (rating != "Any") { ratingChoice = int.Parse(rating); }
            int repeatsChoice = 0;
            if (repeats != "Any") { repeatsChoice = int.Parse(repeats); }

            // Find the problems we want
            IEnumerable<Problem> matchingProbs = moonServer.Problems.Where(prb =>
                (grade.Equals("Any") || prb.Grade.AmericanName.Equals(grade)) &&
                (rating.Equals("Any") || prb.Rating >= ratingChoice) &&
                (repeats.Equals("Any") || prb.Repeats >= repeatsChoice) &&
                (benchmark.Equals("Any") || (prb.IsBenchmark && benchmark.Equals(BMARK_YES)) || (!prb.IsBenchmark && benchmark.Equals(BMARK_NO)))
            );

            // Key - problem's MoonID. Value - offset in data file
            Dictionary<int, int> probOffsets = new Dictionary<int, int>();
            int curOffset = 0;

            // Write data file containing complete problem info
            string dataFileName = String.Format(@"{0}\{1}.dat", folder, filterName);
            if (askOverwrite && File.Exists(dataFileName))
            {
                DialogResult owrite = MessageBox.Show("File " + dataFileName + " already exists. Overwrite?",
                    "File Exists", MessageBoxButtons.YesNo);
                if (owrite == DialogResult.No) { return; }
            }
            String probData, probCountStr;
            int probCount;
            StreamWriter f = new StreamWriter(dataFileName)
            {
                NewLine = "\n"
            };
            foreach (Problem p in matchingProbs)
            {
                probData = ProblemAsString(p);
                probOffsets.Add(p.MoonID, curOffset);
                f.WriteLine(probData);
                curOffset += System.Text.Encoding.UTF8.GetByteCount(probData) + 1;
            }
            f.Close();

            // Write list file containing problems in order with offset of each one in data file
            string listFileName = String.Format(@"{0}\{1}_name.lst", folder, filterName);
            List<int> pageOffsets = new List<int>();
            StreamWriter l = new StreamWriter(listFileName)
            {
                NewLine = "\n"
            };

            probCountStr = String.Format("{0}", matchingProbs.Count());
            probCount = 0;
            l.WriteLine(probCountStr);
            curOffset = System.Text.Encoding.UTF8.GetByteCount(probCountStr) + 1;

            foreach (Problem p in matchingProbs.OrderBy(p => p.Name))
            {
                probData = p.MoonID + ":" + probOffsets[p.MoonID];
                l.WriteLine(probData);
                if (probCount % pageSizeUpDown.Value == 0)
                {
                    pageOffsets.Add(curOffset);
                }
                curOffset += System.Text.Encoding.UTF8.GetByteCount(probData) + 1;
                probCount++;
            }
            l.WriteLine(String.Join(":", pageOffsets));
            l.Close();

            pageOffsets.Clear();
            probCount = 0;
            curOffset = 0;

            listFileName = String.Format(@"{0}\{1}_rpts.lst", folder, filterName);
            l = new StreamWriter(listFileName)
            {
                NewLine = "\n"
            };
            probCountStr = String.Format("{0}", matchingProbs.Count());
            l.WriteLine(probCountStr);
            curOffset = System.Text.Encoding.UTF8.GetByteCount(probCountStr) + 1;
            foreach (Problem p in matchingProbs.OrderByDescending(p => p.Repeats))
            {
                probData = p.MoonID + ":" + probOffsets[p.MoonID];
                l.WriteLine(probData);
                if (probCount % pageSizeUpDown.Value == 0)
                {
                    pageOffsets.Add(curOffset);
                }
                curOffset += System.Text.Encoding.UTF8.GetByteCount(probData) + 1;
                probCount++;
            }
            l.WriteLine(String.Join(":", pageOffsets));
            l.Close();

            StatusTextBox.AppendText("Done\r\n");
        }

        private void exportListsBtn_Click(object sender, EventArgs e)
        {
            if (folderDlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string listDir = String.Format(@"{0}\l", folderDlg.SelectedPath);
            if (!Directory.Exists(listDir))
            {
                if (File.Exists(listDir))
                {
                    StatusTextBox.AppendText("ERROR: File '" + listDir + "' exists." + "\r\n");
                    return;
                }
                try
                {
                    Directory.CreateDirectory(listDir);
                }
                catch (Exception ex)
                {
                    StatusTextBox.AppendText("Error creating '" + listDir + "': " + ex.Message + "\r\n");
                    return;
                }
            }
            foreach (ProblemList pl in moonServer.ProblemLists)
            {
                StatusTextBox.AppendText("Exporting list '" + pl.Name + "'\r\n");
                string dataFileName = String.Format(@"{0}\{1}.dat", listDir, pl.Name);
                StreamWriter f = new StreamWriter(dataFileName)
                {
                    NewLine = "\n"
                };
                List<int> pageOffsets = new List<int>();
                int curOffset = 0;
                string probCountStr = String.Format("{0}", pl.ProblemListEntries.Count);
                f.WriteLine(probCountStr);
                curOffset += System.Text.Encoding.UTF8.GetByteCount(probCountStr) + 1;

                string probData;
                int probCount = 0;
                foreach (ProblemListEntry ple in pl.ProblemListEntries)
                {
                    probData = ProblemAsString(ple.Problem);
                    f.WriteLine(probData);
                    if (probCount % pageSizeUpDown.Value == 0)
                    {
                        pageOffsets.Add(curOffset);
                    }
                    curOffset += System.Text.Encoding.UTF8.GetByteCount(probData) + 1;
                    probCount++;
                }
                if (probCount != pl.ProblemListEntries.Count)
                {
                    StatusTextBox.AppendText("ERROR - mismatching counts\r\n");
                }

                f.WriteLine(String.Join(":", pageOffsets));
                f.Close();
                StatusTextBox.AppendText(String.Format("Wrote {0} problems to {1}\r\n", probCount, dataFileName));
            }
            StatusTextBox.AppendText("Done.\r\n");
        }
    }
}