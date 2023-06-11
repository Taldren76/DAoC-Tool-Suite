using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace DAoCToolSuite.LogTool
{
    public class ParseLog
    {
        #region Raw Data
        private int HealHit = 0;         // Number of individual Heals Done. AE counts as multiple Hits.
                                         // This counts both HealingDone and HealSelf.

        private int CritHealHit = 0;     // Number of individual Crit Heals Done. AE counts as multiple Hits.
                                         // This counts both CritHealingDone and CritHealingSelf.

        private int HealingDone = 0;     // Healing done by spells cast by the player on others.

        private int HealSelf = 0;        // Healing done by spells cast by the player on self. 

        private int CritHealingDone = 0; // Total critical healing done to others..

        private int CritHealingSelf = 0; // Total critical healing done to self.

        private int HealProcPetSelf = 0; // Total healing to yourself from items or pets.

        private int HealProcPet = 0;     // Total healing to others from items or pets.

        private int HealTaken = 0;       // Number of times you were healed.

        private int HealingTaken = 0;    // Total healing you have recieved.

        private int Hit = 0;

        private int CritHit = 0;

        private int DamageDone = 0;

        private int CritDamageDone = 0;
        #endregion

        #region Heal Stats
        public int TotalHealingDone => HealingDone + HealSelf + CritHealingDone;
        public int TotalHealingToSelf => HealSelf + HealProcPetSelf;
        public int HealSelfRatio => Convert.ToInt32(((double)TotalHealingToSelf / (double)(TotalHealingDone==0 ? 1 : TotalHealingDone))*100);
        public int TotalHealingRecieved => HealingTaken + TotalHealingToSelf;
        public int AverageHealingDone => Convert.ToInt32((double)HealingDone / (double)(HealHit == 0 ? 1 : HealHit));
        public int AverageCritHealingDone => Convert.ToInt32((double)CritHealingDone / (double)(CritHealHit == 0 ? 1 : CritHealHit));
        private int TotalHeals => CritHealHit + HealHit;
        public int HealCritRate => Convert.ToInt32(((double)CritHealHit / (double)(TotalHeals == 0 ? 1 : TotalHeals)) * 100);
        public int CritHealRatio => Convert.ToInt32(((double)CritHealingDone / (double)(TotalHealingDone == 0 ? 1 : TotalHealingDone)) * 100);
        
        #endregion

        #region Damage Stats
        public int TotalDamageDone => DamageDone + CritDamageDone;
        public int AverageDamageDone => Convert.ToInt32((double)DamageDone / (double)(Hit == 0 ? 1 : Hit));
        public int AverageCritDamageDone => Convert.ToInt32((double)CritDamageDone / (double)(CritHit == 0 ? 1 : CritHit));
        public int TotalDamageHits => CritHit + Hit;
        public int DamageCritRate => Convert.ToInt32(((double)CritHit / (double)(TotalDamageHits == 0 ? 1 : TotalDamageHits)) * 100);
        public int CritDamageRatio => Convert.ToInt32(((double)CritDamageDone / (double)(TotalDamageDone == 0 ? 1 : TotalDamageDone)) * 100);
        #endregion

        #region Standard Stats
        public int RealmPoints = 0;

        public int DeathBlows = 0;

        public int Deaths = 0;
        public int IRS => Convert.ToInt32((double)RealmPoints / (double)(Deaths == 0 ? 1 : Deaths));
        public int KDR => Convert.ToInt32((double)DeathBlows / (double)(Deaths == 0 ? 1 : Deaths));
        #endregion

        private static readonly object ThisLock = new object();
        private int LogFileReadIndex { get; set; } = -1;
        private string LogPath { get; set; } = "chat.log";
        private List<string> RejectLineContent { get; set; } = new();
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

        public ParseLog(string path)
        {
            LogPath = path;           
            PopulateRejectLineContentList();
            GetFilteredFileContent();
            ParseLogOpen();
            ParseLogClosed();
        }

        public ParseLog(string path, int startIndex)
        {
            LogPath = path;
            LogFileReadIndex = startIndex;
            PopulateRejectLineContentList();
            GetFilteredFileContent();
            ParseLogOpen();
            ParseLogClosed();
        }

        /// <summary>
        /// Polulates a list with substrings of log lines to be rejected from the filtered log.
        /// </summary>
        private void PopulateRejectLineContentList()
        {
            List<string> rejectLineContent = new() {
                    "You are already",
                    "You must equip",
                    "You cannot summon",
                    "You prepare to sprint!",
                    "You are moving",
                    "You are now",
                    "You are no ",
                    "You can't",
                    "You move",
                    "as a follow up!",
                    "Your target is",
                    "You assist",
                    "You mount",
                    "That target is",
                    "This target must be"
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
            foreach (string reject in RejectLineContent)
            {
                string? delimitedLine = line.Split(']').Last().Trim();
                if (string.IsNullOrEmpty(delimitedLine) || delimitedLine.Contains(reject) || line.Contains("@@"))
                {
                    return true;
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

        //private void DontFuckThisUp()
        //{
        //    List<string> fullList = new();
        //    FileAttributes attributes = File.GetAttributes(LogPath);
        //    using (FileStream fs = new(LogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        //    {
        //        using StreamReader sr = new(fs);
        //        while (!sr.EndOfStream)
        //        {
        //            var lineRead = sr.ReadLine();
        //            if (lineRead is not null)
        //                fullList.Add(lineRead);
        //        }
        //    }
        //    File.SetAttributes(LogPath, attributes)
        //    List<string> rejectList = new();
        //    foreach (string line in fullList)
        //    {
        //        foreach (string reject in RejectLineContent)
        //        {
        //            string? delimitedLine = line.Split(']').Last().Trim();
        //            if (string.IsNullOrEmpty(delimitedLine) || delimitedLine.Contains(reject) || line.Contains("@@"))
        //            {
        //                rejectList.Add(line);
        //                break;
        //            }
        //        }
        //    }
        //    var filteredList = fullList.Except(rejectList).ToList();
        //    FilteredLog = filteredList;
        //}

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

        /// <summary>
        /// Parses the FilteredLog list and populates statistics.
        /// </summary>
        public void ParseFile()
        {
            if (!HasUnparsedData())
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

            for (int index = startIndex;index < endIndex; index++)
            {
                string line = FilteredLog[index];
                string line2 = (index + 1 < FilteredLog.Count) ? FilteredLog[(index + 1)] : "";
                string twoLines = $"{line}\n{line2}";

                Regex rps = new Regex(@"You earn (\d+).* realm points");
                var rpsMatch = rps.Match(line);
                if (rpsMatch.Success)
                {
                    int rpsEarned = Int32.TryParse(rpsMatch.Groups[1].Value, out rpsEarned) ? rpsEarned : 0;
                    RealmPoints += rpsEarned;
                    continue;
                }

                Regex youCrit = new Regex(@"You hit .+ (\d+) .*\n.* You critical .* (\d+)");
                var youCritMatch = youCrit.Match(twoLines);
                if (youCritMatch.Success)
                {
                    int normalDamageDone = Int32.TryParse(youCritMatch.Groups[1].Value, out normalDamageDone) ? normalDamageDone : 0;
                    int critDamageDone = Int32.TryParse(youCritMatch.Groups[2].Value, out critDamageDone) ? critDamageDone : 0;
                    CritDamageDone += normalDamageDone + critDamageDone;
                    CritHit += 1;
                    continue;
                }

                Regex youHit = new Regex(@"You hit .+ (\d+)");
                var youHitMatch = youHit.Match(line);
                if (youHitMatch.Success)
                {
                    int damageDone = Int32.TryParse(youHitMatch.Groups[1].Value, out damageDone) ? damageDone : 0;
                    DamageDone += damageDone;
                    Hit += 1;
                    continue;
                }

                Regex healProcsPets = new Regex(@"Your .* heals you .* (\d+)");
                var healProcsPetsMatch = healProcsPets.Match(line);
                if (healProcsPetsMatch.Success)
                {
                    int healProcPet = Int32.TryParse(healProcsPetsMatch.Groups[1].Value, out healProcPet) ? healProcPet : 0;
                    HealProcPetSelf += healProcPet;
                    continue;
                }

                //You critical heal for an additional 139 hit points.

                Regex youSelfHeal = new Regex(@"heal yourself .* (\d+)");
                var youSelfHealMatch = youSelfHeal.Match(line);
                if (youSelfHealMatch.Success)
                {
                    int healSelfDone = Int32.TryParse(youSelfHealMatch.Groups[1].Value, out healSelfDone) ? healSelfDone : 0;
                    HealSelf += healSelfDone;
                    HealHit += 1;
                    continue;
                }

                Regex youHeal = new Regex(@"You heal .* (\d+)");
                var youHealMatch = youHeal.Match(line);
                if (youHealMatch.Success)
                {
                    int healDone = Int32.TryParse(youHealMatch.Groups[1].Value, out healDone) ? healDone : 0;
                    HealingDone += healDone;
                    HealHit += 1;
                    continue;
                }

                Regex healedBy = new Regex(@"healed you .* (\d+)");
                var healedByMatch = healedBy.Match(line);
                if (healedByMatch.Success)
                {
                    int healsTaken = Int32.TryParse(healedByMatch.Groups[1].Value, out healsTaken) ? healsTaken : 0;
                    HealingTaken += healsTaken;
                    continue;
                }
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
                Regex ex = new Regex(@"Chat\sLog\sOpened:\s(\w+\s(\w+)\s+(\d+)\s(\d+:\d+:\d+)\s(\d+))");
                var match = ex.Match(FilteredLog[lineIndex]);
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
