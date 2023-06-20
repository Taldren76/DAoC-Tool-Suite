using System.Diagnostics;

namespace SQLLibrary
{
    //datetime(Date),Account,WebID,Name,Realm,Class,Server,TotalRealmPoints,TotalSoloKills,TotalDeathBlows,TotalKills,TotalDeaths,Level,Race,Guild_WebID,Alchemy,Armorcraft,Fletching,Siegecraft,Spellcrafting,Tailoring,Weaponcraft
    public class CharacterModel
    { 

        public string? Date { get; set; }

        public string? Account { get; set; } = "Default";

        public string? WebID { get; set; } = "";
        public string? FirstName => Name?.Split(" ")?.FirstOrDefault();

        public string? Name { get; set; } = "";

        public string? Realm { get; set; } = "";

        public string? Class { get; set; } = "";

        public string? Server { get; set; } = "";

        public int? TotalRealmPoints { get; set; } = 0;

        public int? TotalSoloKills { get; set; } = 0;

        public int? TotalDeathBlows { get; set; } = 0;

        public int? TotalKills { get; set; } = 0;

        public int? TotalDeaths { get; set; } = 0;

        public string? Level { get; set; } = "";

        public string? Race { get; set; } = "";
        public int? BountyPoints { get; set; } = 0;

        public string? Guild_WebID { get; set; }

        public int? Alchemy { get; set; } = 0;

        public int? Armorcraft { get; set; } = 0;

        public int? Fletching { get; set; } = 0;

        public int? Siegecraft { get; set; } = 0;

        public int? Spellcrafting { get; set; } = 0;

        public int? Tailoring { get; set; } = 0;

        public int? Weaponcraft { get; set; } = 0;

        //
        // Misc
        //

        public int? MasterLevel_Level { get; set; } = 0;
        public string? MasterLevel_Name { get; set; } = null;

        public int? Albion_SoloKills { get; set; } = 0;
        public int? Albion_DeathBlows { get; set; } = 0;
        public int? Albion_Kills { get; set; } = 0;
        public int? Albion_Deaths { get; set; } = 0;

        public int? Hibernia_SoloKills { get; set; } = 0;
        public int? Hibernia_DeathBlows { get; set; } = 0;
        public int? Hibernia_Kills { get; set; } = 0;
        public int? Hibernia_Deaths { get; set; } = 0;


        public int? Midgard_SoloKills { get; set; } = 0;
        public int? Midgard_DeathBlows { get; set; } = 0;
        public int? Midgard_Kills { get; set; } = 0;
        public int? Midgard_Deaths { get; set; } = 0;


        //
        // Calculated Values
        //
        private int _RealmRank => CalculateRealmRank(TotalRealmPoints ?? 0);
        public string RealmRank => _RealmRank.ToString().Insert(_RealmRank.ToString().Length - 1, "L");
        public int IRS => CalculateIRS(TotalRealmPoints ?? 0, TotalDeaths ?? 0);
        public int RPNextRank => RpForNextRealmRank(TotalRealmPoints ?? 0);
        public int RPLastUpdate { get; set; }
        public DateTime DateTime
        {
            get
            {
                _ = DateTime.TryParse(Date, out DateTime temp);
                return temp;
            }
        }
        public DateOnly DateOnly => DateOnly.FromDateTime(DateTime);

        //
        // Helper Methods
        //

        private static readonly Dictionary<int, double> RealmRanks = new();
        private static int CalculateIRS(int realmPoints, int deaths)
        {
            return Convert.ToInt32(deaths == 0 ? 0.0 : (realmPoints / deaths));
        }
        private static int CalculateRealmRank(int realmPoints)
        {
            Dictionary<int, double> realmRanks = new();
            if (RealmRanks is null)
            {
                return 0;
            }
            else
            {
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
            }
            try
            {
                int realmRank = realmRanks?.Where(x => x.Value <= realmPoints)?.Select(x => x.Key)?.Last() ?? -1;

                //double decimalRealmRank = realmRank / 10.0;

                //return decimalRealmRank;
                return realmRank;
            }
            catch
            {
                return -1;
            }
        }
        private static int RpForNextRealmRank(int realmPoints)
        {
            Dictionary<int, double> realmRanks = new();
            if (RealmRanks is null)
            {
                return 0;
            }
            else
            {
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
            }

            try
            {
                if (realmPoints >= 187917143)
                {
                    return int.MaxValue;
                }

                int currentRank = CalculateRealmRank(realmPoints); //* 10;
                int nextRank = Convert.ToInt32(currentRank) + 1;
                int nextRankRP = Convert.ToInt32(realmRanks[nextRank]);
                int RPNeeded = nextRankRP - realmPoints;
                return RPNeeded;
            }
            catch(Exception ex) 
            {
                Trace.WriteLine(ex.Message);
                Trace.Write(ex.StackTrace);
                return -1;
            }
        }
    }
}
