using System.Collections.Generic;

namespace MoonServer
{
    public static class Constants
    {
        public static string CacheKey = "LAST_DB_UPDATE";
        public static string GradeKey = "MOONSERVER_GRADES";
        public static List<int> RepeatThresholds = new List<int>{ 0, 1, 5, 20, 50, 100 };
        public static List<int> Ratings = new List<int> { 0, 1, 2, 3 };
    }
}