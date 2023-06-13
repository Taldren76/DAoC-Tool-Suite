using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Shapes;
using Logger;
using static System.Windows.Forms.LinkLabel;

namespace DAoCToolSuite.LogTool
{
    public class LogParser
    {
        #region Raw Data
        //Damage
        private int DamageDone = 0;
        private int CritDamageDone = 0;
        private int DamageTaken = 0;

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

        //Mitigation
        public int DamageTakenAbsorbed = 0;
        public int DamageDoneAbsorbed = 0;
        public int TotalDamageBlocked = 0;
        public int DamageDoneBlocked = 0;
        public int TotalDamageConverted = 0;

        //NonPlayer
        private int NonPlayerDamageDone = 0;
        private int NonPlayerCriticalDamageDone = 0;
        private int NonPlayerHealingDone = 0;
        private int NonPlayerCriticalHealingDone = 0;
        private int NonPlayerDamageTaken= 0;

        //Misc
        private int Hit = 0;

        private int CritHit = 0;

        private int PetHit = 0; //TODO: Implement this

        private int CritPetHit = 0; //TODO: Implement this

        private int HealHit = 0;         // Number of individual Heals Done. AE counts as multiple Hits.
                                         // This counts both HealingDone and HealSelf.

        private int CritHealHit = 0;     // Number of individual Crit Heals Done. AE counts as multiple Hits.
                                         // This counts both CritHealingDone and CritHealingSelf.    

        #endregion

        #region Heal Stats
        public int TotalCritHealingDone => CriticalHealingDone + CriticalSelfHealingDone + CriticalHealingDoneByYouToYourPet + NonPlayerCriticalHealingDone;
        public int TotalHealingDone => TotalCritHealingDone + HealingDone + SelfHealingDone + AbilitySelfHealingDone + PetHealingDone + PetHealingYou + HealingDoneByYouToYourPet + NonPlayerHealingDone;
        public int TotalHealingToSelf => SelfHealingDone + CriticalSelfHealingDone + AbilitySelfHealingDone + PetHealingYou;
        public double HealSelfRatio => TotalHealingToSelf / (double)(TotalHealingDone == 0 ? 1 : TotalHealingDone);
        public int TotalHealingRecieved => HealingTaken + TotalHealingToSelf + PetHealingYou;
        public int AverageHealingDone => Convert.ToInt32((HealingDone + SelfHealingDone + NonPlayerHealingDone + HealingDoneByYouToYourPet) / (double)(HealHit == 0 ? 1 : HealHit));
        public int AverageCritHealingDone => Convert.ToInt32((CriticalHealingDone + CriticalSelfHealingDone + NonPlayerCriticalHealingDone + CriticalHealingDoneByYouToYourPet) / (double)(CritHealHit == 0 ? 1 : CritHealHit));
        private int TotalHeals => CritHealHit + HealHit;
        public double HealCritRate => CritHealHit / (double)(TotalHeals == 0 ? 1 : TotalHeals);
        public double CritHealRatio => TotalCritHealingDone / (double)(TotalHealingDone == 0 ? 1 : TotalHealingDone);

        #endregion

        #region Damage Stats
        public int TotalDamageDone => DamageDone + CritDamageDone + PetDamageDone + NonPlayerDamageDone + NonPlayerCriticalDamageDone;
        public int TotalCritDamageDone => CritDamageDone + NonPlayerCriticalDamageDone;
        public int AverageDamageDone => Convert.ToInt32((DamageDone + NonPlayerDamageDone) / (double)(Hit == 0 ? 1 : Hit));
        public int AverageCritDamageDone => Convert.ToInt32((CritDamageDone + NonPlayerCriticalDamageDone) / (double)(CritHit == 0 ? 1 : CritHit));
        public int TotalDamageHits => CritHit + Hit;
        public double DamageCritRate => CritHit / (double)(TotalDamageHits == 0 ? 1 : TotalDamageHits);
        public double CritDamageRatio => TotalCritDamageDone / (double)(TotalDamageDone == 0 ? 1 : TotalDamageDone);
        public int TotalDamageTaken => DamageTaken + NonPlayerDamageTaken; //TODO:Add Pet Damage!
        #endregion

        #region Standard Stats
        public int RealmPointsEarned = 0;

