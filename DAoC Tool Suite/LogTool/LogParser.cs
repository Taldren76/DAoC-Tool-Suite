using Logger;
using System.IO;
using System.Text.RegularExpressions;

namespace DAoCToolSuite.LogTool
{
    public class LogParser
    {
        #region Raw Stats
        private int Bladeturned = 0;
        private int CritHealHit = 0;
        private int CriticalHealingDone = 0;
        private int DamageTaken = 0;
        private int DamageDoneBlocked = 0;
        private int DamageTakenBlocked = 0;
        private int HealHit = 0;
        private int HealingDone = 0;
        private int MeleeCritHit = 0;
        private int MeleeCriticalDamageDone = 0;
        private int MeleeDamageDone = 0;
        private int MeleeHit = 0;
        private int MeleeHitBlocked = 0;
        private int MeleeHitEvaded = 0;
        private int MeleeHitParried = 0;
        private int MeleeMisses = 0;
        private int NonMeleeCritHit = 0;
        private int NonMeleeCriticalDamageDone = 0;
        private int NonMeleeDamageDone = 0;
        private int NonMeleeHit = 0;
        private int NonPlayerCritHealHit = 0;
        private int NonPlayerCriticalHealingDone = 0;
        private int NonPlayerDamageDoneBlocked = 0;
        private int NonPlayerDamageTaken = 0;
        private int NonPlayerDamageTakenBlocked = 0;
        private int NonPlayerHealHit = 0;
        private int NonPlayerHealingDone = 0;
        private int NonPlayerMeleeCritHit = 0;
        private int NonPlayerMeleeCriticalDamageDone = 0;
        private int NonPlayerMeleeHit = 0;
        private int NonPlayerMeleeHitBlocked = 0;
        private int NonPlayerMeleeHitEvaded = 0;
        private int NonPlayerMeleeHitParried = 0;
        private int NonPlayerNonMeleeCritHit = 0;
        private int NonPlayerNonMeleeHit = 0;
        private int PetNonPlayerMeleeCriticalDamageDone = 0;
        private int NonPlayerPetNonMeleeDamageDone = 0;
        private int NonPlayerMeleeDamageDone = 0;
        private int NonPlayerPetNonMeleeHit = 0;
        private int NonPlayerNonMeleeCriticalDamageDone = 0;
        private int NonPlayerNonMeleeDamageDone = 0;
        private int NonPlayerPetMeleeHit = 0;
        private int PetNonPlayerMeleeCritHit = 0;
        private int PetMeleeCriticalDamageDone = 0;
        private int PetNonMeleeDamageDone = 0;
        private int PetNonPlayerMeleeDamageDone = 0;
        private int PetHealHit = 0;
        private int PetNonMeleeHit = 0;
        private int PetMeleeCritHit = 0;
        private int PetMeleeDamageDone = 0;
        private int PetMeleeHit = 0;
        private int YouBlock = 0;
        private int YouBlockNonPlayer = 0;
        private int YouEvade = 0;
        private int YouEvadeNonPlayer = 0;
        private int YouParry = 0;
        private int YouParryNonPlayer = 0;
        #endregion

        #region Standard Stats
        public double GoldEarned = 0;
        public int RealmPointsEarned = 0;
        public int DeathBlows = 0;
        public int Deaths = 0;
        public double IRS => RealmPointsEarned / (Deaths == 0 ? 1 : Deaths);
        public int TotalDamageTaken => DamageTaken + (PlayersOnlyFilter ? 0 : NonPlayerDamageTaken);
        public int TotalDamageDone => TotalMeleeDamageDone + TotalNonMeleeDamageDone + TotalPetDamageDone;
        public int DamageDoneAbsorbed = 0;
        public int DamageTakenAbsorbed = 0;
        public int DamageTakenConverted = 0;
        public int AttackDamageBlocked => DamageDoneBlocked + (PlayersOnlyFilter ? 0 : NonPlayerDamageDoneBlocked);
        public int TotalDamageTakeBlocked => DamageTakenBlocked + (PlayersOnlyFilter ? 0 : NonPlayerDamageTakenBlocked);
        #endregion

        #region NonMelee
        private int TotalNonMeleeHits => NonMeleeHit + (PlayersOnlyFilter ? 0 : NonPlayerNonMeleeHit);
        private int TotalNonMeleeCritHits => NonMeleeCritHit + (PlayersOnlyFilter ? 0 : NonPlayerNonMeleeCritHit);
        private int TotalHits => TotalNonMeleeHits + TotalNonMeleeCritHits;
        private int TotalNonMeleeDamage => NonMeleeDamageDone + (PlayersOnlyFilter ? 0 : NonPlayerNonMeleeDamageDone);
        private int TotalNonMeleeCritDamage => NonMeleeCriticalDamageDone + (PlayersOnlyFilter ? 0 : NonPlayerNonMeleeCriticalDamageDone);
        public int TotalNonMeleeDamageDone => TotalNonMeleeDamage + TotalNonMeleeCritDamage;
        public int AverageNonMeleeCriticalDamageDone => TotalNonMeleeCritDamage / (TotalNonMeleeCritHits == 0 ? 1 : TotalNonMeleeCritHits);
        public int AverageNonMeleeDamageDone => TotalNonMeleeDamage / (TotalNonMeleeHits == 0 ? 1 : TotalNonMeleeHits);
        public double NonMeleeCritRate => TotalNonMeleeCritHits / (double)(TotalHits == 0 ? 1 : TotalHits);
        #endregion

