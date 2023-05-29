using System.Configuration;
using DAoCToolSuite.ChimpTool.Logging;

namespace DAoCToolSuite.ChimpTool
{
    public static class AppSettings
    {
        private static readonly Logger Logger = new();
        #region AppSettings
        internal static void SetSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (!configuration.HasFile)
            {
                Logger.Warn($"No file found at {configuration.FilePath}");
                return;
            }

            if (configuration.AppSettings.Settings.AllKeys.Contains(key))
            {
                configuration.AppSettings.Settings[key].Value = value;
            }
            else
            {
                configuration.AppSettings.Settings.Add(key, value);
            }

            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
        internal static string? GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        #endregion
    }
}
