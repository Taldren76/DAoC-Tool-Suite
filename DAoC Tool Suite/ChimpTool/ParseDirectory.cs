﻿using System.IO;

namespace DAoCToolSuite.ChimpTool
{
    public class ParseDirectory
    {
        private string Folder { get; set; }
        private string[]? _IGNFiles = null;
        public List<string> IGNFiles
        {
            get
            {
                if (_IGNFiles is null)
                    _IGNFiles = GetIgnFiles();
                return _IGNFiles.ToList();
            }
        }
        private string[]? _INIFiles = null;
        public List<string> INIFiles
        {
            get
            {
                if (_INIFiles is null)
                    _INIFiles = GetIniFiles();
                return _INIFiles.ToList();
            }
        }
        public Dictionary<string, int> Characters { get; private set; } = new Dictionary<string, int>();

        public ParseDirectory(string path)
        {
            Folder = path;
            PopulateCharacterList();
        }

        private string[] GetFiles()
        {

            string[] files = Directory.GetFiles(Folder);
            return files;
        }

        private string[] GetIniFiles()
        {
            string[] files = GetFiles();
            string[] iniFiles = files.Where(x => x.Contains(".ini")).ToArray();
            return iniFiles;
        }

        private string[] GetIgnFiles()
        {
            string[] files = GetFiles();
            string[] ignFiles = files.Where(x => x.Contains(".ign")).ToArray();
            return ignFiles;
        }

        public void PopulateCharacterList()
        {
            string[] files = GetIniFiles();
            Dictionary<string, int> CharCopies = new();
            foreach (string file in files)
            {
                try
                {
                    string fileName = file.Replace(Folder + @"\", "").Split('.').First();
                    string charName = fileName.Split('-').First();
                    int charServer = -1;
                    if (int.TryParse(fileName.Split('-').Last(), out charServer))
                    {
                        if (Characters.ContainsKey(charName))
                        {
                            if (CharCopies.ContainsKey(charName))
                            {
                                CharCopies[charName] = CharCopies[charName] + 1;

                            }
                            else
                            {
                                CharCopies.Add(charName, 1);

                            }
                            Characters.Add($"{charName} ({CharCopies[charName]})", charServer);
                        }
                        else
                        {
                            Characters.Add(charName, charServer);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        private string[] FindFiles(string searchString)
        {
            string[] files = Directory.GetFiles(Folder, searchString, SearchOption.TopDirectoryOnly);
            return files;
        }

        private string? FindIgnFileByCharacterName(string characterName)
        {
            string? fileName = IGNFiles.Where(x => x.Split('-').First() == characterName).FirstOrDefault();
            return fileName;
        }

        private string? FindIniFileByCharacterName(string characterName)
        {
            string? fileName = INIFiles.Where(x => x.Split("-").First() == characterName).FirstOrDefault();
            return fileName;
        }

    }
}
