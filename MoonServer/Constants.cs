using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MoonServer
{
    public class Filter
    {
        public string Name { get; set; }
        public string CSharpAttr { get; set; }
        public string JsonAttr { get; set; }
        public string Type { get; set; }
        public List<int> Categories { get; set; }
    }

    public class Configuration
    {
        public Dictionary<string, string> Strings;
        public List<Filter> Filters;
    }

    public static class Constants
    {
        public static string CacheKey = "LAST_DB_UPDATE";
        public static string GradeKey = "MOONSERVER_GRADES";
        public static string ConstantsFile = "C:\\Users\\gordoste\\source\\repos\\MoonServer\\MoonServer\\constants.json";

        public static readonly Configuration config;
 
        static Constants()
        {
            config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(ConstantsFile));
        }

        public static string getString(string key)
        {
            return config.Strings[key];
        }
        public static Filter getFilter(string key)
        {
            foreach (Filter f in config.Filters)
            {
                if (f.Name.Equals(key))
                {
                    return f;
                }
            }
            return null;
        }
    }
}