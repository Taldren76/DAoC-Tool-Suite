﻿using System.IO;
using Newtonsoft.Json;
using ConfigSettings.Enums;

namespace ConfigSettings.ChimpTool
{
    public class ChimpTool
    {
        private readonly object thisLock = new();
        private static readonly bool Initialized = false;

        private static readonly Settings DefaultValues = new()
        {
            AlwaysOnTop = SettingsDefault.AlwaysOnTop,
            LastAccount = SettingsDefault.LastAccount,
            UseAPI = SettingsDefault.UseAPI,
            UseSelenium = SettingsDefault.UseSelenium,
            DAoCCharacterFileDirectory = SettingsDefault.DAoCCharacterFileDirectory,
            JsonBackupFileFullPath = SettingsDefault.JsonBackupFileFullPath,
            Server = SettingsDefault.Server,
            DisplayedDatabaseColumnNames = SettingsDefault.DisplayedDatabaseColumnNames,
            DisplayedDataGridViewHeaderNames = SettingsDefault.DisplayedDataGridViewHeaderNames
        };

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
        public string LastAccount
        {
            get => Settings.LastAccount ?? SettingsDefault.LastAccount;
            set
            {
                lock (thisLock)
                {
                    if (!value.Equals("SQLLibrary.AccountModel"))
                    {
                        Settings.LastAccount = value;
                        SaveSettings();
                    }
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
        public bool UseSelenium
        {
            get => Settings.UseSelenium ?? SettingsDefault.UseSelenium;
            set
            {
                lock (thisLock)
                {
                    Settings.UseSelenium = value;
                    SaveSettings();
                }

            }
        }
        public bool UseAPI
        {
            get => Settings.UseAPI ?? SettingsDefault.UseAPI;
            set
            {
                lock (thisLock)
                {
                    Settings.UseAPI = value;
                    SaveSettings();
                }
            }
        }
        public ServerCluster Server
        {
            get => Settings.Server ?? SettingsDefault.Server;
            set
            {
                lock (thisLock)
                {
                    Settings.Server = value;
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
        private string FilePath { get; set; } = $"{Environment.CurrentDirectory}\\ChimpTools.Config";
        public ChimpTool()
        {
            Initialize();

            List<string> columns = new() { "Name", "Realm", "Class", "Server", "RealmRank", "TotalRealmPoints", "TotalSoloKills", "TotalDeathBlows", "TotalKills", "TotalDeaths", "IRS", "RPNextRank", "RPLastUpdate", "BountyPoints" };
            List<string> headers = new() { "Name", "Realm", "Class", "Server", "Realm\nRank", "Realm\nPoints", "Solo\nKills", "Death\nBlows", "Kills", "Deaths", "IRS", "RP Next\nRank", "RP Last\nUpdate", "BountyPoints" };
            string listC = string.Join(",", columns);
            string listH = string.Join(",", headers);
            _ = listC.Split(',').ToList();
            _ = listH.Split(',').ToList();
        }

        public List<string> ListSettings()
        {
            List<string> toWrite = new()
            {
                $"Setting: AlwaysOnTop = {AlwaysOnTop}",
                $"Setting: LastAccount = {LastAccount}",
                $"Setting: UseAPI = {UseAPI}",
                $"Setting: UseSelenium = {UseSelenium}",
                $"Setting: DAoCCharacterFileDirectory = {DAoCCharacterFileDirectory}",
                $"Setting: JsonBackupFileFullPath = {JsonBackupFileFullPath}",
                $"Setting: Server = {Server}",
                $"Setting: DisplayedDatabaseColumnNames = {DisplayedDatabaseColumnNames.Aggregate((x, y) => x + "," + y).Replace("\n", " ")}",
                $"Setting: DisplayedDataGridViewHeaderNames = {DisplayedDataGridViewHeaderNames.Aggregate((x, y) => x + "," + y).Replace("\n", " ")}"
            };
            return toWrite;
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
            catch 
            {

            }
        }
        private void LoadSettings()
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                Settings = JsonConvert.DeserializeObject<Settings>(json) ?? new Settings();
            }
            catch
            {

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
