using System;

using MoonServer.Models;
using MoonServer.Models.Serialization;

namespace DataLoader
{
    public class LoaderUtils
    {
        public enum DataType { Problem, Hold, Position, Grade, HoldPlacement, HoldSetup };
        public static string DataTypeName(DataType type)
        {
            switch (type)
            {
                case DataType.Grade: return "Grade";
                case DataType.Hold: return "Hold";
                case DataType.HoldPlacement: return "Hold Placement";
                case DataType.HoldSetup: return "Hold Setup";
                case DataType.Position: return "Position";
                case DataType.Problem: return "Problem";
                default: throw new ArgumentException("Invalid type");
            }
        }
    }
}