        public int DeathBlows = 0;

        public int Deaths = 0;
        public double IRS => RealmPointsEarned / (double)(Deaths == 0 ? 1 : Deaths);
        public double KDR => DeathBlows / (double)(Deaths == 0 ? 1 : Deaths);
        #endregion

        #region RegEx
        //Earnings
        private static readonly Regex RealmPointsEarnedRegEx = new(@"You earn (\d+).* realm points");

        //Damage
        private static readonly Regex DamageDoneRegEx = new(@"You (attack|hit) ((?!the).+) for (\d+)");
        private static readonly Regex CritDamageDoneRegEx = new(@"You (attack|hit) ((?!the)\w+).* for (\d+).*\n.*You critical.* (\d+)");
        private static readonly Regex DamageTakenRegEx = new(@"^(?!The )(\w+) hits you for (\d+)");

        //Healing
        private static readonly Regex HealingDoneRegEx = new(@"You heal ((?!your|.*'s).+) for (\d+)");
        private static readonly Regex CriticalHealingDoneRegEx = new(@"You critical heal.* (\d+).*\n.*You heal ((?!your|.*'s).+) for (\d+)");
        private static readonly Regex AbilitySelfHealingRegEx = new(@"Your (.*) ability heals you .* (\d+)");
        private static readonly Regex HealingBatteryHealRegEx = new(@"(.*) healing battery.* heals (.*) (for |\()(\d+)");
        private static readonly Regex SelfHealingRegEx = new(@"You heal yourself.*(\d+)");
        private static readonly Regex CriticalSelfHealingRegEx = new(@"You critical heal.* (\d+).*\n.*You heal yourself.*(\d+)");
        private static readonly Regex HealingTakenRegEx = new(@"(.*) healed you.*(\d+)");

        //Your Pets
        private static readonly Regex PetHealingRegEx = new(@"Your (.*) healed (.*) for (\d+)");
        private static readonly Regex PetDamageDoneRegEx = new(@"Your (.*) (hits|attacks) (.*) for (\d+)");
        //TODO: Populate PetCriticalDamageDoneRegEx once I know what a chat log line looks like for that.
        private static readonly Regex HealingDoneByYouToYourPetRegEx = new(@"You heal your (.+) for (\d+)");
        private static readonly Regex CriticalHealingDoneByYouToYourPetRegEx = new(@"You heal your (.+) for (\d+).*\n.*You critical .* (\d+)");

        //Mitigation
        private static readonly Regex DamageTakenAbsorbedRegEx = new(@"Your .* absorb.* (\d+)");
        private static readonly Regex DamageDoneAbsorbedRegEx = new(@"absorb.* (\d+).* of your");
        private static readonly Regex DamageBlockedRegEx = new(@"(\d+) damage was blocked by (your|\w+) .*");
        private static readonly Regex DamageConvertedRegEx = new(@"(\d+) damage was converted");

        //Non-Player
        private static readonly Regex NonPlayerDamageTakenRegEx = new(@"The (.*) hits you for (\d+)");
        private static readonly Regex NonPlayerDamageDoneRegEx = new(@"You hit the (.*) for (\d+)");
        private static readonly Regex NonPlayerCriticalDamageDoneRegEx = new(@"You hit the (.+) for (\d+).*\n.*You critical .* (\d+)");
        private static readonly Regex NonPlayerHealingDoneRegEx = new(@"You heal (.*)'s (.*) for (\d+)");
        private static readonly Regex NonPlayerCriticalHealingDoneRegEx = new(@"You critical heal.* (\d+).*\n.*You heal (.*)'s (.*) for (\d+)");

        //Misc
        private static readonly Regex ChatLogOpenedRegEx = new(@"Chat\sLog\sOpened:\s(\w+\s(\w+)\s+(\d+)\s(\d+:\d+:\d+)\s(\d+))");
        private static readonly Regex DeathRegEx = new(@"You have died.*");
        private static readonly Regex DeathBlowRegEx = new(@"You just killed (\w+)");
        //PlayerKillBy: (\w+) was just killed .* by (\w+).*
        #endregion

