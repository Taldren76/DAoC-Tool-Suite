using SQLLibrary.Enums;

namespace DAoCToolSuite.CharacterTool.Settings
{
    internal static class SettingsDefault
    {
        public static readonly bool AlwaysOnTop = false;
        public static readonly string DAoCCharacterFileDirectory = "%AppData%\\Electronic Arts\\Dark Age of Camelot\\LotM";
        public static readonly string JsonBackupFileFullPath = "%UserProfile%\\Documents\\CharacterToolBackup.json";
        public static readonly string RealmClasses = "{\"Realm\": {\"Albion\": \"Armsman,Armswoman,Cabalist,Cleric,Friar,Heretic,Infiltrator,Mauler,Mercenary,Minstrel,Necromancer,Paladin,Reaver,Scout,Sorcerer,Sorceress,Theurgist,Wizard\", \"Hibernia\": \"Animist,Bainshee,Bard,Blademaster,Champion,Druid,Eldritch,Enchanter,Enchantress,Hero,Mauler,Mentalist,Nightshade,Ranger,Valewalker,Vampiir,Warden\", \"Midgard\": \"Berserker,Bonedancer,Healer,Hunter,Mauler,Runemaster,Savage,Shadowblade,Shaman,Skald,Spiritmaster,Thane,Valkyrie,Warlock,Warrior\"}}";
        public static readonly string Servers = "{\"Servers\": {\"Server\": [{\"Index\": 41, \"Name\": \"YWain1\"}, {\"Index\": 49, \"Name\": \"YWain2\"}, {\"Index\": 50, \"Name\": \"YWain3\"}, {\"Index\": 51, \"Name\": \"YWain4\"}, {\"Index\": 52, \"Name\": \"YWain5\"}, {\"Index\": 53, \"Name\": \"YWain6\"}, {\"Index\": 54, \"Name\": \"YWain7\"}, {\"Index\": 55, \"Name\": \"YWain8\"}, {\"Index\": 56, \"Name\": \"YWain9\"}, {\"Index\": 57, \"Name\": \"YWain10\"}]}}";
        public static readonly ServerCluster Server = ServerCluster.Ywain;
        public static readonly ColumnNames DisplayedDatabaseColumnNames = new() { Names = new() { "DateOnly", "FirstName", "Realm", "Class", "Description" } };
        public static readonly HeaderNames DisplayedDataGridViewHeaderNames = new() { Names = new() { "Date", "Name", "Realm", "Class", "Description" } };
    }
}
