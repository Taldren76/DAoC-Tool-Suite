using Logger;
using Newtonsoft.Json;
using System.IO;

namespace DAoCToolSuite.CharacterTool.Settings
{
    public class SettingsManager
    {
        private readonly object thisLock = new();
        private static readonly bool Initialized = false;

        private static readonly Settings DefaultValues = new()
        {
            AlwaysOnTop = SettingsDefault.AlwaysOnTop,
            DAoCCharacterFileDirectory = SettingsDefault.DAoCCharacterFileDirectory,
            JsonBackupFileFullPath = SettingsDefault.JsonBackupFileFullPath,
            DisplayedDatabaseColumnNames = SettingsDefault.DisplayedDatabaseColumnNames,
            DisplayedDataGridViewHeaderNames = SettingsDefault.DisplayedDataGridViewHeaderNames
        };

        private static LogManager Logger => LogManager.Instance;
        private Settings Settings { get; set; } = new Settings();
        public bool AlwaysOnTop
        {
            get => Settings.AlwaysOnTop ?? SettingsDefault.AlwaysOnTop;
            set
            {
                lock (thisLock)
                {
                    Settings.AlwaysOnTop = value;
                    SaveSettings();
                }
            }
        }

        public string RealmClasses
        {
            get => Settings.RealmClasses ?? SettingsDefault.RealmClasses;
            set
            {
                lock (thisLock)
                {
                    Settings.RealmClasses = value;
                    SaveSettings();
                }
            }
        }

        public string Servers
        {
            get => Settings.Servers ?? SettingsDefault.Servers;
            set
            {
                lock (thisLock)
                {
                    Settings.Servers = value;
                    SaveSettings();
                }
            }
        }

        public string DAoCCharacterFileDirectory
        {
            get => Environment.ExpandEnvironmentVariables(Settings.DAoCCharacterFileDirectory ?? SettingsDefault.DAoCCharacterFileDirectory);
            set
            {
                lock (thisLock)
                {
                    Settings.DAoCCharacterFileDirectory = value;
                    SaveSettings();
                }
            }
        }
        public string JsonBackupFileFullPath
        {
            get => Environment.ExpandEnvironmentVariables(Settings.JsonBackupFileFullPath ?? SettingsDefault.JsonBackupFileFullPath);
            set
            {
                lock (thisLock)
                {
                    Settings.JsonBackupFileFullPath = value;
                    SaveSettings();
                }
            }
        }

        public List<string> DisplayedDataGridViewHeaderNames
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => Settings.DisplayedDataGridViewHeaderNames?.Names ?? SettingsDefault.DisplayedDataGridViewHeaderNames.Names;
#pragma warning restore CS8603 // Possible null reference return.
            set
            {
                lock (thisLock)
                {
                    if (Settings.DisplayedDataGridViewHeaderNames is null)
                    {
                        Settings.DisplayedDataGridViewHeaderNames = new() { Names = value };
                    }
                    else
                    {
                        Settings.DisplayedDataGridViewHeaderNames.Names = value;
                    }

                    SaveSettings();
                }
            }
        }
        public List<string> DisplayedDatabaseColumnNames
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => Settings.DisplayedDatabaseColumnNames?.Names ?? SettingsDefault.DisplayedDatabaseColumnNames.Names;
#pragma warning restore CS8603 // Possible null reference return.
            set
            {
                lock (thisLock)
                {
                    if (Settings.DisplayedDatabaseColumnNames is null)
                    {
                        Settings.DisplayedDatabaseColumnNames = new() { Names = value };
                    }
                    else
                    {
                        Settings.DisplayedDatabaseColumnNames.Names = value;
                    }

                    SaveSettings();
                }
            }
        }
        private string FilePath { get; set; } = $"{Environment.CurrentDirectory}\\CharacterTool.Config";
        public SettingsManager()
        {
            Initialize();

            List<string> columns = new() { "DateOnly", "FirstName", "Realm", "Class", "Description" };
            List<string> headers = new() { "Date", "Name", "Realm", "Class", "Description" };
            string listC = string.Join(",", columns);
            string listH = string.Join(",", headers);
            _ = listC.Split(',').ToList();
            _ = listH.Split(',').ToList();
        }
        private void Initialize()
        {
            if (Initialized)
            {
                return;
            }

            if (File.Exists(FilePath))
            {
                LoadSettings();
                Logger.Debug($"Setting: AlwaysOnTop = {AlwaysOnTop}");
                Logger.Debug($"Setting: DAoCCharacterFileDirectory = {DAoCCharacterFileDirectory}");
                Logger.Debug($"Setting: JsonBackupFileFullPath = {JsonBackupFileFullPath}");
                Logger.Debug($"Setting: DisplayedDatabaseColumnNames = {DisplayedDatabaseColumnNames.Aggregate((x, y) => x + "," + y).Replace("\n", " ")}");
                Logger.Debug($"Setting: DisplayedDataGridViewHeaderNames = {DisplayedDataGridViewHeaderNames.Aggregate((x, y) => x + "," + y).Replace("\n", " ")}");
            }
            else
            {
                DefaultSettings();
            }
            SaveSettings();
        }
        private void SaveSettings()
        {
            try
            {
                string json = JsonConvert.SerializeObject(Settings);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        private void LoadSettings()
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                Settings = JsonConvert.DeserializeObject<Settings>(json) ?? new Settings();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        private void DefaultSettings()
        {
            Settings = DefaultValues;
        }
    }

    public class HeaderNames
    {
        [JsonProperty]
        public List<string>? Names { get; set; }
    }

    public class ColumnNames
    {
        [JsonProperty]
        public List<string>? Names { get; set; }
    }
}
