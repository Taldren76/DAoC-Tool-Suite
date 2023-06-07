using Logger;

namespace DAoCToolSuite
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        internal static LogManager Logger => LogManager.Instance;
        [STAThread]
        public static void Main()
        {

            try
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new DAoCToolSuiteForm());
            }
            catch (Exception ex) { 
                Logger.Error(ex);
            }
        }
    }
}