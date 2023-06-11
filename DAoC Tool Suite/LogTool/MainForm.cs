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
        public ParseLog ParseLog { get; set; }
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
            ParseLog = new ParseLog(DefaultLogPath());
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
                var logDates = ParseLog.LogOpenEntries.Keys.Select(x => x.ToString("G")).ToList();
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
                int fileIndex = ParseLog.LogOpenEntries[startDate];
                ParseLog = new ParseLog(LogPath, fileIndex);
            }
            DisplayParseLogStatistics();
        }

        private void DisplayParseLogStatistics()
        {
            Overlay.AverageCritDamageDoneTextBox.Text = ParseLog.AverageCritDamageDone.ToString("N0");
            Overlay.AverageDamageDoneTextBox.Text = ParseLog.AverageDamageDone.ToString("N0");
            Overlay.TotalDamageDoneTextBox.Text = ParseLog.TotalDamageDone.ToString("N0");
            Overlay.DamageCritRateTextBox.Text = ParseLog.DamageCritRate.ToString() + "%";
            Overlay.AverageHealingDoneTextBox.Text = ParseLog.AverageHealingDone.ToString("N0");
            Overlay.AverageCritHealingDoneTextBox.Text = ParseLog.AverageCritHealingDone.ToString("N0");
            Overlay.HealSelfRatioTextBox.Text = ParseLog.HealSelfRatio.ToString() + "%";
            Overlay.CritHealRatioTextBox.Text = ParseLog.CritHealRatio.ToString() + "%";
            Overlay.HealCritRateTextBox.Text = ParseLog.HealCritRate.ToString() + "%";
            Overlay.TotalHealingDoneTextBox.Text = ParseLog.TotalHealingDone.ToString("N0");
            Overlay.TotalHealingRecievedTextBox.Text = ParseLog.TotalHealingRecieved.ToString("N0");
            Overlay.RealmPointsTextBox.Text = ParseLog.RealmPoints.ToString("N0");
            Overlay.Refresh();
        }

        private void MainForm_TimerHandler(object? sender, EventArgs e)
        {
            if (ParseLog.HasUnparsedData())
                ParseLog.ParseFile();
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
            ParseLog = new ParseLog(LogPath);
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
            ParseLog = new ParseLog(LogPath);
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
            Overlay.DamageCritRateTextBox.ForeColor = colorDialog1.Color;
            Overlay.RealmPointsLabel.ForeColor = colorDialog1.Color;
            Overlay.RealmPointsTextBox.ForeColor = colorDialog1.Color;
            Overlay.TotalDamageDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.TotalDamageDoneTextBox.ForeColor = colorDialog1.Color;
            Overlay.TotalHealingRecievedLabel.ForeColor = colorDialog1.Color;
            Overlay.TotalHealingRecievedTextBox.ForeColor = colorDialog1.Color;
            Overlay.TotalHealingDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.TotalHealingDoneTextBox.ForeColor = colorDialog1.Color;
            Overlay.CritHealRatioLabel.ForeColor = colorDialog1.Color;
            Overlay.HealCritRateTextBox.ForeColor = colorDialog1.Color;
            Overlay.HealCritRateLabel.ForeColor = colorDialog1.Color;
            Overlay.CritHealRatioTextBox.ForeColor = colorDialog1.Color;
            Overlay.HealSelfRatioLabel.ForeColor = colorDialog1.Color;
            Overlay.HealSelfRatioTextBox.ForeColor = colorDialog1.Color;
            Overlay.AverageCritHealingDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageCritHealingDoneTextBox.ForeColor = colorDialog1.Color;
            Overlay.AverageHealingDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageCritDamageDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageDamageDoneLabel.ForeColor = colorDialog1.Color;
            Overlay.AverageHealingDoneTextBox.ForeColor = colorDialog1.Color;
            Overlay.AverageCritDamageDoneTextBox.ForeColor = colorDialog1.Color;
            Overlay.AverageDamageDoneTextBox.ForeColor = colorDialog1.Color;
            FontColorPanel.BackColor = colorDialog1.Color;
            Overlay.Refresh();
        }

        private void OverlayTransparentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                Overlay.DamageCritRateLabel.BackColor = Color.DimGray;
                Overlay.DamageCritRateTextBox.BackColor = Color.DimGray;
                Overlay.RealmPointsLabel.BackColor = Color.DimGray;
                Overlay.RealmPointsTextBox.BackColor = Color.DimGray;
                Overlay.TotalDamageDoneLabel.BackColor = Color.DimGray;
                Overlay.TotalDamageDoneTextBox.BackColor = Color.DimGray;
                Overlay.TotalHealingRecievedLabel.BackColor = Color.DimGray;
                Overlay.TotalHealingRecievedTextBox.BackColor = Color.DimGray;
                Overlay.TotalHealingDoneLabel.BackColor = Color.DimGray;
                Overlay.TotalHealingDoneTextBox.BackColor = Color.DimGray;
                Overlay.CritHealRatioLabel.BackColor = Color.DimGray;
                Overlay.HealCritRateTextBox.BackColor = Color.DimGray;
                Overlay.HealCritRateLabel.BackColor = Color.DimGray;
                Overlay.CritHealRatioTextBox.BackColor = Color.DimGray;
                Overlay.HealSelfRatioLabel.BackColor = Color.DimGray;
                Overlay.HealSelfRatioTextBox.BackColor = Color.DimGray;
                Overlay.AverageCritHealingDoneLabel.BackColor = Color.DimGray;
                Overlay.AverageCritHealingDoneTextBox.BackColor = Color.DimGray;
                Overlay.AverageHealingDoneLabel.BackColor = Color.DimGray;
                Overlay.AverageCritDamageDoneLabel.BackColor = Color.DimGray;
                Overlay.AverageDamageDoneLabel.BackColor = Color.DimGray;
                Overlay.AverageHealingDoneTextBox.BackColor = Color.DimGray;
                Overlay.AverageCritDamageDoneTextBox.BackColor = Color.DimGray;
                Overlay.AverageDamageDoneTextBox.BackColor = Color.DimGray;
                Overlay.Refresh();
            }
            else
            {
                Overlay.DamageCritRateLabel.BackColor = Color.Black;
                Overlay.DamageCritRateTextBox.BackColor = Color.Black;
                Overlay.RealmPointsLabel.BackColor = Color.Black;
                Overlay.RealmPointsTextBox.BackColor = Color.Black;
                Overlay.TotalDamageDoneLabel.BackColor = Color.Black;
                Overlay.TotalDamageDoneTextBox.BackColor = Color.Black;
                Overlay.TotalHealingRecievedLabel.BackColor = Color.Black;
                Overlay.TotalHealingRecievedTextBox.BackColor = Color.Black;
                Overlay.TotalHealingDoneLabel.BackColor = Color.Black;
                Overlay.TotalHealingDoneTextBox.BackColor = Color.Black;
                Overlay.CritHealRatioLabel.BackColor = Color.Black;
                Overlay.HealCritRateTextBox.BackColor = Color.Black;
                Overlay.HealCritRateLabel.BackColor = Color.Black;
                Overlay.CritHealRatioTextBox.BackColor = Color.Black;
                Overlay.HealSelfRatioLabel.BackColor = Color.Black;
                Overlay.HealSelfRatioTextBox.BackColor = Color.Black;
                Overlay.AverageCritHealingDoneLabel.BackColor = Color.Black;
                Overlay.AverageCritHealingDoneTextBox.BackColor = Color.Black;
                Overlay.AverageHealingDoneLabel.BackColor = Color.Black;
                Overlay.AverageCritDamageDoneLabel.BackColor = Color.Black;
                Overlay.AverageDamageDoneLabel.BackColor = Color.Black;
                Overlay.AverageHealingDoneTextBox.BackColor = Color.Black;
                Overlay.AverageCritDamageDoneTextBox.BackColor = Color.Black;
                Overlay.AverageDamageDoneTextBox.BackColor = Color.Black;
                Overlay.Refresh();
            }
        }
    }
}