        #region Melee
        private int TotalMeleeEvaded => MeleeHitEvaded + (PlayersOnlyFilter ? 0 : NonPlayerMeleeHitEvaded);
        private int TotalMeleeParried => MeleeHitParried + (PlayersOnlyFilter ? 0 : NonPlayerMeleeHitParried);
        private int TotalMeleeBlocked => MeleeHitBlocked + (PlayersOnlyFilter ? 0 : NonPlayerMeleeHitBlocked);
        public double AttacksEvadedRate => TotalMeleeEvaded / (double)(TotalMeleeAttacks == 0 ? 1 : TotalMeleeAttacks);
        public double AttacksParriedRate => TotalMeleeParried / (double)(TotalMeleeAttacks == 0 ? 1 : TotalMeleeAttacks);
        public double AttacksBlockedRate => TotalMeleeBlocked / (double)(TotalMeleeAttacks == 0 ? 1 : TotalMeleeAttacks);
        public double AttacksBladeturnedRate => Bladeturned / (double)(TotalMeleeAttacks == 0 ? 1 : TotalMeleeAttacks);
        public double AttackMissRate => MeleeMisses / (double)(TotalMeleeAttacks == 0 ? 1 : TotalMeleeAttacks);
        public int Blocks => YouBlock + (PlayersOnlyFilter ? 0 : YouBlockNonPlayer);
        public int Parries => YouParry + (PlayersOnlyFilter ? 0 : YouParryNonPlayer);
        public int Evades => YouEvade + (PlayersOnlyFilter ? 0 : YouEvadeNonPlayer);
        private int MeleeHits => MeleeHit + (PlayersOnlyFilter ? 0 : NonPlayerMeleeHit);
        private int MeleeCritHits => MeleeCritHit + (PlayersOnlyFilter ? 0 : NonPlayerMeleeCritHit);
        private int TotalMeleeHits => MeleeHits + MeleeCritHits;
        private int TotalNonCriticalMeleeDamageDone => MeleeDamageDone + (PlayersOnlyFilter ? 0 : NonPlayerMeleeDamageDone);
        private int TotalCriticalMeleeDamageDone => MeleeCriticalDamageDone + (PlayersOnlyFilter ? 0 : NonPlayerMeleeCriticalDamageDone);
        public int AverageMeleeDamageDone => TotalNonCriticalMeleeDamageDone / (MeleeHits == 0 ? 1 : MeleeHits);
        public int AverageCriticalMeleeDamageDone => TotalNonCriticalMeleeDamageDone / (MeleeCritHits == 0 ? 1 : MeleeCritHits);
        private int TotalMeleeAttacks => Bladeturned + TotalMeleeEvaded + TotalMeleeParried + TotalMeleeBlocked + MeleeMisses + TotalMeleeHits;
        public double MeleeCritRate => MeleeCritHits / (double)(TotalMeleeHits == 0 ? 1 : TotalMeleeHits);
        public int TotalMeleeDamageDone => TotalNonCriticalMeleeDamageDone + TotalCriticalMeleeDamageDone;
        #endregion

        #region Healing
        private int HealHits => HealHit + (PlayersOnlyFilter ? 0 : NonPlayerHealHit);
        private int CritHealHits => CritHealHit + (PlayersOnlyFilter ? 0 : NonPlayerCritHealHit);
        private int TotalHealHits => HealHits + CritHealHits;
        private int TotalNonCritHealingDone => HealingDone + (PlayersOnlyFilter ? 0 : NonPlayerHealingDone);
        private int TotalCritHealingDone => CriticalHealingDone + (PlayersOnlyFilter ? 0 : NonPlayerCriticalHealingDone);
        public int TotalHealingDone => TotalCritHealingDone + TotalNonCritHealingDone;
        public int AverageHealDone => TotalNonCritHealingDone / (HealHits == 0 ? 1 : HealHits);
        public int AverageCriticalHealingDone => TotalCritHealingDone / (CritHealHits == 0 ? 1 : CritHealHits);
        public double HealCritRate => CritHealHits / (double)(TotalHealHits == 0 ? 1 : TotalHealHits);
        public int HealingTaken = 0;
        #endregion

        #region Pet
        private int PetMeleeCritHits => PetMeleeCritHit + (PlayersOnlyFilter ? 0 : PetNonPlayerMeleeCritHit);
        private int PetMeleeHits => PetMeleeHit + (PlayersOnlyFilter ? 0 : NonPlayerPetMeleeHit);
        private int TotalPetMeleeHits => PetMeleeCritHits + PetMeleeHits;
        private int CombinedPetMeleeDamageDone => PetMeleeDamageDone + (PlayersOnlyFilter ? 0 : PetNonPlayerMeleeDamageDone);
        private int PetCritMeleeDamageDone => PetMeleeCriticalDamageDone + (PlayersOnlyFilter ? 0 : PetNonPlayerMeleeCriticalDamageDone);
        private int TotalPetMeleeDamageDone => CombinedPetMeleeDamageDone + PetCritMeleeDamageDone;
        public double PetMeleeCritRate => PetMeleeCritHits / (double)(TotalPetMeleeHits == 0 ? 1 : TotalPetMeleeHits);
        private int TotalPetNonMeleeDamageDone => PetNonMeleeDamageDone + (PlayersOnlyFilter ? 0 : NonPlayerPetNonMeleeDamageDone);
        private int TotalPetNonMeleeDamageHits => PetNonMeleeHit + (PlayersOnlyFilter ? 0 : NonPlayerPetNonMeleeHit);
        public int AveragePetCriticalMeleeDamageDone => PetCritMeleeDamageDone / (PetMeleeCritHits == 0 ? 1 : PetMeleeCritHits);
        public int AveragePetMeleeDamageDone => CombinedPetMeleeDamageDone / (TotalPetMeleeHits == 0 ? 1 : TotalPetMeleeHits);
        public int AveragePetNonMeleeDamage => TotalPetNonMeleeDamageDone / (TotalPetNonMeleeDamageHits == 0 ? 1 : TotalPetNonMeleeDamageHits);
        public int AveragePetHeal => TotalPetHealingDone / (PetHealHit == 0 ? 1 : PetHealHit);
        public int TotalPetDamageDone => TotalPetNonMeleeDamageDone + TotalPetMeleeDamageDone;
        public int TotalPetHealingDone { get; private set; } = 0;
        #endregion

