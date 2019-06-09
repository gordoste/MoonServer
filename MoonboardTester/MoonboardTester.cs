using MoonServer;
using MoonServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoonboardTester
{
    public partial class MoonboardTester : Form
    {
        private readonly MoonServerDB db;
        private readonly TextBoxStreamWriter logStream;
        private readonly MoonboardClient client;


        public MoonboardTester()
        {
            InitializeComponent();
            string relative = @"..\..\..\MoonServer\App_Data\";
            string absolute = Path.GetFullPath(relative);
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);
            logStream = new TextBoxStreamWriter(LogTextBox);
            db = new MoonServerDB();
            client = new MoonboardClient(db, logStream);
        }

        private void ChooseBtn_Click(object sender, EventArgs e)
        {
            client.ShowProblem(int.Parse(ProblemTextBox.Text));
        }

        public class TextBoxStreamWriter : TextWriter
        {
            TextBox _output = null;

            public TextBoxStreamWriter(TextBox output)
            {
                _output = output;
            }

            public override void Write(char value)
            {
                base.Write(value);
                _output.AppendText(value.ToString()); // When character data is written, append it to the text box.
            }

            public override Encoding Encoding
            {
                get { return System.Text.Encoding.UTF8; }
            }
        }
    }
}
