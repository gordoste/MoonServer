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

        private readonly Regex holdRegex = new Regex(@"\b([A-K])(\d+)", RegexOptions.Compiled);

        public PositionStrings(Problem p)
        {
            Normal = p.ProblemPositions.ToList().ConvertAll(pos => pos.Position.Name);
            Start = p.StartProblemPositions.ToList().ConvertAll(pos => pos.Position.Name);
            End = p.EndProblemPositions.ToList().ConvertAll(pos => pos.Position.Name);

            Normal = Normal.Except(Start.Concat(End)).ToList().ConvertAll(s => ConvertHold(s));

            Start = Start.ConvertAll(s => ConvertStartHold(s));
            End = End.ConvertAll(s => ConvertEndHold(s));
        }

        private string ConvertHold(string hold)
        {
            Match m = holdRegex.Match(hold);
            string colCode = m.Groups["1"].Value;
            int rowNum = int.Parse(m.Groups["2"].Value);
            if (rowNum < 1 || rowNum > 17) { throw new InvalidHoldException(string.Format("ConvertHold: Bad row num {0}", rowNum)); }
            char rowCode = (char)('G' + (rowNum - 1)); // Convert numbers 1-17 to letters G-W
            return string.Format("{0}{1}", colCode, rowCode);
        }

        private string ConvertStartHold(string hold)
        {
            Match m = holdRegex.Match(hold);
            string colCode = m.Groups["1"].Value;
            int rowNum = int.Parse(m.Groups["2"].Value);
            if (rowNum < 1 || rowNum > 6) { throw new InvalidHoldException(string.Format("ConvertStartHold: Bad row num {0}", rowNum)); }
            char rowCode = (char)('A' + (rowNum - 1)); // Convert numbers 1-6 to letters A-F
            return string.Format("{0}{1}", colCode, rowCode);
        }

        private string ConvertEndHold(string hold)
        {
            Match m = holdRegex.Match(hold);
            string colCode = m.Groups["1"].Value;
            int rowNum = int.Parse(m.Groups["2"].Value);
            if (rowNum != 18) { throw new InvalidHoldException(string.Format("ConvertEndHold: Bad row num {0}", rowNum)); }
            char rowCode = 'X'; // Convert numbers 18 to letters X
            return string.Format("{0}{1}", colCode, rowCode);
        }
    }

    public class InvalidHoldException : System.Exception
    {
        public InvalidHoldException() : base() { }
        public InvalidHoldException(string message) : base(message) { }
    }
}