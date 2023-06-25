using DAoCToolSuite.CharacterTool.Files;
using DAoCToolSuite.CharacterTool.Json;
using Logger;
using Newtonsoft.Json;
using SQLLibrary;
using System.Data;
using System.IO;


namespace DAoCToolSuite.CharacterTool
{
    public partial class MainForm : Form
    {
        internal static LogManager Logger => LogManager.Instance;
        private static string DAoCCharacterFileDirectory
        {
            get
            {
                return Properties.Settings.Default.DAoCCharacterFileDirectory;
            }
            set
            {
                Properties.Settings.Default.DAoCCharacterFileDirectory = value;
                Properties.Settings.Default.Save();
            }
        }
        private static string JsonBackupFileFullPath
        {
            get
            {
                return Environment.ExpandEnvironmentVariables(Properties.Settings.Default.JsonBackupFileFullPath);
            }
            set
            {
                Properties.Settings.Default.JsonBackupFileFullPath = value;
                Properties.Settings.Default.Save();
            }
        }
        internal string DAoCCharacterDataFolder { get; private set; }
        internal static ParseDirectory? ParseDirectory { get; set; }
        private List<SettingsBackUpModel> Backups { get; set; } = new();
        private BindingSource BindingSource { get; set; } = new();

        private Dictionary<string, int>? _characterList = null;
        private Dictionary<string, int> CharacterList
        {
            get
            {
                _characterList ??= new ParseDirectory(DAoCCharacterDataFolder).Characters;
                return _characterList;
            }
            set => _characterList = value;
        }
        private string[] CharacterNameList => CharacterList.Keys.ToArray();
        private List<string?> ServerNames { get; set; } = new();
        private static ServerListINI? ServerList { get; set; } = new();
        private static RealmClassINI? RealmList { get; set; } = new();
        private static string GetServerName(int serverIndex)
        {
            Server? result = ServerList?.Servers?.Server?.Where(x => x.Index == serverIndex).FirstOrDefault();
            return result?.Name ?? $"Unknown({serverIndex})";
        }
        private static int GetServerIndex(string serverName)
        {
            Server? result = ServerList?.Servers?.Server?.Where(x => x.Name == serverName).FirstOrDefault();
            return result?.Index ?? -1;
        }
        private static List<string?> GetServerList()
        {
            List<string?>? result = ServerList?.Servers?.Server?.Select(x => x.Name)?.ToList();
            //result?.Sort();
            return result ?? new List<string?>();
        }
        internal static string DefaultLocation()
        {
            string sPath = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.DAoCCharacterFileDirectory);
            return sPath;
        }
        private void UpdateCharNameAutoComplete()
        {
            CharacterList = new ParseDirectory(DAoCCharacterDataFolder).Characters;
            AutoCompleteStringCollection source = new();
            source.AddRange(CharacterNameList);
            //CopyFrom
            CopyFromComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            CopyFromComboBox.AutoCompleteCustomSource = source;
            //CopyTo
            CopyToComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            CopyToComboBox.AutoCompleteCustomSource = source;
            //BackUp
            BackUpNameComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            BackUpNameComboBox.AutoCompleteCustomSource = source;
        }

        public MainForm()
        {
            Logger.Debug("Starting CharacterTool.MainForm()");
            InitializeComponent();
            DAoCCharacterDataFolder = DefaultLocation();
            ServerList = DeserializeServerList();
            RealmList = DeserializeRealmClass();
            _ = BackUpRealmComboBox.Items.Add("Albion");
            _ = BackUpRealmComboBox.Items.Add("Hibernia");
            _ = BackUpRealmComboBox.Items.Add("Midgard");
            _ = BackUpRealmComboBox.Items.Add("Unknown");
            _ = RestoreRealmComboBox.Items.Add("Albion");
            _ = RestoreRealmComboBox.Items.Add("Hibernia");
            _ = RestoreRealmComboBox.Items.Add("Midgard");
            _ = RestoreRealmComboBox.Items.Add("Unknown");
            DAoCDirectoryTextBox.Text = DefaultLocation();
            ClearFilterButton.Enabled = false;
            ParseDirectory = new ParseDirectory(DAoCCharacterDataFolder);
            RestoreDataGridView.DataSource = BindingSource;
            UpdateCharNameAutoComplete();
            List<string?> serverList = GetServerList() ?? throw new NullReferenceException("ServerList is null");
            ServerNames = serverList;
            LoadBackups();
        }

        private static ServerListINI DeserializeServerList()
        {
            try
            {
                ServerListINI input = JsonConvert.DeserializeObject<ServerListINI>(Properties.Settings.Default.ServerList) ?? new ServerListINI();
                return input;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new ServerListINI();
            }
        }

        private static RealmClassINI DeserializeRealmClass()
        {
            try
            {
                RealmClassINI input = JsonConvert.DeserializeObject<RealmClassINI>(Properties.Settings.Default.RealmClasses) ?? new RealmClassINI();
                return input;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new RealmClassINI();
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sender is not MainForm form)
            {
                return;
            }

            Properties.Settings.Default.WindowLocation = form.Location;
            Properties.Settings.Default.Save();
            Logger.Debug($"Shutting down.");
        }