        #region RegEx
        //Earnings
        private static readonly Regex RealmPointsEarnedRegEx = new(@"You earn (\d+) (?:extra |)realm points"); //G1: RPs Earned
        private static readonly Regex MoneyEarnedRegEx = new(@"(Your share of the loot is|You pick up) .*");
        private static readonly Regex MoneyRegEx = new(@"(\d+) (platinum|gold|silver|copper)");

        //Damage
        private static readonly Regex NonMeleeCriticalDamageDoneRegEx = new(@"You hit (the |)(.*)(?:'s|) for (\d+).*\n.*You critical.* (\d+)"); //G1: Non-Player Test, G2: TargetName, G3: DamageDone, G4: Critical DamageDone
        private static readonly Regex NonMeleeDamageDoneRegEx = new(@"You hit (the |)(.*)(?:'s|) for (\d+)"); //G1: Non-Player Test, G2: TargetName, G3: DamageDone
        private static readonly Regex DamageTakenRegEx = new(@"(The |)(.*) hits you for (\d+)"); //G1: Non-Player Test, G2: AttackerName, G3: DamageTaken

        //Spell
        private static readonly Regex ResistedRegEx = new(@"(The |)(.*) resists the effect"); //G1: Non-Player Test, G2: TargetName

        //Melee
        private static readonly Regex MeleeCriticalDamageDoneRegEx = new(@"You attack (the |)(.*) with .* for (\d+).*\n.*You critical.* (\d+)"); //G1: Non-Player Test, G2: TargetName, G3: MeleeDamage, G4: Critical MeleeDamage
        private static readonly Regex MeleeDamageDoneRegEx = new(@"You attack (the |)(.*) with .* for (\d+)"); //G1: Non-Player Test, G2: TargetName, G3: MeleeDamage
        private static readonly Regex NonPlayerMeleeDefenseRegEx = new(@"The (.*) attacks .* (block|parr(?:y|ied)|evade)"); //G1:AttackerName, G2: DefenseType
        private static readonly Regex PlayerMeleeDefenseRegEx = new(@"You (evade|block|parr(?:y|ied)) (.*)'s attack!"); //G1: DefenseType, G2: AttackerName
        private static readonly Regex MeleeAttackDefended = new(@"(The |)(\w+) (block(?:s|ed)|parrie(?:s|d)|evade(?:s|d)) your attack!"); //G1: Non-Player Test, G2: TargetName, G3: DefenceType
        private static readonly Regex MeleeAttackBladeturnedRegEx = new(@"Your (attack|strike) .*absorbed .*\n.*You miss!"); //You strike Bladeturn
        private static readonly Regex MeleeMissRegEx = new(@"You miss!"); //You miss

        //Healing
        private static readonly Regex CriticalHealingDoneRegEx = new(@"You critical heal.* (\d+).*\n.*You heal (the |)(yourself|\w+) for (\d+)"); //G1: Crit Bonus (ignore), //G2: Non-Player Test, G3: TargetName, G4: Critical Healing Done
        private static readonly Regex HealingDoneRegEx = new(@"You heal (the |)(yourself|\w+) for (\d+)"); //G1: Non-Player Test, G2: TargetName, G3: Healing Done
        private static readonly Regex AbilitySelfHealingRegEx = new(@"Your (.*) ability heals you .* (\d+)"); //G1: Ability Name, G2: SelfHealing
        private static readonly Regex HealingBatteryHealRegEx = new(@"((?:Y|y)our|\w+)(?:'s|) healing battery (?:fully heals|heals) (\w+).* (?:\(|)(\d+)"); //G1: OwnerName, G2: TargetName, G3: Healing
        private static readonly Regex HealingTakenRegEx = new(@"(.*) healed you.*(\d+)"); //G1: CasterName, G2: HealingTaken
        private static readonly Regex HealingTaken2RegEx = new(@"You are healed by (\w+)(?:'s|) .* (\d+)"); //G1: CasterName, G2: HealingTaken

        //Pets
        private static readonly Regex PetCriticalMeleeDamageDoneRegEx = new(@"Your (.*) attacks (the |)(.*) and hits for (\d+).*\n.* criticals .* (\d+)"); //G1: PetName, G2: Non-Player Test, G3: TargetName, G4: Damage, G5: Crit Damage
        private static readonly Regex PetMeleeDamageDoneRegEx = new(@"Your (.*) attacks (the |)(.*) and hits for (\d+)"); //G1: PetName, G2: Non-Player Test, G3: TargetName, G4: Damage
        private static readonly Regex PetSpellDamageDoneRegEx = new(@"Your pet's spell hits (the |)(.*) for (\d+)"); //G1: Non-Player Test, G2: TargetName, G3: Damage
        private static readonly Regex CriticalHealingDoneByYouToYourPetRegEx = new(@"You critical heal.* (\d+).*\n.*You heal your (.+) for (\d+)"); //G1: Critical Healing, G2: PetName, G3: Total Healing Done
        private static readonly Regex HealingDoneByYouToYourPetRegEx = new(@"You heal your (.+) for (\d+)"); //G1: PetName, G2: Healing
        private static readonly Regex CriticalHealingDoneByYouToPetsRegEx = new(@"You critical heal.* (\d+).*\n.*You heal (.*)'s (.*) for (\d+)"); //G1: Critical Bonus, G2: PetOwner Name, G3: PetName, G4: Critical Healing Done
        private static readonly Regex HealingDoneByYouToPetsRegEx = new(@"You heal (.*)'s (.*) for (\d+)");  //G1: PetOwner Name, G2: PetName, G3: Healing Done
        private static readonly Regex PetHealingRegEx = new(@"Your (.*) healed (the |)(.*) for (\d+)"); //G1: PetName, G2: Non-Player Test, G3: TargetName, G4 Heal

