﻿using SQLLibrary.Enums;

namespace DAoCToolSuite.CharacterTool.Settings
{
    internal static class SettingsDefault
    {
        public static readonly bool AlwaysOnTop = false;
        public static readonly string DAoCCharacterFileDirectory = "%AppData%\\Electronic Arts\\Dark Age of Camelot\\LotM";
        public static readonly string JsonBackupFileFullPath = "%UserProfile%\\Documents\\CharacterToolBackup.json";
        public static readonly ServerCluster Server = ServerCluster.Ywain;
        public static readonly ColumnNames DisplayedDatabaseColumnNames = new() { Names = new() { "Name", "Realm", "Class", "Server", "RealmRank", "TotalRealmPoints", "TotalSoloKills", "TotalDeathBlows", "TotalKills", "TotalDeaths", "IRS", "RPNextRank", "RPLastUpdate", "BountyPoints" } };
        public static readonly HeaderNames DisplayedDataGridViewHeaderNames = new() { Names = new() { "Name", "Realm", "Class", "Server", "Realm\nRank", "Realm\nPoints", "Solo\nKills", "Death\nBlows", "Kills", "Deaths", "IRS", "RP Next\nRank", "RP Last\nUpdate", "BountyPoints" } };
    }
}
