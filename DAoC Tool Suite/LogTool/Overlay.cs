using System.Runtime.CompilerServices;
using System.Windows;
using Windows.Perception.Spatial;

namespace DAoCToolSuite.LogTool
{
    public partial class Overlay : Form
    {
        public Overlay()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Overlay_OnClosing);
            MouseDown += new MouseEventHandler(Overlay_MouseDown);
            MoveLabel.MouseDown += new MouseEventHandler(MoveLabel_MouseDown);
            FormatOverlay();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            long currentCount = LogTool.Properties.Settings.Default.UseCountOverlay;
            if (currentCount > 0)
            {
                Location = LogTool.Properties.Settings.Default.LastLocationOverlay;
            }
            else
            {
                //ScreenCentered by Default
                LogTool.Properties.Settings.Default.LastLocationOverlay = Location;
            }
            if (currentCount != long.MaxValue)
            {
                LogTool.Properties.Settings.Default.UseCountOverlay = currentCount + 1;
            }
            LogTool.Properties.Settings.Default.Save();
        }


        private void Overlay_OnClosing(object? sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastLocationOverlay = this.Location;
            Properties.Settings.Default.Save();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern bool ReleaseCapture();

        private void Overlay_MouseDown(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _ = ReleaseCapture();
                _ = SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void MoveLabel_MouseDown(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _ = ReleaseCapture();
                _ = SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void MoveLabel_Click(object sender, EventArgs e)
        {

        }

        private void Overlay_Load(object sender, EventArgs e)
        {

        }

        private static void FormatLabel(Label label, int column, int row)
        {
            int padding = 0;
            int rowStep = 15;
            int point_X;
            int point_Y;

            switch(column)
            {
                case 0:
                    column = 175;
                    break;
                default:
                    column *= 425;
                    break;
            }

            ContentAlignment textAlignment;

            if (label.Name.Contains("Title_"))
            {

                point_X = column - label.Width - padding;
                point_Y = row * rowStep;
                textAlignment = ContentAlignment.MiddleRight;
            }
            else
            {
                point_X = column + padding;
                point_Y = row * rowStep;
                textAlignment = ContentAlignment.MiddleLeft;
            }
            label.Location = new(point_X, point_Y);
            label.TextAlign = textAlignment;
        }

        private void FormatOverlay()
        {
            //Column 1 Titles
            FormatLabel(Title_AverageHealingDone, 0, 0);
            FormatLabel(Title_AverageCritHealingDone, 0, 1);
            FormatLabel(Title_TotalHealingDone, 0, 2);
            FormatLabel(Title_HealCritRate, 0, 3);
            FormatLabel(Title_CritHealRatio, 0, 4);
            FormatLabel(Title_HealSelfRatio, 0, 5);
            FormatLabel(Title_TotalHealingRecieved, 0, 7);
            FormatLabel(Title_TotalDamageTaken, 0, 9);
            FormatLabel(Title_TotalDamageConverted, 0, 10);
            FormatLabel(Title_DamageTakenAbsorbed, 0, 11);
            FormatLabel(Title_TotalDamageBlocked, 0, 12);

            //Column 2 Titles
            FormatLabel(Title_AverageDamageDone, 1, 0);
            FormatLabel(Title_AverageCritDamageDone, 1, 1);
            FormatLabel(Title_TotalDamageDone, 1, 2);
            FormatLabel(Title_DamageAbsorbed, 1, 3);
            FormatLabel(Title_DamageDoneBlocked, 1, 4);
            FormatLabel(Title_DamageCritRate, 1, 5);
            FormatLabel(Title_CritDamageRatio, 1, 6);
            FormatLabel(Title_DeathBlows, 1, 8);
            FormatLabel(Title_Deaths, 1, 9);
            FormatLabel(Title_RealmPoints, 1, 11);
            FormatLabel(Title_IRS, 1, 12);

            //Column 1 Values
            FormatLabel(Value_AverageHealingDone, 0, 0);
            FormatLabel(Value_AverageCritHealingDone, 0, 1);
            FormatLabel(Value_TotalHealingDone, 0, 2);
            FormatLabel(Value_HealCritRate, 0, 3);
            FormatLabel(Value_CritHealRatio, 0, 4);
            FormatLabel(Value_HealSelfRatio, 0, 5);
            FormatLabel(Value_TotalHealingRecieved, 0, 7);
            FormatLabel(Value_TotalDamageTaken, 0, 9);
            FormatLabel(Value_TotalDamageConverted, 0, 10);
            FormatLabel(Value_DamageTakenAbsorbed, 0, 11);
            FormatLabel(Value_TotalDamageBlocked, 0, 12);

            //Column 2 Values
            FormatLabel(Value_AverageDamageDone, 1, 0);
            FormatLabel(Value_AverageCritDamageDone, 1, 1);
            FormatLabel(Value_TotalDamageDone, 1, 2);
            FormatLabel(Value_DamageAbsorbed, 1, 3);
            FormatLabel(Value_DamageDoneBlocked, 1, 4);
            FormatLabel(Value_DamageCritRate, 1, 5);
            FormatLabel(Value_CritDamageRatio, 1, 6);
            FormatLabel(Value_DeathBlows, 1, 8);
            FormatLabel(Value_Deaths, 1, 9);
            FormatLabel(Value_RealmPoints, 1, 11);
            FormatLabel(Value_IRS, 1, 12);
        }

        public void SetLabelBackcolor(Color color)
        {
            Title_AverageCritDamageDone.BackColor = color;
            Title_AverageCritHealingDone.BackColor = color;
            Title_AverageDamageDone.BackColor = color;
            Title_AverageHealingDone.BackColor = color;
            Title_CritDamageRatio.BackColor = color;
            Title_CritHealRatio.BackColor = color;
            Title_DamageAbsorbed.BackColor = color;
            Title_DamageCritRate.BackColor = color;
            Title_DamageDoneBlocked.BackColor = color;
            Title_DamageTakenAbsorbed.BackColor = color;
            Title_DeathBlows.BackColor = color;
            Title_Deaths.BackColor = color;
            Title_HealCritRate.BackColor = color;
            Title_HealSelfRatio.BackColor = color;
            Title_IRS.BackColor = color;
            Title_RealmPoints.BackColor = color;
            Title_TotalDamageBlocked.BackColor = color;
            Title_TotalDamageConverted.BackColor = color;
            Title_TotalDamageDone.BackColor = color;
            Title_TotalDamageTaken.BackColor = color;
            Title_TotalHealingDone.BackColor = color;
            Title_TotalHealingRecieved.BackColor = color;
            Value_AverageCritDamageDone.BackColor = color;
            Value_AverageCritHealingDone.BackColor = color;
            Value_AverageDamageDone.BackColor = color;
            Value_AverageHealingDone.BackColor = color;
            Value_CritDamageRatio.BackColor = color;
            Value_CritHealRatio.BackColor = color;
            Value_DamageAbsorbed.BackColor = color;
            Value_DamageCritRate.BackColor = color;
            Value_DamageDoneBlocked.BackColor = color;
            Value_DamageTakenAbsorbed.BackColor = color;
            Value_DeathBlows.BackColor = color;
            Value_Deaths.BackColor = color;
            Value_HealCritRate.BackColor = color;
            Value_HealSelfRatio.BackColor = color;
            Value_IRS.BackColor = color;
            Value_RealmPoints.BackColor = color;
            Value_TotalDamageBlocked.BackColor = color;
            Value_TotalDamageConverted.BackColor = color;
            Value_TotalDamageDone.BackColor = color;
            Value_TotalDamageTaken.BackColor = color;
            Value_TotalHealingDone.BackColor = color;
            Value_TotalHealingRecieved.BackColor = color;
            Refresh();
        }

        public void SetLabelForecolor(Color color)
        {
            Title_AverageCritDamageDone.ForeColor = color;
            Title_AverageCritHealingDone.ForeColor = color;
            Title_AverageDamageDone.ForeColor = color;
            Title_AverageHealingDone.ForeColor = color;
            Title_CritDamageRatio.ForeColor = color;
            Title_CritHealRatio.ForeColor = color;
            Title_DamageAbsorbed.ForeColor = color;
            Title_DamageCritRate.ForeColor = color;
            Title_DamageDoneBlocked.ForeColor = color;
            Title_DamageTakenAbsorbed.ForeColor = color;
            Title_DeathBlows.ForeColor = color;
            Title_Deaths.ForeColor = color;
            Title_HealCritRate.ForeColor = color;
            Title_HealSelfRatio.ForeColor = color;
            Title_IRS.ForeColor = color;
            Title_RealmPoints.ForeColor = color;
            Title_TotalDamageBlocked.ForeColor = color;
            Title_TotalDamageConverted.ForeColor = color;
            Title_TotalDamageDone.ForeColor = color;
            Title_TotalDamageTaken.ForeColor = color;
            Title_TotalHealingDone.ForeColor = color;
            Title_TotalHealingRecieved.ForeColor = color;
            Value_AverageCritDamageDone.ForeColor = color;
            Value_AverageCritHealingDone.ForeColor = color;
            Value_AverageDamageDone.ForeColor = color;
            Value_AverageHealingDone.ForeColor = color;
            Value_CritDamageRatio.ForeColor = color;
            Value_CritHealRatio.ForeColor = color;
            Value_DamageAbsorbed.ForeColor = color;
            Value_DamageCritRate.ForeColor = color;
            Value_DamageDoneBlocked.ForeColor = color;
            Value_DamageTakenAbsorbed.ForeColor = color;
            Value_DeathBlows.ForeColor = color;
            Value_Deaths.ForeColor = color;
            Value_HealCritRate.ForeColor = color;
            Value_HealSelfRatio.ForeColor = color;
            Value_IRS.ForeColor = color;
            Value_RealmPoints.ForeColor = color;
            Value_TotalDamageBlocked.ForeColor = color;
            Value_TotalDamageConverted.ForeColor = color;
            Value_TotalDamageDone.ForeColor = color;
            Value_TotalDamageTaken.ForeColor = color;
            Value_TotalHealingDone.ForeColor = color;
            Value_TotalHealingRecieved.ForeColor = color;
            Refresh();
        }
    }
}
