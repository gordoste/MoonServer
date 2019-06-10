using MoonServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MoonServer
{
    public class PositionStrings
    {
        public List<string> Normal;
        public List<string> Start;
        public List<string> End;

        public List<string> Top;
        public List<string> Middle;
        public List<string> Bottom;

        private readonly Regex holdRegex = new Regex(@"\b([A-K])(\d+)", RegexOptions.Compiled);

        public PositionStrings(Problem p)
        {
            Normal = p.ProblemPositions.ToList().ConvertAll(pos => pos.Position.Name);
            Start = p.StartProblemPositions.ToList().ConvertAll(pos => pos.Position.Name);
            End = p.EndProblemPositions.ToList().ConvertAll(pos => pos.Position.Name);

            Normal = Normal.Except(Start.Concat(End)).ToList();

            Top = new List<string>();
            Middle = new List<string>();
            Bottom = new List<string>();
            foreach (string s in Normal)
            {
                Match m = holdRegex.Match(s);
                string colCode = m.Groups["1"].Value;
                int rowNum = int.Parse(m.Groups["2"].Value);
                if (rowNum < 1 || rowNum > 17) { throw new InvalidHoldException(string.Format("Bad row num {0}", rowNum)); }
                if (rowNum <= 6)
                {
                    Bottom.Add(string.Format("{0}{1}", (char)(('A' - 1) + rowNum + 6), colCode));
                }
                else if (rowNum <= 12)
                {
                    Middle.Add(string.Format("{0}{1}", (char)(('A' - 1) + rowNum - 6), colCode));
                }
                else if (rowNum <= 17)
                {
                    Top.Add(string.Format("{0}{1}", (char)(('A' - 1) + rowNum - 12), colCode));
                }
                else
                {
                    throw new InvalidHoldException(string.Format("Impossible row {0}", rowNum));
                }
            }
            foreach (string s in Start) {
                Match m = holdRegex.Match(s);
                string colCode = m.Groups["1"].Value;
                int rowNum = int.Parse(m.Groups["2"].Value);
                if (rowNum > 6) { throw new InvalidHoldException(string.Format("Bad row num {0}", rowNum)); }
                Bottom.Add(string.Format("{0}{1}", (char)(('A' - 1) + rowNum), colCode));
            }
            foreach (string s in End)
            {
                Match m = holdRegex.Match(s);
                string colCode = m.Groups["1"].Value;
                int rowNum = int.Parse(m.Groups["2"].Value);
                if (rowNum != 18) { throw new InvalidHoldException(string.Format("Bad row num {0}", rowNum)); }
                Top.Add(string.Format("{0}{1}", (char)(('A' - 1) + rowNum - 12), colCode));
            }
        }
    }

    public class InvalidHoldException : System.Exception
    {
        public InvalidHoldException() : base() { }
        public InvalidHoldException(string message) : base(message) { }
    }
}