using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;
using Logger;
using Microsoft.VisualBasic;
using Windows.Security.EnterpriseData;

namespace DAoCToolSuite.LogTool
{
    public partial class MainForm : Form
    {
        public static System.Windows.Forms.Timer Timer { get; set; } = new();
        public LogParser LogParser { get; set; }
        LogManager Logger => LogManager.Instance;
        Overlay Overlay { get; set; } = new();

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
            Timer.Interval = 5000;
            LogParser = new LogParser(DefaultLogPath());
            AttachLogDates();
            OverLayOpacityControl.Value = Convert.ToDecimal(Overlay.Opacity * 100);
            OverLayOpacityControl.Maximum = 100;
            OverLayOpacityControl.Minimum = 0;
            FontColorPanel.BackColor = Overlay.TotalDamageDoneLabel.ForeColor;
            OverlayTransparentCheckBox.Checked = true;

            FormInitialized = true;
        }

        private void AttachLogDates()
        {
            try
            {
                var logDates = LogParser.LogOpenEntries.Keys.Select(x => x.ToString("G")).ToList();
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
                var startDate = Convert.ToDateTime(LogDatesComboBox.Text);
                if (!LogParser.LogOpenEntries.ContainsKey(startDate))
                {
                    LogParser = new LogParser(LogPath, 0);
                }
                else
                {
                    int fileIndex = LogParser.LogOpenEntries[startDate];
                    LogParser = new LogParser(LogPath, fileIndex);
                }
            }
            DisplayParseLogStatistics();
        }

        private void DisplayParseLogStatistics()
        {
            Overlay.AverageCritDamageDoneValueLabel.Text = LogParser.AverageCritDamageDone.ToString("N0");
            Overlay.AverageDamageDoneValueLabel.Text = LogParser.AverageDamageDone.ToString("N0");
            Overlay.TotalDamageDoneValueLabel.Text = LogParser.TotalDamageDone.ToString("N0");
            Overlay.DamageCritRateValueLabel.Text = LogParser.DamageCritRate.ToString() + "%";
            Overlay.AverageHealingDoneValueLabel.Text = LogParser.AverageHealingDone.ToString("N0");
            Overlay.AverageCritHealingDoneValueLabel.Text = LogParser.AverageCritHealingDone.ToString("N0");
            Overlay.HealSelfRatioValueLabel.Text = LogParser.HealSelfRatio.ToString() + "%";
            Overlay.CritHealRatioValueLabel.Text = LogParser.CritHealRatio.ToString() + "%";
            Overlay.HealCritRateValueLabel.Text = LogParser.HealCritRate.ToString() + "%";
            Overlay.TotalHealingDoneValueLabel.Text = LogParser.TotalHealingDone.ToString("N0");
            Overlay.TotalHealingRecievedValueLabel.Text = LogParser.TotalHealingRecieved.ToString("N0");
            Overlay.RealmPointsValueLabel.Text = LogParser.RealmPoints.ToString("N0");
            Overlay.Refresh();
        }

        private void MainForm_TimerHandler(object? sender, EventArgs e)
        {
            if (LogParser.HasUnparsedData())
                LogParser.Parse();
            DisplayParseLogStatistics();
        }

        private string DefaultLogPath()
        {
            return $"{GetLastLogFolder()}\\chat.log";
        }

        private static string DefaultLogFolder()
        {
            var path = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Electronic Arts\\Dark Age of Camelot";
            return path;
        }

