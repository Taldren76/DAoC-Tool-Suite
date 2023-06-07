using System.Windows.Forms;
using Logger;
using DAoCToolSuite.ChimpTool.Selenium;

namespace DAoCToolSuite.ChimpTool
{
    internal static class Program
    {
        private static LogManager Logger => LogManager.Instance;
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
            catch (System.Exception ex)
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