        //Mitigation
        private static readonly Regex DamageDoneAbsorbedRegEx = new(@"(A|Your) .* absorb(?:s|) (\d+).*"); //G1: Owner Test, G2: Damage Absorbed
        private static readonly Regex DamageBlockedRegEx = new(@"(\d+) damage was blocked by (the |)(your|\w+)(?:'s|)"); //G1: DamageBlocked, G2:Non-Player Test, G3:OwnerName
        private static readonly Regex DamageConvertedToEnduranceAndPowerRegEx = new(@"(\d+) damage was converted to endurance and power!"); //G1: DamageConverted
        private static readonly Regex DamageConvertedToHealingRegEx = new(@"Your .* converts (\d+) damage to healing!"); //G1: DamageConverted to SelfHealing

        //Misc
        private static readonly Regex ChatLogOpenedRegEx = new(@"Chat\sLog\sOpened:\s(\w+\s(\w+)\s+(\d+)\s(\d+:\d+:\d+)\s(\d+))");
        private static readonly Regex DeathRegEx = new(@"You have died\.");
        private static readonly Regex DeathBlowRegEx = new(@"You just killed (\w+)");
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
        private TextProgressBar? ProgressBar { get; set; } = null;

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

        /// <summary>
        /// Sets the current read posiiton within the file.
        /// </summary>
        /// <param name="index"></param>
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
            if (!HasUnparsedData())
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
                startIndex = 1;
            }

            int endIndex = FilteredLog.Count - 2;
            if (endIndex < 0)
            {
                endIndex = 1;
            }

            if (ProgressBar != null)
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
                {
                    continue;
                }
                else if (line[0].Equals('[')) //All parsable lines should start with [HH:MM:SS]
                {
                    line = line.Remove(0, 10).Trim();
                }

                //If there isn't a second line its empty.
                string nextLine = (index + 1 < FilteredLog.Count) ? FilteredLog[index + 1] : "";
                if (string.IsNullOrEmpty(nextLine) || nextLine[0].Equals('*')) //Checks for empty or Chat/Close lines
                {
                    line = "";
                }
                else if (nextLine[0].Equals('[')) //All parsable lines should start with [HH:MM:SS]
                {
                    nextLine = nextLine.Remove(0, 10).Trim();
                }

                //Lines is used to parse crit messages
                string lines = $"{line}\n{nextLine}";

                #region Earnings  
                Match realmPointsEarnedMatch = RealmPointsEarnedRegEx.Match(line);
                if (realmPointsEarnedMatch.Success)
                {
                    //G1: RPs Earned
                    int g1 = Convert.ToInt32(realmPointsEarnedMatch.Groups[1].Value);
                    RealmPointsEarned += g1;
                    continue;
                }

                Match moneyEarnedMatch = MoneyEarnedRegEx.Match(line);
                if (moneyEarnedMatch.Success)
                {
                    //(Your share|You pick up) - 1 match

                    MatchCollection moneyMatches = MoneyRegEx.Matches(line);
                    double goldEarned = 0;
                    foreach (Match match in moneyMatches.Cast<Match>())
                    {
                        //(\d+) (platinum|gold|silver|copper) - 1to4 matches
                        //G1: Quantity
                        //G2: Type
                        int g1 = Convert.ToInt32(match.Groups[1].Value);
                        string g2 = match.Groups[2].Value.ToString();
                        switch (g2.ToLower())
                        {
                            case "platinum":
                                goldEarned += g1 * 1000;
                                continue;
                            case "gold":
                                goldEarned += g1;
                                continue;
                            case "silver":
                                goldEarned += (double)g1 / 100;
                                continue;
                            case "copper":
                                goldEarned += (double)g1 / 10000;
                                continue;
                        }
                    }
                    GoldEarned += goldEarned;
                    continue;
                }
                #endregion

                #region Damage
                Match nonMeleeCriticalDamageDone = NonMeleeCriticalDamageDoneRegEx.Match(lines);
                if (nonMeleeCriticalDamageDone.Success)
                {
                    //G1: Non-Player Test, G2: TargetName, G3: DamageDone, G4: Critical DamageDone
                    string? g1 = nonMeleeCriticalDamageDone.Groups[1].Value.ToString();
                    _ = nonMeleeCriticalDamageDone.Groups[2].Value.ToString();
                    int g3 = Convert.ToInt32(nonMeleeCriticalDamageDone.Groups[3].Value);
                    int g4 = Convert.ToInt32(nonMeleeCriticalDamageDone.Groups[4].Value);
                    if (string.IsNullOrEmpty(g1))
                    {
                        NonMeleeCriticalDamageDone += g3 + g4;
                        NonMeleeCritHit += 1;
                    }
                    else
                    {
                        NonPlayerNonMeleeCriticalDamageDone += g3 + g4;
                        NonPlayerNonMeleeCritHit += 1;
                    }


                    index += 1;
                    continue;
                }

                Match nonMeleeDamageDoneMatch = NonMeleeDamageDoneRegEx.Match(line);
                if (nonMeleeDamageDoneMatch.Success)
                {
                    //G1: Non-Player Test, G2: TargetName, G3: DamageDone
                    string? g1 = nonMeleeDamageDoneMatch.Groups[1].Value.ToString();
                    _ = nonMeleeDamageDoneMatch.Groups[2].Value.ToString();
                    int g3 = Convert.ToInt32(nonMeleeDamageDoneMatch.Groups[3].Value);
                    if (string.IsNullOrEmpty(g1))
                    {
                        NonMeleeDamageDone += g3;
                        NonMeleeHit += 1;
                    }
                    else
                    {
                        NonPlayerNonMeleeDamageDone += g3;
                        NonPlayerNonMeleeHit += 1;
                    }
                    continue;
                }

                Match damageTakenMatch = DamageTakenRegEx.Match(line);
                if (damageTakenMatch.Success)
                {
                    //G1: Non-Player Test, G2: AttackerName, G3: DamageTaken
                    string? g1 = damageTakenMatch.Groups[1].Value.ToString();
                    _ = damageTakenMatch.Groups[2].Value.ToString();
                    int g3 = Convert.ToInt32(damageTakenMatch.Groups[3].Value);
                    if (string.IsNullOrEmpty(g1))
                    {
                        DamageTaken += g3;
                    }
                    else
                    {
                        NonPlayerDamageTaken += g3;
                    }

                    continue;
                }
                #endregion

