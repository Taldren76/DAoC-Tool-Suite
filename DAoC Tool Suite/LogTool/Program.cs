using System.Diagnostics;
using System.IO;

namespace DAoCToolSuite.LogTool
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        private static void Main()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Taldren, Inc\\DAoC Tool Suite\\LogTool.log"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Taldren, Inc\\DAoC Tool Suite\\LogTool.log");
            }

            _ = Trace.Listeners.Add(new TextWriterTraceListener(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Taldren, Inc\\DAoC Tool Suite\\LogTool.log"));
            Trace.AutoFlush = true;
            Trace.WriteLine($"***************************************************");
            Trace.WriteLine($"* Log Started: {DateTime.Now:MM/dd/yyyy HH:mm:ss}                *");
            Trace.WriteLine($"***************************************************");
            bool instanceCountOne;
            using Mutex mtex = new(true, "LogTool", out instanceCountOne);
            if (instanceCountOne)
            {
                try
                {
                    ApplicationConfiguration.Initialize();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                    mtex.ReleaseMutex();
                }
                catch (System.ObjectDisposedException e)
                {
                    TraceLog(e.Message);
                    TraceLog(e.StackTrace);
                }
                catch (System.Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                }
            }
            else
            {
                _ = MessageBox.Show("A LogTool instance is already running");
            }
        }
        public static void TraceLog(string? message)
        {
            string toWrite = $"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: {message ?? ""}";
            Trace.WriteLine(toWrite);
        }
    }
}