        private void DAoCTabControl_SelectedIndexChange(object sender, TabControlEventArgs e)
        {

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Logger.Debug("CharacterTool Form Displayed.");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            long currentCount = CharacterTool.Properties.Settings.Default.LoadCount;
            if (currentCount > 0)
            {
                Location = CharacterTool.Properties.Settings.Default.WindowLocation;
            }
            else
            {
                //ScreenCentered by Default
                CharacterTool.Properties.Settings.Default.WindowLocation = Location;
            }
            if (currentCount != long.MaxValue)
            {

                CharacterTool.Properties.Settings.Default.LoadCount = currentCount + 1;
            }
            CharacterTool.Properties.Settings.Default.Save();
        }

        #region Copy Tab
        private void ResetCopySettingsTab()
        {
            NewCharacterServerComboBox.Items.Clear();
            NewCharacterServerComboBox.ResetText();
            BackUpRealmComboBox.ResetText();
            BackUpClassComboBox.ResetText();
            BackUpClassComboBox.Enabled = false;
            BackUpClassLabel.Enabled = false;
            NewCharacterNameTextBox.Clear();
            CopyToTextBox.Clear();
            FromServerText.Clear();
            ExistingCharacterCheckBox.Checked = true;
            DAoCCharacterDataFolder = DAoCDirectoryTextBox.Text;
            CopyFromComboBox.Items.Clear();
            CopyFromComboBox.ResetText();
            CopyToComboBox.Items.Clear();
            CopyToComboBox.ResetText();
            BackUpNameComboBox.Items.Clear();
            BackUpNameComboBox.ResetText();
            CharacterList = new ParseDirectory(DAoCCharacterDataFolder).Characters;
            foreach (KeyValuePair<string, int> character in CharacterList)
            {
                _ = CopyFromComboBox.Items.Add(character.Key);
                _ = CopyToComboBox.Items.Add(character.Key);
                _ = BackUpNameComboBox.Items.Add(character.Key);
            }
        }
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            ResetCopySettingsTab();
        }
        private void ExistingCharacterSaveButton_Click(object sender, EventArgs e)
        {
            string fromCharacterPath = DAoCDirectoryTextBox.Text + $"\\{CopyFromComboBox.Text.Split('(').First()}-{GetServerIndex(FromServerText.Text)}.ini".Replace(" ", "");
            string toCharacterPath = DAoCDirectoryTextBox.Text + $"\\{CopyToComboBox.Text.Split('(').First()}-{GetServerIndex(CopyToTextBox.Text)}.ini".Replace(" ", "");
            CharacterINI fromCharacter = new(fromCharacterPath);
            CharacterINI toCharacter = new(toCharacterPath);
            CopyCharacterIniData(fromCharacter, toCharacter);
            ResetCopySettingsTab();
        }
        private void NewCharacterSaveButton_Click(object sender, EventArgs e)
        {
            string fromCharacterPath = DAoCDirectoryTextBox.Text + $"\\{CopyFromComboBox.Text.Split('(').First()}-{GetServerIndex(FromServerText.Text)}.ini".Replace(" ", "");
            string toCharacterPath = DAoCDirectoryTextBox.Text + $"\\{NewCharacterNameTextBox.Text}-{GetServerIndex(NewCharacterServerComboBox.Text)}.ini".Replace(" ", "");
            CharacterINI fromCharacter = new(fromCharacterPath);
            CharacterINI toCharacter = fromCharacter;

            toCharacter.PurgeQuickbars();

            WriteCharacterIni.WriteCharacter(toCharacterPath, toCharacter);
            NewCharacterNameTextBox.ResetText();
            NewCharacterServerComboBox.ResetText();
            UpdateCharNameAutoComplete();
        }
        private void AllCharactersSaveButton_Click(object sender, EventArgs e)
        {
            string fromCharacterPath = DAoCDirectoryTextBox.Text + $"\\{CopyFromComboBox.Text.Split('(').First()}-{GetServerIndex(FromServerText.Text)}.ini".Replace(" ", "");
            CharacterINI fromCharacter = new(fromCharacterPath);
            Dictionary<string, int> characterList = CharacterList;
            foreach (KeyValuePair<string, int> character in characterList)
            {
                string toCharacterPath = DAoCDirectoryTextBox.Text + $"\\{character.Key.Split('(').First()}-{character.Value}.ini".Replace(" ", "");
                CharacterINI toCharacter = new(toCharacterPath);
                CopyCharacterIniData(fromCharacter, toCharacter);
            }
            ResetCopySettingsTab();
        }
        private void ExistingCharacterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ExistingCharacterCheckBox.Checked)
            {
                NewCharacterCheckBox.Checked = false;
                NewCharacterPanel.Enabled = false;
                AllCharactersCheckBox.Checked = false;
                AllCharacterPanel.Enabled = false;
                ExistingCharacterPanel.Enabled = true;
            }
        }
        private void NewCharacterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NewCharacterCheckBox.Checked)
            {
                ExistingCharacterCheckBox.Checked = false;
                ExistingCharacterPanel.Enabled = false;
                AllCharactersCheckBox.Checked = false;
                AllCharacterPanel.Enabled = false;
                NewCharacterPanel.Enabled = true;
                if (NewCharacterServerComboBox.Items.Count < 1)
                {
                    foreach (string? server in ServerNames)
                    {
                        _ = NewCharacterServerComboBox.Items.Add(server);
                    }
                }
            }
        }
        private void AllCharactersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AllCharactersCheckBox.Checked)
            {
                ExistingCharacterCheckBox.Checked = false;
                ExistingCharacterPanel.Enabled = false;
                NewCharacterCheckBox.Checked = false;
                NewCharacterPanel.Enabled = false;
                AllCharacterPanel.Enabled = true;
            }
        }
        private void CopyFromComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CopyFromComboBox.SelectedItem is not null)
            {
                string? selectedItem = CopyFromComboBox.SelectedItem.ToString();
                if (selectedItem is not null && CharacterList.ContainsKey(selectedItem))
                {
                    FromServerText.Text = GetServerName(CharacterList[selectedItem]);
                }
            }
        }
        private void FromServerText_TextChanged(object sender, EventArgs e)
        {

        }
        private void CopyToComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CopyToComboBox.SelectedItem is not null)
            {
                string? selectedItem = CopyToComboBox.SelectedItem.ToString();
                if (selectedItem is not null && CharacterList.ContainsKey(selectedItem))
                {
                    CopyToTextBox.Text = GetServerName(CharacterList[selectedItem]);
                }
            }
        }

        #region Source Folder
        private void DAoCDirectoryButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer,
                SelectedPath = DAoCDirectoryTextBox.Text
            };

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DAoCDirectoryTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void DAoCDirectoryTextBox_TextChanged(object sender, EventArgs e)
        {
            DAoCCharacterDataFolder = DAoCDirectoryTextBox.Text;
            CharacterList = new ParseDirectory(DAoCCharacterDataFolder).Characters;
            CopyFromComboBox.Items.Clear();
            CopyToComboBox.Items.Clear();
            BackUpNameComboBox.Items.Clear();
            foreach (KeyValuePair<string, int> character in CharacterList)
            {
                _ = CopyFromComboBox.Items.Add(character.Key);
                _ = CopyToComboBox.Items.Add(character.Key);
                _ = BackUpNameComboBox.Items.Add(character.Key);
            }

        }
        #endregion

        #region Settings Filter
        private void AllSettingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AllSettingsCheckBox.Checked)
            {
                CustomSettingsCheckBox.Checked = false;
                CustomSettingsCheckListBox.Enabled = false;
                CustomSettingPanel.Enabled = false;
            }
        }
        private void CustomSettingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CustomSettingsCheckBox.Checked)
            {
                AllSettingsCheckBox.Checked = false;
                CustomSettingsCheckListBox.Enabled = true;
                CustomSettingPanel.Enabled = true;
            }
        }
        private void CustomSettingsCheckListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion

        #region Backup Tab
        private void SaveBackUp_Click(object sender, EventArgs e)
        {

            string iniFileName = ParseDirectory?.FindIniFileByCharacterName(BackUpNameComboBox.Text) ?? $"{BackUpNameComboBox.Text.Split('(').First()}-{GetServerIndex(BackUpServerTextBox.Text)}.ini".Replace(" ", "");
            string ignFileName = ParseDirectory?.FindIgnFileByCharacterName(BackUpNameComboBox.Text) ?? $"{BackUpNameComboBox.Text.Split('(').First()}-143.ign".Replace(" ", "");
            string directoryPath = DAoCDirectoryTextBox.Text;
            string? iniContents = ParseDirectory.GetFileContents(directoryPath + $"\\{iniFileName}");
            string? ignContents = ParseDirectory.GetFileContents(directoryPath + $"\\{ignFileName}");
            SettingsBackUpModel settingsBackup = new()
            {
                FirstName = BackUpNameComboBox.Text,
                Realm = BackUpRealmComboBox.Text,
                Class = BackUpClassComboBox.Text,
                Path = directoryPath,
                Description = BackUpDescriptionTextBox.Text,
            };
            if (!string.IsNullOrEmpty(iniContents))
            {
                settingsBackup.INIFileName = iniFileName;
                settingsBackup.INIData = iniContents;
                if (!string.IsNullOrEmpty(ignContents))
                {
                    settingsBackup.IGNFileName = ignFileName;
                    settingsBackup.IGNData = ignContents;
                }
                try
                {
                    SqliteDataAccess.AddSettingBackup(settingsBackup, DateTime.Now);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
            LoadBackups();
            ResetCharacterBackupTab();
        }
        private void SaveAll_Click(object sender, EventArgs e)
        {
            if (ParseDirectory?.INIFiles is null)
            {
                return;
            }

            SaveAllButton.Enabled = false;

            try
            {
                SaveAllProgressBar.Minimum = 0;
                SaveAllProgressBar.Maximum = ParseDirectory.INIFiles.Count;
                SaveAllProgressBar.Value = 0;
                SaveAllProgressBar.CustomText = "Saving";
                SaveAllProgressBar.Visible = true;
                SaveAllProgressBar.Refresh();

                foreach (string? file in ParseDirectory.INIFiles)
                {
                    SaveAllProgressBar.Value += 1;
                    if (file is null)
                    {
                        continue;
                    }

                    string iniFileName = file.Split('\\').Last();
                    string characterName = iniFileName.Split('-').First();
                    string serverStr = iniFileName.Split("-").Last().Split('.').First();
                    int ServerIndex = int.TryParse(serverStr, out ServerIndex) ? ServerIndex : -1;
                    List<Server>? Servers = ServerList?.Servers?.Server;
                    string? serverName = Servers?.Where(x => x.Index == ServerIndex).Select(x => x.Name).FirstOrDefault();
                    string? ignFileName = ParseDirectory?.FindIgnFileByCharacterName(characterName);
                    string? directoryPath = DAoCDirectoryTextBox.Text;
                    if (ignFileName is null || directoryPath is null)
                    {
                        continue;
                    }

                    string? iniContents = ParseDirectory.GetFileContents(file);
                    string? ignContents = ParseDirectory.GetFileContents(directoryPath + $"\\{ignFileName}");
                    string realm;
                    string className;
                    CharacterModel? characterModel = SqliteDataAccess.LoadCharacterByFirstName(characterName);
                    if (characterModel is not null)
                    {
                        realm = characterModel.Realm ?? "Unknown";
                        className = characterModel.Class ?? "Unknown"; ;
                    }
                    else
                    {
                        realm = "Unknown";
                        className = "Unknown";
                    }
                    SettingsBackUpModel settingsBackup = new()
                    {
                        FirstName = characterName,
                        Realm = realm,
                        Class = className,
                        Path = directoryPath,
                        Description = $"(Server: {serverName ?? "Unknown"}) - Backup created: {DateTime.Now}",
                    };
                    if (!string.IsNullOrEmpty(iniContents))
                    {
                        settingsBackup.INIFileName = iniFileName;
                        settingsBackup.INIData = iniContents;
                        if (!string.IsNullOrEmpty(ignContents))
                        {
                            settingsBackup.IGNFileName = ignFileName;
                            settingsBackup.IGNData = ignContents;
                        }
                        try
                        {
                            SqliteDataAccess.AddSettingBackup(settingsBackup, DateTime.Now);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            SaveAllProgressBar.Visible = false;
            LoadBackups();
            ResetCharacterBackupTab();
            SaveAllButton.Enabled = true;
        }
        private void BackUpNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is not System.Windows.Forms.ComboBox comboBox)
            {
                return;
            }

            string characterFirstName = comboBox.Text;

            //Populate automatically if already in database
            CharacterModel? characterModel = SqliteDataAccess.LoadCharacterByFirstName(characterFirstName);
            if (characterModel is not null)
            {
                BackUpRealmComboBox.Text = characterModel.Realm;
                //BackUpServerTextBox.Text = characterModel.Server;
                BackUpClassComboBox.Text = characterModel.Class;
            }
            else
            {
                BackUpRealmComboBox.Text = "";
                BackUpClassComboBox.Text = "";
            }

            if (comboBox.SelectedItem is not null)
            {
                string? selectedItem = comboBox.SelectedItem.ToString();
                if (selectedItem is not null && CharacterList.ContainsKey(selectedItem))
                {
                    BackUpServerTextBox.Text = GetServerName(CharacterList[selectedItem]);
                }
            }
        }
        private void PopulateClasses(string realm)
        {
            if (RealmList is null)
            {
                throw new NullReferenceException("RealmList is null.");
            }

            List<string> classes = new();
            switch (realm)
            {
                case "Albion":
                    classes = RealmList?.Realm?.Albion?.Split(',').ToList() ?? new();
                    break;
                case "Hibernia":
                    classes = RealmList?.Realm?.Hibernia?.Split(",").ToList() ?? new();
                    break;
                case "Midgard":
                    classes = RealmList?.Realm?.Midgard?.Split(",").ToList() ?? new();
                    break;
                case "Unknown":
                    classes = new() { "Unknown" };
                    break;
            }

            BackUpClassComboBox.Items.Clear();
            BackUpClassComboBox.ResetText();
            RestoreClassComboBox.Items.Clear();
            RestoreClassComboBox.ResetText();
            foreach (string cls in classes)
            {
                _ = BackUpClassComboBox.Items.Add(cls);
                _ = RestoreClassComboBox.Items.Add(cls);
            }
        }
        private void BackUpRealmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BackUpRealmComboBox.Text))
            {
                PopulateClasses(BackUpRealmComboBox.Text);
                BackUpClassComboBox.Enabled = true;
                BackUpClassLabel.Enabled = true;
            }
            else
            {
                BackUpClassComboBox.Items.Clear();
                BackUpClassComboBox.ResetText();
                BackUpClassComboBox.Enabled = false;
                BackUpClassLabel.Enabled = false;
            }
        }
        private void ResetCharacterBackupTab()
        {
            BackUpNameComboBox.Items.Clear();
            BackUpNameComboBox.ResetText();

            BackUpServerTextBox.Clear();
            BackUpRealmComboBox.ResetText();

            BackUpClassComboBox.Items.Clear();
            BackUpClassComboBox.ResetText();
            BackUpClassComboBox.Enabled = false;
            BackUpClassLabel.Enabled = false;

            BackUpDescriptionTextBox.Clear();

            CharacterList = ParseDirectory!.Characters;

            foreach (KeyValuePair<string, int> character in CharacterList)
            {
                _ = CopyFromComboBox.Items.Add(character.Key);
                _ = CopyToComboBox.Items.Add(character.Key);
                _ = BackUpNameComboBox.Items.Add(character.Key);
            }

        }
        private void BackUpCharRefreshButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("BackUpCharRefreshButton clicked.");
            BackUpNameComboBox.Items.Clear();
            BackUpNameComboBox.ResetText();
            BackUpServerTextBox.Clear();
            CharacterList = new ParseDirectory(DAoCCharacterDataFolder).Characters;
            foreach (KeyValuePair<string, int> character in CharacterList)
            {
                _ = CopyFromComboBox.Items.Add(character.Key);
                _ = CopyToComboBox.Items.Add(character.Key);
                _ = BackUpNameComboBox.Items.Add(character.Key);
            }
        }
        #endregion

        #region Restore Tab
        private void LoadBackups()
        {
            try
            {
                Logger.Debug("Loading Setting Backups");
                Backups = SqliteDataAccess.LoadSettingBackUps().OrderBy(x => x.Realm).ThenBy(x => x.Class).ThenBy(x => x.FirstName).ThenByDescending(x => x.DateTime).ToList();
                AttachBackups();
                FormatGridView();
                FilterDataSource();
                Logger.Debug("Setting Backups Loaded");

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void FormatGridView()
        {
            List<string> visibleColumns = new() { "DateOnly", "FirstName", "Realm", "Class", "Description" };
            List<string> visibleColumnHeaderNames = new() { "Date", "Name", "Realm", "Class", "Description" };
            int rowCount = RestoreDataGridView.Rows.Count;
            int columnCount = RestoreDataGridView.Columns.Count;
            int nonVisibleIndex = visibleColumns.Count;


            //Sets the column names, order, and what data from the DB to display.
            //Values pulled from the config file.

            if (columnCount > 0)
            {
                for (int index = 0; index < columnCount; index++)
                {
                    DataGridViewColumn column = RestoreDataGridView.Columns[index];
                    if (!visibleColumns.Contains(column.Name))
                    {
                        column.Visible = false;
                        column.DisplayIndex = nonVisibleIndex;
                        nonVisibleIndex++;
                    }
                    else
                    {
                        column.DisplayIndex = visibleColumns.IndexOf(column.Name);
                        column.HeaderText = visibleColumnHeaderNames[visibleColumns.IndexOf(column.Name)];
                        column.ValueType = typeof(string);
                        //Logger.Debug($"{column.Name},{index},{column.DisplayIndex},{column.Visible}");
                        column.AutoSizeMode = column.Name switch
                        {
                            "Description" => DataGridViewAutoSizeColumnMode.Fill,
                            _ => DataGridViewAutoSizeColumnMode.AllCells,
                        };
                    }

                }
            }
        }

        private void AttachBackups()
        {
            BindingSource.DataSource = Backups ?? new();
            RestoreRestoreSettingsButton.Enabled = BindingSource.Count > 0;
            RestoreDeleteSettingsButton.Enabled = BindingSource.Count > 0;
            BackupDBButton.Enabled = BindingSource.Count > 0;
            RestoreDBButton.Enabled = HasDBBackupJson();
        }

        private void RestoreDataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            FormatGridView();
        }
        private void RestoreRealmLabel_Click(object sender, EventArgs e)
        {

        }

        private static bool IgnoreFilterComboChangeIndexChange { get; set; } = false;
        private void RestoreNameFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IgnoreFilterComboChangeIndexChange)
            {
                return;
            }

            FilterDataSource();
        }
        private void CopyCharacterIniData(CharacterINI fromCharacter, CharacterINI toCharacter)
        {
            if (AllSettingsCheckBox.Checked)
            {
                toCharacter.Chat = fromCharacter.Chat;
                toCharacter.Panels = fromCharacter.Panels;
                toCharacter.QuickBinds = fromCharacter.QuickBinds;
                toCharacter.NameOptions = fromCharacter.NameOptions;
                toCharacter.ToolTips = fromCharacter.ToolTips;
                toCharacter.Macros = fromCharacter.Macros;
                toCharacter.OverwriteQuickbars(fromCharacter.DATA);
            }
            else
            {
                if (CustomSettingsCheckListBox.CheckedItems.Contains("Chat (Colors)"))
                {
                    toCharacter.Chat = fromCharacter.Chat;
                }
                if (CustomSettingsCheckListBox.CheckedItems.Contains("Panels (UI)"))
                {
                    toCharacter.Panels = fromCharacter.Panels;
                }
                if (CustomSettingsCheckListBox.CheckedItems.Contains("QuickBinds"))
                {
                    toCharacter.QuickBinds = fromCharacter.QuickBinds;
                }
                if (CustomSettingsCheckListBox.CheckedItems.Contains("NameOptions"))
                {
                    toCharacter.NameOptions = fromCharacter.NameOptions;
                }
                if (CustomSettingsCheckListBox.CheckedItems.Contains("ToolTips"))
                {
                    toCharacter.ToolTips = fromCharacter.ToolTips;
                }
                if (CustomSettingsCheckListBox.CheckedItems.Contains("Macros"))
                {
                    toCharacter.Macros = fromCharacter.Macros;
                    toCharacter.OverwriteQuickbars(fromCharacter.DATA);
                }
            }
            WriteCharacterIni.WriteCharacter(toCharacter.FilePath, toCharacter);
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RestoreRestoreSettingsButton.Enabled = true;
            RestoreDeleteSettingsButton.Enabled = true;
            FormatGridView();
        }

        private void RestoreTab_Click(object sender, EventArgs e)
        {

        }

        private void FilterDataSource()
        {
            IgnoreFilterComboChangeIndexChange = true;
            string? _realm = RestoreRealmComboBox.Text;
            string? _class = RestoreClassComboBox.Text;
            string? _name = RestoreNameFilterComboBox.Text;

            List<SettingsBackUpModel> filtered = Backups;
            if (!string.IsNullOrEmpty(_name))
            {
                filtered = filtered.Where(x => x.FirstName == _name).ToList();
            }
            if (!string.IsNullOrEmpty(_realm))
            {
                filtered = filtered.Where(x => x.Realm == _realm).ToList();
            }
            if (!string.IsNullOrEmpty(_class))
            {
                filtered = filtered.Where(x => x.Class == _class).ToList();
            }
            BindingSource.DataSource = filtered;

            #region Name Filter
            RestoreNameFilterComboBox.ResetText();
            RestoreNameFilterComboBox.Items.Clear();
            List<SettingsBackUpModel> backupsByName = filtered.OrderBy(x => x.FirstName).ToList();
            foreach (SettingsBackUpModel backup in backupsByName)
            {
                if (!RestoreNameFilterComboBox.Items.Contains(backup.FirstName))
                {
                    _ = RestoreNameFilterComboBox.Items.Add(backup.FirstName);
                }
            }
            if (_name is not null && RestoreNameFilterComboBox.Items.Contains(_name))
            {
                RestoreNameFilterComboBox.Text = _name;
            }

            RestoreNameFilterComboBox.Enabled = RestoreNameFilterComboBox.Items.Count >= 1;
            #endregion

            #region Realm Filter
            RestoreRealmComboBox.ResetText();
            RestoreRealmComboBox.Items.Clear();
            List<SettingsBackUpModel> backupsByRealm = filtered.OrderBy(x => x.Realm).ToList();
            foreach (SettingsBackUpModel backup in backupsByRealm)
            {
                if (!RestoreRealmComboBox.Items.Contains(backup.Realm))
                {
                    _ = RestoreRealmComboBox.Items.Add(backup.Realm);
                }
            }
            if (_realm is not null && RestoreRealmComboBox.Items.Contains(_realm))
            {
                RestoreRealmComboBox.Text = _realm;
            }

            RestoreRealmComboBox.Enabled = RestoreRealmComboBox.Items.Count >= 1;
            #endregion

            #region Class Filter
            RestoreClassComboBox.ResetText();
            RestoreClassComboBox.Items.Clear();
            List<SettingsBackUpModel> backupsByClass = filtered.OrderBy(x => x.Class).ToList();
            foreach (SettingsBackUpModel backup in backupsByClass)
            {
                if (!RestoreClassComboBox.Items.Contains(backup.Class))
                {
                    _ = RestoreClassComboBox.Items.Add(backup.Class);
                }
            }
            if (_class is not null && RestoreClassComboBox.Items.Contains(_class))
            {
                RestoreClassComboBox.Text = _class;
            }

            RestoreClassComboBox.Enabled = RestoreClassComboBox.Items.Count >= 1;
            #endregion

            IgnoreFilterComboChangeIndexChange = false;

            ClearFilterButton.Enabled = !string.IsNullOrEmpty(RestoreRealmComboBox.Text) || !string.IsNullOrEmpty(RestoreClassComboBox.Text) || !string.IsNullOrEmpty(RestoreNameFilterComboBox.Text);
        }

        private void RestoreRealmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IgnoreFilterComboChangeIndexChange)
            {
                return;
            }

            FilterDataSource();
        }
        private void RestoreClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IgnoreFilterComboChangeIndexChange)
            {
                return;
            }

            FilterDataSource();
        }
        private void ClearFilterButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("ClearFilterButton clicked.");
            ResetCharacterRestoreTab();
        }

        private void ResetFilters()
        {
            IgnoreFilterComboChangeIndexChange = true;
            RestoreRealmComboBox.ResetText();
            RestoreNameFilterComboBox.ResetText();
            RestoreClassComboBox.ResetText();
            IgnoreFilterComboChangeIndexChange = false;
        }

        private void ResetCharacterRestoreTab()
        {
            LoadBackups();
            ResetFilters();
            FilterDataSource();

            if (RestoreDataGridView.Rows.Count > 0)
            {
                RestoreRestoreSettingsButton.Enabled = true;
                RestoreDeleteSettingsButton.Enabled = true;
            }
            else
            {
                RestoreRestoreSettingsButton.Enabled = false;
                RestoreDeleteSettingsButton.Enabled = false;
            }
        }
        private void RestoreButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("RestoreButton clicked.");
            RestoreRestoreSettingsButton.Enabled = false;
            List<string> charNames = new();
            DataGridViewSelectedRowCollection selectedRows = RestoreDataGridView.SelectedRows;
            if (selectedRows is null)
            {
                return;
            }

            RestoreDeleteProgressBar.Minimum = 0;
            RestoreDeleteProgressBar.Maximum = selectedRows.Count;
            RestoreDeleteProgressBar.Value = 0;
            RestoreDeleteProgressBar.CustomText = "Restoring";
            RestoreDeleteProgressBar.Visible = true;
            RestoreDeleteProgressBar.Refresh();

            foreach (DataGridViewRow row in selectedRows)
            {
                RestoreDeleteProgressBar.Value += 1;
                string? dbIndexStr = row?.Cells["index"]?.Value?.ToString();
                if (dbIndexStr is not null)
                {
                    int dbIndex = int.TryParse(dbIndexStr, out dbIndex) ? dbIndex : -1;
                    if (dbIndex > -1)
                    {
                        SettingsBackUpModel settingsBackup = SqliteDataAccess.LoadSettingByIndex(dbIndex);
                        File.WriteAllText($"{settingsBackup.Path}\\{settingsBackup.INIFileName}", settingsBackup.INIData);
                        if (settingsBackup.IGNFileName is not null)
                        {
                            File.WriteAllText($"{settingsBackup.Path}\\{settingsBackup.IGNFileName}", settingsBackup.IGNData);
                        }

                        if (selectedRows.Count > 1 && settingsBackup.FirstName is not null)
                        {
                            charNames.Add(settingsBackup.FirstName);
                        }
                        else
                        {
                            _ = MessageBox.Show($"Restored character files for {settingsBackup.FirstName}\n{settingsBackup.Description}", "Restore Character Settings", MessageBoxButtons.OK);
                        }
                    }
                }
            }
            if (selectedRows.Count > 1)
            {
                _ = MessageBox.Show($"Restored character files for {string.Join(", ", charNames)}", "Restore Character Settings", MessageBoxButtons.OK);
            }

            RestoreDeleteProgressBar.Visible = false;
            RestoreRestoreSettingsButton.Enabled = true;
        }

        private void RestoreDeleteRecordButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("RestoreDeleteRecordButton clicked.");
            RestoreDeleteSettingsButton.Enabled = false;
            DataGridViewSelectedRowCollection selectedRows = RestoreDataGridView.SelectedRows;
            if (selectedRows is null)
            {
                return;
            }

            List<int> charactersToDelete = new();
            foreach (DataGridViewRow row in selectedRows)
            {
                string? dbIndexStr = row?.Cells["index"]?.Value?.ToString();
                if (dbIndexStr is not null)
                {
                    int dbIndex = int.TryParse(dbIndexStr, out dbIndex) ? dbIndex : -1;
                    if (dbIndex > -1)
                    {
                        SettingsBackUpModel settingsBackup = SqliteDataAccess.LoadSettingByIndex(dbIndex);
                        if (selectedRows.Count > 1 && settingsBackup.FirstName is not null)
                        {
                            charactersToDelete.Add(dbIndex);
                        }
                        else
                        {
                            DialogResult del = MessageBox.Show($"Delete backup for:{settingsBackup.FirstName}?\n{settingsBackup.Description}", "Delete Setting Backup", MessageBoxButtons.YesNo);
                            if (del == DialogResult.No)
                            {
                                return;
                            }
                            SqliteDataAccess.DeleteSettingBackupByIndex(dbIndex);
                        }
                    }
                }
            }
            if (selectedRows.Count > 1)
            {
                RestoreDeleteProgressBar.Minimum = 0;
                RestoreDeleteProgressBar.Maximum = charactersToDelete.Count;
                RestoreDeleteProgressBar.Value = 0;
                RestoreDeleteProgressBar.CustomText = "Deleting";
                RestoreDeleteProgressBar.Visible = true;
                RestoreDeleteProgressBar.Refresh();

                DialogResult del = MessageBox.Show($"Are you sure you want to delete the {charactersToDelete.Count} selected records?", "Delete Setting Backup", MessageBoxButtons.YesNo);
                if (del == DialogResult.No)
                {
                    RestoreDeleteProgressBar.Visible = false;
                    return;
                }
                foreach (int index in charactersToDelete)
                {
                    RestoreDeleteProgressBar.Value += 1;
                    SqliteDataAccess.DeleteSettingBackupByIndex(index);
                }
            }
            RestoreDeleteProgressBar.Visible = false;
            LoadBackups();
            FilterDataSource();
            RestoreDeleteSettingsButton.Enabled = true;
        }

        private void EditDescriptionButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("EditDescriptionButton clicked.");
            SettingsBackUpModel? settingsBackup = null;
            DataGridViewSelectedRowCollection selectedRows = RestoreDataGridView.SelectedRows;
            DataGridViewRow row = selectedRows[0];
            if (row is null)
            {
                return;
            }

            string? dbIndexStr = row?.Cells["index"]?.Value?.ToString();
            if (dbIndexStr is not null)
            {
                int dbIndex = int.TryParse(dbIndexStr, out dbIndex) ? dbIndex : -1;
                if (dbIndex > -1)
                {
                    settingsBackup = SqliteDataAccess.LoadSettingByIndex(dbIndex);
                }
                else
                {
                    return;
                }
            }
            if (settingsBackup is null)
            {
                return;
            }

            EditDialog dialog = new(row!, DAoCCharacterDataFolder, ServerList!, RealmList!)
            {
                Owner = this,
                StartPosition = FormStartPosition.Manual
            };
            dialog.BackUpNameComboBox.Text = settingsBackup.FirstName;

            if (settingsBackup.FirstName is not null && CharacterList.ContainsKey(settingsBackup.FirstName))
            {
                dialog.BackUpServerTextBox.Text = GetServerName(CharacterList[settingsBackup.FirstName]);

            }
            dialog.BackUpRealmComboBox.Text = settingsBackup.Realm;
            dialog.BackUpClassComboBox.Text = settingsBackup.Class;
            dialog.BackUpDescriptionTextBox.Text = settingsBackup.Description;
            dialog.SetLocation();
            _ = dialog.ShowDialog();
            LoadBackups();
        }
        #endregion

        #region SQLite DB (Restore and Backup)
        private static readonly object jsonLock = new();
        private string SerializeBackups()
        {
            try
            {
                SettingsBackupJson toWrite = new() { Backups = Backups };
                string output = JsonConvert.SerializeObject(toWrite);
                return output;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return "{}";
            }
        }
        private static void WriteDBBackup(string json)
        {
            lock (jsonLock)
            {
                try
                {
                    File.WriteAllText(JsonBackupFileFullPath, json);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }
        private void BackupDBButton_Click(object sender, EventArgs e)
        {
            string json = SerializeBackups();
            WriteDBBackup(json);
            RestoreDBButton.Enabled = HasDBBackupJson();
        }

        private static bool HasDBBackupJson()
        {
            return File.Exists(JsonBackupFileFullPath);
        }

        private static string ReadDBBackupJson()
        {
            lock (jsonLock)
            {
                try
                {
                    string json = File.ReadAllText(JsonBackupFileFullPath);
                    return json;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return "{}";
                }
            }
        }

        private static SettingsBackupJson DeserializeDBBackupJson(string json)
        {
            try
            {
                SettingsBackupJson input = JsonConvert.DeserializeObject<SettingsBackupJson>(json) ?? new SettingsBackupJson();
                return input;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new SettingsBackupJson();
            }

        }

        private static SettingsBackupJson LoadDBBackupJson()
        {
            Logger.Debug("Loading DB Backup from disk.");
            SettingsBackupJson settingsBackupJson = new();
            if (HasDBBackupJson())
            {
                try
                {
                    string json = ReadDBBackupJson();
                    settingsBackupJson = DeserializeDBBackupJson(json);
                    if (settingsBackupJson.Count > 0)
                    {
                        Logger.Debug($"Successfully loaded {settingsBackupJson.Count} setting backups from disk.");
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
            else
            {
                Logger.Warn($"No valid character repository file found. A new file will be created.");
                settingsBackupJson = new();
            }
            return settingsBackupJson;
        }

        private static void RestoreDBFromJson()
        {
            if (!HasDBBackupJson())
            {
                return;
            }

            try
            {
                SqliteDataAccess.ResetSettingsBackup();

                SettingsBackupJson settingsBackupJson = LoadDBBackupJson();
                if (settingsBackupJson?.Backups is null)
                {
                    return;
                }

                foreach (SettingsBackUpModel settingsBackup in settingsBackupJson.Backups)
                {
                    SqliteDataAccess.AddSettingBackup(settingsBackup, settingsBackup.DateTime);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void RestoreDBButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("Restore button has been pressed.");
            if (File.Exists(JsonBackupFileFullPath))
            {
                DialogResult del = MessageBox.Show($"WARNING: Restoring DB will delete all existing records.\nContinue?", "Restore Database", MessageBoxButtons.YesNo);
                if (del == DialogResult.No)
                {
                    return;
                }
                RestoreDBFromJson();
                Logger.Debug($"Backup restored from {JsonBackupFileFullPath}");
                LoadBackups();
            }
            else
            {
                Logger.Debug($"No DB Backup Json found at {JsonBackupFileFullPath}");
            }
        }
        #endregion

    }
}