                #region Melee
                Match meleeCriticalDamageDoneMatch = MeleeCriticalDamageDoneRegEx.Match(lines);
                if (meleeCriticalDamageDoneMatch.Success)
                {
                    //You attack (the |)(.*) with .* for (\d+).*\n.*You critical.* (\d+)
                    //G1: Non-Player Test, G2: TargetName, G3: MeleeDamage, G4: Critical MeleeDamage
                    string? g1 = meleeCriticalDamageDoneMatch.Groups[1].Value.ToString();
                    _ = meleeCriticalDamageDoneMatch.Groups[2].Value.ToString();
                    int g3 = Convert.ToInt32(meleeCriticalDamageDoneMatch.Groups[3].Value);
                    int g4 = Convert.ToInt32(meleeCriticalDamageDoneMatch.Groups[4].Value);
                    if (string.IsNullOrEmpty(g1))
                    {
                        MeleeCritHit += 1;
                        MeleeCriticalDamageDone += g3 + g4;
                    }
                    else
                    {
                        NonPlayerMeleeCritHit += 1;
                        NonPlayerMeleeCriticalDamageDone += g3 + g4;
                    }
                    index += 1;
                    continue;
                }

                Match meleeDamageDoneMatch = MeleeDamageDoneRegEx.Match(line);
                if (meleeDamageDoneMatch.Success)
                {
                    //You attack (the |)(.*) with .* for (\d+)
                    //G1: Non-Player Test, G2: TargetName, G3: MeleeDamage
                    string? g1 = meleeDamageDoneMatch.Groups[1].Value.ToString();
                    _ = meleeDamageDoneMatch.Groups[2].Value.ToString();
                    int g3 = Convert.ToInt32(meleeDamageDoneMatch.Groups[3].Value);
                    if (string.IsNullOrEmpty(g1))
                    {
                        MeleeHit += 1;
                        MeleeDamageDone += g3;
                    }
                    else
                    {
                        NonPlayerMeleeHit += 1;
                        NonPlayerMeleeDamageDone += g3;
                    }
                    continue;
                }

                Match nonPlayerMeleeDefenseMatch = NonPlayerMeleeDefenseRegEx.Match(line);
                if (nonPlayerMeleeDefenseMatch.Success)
                {
                    //The (.*) attacks .* (block|parr(?:y|ied)|evade)
                    //G1:AttackerName, G2: DefenseType
                    _ = nonPlayerMeleeDefenseMatch.Groups[1].Value.ToString();
                    string g2 = nonPlayerMeleeDefenseMatch.Groups[2].Value.ToString();
                    switch (g2.ToLower())
                    {
                        case "evade":
                            NonPlayerMeleeHitEvaded += 1;
                            break;
                        case "parry":
                        case "parried":
                            NonPlayerMeleeHitParried += 1;
                            break;
                        case "block":
                            NonPlayerMeleeHitBlocked += 1;
                            break;
                    }
                    continue;
                }

                Match playerMeleeDefenseMatch = PlayerMeleeDefenseRegEx.Match(line);
                if (playerMeleeDefenseMatch.Success)
                {
                    //The (.*) attacks .* (block|parr(?:y|ied)|evade)
                    //G1: DefenseType, G2: AttackerName
                    string g1 = playerMeleeDefenseMatch.Groups[1].Value.ToString();
                    _ = playerMeleeDefenseMatch.Groups[2].Value.ToString();
                    switch (g1.ToLower())
                    {
                        case "evade":
                            MeleeHitEvaded += 1;
                            break;
                        case "parry":
                        case "parried":
                            MeleeHitParried += 1;
                            break;
                        case "block":
                            MeleeHitBlocked += 1;
                            break;
                    }
                    continue;
                }

                Match meleeAttackDefendedMatch = MeleeAttackDefended.Match(line);
                if (meleeAttackDefendedMatch.Success)
                {
                    //(The |)(\w+) (block(?:s|ed)|parrie(?:s|d)|evade(?:s|d)) your attack!
                    //G1: Non-Player Test, G2: TargetName, G3: DefenceType
                    string? g1 = playerMeleeDefenseMatch.Groups[1].Value.ToString();
                    _ = playerMeleeDefenseMatch.Groups[2].Value.ToString();
                    string g3 = playerMeleeDefenseMatch.Groups[3].Value.ToString();
                    switch (g3.ToLower())
                    {
                        case "evade":
                        case "evades":
                        case "evaded":
                            if (string.IsNullOrEmpty(g1))
                            {
                                YouEvade += 1;
                            }
                            else
                            {
                                YouEvadeNonPlayer += 1;
                            }

                            break;
                        case "parry":
                        case "parries":
                        case "parried":
                            if (string.IsNullOrEmpty(g1))
                            {
                                YouParry += 1;
                            }
                            else
                            {
                                YouParryNonPlayer += 1;
                            }

                            break;
                        case "block":
                        case "blocks":
                        case "blocked":
                            if (string.IsNullOrEmpty(g1))
                            {
                                YouBlock += 1;
                            }
                            else
                            {
                                YouBlockNonPlayer += 1;
                            }

                            break;
                    }
                    continue;
                }

                Match meleeAttackBladeturnedMatch = MeleeAttackBladeturnedRegEx.Match(line);
                if (meleeAttackBladeturnedMatch.Success)
                {
                    // Your(attack | strike).* absorbed.*\n.* You miss!
                    Bladeturned += 1;
                    continue;
                }

                Match meleeMissMatch = MeleeMissRegEx.Match(line);
                if (meleeMissMatch.Success)
                {
                    //I have no good way to determine if this is against players or not. 
                    //I have to assume it is.
                    MeleeMisses += 1;
                    continue;
                }
                #endregion

