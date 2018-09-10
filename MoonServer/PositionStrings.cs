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
            Normal = p.ProblemPositions.ToList().ConvertAll(pos => ConvertHold(pos.Position.Name));
            Start = p.StartProblemPositions.ToList().ConvertAll(pos => ConvertHold(pos.Position.Name));
            End = p.EndProblemPositions.ToList().ConvertAll(pos => ConvertHold(pos.Position.Name));

            Normal = Normal.Except(Start.Concat(End)).ToList();
        }

        private string ConvertHold(string hold)
        {
            Match m = holdRegex.Match(hold);
            string colCode = m.Groups["1"].Value;
            int rowNum = int.Parse(m.Groups["2"].Value);
            char rowCode = (char)('A' + (rowNum - 1)); // Convert numbers 1-24 to letters A-X
            return string.Format("{0}{1}", colCode, rowCode);
        }
    }
}