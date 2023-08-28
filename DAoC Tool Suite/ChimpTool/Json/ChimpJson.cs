using Logger;
using Newtonsoft.Json;

namespace DAoCToolSuite.ChimpTool.Json
{
    public class ChimpJson
    {
        private static LogManager Logger => LogManager.Instance;

        #region JsonProperties
        [JsonProperty]
        public string? WebID { get; set; }
        [JsonProperty]
        public string? Name { get; set; }
        [JsonProperty]
        public string? Realm { get; set; }
        [JsonProperty]
        public string? Class { get; set; }
        [JsonProperty]
        public string? Server { get; set; }
        [JsonProperty]
        public string? TotalRealmPoints { get; set; }
        [JsonProperty]
        public string? TotalSoloKills { get; set; }
        [JsonProperty]
        public string? TotalDeathBlows { get; set; }
        [JsonProperty]
        public string? TotalKills { get; set; }
        [JsonProperty]
        public string? TotalDeaths { get; set; }
        [JsonProperty]
        public string? Level { get; set; }
        [JsonProperty]
        public string? Race { get; set; }
        [JsonProperty]
        public string? BountyPoints { get; set; }
        [JsonProperty]
        public string? Guild_WebID { get; set; }
        [JsonProperty]
        public string? Alchemy { get; set; }
        [JsonProperty]
        public string? Armorcraft { get; set; }
        [JsonProperty]
        public string? Fletching { get; set; }
        [JsonProperty]
        public string? Siegecraft { get; set; }
        [JsonProperty]
        public string? Spellcrafting { get; set; }
        [JsonProperty]
        public string? Tailoring { get; set; }
        [JsonProperty]
        public string? Weaponcraft { get; set; }
        [JsonProperty]
        public string? GuildName { get; set; }
        [JsonProperty]
        public string? RealmRank { get; set; }
        [JsonProperty]
        public string? IRS { get; set; }
        public string? RPNextRank { get; set; }
        public string? RPLastUpdate { get; set; }

        public string? MasterLevel_Level { get; set; }
        public string? MasterLevel_Name { get; set; }

        public string? Albion_SoloKills { get; set; }
        public string? Albion_DeathBlows { get; set; }
        public string? Albion_Kills { get; set; }
        public string? Albion_Deaths { get; set; }

        public string? Hibernia_SoloKills { get; set; }
        public string? Hibernia_DeathBlows { get; set; }
        public string? Hibernia_Kills { get; set; }
        public string? Hibernia_Deaths { get; set; }


        public string? Midgard_SoloKills { get; set; }
        public string? Midgard_DeathBlows { get; set; }
        public string? Midgard_Kills { get; set; }
        public string? Midgard_Deaths { get; set; }
        #endregion

        private List<string> Realms { get; set; } = new List<string>();
        private List<string> Races { get; set; } = new List<string>();
        private List<string> Classes { get; set; } = new List<string>();
        private List<string> Servers { get; set; } = new List<string>();

        public ChimpJson()
        {
            SetRealms();
            SetRaces();
            SetClasses();
            SetServers();
        }

        private void SetRealms()
        {
            Realms = new List<string>()
            {
                "Albion", "Hibernia", "Midgard"
            };
        }

        private void SetRaces()
        {
            Races = new List<string>()
            {
                "Briton","Saracen","Avalonian","Highlander","Inconnu","Half Ogre","Korazh",
                "Celt", "Lurikeen","Firbolg","Elf","Sylvan", "Shar", "Graoch",
                "Kobold", "Dwarf", "Norseman", "Troll", "Valkyn", "Frostalf", "Deifrang"
            };
        }

        private void SetClasses()
        {
            //TODO: Make this load the copy from settings.
            Classes = new List<string>()
            {
                "Animist","Bainshee","Bard","Blademaster","Champion","Druid","Eldritch","Enchanter","Enchantress","Hero","Heroine","Mauler","Mentalist","Nightshade","Ranger","Valewalker","Vampiir","Warden",
                "Armsman","Armswoman","Cabalist","Cleric","Friar","Heretic","Infiltrator","Mercenary","Minstrel","Necromancer","Paladin","Reaver","Scout","Sorcerer", "Sorceress", "Theurgist","Wizard",
                "Berserker","Bonedancer","Healer","Hunter","Huntress","Runemaster","Savage","Shadowblade","Shaman","Skald","Spiritmaster","Thane","Valkyrie","Warlock","Warrior"
            };
        }

        private void SetServers()
        {
            Servers = new List<string>()
            {
                "Ywain1","Ywain2","Ywain3","Ywain4","Ywain5","Ywain6","Ywain7","Ywain8","Ywain9","Ywain10",
                "YWain1","YWain2","YWain3","YWain4","YWain5","YWain6","YWain7","YWain8","YWain9","YWain10",
                "Pendragon",
                "Gaheris", "Tintagel"
            };
        }

        public override string ToString()
        {
            string toWrite = $"Id:{WebID ?? "null"},Name:{Name ?? "null"},Race:{Race ?? "null"},Class:{Class ?? "null"},RealmRank:{RealmRank ?? "null"},SoloKills:{TotalSoloKills ?? "null"},RealmPoints:{TotalRealmPoints ?? "null"},PvPKills:{TotalKills ?? "null"},PvPDeaths:{TotalDeaths ?? "null"},DeathBlows:{TotalDeathBlows ?? "null"}";
            return toWrite;
        }

        public bool IsValid()
        {
            bool result = true;
            if (string.IsNullOrEmpty(WebID))
            {
                Logger.Debug($"Invalid WebID.");
                result &= false;
            }
            if (string.IsNullOrEmpty(Name))
            {
                Logger.Debug($"Invalid Name.");
                result &= false;
            }
            if (string.IsNullOrEmpty(Realm) || !Realms.Contains(Realm))
            {
                Logger.Debug($"Invalid Realm: {Realm ?? "null"} WebID: {WebID ?? "null"} Name: {Name ?? "null"}");
                result &= false;
            }
            if (string.IsNullOrEmpty(Race) || !Races.Contains(Race))
            {
                Logger.Debug($"Invalid Race: {Race ?? "null"} WebID: {WebID ?? "null"} Name: {Name ?? "null"}");
                result &= false;
            }
            if (string.IsNullOrEmpty(Class) || !Classes.Contains(Class))
            {
                Logger.Debug($"Invalid Class: {Class ?? "null"} WebID: {WebID ?? "null"} Name: {Name ?? "null"}");
                result &= false;
            }
            if (string.IsNullOrEmpty(Server) || !Servers.Contains(Server))
            {
                Logger.Debug($"Invalid Server: {Server ?? "null"} WebID: {WebID ?? "null"} Name: {Name ?? "null"}");
                result &= false;
            }
            return result;
        }
    }
}