                #region Healing
                Match criticalHealingDoneMatch = CriticalHealingDoneRegEx.Match(lines);
                if (criticalHealingDoneMatch.Success)
                {
                    //You critical heal.* (\d+).*\n.*You heal (the |)(yourself|\w+) for (\d+)
                    ////G1: Crit Bonus (ignore), //G2: Non-Player Test, G3: TargetName, G4: Critical Healing Done
                    _ = Convert.ToInt32(criticalHealingDoneMatch.Groups[1].Value);
                    string? g2 = criticalHealingDoneMatch.Groups[2].Value?.ToString();
                    string g3 = criticalHealingDoneMatch.Groups[3].Value.ToString();
                    int g4 = Convert.ToInt32(criticalHealingDoneMatch.Groups[4].Value);

                    if (g3.ToLower().Equals("yourself"))
                    {
                        HealingTaken += g4;
                    }

                    if (string.IsNullOrEmpty(g2))
                    {
                        CriticalHealingDone += g4;
                        CritHealHit += 1;
                    }
                    else
                    {
                        NonPlayerCriticalHealingDone += g4;
                        NonPlayerCritHealHit += g4;
                    }
                    index += 1;
                    continue;
                }

                Match healingDoneMatch = HealingDoneRegEx.Match(line);
                if (healingDoneMatch.Success)
                {
                    //You heal (the |)(yourself|\w+) for (\d+) 
                    //G1: Non-Player Test, G2: TargetName, G3: Healing Done
                    string? g1 = healingDoneMatch.Groups[1].Value?.ToString();
                    string g2 = healingDoneMatch.Groups[2].Value.ToString();
                    int g3 = Convert.ToInt32(healingDoneMatch.Groups[3].Value);

                    if (g2.ToLower().Equals("yourself"))
                    {
                        HealingTaken += g3;
                    }

                    if (string.IsNullOrEmpty(g1))
                    {
                        HealingDone += g3;
                        HealHit += 1;
                    }
                    else
                    {
                        NonPlayerHealingDone += g3;
                        NonPlayerHealHit += 1;
                    }
                    continue;
                }

                Match abilitySelfHealingMatch = AbilitySelfHealingRegEx.Match(line);
                if (abilitySelfHealingMatch.Success)
                {
                    //Your (.*) ability heals you .* (\d+) 
                    //G1: Ability Name, G2: SelfHealing
                    _ = abilitySelfHealingMatch.Groups[1].Value.ToString();
                    int g2 = Convert.ToInt32(abilitySelfHealingMatch.Groups[2].Value);
                    HealingDone += g2;
                    HealingTaken += g2;
                    continue;
                }

                Match healingBatteryHealMatch = HealingBatteryHealRegEx.Match(line);
                if (healingBatteryHealMatch.Success)
                {
                    //((?:Y|y)our|\w+)(?:'s|) healing battery (?:fully heals|heals) (\w+).* (?:\(|)(\d+)
                    //G1: OwnerName, G2: TargetName, G3: Healing
                    string g1 = healingBatteryHealMatch.Groups[1].Value.ToString();
                    _ = healingBatteryHealMatch.Groups[2].Value.ToString();
                    int g3 = Convert.ToInt32(healingBatteryHealMatch.Groups[3].Value);
                    if (g1.ToLower().Equals("your") && g1.ToLower().Equals("you"))
                    {
                        HealingTaken += g3;
                        HealingDone += g3;
                    }
                    else if (g1.ToLower().Equals("your"))
                    {
                        HealingDone += g3;
                    }
                    else
                    {
                        HealingTaken += g3;
                    }

                    continue;
                }

                Match healingTakenMatch = HealingTakenRegEx.Match(line);
                if (healingTakenMatch.Success)
                {
                    //(.*) healed you.*(\d+)
                    //G1: CasterName, G2: HealingTaken
                    _ = healingTakenMatch.Groups[1].Value.ToString();
                    int g2 = Convert.ToInt32(healingTakenMatch.Groups[2].Value);
                    HealingTaken += g2;
                    continue;
                }

                Match healingTaken2Match = HealingTaken2RegEx.Match(line);
                if (healingTaken2Match.Success)
                {
                    //(.*) healed you.*(\d+)
                    //G1: CasterName, G2: HealingTaken
                    _ = healingTaken2Match.Groups[1].Value.ToString();
                    int g2 = Convert.ToInt32(healingTaken2Match.Groups[2].Value);
                    HealingTaken += g2;
                    continue;
                }
                #endregion

                #region Pets
                Match petCriticalMeleeDamageDoneMatch = PetCriticalMeleeDamageDoneRegEx.Match(lines);
                if (petCriticalMeleeDamageDoneMatch.Success)
                {
                    //Your (.*) attacks (the |)(.*) and hits for (\d+).*\n.* criticals .* (\d+)"); 
                    //G1: PetName, G2: Non-Player Test, G3: TargetName, G4: Damage, G5: Crit Damage
                    _ = petCriticalMeleeDamageDoneMatch.Groups[1].Value.ToString();
                    string? g2 = petCriticalMeleeDamageDoneMatch.Groups[2].Value?.ToString();
                    _ = petCriticalMeleeDamageDoneMatch.Groups[3].Value.ToString();
                    int g4 = Convert.ToInt32(petCriticalMeleeDamageDoneMatch.Groups[4].Value);
                    int g5 = Convert.ToInt32(petCriticalMeleeDamageDoneMatch.Groups[5].Value);
                    if (string.IsNullOrEmpty(g2))
                    {
                        PetMeleeCritHit += 1;
                        PetMeleeCriticalDamageDone += g4 + g5;
                    }
                    else
                    {
                        PetNonPlayerMeleeCritHit += 1;
                        PetNonPlayerMeleeCriticalDamageDone += g4 + g5;
                    }

                    index += 1;
                    continue;
                }

