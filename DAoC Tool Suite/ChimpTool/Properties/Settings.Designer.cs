﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAoCToolSuite.ChimpTool.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.6.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10, 10")]
        public global::System.Drawing.Point WindowLocation {
            get {
                return ((global::System.Drawing.Point)(this["WindowLocation"]));
            }
            set {
                this["WindowLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public long LoadCount {
            get {
                return ((long)(this["LoadCount"]));
            }
            set {
                this["LoadCount"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2001-01-01")]
        public global::System.DateTime NextRefreshAll {
            get {
                return ((global::System.DateTime)(this["NextRefreshAll"]));
            }
            set {
                this["NextRefreshAll"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2001-01-01")]
        public global::System.DateTime NextRefresh {
            get {
                return ((global::System.DateTime)(this["NextRefresh"]));
            }
            set {
                this["NextRefresh"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2001-01-01")]
        public global::System.DateTime NextLoadAll {
            get {
                return ((global::System.DateTime)(this["NextLoadAll"]));
            }
            set {
                this["NextLoadAll"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%ProgramFiles(x86)%\\Electronic Arts\\Dark Age of Camelot")]
        public string GameDllLocation {
            get {
                return ((string)(this["GameDllLocation"]));
            }
            set {
                this["GameDllLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%AppData%\\Electronic Arts\\Dark Age of Camelot\\LotM")]
        public string DAoCCharacterFileDirectory {
            get {
                return ((string)(this["DAoCCharacterFileDirectory"]));
            }
            set {
                this["DAoCCharacterFileDirectory"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{\"Names\": [\"Name\", \"Realm\", \"Level\",\"Class\", \"Server\", \"RealmRank\", \"TotalRealmPo" +
            "ints\", \"TotalSoloKills\", \"TotalDeathBlows\", \"TotalKills\", \"TotalDeaths\", \"IRS\", " +
            "\"RPNextRank\", \"RPLastUpdate\", \"BountyPoints\", \"MasterLevel_Name\"]}")]
        public string DisplayedDatabaseColumnNames {
            get {
                return ((string)(this["DisplayedDatabaseColumnNames"]));
            }
            set {
                this["DisplayedDatabaseColumnNames"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{\"Names\": [\"Name\", \"Realm\",\"Level\", \"Class\", \"Server\", \"Realm\\nRank\", \"Realm\\nPoi" +
            "nts\", \"Solo\\nKills\", \"Death\\nBlows\", \"Kills\", \"Deaths\", \"IRS\", \"RP Next\\nRank\", " +
            "\"RP Last\\nUpdate\", \"Bounty\\nPoints\", \"MasterLevel Path\"]}")]
        public string DisplayedDataGridViewHeaderNames {
            get {
                return ((string)(this["DisplayedDataGridViewHeaderNames"]));
            }
            set {
                this["DisplayedDataGridViewHeaderNames"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%UserProfile%\\Documents\\ChimpToolBackup.json")]
        public string JsonBackupFileFullPath {
            get {
                return ((string)(this["JsonBackupFileFullPath"]));
            }
            set {
                this["JsonBackupFileFullPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Default")]
        public string LastAccount {
            get {
                return ((string)(this["LastAccount"]));
            }
            set {
                this["LastAccount"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int Server {
            get {
                return ((int)(this["Server"]));
            }
            set {
                this["Server"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseAPI {
            get {
                return ((bool)(this["UseAPI"]));
            }
            set {
                this["UseAPI"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseSelenium {
            get {
                return ((bool)(this["UseSelenium"]));
            }
            set {
                this["UseSelenium"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"{""Servers"": {""Server"": [{""Index"": 41, ""IP"": ""107.23.173.143"", ""Name"": ""YWain1"", ""Port"": 10622}, {""Index"": 49, ""IP"": ""107.23.173.143"", ""Name"": ""YWain2"", ""Port"": 10622}, {""Index"": 50, ""IP"": ""107.23.173.143"", ""Name"": ""YWain3"", ""Port"": 10622}, {""Index"": 51, ""IP"": ""107.23.173.143"", ""Name"": ""YWain4"", ""Port"": 10622}, {""Index"": 52, ""IP"": ""107.23.173.143"", ""Name"": ""YWain5"", ""Port"": 10622}, {""Index"": 53, ""IP"": ""107.23.173.143"", ""Name"": ""YWain6"", ""Port"": 10622}, {""Index"": 54, ""IP"": ""107.23.173.143"", ""Name"": ""YWain7"", ""Port"": 10622}, {""Index"": 55, ""IP"": ""107.23.173.143"", ""Name"": ""YWain8"", ""Port"": 10622}, {""Index"": 56, ""IP"": ""107.23.173.143"", ""Name"": ""YWain9"", ""Port"": 10622}, {""Index"": 57, ""IP"": ""107.23.173.143"", ""Name"": ""YWain10"", ""Port"": 10622}]}}")]
        public string ServerList {
            get {
                return ((string)(this["ServerList"]));
            }
            set {
                this["ServerList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"{""Realm"": {""Albion"": ""Armsman,Armswoman,Cabalist,Cleric,Friar,Heretic,Infiltrator,Mauler,Mercenary,Minstrel,Necromancer,Paladin,Reaver,Scout,Sorcerer,Sorceress,Theurgist,Wizard"", ""Hibernia"": ""Animist,Bainshee,Bard,Blademaster,Champion,Druid,Eldritch,Enchanter,Enchantress,Hero,Mauler,Mentalist,Nightshade,Ranger,Valewalker,Vampiir,Warden"", ""Midgard"": ""Berserker,Bonedancer,Healer,Hunter,Huntress,Mauler,Runemaster,Savage,Shadowblade,Shaman,Skald,Spiritmaster,Thane,Valkyrie,Warlock,Warrior""}}")]
        public string RealmClasses {
            get {
                return ((string)(this["RealmClasses"]));
            }
            set {
                this["RealmClasses"] = value;
            }
        }
    }
}
