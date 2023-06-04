using DAoCToolSuite.ChimpTool.Logging;
using Newtonsoft.Json;

namespace DAoCToolSuite.ChimpTool.Json
{
    public class ChimpJson
    {
        private static readonly Logger Logger = new();

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

        private static Dictionary<int, double> RealmRanks { get; set; } = new Dictionary<int, double>();

        public ChimpJson(bool StubObject)
        {
            Dictionary<int, double> realmRanks = new();
            Realms = new();
            Races = new();
            Classes = new();
            Servers = new();
        }

        public ChimpJson()
        {
            #region RealmRanks
            Dictionary<int, double> realmRanks = new();
            for (int rr = 0; rr < 100; rr++)
            {
                realmRanks.Add(rr + 11, ((50 * Math.Pow(rr, 3)) + (75 * Math.Pow(rr, 2)) + (25 * rr)) / 6);
            }
            realmRanks.Add(111, 9111713);
            realmRanks.Add(112, 10114001);
            realmRanks.Add(113, 11226541);
            realmRanks.Add(114, 12461460);
            realmRanks.Add(115, 13832221);
            realmRanks.Add(116, 15353765);
            realmRanks.Add(117, 17042680);
            realmRanks.Add(118, 18917374);
            realmRanks.Add(119, 20998286);
            realmRanks.Add(120, 23308097);
            realmRanks.Add(121, 25871988);
            realmRanks.Add(122, 28717906);
            realmRanks.Add(123, 31876876);
            realmRanks.Add(124, 35383333);
            realmRanks.Add(125, 39275499);
            realmRanks.Add(126, 43595804);
            realmRanks.Add(127, 48391343);
            realmRanks.Add(128, 53714390);
            realmRanks.Add(129, 59622973);
            realmRanks.Add(130, 66181501);
            realmRanks.Add(131, 73461466);
            realmRanks.Add(132, 81542227);
            realmRanks.Add(133, 90511872);
            realmRanks.Add(134, 100468178);
            realmRanks.Add(135, 111519678);
            realmRanks.Add(136, 123786843);
            realmRanks.Add(137, 137403395);
            realmRanks.Add(138, 152517769);
            realmRanks.Add(139, 169294723);
            realmRanks.Add(140, 187917143);
            RealmRanks = realmRanks;
            #endregion

            Realms = new List<string>()
            {
                "Albion", "Hibernia", "Midgard"
            };

            Races = new List<string>()
            {
                "Briton","Saracen","Avalonian","Highlander","Inconnu","Half Ogre","Korazh",
                "Celt", "Lurikeen","Firbolg","Elf","Sylvan", "Shar", "Graoch",
                "Kobold", "Dwarf", "Norseman", "Troll", "Valkyn", "Frostalf", "Deifrang"
            };

            Classes = new List<string>()
            {
                "Animist","Bainshee","Bard","Blademaster","Champion","Druid","Eldritch","Enchanter","Enchantress","Hero","Mauler","Mentalist","Nightshade","Ranger","Valewalker","Vampiir","Warden",
                "Armsman","Armswoman","Cabalist","Cleric","Friar","Heretic","Infiltrator","Mercenary","Minstrel","Necromancer","Paladin","Reaver","Scout","Sorcerer", "Sorceress", "Theurgist","Wizard",
                "Berserker","Bonedancer","Healer","Hunter","Runemaster","Savage","Shadowblade","Shaman","Skald","Spiritmaster","Thane","Valkyrie","Warlock","Warrior"
            };



            Servers = new List<string>()
            {
                "Ywain1","Ywain2","Ywain3","Ywain4","Ywain5","Ywain6","Ywain7","Ywain8","Ywain9","Ywain10",
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
                Logger.Debug($"Invalid WebID: {WebID}");
                result &= false;
            }
            if (string.IsNullOrEmpty(Name))
            {
                Logger.Debug($"Invalid Name: {Name}");
                result &= false;
            }
            if (string.IsNullOrEmpty(Realm) || !Realms.Contains(Realm))
            {
                Logger.Debug($"Invalid Realm: {Realm}");
                result &= false;
            }
            if (string.IsNullOrEmpty(Race) || !Races.Contains(Race))
            {
                Logger.Debug($"Invalid Race: {Race}");
                result &= false;
            }
            if (string.IsNullOrEmpty(Class) || !Classes.Contains(Class))
            {
                Logger.Debug($"Invalid Class: {Class}");
                result &= false;
            }
            if (string.IsNullOrEmpty(Server) || !Servers.Contains(Server))
            {
                Logger.Debug($"Invalid Server: {Server}");
                result &= false;
            }
            return result;
        }
    }
}