                Match petMeleeDamageDoneMatch = PetMeleeDamageDoneRegEx.Match(line);
                if (petMeleeDamageDoneMatch.Success)
                {
                    //Your (.*) attacks (the |)(.*) and hits for (\d+)"); 
                    //G1: PetName, G2: Non-Player Test, G3: TargetName, G4: Damage
                    _ = petMeleeDamageDoneMatch.Groups[1].Value.ToString();
                    string? g2 = petMeleeDamageDoneMatch.Groups[2].Value?.ToString();
                    _ = petMeleeDamageDoneMatch.Groups[3].Value.ToString();
                    int g4 = Convert.ToInt32(petMeleeDamageDoneMatch.Groups[4].Value);
                    if (string.IsNullOrEmpty(g2))
                    {
                        PetMeleeHit += 1;
                        PetMeleeDamageDone += g4;
                    }
                    else
                    {
                        NonPlayerPetMeleeHit += 1;
                        PetNonPlayerMeleeDamageDone += g4;
                    }

                    continue;
                }

                Match petSpellDamageDoneMatch = PetSpellDamageDoneRegEx.Match(line);
                if (petSpellDamageDoneMatch.Success)
                {
                    //Your pet's spell hits (the |)(.*) for (\d+)"); 
                    //G1: Non-Player Test, G2: TargetName, G3: Damage
                    string? g1 = petSpellDamageDoneMatch.Groups[1].Value?.ToString();
                    _ = petSpellDamageDoneMatch.Groups[2].Value.ToString();
                    int g3 = Convert.ToInt32(petSpellDamageDoneMatch.Groups[3].Value);
                    if (string.IsNullOrEmpty(g1))
                    {
                        PetNonMeleeHit += 1;
                        PetNonMeleeDamageDone += g3;
                    }
                    else
                    {
                        NonPlayerPetNonMeleeHit += 1;
                        NonPlayerPetNonMeleeDamageDone += g3;
                    }

                    continue;
                }

                Match criticalHealingDoneByYouToYourPetMatch = CriticalHealingDoneByYouToYourPetRegEx.Match(lines);
                if (criticalHealingDoneByYouToYourPetMatch.Success)
                {
                    //You critical heal.* (\d+).*\n.*You heal your (.+) for (\d+)"); 
                    //G1: Critical Healing, G2: PetName, G3: Total Healing Done
                    _ = Convert.ToInt32(criticalHealingDoneByYouToYourPetMatch.Groups[1].Value);
                    _ = criticalHealingDoneByYouToYourPetMatch.Groups[2].Value.ToString();
                    int g3 = Convert.ToInt32(criticalHealingDoneByYouToYourPetMatch.Groups[3].Value);

                    CriticalHealingDone += g3;
                    CritHealHit += 1;

                    index += 1;
                    continue;
                }

                Match healingDoneByYouToYourPetMatch = HealingDoneByYouToYourPetRegEx.Match(line);
                if (healingDoneByYouToYourPetMatch.Success)
                {
                    //You heal your (.+) for (\d+)"); 
                    //G1: PetName, G2: Healing
                    _ = healingDoneByYouToYourPetMatch.Groups[1].Value.ToString();
                    int g2 = Convert.ToInt32(healingDoneByYouToYourPetMatch.Groups[2].Value);

                    HealingDone += g2;
                    HealHit += 1;

                    continue;
                }

                Match criticalHealingDoneByYouToPetsMatch = CriticalHealingDoneByYouToPetsRegEx.Match(lines);
                if (criticalHealingDoneByYouToPetsMatch.Success)
                {
                    //You critical heal.* (\d+).*\n.*You heal (.*)'s (.*) for (\d+)"); 
                    //G1: Critical Bonus, G2: PetOwner Name, G3: PetName, G4: Critical Healing Done
                    _ = Convert.ToInt32(criticalHealingDoneByYouToPetsMatch.Groups[1].Value);
                    _ = criticalHealingDoneByYouToPetsMatch.Groups[2].Value.ToString();
                    _ = criticalHealingDoneByYouToPetsMatch.Groups[3].Value.ToString();
                    int g4 = Convert.ToInt32(criticalHealingDoneByYouToPetsMatch.Groups[4].Value);

                    CriticalHealingDone += g4;
                    CritHealHit += 1;

                    index += 1;
                    continue;
                }

                Match healingDoneByYouToPetsMatch = HealingDoneByYouToPetsRegEx.Match(line);
                if (healingDoneByYouToPetsMatch.Success)
                {
                    //You heal (.*)'s (.*) for (\d+)");  
                    //G1: PetOwner Name, G2: PetName, G3: Healing Done
                    _ = healingDoneByYouToPetsMatch.Groups[1].Value.ToString();
                    _ = healingDoneByYouToPetsMatch.Groups[2].Value.ToString();
                    int g3 = Convert.ToInt32(healingDoneByYouToPetsMatch.Groups[3].Value);

                    HealingDone += g3;
                    HealHit += 1;

                    continue;
                }

                Match petHealingMatch = PetHealingRegEx.Match(line);
                if (petHealingMatch.Success)
                {
                    //Your (.*) healed (the |)(.*) for (\d+)"); 
                    //G1: PetName, G2: Non-Player Test, G3: TargetName, G4 Heal
                    _ = petHealingMatch.Groups[1].Value.ToString();
                    _ = petHealingMatch.Groups[2]?.Value.ToString();
                    string g3 = petHealingMatch.Groups[3].Value.ToString();
                    int g4 = Convert.ToInt32(petHealingMatch.Groups[4].Value);

                    if (g3.ToLower().Equals("you"))
                    {
                        HealingTaken += g4;
                    }

                    TotalPetHealingDone += g4;
                    PetHealHit += 1;

                    continue;
                }

                #endregion

                #region Mitigiation
                Match damageDoneAbsorbedMatch = DamageDoneAbsorbedRegEx.Match(line);
                if (damageDoneAbsorbedMatch.Success)
                {
                    //(A|Your) .* absorb(?:s|) (\d+).*
                    //G1: Owner Test, G2: Damage Absorbed
                    string g1 = damageDoneAbsorbedMatch.Groups[1].Value.ToString();
                    int g2 = Convert.ToInt32(damageDoneAbsorbedMatch.Groups[2].Value);

                    if (g1.ToLower().Equals("your"))
                    {
                        DamageTakenAbsorbed += g2;
                    }
                    else
                    {
                        DamageDoneAbsorbed += g2;
                    }

                    continue;
                }

