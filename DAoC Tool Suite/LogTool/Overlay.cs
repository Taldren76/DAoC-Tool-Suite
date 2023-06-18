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

        private static void FormatLabel(Label label, int column, int row, int breakCount = 0)
        {
            label.AutoSize = false;
            label.AutoSize = true;
            int padding = 0;
            int rowStep = 15;
            int point_X;
            int point_Y = breakCount * (rowStep / 2);

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
                point_Y += row * rowStep;
                textAlignment = ContentAlignment.MiddleRight;
            }
            else
            {
                point_X = column + padding;
                point_Y += row * rowStep;
                textAlignment = ContentAlignment.MiddleLeft;
            }
            label.Location = new(point_X, point_Y);
            label.TextAlign = textAlignment;
        }

        private void FormatOverlay()
        {
            //Column 1 Titles
            Title_TotalDamageDone.Text = "Total Damage Done:";
            FormatLabel(Title_TotalDamageDone, 0, 0, 0);
            Title_TotalSpellDamage.Text = "Total Spell Dmg:";
            FormatLabel(Title_TotalSpellDamage, 0, 1, 1);
            Title_AverageSpellDamageDone.Text = "Avg Spell Dmg:";
            FormatLabel(Title_AverageSpellDamageDone, 0, 2, 1);
            Title_AverageSpellCritDamageDone.Text = "Avg Spell Crit Dmg:";
            FormatLabel(Title_AverageSpellCritDamageDone, 0, 3, 1);
            Title_SpellCritRate.Text = "Spell Crit Rate:";
            FormatLabel(Title_SpellCritRate, 0, 4, 1);
            Title_TotalMeleeDamage.Text = "Total Melee Dmg:";
            FormatLabel(Title_TotalMeleeDamage, 0, 5, 2);
            Title_AverageMeleeDamageDone.Text = "Avg Melee Dmg:";
            FormatLabel(Title_AverageMeleeDamageDone, 0, 6, 2);
            Title_AverageMeleeCritDamageDone.Text = "Avg Melee Crit Dmg:";
            FormatLabel(Title_AverageMeleeCritDamageDone, 0, 7, 2);
            Title_MeleeCritRate.Text = "Melee Crit Rate:";
            FormatLabel(Title_MeleeCritRate, 0, 8, 2);
            Title_TotalDamageTaken.Text = "Total Damage Taken:";
            FormatLabel(Title_TotalDamageTaken, 0, 9, 3);
            Title_DamageTakenAbsorbed.Text = "Dmg Absorbed:";
            FormatLabel(Title_DamageTakenAbsorbed, 0, 10, 3);
            Title_DamageTakenConverted.Text = "Dmg Converted:";
            FormatLabel(Title_DamageTakenConverted, 0, 11, 3);
            Title_DamageTakenBlocked.Text = "Dmg Blocked:";
            FormatLabel(Title_DamageTakenBlocked, 0, 12, 3);
            Title_DeathBlows.Text = "DeathBlows:";
            FormatLabel(Title_DeathBlows, 0, 13, 4);
            Title_Deaths.Text = "Deaths:";
            FormatLabel(Title_Deaths, 0, 14, 4);

            //Column 2 Titles
            Title_TotalHealingRecieved.Text = "Total Healing Recieved:";
            FormatLabel(Title_TotalHealingRecieved, 1, 0, 0);
            Title_TotalHealingDone.Text = "Total Healing Done:";
            FormatLabel(Title_TotalHealingDone, 1, 1, 1);
            Title_AverageHealingDone.Text = "Avg Heal:";
            FormatLabel(Title_AverageHealingDone, 1, 2, 1);
            Title_AverageCritHealingDone.Text = "Avg Crit Heal:";
            FormatLabel(Title_AverageCritHealingDone, 1, 3, 1);
            Title_HealCritRate.Text = "Heal Crit Rate:";
            FormatLabel(Title_HealCritRate, 1, 4, 1);
            Title_BlockRate.Text = "Block Rate:";
            FormatLabel(Title_BlockRate, 1, 5, 2);
            Title_EvadeRate.Text = "Evade Rate:";
            FormatLabel(Title_EvadeRate, 1, 6, 2);
            Title_ParryRate.Text = "Parry Rate:";
            FormatLabel(Title_ParryRate, 1, 7, 2);
            Title_MissRate.Text = "Miss Rate:";
            FormatLabel(Title_MissRate, 1, 8, 2);
            Title_TotalPetDamage.Text = "Total Pet Damage:";
            FormatLabel(Title_TotalPetDamage, 1, 9, 3);
            Title_PetMeleeCritRate.Text = "Pet Melee Crit Rate:";
            FormatLabel(Title_PetMeleeCritRate, 1, 10, 3);
            Title_TotalPetHealing.Text = "Total Pet Healing:";
            FormatLabel(Title_TotalPetHealing, 1, 11, 3);
            Title_GoldEarned.Text = "Gold Earned:";
            FormatLabel(Title_GoldEarned, 1, 12, 4);
            Title_RealmPoints.Text = "Realm Points Earned:";
            FormatLabel(Title_RealmPoints, 1, 13, 4);
            Title_IRS.Text = "IRS:";
            FormatLabel(Title_IRS, 1, 14, 4);

            //Column 1 Values
            FormatLabel(Value_TotalDamageDone, 0, 0, 0);
            FormatLabel(Value_TotalSpellDamage, 0, 1, 1);
            FormatLabel(Value_AverageSpellDamageDone, 0, 2, 1);
            FormatLabel(Value_AverageSpellCritDamageDone, 0, 3, 1);
            FormatLabel(Value_SpellCritRate, 0, 4, 1);
            FormatLabel(Value_TotalMeleeDamage, 0, 5, 2);
            FormatLabel(Value_AverageMeleeDamageDone, 0, 6, 2);
            FormatLabel(Value_AverageMeleeCritDamageDone, 0, 7, 2);
            FormatLabel(Value_MeleeCritRate, 0, 8, 2);
            FormatLabel(Value_TotalDamageTaken, 0, 9, 3);
            FormatLabel(Value_DamageTakenAbsorbed, 0, 10, 3);
            FormatLabel(Value_DamageTakenConverted, 0, 11, 3);
            FormatLabel(Value_DamageTakenBlocked, 0, 12, 3);
            FormatLabel(Value_DeathBlows, 0, 13, 4);
            FormatLabel(Value_Deaths, 0, 14, 4);

            //Column 2 Values
            FormatLabel(Value_TotalHealingRecieved, 1, 0, 0);
            FormatLabel(Value_TotalHealingDone, 1, 1, 1);
            FormatLabel(Value_AverageHealingDone, 1, 2, 1);
            FormatLabel(Value_AverageCritHealingDone, 1, 3, 1);
            FormatLabel(Value_HealCritRate, 1, 4, 1);
            FormatLabel(Value_BlockRate, 1, 5, 2);
            FormatLabel(Value_EvadeRate, 1, 6, 2);
            FormatLabel(Value_ParryRate, 1, 7, 2);
            FormatLabel(Value_MissRate, 1, 8, 2);
            FormatLabel(Value_TotalPetDamage, 1, 9, 3);
            FormatLabel(Value_PetMeleeCritRate, 1, 10, 3);
            FormatLabel(Value_TotalPetHealing, 1, 11, 3);
            FormatLabel(Value_GoldEarned, 1, 12, 4);
            FormatLabel(Value_RealmPoints, 1, 13, 4);
            FormatLabel(Value_IRS, 1, 14, 4);
        }

        public void SetLabelBackcolor(Color color)
        {
            Title_AverageSpellCritDamageDone.BackColor = color;
            Title_AverageCritHealingDone.BackColor = color;
            Title_AverageSpellDamageDone.BackColor = color;
            Title_AverageHealingDone.BackColor = color;
            Title_SpellCritRate.BackColor = color;
            Title_DamageTakenAbsorbed.BackColor = color;
            Title_DeathBlows.BackColor = color;
            Title_Deaths.BackColor = color;
            Title_HealCritRate.BackColor = color;
            Title_IRS.BackColor = color;
            Title_RealmPoints.BackColor = color;
            Title_DamageTakenBlocked.BackColor = color;
            Title_DamageTakenConverted.BackColor = color;
            Title_TotalDamageDone.BackColor = color;
            Title_TotalDamageTaken.BackColor = color;
            Title_TotalHealingDone.BackColor = color;
            Title_TotalHealingRecieved.BackColor = color;

            Title_TotalSpellDamage.BackColor = color;
            Title_TotalMeleeDamage.BackColor = color;
            Title_AverageMeleeDamageDone.BackColor = color;
            Title_AverageMeleeCritDamageDone.BackColor = color;
            Title_MeleeCritRate.BackColor = color;
            Title_BlockRate.BackColor = color;
            Title_EvadeRate.BackColor = color;
            Title_ParryRate.BackColor = color;
            Title_MissRate.BackColor = color;
            Title_TotalPetDamage.BackColor = color;
            Title_PetMeleeCritRate.BackColor = color;
            Title_TotalPetHealing.BackColor = color;
            Title_GoldEarned.BackColor = color;

            Value_AverageSpellCritDamageDone.BackColor = color;
            Value_AverageCritHealingDone.BackColor = color;
            Value_AverageSpellDamageDone.BackColor = color;
            Value_AverageHealingDone.BackColor = color;
            Value_SpellCritRate.BackColor = color;
            Value_DamageTakenAbsorbed.BackColor = color;
            Value_DeathBlows.BackColor = color;
            Value_Deaths.BackColor = color;
            Value_HealCritRate.BackColor = color;
            Value_IRS.BackColor = color;
            Value_RealmPoints.BackColor = color;
            Value_DamageTakenBlocked.BackColor = color;
            Value_DamageTakenConverted.BackColor = color;
            Value_TotalDamageDone.BackColor = color;
            Value_TotalDamageTaken.BackColor = color;
            Value_TotalHealingDone.BackColor = color;
            Value_TotalHealingRecieved.BackColor = color;

            Value_TotalSpellDamage.BackColor = color;
            Value_TotalMeleeDamage.BackColor = color;
            Value_AverageMeleeDamageDone.BackColor = color;
            Value_AverageMeleeCritDamageDone.BackColor = color;
            Value_MeleeCritRate.BackColor = color;
            Value_BlockRate.BackColor = color;
            Value_EvadeRate.BackColor = color;
            Value_ParryRate.BackColor = color;
            Value_MissRate.BackColor = color;
            Value_TotalPetDamage.BackColor = color;
            Value_PetMeleeCritRate.BackColor = color;
            Value_TotalPetHealing.BackColor = color;
            Value_GoldEarned.BackColor = color;

            Refresh();
        }

        public void SetLabelForecolor(Color color)
        {
            Title_AverageSpellCritDamageDone.ForeColor = color;
            Title_AverageCritHealingDone.ForeColor = color;
            Title_AverageSpellDamageDone.ForeColor = color;
            Title_AverageHealingDone.ForeColor = color;
            Title_SpellCritRate.ForeColor = color;
            Title_DamageTakenAbsorbed.ForeColor = color;
            Title_DeathBlows.ForeColor = color;
            Title_Deaths.ForeColor = color;
            Title_HealCritRate.ForeColor = color;
            Title_IRS.ForeColor = color;
            Title_RealmPoints.ForeColor = color;
            Title_DamageTakenBlocked.ForeColor = color;
            Title_DamageTakenConverted.ForeColor = color;
            Title_TotalDamageDone.ForeColor = color;
            Title_TotalDamageTaken.ForeColor = color;
            Title_TotalHealingDone.ForeColor = color;
            Title_TotalHealingRecieved.ForeColor = color;
            Title_TotalSpellDamage.ForeColor = color;
            Title_TotalMeleeDamage.ForeColor = color;
            Title_AverageMeleeDamageDone.ForeColor = color;
            Title_AverageMeleeCritDamageDone.ForeColor = color;
            Title_MeleeCritRate.ForeColor = color;
            Title_BlockRate.ForeColor = color;
            Title_EvadeRate.ForeColor = color;
            Title_ParryRate.ForeColor = color;
            Title_MissRate.ForeColor = color;
            Title_TotalPetDamage.ForeColor = color;
            Title_PetMeleeCritRate.ForeColor = color;
            Title_TotalPetHealing.ForeColor = color;
            Title_GoldEarned.ForeColor = color;

            Value_AverageSpellCritDamageDone.ForeColor = color;
            Value_AverageCritHealingDone.ForeColor = color;
            Value_AverageSpellDamageDone.ForeColor = color;
            Value_AverageHealingDone.ForeColor = color;
            Value_SpellCritRate.ForeColor = color;
            Value_DamageTakenAbsorbed.ForeColor = color;
            Value_DeathBlows.ForeColor = color;
            Value_Deaths.ForeColor = color;
            Value_HealCritRate.ForeColor = color;
            Value_IRS.ForeColor = color;
            Value_RealmPoints.ForeColor = color;
            Value_DamageTakenBlocked.ForeColor = color;
            Value_DamageTakenConverted.ForeColor = color;
            Value_TotalDamageDone.ForeColor = color;
            Value_TotalDamageTaken.ForeColor = color;
            Value_TotalHealingDone.ForeColor = color;
            Value_TotalHealingRecieved.ForeColor = color;
            Value_TotalSpellDamage.ForeColor = color;
            Value_TotalMeleeDamage.ForeColor = color;
            Value_AverageMeleeDamageDone.ForeColor = color;
            Value_AverageMeleeCritDamageDone.ForeColor = color;
            Value_MeleeCritRate.ForeColor = color;
            Value_BlockRate.ForeColor = color;
            Value_EvadeRate.ForeColor = color;
            Value_ParryRate.ForeColor = color;
            Value_MissRate.ForeColor = color;
            Value_TotalPetDamage.ForeColor = color;
            Value_PetMeleeCritRate.ForeColor = color;
            Value_TotalPetHealing.ForeColor = color;
            Value_GoldEarned.ForeColor = color;

            Refresh();
        }
    }
}