        public Dictionary<DateTime, int> LogOpenEntries = new();
        public Dictionary<DateTime, int> LogCloseEntries = new();
        public bool PlayersOnlyFilter { get; set; } = false; 
        private static readonly object ThisLock = new();
        private static LogManager Logger => LogManager.Instance;
        private int LogFileReadIndex { get; set; } = -1;
        private string LogPath { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Electronic Arts\\Dark Age of Camelot\\chat.log";
        private static List<Regex> RejectLineContent { get; set; } = new();
        private DateTime LastParse { get; set; } = DateTime.MinValue;
        private List<string> FilteredLog { get; set; } = new();
        TextProgressBar? ProgressBar { get; set; } = null;

        public LogParser()
        {

        }

        public LogParser(string path, TextProgressBar? progressBar = null)
        {
            LogPath = path;
            ProgressBar = progressBar;
            PopulateRejectLineContentList();
            GetFilteredFileContent();
            ParseLogOpen();
            ParseLogClosed();
        }

        public LogParser(string path, int startIndex, TextProgressBar? progressBar = null)
        {
            LogPath = path;
            LogFileReadIndex = startIndex;
            ProgressBar = progressBar;
            PopulateRejectLineContentList();
            GetFilteredFileContent();
            ParseLogOpen();
            ParseLogClosed();
        }

        public void SetFileIndex(int index)
        {
            LogFileReadIndex = index;
        }

        /// <summary>
        /// Determines if the LogFile contains information that has not been parsed.
        /// </summary>
        /// <returns>Boolean</returns>
        public bool HasUnparsedData()
        {
            DateTime lastModified = GetLogLastModified();
            return LastParse < lastModified;
        }

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
            //Since we read two lines at a time to process crits, we filter the last two lines out
            //of the parse and make sure to read them once the file is updated again.

            int startIndex = LogFileReadIndex - 2;
            if (startIndex < 0)
            {
                startIndex = 0;
            }

            int endIndex = FilteredLog.Count - 2;
            if (endIndex < 0)
            {
                endIndex = 1;
            }

            if(ProgressBar != null)
            {
                ProgressBar.Minimum = startIndex;
                ProgressBar.Maximum = endIndex;
                ProgressBar.Value = startIndex;
                ProgressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
                ProgressBar.CustomText = "Parsing File";
                ProgressBar.Visible = true;
            }

            for (int index = startIndex; index < endIndex; index++)
            {
                if (ProgressBar != null)
                {
                    ProgressBar.Value = index;
                    
                }
                string line = FilteredLog[index];       
                if (string.IsNullOrEmpty(line) || line[0].Equals('*')) //Checks for empty or Chat Open/Close lines
                    continue;        
                else if (line[0].Equals('[')) //All parsable lines should start with [HH:MM:SS]
                    line = line.Remove(0, 10).Trim();

                //If there isn't a second line its empty.
                string nextLine = (index + 1 < FilteredLog.Count) ? FilteredLog[index + 1] : "";
                if (string.IsNullOrEmpty(nextLine) || nextLine[0].Equals('*')) //Checks for empty or Chat/Close lines
                    line = "";
                else if(nextLine[0].Equals('[')) //All parsable lines should start with [HH:MM:SS]
                    nextLine = nextLine.Remove(0, 10).Trim();

                //Lines is used to parse crit messages
                string lines = $"{line}\n{nextLine}";

                #region Death   
                //new(@"You have died.*");
                Match deathMatch = DeathRegEx.Match(line);
                if (deathMatch.Success)
                {
                    Deaths += 1;
                    continue;
                }

                //new(@"You have died.*");
                Match deathBlow = DeathBlowRegEx.Match(line);
                if (deathBlow.Success)
                {
                    DeathBlows += 1;
                    continue;
                }

                #endregion

                #region Earnings
                Match realmPointsMatch = RealmPointsEarnedRegEx.Match(line);
                if (realmPointsMatch.Success)
                {
                    int rpsEarned = int.TryParse(realmPointsMatch.Groups[1].Value, out rpsEarned) ? rpsEarned : 0;
                    RealmPointsEarned += rpsEarned;
                    continue;
                }
                #endregion

                #region Damage
                // new Regex(@"You (attack|hit) ((?!the)\w+).* for (\d+).*\n.*You critical.* (\d+)");
                Match critDamageMatch = CritDamageDoneRegEx.Match(lines);
                if (critDamageMatch.Success)
                {
                    int normalDamageDone = int.TryParse(critDamageMatch.Groups[3].Value, out normalDamageDone) ? normalDamageDone : 0;
                    int critDamageDone = int.TryParse(critDamageMatch.Groups[4].Value, out critDamageDone) ? critDamageDone : 0;
                    CritDamageDone += normalDamageDone + critDamageDone;
                    CritHit += 1;
                    index += 1;
                    continue;
                }

                // new Regex(@"You (attack|hit) ((?!the).+) for (\d+)")
                Match damageDoneMatch = DamageDoneRegEx.Match(line);
                if (damageDoneMatch.Success)
                {
                    int damageDone = int.TryParse(damageDoneMatch.Groups[3].Value, out damageDone) ? damageDone : 0;
                    DamageDone += damageDone;
                    Hit += 1;
                    continue;
                }

                // new Regex(@"^(?!The )(\w+) hits you for (\d+)")
                Match damageTakenMatch = DamageTakenRegEx.Match(line);
                if (damageTakenMatch.Success)
                {
                    int damageTaken = int.TryParse(damageTakenMatch.Groups[2].Value, out damageTaken) ? damageTaken : 0;
                    DamageTaken += damageTaken;
                    continue;
                }
                #endregion

                #region Healing
                //new Regex(@"(.*) healing battery.* heals (.*) (for |\()(\d+)");
                Match healingBatteryHealRegEx = HealingBatteryHealRegEx.Match(line);
                if (healingBatteryHealRegEx.Success)
                {
                    string whosBattery = healingBatteryHealRegEx.Groups[1].Value.ToString();
                    string whosHealed = healingBatteryHealRegEx.Groups[2].Value.ToString();
                    int healBattery = int.TryParse(healingBatteryHealRegEx.Groups[4].Value, out healBattery) ? healBattery : 0;
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
                    {
                        HealingTaken += healBattery;
                    }

                    continue;
                }

                //new Regex(@"You critical heal.* (\d+).*\n.*You heal yourself.*(\d+)")
                Match criticalSelfHealingRegEx = CriticalSelfHealingRegEx.Match(lines);
                if (criticalSelfHealingRegEx.Success)
                {
                    int selfCritHeal = int.TryParse(criticalSelfHealingRegEx.Groups[2].Value, out selfCritHeal) ? selfCritHeal : 0;
                    CriticalSelfHealingDone += selfCritHeal;
                    CritHealHit += 1;
                    index += 1;
                    continue;
                }

                //new Regex(@"You critical heal.* (\d+).*\n.*You heal ((?!your|.*'s).+) for (\d+)");
                Match criticalHealingDoneRegEx = CriticalHealingDoneRegEx.Match(lines);
                if (criticalHealingDoneRegEx.Success)
                {
                    int healCritDone = int.TryParse(criticalHealingDoneRegEx.Groups[3].Value, out healCritDone) ? healCritDone : 0;
                    CriticalHealingDone += healCritDone;
                    CritHealHit += 1;
                    index += 1;
                    continue;
                }

                //new Regex(@"You heal yourself.*(\d+)");
                Match selfHealingRegEx = SelfHealingRegEx.Match(line);
                if (selfHealingRegEx.Success)
                {
                    int healSelf = int.TryParse(selfHealingRegEx.Groups[1].Value, out healSelf) ? healSelf : 0;
                    SelfHealingDone += healSelf;
                    HealHit += 1;
                    continue;
                }

                //new Regex(@"Your (.*) ability heals you .* (\d+)");
                Match abilitySelfHealingRegEx = AbilitySelfHealingRegEx.Match(line);
                if (abilitySelfHealingRegEx.Success)
                {
                    int abilitySelfHealing = int.TryParse(abilitySelfHealingRegEx.Groups[2].Value, out abilitySelfHealing) ? abilitySelfHealing : 0;
                    AbilitySelfHealingDone += abilitySelfHealing;
                    continue;
                }

                //new Regex(@"You heal ((?!your|.*'s).+) for (\d+)");
                Match healingDoneMatch = HealingDoneRegEx.Match(line);
                if (healingDoneMatch.Success)
                {
                    int healDone = int.TryParse(healingDoneMatch.Groups[2].Value, out healDone) ? healDone : 0;
                    HealingDone += healDone;
                    HealHit += 1;
                    continue;
                }

                //new Regex(@"(.*) healed you.*(\d+)");
                Match healingTakenRegEx = HealingTakenRegEx.Match(line);
                if (healingTakenRegEx.Success)
                {
                    int healingTaken = int.TryParse(healingTakenRegEx.Groups[2].Value, out healingTaken) ? healingTaken : 0;
                    HealingTaken += healingTaken;
                    continue;
                }
                #endregion

                #region Pets
                //new Regex(@"You heal your (.+) for (\d+) .*\n.* You critical .* (\d+)");
                Match criticalHealingDoneByYouToYourPetRegEx = CriticalHealingDoneByYouToYourPetRegEx.Match(lines);
                if (criticalHealingDoneByYouToYourPetRegEx.Success)
                {
                    int healDone = int.TryParse(criticalHealingDoneByYouToYourPetRegEx.Groups[2].Value, out healDone) ? healDone : 0;
                    int healCritDone = int.TryParse(criticalHealingDoneByYouToYourPetRegEx.Groups[3].Value, out healCritDone) ? healCritDone : 0;
                    CriticalHealingDoneByYouToYourPet += healDone + healCritDone;
                    CritHealHit += 1;
                    index += 1;
                    continue;
                }

                //new Regex(@"Your (.*) healed (.*) for (\d+)");
                Match petHealingRegEx = PetHealingRegEx.Match(line);
                if (petHealingRegEx.Success)
                {
                    string petHealTarget = petHealingRegEx.Groups[2].Value.ToString();
                    int petHealing = int.TryParse(petHealingRegEx.Groups[3].Value, out petHealing) ? petHealing : 0;
                    if (petHealTarget.Equals("you"))
                    {
                        PetHealingYou += petHealing;
                    }
                    else
                    {
                        PetHealingDone += petHealing;
                    }

                    continue;
                }

                //new Regex(@"You heal your (.+) for (\d+)");
                Match healingDoneByYouToYourPetRegEx = HealingDoneByYouToYourPetRegEx.Match(line);
                if (healingDoneByYouToYourPetRegEx.Success)
                {
                    int healPet = int.TryParse(healingDoneByYouToYourPetRegEx.Groups[3].Value, out healPet) ? healPet : 0;
                    HealingDoneByYouToYourPet += healPet;
                    HealHit += 1;
                    continue;
                }

                //new Regex(@"Your (.*) (hits|attacks) (.*) for (\d+)")
                Match petDamageDoneRegEx = PetDamageDoneRegEx.Match(line);
                if (petDamageDoneRegEx.Success)
                {
                    int petDamage = int.TryParse(petDamageDoneRegEx.Groups[3].Value, out petDamage) ? petDamage : 0;
                    PetDamageDone += petDamage;
                    HealHit += 1;
                    continue;
                }
                #endregion

                #region Mitigation
                // new Regex(@"Your .* absorb.* (\d+)");
                Match damageTakenAbsorbedRegEx = DamageTakenAbsorbedRegEx.Match(line);
                if (damageTakenAbsorbedRegEx.Success)
                {
                    int damageTakenAbsorbed = int.TryParse(damageTakenAbsorbedRegEx.Groups[1].Value, out damageTakenAbsorbed) ? damageTakenAbsorbed : 0;
                    DamageTakenAbsorbed += damageTakenAbsorbed;
                    continue;
                }

                // new Regex(@"absorb.* (\d+).* of your");
                Match damageDoneAbsorbedRegEx = DamageDoneAbsorbedRegEx.Match(line);
                if (damageDoneAbsorbedRegEx.Success)
                {
                    int damageDoneAbsorbed = int.TryParse(damageDoneAbsorbedRegEx.Groups[1].Value, out damageDoneAbsorbed) ? damageDoneAbsorbed : 0;
                    DamageDoneAbsorbed += damageDoneAbsorbed;
                    continue;
                }

                //new(@"(\d+) damage was blocked by (your|\w+) .*");
                Match damageBlockedRegEx = DamageBlockedRegEx.Match(line);
                if (damageBlockedRegEx.Success)
                {                    
                    int damageBlocked = int.TryParse(damageBlockedRegEx.Groups[1].Value, out damageBlocked) ? damageBlocked : 0;
                    string whoBenefited = damageBlockedRegEx.Groups[2].Value.ToString();
                    if (whoBenefited.Equals("your"))
                    {
                        TotalDamageBlocked += damageBlocked;
                    }
                    else
                    {
                        DamageDoneBlocked += damageBlocked;
                    }
                    continue;
                }

                //new(@"(\d+) damage was converted");
                Match damageConvertedRegEx = DamageConvertedRegEx.Match(line);
                if (damageConvertedRegEx.Success)
                {
                    int damageConverted = int.TryParse(damageConvertedRegEx.Groups[1].Value, out damageConverted) ? damageConverted : 0;
                    TotalDamageConverted += damageConverted;
                    continue;
                }
                #endregion

                if (PlayersOnlyFilter)
                {
                    continue;
                }

                #region Non-Player

                Match nonPlayerCriticalDamageDoneRegEx = NonPlayerCriticalDamageDoneRegEx.Match(lines);
                if (nonPlayerCriticalDamageDoneRegEx.Success)
                {
                    //new Regex(@"You hit the (.+) for (\d+).*\n.*You critical .* (\d+)");
                    int nonPlayerDamageDone = int.TryParse(nonPlayerCriticalDamageDoneRegEx.Groups[2].Value, out nonPlayerDamageDone) ? nonPlayerDamageDone : 0;
                    int nonPlayerCriticalDamageDone = int.TryParse(nonPlayerCriticalDamageDoneRegEx.Groups[3].Value, out nonPlayerCriticalDamageDone) ? nonPlayerCriticalDamageDone : 0;
                    NonPlayerCriticalDamageDone += nonPlayerCriticalDamageDone + nonPlayerDamageDone;
                    CritHit += 1;
                    index += 1;
                    continue;
                }

                Match nonPlayerCriticalHealingDoneRegEx = NonPlayerCriticalHealingDoneRegEx.Match(lines);
                if (nonPlayerCriticalHealingDoneRegEx.Success)
                {
                    //new Regex(@"You critical heal.* (\d+).*\n.*You heal (.*)'s (.*) for (\d+)");
                    int nonPlayerCriticalHealingDone = int.TryParse(nonPlayerCriticalHealingDoneRegEx.Groups[4].Value, out nonPlayerCriticalHealingDone) ? nonPlayerCriticalHealingDone : 0;
                    NonPlayerCriticalHealingDone += nonPlayerCriticalHealingDone;
                    CritHealHit += 1;
                    index += 1;
                    continue;
                }

                Match nonPlayerDamageTakenRegEx = NonPlayerDamageTakenRegEx.Match(line);
                if (nonPlayerDamageTakenRegEx.Success)
                {
                    //new Regex(@"The (.*) hits you for (\d+)");
                    int nonPlayerDamageTaken = int.TryParse(nonPlayerDamageTakenRegEx.Groups[2].Value, out nonPlayerDamageTaken) ? nonPlayerDamageTaken : 0;
                    NonPlayerDamageTaken += nonPlayerDamageTaken;
                    continue;
                }

                Match nonPlayerDamageDoneRegEx = NonPlayerDamageDoneRegEx.Match(line);
                if (nonPlayerDamageDoneRegEx.Success)
                {
                    //new Regex(@"You hit the (.*) for (\d+)");
                    int nonPlayerDamageDone = int.TryParse(nonPlayerDamageDoneRegEx.Groups[2].Value, out nonPlayerDamageDone) ? nonPlayerDamageDone : 0;
                    NonPlayerDamageDone += nonPlayerDamageDone;
                    Hit += 1;
                    continue;
                }

                Match nonPlayerHealingDoneRegEx = NonPlayerHealingDoneRegEx.Match(line);
                if (nonPlayerHealingDoneRegEx.Success)
                {
                    //new Regex(@"You heal (.*)'s (.*) for (\d+)");
                    int nonPlayerHealingDone = int.TryParse(nonPlayerHealingDoneRegEx.Groups[3].Value, out nonPlayerHealingDone) ? nonPlayerHealingDone : 0;
                    NonPlayerHealingDone += nonPlayerHealingDone;
                    HealHit += 1;
                    continue;
                }
                #endregion
            }
            LogFileReadIndex = endIndex;

            if (ProgressBar != null)
            {
                ProgressBar.Visible = false;
            }
        }

