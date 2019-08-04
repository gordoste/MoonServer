using MoonServer.Models;
using MoonServer.Models.Proxy;
using MoonServer.Models.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DataLoader
{
    public partial class LoadDataForm : Form
    {
        private MoonServerDB moonServer = new MoonServerDB();
        private List<RadioButton> _radioButtons;

        public LoadDataForm()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", "C:\\Users\\stephen\\Desktop\\MoonServer\\MoonServer\\App_Data");
            InitializeComponent();
            _radioButtons = new List<RadioButton>{
                GradesRadioBtn, ProblemsRadioBtn,
                HoldsRadioBtn, HoldSetupsRadioBtn,
                HoldPlacementsRadioBtn, PositionsRadioBtn
            };
        }

        private void ChooseFileBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ValidateInput();
            }

        }

        private void ValidateInput()
        {
            bool valid = _radioButtons.Any(rb => rb.Checked);
            LoadBtn.Enabled = valid;
            SaveBtn.Enabled = valid;
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            LoaderUtils.DataType dataType = GetDataType();
            StatusTextBox.AppendText("Loading " + LoaderUtils.DataTypeName(dataType).ToLower() + "(s) from file '" + openFileDialog.FileName + "'\n");
            String json = File.ReadAllText(openFileDialog.FileName);
            ProgressBar.Visible = true;
            ProgressLbl.Visible = true;
            try
            {
                int recordsLoaded = LoadJSON(json, dataType);
                StatusTextBox.AppendText(String.Format("Processed {0} {1}(s) from {2}\n",
                    recordsLoaded, LoaderUtils.DataTypeName(dataType).ToLower(), openFileDialog.FileName));
            }
            catch (DuplicateException de)
            {
                StatusTextBox.AppendText("Error: " + de.Message + "\n");
            }
            catch (JsonReaderException je)
            {
                StatusTextBox.AppendText("Error: " + je.Message + "\n");
            }
            ProgressBar.Visible = false;
            ProgressLbl.Visible = false;
        }

        private int LoadJSON(string json, LoaderUtils.DataType dataType)
        {
            int recordsToProcess = 0, recordsLoaded = 0, recordsProcessed = 0;
            switch (dataType)
            {
                case LoaderUtils.DataType.Grade:
                    List<GradeProxy> gradeProxies = JsonConvert.DeserializeObject<List<GradeProxy>>(json);
                    recordsToProcess = gradeProxies.Count;
                    ProgressBar.Maximum = recordsToProcess;
                    foreach (GradeProxy p in gradeProxies)
                    {
                        if (LoadGrade(p)) { recordsLoaded++; }
                        recordsProcessed++;
                        ProgressBar.Value = recordsProcessed;
                        ProgressLbl.Text = String.Format("{0} / {1}", recordsProcessed, recordsToProcess);
                        Update();
                    }
                    break;
                case LoaderUtils.DataType.Hold:
                    List<HoldProxy> holdProxies = JsonConvert.DeserializeObject<List<HoldProxy>>(json);
                    recordsToProcess = holdProxies.Count;
                    ProgressBar.Maximum = recordsToProcess;
                    foreach (HoldProxy p in holdProxies)
                    {
                        if (LoadHold(p)) { recordsLoaded++; }
                        recordsProcessed++;
                        ProgressBar.Value = recordsProcessed;
                        ProgressLbl.Text = String.Format("{0} / {1}", recordsProcessed, recordsToProcess);
                        Update();
                    }
                    break;
                case LoaderUtils.DataType.HoldPlacement:
                    List<HoldPlacementProxy> holdPlacementProxies = JsonConvert.DeserializeObject<List<HoldPlacementProxy>>(json);
                    recordsToProcess = holdPlacementProxies.Count;
                    ProgressBar.Maximum = recordsToProcess;
                    foreach (HoldPlacementProxy p in holdPlacementProxies)
                    {
                        if (LoadHoldPlacement(p)) { recordsLoaded++; }
                        recordsProcessed++;
                        ProgressBar.Value = recordsProcessed;
                        ProgressLbl.Text = String.Format("{0} / {1}", recordsProcessed, recordsToProcess);
                        Update();
                    }
                    break;
                case LoaderUtils.DataType.HoldSetup:
                    List<HoldSetupProxy> holdSetupProxies = JsonConvert.DeserializeObject<List<HoldSetupProxy>>(json);
                    recordsToProcess = holdSetupProxies.Count;
                    ProgressBar.Maximum = recordsToProcess;
                    foreach (HoldSetupProxy p in holdSetupProxies)
                    {
                        if (LoadHoldSetup(p)) { recordsLoaded++; }
                        recordsProcessed++;
                        ProgressBar.Value = recordsProcessed;
                        ProgressLbl.Text = String.Format("{0} / {1}", recordsProcessed, recordsToProcess);
                        Update();
                    }
                    break;
                case LoaderUtils.DataType.Position:
                    List<PositionProxy> positionProxies = JsonConvert.DeserializeObject<List<PositionProxy>>(json);
                    recordsToProcess = positionProxies.Count;
                    ProgressBar.Maximum = recordsToProcess;
                    foreach (PositionProxy p in positionProxies)
                    {
                        if (LoadPosition(p)) { recordsLoaded++; }
                        recordsProcessed++;
                        ProgressBar.Value = recordsProcessed;
                        ProgressLbl.Text = String.Format("{0} / {1}", recordsProcessed, recordsToProcess);
                        Update();
                    }
                    break;
                case LoaderUtils.DataType.Problem:
                    if (PageTextBox.Text.Length == 0)
                    {
                        break;
                    }
                    int pageSize = 500;
                    IEnumerable<ProblemProxy> problemProxies = JsonConvert.DeserializeObject<List<ProblemProxy>>(json)
                        .Skip(Int32.Parse(PageTextBox.Text) * pageSize)
                        .Take(pageSize);

                    recordsToProcess = problemProxies.Count();
                    ProgressBar.Maximum = recordsToProcess;
                    foreach (ProblemProxy p in problemProxies)
                    {
                        if (LoadProblem(p)) { recordsLoaded++; }
                        recordsProcessed++;
                        ProgressBar.Value = recordsProcessed;
                        ProgressLbl.Text = String.Format("{0} / {1}", recordsProcessed, recordsToProcess);
                        Update();
                    }
                    break;
                default: throw new ArgumentException("Invalid type");
            }
            return recordsLoaded;
        }

        private bool LoadPosition(PositionProxy p)
        {
            Position pos = Deproxy.GetPosition(p);
            string objTypeAndName = p.GetDataType().ToLower() + ": " + p.FriendlyString();
            if (moonServer.Positions.Any(o => o.Id.Equals(pos.Id) || o.Name.Equals(pos.Name)))
            {
                if (ErrorOnDupCheckBox.Checked) { throw new DuplicateException(objTypeAndName); }
                StatusTextBox.AppendText("Skipping duplicate " + objTypeAndName + "\n");
                return false;
            }
            StatusTextBox.AppendText("Adding " + objTypeAndName + "\n");
            moonServer.Positions.Add(pos);
            moonServer.SaveChanges();
            return true;
        }

        private bool LoadHoldSetup(HoldSetupProxy p)
        {
            HoldSetup hs = Deproxy.GetHoldSetup(p);
            string objTypeAndName = p.GetDataType().ToLower() + ": " + p.FriendlyString();
            if (moonServer.HoldSetups.Any(o => o.Id.Equals(hs.Id) || o.Name.Equals(hs.Name)))
            {
                if (ErrorOnDupCheckBox.Checked) { throw new DuplicateException(objTypeAndName); }
                StatusTextBox.AppendText("Skipping duplicate " + objTypeAndName + "\n");
                return false;
            }
            StatusTextBox.AppendText("Adding " + objTypeAndName + "\n");
            moonServer.HoldSetups.Add(hs);
            moonServer.SaveChanges();
            return true;
        }

        private bool LoadHoldPlacement(HoldPlacementProxy p)
        {
            HoldPlacement hp = Deproxy.GetHoldPlacement(p, moonServer);
            string objTypeAndName = p.GetDataType().ToLower() + ": " + p.FriendlyString();
            if (moonServer.HoldPlacements.Any(o => o.Id.Equals(hp.Id)))
            {
                if (ErrorOnDupCheckBox.Checked) { throw new DuplicateException(objTypeAndName); }
                StatusTextBox.AppendText("Skipping duplicate " + objTypeAndName + "\n");
                return false;
            }
            StatusTextBox.AppendText("Adding " + objTypeAndName + "\n");
            moonServer.HoldPlacements.Add(hp);
            moonServer.SaveChanges();
            return true;
        }

        private bool LoadHold(HoldProxy p)
        {
            Hold h = Deproxy.GetHold(p);
            string objTypeAndName = p.GetDataType().ToLower() + ": " + p.FriendlyString();
            if (moonServer.Holds.Any(o => o.Name.Equals(h.Name)))
            {
                if (ErrorOnDupCheckBox.Checked) { throw new DuplicateException(objTypeAndName); }
                StatusTextBox.AppendText("Skipping duplicate " + objTypeAndName + "\n");
                return false;
            }
            StatusTextBox.AppendText("Adding " + objTypeAndName + "\n");
            moonServer.Holds.Add(h);
            moonServer.SaveChanges();
            return true;
        }

        private bool LoadGrade(GradeProxy p)
        {
            Grade g = Deproxy.GetGrade(p);
            string objTypeAndName = p.GetDataType().ToLower() + ": " + p.FriendlyString();
            if (moonServer.Grades.Any(o => o.Id == g.Id || o.EuroName.Equals(g.EuroName) || o.AmericanName.Equals(g.AmericanName)))
            {
                if (ErrorOnDupCheckBox.Checked) { throw new DuplicateException(objTypeAndName); }
                StatusTextBox.AppendText("Skipping duplicate " + objTypeAndName + "\n");
                return false;
            }
            StatusTextBox.AppendText("Adding " + objTypeAndName + "\n");
            moonServer.Grades.Add(g);
            moonServer.SaveChanges();
            return true;
        }

        private bool LoadProblem(ProblemProxy p)
        {
            Problem prb = Deproxy.GetProblem(p, moonServer);
            string objTypeAndName = p.GetDataType().ToLower() + ": " + p.FriendlyString();
            if (moonServer.Problems.Any(o => o.Name.Equals(prb.Name)))
            {
                if (ErrorOnDupCheckBox.Checked) { throw new DuplicateException("Duplicate name for " + objTypeAndName); }
                StatusTextBox.AppendText("Skipping duplicate " + objTypeAndName + "\n");
                return false;
            }
            IEnumerable<Problem> sameIDProblems = moonServer.Problems.Where(o => o.MoonID.Equals(prb.MoonID));
            if (sameIDProblems.Count() > 0)
            {
                if (!ReplaceCheckBox.Checked)
                {
                    foreach (Problem sameIDproblem in sameIDProblems)
                    {
                        if (!sameIDproblem.Name.Equals(prb.Name))
                        {
                            throw new DuplicateException(
                                String.Format("Same ID different name. MoonID={0} Name={1}/{2} ID={3}/{4}",
                                    prb.MoonID, prb.Name, sameIDproblem.Name, prb.Id, sameIDproblem.Id));
                        }
                    }
                    if (ErrorOnDupCheckBox.Checked) { throw new DuplicateException("Duplicate MoonID for " + objTypeAndName); }
                    StatusTextBox.AppendText("Skipping " + objTypeAndName + " (duplicate MoonID)\n");
                    return false;
                }
                else
                {
                    StatusTextBox.AppendText(String.Format("Removing {0} problem(s) with same ID as {1}\n", sameIDProblems.Count(), objTypeAndName));
                    foreach (Problem sameIDproblem in sameIDProblems)
                    {
                        moonServer.ProblemPositions.RemoveRange(sameIDproblem.ProblemPositions);
                        moonServer.StartProblemPositions.RemoveRange(sameIDproblem.StartProblemPositions);
                        moonServer.EndProblemPositions.RemoveRange(sameIDproblem.EndProblemPositions);
                        moonServer.Problems.Remove(sameIDproblem);
                    }
                }
            }
            StatusTextBox.AppendText("Adding " + objTypeAndName + "\n");
            moonServer.Problems.Add(prb);
            moonServer.SaveChanges();
            prb = moonServer.Problems.First(_prb => _prb.Name.Equals(p.Name)); // Pull back the allocated ID
            foreach (string posName in p.Positions)
            {
                moonServer.ProblemPositions.Add(new ProblemPosition
                {
                    PositionId = moonServer.Positions.First(pos => pos.Name.Equals(posName)).Id,
                    ProblemId = prb.Id
                });
            }
            moonServer.SaveChanges();
            foreach (string posName in p.StartPositions)
            {
                moonServer.StartProblemPositions.Add(new StartProblemPosition
                {
                    PositionId = moonServer.Positions.First(pos => pos.Name.Equals(posName)).Id,
                    ProblemId = prb.Id
                });
            }
            moonServer.SaveChanges();
            foreach (string posName in p.EndPositions)
            {
                moonServer.EndProblemPositions.Add(new EndProblemPosition
                {
                    PositionId = moonServer.Positions.First(pos => pos.Name.Equals(posName)).Id,
                    ProblemId = prb.Id
                });
            }
            moonServer.SaveChanges();
            return true;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            LoaderUtils.DataType dataType = GetDataType();
            StatusTextBox.AppendText("Saving " + LoaderUtils.DataTypeName(dataType).ToLower() + " to file '" + saveFileDialog.FileName + "'\n");
            using (moonServer = new MoonServerDB())
            {
                IEnumerable<Object> data;
                if (dataType.Equals(LoaderUtils.DataType.Problem))
                {
                    data = GetData(moonServer).ToList().ConvertAll(o => (ProblemProxy)o).OrderBy(p => p.MoonID);
                }
                else
                {
                    data = GetData(moonServer);
                }
                data.ToList();
                File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(data, Formatting.Indented));
            }
        }

        private LoaderUtils.DataType GetDataType()
        {
            if (ProblemsRadioBtn.Checked) { return LoaderUtils.DataType.Problem; }
            else if (HoldPlacementsRadioBtn.Checked) { return LoaderUtils.DataType.HoldPlacement; }
            else if (HoldSetupsRadioBtn.Checked) { return LoaderUtils.DataType.HoldSetup; }
            else if (HoldsRadioBtn.Checked) { return LoaderUtils.DataType.Hold; }
            else if (PositionsRadioBtn.Checked) { return LoaderUtils.DataType.Position; }
            else if (GradesRadioBtn.Checked) { return LoaderUtils.DataType.Grade; }
            else { throw new ArgumentException("No radio buttons checked"); }
        }

        private IEnumerable<Object> GetData(MoonServerDB msdb)
        {
            if (ProblemsRadioBtn.Checked)
            {
                return new List<Problem>(msdb.Problems)
                    .ConvertAll(p => new ProblemProxy(p));
            }
            else if (HoldPlacementsRadioBtn.Checked)
            {
                return new List<HoldPlacement>(msdb.HoldPlacements)
                    .ConvertAll(hp => new HoldPlacementProxy(hp));
            }
            else if (HoldSetupsRadioBtn.Checked)
            {
                return new List<HoldSetup>(msdb.HoldSetups)
                    .ConvertAll(hs => new HoldSetupProxy(hs));
            }
            else if (HoldsRadioBtn.Checked)
            {
                return new List<Hold>(msdb.Holds)
                    .ConvertAll(h => new HoldProxy(h));
            }
            else if (PositionsRadioBtn.Checked)
            {
                return new List<Position>(msdb.Positions)
                    .ConvertAll(p => new PositionProxy(p));
            }
            else if (GradesRadioBtn.Checked)
            {
                return new List<Grade>(msdb.Grades)
                    .ConvertAll(g => new GradeProxy(g));
            }
            else { throw new ArgumentException("No radio buttons checked"); }
        }

        private void ProblemsRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            ValidateInput();
            ReplaceCheckBox.Enabled = ProblemsRadioBtn.Checked;
        }

        private void HoldPlacementsRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void GradesRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void HoldSetupsRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void HoldsRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void PositionsRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void Check1Button_Click(object sender, EventArgs e)
        {
            StatusTextBox.AppendText("Checking problems for missing Positions...\n");
            foreach (Problem p in moonServer.Problems.Where(p => p.ProblemPositions.Count == 0))
            {
                StatusTextBox.AppendText(String.Format("Problem #{0} ({1}) has no position\n", p.Id, p.Name));
            }
            StatusTextBox.AppendText("Checking problems for missing StartPositions...\n");
            foreach (Problem p in moonServer.Problems.Where(p => p.StartProblemPositions.Count == 0))
            {
                StatusTextBox.AppendText(String.Format("Problem #{0} ({1}) has no start positions\n", p.Id, p.Name));
            }
            StatusTextBox.AppendText("Checking problems for missing EndPositions...\n");
            foreach (Problem p in moonServer.Problems.Where(p => p.EndProblemPositions.Count == 0))
            {
                StatusTextBox.AppendText(String.Format("Problem #{0} ({1}) has no end positions\n", p.Id, p.Name));
            }
        }
    }

    public class DuplicateException : Exception
    {
        public DuplicateException() : base() { }
        public DuplicateException(string s) : base(s) { }
    }
}
