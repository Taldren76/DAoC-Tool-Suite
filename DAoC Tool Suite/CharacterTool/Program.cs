using System.Diagnostics;

namespace DAoCToolSuite.CharacterTool
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            _ = Trace.Listeners.Add(new TextWriterTraceListener("CharacterTool.log"));
            Trace.AutoFlush = true;
            Trace.WriteLine($"***************************************************");
            Trace.WriteLine($"* Log Started: {DateTime.Now:MM/dd/yyyy HH:mm:ss}                *");
            Trace.WriteLine($"***************************************************");

            //Trace.Indent();           
            bool instanceCountOne;
            using Mutex mtex = new(true, "CharacterTool", out instanceCountOne);
            if (instanceCountOne)
            {
                try
                {
                    TraceLog("Starting CharacterTool.Program");
                    ApplicationConfiguration.Initialize();
                    TraceLog("Finished ApplicationConfiguration.Initialize()");
                    Application.Run(new MainForm());
                    TraceLog("Finished Application.Run(new MainForm())");
                    mtex.ReleaseMutex();
                }
                catch (System.Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                }
            }
            else
            {
                _ = MessageBox.Show("An CharacterTool instance is already running");
            }
            //Trace.Unindent();
            Trace.Flush();
        }
        public static void TraceLog(string? message)
        {
            string toWrite = $"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: {message ?? ""}";
            Trace.WriteLine(toWrite);
        }
    }
}