using Logger;

namespace DAoCToolSuite
{
    public partial class DAoCToolSuiteForm : Form
    {
        internal static LogManager Logger => LogManager.Instance;

        public DAoCToolSuiteForm()
        {
            InitializeComponent();
        }

        private void ChimpToolButton_Click(object sender, EventArgs e)
        {
            _ = System.Diagnostics.Process.Start($"{System.IO.Path.GetDirectoryName(Application.ExecutablePath)}\\ChimpTool.exe");
        }


        private void CharacterToolButton_Click(object sender, EventArgs e)
        {
            _ = System.Diagnostics.Process.Start($"{System.IO.Path.GetDirectoryName(Application.ExecutablePath)}\\CharacterTool.exe");
        }

        private void CharacterToolForm_Disposed(object? sender, EventArgs e)
        {
            Activate();
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
            _ = System.Diagnostics.Process.Start($"{System.IO.Path.GetDirectoryName(Application.ExecutablePath)}\\LogTool.exe");
        }
        private void LogToolForm_Disposed(object? sender, EventArgs e)
        {
            Activate();
        }
    }
}
