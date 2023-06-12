namespace DAoCToolSuite.LogTool
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            BrowseButton = new Button();
            groupBox1 = new GroupBox();
            LogFileTextBox = new TextBox();
            splitContainer1 = new SplitContainer();
            Title_IRS = new Label();
            Value_IRS = new Label();
            Title_DamageDoneBlocked = new Label();
            Value_DamageDoneBlocked = new Label();
            Title_TotalDamageBlocked = new Label();
            Value_TotalDamageBlocked = new Label();
            Title_TotalDamageConverted = new Label();
            Value_TotalDamageConverted = new Label();
            Title_Deaths = new Label();
            Value_Deaths = new Label();
            Title_DeathBlows = new Label();
            Value_DeathBlows = new Label();
            Title_TotalDamageTaken = new Label();
            Value_TotalDamageTaken = new Label();
            Title_DamageTakenAbsorbed = new Label();
            Value_DamageTakenAbsorbed = new Label();
            Title_DamageAbsorbed = new Label();
            Value_DamageAbsorbed = new Label();
            Title_CritDamageRatio = new Label();
            Value_CritDamageRatio = new Label();
            Title_DamageCritRate = new Label();
            Value_DamageCritRate = new Label();
            Title_RealmPoints = new Label();
            Value_RealmPoints = new Label();
            Title_TotalDamageDone = new Label();
            Value_TotalDamageDone = new Label();
            Title_TotalHealingRecieved = new Label();
            Value_TotalHealingRecieved = new Label();
            Title_TotalHealingDone = new Label();
            Value_TotalHealingDone = new Label();
            Value_HealCritRate = new Label();
            Title_HealCritRate = new Label();
            Title_CritHealRatio = new Label();
            Value_CritHealRatio = new Label();
            Title_HealSelfRatio = new Label();
            Value_HealSelfRatio = new Label();
            Title_AverageCritHealingDone = new Label();
            Value_AverageCritHealingDone = new Label();
            Title_AverageHealingDone = new Label();
            Title_AverageCritDamageDone = new Label();
            Title_AverageDamageDone = new Label();
            Value_AverageHealingDone = new Label();
            Value_AverageCritDamageDone = new Label();
            Value_AverageDamageDone = new Label();
            ResetButton = new Button();
            LogDatesComboBox = new ComboBox();
            ParseButton = new Button();
            label1 = new Label();
            FilterPlayersOnlyCheckBox = new CheckBox();
            OverlayTransparentCheckBox = new CheckBox();
            FontColorPanel = new Panel();
            OverLayFontColorButton = new Button();
            label2 = new Label();
            OverLayOpacityControl = new NumericUpDown();
            LockOverlayButton = new Button();
            OverlayButton = new Button();
            colorDialog1 = new ColorDialog();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OverLayOpacityControl).BeginInit();
            SuspendLayout();
            // 
            // BrowseButton
            // 
            BrowseButton.Location = new Point(698, 21);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Size = new Size(75, 23);
            BrowseButton.TabIndex = 0;
            BrowseButton.Text = "Browse";
            BrowseButton.UseVisualStyleBackColor = true;
            BrowseButton.Click += BrowseButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(LogFileTextBox);
            groupBox1.Controls.Add(BrowseButton);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 60);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "LogFile Location and Name";
            // 
            // LogFileTextBox
            // 
            LogFileTextBox.Location = new Point(6, 22);
            LogFileTextBox.Name = "LogFileTextBox";
            LogFileTextBox.Size = new Size(686, 23);
            LogFileTextBox.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(12, 78);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.Black;
            splitContainer1.Panel1.Controls.Add(Title_IRS);
            splitContainer1.Panel1.Controls.Add(Value_IRS);
            splitContainer1.Panel1.Controls.Add(Title_DamageDoneBlocked);
            splitContainer1.Panel1.Controls.Add(Value_DamageDoneBlocked);
            splitContainer1.Panel1.Controls.Add(Title_TotalDamageBlocked);
            splitContainer1.Panel1.Controls.Add(Value_TotalDamageBlocked);
            splitContainer1.Panel1.Controls.Add(Title_TotalDamageConverted);
            splitContainer1.Panel1.Controls.Add(Value_TotalDamageConverted);
            splitContainer1.Panel1.Controls.Add(Title_Deaths);
            splitContainer1.Panel1.Controls.Add(Value_Deaths);
            splitContainer1.Panel1.Controls.Add(Title_DeathBlows);
            splitContainer1.Panel1.Controls.Add(Value_DeathBlows);
            splitContainer1.Panel1.Controls.Add(Title_TotalDamageTaken);
            splitContainer1.Panel1.Controls.Add(Value_TotalDamageTaken);
            splitContainer1.Panel1.Controls.Add(Title_DamageTakenAbsorbed);
            splitContainer1.Panel1.Controls.Add(Value_DamageTakenAbsorbed);
            splitContainer1.Panel1.Controls.Add(Title_DamageAbsorbed);
            splitContainer1.Panel1.Controls.Add(Value_DamageAbsorbed);
            splitContainer1.Panel1.Controls.Add(Title_CritDamageRatio);
            splitContainer1.Panel1.Controls.Add(Value_CritDamageRatio);
            splitContainer1.Panel1.Controls.Add(Title_DamageCritRate);
            splitContainer1.Panel1.Controls.Add(Value_DamageCritRate);
            splitContainer1.Panel1.Controls.Add(Title_RealmPoints);
            splitContainer1.Panel1.Controls.Add(Value_RealmPoints);
            splitContainer1.Panel1.Controls.Add(Title_TotalDamageDone);
            splitContainer1.Panel1.Controls.Add(Value_TotalDamageDone);
            splitContainer1.Panel1.Controls.Add(Title_TotalHealingRecieved);
            splitContainer1.Panel1.Controls.Add(Value_TotalHealingRecieved);
            splitContainer1.Panel1.Controls.Add(Title_TotalHealingDone);
            splitContainer1.Panel1.Controls.Add(Value_TotalHealingDone);
            splitContainer1.Panel1.Controls.Add(Value_HealCritRate);
            splitContainer1.Panel1.Controls.Add(Title_HealCritRate);
            splitContainer1.Panel1.Controls.Add(Title_CritHealRatio);
            splitContainer1.Panel1.Controls.Add(Value_CritHealRatio);
            splitContainer1.Panel1.Controls.Add(Title_HealSelfRatio);
            splitContainer1.Panel1.Controls.Add(Value_HealSelfRatio);
            splitContainer1.Panel1.Controls.Add(Title_AverageCritHealingDone);
            splitContainer1.Panel1.Controls.Add(Value_AverageCritHealingDone);
            splitContainer1.Panel1.Controls.Add(Title_AverageHealingDone);
            splitContainer1.Panel1.Controls.Add(Title_AverageCritDamageDone);
            splitContainer1.Panel1.Controls.Add(Title_AverageDamageDone);
            splitContainer1.Panel1.Controls.Add(Value_AverageHealingDone);
            splitContainer1.Panel1.Controls.Add(Value_AverageCritDamageDone);
            splitContainer1.Panel1.Controls.Add(Value_AverageDamageDone);
            splitContainer1.Panel1.Controls.Add(ResetButton);
            splitContainer1.Panel1.Controls.Add(LogDatesComboBox);
            splitContainer1.Panel1.Controls.Add(ParseButton);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.ForeColor = SystemColors.ControlText;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(FilterPlayersOnlyCheckBox);
            splitContainer1.Panel2.Controls.Add(OverlayTransparentCheckBox);
            splitContainer1.Panel2.Controls.Add(FontColorPanel);
            splitContainer1.Panel2.Controls.Add(OverLayFontColorButton);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(OverLayOpacityControl);
            splitContainer1.Panel2.Controls.Add(LockOverlayButton);
            splitContainer1.Panel2.Controls.Add(OverlayButton);
            splitContainer1.Size = new Size(776, 310);
            splitContainer1.SplitterDistance = 631;
            splitContainer1.TabIndex = 2;
            // 
            // Title_IRS
            // 
            Title_IRS.AutoSize = true;
            Title_IRS.BackColor = Color.Black;
            Title_IRS.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_IRS.ForeColor = Color.White;
            Title_IRS.Location = new Point(442, 282);
            Title_IRS.Name = "Title_IRS";
            Title_IRS.Size = new Size(36, 14);
            Title_IRS.TabIndex = 113;
            Title_IRS.Text = "IRS:";
            // 
            // Value_IRS
            // 
            Value_IRS.AutoSize = true;
            Value_IRS.BackColor = Color.Black;
            Value_IRS.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_IRS.ForeColor = Color.White;
            Value_IRS.Location = new Point(486, 282);
            Value_IRS.Name = "Value_IRS";
            Value_IRS.Size = new Size(0, 14);
            Value_IRS.TabIndex = 112;
            // 
            // Title_DamageDoneBlocked
            // 
            Title_DamageDoneBlocked.AutoSize = true;
            Title_DamageDoneBlocked.BackColor = Color.Black;
            Title_DamageDoneBlocked.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_DamageDoneBlocked.ForeColor = Color.White;
            Title_DamageDoneBlocked.Location = new Point(356, 143);
            Title_DamageDoneBlocked.Name = "Title_DamageDoneBlocked";
            Title_DamageDoneBlocked.Size = new Size(122, 14);
            Title_DamageDoneBlocked.TabIndex = 111;
            Title_DamageDoneBlocked.Text = "Damage Blocked:";
            // 
            // Value_DamageDoneBlocked
            // 
            Value_DamageDoneBlocked.AutoSize = true;
            Value_DamageDoneBlocked.BackColor = Color.Black;
            Value_DamageDoneBlocked.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_DamageDoneBlocked.ForeColor = Color.White;
            Value_DamageDoneBlocked.Location = new Point(485, 143);
            Value_DamageDoneBlocked.Name = "Value_DamageDoneBlocked";
            Value_DamageDoneBlocked.Size = new Size(0, 14);
            Value_DamageDoneBlocked.TabIndex = 110;
            // 
            // Title_TotalDamageBlocked
            // 
            Title_TotalDamageBlocked.AutoSize = true;
            Title_TotalDamageBlocked.BackColor = Color.Black;
            Title_TotalDamageBlocked.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_TotalDamageBlocked.ForeColor = Color.White;
            Title_TotalDamageBlocked.Location = new Point(29, 283);
            Title_TotalDamageBlocked.Name = "Title_TotalDamageBlocked";
            Title_TotalDamageBlocked.Size = new Size(159, 14);
            Title_TotalDamageBlocked.TabIndex = 108;
            Title_TotalDamageBlocked.Text = "Total Damage Blocked:";
            // 
            // Value_TotalDamageBlocked
            // 
            Value_TotalDamageBlocked.AutoSize = true;
            Value_TotalDamageBlocked.BackColor = Color.Black;
            Value_TotalDamageBlocked.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_TotalDamageBlocked.ForeColor = Color.White;
            Value_TotalDamageBlocked.Location = new Point(195, 283);
            Value_TotalDamageBlocked.Name = "Value_TotalDamageBlocked";
            Value_TotalDamageBlocked.Size = new Size(0, 14);
            Value_TotalDamageBlocked.TabIndex = 109;
            // 
            // Title_TotalDamageConverted
            // 
            Title_TotalDamageConverted.AutoSize = true;
            Title_TotalDamageConverted.BackColor = Color.Black;
            Title_TotalDamageConverted.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_TotalDamageConverted.ForeColor = Color.White;
            Title_TotalDamageConverted.Location = new Point(13, 243);
            Title_TotalDamageConverted.Name = "Title_TotalDamageConverted";
            Title_TotalDamageConverted.Size = new Size(175, 14);
            Title_TotalDamageConverted.TabIndex = 106;
            Title_TotalDamageConverted.Text = "Total Damage Converted:";
            // 
            // Value_TotalDamageConverted
            // 
            Value_TotalDamageConverted.AutoSize = true;
            Value_TotalDamageConverted.BackColor = Color.Black;
            Value_TotalDamageConverted.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_TotalDamageConverted.ForeColor = Color.White;
            Value_TotalDamageConverted.Location = new Point(195, 243);
            Value_TotalDamageConverted.Name = "Value_TotalDamageConverted";
            Value_TotalDamageConverted.Size = new Size(0, 14);
            Value_TotalDamageConverted.TabIndex = 107;
            // 
            // Title_Deaths
            // 
            Title_Deaths.AutoSize = true;
            Title_Deaths.BackColor = Color.Black;
            Title_Deaths.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_Deaths.ForeColor = Color.White;
            Title_Deaths.Location = new Point(420, 233);
            Title_Deaths.Name = "Title_Deaths";
            Title_Deaths.Size = new Size(58, 14);
            Title_Deaths.TabIndex = 105;
            Title_Deaths.Text = "Deaths:";
            // 
            // Value_Deaths
            // 
            Value_Deaths.AutoSize = true;
            Value_Deaths.BackColor = Color.Black;
            Value_Deaths.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_Deaths.ForeColor = Color.White;
            Value_Deaths.Location = new Point(485, 233);
            Value_Deaths.Name = "Value_Deaths";
            Value_Deaths.Size = new Size(0, 14);
            Value_Deaths.TabIndex = 104;
            // 
            // Title_DeathBlows
            // 
            Title_DeathBlows.AutoSize = true;
            Title_DeathBlows.BackColor = Color.Black;
            Title_DeathBlows.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_DeathBlows.ForeColor = Color.White;
            Title_DeathBlows.Location = new Point(387, 213);
            Title_DeathBlows.Name = "Title_DeathBlows";
            Title_DeathBlows.Size = new Size(91, 14);
            Title_DeathBlows.TabIndex = 103;
            Title_DeathBlows.Text = "DeathBlows:";
            // 
            // Value_DeathBlows
            // 
            Value_DeathBlows.AutoSize = true;
            Value_DeathBlows.BackColor = Color.Black;
            Value_DeathBlows.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_DeathBlows.ForeColor = Color.White;
            Value_DeathBlows.Location = new Point(485, 213);
            Value_DeathBlows.Name = "Value_DeathBlows";
            Value_DeathBlows.Size = new Size(0, 14);
            Value_DeathBlows.TabIndex = 102;
            // 
            // Title_TotalDamageTaken
            // 
            Title_TotalDamageTaken.AutoSize = true;
            Title_TotalDamageTaken.BackColor = Color.Black;
            Title_TotalDamageTaken.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_TotalDamageTaken.ForeColor = Color.White;
            Title_TotalDamageTaken.Location = new Point(41, 223);
            Title_TotalDamageTaken.Name = "Title_TotalDamageTaken";
            Title_TotalDamageTaken.Size = new Size(147, 14);
            Title_TotalDamageTaken.TabIndex = 101;
            Title_TotalDamageTaken.Text = "Total Damage Taken:";
            // 
            // Value_TotalDamageTaken
            // 
            Value_TotalDamageTaken.AutoSize = true;
            Value_TotalDamageTaken.BackColor = Color.Black;
            Value_TotalDamageTaken.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_TotalDamageTaken.ForeColor = Color.White;
            Value_TotalDamageTaken.Location = new Point(195, 223);
            Value_TotalDamageTaken.Name = "Value_TotalDamageTaken";
            Value_TotalDamageTaken.Size = new Size(0, 14);
            Value_TotalDamageTaken.TabIndex = 100;
            // 
            // Title_DamageTakenAbsorbed
            // 
            Title_DamageTakenAbsorbed.AutoSize = true;
            Title_DamageTakenAbsorbed.BackColor = Color.Black;
            Title_DamageTakenAbsorbed.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_DamageTakenAbsorbed.ForeColor = Color.White;
            Title_DamageTakenAbsorbed.Location = new Point(19, 263);
            Title_DamageTakenAbsorbed.Name = "Title_DamageTakenAbsorbed";
            Title_DamageTakenAbsorbed.Size = new Size(169, 14);
            Title_DamageTakenAbsorbed.TabIndex = 97;
            Title_DamageTakenAbsorbed.Text = "Total Damage Absorbed:";
            // 
            // Value_DamageTakenAbsorbed
            // 
            Value_DamageTakenAbsorbed.AutoSize = true;
            Value_DamageTakenAbsorbed.BackColor = Color.Black;
            Value_DamageTakenAbsorbed.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_DamageTakenAbsorbed.ForeColor = Color.White;
            Value_DamageTakenAbsorbed.Location = new Point(195, 263);
            Value_DamageTakenAbsorbed.Name = "Value_DamageTakenAbsorbed";
            Value_DamageTakenAbsorbed.Size = new Size(0, 14);
            Value_DamageTakenAbsorbed.TabIndex = 99;
            // 
            // Title_DamageAbsorbed
            // 
            Title_DamageAbsorbed.AutoSize = true;
            Title_DamageAbsorbed.BackColor = Color.Black;
            Title_DamageAbsorbed.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_DamageAbsorbed.ForeColor = Color.White;
            Title_DamageAbsorbed.Location = new Point(346, 123);
            Title_DamageAbsorbed.Name = "Title_DamageAbsorbed";
            Title_DamageAbsorbed.Size = new Size(132, 14);
            Title_DamageAbsorbed.TabIndex = 98;
            Title_DamageAbsorbed.Text = "Damage Absorbed:";
            // 
            // Value_DamageAbsorbed
            // 
            Value_DamageAbsorbed.AutoSize = true;
            Value_DamageAbsorbed.BackColor = Color.Black;
            Value_DamageAbsorbed.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_DamageAbsorbed.ForeColor = Color.White;
            Value_DamageAbsorbed.Location = new Point(485, 123);
            Value_DamageAbsorbed.Name = "Value_DamageAbsorbed";
            Value_DamageAbsorbed.Size = new Size(0, 14);
            Value_DamageAbsorbed.TabIndex = 96;
            // 
            // Title_CritDamageRatio
            // 
            Title_CritDamageRatio.AutoSize = true;
            Title_CritDamageRatio.BackColor = Color.Black;
            Title_CritDamageRatio.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_CritDamageRatio.ForeColor = Color.White;
            Title_CritDamageRatio.Location = new Point(346, 183);
            Title_CritDamageRatio.Name = "Title_CritDamageRatio";
            Title_CritDamageRatio.Size = new Size(132, 14);
            Title_CritDamageRatio.TabIndex = 95;
            Title_CritDamageRatio.Text = "Crit Damage Ratio:";
            // 
            // Value_CritDamageRatio
            // 
            Value_CritDamageRatio.AutoSize = true;
            Value_CritDamageRatio.BackColor = Color.Black;
            Value_CritDamageRatio.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_CritDamageRatio.ForeColor = Color.White;
            Value_CritDamageRatio.Location = new Point(485, 183);
            Value_CritDamageRatio.Name = "Value_CritDamageRatio";
            Value_CritDamageRatio.Size = new Size(0, 14);
            Value_CritDamageRatio.TabIndex = 94;
            // 
            // Title_DamageCritRate
            // 
            Title_DamageCritRate.AutoSize = true;
            Title_DamageCritRate.BackColor = Color.Black;
            Title_DamageCritRate.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_DamageCritRate.ForeColor = Color.White;
            Title_DamageCritRate.Location = new Point(350, 163);
            Title_DamageCritRate.Name = "Title_DamageCritRate";
            Title_DamageCritRate.Size = new Size(128, 14);
            Title_DamageCritRate.TabIndex = 93;
            Title_DamageCritRate.Text = "Damage Crit Rate:";
            // 
            // Value_DamageCritRate
            // 
            Value_DamageCritRate.AutoSize = true;
            Value_DamageCritRate.BackColor = Color.Black;
            Value_DamageCritRate.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_DamageCritRate.ForeColor = Color.White;
            Value_DamageCritRate.Location = new Point(485, 163);
            Value_DamageCritRate.Name = "Value_DamageCritRate";
            Value_DamageCritRate.Size = new Size(0, 14);
            Value_DamageCritRate.TabIndex = 92;
            // 
            // Title_RealmPoints
            // 
            Title_RealmPoints.AutoSize = true;
            Title_RealmPoints.BackColor = Color.Black;
            Title_RealmPoints.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_RealmPoints.ForeColor = Color.White;
            Title_RealmPoints.Location = new Point(380, 263);
            Title_RealmPoints.Name = "Title_RealmPoints";
            Title_RealmPoints.Size = new Size(98, 14);
            Title_RealmPoints.TabIndex = 91;
            Title_RealmPoints.Text = "Realm Points:";
            // 
            // Value_RealmPoints
            // 
            Value_RealmPoints.AutoSize = true;
            Value_RealmPoints.BackColor = Color.Black;
            Value_RealmPoints.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_RealmPoints.ForeColor = Color.White;
            Value_RealmPoints.Location = new Point(485, 263);
            Value_RealmPoints.Name = "Value_RealmPoints";
            Value_RealmPoints.Size = new Size(0, 14);
            Value_RealmPoints.TabIndex = 90;
            // 
            // Title_TotalDamageDone
            // 
            Title_TotalDamageDone.AutoSize = true;
            Title_TotalDamageDone.BackColor = Color.Black;
            Title_TotalDamageDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_TotalDamageDone.ForeColor = Color.White;
            Title_TotalDamageDone.Location = new Point(337, 103);
            Title_TotalDamageDone.Name = "Title_TotalDamageDone";
            Title_TotalDamageDone.Size = new Size(141, 14);
            Title_TotalDamageDone.TabIndex = 89;
            Title_TotalDamageDone.Text = "Total Damage Done:";
            // 
            // Value_TotalDamageDone
            // 
            Value_TotalDamageDone.AutoSize = true;
            Value_TotalDamageDone.BackColor = Color.Black;
            Value_TotalDamageDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_TotalDamageDone.ForeColor = Color.White;
            Value_TotalDamageDone.Location = new Point(485, 103);
            Value_TotalDamageDone.Name = "Value_TotalDamageDone";
            Value_TotalDamageDone.Size = new Size(0, 14);
            Value_TotalDamageDone.TabIndex = 88;
            // 
            // Title_TotalHealingRecieved
            // 
            Title_TotalHealingRecieved.AutoSize = true;
            Title_TotalHealingRecieved.BackColor = Color.Black;
            Title_TotalHealingRecieved.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_TotalHealingRecieved.ForeColor = Color.White;
            Title_TotalHealingRecieved.Location = new Point(25, 193);
            Title_TotalHealingRecieved.Name = "Title_TotalHealingRecieved";
            Title_TotalHealingRecieved.Size = new Size(163, 14);
            Title_TotalHealingRecieved.TabIndex = 87;
            Title_TotalHealingRecieved.Text = "Total Healing Recieved:";
            // 
            // Value_TotalHealingRecieved
            // 
            Value_TotalHealingRecieved.AutoSize = true;
            Value_TotalHealingRecieved.BackColor = Color.Black;
            Value_TotalHealingRecieved.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_TotalHealingRecieved.ForeColor = Color.White;
            Value_TotalHealingRecieved.Location = new Point(195, 193);
            Value_TotalHealingRecieved.Name = "Value_TotalHealingRecieved";
            Value_TotalHealingRecieved.Size = new Size(0, 14);
            Value_TotalHealingRecieved.TabIndex = 86;
            // 
            // Title_TotalHealingDone
            // 
            Title_TotalHealingDone.AutoSize = true;
            Title_TotalHealingDone.BackColor = Color.Black;
            Title_TotalHealingDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_TotalHealingDone.ForeColor = Color.White;
            Title_TotalHealingDone.Location = new Point(51, 103);
            Title_TotalHealingDone.Name = "Title_TotalHealingDone";
            Title_TotalHealingDone.Size = new Size(137, 14);
            Title_TotalHealingDone.TabIndex = 85;
            Title_TotalHealingDone.Text = "Total Healing Done:";
            // 
            // Value_TotalHealingDone
            // 
            Value_TotalHealingDone.AutoSize = true;
            Value_TotalHealingDone.BackColor = Color.Black;
            Value_TotalHealingDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_TotalHealingDone.ForeColor = Color.White;
            Value_TotalHealingDone.Location = new Point(195, 103);
            Value_TotalHealingDone.Name = "Value_TotalHealingDone";
            Value_TotalHealingDone.Size = new Size(0, 14);
            Value_TotalHealingDone.TabIndex = 84;
            // 
            // Value_HealCritRate
            // 
            Value_HealCritRate.AutoSize = true;
            Value_HealCritRate.BackColor = Color.Black;
            Value_HealCritRate.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_HealCritRate.ForeColor = Color.White;
            Value_HealCritRate.Location = new Point(195, 123);
            Value_HealCritRate.Name = "Value_HealCritRate";
            Value_HealCritRate.Size = new Size(0, 14);
            Value_HealCritRate.TabIndex = 82;
            // 
            // Title_HealCritRate
            // 
            Title_HealCritRate.AutoSize = true;
            Title_HealCritRate.BackColor = Color.Black;
            Title_HealCritRate.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_HealCritRate.ForeColor = Color.White;
            Title_HealCritRate.Location = new Point(84, 123);
            Title_HealCritRate.Name = "Title_HealCritRate";
            Title_HealCritRate.Size = new Size(104, 14);
            Title_HealCritRate.TabIndex = 79;
            Title_HealCritRate.Text = "Heal Crit Rate:";
            // 
            // Title_CritHealRatio
            // 
            Title_CritHealRatio.AutoSize = true;
            Title_CritHealRatio.BackColor = Color.Black;
            Title_CritHealRatio.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_CritHealRatio.ForeColor = Color.White;
            Title_CritHealRatio.Location = new Point(80, 143);
            Title_CritHealRatio.Name = "Title_CritHealRatio";
            Title_CritHealRatio.Size = new Size(108, 14);
            Title_CritHealRatio.TabIndex = 83;
            Title_CritHealRatio.Text = "Crit Heal Ratio:";
            // 
            // Value_CritHealRatio
            // 
            Value_CritHealRatio.AutoSize = true;
            Value_CritHealRatio.BackColor = Color.Black;
            Value_CritHealRatio.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_CritHealRatio.ForeColor = Color.White;
            Value_CritHealRatio.Location = new Point(195, 143);
            Value_CritHealRatio.Name = "Value_CritHealRatio";
            Value_CritHealRatio.Size = new Size(0, 14);
            Value_CritHealRatio.TabIndex = 80;
            // 
            // Title_HealSelfRatio
            // 
            Title_HealSelfRatio.AutoSize = true;
            Title_HealSelfRatio.BackColor = Color.Black;
            Title_HealSelfRatio.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_HealSelfRatio.ForeColor = Color.White;
            Title_HealSelfRatio.Location = new Point(78, 163);
            Title_HealSelfRatio.Name = "Title_HealSelfRatio";
            Title_HealSelfRatio.Size = new Size(110, 14);
            Title_HealSelfRatio.TabIndex = 81;
            Title_HealSelfRatio.Text = "Self Heal Ratio:";
            // 
            // Value_HealSelfRatio
            // 
            Value_HealSelfRatio.AutoSize = true;
            Value_HealSelfRatio.BackColor = Color.Black;
            Value_HealSelfRatio.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_HealSelfRatio.ForeColor = Color.White;
            Value_HealSelfRatio.Location = new Point(195, 163);
            Value_HealSelfRatio.Name = "Value_HealSelfRatio";
            Value_HealSelfRatio.Size = new Size(0, 14);
            Value_HealSelfRatio.TabIndex = 78;
            // 
            // Title_AverageCritHealingDone
            // 
            Title_AverageCritHealingDone.AutoSize = true;
            Title_AverageCritHealingDone.BackColor = Color.Black;
            Title_AverageCritHealingDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_AverageCritHealingDone.ForeColor = Color.White;
            Title_AverageCritHealingDone.Location = new Point(59, 83);
            Title_AverageCritHealingDone.Name = "Title_AverageCritHealingDone";
            Title_AverageCritHealingDone.Size = new Size(129, 14);
            Title_AverageCritHealingDone.TabIndex = 77;
            Title_AverageCritHealingDone.Text = "Average Crit Heal:";
            // 
            // Value_AverageCritHealingDone
            // 
            Value_AverageCritHealingDone.AutoSize = true;
            Value_AverageCritHealingDone.BackColor = Color.Black;
            Value_AverageCritHealingDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_AverageCritHealingDone.ForeColor = Color.White;
            Value_AverageCritHealingDone.Location = new Point(195, 83);
            Value_AverageCritHealingDone.Name = "Value_AverageCritHealingDone";
            Value_AverageCritHealingDone.Size = new Size(0, 14);
            Value_AverageCritHealingDone.TabIndex = 76;
            // 
            // Title_AverageHealingDone
            // 
            Title_AverageHealingDone.AutoSize = true;
            Title_AverageHealingDone.BackColor = Color.Black;
            Title_AverageHealingDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_AverageHealingDone.ForeColor = Color.White;
            Title_AverageHealingDone.Location = new Point(87, 63);
            Title_AverageHealingDone.Name = "Title_AverageHealingDone";
            Title_AverageHealingDone.Size = new Size(101, 14);
            Title_AverageHealingDone.TabIndex = 75;
            Title_AverageHealingDone.Text = "Average Heal:";
            // 
            // Title_AverageCritDamageDone
            // 
            Title_AverageCritDamageDone.AutoSize = true;
            Title_AverageCritDamageDone.BackColor = Color.Black;
            Title_AverageCritDamageDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_AverageCritDamageDone.ForeColor = Color.White;
            Title_AverageCritDamageDone.Location = new Point(325, 83);
            Title_AverageCritDamageDone.Name = "Title_AverageCritDamageDone";
            Title_AverageCritDamageDone.Size = new Size(153, 14);
            Title_AverageCritDamageDone.TabIndex = 74;
            Title_AverageCritDamageDone.Text = "Average Crit Damage:";
            // 
            // Title_AverageDamageDone
            // 
            Title_AverageDamageDone.AutoSize = true;
            Title_AverageDamageDone.BackColor = Color.Black;
            Title_AverageDamageDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Title_AverageDamageDone.ForeColor = Color.White;
            Title_AverageDamageDone.Location = new Point(353, 63);
            Title_AverageDamageDone.Name = "Title_AverageDamageDone";
            Title_AverageDamageDone.Size = new Size(125, 14);
            Title_AverageDamageDone.TabIndex = 73;
            Title_AverageDamageDone.Text = "Average Damage:";
            // 
            // Value_AverageHealingDone
            // 
            Value_AverageHealingDone.AutoSize = true;
            Value_AverageHealingDone.BackColor = Color.Black;
            Value_AverageHealingDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_AverageHealingDone.ForeColor = Color.White;
            Value_AverageHealingDone.Location = new Point(195, 63);
            Value_AverageHealingDone.Name = "Value_AverageHealingDone";
            Value_AverageHealingDone.Size = new Size(0, 14);
            Value_AverageHealingDone.TabIndex = 72;
            // 
            // Value_AverageCritDamageDone
            // 
            Value_AverageCritDamageDone.AutoSize = true;
            Value_AverageCritDamageDone.BackColor = Color.Black;
            Value_AverageCritDamageDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_AverageCritDamageDone.ForeColor = Color.White;
            Value_AverageCritDamageDone.Location = new Point(484, 83);
            Value_AverageCritDamageDone.Name = "Value_AverageCritDamageDone";
            Value_AverageCritDamageDone.Size = new Size(0, 14);
            Value_AverageCritDamageDone.TabIndex = 71;
            // 
            // Value_AverageDamageDone
            // 
            Value_AverageDamageDone.AutoSize = true;
            Value_AverageDamageDone.BackColor = Color.Black;
            Value_AverageDamageDone.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Value_AverageDamageDone.ForeColor = Color.White;
            Value_AverageDamageDone.Location = new Point(485, 63);
            Value_AverageDamageDone.Name = "Value_AverageDamageDone";
            Value_AverageDamageDone.Size = new Size(0, 14);
            Value_AverageDamageDone.TabIndex = 70;
            // 
            // ResetButton
            // 
            ResetButton.BackColor = SystemColors.Control;
            ResetButton.ForeColor = SystemColors.ControlText;
            ResetButton.Location = new Point(242, 25);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(75, 23);
            ResetButton.TabIndex = 4;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // LogDatesComboBox
            // 
            LogDatesComboBox.BackColor = SystemColors.Control;
            LogDatesComboBox.ForeColor = SystemColors.ControlText;
            LogDatesComboBox.FormattingEnabled = true;
            LogDatesComboBox.Location = new Point(6, 25);
            LogDatesComboBox.Name = "LogDatesComboBox";
            LogDatesComboBox.Size = new Size(149, 23);
            LogDatesComboBox.TabIndex = 2;
            LogDatesComboBox.SelectedIndexChanged += LogDatesComboBox_SelectedIndexChange;
            // 
            // ParseButton
            // 
            ParseButton.BackColor = SystemColors.Control;
            ParseButton.ForeColor = SystemColors.ControlText;
            ParseButton.Location = new Point(161, 25);
            ParseButton.Name = "ParseButton";
            ParseButton.Size = new Size(75, 23);
            ParseButton.TabIndex = 1;
            ParseButton.Text = "Start";
            ParseButton.UseVisualStyleBackColor = false;
            ParseButton.Click += ParseButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Black;
            label1.ForeColor = Color.White;
            label1.Location = new Point(6, 7);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 3;
            label1.Text = "Start Location";
            // 
            // FilterPlayersOnlyCheckBox
            // 
            FilterPlayersOnlyCheckBox.AutoSize = true;
            FilterPlayersOnlyCheckBox.Location = new Point(12, 163);
            FilterPlayersOnlyCheckBox.Name = "FilterPlayersOnlyCheckBox";
            FilterPlayersOnlyCheckBox.Size = new Size(91, 19);
            FilterPlayersOnlyCheckBox.TabIndex = 7;
            FilterPlayersOnlyCheckBox.Text = "Players Only";
            FilterPlayersOnlyCheckBox.UseVisualStyleBackColor = true;
            FilterPlayersOnlyCheckBox.CheckedChanged += FilterPlayersOnlyCheckBox_CheckedChanged;
            // 
            // OverlayTransparentCheckBox
            // 
            OverlayTransparentCheckBox.AutoSize = true;
            OverlayTransparentCheckBox.Location = new Point(12, 138);
            OverlayTransparentCheckBox.Name = "OverlayTransparentCheckBox";
            OverlayTransparentCheckBox.Size = new Size(87, 19);
            OverlayTransparentCheckBox.TabIndex = 6;
            OverlayTransparentCheckBox.Text = "Transparent";
            OverlayTransparentCheckBox.UseVisualStyleBackColor = true;
            OverlayTransparentCheckBox.CheckedChanged += OverlayTransparentCheckBox_CheckedChanged;
            // 
            // FontColorPanel
            // 
            FontColorPanel.BorderStyle = BorderStyle.FixedSingle;
            FontColorPanel.Location = new Point(105, 109);
            FontColorPanel.Name = "FontColorPanel";
            FontColorPanel.Size = new Size(23, 23);
            FontColorPanel.TabIndex = 5;
            // 
            // OverLayFontColorButton
            // 
            OverLayFontColorButton.Location = new Point(12, 109);
            OverLayFontColorButton.Name = "OverLayFontColorButton";
            OverLayFontColorButton.Size = new Size(87, 23);
            OverLayFontColorButton.TabIndex = 4;
            OverLayFontColorButton.Text = "Font Color";
            OverLayFontColorButton.UseVisualStyleBackColor = true;
            OverLayFontColorButton.Click += ColorButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 62);
            label2.Name = "label2";
            label2.Size = new Size(91, 15);
            label2.TabIndex = 3;
            label2.Text = "Overlay Opacity";
            // 
            // OverLayOpacityControl
            // 
            OverLayOpacityControl.Location = new Point(12, 80);
            OverLayOpacityControl.Name = "OverLayOpacityControl";
            OverLayOpacityControl.Size = new Size(116, 23);
            OverLayOpacityControl.TabIndex = 2;
            OverLayOpacityControl.ValueChanged += OverLayOpacityControl_ValueChanged;
            // 
            // LockOverlayButton
            // 
            LockOverlayButton.Location = new Point(12, 36);
            LockOverlayButton.Name = "LockOverlayButton";
            LockOverlayButton.Size = new Size(116, 23);
            LockOverlayButton.TabIndex = 1;
            LockOverlayButton.Text = "Lock Overlay";
            LockOverlayButton.UseVisualStyleBackColor = true;
            LockOverlayButton.Click += LockOverlayButton_Click;
            // 
            // OverlayButton
            // 
            OverlayButton.Location = new Point(12, 7);
            OverlayButton.Name = "OverlayButton";
            OverlayButton.Size = new Size(116, 23);
            OverlayButton.TabIndex = 0;
            OverlayButton.Text = "Show Overlay";
            OverlayButton.UseVisualStyleBackColor = true;
            OverlayButton.Click += OverlayButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 398);
            Controls.Add(splitContainer1);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "LogTool v0.1.0";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)OverLayOpacityControl).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button BrowseButton;
        private GroupBox groupBox1;
        private TextBox LogFileTextBox;
        private SplitContainer splitContainer1;
        private Button ParseButton;
        private ComboBox LogDatesComboBox;
        private Label label1;
        private Button ResetButton;
        private Button OverlayButton;
        private Button LockOverlayButton;
        private Label label2;
        private NumericUpDown OverLayOpacityControl;
        private Button OverLayFontColorButton;
        private ColorDialog colorDialog1;
        private Panel FontColorPanel;
        private CheckBox OverlayTransparentCheckBox;
        private CheckBox FilterPlayersOnlyCheckBox;
        public Label Title_IRS;
        public Label Value_IRS;
        public Label Title_DamageDoneBlocked;
        public Label Value_DamageDoneBlocked;
        public Label Title_TotalDamageBlocked;
        public Label Value_TotalDamageBlocked;
        public Label Title_TotalDamageConverted;
        public Label Value_TotalDamageConverted;
        public Label Title_Deaths;
        public Label Value_Deaths;
        public Label Title_DeathBlows;
        public Label Value_DeathBlows;
        public Label Title_TotalDamageTaken;
        public Label Value_TotalDamageTaken;
        public Label Title_DamageTakenAbsorbed;
        public Label Value_DamageTakenAbsorbed;
        public Label Title_DamageAbsorbed;
        public Label Value_DamageAbsorbed;
        public Label Title_CritDamageRatio;
        public Label Value_CritDamageRatio;
        public Label Title_DamageCritRate;
        public Label Value_DamageCritRate;
        public Label Title_RealmPoints;
        public Label Value_RealmPoints;
        public Label Title_TotalDamageDone;
        public Label Value_TotalDamageDone;
        public Label Title_TotalHealingRecieved;
        public Label Value_TotalHealingRecieved;
        public Label Title_TotalHealingDone;
        public Label Value_TotalHealingDone;
        public Label Value_HealCritRate;
        public Label Title_HealCritRate;
        public Label Title_CritHealRatio;
        public Label Value_CritHealRatio;
        public Label Title_HealSelfRatio;
        public Label Value_HealSelfRatio;
        public Label Title_AverageCritHealingDone;
        public Label Value_AverageCritHealingDone;
        public Label Title_AverageHealingDone;
        public Label Title_AverageCritDamageDone;
        public Label Title_AverageDamageDone;
        public Label Value_AverageHealingDone;
        public Label Value_AverageCritDamageDone;
        public Label Value_AverageDamageDone;
    }
}