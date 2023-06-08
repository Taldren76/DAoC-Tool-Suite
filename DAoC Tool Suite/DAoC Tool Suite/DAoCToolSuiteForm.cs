using Logger;

namespace DAoCToolSuite
{
    public partial class DAoCToolSuiteForm : Form
    {
        internal static LogManager Logger => LogManager.Instance;
        private static ChimpTool.MainForm? ChimpToolForm { get; set; } = null;
        private static CharacterTool.MainForm? CharacterToolForm { get; set; } = null;

        public DAoCToolSuiteForm()
        {
            InitializeComponent();
        }

        private void ChimpToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChimpToolForm is null || ChimpToolForm.IsDisposed)
                {
                    ChimpToolForm = new();
                }
                ChimpToolForm.FormClosing -= ChimpToolForm_FormClosing;
                ChimpToolForm.FormClosing += ChimpToolForm_FormClosing;
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

        private void ActivateThis()
        {
            if (!IsDisposed)
            {
                Activate();
            }
        }

        private void ChimpToolForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                ActivateThis(); //Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void characterToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CharacterToolForm is null || CharacterToolForm.IsDisposed)
                {
                    CharacterToolForm = new();
                }
                CharacterToolForm.FormClosing -= CharacterToolForm_FormClosing;
                CharacterToolForm.FormClosing += CharacterToolForm_FormClosing;
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

        private void CharacterToolForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _ = Focus();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

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

        private void button1_Click(object sender, EventArgs e)
        {
            ChimpTool.Properties.Settings.Default.Reset();
            ChimpTool.Properties.Settings.Default.Save();
            CharacterTool.Properties.Settings.Default.Reset();
            CharacterTool.Properties.Settings.Default.Save();
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
        }
    }
}
