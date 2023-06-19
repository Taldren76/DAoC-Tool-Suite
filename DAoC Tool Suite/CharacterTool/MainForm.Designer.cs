using System.Windows.Forms;

namespace DAoCToolSuite.CharacterTool
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DAoCDirectoryPanel = new Panel();
            DAoCDirectoryButton = new Button();
            DAoCDirectoryTextBox = new TextBox();
            DAoCDirectoryLabel = new Label();
            TabPanel = new Panel();
            DAoCTabControl = new TabControl();
            copySettingsTab = new TabPage();
            panel2 = new Panel();
            CustomSettingPanel = new Panel();
            CustomSettingsCheckListBox = new CheckedListBox();
            CustomSettingsCheckBox = new RadioButton();
            AllSettingsCheckBox = new RadioButton();
            panel1 = new Panel();
            AllCharacterPanel = new Panel();
            AllCharactersText = new Label();
            AllCharactersSaveButton = new Button();
            AllCharactersCheckBox = new RadioButton();
            NewCharacterPanel = new Panel();
            NewCharacterSaveButton = new Button();
            NewCharacterServerComboBox = new ComboBox();
            NewCharacterNameTextBox = new TextBox();
            ServerComboBox = new Label();
            CharacterNameLabel = new Label();
            NewCharacterCheckBox = new RadioButton();
            ExistingCharacterCheckBox = new RadioButton();
            ExistingCharacterPanel = new Panel();
            CopyToLabel2 = new Label();
            CopyToTextBox = new TextBox();
            ExistingCharacterSaveButton = new Button();
            CopyToComboBox = new ComboBox();
            CopyToLabel1 = new Label();
            CopyLabel2 = new Label();
            FromServerText = new TextBox();
            CopyLabel1 = new Label();
            CopyFromComboBox = new ComboBox();
            RefreshButton = new Button();
            backUpCharacterTab = new TabPage();
            panel6 = new Panel();
            SaveAllProgressBar = new TextProgressBar();
            panel5 = new Panel();
            BackUpDescriptionTextBox = new TextBox();
            BackupLabel = new Label();
            SaveBackUp = new Button();
            BackUpCharRefreshButton = new Button();
            BackUpDescriptionLabel = new Label();
            BackUpServerTextBox = new TextBox();
            BackUpNameComboBox = new ComboBox();
            BackUpClassLabel = new Label();
            BackUpServerLabel = new Label();
            BackUpClassComboBox = new ComboBox();
            BackUpRealmComboBox = new ComboBox();
            BackupRealmLabel = new Label();
            panel4 = new Panel();
            WarningLabel = new Label();
            SaveAllButton = new Button();
            restoreCharacterTab = new TabPage();
            RestoreDeleteProgressBar = new TextProgressBar();
            EditDescriptionButton = new Button();
            RestoreDBButton = new Button();
            BackupDBButton = new Button();
            RestoreNameFilterComboBox = new ComboBox();
            RestoreNameFilterLabel = new Label();
            panel3 = new Panel();
            restoreDataGridView = new DataGridView();
            ClearFilterButton = new Button();
            RestoreDeleteSettingsButton = new Button();
            RestoreRestoreSettingsButton = new Button();
            RestoreClassLabel = new Label();
            RestoreClassComboBox = new ComboBox();
            RestoreRealmLabel = new Label();
            RestoreRealmComboBox = new ComboBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            DAoCDirectoryPanel.SuspendLayout();
            TabPanel.SuspendLayout();
            DAoCTabControl.SuspendLayout();
            copySettingsTab.SuspendLayout();
            panel2.SuspendLayout();
            CustomSettingPanel.SuspendLayout();
            panel1.SuspendLayout();
            AllCharacterPanel.SuspendLayout();
            NewCharacterPanel.SuspendLayout();
            ExistingCharacterPanel.SuspendLayout();
            backUpCharacterTab.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            restoreCharacterTab.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)restoreDataGridView).BeginInit();
            SuspendLayout();
            // 
            // DAoCDirectoryPanel
            // 
            DAoCDirectoryPanel.Controls.Add(DAoCDirectoryButton);
            DAoCDirectoryPanel.Controls.Add(DAoCDirectoryTextBox);
            DAoCDirectoryPanel.Controls.Add(DAoCDirectoryLabel);
            DAoCDirectoryPanel.Location = new Point(12, 12);
            DAoCDirectoryPanel.Name = "DAoCDirectoryPanel";
            DAoCDirectoryPanel.Size = new Size(776, 56);
            DAoCDirectoryPanel.TabIndex = 0;
            // 
            // DAoCDirectoryButton
            // 
            DAoCDirectoryButton.Location = new Point(686, 14);
            DAoCDirectoryButton.Name = "DAoCDirectoryButton";
            DAoCDirectoryButton.Size = new Size(75, 23);
            DAoCDirectoryButton.TabIndex = 2;
            DAoCDirectoryButton.Text = "Browse";
            DAoCDirectoryButton.UseVisualStyleBackColor = true;
            DAoCDirectoryButton.Click += DAoCDirectoryButton_Click;
            // 
            // DAoCDirectoryTextBox
            // 
            DAoCDirectoryTextBox.Location = new Point(139, 14);
            DAoCDirectoryTextBox.Name = "DAoCDirectoryTextBox";
            DAoCDirectoryTextBox.Size = new Size(541, 23);
            DAoCDirectoryTextBox.TabIndex = 1;
            DAoCDirectoryTextBox.Text = "%AppData%\\Electronic Arts\\Dark Age of Camelot";
            DAoCDirectoryTextBox.TextChanged += DAoCDirectoryTextBox_TextChanged;
            // 
            // DAoCDirectoryLabel
            // 
            DAoCDirectoryLabel.AutoSize = true;
            DAoCDirectoryLabel.Location = new Point(7, 17);
            DAoCDirectoryLabel.Name = "DAoCDirectoryLabel";
            DAoCDirectoryLabel.Size = new Size(126, 15);
            DAoCDirectoryLabel.TabIndex = 0;
            DAoCDirectoryLabel.Text = "DAoC AppData Folder:";
            // 
            // TabPanel
            // 
            TabPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TabPanel.Controls.Add(DAoCTabControl);
            TabPanel.Location = new Point(12, 74);
            TabPanel.Name = "TabPanel";
            TabPanel.Size = new Size(776, 364);
            TabPanel.TabIndex = 1;
            // 
            // DAoCTabControl
            // 
            DAoCTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DAoCTabControl.Controls.Add(copySettingsTab);
            DAoCTabControl.Controls.Add(backUpCharacterTab);
            DAoCTabControl.Controls.Add(restoreCharacterTab);
            DAoCTabControl.Location = new Point(4, 3);
            DAoCTabControl.Name = "DAoCTabControl";
            DAoCTabControl.SelectedIndex = 0;
            DAoCTabControl.Size = new Size(770, 358);
            DAoCTabControl.TabIndex = 0;
            DAoCTabControl.Selected += DAoCTabControl_SelectedIndexChange;
            // 
            // copySettingsTab
            // 
            copySettingsTab.Controls.Add(panel2);
            copySettingsTab.Controls.Add(panel1);
            copySettingsTab.Location = new Point(4, 24);
            copySettingsTab.Name = "copySettingsTab";
            copySettingsTab.Padding = new Padding(3);
            copySettingsTab.Size = new Size(762, 330);
            copySettingsTab.TabIndex = 0;
            copySettingsTab.Text = "Copy Settings";
            copySettingsTab.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(CustomSettingPanel);
            panel2.Controls.Add(CustomSettingsCheckBox);
            panel2.Controls.Add(AllSettingsCheckBox);
            panel2.Location = new Point(607, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(149, 318);
            panel2.TabIndex = 9;
            // 
            // CustomSettingPanel
            // 
            CustomSettingPanel.Controls.Add(CustomSettingsCheckListBox);
            CustomSettingPanel.Enabled = false;
            CustomSettingPanel.Location = new Point(3, 52);
            CustomSettingPanel.Name = "CustomSettingPanel";
            CustomSettingPanel.Size = new Size(143, 263);
            CustomSettingPanel.TabIndex = 11;
            // 
            // CustomSettingsCheckListBox
            // 
            CustomSettingsCheckListBox.CheckOnClick = true;
            CustomSettingsCheckListBox.Enabled = false;
            CustomSettingsCheckListBox.ForeColor = SystemColors.WindowText;
            CustomSettingsCheckListBox.FormattingEnabled = true;
            CustomSettingsCheckListBox.Items.AddRange(new object[] { "Chat (Colors)", "Panels (UI)", "Camera", "QuickBinds", "Macros", "NameOptions", "ToolTips" });
            CustomSettingsCheckListBox.Location = new Point(12, 3);
            CustomSettingsCheckListBox.Name = "CustomSettingsCheckListBox";
            CustomSettingsCheckListBox.Size = new Size(118, 130);
            CustomSettingsCheckListBox.TabIndex = 11;
            // 
            // CustomSettingsCheckBox
            // 
            CustomSettingsCheckBox.AutoSize = true;
            CustomSettingsCheckBox.Location = new Point(3, 27);
            CustomSettingsCheckBox.Name = "CustomSettingsCheckBox";
            CustomSettingsCheckBox.Size = new Size(67, 19);
            CustomSettingsCheckBox.TabIndex = 9;
            CustomSettingsCheckBox.Text = "Custom";
            CustomSettingsCheckBox.UseVisualStyleBackColor = true;
            CustomSettingsCheckBox.CheckedChanged += CustomSettingsCheckBox_CheckedChanged;
            // 
            // AllSettingsCheckBox
            // 
            AllSettingsCheckBox.AutoSize = true;
            AllSettingsCheckBox.Checked = true;
            AllSettingsCheckBox.Location = new Point(3, 2);
            AllSettingsCheckBox.Name = "AllSettingsCheckBox";
            AllSettingsCheckBox.Size = new Size(84, 19);
            AllSettingsCheckBox.TabIndex = 8;
            AllSettingsCheckBox.TabStop = true;
            AllSettingsCheckBox.Text = "All Settings";
            AllSettingsCheckBox.UseVisualStyleBackColor = true;
            AllSettingsCheckBox.CheckedChanged += AllSettingsCheckBox_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(AllCharacterPanel);
            panel1.Controls.Add(AllCharactersCheckBox);
            panel1.Controls.Add(NewCharacterPanel);
            panel1.Controls.Add(NewCharacterCheckBox);
            panel1.Controls.Add(ExistingCharacterCheckBox);
            panel1.Controls.Add(ExistingCharacterPanel);
            panel1.Controls.Add(CopyLabel2);
            panel1.Controls.Add(FromServerText);
            panel1.Controls.Add(CopyLabel1);
            panel1.Controls.Add(CopyFromComboBox);
            panel1.Controls.Add(RefreshButton);
            panel1.Location = new Point(6, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(595, 318);
            panel1.TabIndex = 8;
            // 
            // AllCharacterPanel
            // 
            AllCharacterPanel.BorderStyle = BorderStyle.FixedSingle;
            AllCharacterPanel.Controls.Add(AllCharactersText);
            AllCharacterPanel.Controls.Add(AllCharactersSaveButton);
            AllCharacterPanel.Enabled = false;
            AllCharacterPanel.Location = new Point(6, 255);
            AllCharacterPanel.Name = "AllCharacterPanel";
            AllCharacterPanel.Size = new Size(582, 56);
            AllCharacterPanel.TabIndex = 15;
            // 
            // AllCharactersText
            // 
            AllCharactersText.AutoSize = true;
            AllCharactersText.ForeColor = Color.Red;
            AllCharactersText.Location = new Point(4, 19);
            AllCharactersText.Name = "AllCharactersText";
            AllCharactersText.Size = new Size(392, 15);
            AllCharactersText.TabIndex = 6;
            AllCharactersText.Text = "WARNING: This action is not reversible. Be VERY sure you want to do this.";
            // 
            // AllCharactersSaveButton
            // 
            AllCharactersSaveButton.Location = new Point(499, 15);
            AllCharactersSaveButton.Name = "AllCharactersSaveButton";
            AllCharactersSaveButton.Size = new Size(75, 23);
            AllCharactersSaveButton.TabIndex = 5;
            AllCharactersSaveButton.Text = "Save";
            AllCharactersSaveButton.UseVisualStyleBackColor = true;
            AllCharactersSaveButton.Click += AllCharactersSaveButton_Click;
            // 
            // AllCharactersCheckBox
            // 
            AllCharactersCheckBox.AutoSize = true;
            AllCharactersCheckBox.Location = new Point(6, 230);
            AllCharactersCheckBox.Name = "AllCharactersCheckBox";
            AllCharactersCheckBox.Size = new Size(98, 19);
            AllCharactersCheckBox.TabIndex = 14;
            AllCharactersCheckBox.TabStop = true;
            AllCharactersCheckBox.Text = "All Characters";
            AllCharactersCheckBox.UseVisualStyleBackColor = true;
            AllCharactersCheckBox.CheckedChanged += AllCharactersCheckBox_CheckedChanged;
            // 
            // NewCharacterPanel
            // 
            NewCharacterPanel.BorderStyle = BorderStyle.FixedSingle;
            NewCharacterPanel.Controls.Add(NewCharacterSaveButton);
            NewCharacterPanel.Controls.Add(NewCharacterServerComboBox);
            NewCharacterPanel.Controls.Add(NewCharacterNameTextBox);
            NewCharacterPanel.Controls.Add(ServerComboBox);
            NewCharacterPanel.Controls.Add(CharacterNameLabel);
            NewCharacterPanel.Enabled = false;
            NewCharacterPanel.ForeColor = SystemColors.WindowText;
            NewCharacterPanel.Location = new Point(6, 145);
            NewCharacterPanel.Name = "NewCharacterPanel";
            NewCharacterPanel.Size = new Size(582, 79);
            NewCharacterPanel.TabIndex = 13;
            // 
            // NewCharacterSaveButton
            // 
            NewCharacterSaveButton.Location = new Point(499, 10);
            NewCharacterSaveButton.Name = "NewCharacterSaveButton";
            NewCharacterSaveButton.Size = new Size(75, 23);
            NewCharacterSaveButton.TabIndex = 4;
            NewCharacterSaveButton.Text = "Save";
            NewCharacterSaveButton.UseVisualStyleBackColor = true;
            NewCharacterSaveButton.Click += NewCharacterSaveButton_Click;
            // 
            // NewCharacterServerComboBox
            // 
            NewCharacterServerComboBox.FormattingEnabled = true;
            NewCharacterServerComboBox.Location = new Point(101, 46);
            NewCharacterServerComboBox.Name = "NewCharacterServerComboBox";
            NewCharacterServerComboBox.Size = new Size(134, 23);
            NewCharacterServerComboBox.TabIndex = 3;
            // 
            // NewCharacterNameTextBox
            // 
            NewCharacterNameTextBox.Location = new Point(101, 10);
            NewCharacterNameTextBox.Name = "NewCharacterNameTextBox";
            NewCharacterNameTextBox.Size = new Size(262, 23);
            NewCharacterNameTextBox.TabIndex = 2;
            // 
            // ServerComboBox
            // 
            ServerComboBox.AutoSize = true;
            ServerComboBox.Location = new Point(53, 49);
            ServerComboBox.Name = "ServerComboBox";
            ServerComboBox.Size = new Size(42, 15);
            ServerComboBox.TabIndex = 1;
            ServerComboBox.Text = "Server:";
            // 
            // CharacterNameLabel
            // 
            CharacterNameLabel.AutoSize = true;
            CharacterNameLabel.Location = new Point(3, 13);
            CharacterNameLabel.Name = "CharacterNameLabel";
            CharacterNameLabel.Size = new Size(96, 15);
            CharacterNameLabel.TabIndex = 0;
            CharacterNameLabel.Text = "Character Name:";
            // 
            // NewCharacterCheckBox
            // 
            NewCharacterCheckBox.AutoSize = true;
            NewCharacterCheckBox.Location = new Point(6, 120);
            NewCharacterCheckBox.Name = "NewCharacterCheckBox";
            NewCharacterCheckBox.Size = new Size(103, 19);
            NewCharacterCheckBox.TabIndex = 12;
            NewCharacterCheckBox.Text = "New Character";
            NewCharacterCheckBox.UseVisualStyleBackColor = true;
            NewCharacterCheckBox.CheckedChanged += NewCharacterCheckBox_CheckedChanged;
            // 
            // ExistingCharacterCheckBox
            // 
            ExistingCharacterCheckBox.AutoSize = true;
            ExistingCharacterCheckBox.Checked = true;
            ExistingCharacterCheckBox.Location = new Point(6, 41);
            ExistingCharacterCheckBox.Name = "ExistingCharacterCheckBox";
            ExistingCharacterCheckBox.Size = new Size(120, 19);
            ExistingCharacterCheckBox.TabIndex = 11;
            ExistingCharacterCheckBox.TabStop = true;
            ExistingCharacterCheckBox.Text = "Existing Character";
            ExistingCharacterCheckBox.UseVisualStyleBackColor = true;
            ExistingCharacterCheckBox.CheckedChanged += ExistingCharacterCheckBox_CheckedChanged;
            // 
            // ExistingCharacterPanel
            // 
            ExistingCharacterPanel.BorderStyle = BorderStyle.FixedSingle;
            ExistingCharacterPanel.Controls.Add(CopyToLabel2);
            ExistingCharacterPanel.Controls.Add(CopyToTextBox);
            ExistingCharacterPanel.Controls.Add(ExistingCharacterSaveButton);
            ExistingCharacterPanel.Controls.Add(CopyToComboBox);
            ExistingCharacterPanel.Controls.Add(CopyToLabel1);
            ExistingCharacterPanel.Location = new Point(6, 66);
            ExistingCharacterPanel.Name = "ExistingCharacterPanel";
            ExistingCharacterPanel.Size = new Size(582, 48);
            ExistingCharacterPanel.TabIndex = 10;
            // 
            // CopyToLabel2
            // 
            CopyToLabel2.AutoSize = true;
            CopyToLabel2.Location = new Point(318, 15);
            CopyToLabel2.Name = "CopyToLabel2";
            CopyToLabel2.Size = new Size(42, 15);
            CopyToLabel2.TabIndex = 12;
            CopyToLabel2.Text = "Server:";
            // 
            // CopyToTextBox
            // 
            CopyToTextBox.Location = new Point(366, 12);
            CopyToTextBox.Name = "CopyToTextBox";
            CopyToTextBox.ReadOnly = true;
            CopyToTextBox.Size = new Size(127, 23);
            CopyToTextBox.TabIndex = 11;
            // 
            // ExistingCharacterSaveButton
            // 
            ExistingCharacterSaveButton.Location = new Point(499, 11);
            ExistingCharacterSaveButton.Name = "ExistingCharacterSaveButton";
            ExistingCharacterSaveButton.Size = new Size(75, 23);
            ExistingCharacterSaveButton.TabIndex = 10;
            ExistingCharacterSaveButton.Text = "Save";
            ExistingCharacterSaveButton.UseVisualStyleBackColor = true;
            ExistingCharacterSaveButton.Click += ExistingCharacterSaveButton_Click;
            // 
            // CopyToComboBox
            // 
            CopyToComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CopyToComboBox.FormattingEnabled = true;
            CopyToComboBox.Location = new Point(75, 12);
            CopyToComboBox.Name = "CopyToComboBox";
            CopyToComboBox.Size = new Size(240, 23);
            CopyToComboBox.TabIndex = 1;
            CopyToComboBox.SelectedIndexChanged += CopyToComboBox_SelectedIndexChanged;
            // 
            // CopyToLabel1
            // 
            CopyToLabel1.AutoSize = true;
            CopyToLabel1.Location = new Point(3, 15);
            CopyToLabel1.Name = "CopyToLabel1";
            CopyToLabel1.Size = new Size(53, 15);
            CopyToLabel1.TabIndex = 0;
            CopyToLabel1.Text = "Copy To:";
            // 
            // CopyLabel2
            // 
            CopyLabel2.AutoSize = true;
            CopyLabel2.Location = new Point(327, 15);
            CopyLabel2.Name = "CopyLabel2";
            CopyLabel2.Size = new Size(42, 15);
            CopyLabel2.TabIndex = 9;
            CopyLabel2.Text = "Server:";
            // 
            // FromServerText
            // 
            FromServerText.Location = new Point(375, 12);
            FromServerText.Name = "FromServerText";
            FromServerText.ReadOnly = true;
            FromServerText.Size = new Size(127, 23);
            FromServerText.TabIndex = 8;
            FromServerText.TextChanged += FromServerText_TextChanged;
            // 
            // CopyLabel1
            // 
            CopyLabel1.AutoSize = true;
            CopyLabel1.Location = new Point(6, 15);
            CopyLabel1.Name = "CopyLabel1";
            CopyLabel1.Size = new Size(69, 15);
            CopyLabel1.TabIndex = 7;
            CopyLabel1.Text = "Copy From:";
            // 
            // CopyFromComboBox
            // 
            CopyFromComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CopyFromComboBox.FormattingEnabled = true;
            CopyFromComboBox.Location = new Point(81, 12);
            CopyFromComboBox.Name = "CopyFromComboBox";
            CopyFromComboBox.Size = new Size(240, 23);
            CopyFromComboBox.TabIndex = 6;
            CopyFromComboBox.SelectedIndexChanged += CopyFromComboBox_SelectedIndexChanged;
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(508, 11);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(75, 23);
            RefreshButton.TabIndex = 5;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // backUpCharacterTab
            // 
            backUpCharacterTab.Controls.Add(panel6);
            backUpCharacterTab.Location = new Point(4, 24);
            backUpCharacterTab.Name = "backUpCharacterTab";
            backUpCharacterTab.Padding = new Padding(3);
            backUpCharacterTab.Size = new Size(762, 330);
            backUpCharacterTab.TabIndex = 1;
            backUpCharacterTab.Text = "Character Backup";
            backUpCharacterTab.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel6.BorderStyle = BorderStyle.Fixed3D;
            panel6.Controls.Add(SaveAllProgressBar);
            panel6.Controls.Add(panel5);
            panel6.Controls.Add(panel4);
            panel6.Location = new Point(6, 6);
            panel6.Name = "panel6";
            panel6.Size = new Size(750, 318);
            panel6.TabIndex = 24;
            // 
            // SaveAllProgressBar
            // 
            SaveAllProgressBar.CustomText = "";
            SaveAllProgressBar.ForeColor = Color.Lime;
            SaveAllProgressBar.Location = new Point(6, 265);
            SaveAllProgressBar.Name = "SaveAllProgressBar";
            SaveAllProgressBar.ProgressColor = Color.Chartreuse;
            SaveAllProgressBar.Size = new Size(737, 23);
            SaveAllProgressBar.TabIndex = 25;
            SaveAllProgressBar.TextColor = Color.Black;
            SaveAllProgressBar.TextFont = new Font("Verdana", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            SaveAllProgressBar.Visible = false;
            SaveAllProgressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(BackUpDescriptionTextBox);
            panel5.Controls.Add(BackupLabel);
            panel5.Controls.Add(SaveBackUp);
            panel5.Controls.Add(BackUpCharRefreshButton);
            panel5.Controls.Add(BackUpDescriptionLabel);
            panel5.Controls.Add(BackUpServerTextBox);
            panel5.Controls.Add(BackUpNameComboBox);
            panel5.Controls.Add(BackUpClassLabel);
            panel5.Controls.Add(BackUpServerLabel);
            panel5.Controls.Add(BackUpClassComboBox);
            panel5.Controls.Add(BackUpRealmComboBox);
            panel5.Controls.Add(BackupRealmLabel);
            panel5.Location = new Point(6, 9);
            panel5.Name = "panel5";
            panel5.Size = new Size(737, 165);
            panel5.TabIndex = 24;
            // 
            // BackUpDescriptionTextBox
            // 
            BackUpDescriptionTextBox.Location = new Point(12, 122);
            BackUpDescriptionTextBox.Name = "BackUpDescriptionTextBox";
            BackUpDescriptionTextBox.Size = new Size(639, 23);
            BackUpDescriptionTextBox.TabIndex = 19;
            // 
            // BackupLabel
            // 
            BackupLabel.AutoSize = true;
            BackupLabel.Location = new Point(12, 12);
            BackupLabel.Name = "BackupLabel";
            BackupLabel.Size = new Size(49, 15);
            BackupLabel.TabIndex = 12;
            BackupLabel.Text = "Backup:";
            // 
            // SaveBackUp
            // 
            SaveBackUp.Location = new Point(657, 122);
            SaveBackUp.Name = "SaveBackUp";
            SaveBackUp.Size = new Size(75, 23);
            SaveBackUp.TabIndex = 21;
            SaveBackUp.Text = "Save";
            SaveBackUp.UseVisualStyleBackColor = true;
            SaveBackUp.Click += SaveBackUp_Click;
            // 
            // BackUpCharRefreshButton
            // 
            BackUpCharRefreshButton.Location = new Point(451, 29);
            BackUpCharRefreshButton.Name = "BackUpCharRefreshButton";
            BackUpCharRefreshButton.Size = new Size(75, 23);
            BackUpCharRefreshButton.TabIndex = 10;
            BackUpCharRefreshButton.Text = "Refresh";
            BackUpCharRefreshButton.UseVisualStyleBackColor = true;
            // 
            // BackUpDescriptionLabel
            // 
            BackUpDescriptionLabel.AutoSize = true;
            BackUpDescriptionLabel.Location = new Point(12, 104);
            BackUpDescriptionLabel.Name = "BackUpDescriptionLabel";
            BackUpDescriptionLabel.Size = new Size(70, 15);
            BackUpDescriptionLabel.TabIndex = 20;
            BackUpDescriptionLabel.Text = "Description:";
            // 
            // BackUpServerTextBox
            // 
            BackUpServerTextBox.Location = new Point(286, 30);
            BackUpServerTextBox.Name = "BackUpServerTextBox";
            BackUpServerTextBox.ReadOnly = true;
            BackUpServerTextBox.Size = new Size(159, 23);
            BackUpServerTextBox.TabIndex = 13;
            // 
            // BackUpNameComboBox
            // 
            BackUpNameComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            BackUpNameComboBox.FormattingEnabled = true;
            BackUpNameComboBox.Location = new Point(12, 30);
            BackUpNameComboBox.Name = "BackUpNameComboBox";
            BackUpNameComboBox.Size = new Size(240, 23);
            BackUpNameComboBox.TabIndex = 11;
            BackUpNameComboBox.SelectedIndexChanged += BackUpNameComboBox_SelectedIndexChanged;
            // 
            // BackUpClassLabel
            // 
            BackUpClassLabel.AutoSize = true;
            BackUpClassLabel.Location = new Point(286, 56);
            BackUpClassLabel.Name = "BackUpClassLabel";
            BackUpClassLabel.Size = new Size(37, 15);
            BackUpClassLabel.TabIndex = 18;
            BackUpClassLabel.Text = "Class:";
            // 
            // BackUpServerLabel
            // 
            BackUpServerLabel.AutoSize = true;
            BackUpServerLabel.Location = new Point(286, 12);
            BackUpServerLabel.Name = "BackUpServerLabel";
            BackUpServerLabel.Size = new Size(42, 15);
            BackUpServerLabel.TabIndex = 14;
            BackUpServerLabel.Text = "Server:";
            // 
            // BackUpClassComboBox
            // 
            BackUpClassComboBox.FormattingEnabled = true;
            BackUpClassComboBox.Location = new Point(286, 78);
            BackUpClassComboBox.Name = "BackUpClassComboBox";
            BackUpClassComboBox.Size = new Size(240, 23);
            BackUpClassComboBox.TabIndex = 17;
            // 
            // BackUpRealmComboBox
            // 
            BackUpRealmComboBox.FormattingEnabled = true;
            BackUpRealmComboBox.Location = new Point(12, 78);
            BackUpRealmComboBox.Name = "BackUpRealmComboBox";
            BackUpRealmComboBox.Size = new Size(240, 23);
            BackUpRealmComboBox.TabIndex = 15;
            BackUpRealmComboBox.SelectedIndexChanged += BackUpRealmComboBox_SelectedIndexChanged;
            // 
            // BackupRealmLabel
            // 
            BackupRealmLabel.AutoSize = true;
            BackupRealmLabel.Location = new Point(12, 56);
            BackupRealmLabel.Name = "BackupRealmLabel";
            BackupRealmLabel.Size = new Size(43, 15);
            BackupRealmLabel.TabIndex = 16;
            BackupRealmLabel.Text = "Realm:";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(WarningLabel);
            panel4.Controls.Add(SaveAllButton);
            panel4.Location = new Point(6, 180);
            panel4.Name = "panel4";
            panel4.Size = new Size(737, 79);
            panel4.TabIndex = 23;
            // 
            // WarningLabel
            // 
            WarningLabel.ForeColor = Color.Red;
            WarningLabel.Location = new Point(3, 6);
            WarningLabel.Name = "WarningLabel";
            WarningLabel.Size = new Size(654, 66);
            WarningLabel.TabIndex = 9;
            WarningLabel.Text = resources.GetString("WarningLabel.Text");
            // 
            // SaveAllButton
            // 
            SaveAllButton.Location = new Point(657, 6);
            SaveAllButton.Name = "SaveAllButton";
            SaveAllButton.Size = new Size(75, 23);
            SaveAllButton.TabIndex = 7;
            SaveAllButton.Text = "Save All";
            SaveAllButton.UseVisualStyleBackColor = true;
            SaveAllButton.Click += SaveAll_Click;
            // 
            // restoreCharacterTab
            // 
            restoreCharacterTab.Controls.Add(RestoreDeleteProgressBar);
            restoreCharacterTab.Controls.Add(EditDescriptionButton);
            restoreCharacterTab.Controls.Add(RestoreDBButton);
            restoreCharacterTab.Controls.Add(BackupDBButton);
            restoreCharacterTab.Controls.Add(RestoreNameFilterComboBox);
            restoreCharacterTab.Controls.Add(RestoreNameFilterLabel);
            restoreCharacterTab.Controls.Add(panel3);
            restoreCharacterTab.Controls.Add(ClearFilterButton);
            restoreCharacterTab.Controls.Add(RestoreDeleteSettingsButton);
            restoreCharacterTab.Controls.Add(RestoreRestoreSettingsButton);
            restoreCharacterTab.Controls.Add(RestoreClassLabel);
            restoreCharacterTab.Controls.Add(RestoreClassComboBox);
            restoreCharacterTab.Controls.Add(RestoreRealmLabel);
            restoreCharacterTab.Controls.Add(RestoreRealmComboBox);
            restoreCharacterTab.Location = new Point(4, 24);
            restoreCharacterTab.Name = "restoreCharacterTab";
            restoreCharacterTab.Padding = new Padding(3);
            restoreCharacterTab.Size = new Size(762, 330);
            restoreCharacterTab.TabIndex = 2;
            restoreCharacterTab.Text = "Character Restore";
            restoreCharacterTab.UseVisualStyleBackColor = true;
            // 
            // RestoreDeleteProgressBar
            // 
            RestoreDeleteProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RestoreDeleteProgressBar.CustomText = "";
            RestoreDeleteProgressBar.ForeColor = Color.Lime;
            RestoreDeleteProgressBar.Location = new Point(168, 300);
            RestoreDeleteProgressBar.Name = "RestoreDeleteProgressBar";
            RestoreDeleteProgressBar.ProgressColor = Color.Chartreuse;
            RestoreDeleteProgressBar.Size = new Size(220, 23);
            RestoreDeleteProgressBar.TabIndex = 33;
            RestoreDeleteProgressBar.TextColor = Color.Black;
            RestoreDeleteProgressBar.TextFont = new Font("Verdana", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            RestoreDeleteProgressBar.Visible = false;
            RestoreDeleteProgressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
            // 
            // EditDescriptionButton
            // 
            EditDescriptionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            EditDescriptionButton.Location = new Point(525, 300);
            EditDescriptionButton.Name = "EditDescriptionButton";
            EditDescriptionButton.Size = new Size(125, 23);
            EditDescriptionButton.TabIndex = 32;
            EditDescriptionButton.Text = "Edit Record";
            EditDescriptionButton.UseVisualStyleBackColor = true;
            EditDescriptionButton.Click += EditDescriptionButton_Click;
            // 
            // RestoreDBButton
            // 
            RestoreDBButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            RestoreDBButton.Location = new Point(87, 300);
            RestoreDBButton.Name = "RestoreDBButton";
            RestoreDBButton.Size = new Size(75, 23);
            RestoreDBButton.TabIndex = 31;
            RestoreDBButton.Text = "Restore DB";
            RestoreDBButton.UseVisualStyleBackColor = true;
            RestoreDBButton.Click += RestoreDBButton_Click;
            // 
            // BackupDBButton
            // 
            BackupDBButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BackupDBButton.Location = new Point(6, 300);
            BackupDBButton.Name = "BackupDBButton";
            BackupDBButton.Size = new Size(75, 23);
            BackupDBButton.TabIndex = 30;
            BackupDBButton.Text = "Backup DB";
            BackupDBButton.UseVisualStyleBackColor = true;
            BackupDBButton.Click += BackupDBButton_Click;
            // 
            // RestoreNameFilterComboBox
            // 
            RestoreNameFilterComboBox.FormattingEnabled = true;
            RestoreNameFilterComboBox.Location = new Point(50, 6);
            RestoreNameFilterComboBox.Name = "RestoreNameFilterComboBox";
            RestoreNameFilterComboBox.Size = new Size(150, 23);
            RestoreNameFilterComboBox.TabIndex = 29;
            RestoreNameFilterComboBox.SelectedIndexChanged += RestoreNameFilterComboBox_SelectedIndexChanged;
            // 
            // RestoreNameFilterLabel
            // 
            RestoreNameFilterLabel.AutoSize = true;
            RestoreNameFilterLabel.Location = new Point(5, 10);
            RestoreNameFilterLabel.Name = "RestoreNameFilterLabel";
            RestoreNameFilterLabel.Size = new Size(39, 15);
            RestoreNameFilterLabel.TabIndex = 28;
            RestoreNameFilterLabel.Text = "Name";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel3.Controls.Add(restoreDataGridView);
            panel3.Location = new Point(0, 35);
            panel3.Name = "panel3";
            panel3.Size = new Size(762, 259);
            panel3.TabIndex = 27;
            // 
            // restoreDataGridView
            // 
            restoreDataGridView.AllowUserToAddRows = false;
            restoreDataGridView.AllowUserToDeleteRows = false;
            restoreDataGridView.AllowUserToResizeColumns = false;
            restoreDataGridView.AllowUserToResizeRows = false;
            restoreDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            restoreDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            restoreDataGridView.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            restoreDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            restoreDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.NullValue = "null";
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            restoreDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            restoreDataGridView.Dock = DockStyle.Fill;
            restoreDataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            restoreDataGridView.Location = new Point(0, 0);
            restoreDataGridView.Margin = new Padding(4, 3, 4, 3);
            restoreDataGridView.Name = "restoreDataGridView";
            restoreDataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            restoreDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            restoreDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            restoreDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            restoreDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            restoreDataGridView.ShowCellErrors = false;
            restoreDataGridView.ShowCellToolTips = false;
            restoreDataGridView.ShowEditingIcon = false;
            restoreDataGridView.ShowRowErrors = false;
            restoreDataGridView.Size = new Size(762, 259);
            restoreDataGridView.TabIndex = 23;
            restoreDataGridView.DataSourceChanged += RestoreDataGridView_DataSourceChanged;
            // 
            // ClearFilterButton
            // 
            ClearFilterButton.Location = new Point(667, 6);
            ClearFilterButton.Name = "ClearFilterButton";
            ClearFilterButton.Size = new Size(87, 23);
            ClearFilterButton.TabIndex = 26;
            ClearFilterButton.Text = "Clear Filters";
            ClearFilterButton.UseVisualStyleBackColor = true;
            ClearFilterButton.Click += ClearFilterButton_Click;
            // 
            // RestoreDeleteSettingsButton
            // 
            RestoreDeleteSettingsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            RestoreDeleteSettingsButton.Location = new Point(656, 300);
            RestoreDeleteSettingsButton.Name = "RestoreDeleteSettingsButton";
            RestoreDeleteSettingsButton.Size = new Size(100, 23);
            RestoreDeleteSettingsButton.TabIndex = 25;
            RestoreDeleteSettingsButton.Text = "Delete Record";
            RestoreDeleteSettingsButton.UseVisualStyleBackColor = true;
            RestoreDeleteSettingsButton.Click += RestoreDeleteRecordButton_Click;
            // 
            // RestoreRestoreSettingsButton
            // 
            RestoreRestoreSettingsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            RestoreRestoreSettingsButton.Location = new Point(394, 300);
            RestoreRestoreSettingsButton.Name = "RestoreRestoreSettingsButton";
            RestoreRestoreSettingsButton.Size = new Size(125, 23);
            RestoreRestoreSettingsButton.TabIndex = 24;
            RestoreRestoreSettingsButton.Text = "Restore Settings";
            RestoreRestoreSettingsButton.UseVisualStyleBackColor = true;
            RestoreRestoreSettingsButton.Click += RestoreButton_Click;
            // 
            // RestoreClassLabel
            // 
            RestoreClassLabel.AutoSize = true;
            RestoreClassLabel.Location = new Point(420, 10);
            RestoreClassLabel.Name = "RestoreClassLabel";
            RestoreClassLabel.Size = new Size(37, 15);
            RestoreClassLabel.TabIndex = 22;
            RestoreClassLabel.Text = "Class:";
            // 
            // RestoreClassComboBox
            // 
            RestoreClassComboBox.FormattingEnabled = true;
            RestoreClassComboBox.Location = new Point(457, 6);
            RestoreClassComboBox.Name = "RestoreClassComboBox";
            RestoreClassComboBox.Size = new Size(150, 23);
            RestoreClassComboBox.TabIndex = 21;
            RestoreClassComboBox.SelectedIndexChanged += RestoreClassComboBox_SelectedIndexChanged;
            // 
            // RestoreRealmLabel
            // 
            RestoreRealmLabel.AutoSize = true;
            RestoreRealmLabel.Location = new Point(215, 10);
            RestoreRealmLabel.Name = "RestoreRealmLabel";
            RestoreRealmLabel.Size = new Size(43, 15);
            RestoreRealmLabel.TabIndex = 20;
            RestoreRealmLabel.Text = "Realm:";
            RestoreRealmLabel.Click += RestoreRealmLabel_Click;
            // 
            // RestoreRealmComboBox
            // 
            RestoreRealmComboBox.FormattingEnabled = true;
            RestoreRealmComboBox.Location = new Point(264, 6);
            RestoreRealmComboBox.Name = "RestoreRealmComboBox";
            RestoreRealmComboBox.Size = new Size(150, 23);
            RestoreRealmComboBox.TabIndex = 19;
            RestoreRealmComboBox.SelectedIndexChanged += RestoreRealmComboBox_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TabPanel);
            Controls.Add(DAoCDirectoryPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(816, 489);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CharacterTool v1.0.0";
            FormClosing += MainForm_FormClosing;
            DAoCDirectoryPanel.ResumeLayout(false);
            DAoCDirectoryPanel.PerformLayout();
            TabPanel.ResumeLayout(false);
            DAoCTabControl.ResumeLayout(false);
            copySettingsTab.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            CustomSettingPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            AllCharacterPanel.ResumeLayout(false);
            AllCharacterPanel.PerformLayout();
            NewCharacterPanel.ResumeLayout(false);
            NewCharacterPanel.PerformLayout();
            ExistingCharacterPanel.ResumeLayout(false);
            ExistingCharacterPanel.PerformLayout();
            backUpCharacterTab.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            restoreCharacterTab.ResumeLayout(false);
            restoreCharacterTab.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)restoreDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel DAoCDirectoryPanel;
        private Label DAoCDirectoryLabel;
        private Panel TabPanel;
        private Button DAoCDirectoryButton;
        private TextBox DAoCDirectoryTextBox;
        private FolderBrowserDialog folderBrowserDialog1;
        private TabControl DAoCTabControl;
        private TabPage copySettingsTab;
        private Panel panel2;
        private Panel CustomSettingPanel;
        private CheckedListBox CustomSettingsCheckListBox;
        private RadioButton CustomSettingsCheckBox;
        private RadioButton AllSettingsCheckBox;
        private Panel panel1;
        private Panel AllCharacterPanel;
        private Label AllCharactersText;
        private Button AllCharactersSaveButton;
        private RadioButton AllCharactersCheckBox;
        private Panel NewCharacterPanel;
        private Button NewCharacterSaveButton;
        private ComboBox NewCharacterServerComboBox;
        private TextBox NewCharacterNameTextBox;
        private Label ServerComboBox;
        private Label CharacterNameLabel;
        private RadioButton NewCharacterCheckBox;
        private RadioButton ExistingCharacterCheckBox;
        private Panel ExistingCharacterPanel;
        private Label CopyToLabel2;
        private TextBox CopyToTextBox;
        private Button ExistingCharacterSaveButton;
        private ComboBox CopyToComboBox;
        private Label CopyToLabel1;
        private Label CopyLabel2;
        private TextBox FromServerText;
        private Label CopyLabel1;
        private ComboBox CopyFromComboBox;
        private Button RefreshButton;
        private TabPage backUpCharacterTab;
        private TabPage restoreCharacterTab;
        private Button ClearFilterButton;
        private Button RestoreDeleteSettingsButton;
        private Button RestoreRestoreSettingsButton;
        private DataGridView restoreDataGridView;
        private Label RestoreClassLabel;
        private ComboBox RestoreClassComboBox;
        private Label RestoreRealmLabel;
        private ComboBox RestoreRealmComboBox;
        private Panel panel3;
        private ComboBox RestoreNameFilterComboBox;
        private Label RestoreNameFilterLabel;
        private Button RestoreDBButton;
        private Button BackupDBButton;
        private Button EditDescriptionButton;
        private Panel panel6;
        private Panel panel5;
        private TextBox BackUpDescriptionTextBox;
        private Label BackupLabel;
        private Button SaveBackUp;
        private Button BackUpCharRefreshButton;
        private Label BackUpDescriptionLabel;
        private TextBox BackUpServerTextBox;
        private ComboBox BackUpNameComboBox;
        private Label BackUpClassLabel;
        private Label BackUpServerLabel;
        private ComboBox BackUpClassComboBox;
        private ComboBox BackUpRealmComboBox;
        private Label BackupRealmLabel;
        private Panel panel4;
        private Label WarningLabel;
        private Button SaveAllButton;
        private TextProgressBar SaveAllProgressBar;
        private TextProgressBar RestoreDeleteProgressBar;
    }
}