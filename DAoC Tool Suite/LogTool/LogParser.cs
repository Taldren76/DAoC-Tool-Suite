using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Shapes;
using Logger;
using Windows.Gaming.Preview.GamesEnumeration;

namespace DAoCToolSuite.LogTool
{
    public class LogParser
    {
        #region Raw Data
        //Damage
        private int DamageDone = 0;
        private int CritDamageDone = 0;

        //Healing
        private int HealingDone = 0; // Healing done by spells cast by the player on others.
        private int CriticalHealingDone = 0;
        private int AbilitySelfHealingDone = 0;
        private int SelfHealingDone = 0; // Healing done by spells cast by the player on self. 
        private int CriticalSelfHealingDone = 0;
        private int HealingTaken = 0; // Total healing you have recieved.

        //Your Pets
        private int PetHealingDone = 0;
        private int PetDamageDone = 0;
        private int PetHealingYou = 0;
        private int HealingDoneByYouToYourPet = 0;
        private int CriticalHealingDoneByYouToYourPet = 0;

        //Absorbs
        private int DamageTakenAbsorbed = 0;
        private int DamageDoneAbsorbed = 0;

        //Non-Player
        private int NonPlayerDamageTaken = 0;
        private int NonPlayerDamageDone = 0;
        private int NonPlayerCriticalDamageDone = 0;
        private int NonPlayerHealingDone = 0;
        private int NonPlayerCriticalHealingDone = 0;
       
        //Misc
        private int Hit = 0;
        
        private int CritHit = 0;
        
        private int PetHit = 0;
        
        private int CritPetHit = 0;
        
        private int HealHit = 0;         // Number of individual Heals Done. AE counts as multiple Hits.
                                         // This counts both HealingDone and HealSelf.
        
        private int CritHealHit = 0;     // Number of individual Crit Heals Done. AE counts as multiple Hits.
                                         // This counts both CritHealingDone and CritHealingSelf.    

        #endregion



        #region Heal Stats
        public int TotalHealingDone => HealingDone + CriticalHealingDone + SelfHealingDone + CriticalSelfHealingDone + AbilitySelfHealingDone + PetHealingDone + PetHealingYou + HealingDoneByYouToYourPet + CriticalHealingDoneByYouToYourPet + NonPlayerHealingDone;
        public int TotalHealingToSelf => SelfHealingDone + CriticalSelfHealingDone + AbilitySelfHealingDone + PetHealingYou;
        public int HealSelfRatio => Convert.ToInt32(((double)TotalHealingToSelf / (double)(TotalHealingDone==0 ? 1 : TotalHealingDone))*100);
        public int TotalHealingRecieved => HealingTaken + TotalHealingToSelf + PetHealingYou;
        public int AverageHealingDone => Convert.ToInt32((double)(HealingDone + SelfHealingDone + NonPlayerHealingDone + HealingDoneByYouToYourPet) / (double)(HealHit == 0 ? 1 : HealHit));
        public int AverageCritHealingDone => Convert.ToInt32((double)(CriticalHealingDone+ CriticalSelfHealingDone+ NonPlayerCriticalHealingDone + CriticalHealingDoneByYouToYourPet) / (double)(CritHealHit == 0 ? 1 : CritHealHit));
        private int TotalHeals => CritHealHit + HealHit;
        public int HealCritRate => Convert.ToInt32(((double)CritHealHit / (double)(TotalHeals == 0 ? 1 : TotalHeals)) * 100);
        public int CritHealRatio => Convert.ToInt32(((double)CriticalHealingDone / (double)(TotalHealingDone == 0 ? 1 : TotalHealingDone)) * 100);
        
        #endregion

        #region Damage Stats
        public int TotalDamageDone => DamageDone + CritDamageDone + PetDamageDone + NonPlayerDamageDone + NonPlayerCriticalDamageDone;
        public int AverageDamageDone => Convert.ToInt32((double)(DamageDone+ NonPlayerDamageDone) / (double)(Hit == 0 ? 1 : Hit));
        public int AverageCritDamageDone => Convert.ToInt32((double)(CritDamageDone+ NonPlayerCriticalDamageDone) / (double)(CritHit == 0 ? 1 : CritHit));
        public int TotalDamageHits => CritHit + Hit;
        public int DamageCritRate => Convert.ToInt32(((double)CritHit / (double)(TotalDamageHits == 0 ? 1 : TotalDamageHits)) * 100);
        public int CritDamageRatio => Convert.ToInt32(((double)CritDamageDone / (double)(TotalDamageDone == 0 ? 1 : TotalDamageDone)) * 100);
        public int TotalDamageTaken => NonPlayerDamageTaken; //TODO:Add PLayer/Pet Damage!
        #endregion

