using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoonServer.Models
{
    public class GradeComparer : IComparer<string>
    {
        private bool Ascending { get; set; }
        public GradeComparer(bool Ascending= true)
        {
            this.Ascending = Ascending;
        }
        public int Compare(string x, string y)
        {
            if (x.Substring(0, 1) == "V" && y.Substring(0, 1) == "V") {
                int gx = int.Parse(x.Substring(1));
                int gy = int.Parse(y.Substring(1));
                return Ascending ? (gx - gy) : (gy - gx);
            }
            return StringComparer.InvariantCulture.Compare(x, y);
        }
    }
}