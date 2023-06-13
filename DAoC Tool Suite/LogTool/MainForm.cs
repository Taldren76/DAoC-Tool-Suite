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
        private bool OverlayLock { get; set; }
        private string LogPath { get; set; }
        private bool FormInitialized { get; set; } = false;
        private Dictionary<DateTime, int> CustomLogDates { get; set; } = new();
        BindingSource BindingSource { get; set; } = new();

        public MainForm()
        {
            InitializeComponent();
            this.FormClosing -= new FormClosingEventHandler(MainForm_Closing);
            this.FormClosing += new FormClosingEventHandler(MainForm_Closing);
            LogPath = DefaultLogPath();
            LogFileTextBox.Text = LogPath;
            Timer.Tick -= new EventHandler(MainForm_TimerHandler);
            Timer.Tick += new EventHandler(MainForm_TimerHandler);
            Timer.Interval = 1000;
            LogParser = new LogParser(GetLastLogFolderPath());
            AttachLogDates();
            OverLayOpacityControl.Maximum = 100;
            OverLayOpacityControl.Minimum = 0;
            BindingSource.DataSource = ProduceDataTable();
            AttachDataSource();
            FormatTable();
            FormInitialized = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
            colorDialog1.Color = Properties.Settings.Default.OverlayFontColor;
            FontColorPanel.BackColor = Properties.Settings.Default.OverlayFontColor;
            Overlay.SetLabelForecolor( Properties.Settings.Default.OverlayFontColor);          
            FilterPlayersOnlyCheckBox.Checked = Properties.Settings.Default.PlayersOnly;
            OverlayTransparentCheckBox.Checked = Properties.Settings.Default.OverlayTrans;
            Overlay.SetLabelBackcolor(Properties.Settings.Default.OverlayTrans ? Color.DimGray : Color.Black);
            LogParser.PlayersOnlyFilter = Properties.Settings.Default.PlayersOnly;
            LockOverlayButton.Text = Properties.Settings.Default.OverlayLocked ? "Unlock Overlay" : "Lock Overlay";
            Overlay.MoveLabel.Visible = !Properties.Settings.Default.OverlayLocked;
            Overlay.Opacity = Convert.ToDouble(Properties.Settings.Default.OverlayOpacity / 100);
            OverLayOpacityControl.Value = Properties.Settings.Default.OverlayOpacity;
            LogTool.Properties.Settings.Default.Save();
        }

        private void MainForm_Closing(object? sender, FormClosingEventArgs e)
        {
            Overlay?.Close();
            Properties.Settings.Default.LastLocationMainform = this.Location;
            Properties.Settings.Default.Save();
        }

        private void FormatTable()
        {
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[1].DefaultCellStyle.Padding = new Padding(0, 0, 30, 0);
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void AttachDataSource()
        {
            dataGridView1.DataSource = BindingSource;
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
            }
            DisplayParseLogStatistics();
        }

        private DataTable ProduceDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            AddRow("Average Heal:", LogParser.AverageHealingDone.ToString("N0"), "Average Damage:", LogParser.AverageDamageDone.ToString("N0"));
            AddRow("Average Crit Heal:", LogParser.AverageCritHealingDone.ToString("N0"), "Average Crit Damage:", LogParser.AverageCritDamageDone.ToString("N0"));
            AddRow("Total Healing Done:", LogParser.TotalHealingDone.ToString("N0"), "Total Damage Done:", LogParser.TotalDamageDone.ToString("N0"));
            AddRow("Heal Crit Rate:", LogParser.HealCritRate.ToString("0.0%"), "Damage Absorbed:", LogParser.DamageDoneAbsorbed.ToString("N0"));
            AddRow("Crit:Heal Ratio:", LogParser.CritHealRatio.ToString("0.0%"), "Damage Blocked:", LogParser.DamageDoneBlocked.ToString("N0"));
            AddRow("Self:Heal Ratio:", LogParser.HealSelfRatio.ToString("0.0%"), "Damage Crit Rate:", LogParser.DamageCritRate.ToString("0.0%"));
            AddRow("", "", "Crit:Damage Ratio:", LogParser.CritDamageRatio.ToString("0.0%"));
            AddRow("Total Healing Recieved:", LogParser.TotalHealingRecieved.ToString("N0"), "", "");
            AddRow("", "", "DeathBlows:", LogParser.DeathBlows.ToString("N0"));
            AddRow("Total Damage Taken:", LogParser.TotalDamageTaken.ToString("N0"), "Deaths:", LogParser.Deaths.ToString("N0"));
            AddRow("Total Damage Converted:", LogParser.TotalDamageConverted.ToString("N0"), "", "");
            AddRow("Total Damage Absorbed:", LogParser.DamageTakenAbsorbed.ToString("N0"), "Realm Points:", LogParser.RealmPointsEarned.ToString("N0"));
            AddRow("Total Damage Blocked:", LogParser.TotalDamageBlocked.ToString("N0"), "IRS", LogParser.IRS.ToString("N0"));
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

        private void DisplayParseLogStatistics()
        {
            BindingSource.DataSource = ProduceDataTable();
            Overlay.Value_AverageCritDamageDone.Text = LogParser.AverageCritDamageDone.ToString("N0");
            Overlay.Value_AverageDamageDone.Text = LogParser.AverageDamageDone.ToString("N0");
            Overlay.Value_TotalDamageDone.Text = LogParser.TotalDamageDone.ToString("N0");
            Overlay.Value_CritDamageRatio.Text = LogParser.CritDamageRatio.ToString("0.0%");
            Overlay.Value_DamageCritRate.Text = LogParser.DamageCritRate.ToString("0.0%");
            Overlay.Value_AverageHealingDone.Text = LogParser.AverageHealingDone.ToString("N0");
            Overlay.Value_AverageCritHealingDone.Text = LogParser.AverageCritHealingDone.ToString("N0");
            Overlay.Value_HealSelfRatio.Text = LogParser.HealSelfRatio.ToString("0.0%");
            Overlay.Value_CritHealRatio.Text = LogParser.CritHealRatio.ToString("0.0%");
            Overlay.Value_HealCritRate.Text = LogParser.HealCritRate.ToString("0.0%");
            Overlay.Value_TotalHealingDone.Text = LogParser.TotalHealingDone.ToString("N0");
            Overlay.Value_TotalHealingRecieved.Text = LogParser.TotalHealingRecieved.ToString("N0");
            Overlay.Value_RealmPoints.Text = LogParser.RealmPointsEarned.ToString("N0");
            Overlay.Value_DamageAbsorbed.Text = LogParser.DamageDoneAbsorbed.ToString("N0");
            Overlay.Value_DamageTakenAbsorbed.Text = LogParser.DamageTakenAbsorbed.ToString("N0");
            Overlay.Value_TotalDamageTaken.Text = LogParser.TotalDamageTaken.ToString("N0");
            Overlay.Value_DeathBlows.Text = LogParser.DeathBlows.ToString("N0");
            Overlay.Value_Deaths.Text = LogParser.Deaths.ToString("N0");
            Overlay.Value_DamageDoneBlocked.Text = LogParser.DamageDoneBlocked.ToString("N0");
            Overlay.Value_TotalDamageBlocked.Text = LogParser.TotalDamageBlocked.ToString("N0");
            Overlay.Value_TotalDamageConverted.Text = LogParser.TotalDamageConverted.ToString("N0");
            Overlay.Value_IRS.Text = LogParser.IRS.ToString("N0");
            Overlay.Refresh();
        }


        private void MainForm_TimerHandler(object? sender, EventArgs e)
        {
            if (LogParser.HasUnparsedData())
            {
                LogParser.Parse();
            }

            DisplayParseLogStatistics();
        }

        private static string DefaultLogPath()
        {
            return $"{DefaultLogFolder()}\\chat.log";
        }

        private static string GetLastLogFolderPath()
        {
            string path = Properties.Settings.Default.LastLogPath;
            return string.IsNullOrEmpty(path) ? DefaultLogPath() : Properties.Settings.Default.LastLogPath;
        }

        private static string DefaultLogFolder()
        {
            string path = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Electronic Arts\\Dark Age of Camelot";
            return path;
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
            LogParser = new LogParser(LogPath);
            LogParser.PlayersOnlyFilter = FilterPlayersOnlyCheckBox.Checked;
            AttachLogDates();
            DisplayParseLogStatistics();
            LogDatesComboBox.Enabled = true;
            BrowseButton.Enabled = true;
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
            LogParser = new LogParser(LogPath);
            LogParser.PlayersOnlyFilter = FilterPlayersOnlyCheckBox.Checked;
            AttachLogDates();
            DisplayParseLogStatistics();
            LogDatesComboBox.Enabled = true;
            ResetButton.Enabled = true;
        }

        private void OverlayButton_Click(object sender, EventArgs e)
        {
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
        }

        private void LockOverlayButton_Click(object sender, EventArgs e)
        {
            if (!Overlay.Visible)
                return;

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
        }

        private void OverLayOpacityControl_ValueChanged(object sender, EventArgs e)
        {
            Overlay.Opacity = Convert.ToDouble(OverLayOpacityControl.Value / 100);
            Overlay.Refresh();
            Properties.Settings.Default.OverlayOpacity = OverLayOpacityControl.Value;
            Properties.Settings.Default.Save();
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            _ = colorDialog1.ShowDialog();
            Overlay.SetLabelForecolor(colorDialog1.Color);
            FontColorPanel.BackColor = colorDialog1.Color;
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
            Properties.Settings.Default.PlayersOnly = checkBox.Checked;
            Properties.Settings.Default.Save();

        }
    }
}