        #region Standard Stats
        public int RealmPointsEarned = 0;

        public int DeathBlows = 0;

        public int Deaths = 0;
        public int IRS => Convert.ToInt32((double)RealmPointsEarned / (double)(Deaths == 0 ? 1 : Deaths));
        public int KDR => Convert.ToInt32((double)DeathBlows / (double)(Deaths == 0 ? 1 : Deaths));
        #endregion

        private static readonly object ThisLock = new object();
        private static LogManager Logger => LogManager.Instance;
        public bool PlayersOnlyFilter { get; set; } = false;
        private int LogFileReadIndex { get; set; } = -1;
        private string LogPath { get; set; } = "chat.log";
        private static List<Regex> RejectLineContent { get; set; } = new();
        private DateTime LastParse { get; set; } = DateTime.MinValue;
        private List<string> FilteredLog { get; set; } = new();

        public Dictionary<DateTime, int> LogOpenEntries = new();
        public Dictionary<DateTime, int> LogCloseEntries = new();

        /// <summary>
        /// Determines if the LogFile contains information that has not been parsed.
        /// </summary>
        /// <returns>Boolean</returns>
        public bool HasUnparsedData()
        {
            var lastModified = GetLogLastModified();
            if (LastParse < lastModified)
            {
                return true;
            }
            return false;
        }

        public LogParser()
        {

        }

        public LogParser(string path)
        {
            LogPath = path;           
            PopulateRejectLineContentList();
            GetFilteredFileContent();
            ParseLogOpen();
            ParseLogClosed();
        }

        public LogParser(string path, int startIndex)
        {
            LogPath = path;
            LogFileReadIndex = startIndex;
            PopulateRejectLineContentList();
            GetFilteredFileContent();
            ParseLogOpen();
            ParseLogClosed();
        }

        public void SetFileIndex(int index)
        {
            LogFileReadIndex = index;
        }

        private bool IsFiltered { get; set; } = false;

        /// <summary>
        /// Polulates a list with substrings of log lines to be rejected from the filtered log.
        /// </summary>
        private void PopulateRejectLineContentList()
        {
            List<Regex> rejectLineContent = new()
            {
                new Regex(@"@@.*"),
                new Regex(@"##.*"),
                new Regex(@"You (?!hit|critical|critically|begin|drain|earn|fail|attack|perform|prepare|steal|use|just|heal).*"),
                new Regex(@"You prepare to sprint!"),
                new Regex(@"Your share.*"),
                new Regex(@"You've (?!been awarded).*"),
                new Regex(@"Your target.*"),
                new Regex(@"That target.*"),
                new Regex(@".*casts a spell!"),
                new Regex(@".*is dead!"),
                new Regex(@".*as a follow up!"),
                new Regex(@"(.*) uses [the ]*(.*)\."),
                new Regex(@"(.*) doesn't currently.*")
            };
            RejectLineContent = rejectLineContent;
        }

