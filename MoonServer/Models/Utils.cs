using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoonServer.Models
{
    // Convert enums to/from strings
    public static class Utils
    {
        public static Holdset ReadHoldsetString(string s)
        {
            switch (s)
            {
                case "Original": return Holdset.Original;
                case "Set A": return Holdset.SetA;
                case "Set B": return Holdset.SetB;
                case "Set C": return Holdset.SetC;
                case "Wooden": return Holdset.Wooden;
                default: throw new ArgumentException("Invalid holdset value");
            }
        }
        public static string HoldsetAsString(Holdset hs)
        {
            switch (hs)
            {
                case Holdset.Original: return "Original";
                case Holdset.SetA: return "Set A";
                case Holdset.SetB: return "Set B";
                case Holdset.SetC: return "Set C";
                case Holdset.Wooden: return "Wooden";
                default: throw new ArgumentException("Invalid enum value");
            }
        }

        public static Orientation ReadOrientationString(string s)
        {
            switch (s)
            {
                case "N": return Orientation.N;
                case "NE": return Orientation.NE;
                case "E": return Orientation.E;
                case "SE": return Orientation.SE;
                case "S": return Orientation.S;
                case "SW": return Orientation.SW;
                case "W": return Orientation.W;
                case "NW": return Orientation.NW;
                default: throw new ArgumentException("Invalid enum value");
            }
        }
        public static string OrientationAsString(Orientation o)
        {
            switch (o)
            {
                case Orientation.N: return "N";
                case Orientation.NE: return "NE";
                case Orientation.E: return "E";
                case Orientation.SE: return "SE";
                case Orientation.S: return "S";
                case Orientation.SW: return "SW";
                case Orientation.W: return "W";
                case Orientation.NW: return "NW";
                default: throw new ArgumentException("Invalid enum value");
            }
        }
    }
}