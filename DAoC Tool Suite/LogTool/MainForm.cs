using System.Data;
using Logger;

namespace DAoCToolSuite.LogTool
{
    public partial class MainForm : Form
    {
        public static System.Windows.Forms.Timer Timer { get; set; } = new();
        public LogParser LogParser { get; set; } = new() { };
        private static LogManager Logger => LogManager.Instance;
        private Overlay Overlay { get; set; } = new();
        private string LogPath { get; set; }
        private bool FormInitialized { get; set; } = false;
        private static BindingSource BindingSource { get; set; } = new();

        public MainForm()
        {
            InitializeComponent();

            this.FormClosing -= new FormClosingEventHandler(MainForm_Closing);
            this.FormClosing += new FormClosingEventHandler(MainForm_Closing);

            Timer.Tick -= new EventHandler(MainForm_TimerHandler);
            Timer.Tick += new EventHandler(MainForm_TimerHandler);
            Timer.Interval = 1000;

            LogPath = GetLastLogFolderPath();
            LogFileTextBox.Text = LogPath;
            LogParser = new LogParser(LogPath, ParseProgressBar);

            BindingSource.DataSource = ProduceDataTable();
            AttachDataSource();
            AttachLogDates();

            OverLayOpacityControl.Maximum = 100;
            OverLayOpacityControl.Minimum = 0;

            FormInitialized = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            #region Load Settings
            colorDialog1.Color = Properties.Settings.Default.OverlayFontColor;
            FontColorPanel.BackColor = Properties.Settings.Default.OverlayFontColor;
            Overlay.SetLabelForecolor(Properties.Settings.Default.OverlayFontColor);
            Overlay.SetLabelBackcolor(Properties.Settings.Default.OverlayTrans ? Color.DimGray : Color.Black);

            FilterPlayersOnlyCheckBox.Checked = Properties.Settings.Default.PlayersOnly;
            LogParser.PlayersOnlyFilter = Properties.Settings.Default.PlayersOnly;

            OverlayTransparentCheckBox.Checked = Properties.Settings.Default.OverlayTrans;

            LockOverlayButton.Text = Properties.Settings.Default.OverlayLocked ? "Unlock Overlay" : "Lock Overlay";
            Overlay.MoveLabel.Visible = !Properties.Settings.Default.OverlayLocked;

            Overlay.Opacity = Convert.ToDouble(Properties.Settings.Default.OverlayOpacity / 100);
            OverLayOpacityControl.Value = Properties.Settings.Default.OverlayOpacity;
            #endregion

            #region  MainForm Location
            long currentCount = LogTool.Properties.Settings.Default.UseCountMainform;
            if (currentCount > 0)
            {
                Location = LogTool.Properties.Settings.Default.LastLocationMainform;
            }
            else
            {
                //ScreenCentered by Default
                LogTool.Properties.Settings.Default.LastLocationMainform = Location;
            }
            if (currentCount != long.MaxValue)
            {
                LogTool.Properties.Settings.Default.UseCountMainform = currentCount + 1;
            }
            #endregion

            LogTool.Properties.Settings.Default.Save();
        }

