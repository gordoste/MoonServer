using System;
using System.Collections.Generic;

namespace MoonServer.Models.Proxy
{
    public class ProblemProxy : Proxy
    {
        public long Id;
        public int MoonID;
        public string Name;
        public bool IsBenchmark;
        public string GradeName;
        public string HoldSetupName;
        public int Repeats;
        public int Rating;
        public DateTime DateAdded;
        public List<string> Positions = new List<string>();
        public List<string> StartPositions = new List<string>();
        public List<string> EndPositions = new List<string>();

        public ProblemProxy() { }

        public ProblemProxy(Problem _p)
        {
            Id = _p.Id;
            MoonID = _p.MoonID;
            Name = _p.Name;
            IsBenchmark = _p.IsBenchmark;
            GradeName = _p.Grade.EuroName;
            HoldSetupName = _p.HoldSetup.Name;
            Repeats = _p.Repeats;
            Rating = _p.Rating;
            DateAdded = _p.DateAdded;
            Positions = new List<string>(new List<ProblemPosition>(_p.ProblemPositions).ConvertAll(pp => pp.Position.Name));
            StartPositions = new List<string>(new List<StartProblemPosition>(_p.StartProblemPositions).ConvertAll(pp => pp.Position.Name));
            EndPositions = new List<string>(new List<EndProblemPosition>(_p.EndProblemPositions).ConvertAll(pp => pp.Position.Name));
        }

        public override string GetDataType()
        {
            return "Problem";
        }

        public override string FriendlyString()
        {
            return String.Format("{0}", Name);
        }
    }
}