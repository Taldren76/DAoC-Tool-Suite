﻿using DAoCToolSuite.ChimpTool.Logging;
using DAoCToolSuite.ChimpTool.Selenium;

namespace DAoCToolSuite.ChimpTool
{
    internal static class Program
    {
        private static readonly Logger Logger = new();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                CamelotHerald.Quit();
            }
        }
    }
}
