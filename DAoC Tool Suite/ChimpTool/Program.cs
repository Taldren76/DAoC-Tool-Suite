using DAoCToolSuite.ChimpTool.Selenium;
using System.Diagnostics;
using System.IO;

namespace DAoCToolSuite.ChimpTool
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (File.Exists("ChimpTool.log"))
                File.Delete("ChimpTool.log");
            _ = Trace.Listeners.Add(new TextWriterTraceListener("ChimpTool.log"));
            Trace.AutoFlush = true;
            Trace.WriteLine($"***************************************************");
            Trace.WriteLine($"* Log Started: {DateTime.Now:MM/dd/yyyy HH:mm:ss}                *");
            Trace.WriteLine($"***************************************************");
            bool instanceCountOne;
            using Mutex mtex = new(true, "ChimpTool", out instanceCountOne);
            if (instanceCountOne)
            {
                try
                {
                    ApplicationConfiguration.Initialize();
                    Application.Run(new MainForm());
                    mtex.ReleaseMutex();
                }
                catch (System.Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                }
                finally
                {
                    CamelotHerald.Quit();
                }
            }
            else
            {
                _ = MessageBox.Show("An ChimpTool instance is already running");
            }
        }
        public static void TraceLog(string? message)
        {
            string toWrite = $"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: {message ?? ""}";
            Trace.WriteLine(toWrite);
        }
    }
}
