using System.IO;

namespace DAoCToolSuite.CharacterTool.Files
{
    internal static class WriteCharacterIni
    {
        public static void WriteCharacter(string fileName, CharacterINI data)
        {
            string toWrite = string.Empty;

            foreach (string section in data.DATA.Keys)
            {
                toWrite += $"[{section}]\r\n";
                foreach (KeyValuePair<string, string> pair in data.DATA[section])
                {
                    toWrite += $"{pair.Key}={pair.Value}\r\n";
                }
            }
            toWrite = toWrite.Replace("DUPLICATEFIX", "LFGClass2");
            using StreamWriter writer = new(fileName, false);
            writer.Write(toWrite);
        }
    }
}
