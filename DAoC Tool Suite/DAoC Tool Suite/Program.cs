using System.Diagnostics;
using System.IO;

namespace DAoCToolSuite
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        private static void Main()
        {
            if (File.Exists("DAoCToolSuite.log"))
                File.Delete("DAoCToolSuite.log");
            _ = Trace.Listeners.Add(new TextWriterTraceListener("DAoCToolSuite.log"));
            Trace.AutoFlush = true;
            Trace.WriteLine($"***************************************************");
            Trace.WriteLine($"* Log Started: {DateTime.Now:MM/dd/yyyy HH:mm:ss}                *");
            Trace.WriteLine($"***************************************************");
            bool instanceCountOne;
            using Mutex mtex = new(true, "DAoCToolSuite", out instanceCountOne);
            if (instanceCountOne)
            {
                try
                {
                    ApplicationConfiguration.Initialize();
                    Application.Run(new DAoCToolSuiteForm());
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
                _ = MessageBox.Show("An DAoCToolSuite instance is already running");
            }
        }
        public static void TraceLog(string? message)
        {
            string toWrite = $"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: {message ?? ""}";
            Trace.WriteLine(toWrite);
        }
    }
}