﻿// See https://aka.ms/new-console-template for more information

namespace DAoCToolsResetSettings
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Console.WriteLine("Restoring Default Settings.");
            string ChimpToolPath = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\ChimpTool\");
            string CharacterToolPath = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\CharacterTool\");
            string LogToolPath = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\LogTool\");
            string LauncherPath = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\DAoC_Tool_Suite\");
            string TaldInc = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Taldren, Inc\");
            string Tald_Inc = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Taldren Inc\");
            string VSx86 = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\VirtualStore\Program Files(x86)\Taldren, Inc\");
            string VS = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\VirtualStore\Program Files\Taldren Inc\");

            DelDirectory(ChimpToolPath);
            DelDirectory(CharacterToolPath);
            DelDirectory(LogToolPath);
            DelDirectory(LauncherPath);
            DelDirectory(TaldInc);
            DelDirectory(Tald_Inc);
            DelDirectory(VSx86);
            DelDirectory(VS);
        }

        private static void DelDirectory(string dirName)
        {
            if (Directory.Exists(dirName))
            {
                Console.WriteLine($"Deleting contents of {dirName}");
                Directory.Delete(dirName, true);
            }
        }
    }
}