        /// <summary>
        /// Determines if a string contains a substring from the RejectLineContent list.
        /// </summary>
        /// <param name="line">Line from the log file</param>
        /// <returns>Boolean</returns>
        private bool IsRejectContent(string line)
        {
            if (string.IsNullOrEmpty(line.Trim()))
                return true;

            foreach (Regex reject in RejectLineContent)
            {
                if (line[0].Equals('*'))
                    return false;
                else if (!line![0].Equals('['))
                    return true;
                try
                {
                    string? delimitedLine = line.Remove(0, 10).Trim();
                    if (string.IsNullOrEmpty(delimitedLine) || reject.IsMatch(delimitedLine))
                    {
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    Logger.Error(ex);
                }
            }
            return false;
        }

        /// <summary>
        /// Reads the Log File and populates the FilteredLog list.
        /// </summary>
        private void GetFilteredFileContent()
        {
            lock (ThisLock)
            {
                if (!File.Exists(LogPath))
                {
                    FilteredLog = new();
                    return;
                }
                List<string> filteredList = new();
                FileAttributes attributes = File.GetAttributes(LogPath);
                using (FileStream fs = new(LogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using StreamReader sr = new(fs);
                    while (!sr.EndOfStream)
                    {
                        var lineRead = sr.ReadLine();
                        if (lineRead is null)
                            continue;
                        if (!IsRejectContent(lineRead))
                            filteredList.Add(lineRead);
                    }
                }
                File.SetAttributes(LogPath, attributes);
                FilteredLog = filteredList;
            }
        }

        /// <summary>
        /// Returns the DateTime stamp that represents the last time the LogFile was modified.
        /// </summary>
        /// <returns>DateTime</returns>
        private DateTime GetLogLastModified()
        {
            lock (ThisLock)
            {
                var lastModified = File.GetLastWriteTime(LogPath);
                return lastModified;
            }
        }

        #region RegEx
        //Earnings
        private static readonly Regex RealmPointsEarnedRegEx = new Regex(@"You earn (\d+).* realm points");

        //Damage
        private static readonly Regex DamageDoneRegEx = new Regex(@"You (attack|hit) ((?!the).+) for (\d+)");
        private static readonly Regex CritDamageDoneRegEx = new Regex(@"You (attack|hit) ((?!the)\w+).* for (\d+).*\n.*You critical.* (\d+)");

        //Healing
        private static readonly Regex HealingDoneRegEx = new Regex(@"You heal ((?!your|.*'s).+) for (\d+)");
        private static readonly Regex CriticalHealingDoneRegEx = new Regex(@"You critical heal.* (\d+).*\n.*You heal ((?!your|.*'s).+) for (\d+)");
        private static readonly Regex AbilitySelfHealingRegEx = new Regex(@"Your (.*) ability heals you .* (\d+)");
        private static readonly Regex HealingBatteryHealRegEx = new Regex(@"(.*) healing battery.* heals (.*) (for |\()(\d+)");
        private static readonly Regex SelfHealingRegEx = new Regex(@"You heal yourself.*(\d+)");
        private static readonly Regex CriticalSelfHealingRegEx = new Regex(@"You critical heal.* (\d+).*\n.*You heal yourself.*(\d+)");
        private static readonly Regex HealingTakenRegEx = new Regex(@"(.*) healed you.*(\d+)");

        //Your Pets
        private static readonly Regex PetHealingRegEx = new Regex(@"Your (.*) healed (.*) for (\d+)");
        private static readonly Regex PetDamageDoneRegEx = new Regex(@"Your (.*) (hits|attacks) (.*) for (\d+)");
        //TODO: Populate PetCriticalDamageDoneRegEx once I know what a chat log line looks like for that.
        private static readonly Regex HealingDoneByYouToYourPetRegEx = new Regex(@"You heal your (.+) for (\d+)");
        private static readonly Regex CriticalHealingDoneByYouToYourPetRegEx = new Regex(@"You heal your (.+) for (\d+) .*\n.* You critical .* (\d+)");

        //Absorbs
        private static readonly Regex DamageTakenAbsorbedRegEx = new Regex(@"Your .* absorb.* (\d+)");
        private static readonly Regex DamageDoneAbsorbedRegEx = new Regex(@"absorb.* (\d+).* of your");

        //Non-Player
        private static readonly Regex NonPlayerDamageTakenRegEx = new Regex(@"The (.*) hits you for (\d+)");
        private static readonly Regex NonPlayerDamageDoneRegEx = new Regex(@"You hit the (.*) for (\d+)");
        private static readonly Regex NonPlayerCriticalDamageDoneRegEx = new Regex(@"You hit the (.+) for (\d+) .*\n.* You critical .* (\d+)");
        private static readonly Regex NonPlayerHealingDoneRegEx = new Regex(@"You heal (.*)'s (.*) for (\d+)");
        private static readonly Regex NonPlayerCriticalHealingDoneRegEx = new Regex(@"You critical heal.* (\d+).*\n.*You heal (.*)'s (.*) for (\d+)");

        //Misc
        private static readonly Regex ChatLogOpenedRegEx = new Regex(@"Chat\sLog\sOpened:\s(\w+\s(\w+)\s+(\d+)\s(\d+:\d+:\d+)\s(\d+))");
        #endregion

        /// <summary>
        /// Parses the FilteredLog list and populates statistics.
        /// </summary>
        public void Parse()
        {
            if (!HasUnparsedData() || FilteredLog.Count == 0)
            {
                return;
            }

            LastParse = DateTime.Now;

            GetFilteredFileContent();

            //The last line of a file is often only partially written. 
            int startIndex = LogFileReadIndex - 1;
            if (startIndex < 0)
                startIndex = 0;

            int endIndex = FilteredLog.Count - 1;
            if (endIndex < 0)
                endIndex = 1;

            for (int index = startIndex; index < endIndex; index++)
            {
                string line = FilteredLog[index];
                string line2 = (index + 1 < FilteredLog.Count) ? FilteredLog[(index + 1)] : "";
                string twoLines = $"{line}\n{line2}";

                #region Earnings
                var realmPointsMatch = RealmPointsEarnedRegEx.Match(line);
                if (realmPointsMatch.Success)
                {
                    int rpsEarned = Int32.TryParse(realmPointsMatch.Groups[1].Value, out rpsEarned) ? rpsEarned : 0;
                    RealmPointsEarned += rpsEarned;
                    continue;
                }
                #endregion

                #region Damage
                // new Regex(@"You (attack|hit) ((?!the)\w+).* for (\d+).*\n.*You critical.* (\d+)");
                var critDamageMatch = CritDamageDoneRegEx.Match(twoLines);
                if (critDamageMatch.Success)
                {
                    int normalDamageDone = Int32.TryParse(critDamageMatch.Groups[3].Value, out normalDamageDone) ? normalDamageDone : 0;
                    int critDamageDone = Int32.TryParse(critDamageMatch.Groups[4].Value, out critDamageDone) ? critDamageDone : 0;
                    CritDamageDone += normalDamageDone + critDamageDone;
                    CritHit += 1;
                    index += 1;
                    continue;
                }

                // new Regex(@"You (attack|hit) ((?!the).+) for (\d+)")
                var damageDoneMatch = DamageDoneRegEx.Match(line);
                if (damageDoneMatch.Success)
                {
                    int damageDone = Int32.TryParse(damageDoneMatch.Groups[3].Value, out damageDone) ? damageDone : 0;
                    DamageDone += damageDone;
                    Hit += 1;
                    continue;
                }
                #endregion

                #region Healing
                //new Regex(@"(.*) healing battery.* heals (.*) (for |\()(\d+)");
                var healingBatteryHealRegEx = HealingBatteryHealRegEx.Match(line);
                if (healingBatteryHealRegEx.Success)
                {
                    string whosBattery = healingBatteryHealRegEx.Groups[1].Value.ToString();
                    string whosHealed = healingBatteryHealRegEx.Groups[2].Value.ToString();
                    int healBattery = Int32.TryParse(healingBatteryHealRegEx.Groups[4].Value, out healBattery) ? healBattery : 0;
                    if (whosBattery.Equals("Your") && whosHealed.Equals("you"))
                    {
                        HealHit += 1;
                        SelfHealingDone += healBattery;
                    }
                    else if (whosBattery.Equals("Your") && !whosHealed.Equals("you"))
                    {
                        HealHit += 1;
                        HealingDone += healBattery;
                    }
                    else if (!whosBattery.Equals("Your") && whosHealed.Equals("you"))
                        HealingTaken += healBattery;
                    continue;
                }

                //new Regex(@"You critical heal.* (\d+).*\n.*You heal yourself.*(\d+)")
                var criticalSelfHealingRegEx = CriticalSelfHealingRegEx.Match(twoLines);
                if (criticalSelfHealingRegEx.Success)
                {
                    int selfCritHeal = Int32.TryParse(criticalSelfHealingRegEx.Groups[2].Value, out selfCritHeal) ? selfCritHeal : 0;
                    CriticalSelfHealingDone += selfCritHeal;
                    CritHealHit += 1;
                    index += 1;
                    continue;
                }

                //new Regex(@"You critical heal.* (\d+).*\n.*You heal ((?!your|.*'s).+) for (\d+)");
                var criticalHealingDoneRegEx = CriticalHealingDoneRegEx.Match(twoLines);
                if (criticalHealingDoneRegEx.Success)
                {
                    int healCritDone = Int32.TryParse(criticalHealingDoneRegEx.Groups[3].Value, out healCritDone) ? healCritDone : 0;
                    CriticalHealingDone += healCritDone;
                    CritHealHit += 1;
                    index += 1;
                    continue;
                }

                //new Regex(@"You heal yourself.*(\d+)");
                var selfHealingRegEx = SelfHealingRegEx.Match(line);
                if (selfHealingRegEx.Success)
                {
                    int healSelf = Int32.TryParse(selfHealingRegEx.Groups[1].Value, out healSelf) ? healSelf : 0;
                    SelfHealingDone += healSelf;
                    HealHit += 1;
                    continue;
                }

                //new Regex(@"Your (.*) ability heals you .* (\d+)");
                var abilitySelfHealingRegEx = AbilitySelfHealingRegEx.Match(line);
                if (abilitySelfHealingRegEx.Success)
                {
                    int abilitySelfHealing = Int32.TryParse(abilitySelfHealingRegEx.Groups[2].Value, out abilitySelfHealing) ? abilitySelfHealing : 0;
                    AbilitySelfHealingDone += abilitySelfHealing;
                    continue;
                }

                //new Regex(@"You heal ((?!your|.*'s).+) for (\d+)");
                var healingDoneMatch = HealingDoneRegEx.Match(line);
                if (healingDoneMatch.Success)
                {
                    int healDone = Int32.TryParse(healingDoneMatch.Groups[2].Value, out healDone) ? healDone : 0;
                    HealingDone += healDone;
                    HealHit += 1;
                    continue;
                }

                //new Regex(@"(.*) healed you.*(\d+)");
                var healingTakenRegEx = HealingTakenRegEx.Match(line);
                if (healingTakenRegEx.Success)
                {
                    int healingTaken = Int32.TryParse(healingTakenRegEx.Groups[2].Value, out healingTaken) ? healingTaken : 0;
                    HealingTaken += healingTaken;
                    continue;
                }
                #endregion

                #region Pets
                //new Regex(@"You heal your (.+) for (\d+) .*\n.* You critical .* (\d+)");
                var criticalHealingDoneByYouToYourPetRegEx = CriticalHealingDoneByYouToYourPetRegEx.Match(twoLines);
                if (criticalHealingDoneByYouToYourPetRegEx.Success)
                {
                    int healDone = Int32.TryParse(criticalHealingDoneByYouToYourPetRegEx.Groups[2].Value, out healDone) ? healDone : 0;
                    int healCritDone = Int32.TryParse(criticalHealingDoneByYouToYourPetRegEx.Groups[3].Value, out healCritDone) ? healCritDone : 0;
                    CriticalHealingDoneByYouToYourPet += healDone + healCritDone;
                    CritHealHit += 1;
                    index += 1;
                    continue;
                }

                //new Regex(@"Your (.*) healed (.*) for (\d+)");
                var petHealingRegEx = PetHealingRegEx.Match(line);
                if (petHealingRegEx.Success)
                {
                    string petHealTarget = petHealingRegEx.Groups[2].Value.ToString();
                    int petHealing = Int32.TryParse(petHealingRegEx.Groups[3].Value, out petHealing) ? petHealing : 0;
                    if (petHealTarget.Equals("you"))
                        PetHealingYou += petHealing;
                    else
                        PetHealingDone += petHealing;
                    continue;
                }

                //new Regex(@"You heal your (.+) for (\d+)");
                var healingDoneByYouToYourPetRegEx = HealingDoneByYouToYourPetRegEx.Match(line);
                if (healingDoneByYouToYourPetRegEx.Success)
                {
                    int healPet = Int32.TryParse(healingDoneByYouToYourPetRegEx.Groups[3].Value, out healPet) ? healPet : 0;
                    HealingDoneByYouToYourPet += healPet;
                    HealHit += 1;
                    continue;
                }

                //new Regex(@"Your (.*) (hits|attacks) (.*) for (\d+)")
                var petDamageDoneRegEx = PetDamageDoneRegEx.Match(line);
                if (petDamageDoneRegEx.Success)
                {
                    int petDamage = Int32.TryParse(petDamageDoneRegEx.Groups[3].Value, out petDamage) ? petDamage : 0;
                    PetDamageDone += petDamage;
                    HealHit += 1;
                    continue;
                }
                #endregion

                #region Absorbs
                // new Regex(@"Your .* absorb.* (\d+)");
                var damageTakenAbsorbedRegEx = DamageTakenAbsorbedRegEx.Match(line);
                if (damageTakenAbsorbedRegEx.Success)
                {
                    int damageTakenAbsorbed = Int32.TryParse(damageTakenAbsorbedRegEx.Groups[1].Value, out damageTakenAbsorbed) ? damageTakenAbsorbed : 0;
                    DamageTakenAbsorbed += damageTakenAbsorbed;
                    continue;
                }

                // new Regex(@"absorb.* (\d+).* of your");
                var damageDoneAbsorbedRegEx = DamageDoneAbsorbedRegEx.Match(line);
                if (damageDoneAbsorbedRegEx.Success)
                {
                    int damageDoneAbsorbed = Int32.TryParse(damageDoneAbsorbedRegEx.Groups[1].Value, out damageDoneAbsorbed) ? damageDoneAbsorbed : 0;
                    DamageDoneAbsorbed += damageDoneAbsorbed;
                    continue;
                }
                #endregion

                if (PlayersOnlyFilter)
                    continue;

                #region Non-Player
                //private static readonly Regex NonPlayerDamageTakenRegEx = new Regex(@"The (.*) hits you for (\d+)");
                //private static readonly Regex NonPlayerDamageDoneRegEx = new Regex(@"You hit the (.*) for (\d+)");
                //private static readonly Regex NonPlayerCriticalDamageDoneRegEx = new Regex(@"You hit the (.+) for (\d+) .*\n.* You critical .* (\d+)");
                //private static readonly Regex NonPlayerHealingDoneRegEx = new Regex(@"You heal (.*)'s (.*) for (\d+)");
                //private static readonly Regex NonPlayerCriticalHealingDoneRegEx = new Regex(@"You heal (.*)'s (.*) for (\d+) .*\n.* You critical heal .* (\d+)");

                var nonPlayerCriticalDamageDoneRegEx = NonPlayerCriticalDamageDoneRegEx.Match(twoLines);
                if (nonPlayerCriticalDamageDoneRegEx.Success)
                {
                    //new Regex(@"You hit the (.+) for (\d+) .*\n.* You critical .* (\d+)");
                    int nonPlayerDamageDone = Int32.TryParse(nonPlayerCriticalDamageDoneRegEx.Groups[2].Value, out nonPlayerDamageDone) ? nonPlayerDamageDone : 0;
                    int nonPlayerCriticalDamageDone = Int32.TryParse(nonPlayerCriticalDamageDoneRegEx.Groups[3].Value, out nonPlayerCriticalDamageDone) ? nonPlayerCriticalDamageDone : 0;
                    NonPlayerCriticalDamageDone += nonPlayerCriticalDamageDone + nonPlayerDamageDone;
                    CritHit += 1;
                    index += 1;
                    continue;
                }

                var nonPlayerCriticalHealingDoneRegEx = NonPlayerCriticalHealingDoneRegEx.Match(twoLines);
                if (nonPlayerCriticalHealingDoneRegEx.Success)
                {
                    //new Regex(@"You critical heal.* (\d+).*\n.*You heal (.*)'s (.*) for (\d+)");
                    int nonPlayerCriticalHealingDone = Int32.TryParse(nonPlayerCriticalHealingDoneRegEx.Groups[4].Value, out nonPlayerCriticalHealingDone) ? nonPlayerCriticalHealingDone : 0;
                    NonPlayerCriticalHealingDone += nonPlayerCriticalHealingDone;
                    CritHealHit += 1;
                    index += 1;
                    continue;
                }

                var nonPlayerDamageTakenRegEx = NonPlayerDamageTakenRegEx.Match(line);
                if (nonPlayerDamageTakenRegEx.Success)
                {
                    //new Regex(@"The (.*) hits you for (\d+)");
                    int nonPlayerDamageTaken = Int32.TryParse(nonPlayerDamageTakenRegEx.Groups[2].Value, out nonPlayerDamageTaken) ? nonPlayerDamageTaken : 0;
                    NonPlayerDamageTaken += nonPlayerDamageTaken;
                    continue;
                }

                var nonPlayerDamageDoneRegEx = NonPlayerDamageDoneRegEx.Match(line);
                if (nonPlayerDamageDoneRegEx.Success)
                {
                    //new Regex(@"You hit the (.*) for (\d+)");
                    int nonPlayerDamageDone = Int32.TryParse(nonPlayerDamageDoneRegEx.Groups[2].Value, out nonPlayerDamageDone) ? nonPlayerDamageDone : 0;
                    NonPlayerDamageDone += nonPlayerDamageDone;
                    Hit += 1;
                    continue;
                }

                var nonPlayerHealingDoneRegEx = NonPlayerHealingDoneRegEx.Match(line);
                if (nonPlayerHealingDoneRegEx.Success)
                {
                    //new Regex(@"You heal (.*)'s (.*) for (\d+)");
                    int nonPlayerHealingDone = Int32.TryParse(nonPlayerHealingDoneRegEx.Groups[3].Value, out nonPlayerHealingDone) ? nonPlayerHealingDone : 0;
                    NonPlayerHealingDone += nonPlayerHealingDone;
                    HealHit += 1;
                    continue;
                }
                #endregion
            }
            LogFileReadIndex = endIndex;
        }

        /// <summary>
        /// Converts the short month string (Jan, Feb, ect) in the log to an integer value
        /// </summary>
        /// <param name="monthString">Short month string (Jan, Feb, ect)</param>
        /// <returns>Integer</returns>
        private int GetMonthFromShortString(string monthString)
        {
            int month = 1;
            switch (monthString)
            {
                case "Jan":
                    month = 1;
                    break;
                case "Feb":
                    month = 2;
                    break;
                case "Mar":
                    month = 3;
                    break;
                case "Apr":
                    month = 4;
                    break;
                case "May":
                    month = 5;
                    break;
                case "Jun":
                    month = 6;
                    break;
                case "Jul":
                    month = 7;
                    break;
                case "Aug":
                    month = 8;
                    break;
                case "Sep":
                    month = 9;
                    break;
                case "Oct":
                    month = 10;
                    break;
                case "Nov":
                    month = 11;
                    break;
                case "Dec":
                    month = 12;
                    break;
            }
            return month;
        }

        /// <summary>
        /// Populates the LogOpenEntries dictionary
        /// </summary>
        private void ParseLogOpen()
        {
            LogOpenEntries.Clear();
            for (int lineIndex = 0; lineIndex < FilteredLog.Count; lineIndex++)
            {
                var line = FilteredLog[lineIndex];
                var match = ChatLogOpenedRegEx.Match(line);
                if (match.Success)
                {
                    int month = GetMonthFromShortString(match.Groups[2].Value.ToString());
                    string strDateTime = $"{month}/{match.Groups[3].Value}/{match.Groups[5].Value} {match.Groups[4].Value}";
                    DateTime dateTime = Convert.ToDateTime(strDateTime);
                    if (LogOpenEntries.ContainsKey(dateTime))
                    {
                        LogOpenEntries[dateTime] = lineIndex;
                    }
                    else
                    {
                        LogOpenEntries.Add(dateTime, lineIndex);
                    }
                }
            }
            LogOpenEntries = LogOpenEntries.OrderByDescending(x => (DateTime)x.Key).ToDictionary(k => k.Key, v => v.Value);
            if (LogOpenEntries.Count == 0)
                LogOpenEntries.Add(DateTime.Now, 1);
        }

        /// <summary>
        /// Populates the LogCloseEntries dictionary
        /// </summary>
        private void ParseLogClosed()
        {
            //Closed
            LogCloseEntries.Clear();
            for (int lineIndex = 0; lineIndex < FilteredLog.Count; lineIndex++)
            {
                Regex ex = new Regex(@"Chat\sLog\sClosed:\s(\w+\s(\w+)\s+(\d+)\s(\d+:\d+:\d+)\s(\d+))");
                var match = ex.Match(FilteredLog[lineIndex]);
                if (match.Success)
                {
                    int month = GetMonthFromShortString(match.Groups[2].Value.ToString());
                    string strDateTime = $"{month}/{match.Groups[3].Value}/{match.Groups[5].Value} {match.Groups[4].Value}";
                    DateTime dateTime = Convert.ToDateTime(strDateTime);
                    if (LogCloseEntries.ContainsKey(dateTime))
                    {
                        LogCloseEntries[dateTime] = lineIndex;
                    }
                    else
                    {
                        LogCloseEntries.Add(dateTime, lineIndex);
                    }
                }
            }
            LogCloseEntries = LogCloseEntries.OrderByDescending(x => (DateTime)x.Key).ToDictionary(k => k.Key, v => v.Value);
            if (LogCloseEntries.Count == 0)
                LogCloseEntries.Add(DateTime.Now, 1);
        }

    }
}
