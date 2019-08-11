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
        public static Configuration Config;
        public static Dictionary<string, string> FileSettings;

        static Constants() { Init(AppDomain.CurrentDomain.BaseDirectory); }

        public static void Init(string storageDir)
        {
            string constantsFile = string.Format("{0}\\{1}", storageDir, "constants.json");
            if (File.Exists(constantsFile)) {
                Config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(constantsFile));
            }

            string configFile = String.Format("{0}\\{1}", storageDir, "config.json");
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