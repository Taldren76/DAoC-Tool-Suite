using System.IO;

namespace DAoCToolSuite.CharacterTool
{ 
    public class ParseDirectory
    {
        private string Folder { get; set; }
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

    }
}
