using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MoonServer.Models;

namespace ProblemExport
{
    public partial class ProblemExportForm : Form
    {
        private MoonServerDB moonServer = new MoonServerDB();

        public ProblemExportForm()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", "C:\\Users\\gordo\\source\\repos\\MoonServer\\MoonServer\\App_Data");
            InitializeComponent();
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (folderDlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            foreach (Problem p in moonServer.Problems)
            {
                File.WriteAllText(folderDlg.SelectedPath + "\\" + p.MoonID + ".dat",
                    String.Join(",", p.ProblemPositions.ToList().ConvertAll(pp => pp.Position.Name)));
            }
        }
    }
}