        private void MainForm_Closing(object? sender, FormClosingEventArgs e)
        {
            Overlay?.Close();
            Properties.Settings.Default.LastLocationMainform = this.Location;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Sets the Table Alignment for Title and Value.
        /// </summary>
        private void FormatTable()
        {
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[1].DefaultCellStyle.Padding = new Padding(0, 0, 30, 0);
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void AttachDataSource()
        {
            dataGridView1.DataSource = BindingSource;
            FormatTable();
        }

        private void AttachLogDates()
        {
            try
            {
                List<string> logDates = LogParser.LogOpenEntries.Keys.Select(x => x.ToString("G")).ToList();
                LogDatesComboBox.DataSource = logDates;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LogDatesComboBox_SelectedIndexChange(object sender, EventArgs e)
        {
            if (FormInitialized)
            {
                DateTime startDate = Convert.ToDateTime(LogDatesComboBox.Text);
                if (!LogParser.LogOpenEntries.ContainsKey(startDate))
                {
                    LogParser.SetFileIndex(0);
                }
                else
                {
                    int fileIndex = LogParser.LogOpenEntries[startDate];
                    LogParser.SetFileIndex(fileIndex);
                }
                DisplayParseLogStatistics();
            }
        }

        private DataTable ProduceDataTable()
        {
            DataTable dt = new();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            AddRow("Total Damage Done:", LogParser.TotalDamageDone.ToString("N0"), "Total Healing Recieved:", LogParser.HealingTaken.ToString("N0"));
            AddRow("", "", "", "");
            AddRow("Total Spell Dmg:", LogParser.TotalNonMeleeDamageDone.ToString("N0"), "Total Healing Done:", LogParser.TotalHealingDone.ToString("N0"));
            AddRow("Avg Spell Damage:", LogParser.AverageNonMeleeDamageDone.ToString("N0"), "Avg Heal:", LogParser.AverageHealDone.ToString("N0"));
            AddRow("Avg Spell Crit Dmg:", LogParser.AverageNonMeleeCriticalDamageDone.ToString("N0"), "Avg Crit Heal:", LogParser.AverageCriticalHealingDone.ToString("N0"));
            AddRow("Spell Crit Rate:", LogParser.NonMeleeCritRate.ToString("0.0%"), "Heal Crit Rate:", LogParser.HealCritRate.ToString("0.0%"));
            AddRow("", "", "", "");
            AddRow("Total Melee Dmg:", LogParser.TotalMeleeDamageDone.ToString("N0"), "Block Rate:", LogParser.AttacksBlockedRate.ToString("0.0%"));
            AddRow("Avg Melee Dmg:", LogParser.AverageMeleeDamageDone.ToString("N0"), "Evade Rate:", LogParser.AttacksEvadedRate.ToString("0.0%"));
            AddRow("Avg Melee Crit Dmg:", LogParser.AverageCriticalMeleeDamageDone.ToString("N0"), "Parry Rate:", LogParser.AttacksParriedRate.ToString("0.0%"));
            AddRow("Melee Crit Rate:", LogParser.MeleeCritRate.ToString("0.0%"), "Miss Rate:", LogParser.AttackMissRate.ToString("0.0%"));
            AddRow("", "", "", "");
            AddRow("Total Dmg Taken:", LogParser.TotalDamageTaken.ToString("N0"), "Total Pet Damage:", LogParser.TotalPetDamageDone.ToString("N0"));
            AddRow("Dmg Absorbed:", LogParser.DamageTakenAbsorbed.ToString("N0"), "Pet Melee Crit Rate:", LogParser.PetMeleeCritRate.ToString("0.0%"));
            AddRow("Dmg Converted:", LogParser.DamageTakenConverted.ToString("N0"), "Total Pet Healing:", LogParser.TotalPetHealingDone.ToString("N0"));
            AddRow("Dmg Blocked:", LogParser.TotalDamageTakeBlocked.ToString("N0"), "", "");
            AddRow("", "", "Gold Earned:", LogParser.GoldEarned.ToString("N4"));
            AddRow("DeathBlows:", LogParser.DeathBlows.ToString("N0"), "Realm Points Earned:", LogParser.RealmPointsEarned.ToString("N0"));
            AddRow("Deaths:", LogParser.Deaths.ToString("N0"), "IRS:", LogParser.IRS.ToString("N0"));
            return dt;

            void AddRow(string title1 = "", string value1 = "", string title2 = "", string value2 = "")
            {
                DataRow dr = dt.NewRow();
                dr[0] = title1;
                dr[1] = value1;
                dr[2] = title2;
                dr[3] = value2;
                dt.Rows.Add(dr);
            }
        }

        /// <summary>
        /// Updates the statistics displayed in the table and in the overlay.
        /// </summary>
        private void DisplayParseLogStatistics()
        {
            BindingSource.DataSource = ProduceDataTable();

            #region Column 1 Values
            Overlay.Value_TotalDamageDone.Text = LogParser.TotalDamageDone.ToString("N0");
            Overlay.Value_TotalSpellDamage.Text = LogParser.TotalNonMeleeDamageDone.ToString("N0");
            Overlay.Value_AverageSpellDamageDone.Text = LogParser.AverageNonMeleeDamageDone.ToString("N0");
            Overlay.Value_AverageSpellCritDamageDone.Text = LogParser.AverageNonMeleeCriticalDamageDone.ToString("N0");
            Overlay.Value_SpellCritRate.Text = LogParser.NonMeleeCritRate.ToString("0.0%");
            Overlay.Value_TotalMeleeDamage.Text = LogParser.TotalMeleeDamageDone.ToString("N0");
            Overlay.Value_AverageMeleeDamageDone.Text = LogParser.AverageMeleeDamageDone.ToString("N0");
            Overlay.Value_AverageMeleeCritDamageDone.Text = LogParser.AverageCriticalMeleeDamageDone.ToString("N0");
            Overlay.Value_MeleeCritRate.Text = LogParser.MeleeCritRate.ToString("0.0%");
            Overlay.Value_TotalDamageTaken.Text = LogParser.TotalDamageTaken.ToString("N0");
            Overlay.Value_DamageTakenAbsorbed.Text = LogParser.DamageTakenAbsorbed.ToString("N0");
            Overlay.Value_DamageTakenConverted.Text = LogParser.DamageTakenConverted.ToString("N0");
            Overlay.Value_DamageTakenBlocked.Text = LogParser.TotalDamageTakeBlocked.ToString("N0");
            Overlay.Value_DeathBlows.Text = LogParser.DeathBlows.ToString("N0");
            Overlay.Value_Deaths.Text = LogParser.Deaths.ToString("N0");
            #endregion
            #region Column 2 Values
            Overlay.Value_TotalHealingRecieved.Text = LogParser.HealingTaken.ToString("N0");
            Overlay.Value_TotalHealingDone.Text = LogParser.TotalHealingDone.ToString("N0");
            Overlay.Value_AverageHealingDone.Text = LogParser.AverageHealDone.ToString("N0");
            Overlay.Value_AverageCritHealingDone.Text = LogParser.AverageCriticalHealingDone.ToString("N0");
            Overlay.Value_HealCritRate.Text = LogParser.HealCritRate.ToString("0.0%");
            Overlay.Value_BlockRate.Text = LogParser.AttacksBlockedRate.ToString("0.0%");
            Overlay.Value_EvadeRate.Text = LogParser.AttacksEvadedRate.ToString("0.0%");
            Overlay.Value_ParryRate.Text = LogParser.AttacksParriedRate.ToString("0.0%");
            Overlay.Value_MissRate.Text = LogParser.AttackMissRate.ToString("0.0%");
            Overlay.Value_TotalPetDamage.Text = LogParser.TotalPetDamageDone.ToString("N0");
            Overlay.Value_PetMeleeCritRate.Text = LogParser.PetMeleeCritRate.ToString("0.0%");
            Overlay.Value_TotalPetHealing.Text = LogParser.TotalPetHealingDone.ToString("N0");
            Overlay.Value_GoldEarned.Text = LogParser.GoldEarned.ToString("N4");
            Overlay.Value_RealmPoints.Text = LogParser.RealmPointsEarned.ToString("N0");
            Overlay.Value_IRS.Text = LogParser.IRS.ToString("N0");
            #endregion
        }

        /// <summary>
        /// This is the Timer event for parsing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_TimerHandler(object? sender, EventArgs e)
        {
            if (LogParser.HasUnparsedData())
            {
                LogParser.Parse();
                DisplayParseLogStatistics();
            }
        }

        private static string DefaultLogPath()
        {
            return $"{DefaultLogFolder()}\\chat.log";
        }

        private static string DefaultLogFolder()
        {
            string path = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Electronic Arts\\Dark Age of Camelot";
            return path;
        }

        private static string GetLastLogFolderPath()
        {
            string path = Properties.Settings.Default.LastLogPath;
            return string.IsNullOrEmpty(path) ? DefaultLogPath() : Properties.Settings.Default.LastLogPath;
        }

        private static string GetLastLogFolder()
        {
            string path = Properties.Settings.Default.LastLogFolder;
            return string.IsNullOrEmpty(path) ? DefaultLogFolder() : Properties.Settings.Default.LastLogFolder;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            BrowseButton.Enabled = false;
            LogDatesComboBox.Enabled = false;
            Timer.Stop();
            ParseButton.Text = "Start";
            Parsing = false;
            try
            {
                OpenFileDialog ofd = new()
                {
                    Filter = "Log Files|*.log",
                    Multiselect = false,
                    InitialDirectory = GetLastLogFolder()
                };
                _ = ofd.ShowDialog(this);
                LogPath = ofd.FileName;
                LogFileTextBox.Text = ofd.FileName;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            LogParser = new(LogPath, ParseProgressBar)
            {
                PlayersOnlyFilter = FilterPlayersOnlyCheckBox.Checked
            };

            AttachLogDates();
            DisplayParseLogStatistics();
            LogDatesComboBox.Enabled = true;
            BrowseButton.Enabled = true;
            Properties.Settings.Default.LastLogPath = LogPath;
            Properties.Settings.Default.Save();
        }

        private static bool Parsing = false;
        private void ParseButton_Click(object sender, EventArgs e)
        {
            ParseButton.Enabled = false;
            if (Parsing)
            {
                Timer.Stop();
                ParseButton.Text = "Continue";
                Parsing = false;
                ResetButton.Enabled = true;
                LogFileTextBox.Enabled = true;
                BrowseButton.Enabled = true;
            }
            else
            {
                Timer.Start();
                ParseButton.Text = "Pause";
                Parsing = true;
                ResetButton.Enabled = false;
                LogFileTextBox.Enabled = false;
                BrowseButton.Enabled = false;
            }
            ParseButton.Enabled = true;
            LogDatesComboBox.Enabled = false;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetButton.Enabled = false;
            LogParser = new(LogPath, ParseProgressBar)
            {
                PlayersOnlyFilter = FilterPlayersOnlyCheckBox.Checked
            };
            ParseButton.Text = "Start";
            AttachLogDates();
            DisplayParseLogStatistics();
            LogDatesComboBox.Enabled = true;
            ResetButton.Enabled = true;
        }

        private void OverlayButton_Click(object sender, EventArgs e)
        {
            OverlayButton.Enabled = false;
            if (Overlay.Visible)
            {
                OverlayButton.Text = "Show Overlay";
                Overlay.Hide();
            }
            else
            {
                OverlayButton.Text = "Hide Overlay";
                Overlay.Show();
            }
            OverlayButton.Enabled = true;
        }

        private void LockOverlayButton_Click(object sender, EventArgs e)
        {
            LockOverlayButton.Enabled = false;
            if (!Overlay.Visible)
            {
                LockOverlayButton.Enabled = true;
                return;
            }

            if (Overlay.MoveLabel.Visible)
            {
                LockOverlayButton.Text = "Unlock Overlay";
                Overlay.MoveLabel.Hide();
                Properties.Settings.Default.OverlayLocked = true;
            }
            else
            {
                LockOverlayButton.Text = "Lock Overlay";
                Overlay.MoveLabel.Show();
                Properties.Settings.Default.OverlayLocked = false;
            }
            Properties.Settings.Default.Save();
            LockOverlayButton.Enabled = true;
        }

        private void OverLayOpacityControl_ValueChanged(object sender, EventArgs e)
        {
            Overlay.Opacity = Convert.ToDouble(OverLayOpacityControl.Value / 100);
            Overlay.Refresh();
            Properties.Settings.Default.OverlayOpacity = OverLayOpacityControl.Value;
            Properties.Settings.Default.Save();
        }

        private void OverLayFontColorButton_Click(object sender, EventArgs e)
        {
            OverLayFontColorButton.Enabled = false;
            _ = colorDialog1.ShowDialog();
            Overlay.SetLabelForecolor(colorDialog1.Color);
            FontColorPanel.BackColor = colorDialog1.Color;
            OverLayFontColorButton.Enabled = true;
            Properties.Settings.Default.OverlayFontColor = colorDialog1.Color;
            Properties.Settings.Default.Save();
        }

        private void OverlayTransparentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                Overlay.SetLabelBackcolor(Color.DimGray);
            }
            else
            {
                Overlay.SetLabelBackcolor(Color.Black);
            }
            Properties.Settings.Default.OverlayTrans = checkBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void FilterPlayersOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogParser.PlayersOnlyFilter = checkBox.Checked;
            DisplayParseLogStatistics();
            Properties.Settings.Default.PlayersOnly = checkBox.Checked;
            Properties.Settings.Default.Save();

        }
    }
}