        /// <summary>
        /// Converts the short month string (Jan, Feb, ect) in the log to an integer value
        /// </summary>
        /// <param name="monthString">Short month string (Jan, Feb, ect)</param>
        /// <returns>Integer</returns>
        private static int GetMonthFromShortString(string monthString)
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
                string line = FilteredLog[lineIndex];
                Match match = ChatLogOpenedRegEx.Match(line);
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
            LogOpenEntries = LogOpenEntries.OrderByDescending(x => x.Key).ToDictionary(k => k.Key, v => v.Value);
            if (LogOpenEntries.Count == 0)
            {
                LogOpenEntries.Add(DateTime.Now, 1);
            }
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
                Regex ex = new(@"Chat\sLog\sClosed:\s(\w+\s(\w+)\s+(\d+)\s(\d+:\d+:\d+)\s(\d+))");
                Match match = ex.Match(FilteredLog[lineIndex]);
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
            LogCloseEntries = LogCloseEntries.OrderByDescending(x => x.Key).ToDictionary(k => k.Key, v => v.Value);
            if (LogCloseEntries.Count == 0)
            {
                LogCloseEntries.Add(DateTime.Now, 1);
            }
        }
        /// <summary>
        /// Polulates a list with substrings of log lines to be rejected from the filtered log.
        /// </summary>
        private static void PopulateRejectLineContentList()
        {
            List<Regex> rejectLineContent = new()
            {
                new Regex(@"@@.*"),
                new Regex(@"##.*"),
                new Regex(@"You (?!hit|critical|critically|begin|drain|earn|fail|attack|perform|prepare|steal|use|just|heal|have died).*"),
                new Regex(@"You prepare to sprint!"),
                new Regex(@"Your (?!health buffer|magic buffer|health battery).*"),
                new Regex(@"You've (?!been awarded).*"),
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
        private static bool IsRejectContent(string line)
        {
            if (string.IsNullOrEmpty(line.Trim()))
            {
                return true;
            }

            foreach (Regex reject in RejectLineContent)
            {
                if (line[0].Equals('*'))
                {
                    return false;
                }
                else if (!line![0].Equals('['))
                {
                    return true;
                }

                try
                {
                    string? delimitedLine = line.Remove(0, 10).Trim();
                    if (string.IsNullOrEmpty(delimitedLine) || reject.IsMatch(delimitedLine))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
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
            long currentPosition = 0;
            lock (ThisLock)
            {
                
                if (!File.Exists(LogPath))
                {
                    FilteredLog = new();
                    return;
                }

                long length = new FileInfo(LogPath).Length;

                if (ProgressBar != null)
                {
                    ProgressBar.Value = 0;
                    ProgressBar.Minimum = 0;
                    ProgressBar.Maximum = 100;
                    ProgressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
                    ProgressBar.CustomText = "Reading File";
                    ProgressBar.Visible = true;
                }

                List<string> filteredList = new();
                FileAttributes attributes = File.GetAttributes(LogPath);
                using (FileStream fs = new(LogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using StreamReader sr = new(fs);
                    while (!sr.EndOfStream)
                    {
                        string? lineRead = sr.ReadLine();
                        if (lineRead is null)
                        {
                            continue;
                        }
                        UpdateProgressBar(lineRead.Length);

                        if (!IsRejectContent(lineRead))
                        {
                            filteredList.Add(lineRead);
                        }
                    }
                }
                File.SetAttributes(LogPath, attributes);
                FilteredLog = filteredList;

                if (ProgressBar != null)
                {
                    ProgressBar.Visible = false;
                }

                void UpdateProgressBar(int lineLength)
                {
                    currentPosition += lineLength;// or plus 2 if you need to take into account carriage return
                    if (ProgressBar != null)
                    {
                        ProgressBar.Value = (int)(((decimal)currentPosition / (decimal)length) * (decimal)100);
                    }
                }
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
                DateTime lastModified = File.GetLastWriteTime(LogPath);
                return lastModified;
            }
        }
    }
}
