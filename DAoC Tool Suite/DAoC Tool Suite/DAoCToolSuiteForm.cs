using Logger;

namespace DAoCToolSuite
{
    public partial class DAoCToolSuiteForm : Form
    {
        internal static LogManager Logger => LogManager.Instance;
        private static ChimpTool.MainForm? ChimpToolForm { get; set; } = null;
        private static CharacterTool.MainForm? CharacterToolForm { get; set; } = null;
        private static LogTool.MainForm? LogToolForm { get; set; } = null;

        public DAoCToolSuiteForm()
        {
            InitializeComponent();
        }

        private void ChimpToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                ChimpToolForm ??= new();
                ChimpToolForm.Disposed -= ChimpToolForm_Disposed;
                ChimpToolForm.Disposed += ChimpToolForm_Disposed;
                ChimpToolForm.Show();
                Logger.Debug("ChimpToolForm MainForm Shown");
                _ = ChimpToolForm.Focus();
                Logger.Debug("ChimpToolForm MainForm Focused");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void ChimpToolForm_Disposed(object? sender, EventArgs e)
        {
            Activate();
            ChimpToolForm = null;
        }

        private void CharacterToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                CharacterToolForm ??= new();
                CharacterToolForm.Disposed -= CharacterToolForm_Disposed;
                CharacterToolForm.Disposed += CharacterToolForm_Disposed;
                CharacterToolForm.Show();
                Logger.Debug("CharacterTool MainForm Shown");
                _ = CharacterToolForm.Focus();
                Logger.Debug("CharacterTool MainForm Focused");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void CharacterToolForm_Disposed(object? sender, EventArgs e)
        {
            Activate();
            CharacterToolForm = null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            long currentCount = DAoCToolSuite.Properties.Settings.Default.LoadCount;
            if (currentCount > 0)
            {
                Location = DAoCToolSuite.Properties.Settings.Default.WindowLocation;
            }
            else
            {
                //ScreenCentered by Default
                DAoCToolSuite.Properties.Settings.Default.WindowLocation = Location;
            }
            if (currentCount != long.MaxValue)
            {
                DAoCToolSuite.Properties.Settings.Default.LoadCount = currentCount + 1;
            }
            DAoCToolSuite.Properties.Settings.Default.Save();
        }

        private void DAoCTestSuiteForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (sender is not DAoCToolSuiteForm form)
            {
                return;
            }
            Properties.Settings.Default.WindowLocation = form.Location;
            Properties.Settings.Default.Save();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ChimpTool.Properties.Settings.Default.Reset();
            ChimpTool.Properties.Settings.Default.Save();
            CharacterTool.Properties.Settings.Default.Reset();
            CharacterTool.Properties.Settings.Default.Save();
            DAoCToolSuite.LogTool.Properties.Settings.Default.Reset();
            DAoCToolSuite.LogTool.Properties.Settings.Default.Save();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
        }

        private void LogParserButton_Click(object sender, EventArgs e)
        {
            try
            {
                LogToolForm ??= new();
                LogToolForm.Disposed -= LogToolForm_Disposed;
                LogToolForm.Disposed += LogToolForm_Disposed;
                LogToolForm.Show();
                Logger.Debug("LogTool MainForm Shown");
                _ = LogToolForm.Focus();
                Logger.Debug("LogToolForm MainForm Focused");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        private void LogToolForm_Disposed(object? sender, EventArgs e)
        {
            Activate();
            LogToolForm = null;
        }
    }
}