        private string GetLastLogFolder()
        {
            var path = Properties.Settings.Default.LastLogFolder;
            if (string.IsNullOrEmpty(path))
                return DefaultLogFolder();
            return Properties.Settings.Default.LastLogFolder;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            BrowseButton.Enabled = false;
            try
            {
                OpenFileDialog ofd = new()
                {
                    Filter = "Log Files|*.log",
                    Multiselect = false,
                    InitialDirectory = GetLastLogFolder()
                };
                ofd.ShowDialog(this);
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

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Overlay.DamageCritRateLabel.ForeColor = colorDialog1.Color;
            Overlay.DamageCritRateValueLabel.ForeColor = colorDialog1.Color;
            Overlay.RealmPointsLabel.ForeColor = colorDialog1.Color;
            Overlay.RealmPointsValueLabel.ForeColor = colorDialog1.Color;
            Overlay.TotalDamageDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.TotalDamageDoneValueLabel.ForeColor = colorDialog1.Color;
            Overlay.TotalHealingRecievedLabel.ForeColor = colorDialog1.Color;
            Overlay.TotalHealingRecievedValueLabel.ForeColor = colorDialog1.Color;
            Overlay.TotalHealingDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.TotalHealingDoneValueLabel.ForeColor = colorDialog1.Color;
            Overlay.CritHealRatioLabel.ForeColor = colorDialog1.Color;
            Overlay.HealCritRateValueLabel.ForeColor = colorDialog1.Color;
            Overlay.HealCritRateLabel.ForeColor = colorDialog1.Color;
            Overlay.CritHealRatioValueLabel.ForeColor = colorDialog1.Color;
            Overlay.HealSelfRatioLabel.ForeColor = colorDialog1.Color;
            Overlay.HealSelfRatioValueLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageCritHealingDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageCritHealingDoneValueLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageHealingDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageCritDamageDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageDamageDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageHealingDoneValueLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageCritDamageDoneValueLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageDamageDoneValueLabel.ForeColor = colorDialog1.Color;
            FontColorPanel.BackColor = colorDialog1.Color;
            Overlay.Refresh();
        }

        private void OverlayTransparentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                Overlay.DamageCritRateLabel.BackColor = Color.DimGray;
                Overlay.DamageCritRateValueLabel.BackColor = Color.DimGray;
                Overlay.RealmPointsLabel.BackColor = Color.DimGray;
                Overlay.RealmPointsValueLabel.BackColor = Color.DimGray;
                Overlay.TotalDamageDoneLabel.BackColor = Color.DimGray;
                Overlay.TotalDamageDoneValueLabel.BackColor = Color.DimGray;
                Overlay.TotalHealingRecievedLabel.BackColor = Color.DimGray;
                Overlay.TotalHealingRecievedValueLabel.BackColor = Color.DimGray;
                Overlay.TotalHealingDoneLabel.BackColor = Color.DimGray;
                Overlay.TotalHealingDoneValueLabel.BackColor = Color.DimGray;
                Overlay.CritHealRatioLabel.BackColor = Color.DimGray;
                Overlay.HealCritRateValueLabel.BackColor = Color.DimGray;
                Overlay.HealCritRateLabel.BackColor = Color.DimGray;
                Overlay.CritHealRatioValueLabel.BackColor = Color.DimGray;
                Overlay.HealSelfRatioLabel.BackColor = Color.DimGray;
                Overlay.HealSelfRatioValueLabel.BackColor = Color.DimGray;
                Overlay.AverageCritHealingDoneLabel.BackColor = Color.DimGray;
                Overlay.AverageCritHealingDoneValueLabel.BackColor = Color.DimGray;
                Overlay.AverageHealingDoneLabel.BackColor = Color.DimGray;
                Overlay.AverageCritDamageDoneLabel.BackColor = Color.DimGray;
                Overlay.AverageDamageDoneLabel.BackColor = Color.DimGray;
                Overlay.AverageHealingDoneValueLabel.BackColor = Color.DimGray;
                Overlay.AverageCritDamageDoneValueLabel.BackColor = Color.DimGray;
                Overlay.AverageDamageDoneValueLabel.BackColor = Color.DimGray;
                Overlay.Refresh();
            }
            else
            {
                Overlay.DamageCritRateLabel.BackColor = Color.Black;
                Overlay.DamageCritRateValueLabel.BackColor = Color.Black;
                Overlay.RealmPointsLabel.BackColor = Color.Black;
                Overlay.RealmPointsValueLabel.BackColor = Color.Black;
                Overlay.TotalDamageDoneLabel.BackColor = Color.Black;
                Overlay.TotalDamageDoneValueLabel.BackColor = Color.Black;
                Overlay.TotalHealingRecievedLabel.BackColor = Color.Black;
                Overlay.TotalHealingRecievedValueLabel.BackColor = Color.Black;
                Overlay.TotalHealingDoneLabel.BackColor = Color.Black;
                Overlay.TotalHealingDoneValueLabel.BackColor = Color.Black;
                Overlay.CritHealRatioLabel.BackColor = Color.Black;
                Overlay.HealCritRateValueLabel.BackColor = Color.Black;
                Overlay.HealCritRateLabel.BackColor = Color.Black;
                Overlay.CritHealRatioValueLabel.BackColor = Color.Black;
                Overlay.HealSelfRatioLabel.BackColor = Color.Black;
                Overlay.HealSelfRatioValueLabel.BackColor = Color.Black;
                Overlay.AverageCritHealingDoneLabel.BackColor = Color.Black;
                Overlay.AverageCritHealingDoneValueLabel.BackColor = Color.Black;
                Overlay.AverageHealingDoneLabel.BackColor = Color.Black;
                Overlay.AverageCritDamageDoneLabel.BackColor = Color.Black;
                Overlay.AverageDamageDoneLabel.BackColor = Color.Black;
                Overlay.AverageHealingDoneValueLabel.BackColor = Color.Black;
                Overlay.AverageCritDamageDoneValueLabel.BackColor = Color.Black;
                Overlay.AverageDamageDoneValueLabel.BackColor = Color.Black;
                Overlay.Refresh();
            }
        }

        private void FilterPlayersOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}