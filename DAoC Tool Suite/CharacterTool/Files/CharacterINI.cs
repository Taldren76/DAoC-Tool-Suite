using IniParser;
using IniParser.Model;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DAoCToolSuite.CharacterTool.Files
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(CharacterINI))]
    internal class CharacterINI
    {
        [JsonProperty]
        public readonly string FilePath = string.Empty;
        [JsonProperty]
        public Dictionary<string, Dictionary<string, string>> DATA { get; private set; } = new();
        internal Dictionary<string, string> Panels
        {
            get => DATA["Panels"];
            set => DATA["Panels"] = value;
        }
        internal Dictionary<string, string> Macros
        {
            get => DATA["Macros"];
            set => DATA["Macros"] = value;
        }
        internal Dictionary<string, string> Chat
        {
            get => DATA["Chat"];
            set => DATA["Chat"] = value;
        }
        internal Dictionary<string, string> NameOptions
        {
            get => DATA["NameOptions"];
            set => DATA["NameOptions"] = value;
        }
        internal Dictionary<string, string> QuickBinds
        {
            get => DATA["QuickBinds"];
            set => DATA["QuickBinds"] = value;
        }
        internal Dictionary<string, string> ToolTips
        {
            get => DATA["ToolTips"];
            set => DATA["ToolTips"] = value;
        }
        internal Dictionary<string, string> Camera
        {
            get => DATA["Camera"];
            set => DATA["Camera"] = value;
        }

        public CharacterINI(string filepath)
        {
            FilePath = filepath;
            ParseFile();
        }

        public void OverwriteQuickbars(Dictionary<string, Dictionary<string, string>> data)
        {
            PurgeQuickbar("Quickbar");
            PurgeQuickbar("Quickbar2");
            PurgeQuickbar("Quickbar3");

            UpdateQuickbar("Quickbar");
            UpdateQuickbar("Quickbar2");
            UpdateQuickbar("Quickbar3");

            void PurgeQuickbar(string section)
            {
                if (data.ContainsKey(section))
                {
                    foreach (KeyValuePair<string, string> pair in data[section])
                    {
                        if (pair.Key.Contains("Hotkey_") && !pair.Value.Contains("Macro"))
                        {
                            _ = data[section].Remove(pair.Key);
                        }
                    }
                }
            }
            void UpdateQuickbar(string section)
            {
                if (data.ContainsKey(section))
                {
                    if (DATA.ContainsKey(section))
                    {
                        foreach (KeyValuePair<string, string> pair in data[section])
                        {
                            if (DATA[section].ContainsKey(pair.Key))
                            {
                                DATA[section][pair.Key] = pair.Value;
                            }
                            else
                            {
                                DATA[section].Add(pair.Key, pair.Value);
                            }
                        }
                    }
                    else
                    {
                        DATA.Add(section, data[section]);
                    }
                }
            }
        }

        public void PurgeQuickbars()
        {
            PurgeQuickbar("Quickbar");
            PurgeQuickbar("Quickbar2");
            PurgeQuickbar("Quickbar3");

            void PurgeQuickbar(string section)
            {
                if (DATA.ContainsKey(section))
                {
                    foreach (KeyValuePair<string, string> pair in DATA[section])
                    {
                        if (pair.Key.Contains("Hotkey_") && !pair.Value.Contains("Macro"))
                        {
                            _ = DATA[section].Remove(pair.Key);
                        }
                    }
                }
            }
        }

        private void ParseFile()
        {
            if (FilePath is null)
            {
                return;
            }

            Regex regex = new(Regex.Escape("LFGClass2"));
            string text = File.ReadAllText(FilePath);
            string text2 = regex.Replace(text, "DUPLICATEFIX", 1);

            byte[] byteArray = Encoding.ASCII.GetBytes(text2);
            MemoryStream memoryStream = new(byteArray);

            StreamReader sr = new(memoryStream);

            FileIniDataParser dataParser = new();
            IniData data = dataParser.ReadData(sr);

            SectionDataCollection sections = data.Sections;

            foreach (SectionData? section in sections)
            {
                Dictionary<string, string> sectionData = new();
                Console.WriteLine(section.ToString());
                KeyDataCollection _data = data[section.SectionName];
                foreach (KeyData? pair in _data)
                {
                    sectionData.Add(pair.KeyName, pair.Value);
                }
                DATA.Add(section.SectionName, sectionData);
            }
        }
    }
}
