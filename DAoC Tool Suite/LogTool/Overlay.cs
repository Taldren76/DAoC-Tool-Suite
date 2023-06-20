using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using Windows.Perception.Spatial;

namespace DAoCToolSuite.LogTool
{
    public partial class Overlay : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern bool ReleaseCapture();

        private Bitmap? BTMP { get; set; }
        private Color myForeColor { get; set; } = Color.White;
        private Color myBackColor { get; set; } = Color.Black;
        public bool Transparent { get; set; } = true;
        public bool ThreeDFont { get; set; } = true;
        public bool SpellDamageSection { get; set; } = true;
        public bool MeleeDamageSection { get; set; } = true;
        public bool MitigationSection { get; set; } = true;
        public bool HealingSection { get; set; } = true;
        public bool DefenseSection { get; set; } = true;
        public bool PetSection { get; set; } = true;

        private List<Label> Labels = new List<Label>();
        private int RowIndex = 0;
        private int ColumnIndex = 0;
        private int SectionIndex = 0;

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

        private void Overlay_OnClosing(object? sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastLocationOverlay = this.Location;
            Properties.Settings.Default.Save();
        }

        private void FormatLabel(Label label)
        {
            label.AutoSize = true;
            label.Visible = false;
            int padding = 0;
            int rowStep = 15;
            int point_X;
            int point_Y = SectionIndex * (rowStep / 2);
            int column = ColumnIndex;

            switch (column)
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
                point_Y += RowIndex * rowStep;
                textAlignment = ContentAlignment.MiddleRight;
            }
            else
            {
                point_X = column + padding;
                point_Y += RowIndex * rowStep;
                textAlignment = ContentAlignment.MiddleLeft;
            }
            label.Location = new(point_X, point_Y);
            label.TextAlign = textAlignment;
            label.ForeColor = myForeColor;
            label.BackColor = myBackColor;
            Labels.Add(label);
            RowIndex++;
        }

        private void DrawLabels()
        {
            BTMP = new Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics grpx = Graphics.FromImage(BTMP);
            foreach (Label label in Labels)
            {
                Color myColor = label.ForeColor;
                SolidBrush solidBrush = new SolidBrush(myColor);
                Brush brush = new SolidBrush(Color.Black);
                grpx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                if (!Transparent)
                    grpx.FillRectangle(brush, label.Location.X, label.Location.Y, label.Width, label.Height);
                if (ThreeDFont)
                {
                    System.Drawing.Point shadowLocation = new System.Drawing.Point(label.Location.X - 1, label.Location.Y + 1);
                    grpx.DrawString(label.Text, label.Font, new SolidBrush(Color.Black), shadowLocation);
                }
                grpx.DrawString(label.Text, label.Font, solidBrush, label.Location);
            }
        }

        private void FormatOverlay()
        {
            Labels.Clear();

            #region Titles
            ColumnIndex = 0;
            RowIndex = 0;
            SectionIndex = 0;
            Title_TotalDamageDone.Text = "Total Damage Done:";
            FormatLabel(Title_TotalDamageDone);

            if (SpellDamageSection)
            {
                SectionIndex += 1;
                Title_TotalSpellDamage.Text = "Total Spell Dmg:";
                FormatLabel(Title_TotalSpellDamage);
                Title_AverageSpellDamageDone.Text = "Avg Spell Dmg:";
                FormatLabel(Title_AverageSpellDamageDone);
                Title_AverageSpellCritDamageDone.Text = "Avg Spell Crit Dmg:";
                FormatLabel(Title_AverageSpellCritDamageDone);
                Title_SpellCritRate.Text = "Spell Crit Rate:";
                FormatLabel(Title_SpellCritRate);
            }

            if (MeleeDamageSection)
            {
                SectionIndex += 1;
                Title_TotalMeleeDamage.Text = "Total Melee Dmg:";
                FormatLabel(Title_TotalMeleeDamage);
                Title_AverageMeleeDamageDone.Text = "Avg Melee Dmg:";
                FormatLabel(Title_AverageMeleeDamageDone);
                Title_AverageMeleeCritDamageDone.Text = "Avg Melee Crit Dmg:";
                FormatLabel(Title_AverageMeleeCritDamageDone);
                Title_MeleeCritRate.Text = "Melee Crit Rate:";
                FormatLabel(Title_MeleeCritRate);
            }

            SectionIndex += 1;
            Title_TotalDamageTaken.Text = "Total Damage Taken:";
            FormatLabel(Title_TotalDamageTaken);
            if (MitigationSection)
            {
                Title_DamageTakenAbsorbed.Text = "Dmg Absorbed:";
                FormatLabel(Title_DamageTakenAbsorbed);
                Title_DamageTakenConverted.Text = "Dmg Converted:";
                FormatLabel(Title_DamageTakenConverted);
                Title_DamageTakenBlocked.Text = "Dmg Blocked:";
                FormatLabel(Title_DamageTakenBlocked);
            }

            SectionIndex += 1;
            Title_DeathBlows.Text = "DeathBlows:";
            FormatLabel(Title_DeathBlows);
            Title_Deaths.Text = "Deaths:";
            FormatLabel(Title_Deaths);

            ColumnIndex = 1;
            RowIndex = 0;
            SectionIndex = 0;
            Title_TotalHealingRecieved.Text = "Total Healing Recieved:";
            FormatLabel(Title_TotalHealingRecieved);

            if (HealingSection)
            {
                SectionIndex += 1;
                Title_TotalHealingDone.Text = "Total Healing Done:";
                FormatLabel(Title_TotalHealingDone);
                Title_AverageHealingDone.Text = "Avg Heal:";
                FormatLabel(Title_AverageHealingDone);
                Title_AverageCritHealingDone.Text = "Avg Crit Heal:";
                FormatLabel(Title_AverageCritHealingDone);
                Title_HealCritRate.Text = "Heal Crit Rate:";
                FormatLabel(Title_HealCritRate);
            }

            if (DefenseSection)
            {
                SectionIndex += 1;
                Title_BlockRate.Text = "Block Rate:";
                FormatLabel(Title_BlockRate);
                Title_EvadeRate.Text = "Evade Rate:";
                FormatLabel(Title_EvadeRate);
                Title_ParryRate.Text = "Parry Rate:";
                FormatLabel(Title_ParryRate);
                Title_MissRate.Text = "Miss Rate:";
                FormatLabel(Title_MissRate);
            }

            if (PetSection)
            {
                SectionIndex += 1;
                Title_TotalPetDamage.Text = "Total Pet Damage:";
                FormatLabel(Title_TotalPetDamage);
                Title_PetMeleeCritRate.Text = "Pet Melee Crit Rate:";
                FormatLabel(Title_PetMeleeCritRate);
                Title_TotalPetHealing.Text = "Total Pet Healing:";
                FormatLabel(Title_TotalPetHealing);
            }

            SectionIndex += 1;
            Title_GoldEarned.Text = "Gold Earned:";
            FormatLabel(Title_GoldEarned);
            Title_RealmPoints.Text = "Realm Points Earned:";
            FormatLabel(Title_RealmPoints);
            Title_IRS.Text = "IRS:";
            FormatLabel(Title_IRS);
            #endregion

            #region Values
            ColumnIndex = 0;
            RowIndex = 0;
            SectionIndex = 0;
            FormatLabel(Value_TotalDamageDone);

            if (SpellDamageSection)
            {
                SectionIndex += 1;
                FormatLabel(Value_TotalSpellDamage);
                FormatLabel(Value_AverageSpellDamageDone);
                FormatLabel(Value_AverageSpellCritDamageDone);
                FormatLabel(Value_SpellCritRate);
            }

            if (MeleeDamageSection)
            {
                SectionIndex += 1;
                FormatLabel(Value_TotalMeleeDamage);
                FormatLabel(Value_AverageMeleeDamageDone);
                FormatLabel(Value_AverageMeleeCritDamageDone);
                FormatLabel(Value_MeleeCritRate);
            }

            SectionIndex += 1;
            FormatLabel(Value_TotalDamageTaken);
            if (MitigationSection)
            {
                FormatLabel(Value_DamageTakenAbsorbed);
                FormatLabel(Value_DamageTakenConverted);
                FormatLabel(Value_DamageTakenBlocked);
            }

            SectionIndex += 1;
            FormatLabel(Value_DeathBlows);
            FormatLabel(Value_Deaths);

            ColumnIndex = 1;
            RowIndex = 0;
            SectionIndex = 0;
            FormatLabel(Value_TotalHealingRecieved);

            if (HealingSection)
            {
                SectionIndex += 1;
                FormatLabel(Value_TotalHealingDone);
                FormatLabel(Value_AverageHealingDone);
                FormatLabel(Value_AverageCritHealingDone);
                FormatLabel(Value_HealCritRate);
            }

            if (DefenseSection)
            {
                SectionIndex += 1;
                FormatLabel(Value_BlockRate);
                FormatLabel(Value_EvadeRate);
                FormatLabel(Value_ParryRate);
                FormatLabel(Value_MissRate);
            }

            if (PetSection)
            {
                SectionIndex += 1;
                FormatLabel(Value_TotalPetDamage);
                FormatLabel(Value_PetMeleeCritRate);
                FormatLabel(Value_TotalPetHealing);
            }

            SectionIndex += 1;
            FormatLabel(Value_GoldEarned);
            FormatLabel(Value_RealmPoints);
            FormatLabel(Value_IRS);
            #endregion

            DrawLabels();
            this.BackgroundImage = BTMP;
        }

        public void Draw()
        {
            Labels.Clear();
            FormatOverlay();
        }

        public void SetLabelBackcolor(Color color)
        {
            myBackColor = color;
            Draw();
        }

        public void SetLabelForecolor(Color color)
        {
            myForeColor = color;
            Draw();
        }
    }
}
