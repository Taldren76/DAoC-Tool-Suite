// See https://aka.ms/new-console-template for more information

namespace DAoCToolSuite.ResetLocations
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("Restoring Default Settings.");
            string ChimpToolPath = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\ChimpTool\");
            string CharacterToolPath = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\CharacterTool\");
            string LogToolPath = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\LogTool\");
            string LauncherPath = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\DAoC_Tool_Suite\");
            if(Directory.Exists(ChimpToolPath))
            {
                Directory.Delete(ChimpToolPath, true);
            }
            if(Directory.Exists(CharacterToolPath))
            {
                Directory.Delete(CharacterToolPath, true);
            }
            if(Directory.Exists(LogToolPath))
            {
                Directory.Delete(LogToolPath, true);
            }
            if(Directory.Exists(LauncherPath))
            {
                Directory.Delete(LauncherPath, true);
            }
        }
    }
}