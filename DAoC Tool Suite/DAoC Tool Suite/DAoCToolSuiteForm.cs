namespace DAoCToolSuite
{
    public partial class DAoCToolSuiteForm : Form
    {
        private static DAoCToolSuite.ChimpTool.MainForm ChimpToolForm { get; set; } = new();
        private static DAoCToolSuite.CharacterTool.MainForm CharacterToolForm { get; set; } = new();

        public DAoCToolSuiteForm()
        {
            InitializeComponent();
        }

        private void chimpToolButton_Click(object sender, EventArgs e)
        {
            if (ChimpToolForm is null || ChimpToolForm.IsDisposed)
            {
                ChimpToolForm = new();
            }

            ChimpToolForm.FormClosed += ChimpToolForm_FormClosed;
            ChimpToolForm.Show();
            _ = ChimpToolForm.Focus();

        }

        private void ChimpToolForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            _ = Focus();
        }

        private void characterToolButton_Click(object sender, EventArgs e)
        {
            if (CharacterToolForm is null || CharacterToolForm.IsDisposed)
            {
                CharacterToolForm = new();
            }

            CharacterToolForm.FormClosed += CharacterToolForm_FormClosed;
            CharacterToolForm.Show();
            _ = CharacterToolForm.Focus();
        }

        private void CharacterToolForm_FormClosed(object? sender, FormClosedEventArgs e)
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

        private void DAoCTestSuiteForm_FormClosing(object sender, FormClosingEventArgs e)
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
