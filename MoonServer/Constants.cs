using Newtonsoft.Json;
using System;
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
        public List<string> Categories { get; set; }
    }

    public class Configuration
    {
        public Dictionary<string, string> Strings;
        public List<Filter> Filters;
    }

    public static class Constants
    {
        public static string ConstantsFile = string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "constants.json");

        public static readonly Configuration Config;
        public static readonly Dictionary<string, string> FileSettings;

        static Constants()
        {
            Config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(ConstantsFile));
            string configFile = String.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "config.json");
            string a = JsonConvert.SerializeObject(Config.Strings);
            if (File.Exists(configFile))
            {
                FileSettings = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(configFile));
            }
        }

        public static string GetString(string key)
        {
            if (Config.Strings.TryGetValue(key, out string value)) { return value; }
            else { return null; }
        }

        public static Filter GetFilter(string key)
        {
            foreach (Filter f in Config.Filters)
            {
                if (f.Name.Equals(key))
                {
                    return f;
                }
            }
            return null;
        }
        public static string GetFileConfig(string key)
        {
            if (FileSettings.TryGetValue(key, out string val)) { return val; }
            return null;
        }
    }
}