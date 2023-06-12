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
        private Dictionary<DateTime, int> CustomLogDates { get; set; } = new();

        public MainForm()
        {
            InitializeComponent();
            LogPath = DefaultLogPath();
            LogFileTextBox.Text = LogPath;
            Timer.Tick -= new EventHandler(MainForm_TimerHandler);
            Timer.Tick += new EventHandler(MainForm_TimerHandler);
            Timer.Interval = 1000;
            LogParser = new LogParser(GetLastLogFolderPath());
            AttachLogDates();
            OverLayOpacityControl.Value = Convert.ToDecimal(Overlay.Opacity * 100);
            OverLayOpacityControl.Maximum = 100;
            OverLayOpacityControl.Minimum = 0;
            FontColorPanel.BackColor = Overlay.Title_TotalDamageDone.ForeColor;
            OverlayTransparentCheckBox.Checked = true;

            FormInitialized = true;
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
                    //LogParser = new LogParser(LogPath, 0);
                }
                else
                {
                    int fileIndex = LogParser.LogOpenEntries[startDate];
                    LogParser.SetFileIndex(fileIndex);
                    //LogParser = new LogParser(LogPath, fileIndex);
                }
            }
            DisplayParseLogStatistics();
        }

        private void DisplayParseLogStatistics()
        {
            Overlay.Value_AverageCritDamageDone.Text = LogParser.AverageCritDamageDone.ToString("N0");
            Overlay.Value_AverageDamageDone.Text = LogParser.AverageDamageDone.ToString("N0");
            Overlay.Value_TotalDamageDone.Text = LogParser.TotalDamageDone.ToString("N0");
            Overlay.Value_CritDamageRatio.Text = LogParser.CritDamageRatio.ToString("0.0%");//.ToRatioString();
            Overlay.Value_DamageCritRate.Text = LogParser.DamageCritRate.ToString("0.0%");
            Overlay.Value_AverageHealingDone.Text = LogParser.AverageHealingDone.ToString("N0");
            Overlay.Value_AverageCritHealingDone.Text = LogParser.AverageCritHealingDone.ToString("N0");
            Overlay.Value_HealSelfRatio.Text = LogParser.HealSelfRatio.ToString("0.0%");//.ToRatioString();
            Overlay.Value_CritHealRatio.Text = LogParser.CritHealRatio.ToString("0.0%");//.ToRatioString();
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
                ParseButton.Text = "Start";
                Parsing = false;
                ResetButton.Enabled = true;
                LogFileTextBox.Enabled = true;
                BrowseButton.Enabled = true;
            }
            else
            {
                Timer.Start();
                ParseButton.Text = "Stop";
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
            if (Overlay.MoveLabel.Visible)
            {
                LockOverlayButton.Text = "Unlock Overlay";
                Overlay.MoveLabel.Hide();
            }
            else
            {
                LockOverlayButton.Text = "Lock Overlay";
                Overlay.MoveLabel.Show();
            }
        }

        private void OverLayOpacityControl_ValueChanged(object sender, EventArgs e)
        {
            Overlay.Opacity = Convert.ToDouble(OverLayOpacityControl.Value / 100);
            Overlay.Refresh();
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            _ = colorDialog1.ShowDialog();
            Overlay.Title_DamageCritRate.ForeColor = colorDialog1.Color;
            Overlay.Value_DamageCritRate.ForeColor = colorDialog1.Color;
            Overlay.Title_RealmPoints.ForeColor = colorDialog1.Color;
            Overlay.Value_RealmPoints.ForeColor = colorDialog1.Color;
            Overlay.Title_TotalDamageDone.ForeColor = colorDialog1.Color;
            Overlay.Value_TotalDamageDone.ForeColor = colorDialog1.Color;
            Overlay.Title_TotalHealingRecieved.ForeColor = colorDialog1.Color;
            Overlay.Value_TotalHealingRecieved.ForeColor = colorDialog1.Color;
            Overlay.Title_TotalHealingDone.ForeColor = colorDialog1.Color;
            Overlay.Value_TotalHealingDone.ForeColor = colorDialog1.Color;
            Overlay.Title_CritHealRatio.ForeColor = colorDialog1.Color;
            Overlay.Value_HealCritRate.ForeColor = colorDialog1.Color;
            Overlay.Title_HealCritRate.ForeColor = colorDialog1.Color;
            Overlay.Value_CritHealRatio.ForeColor = colorDialog1.Color;
            Overlay.Title_HealSelfRatio.ForeColor = colorDialog1.Color;
            Overlay.Value_HealSelfRatio.ForeColor = colorDialog1.Color;
            Overlay.Title_AverageCritHealingDone.ForeColor = colorDialog1.Color;
            Overlay.Value_AverageCritHealingDone.ForeColor = colorDialog1.Color;
            Overlay.Title_AverageHealingDone.ForeColor = colorDialog1.Color;
            Overlay.Title_AverageCritDamageDone.ForeColor = colorDialog1.Color;
            Overlay.Title_AverageDamageDone.ForeColor = colorDialog1.Color;
            Overlay.Value_AverageHealingDone.ForeColor = colorDialog1.Color;
            Overlay.Value_AverageCritDamageDone.ForeColor = colorDialog1.Color;
            Overlay.Value_AverageDamageDone.ForeColor = colorDialog1.Color;
            FontColorPanel.BackColor = colorDialog1.Color;
            Overlay.Refresh();
        }

        private void OverlayTransparentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                Overlay.Title_DamageCritRate.BackColor = Color.DimGray;
                Overlay.Value_DamageCritRate.BackColor = Color.DimGray;
                Overlay.Title_RealmPoints.BackColor = Color.DimGray;
                Overlay.Value_RealmPoints.BackColor = Color.DimGray;
                Overlay.Title_TotalDamageDone.BackColor = Color.DimGray;
                Overlay.Value_TotalDamageDone.BackColor = Color.DimGray;
                Overlay.Title_TotalHealingRecieved.BackColor = Color.DimGray;
                Overlay.Value_TotalHealingRecieved.BackColor = Color.DimGray;
                Overlay.Title_TotalHealingDone.BackColor = Color.DimGray;
                Overlay.Value_TotalHealingDone.BackColor = Color.DimGray;
                Overlay.Title_CritHealRatio.BackColor = Color.DimGray;
                Overlay.Value_HealCritRate.BackColor = Color.DimGray;
                Overlay.Title_HealCritRate.BackColor = Color.DimGray;
                Overlay.Value_CritHealRatio.BackColor = Color.DimGray;
                Overlay.Title_HealSelfRatio.BackColor = Color.DimGray;
                Overlay.Value_HealSelfRatio.BackColor = Color.DimGray;
                Overlay.Title_AverageCritHealingDone.BackColor = Color.DimGray;
                Overlay.Value_AverageCritHealingDone.BackColor = Color.DimGray;
                Overlay.Title_AverageHealingDone.BackColor = Color.DimGray;
                Overlay.Title_AverageCritDamageDone.BackColor = Color.DimGray;
                Overlay.Title_AverageDamageDone.BackColor = Color.DimGray;
                Overlay.Value_AverageHealingDone.BackColor = Color.DimGray;
                Overlay.Value_AverageCritDamageDone.BackColor = Color.DimGray;
                Overlay.Value_AverageDamageDone.BackColor = Color.DimGray;
                Overlay.Refresh();
            }
            else
            {
                Overlay.Title_DamageCritRate.BackColor = Color.Black;
                Overlay.Value_DamageCritRate.BackColor = Color.Black;
                Overlay.Title_RealmPoints.BackColor = Color.Black;
                Overlay.Value_RealmPoints.BackColor = Color.Black;
                Overlay.Title_TotalDamageDone.BackColor = Color.Black;
                Overlay.Value_TotalDamageDone.BackColor = Color.Black;
                Overlay.Title_TotalHealingRecieved.BackColor = Color.Black;
                Overlay.Value_TotalHealingRecieved.BackColor = Color.Black;
                Overlay.Title_TotalHealingDone.BackColor = Color.Black;
                Overlay.Value_TotalHealingDone.BackColor = Color.Black;
                Overlay.Title_CritHealRatio.BackColor = Color.Black;
                Overlay.Value_HealCritRate.BackColor = Color.Black;
                Overlay.Title_HealCritRate.BackColor = Color.Black;
                Overlay.Value_CritHealRatio.BackColor = Color.Black;
                Overlay.Title_HealSelfRatio.BackColor = Color.Black;
                Overlay.Value_HealSelfRatio.BackColor = Color.Black;
                Overlay.Title_AverageCritHealingDone.BackColor = Color.Black;
                Overlay.Value_AverageCritHealingDone.BackColor = Color.Black;
                Overlay.Title_AverageHealingDone.BackColor = Color.Black;
                Overlay.Title_AverageCritDamageDone.BackColor = Color.Black;
                Overlay.Title_AverageDamageDone.BackColor = Color.Black;
                Overlay.Value_AverageHealingDone.BackColor = Color.Black;
                Overlay.Value_AverageCritDamageDone.BackColor = Color.Black;
                Overlay.Value_AverageDamageDone.BackColor = Color.Black;
                Overlay.Refresh();
            }
        }

        private void FilterPlayersOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LogParser.PlayersOnlyFilter = true;
        }
    }
}