                Match damageBlockedMatch = DamageBlockedRegEx.Match(line);
                if (damageBlockedMatch.Success)
                {
                    //(\d+) damage was blocked by (the |)(your|\w+)(?:'s|)
                    //G1: DamageBlocked, G2:Non-Player Test, G3:OwnerName
                    int g1 = Convert.ToInt32(damageBlockedMatch.Groups[1].Value);
                    string? g2 = damageBlockedMatch.Groups[2].Value?.ToString();
                    string g3 = damageBlockedMatch.Groups[3].Value.ToString();
                    if (string.IsNullOrEmpty(g2))
                    {
                        if (g3.ToLower().Equals("your"))
                        {
                            DamageTakenBlocked += g1;
                        }
                        else
                        {
                            DamageDoneBlocked += g1;
                        }
                    }
                    else
                    {
                        if (g3.ToLower().Equals("your"))
                        {
                            NonPlayerDamageTakenBlocked += g1;
                        }
                        else
                        {
                            NonPlayerDamageDoneBlocked += g1;

                        }
                    }
                    continue;
                }

                Match damageConvertedToEnduranceAndPowerMatch = DamageConvertedToEnduranceAndPowerRegEx.Match(line);
                if (damageConvertedToEnduranceAndPowerMatch.Success)
                {
                    //(\d+) damage was converted to endurance and power!"); 
                    //G1: DamageConverted
                    int g1 = Convert.ToInt32(damageConvertedToEnduranceAndPowerMatch.Groups[1].Value);
                    DamageTakenConverted += g1;
                    continue;
                }

                Match damageConvertedToHealingMatch = DamageConvertedToHealingRegEx.Match(line);
                if (damageConvertedToHealingMatch.Success)
                {
                    //Your .* converts (\d+) damage to healing!"); 
                    //G1: DamageConverted to SelfHealing
                    int g1 = Convert.ToInt32(damageConvertedToHealingMatch.Groups[1].Value);
                    DamageTakenConverted += g1;
                    HealingDone += g1;
                    HealingTaken += g1;
                    continue;
                }
                #endregion

                #region Misc
                Match deathMatch = DeathRegEx.Match(line);
                if (deathMatch.Success)
                {
                    Deaths += 1;
                }

                Match deathBlowMatch = DeathBlowRegEx.Match(line);
                if (deathBlowMatch.Success)
                {
                    DeathBlows += 1;
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
                    string strDateTime = $"{month}-{match.Groups[3].Value}-{match.Groups[5].Value} {match.Groups[4].Value}";

                    int Month = month;
                    int Day = int.TryParse(match.Groups[3].Value, out Day) ? Day : 1;
                    int Year = int.TryParse(match.Groups[5].Value, out Year) ? Year : 2001;
                    int Hour = int.TryParse(match.Groups[4].Value.Split(':')[0], out Hour) ? Hour : 1;
                    int Minute = int.TryParse(match.Groups[4].Value.Split(':')[1], out Minute) ? Minute : 1;
                    int Second = int.TryParse(match.Groups[4].Value.Split(':')[2], out Second) ? Second : 1;

                    //DateTime test = new DateTime(Year, Month, Day, Hour, Minute, Second);

                    DateTime dateTime = new(Year, Month, Day, Hour, Minute, Second); // = DateTime.Parse(strDateTime, CultureInfo.GetCultureInfo("en-US")); //Convert.ToDateTime(strDateTime);
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
                LogOpenEntries.Add(DateTime.Now, 0);
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
                    //string strDateTime = $"{month}/{match.Groups[3].Value}/{match.Groups[5].Value} {match.Groups[4].Value}";

                    int Month = month;
                    int Day = int.TryParse(match.Groups[3].Value, out Day) ? Day : 1;
                    int Year = int.TryParse(match.Groups[5].Value, out Year) ? Year : 2001;
                    int Hour = int.TryParse(match.Groups[4].Value.Split(':')[0], out Hour) ? Hour : 1;
                    int Minute = int.TryParse(match.Groups[4].Value.Split(':')[1], out Minute) ? Minute : 1;
                    int Second = int.TryParse(match.Groups[4].Value.Split(':')[2], out Second) ? Second : 1;
                    DateTime dateTime = new(Year, Month, Day, Hour, Minute, Second);
                    //DateTime dateTime = Convert.ToDateTime(strDateTime);
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
                new Regex(@"You (?!hit|miss|critical|critically|begin|drain|earn|fail|attack|perform|prepare|steal|use|just|heal|are healed|have died|pick up).*"),
                new Regex(@"You begin moving .*"),
                new Regex(@"You prepare to sprint!"),
                new Regex(@"Your (?!health buffer|magic buffer|health battery|pet|pet's|share of|([\w\s]+) attacks|([\w\s]+) criticals).*"),
                new Regex(@"You've (?!been awarded).*"),
                new Regex(@"That target.*"),
                new Regex(@".*casts a spell!"),
                new Regex(@".*is dead!"),
                new Regex(@".*as a follow up!"),
                new Regex(@"(.*) uses [the ]*(.*)\."),
                new Regex(@"(.*) doesn't currently.*"),
                new Regex(@"EV (\w+) groups")
            };
            RejectLineContent = rejectLineContent;
        }

        private static readonly Regex QuestFilterRegEx = new(@"\[[\w\s]+\](?!\.)");

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
                    if (QuestFilterRegEx.IsMatch(line))
                    {
                        return true;
                    }
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
                    ProgressBar.Minimum = 0;
                    ProgressBar.Maximum = 100;
                    ProgressBar.Value = 0;
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
                        ProgressBar.Value = (int)(currentPosition / (decimal)length * 100);
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
