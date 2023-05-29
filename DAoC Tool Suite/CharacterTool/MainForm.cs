using System.Data;
using System.IO;
using DAoCToolSuite.CharacterTool.Logging;
using Newtonsoft.Json;

namespace DAoCToolSuite.CharacterTool
{
    public partial class MainForm : Form
    {
        internal static Logger Logger = new();
        internal string Folder { get; private set; }
        internal static string RepositoryFullPath = $"{Environment.CurrentDirectory}\\CharacterRepository.json";
        internal static string ServerConfigFullPath = $"{Environment.CurrentDirectory}\\ServerConfig.json";
        internal static string RealmClassConfigFullPath = $"{Environment.CurrentDirectory}\\RealmClassConfig.json";
        internal const string CharFolder = @"\LotM";
        private Dictionary<string, int>? _characterList = null;
        private Dictionary<string, int> CharacterList
        {
            get
            {
                _characterList ??= new ParseDirectory(Folder + CharFolder).Characters;
                return _characterList;
            }
            set => _characterList = value;
        }
        private string[] CharacterNameList => CharacterList.Keys.ToArray();
        private readonly List<string?> ServerNames = new();
        private static readonly ServerListINI? ServerList = JsonFileReader.Read<ServerListINI>(ServerConfigFullPath);
        private static readonly RealmClassINI? RealmList = JsonFileReader.Read<RealmClassINI>(RealmClassConfigFullPath);
        private static RecordRepository? _characterRepository = null;
        private static RecordRepository? CharacterRepository
        {
            get
            {
                _characterRepository ??= LoadCharacterRepository();
                return _characterRepository;
            }
            set => _characterRepository = value;
        }
        private static DataTable RestoreDataTable { get; set; } = new DataTable();


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
        private static List<string?>? GetServerList()
        {
            List<string?>? result = ServerList?.Servers?.Server?.Select(x => x.Name)?.ToList();
            //result?.Sort();
            return result ?? new List<string?>();
        }
        internal static string DefaultLocation()
        {
            string sPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return sPath + @"\Electronic Arts\Dark Age of Camelot";
        }
        private void UpdateCharNameAutoComplete()
        {
            CharacterList = new ParseDirectory(Folder + CharFolder).Characters;
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
            InitializeComponent();
            _ = BackUpRealmComboBox.Items.Add("Albion");
            _ = BackUpRealmComboBox.Items.Add("Hibernia");
            _ = BackUpRealmComboBox.Items.Add("Midgard");
            _ = RestoreRealmComboBox.Items.Add("Albion");
            _ = RestoreRealmComboBox.Items.Add("Hibernia");
            _ = RestoreRealmComboBox.Items.Add("Midgard");
            DAoCDirectoryTextBox.Text = DefaultLocation();
            RestoreClassComboBox.Enabled = false;
            RestoreClassLabel.Enabled = false;
            ClearFilterButton.Enabled = false;
            Folder = DAoCDirectoryTextBox.Text;
            UpdateCharNameAutoComplete();

            //CharacterList = new ParseDirectory(Folder + CharFolder).Characters;
            List<string?> serverList = GetServerList() ?? throw new NullReferenceException("ServerList is null");
            ServerNames = serverList;
            CreateDataTable();
            ReDrawTable();
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
            Folder = DAoCDirectoryTextBox.Text;
            CopyFromComboBox.Items.Clear();
            CopyFromComboBox.ResetText();
            CopyToComboBox.Items.Clear();
            CopyToComboBox.ResetText();
            BackUpNameComboBox.Items.Clear();
            BackUpNameComboBox.ResetText();
            CharacterList = new ParseDirectory(Folder + CharFolder).Characters;
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
            string fromCharacterPath = DAoCDirectoryTextBox.Text + CharFolder + $"\\{CopyFromComboBox.Text.Split('(').First()}-{GetServerIndex(FromServerText.Text)}.ini".Replace(" ", "");
            string toCharacterPath = DAoCDirectoryTextBox.Text + CharFolder + $"\\{CopyToComboBox.Text.Split('(').First()}-{GetServerIndex(CopyToTextBox.Text)}.ini".Replace(" ", "");
            CharacterINI fromCharacter = new(fromCharacterPath);
            CharacterINI toCharacter = new(toCharacterPath);
            CopyCharacterIniData(fromCharacter, toCharacter);
            ResetCopySettingsTab();
        }
        private void NewCharacterSaveButton_Click(object sender, EventArgs e)
        {
            string fromCharacterPath = DAoCDirectoryTextBox.Text + CharFolder + $"\\{CopyFromComboBox.Text.Split('(').First()}-{GetServerIndex(FromServerText.Text)}.ini".Replace(" ", "");
            string toCharacterPath = DAoCDirectoryTextBox.Text + CharFolder + $"\\{NewCharacterNameTextBox.Text}-{GetServerIndex(NewCharacterServerComboBox.Text)}.ini".Replace(" ", "");
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
            string fromCharacterPath = DAoCDirectoryTextBox.Text + CharFolder + $"\\{CopyFromComboBox.Text.Split('(').First()}-{GetServerIndex(FromServerText.Text)}.ini".Replace(" ", "");
            CharacterINI fromCharacter = new(fromCharacterPath);
            Dictionary<string, int> characterList = CharacterList;
            foreach (KeyValuePair<string, int> character in characterList)
            {
                string toCharacterPath = DAoCDirectoryTextBox.Text + CharFolder + $"\\{character.Key.Split('(').First()}-{character.Value}.ini".Replace(" ", "");
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
            Folder = DAoCDirectoryTextBox.Text;
            CharacterList = new ParseDirectory(Folder + CharFolder).Characters;
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
            string characterPath = DAoCDirectoryTextBox.Text + CharFolder + $"\\{BackUpNameComboBox.Text.Split('(').First()}-{GetServerIndex(BackUpServerTextBox.Text)}.ini".Replace(" ", "");
            CharacterINI characterIniData = new(characterPath);

            if (CharacterRepository is null)
            {
                throw new NullReferenceException("Could not load backup file.");
            }

            RecordRepository characterRepository = CharacterRepository;
            int recordCount = characterRepository.Count;

            CharacterRecord characterRecord = new()
            {
                Name = BackUpNameComboBox.Text,
                Realm = BackUpRealmComboBox.Text,
                Class = BackUpClassComboBox.Text,
                Description = BackUpDescriptionTextBox.Text,
                CharacterINI = characterIniData,
                Server = BackUpServerTextBox.Text,
                Index = (recordCount == 0) ? 0 : recordCount
            };

            CharacterRepository ??= new();
            if (CharacterRepository.Characters is null)
            {
                CharacterRepository.Characters = new();
            }

            CharacterRepository.Characters.Add(characterRecord);
            string json = SerializeRepository(CharacterRepository);
            WriteRepository(json);
            Logger.Debug($"Successfully added {characterRecord.Name} to the repository.");

            CreateDataTable();
            ReDrawTable();
            ResetCharacterBackupTab();
        }
        private void BackUpComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BackUpNameComboBox.SelectedItem is not null)
            {
                string? selectedItem = BackUpNameComboBox.SelectedItem.ToString();
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

            CharacterList = new ParseDirectory(Folder + CharFolder).Characters;
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
            CharacterList = new ParseDirectory(Folder + CharFolder).Characters;
            foreach (KeyValuePair<string, int> character in CharacterList)
            {
                _ = CopyFromComboBox.Items.Add(character.Key);
                _ = CopyToComboBox.Items.Add(character.Key);
                _ = BackUpNameComboBox.Items.Add(character.Key);
            }
        }
        #endregion

        #region Restore Tab
        private static void CreateDataTable()
        {
            DataTable dt = new();
            _ = dt.Columns.Add("Name", typeof(string));
            _ = dt.Columns.Add("Realm", typeof(string));
            _ = dt.Columns.Add("Class", typeof(string));
            _ = dt.Columns.Add("Server", typeof(string));
            _ = dt.Columns.Add("Description", typeof(string));
            _ = dt.Columns.Add("Index", typeof(int));
            RecordRepository? characterRespository = CharacterRepository;
            if (characterRespository is not null)
            {
                if (characterRespository.Count > 0)
                {
                    if (characterRespository.Characters is not null)
                    {
                        foreach (CharacterRecord characterBackUp in characterRespository.Characters)
                        {
                            DataRow row = dt.NewRow();
                            row["Name"] = characterBackUp.Name;
                            row["Realm"] = characterBackUp.Realm ?? "";
                            row["Class"] = characterBackUp.Class ?? "";
                            row["Server"] = characterBackUp.Server ?? "";
                            row["Description"] = characterBackUp.Description ?? "";
                            row["Index"] = characterBackUp.Index;
                            dt.Rows.Add(row);
                        }
                    }
                }
                RestoreDataTable = dt;
            }
        }
        private void ReDrawTable()
        {
            restoreDataGridView.DataSource = RestoreDataTable;
            restoreDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            restoreDataGridView.Columns["Index"].Visible = false;
            restoreDataGridView.Columns["Realm"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            restoreDataGridView.Columns["Class"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            restoreDataGridView.Columns["Server"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            restoreDataGridView.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            restoreDataGridView.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (restoreDataGridView.Rows.Count < 1)
            {
                RestoreButton.Enabled = false;
                RestoreDeleteRecordButton.Enabled = false;
            }
            else
            {
                RestoreButton.Enabled = true;
                RestoreDeleteRecordButton.Enabled = true;
            }
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
            RestoreButton.Enabled = true;
            RestoreDeleteRecordButton.Enabled = true;
        }
        private void RestoreRealmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RestoreRealmComboBox.Text))
            {
                PopulateClasses(RestoreRealmComboBox.Text);
                RestoreClassComboBox.Enabled = true;
                RestoreClassLabel.Enabled = true;
                DataTable dt = (restoreDataGridView.DataSource as DataTable) ?? throw new NullReferenceException();
                dt.DefaultView.RowFilter = $"Realm = '{RestoreRealmComboBox.Text}'";
                Logger.Debug($"Applied filter: \"{$"Realm = '{RestoreRealmComboBox.Text}'"}\" to restoreDataGridView.");
                ReDrawTable();
                ClearFilterButton.Enabled = true;
            }
            else
            {
                RestoreClassComboBox.Items.Clear();
                RestoreClassComboBox.ResetText();
                RestoreClassComboBox.Enabled = false;
                RestoreClassLabel.Enabled = false;
            }
        }
        private void RestoreClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RestoreClassComboBox.Text))
            {
                DataTable? dt = restoreDataGridView.DataSource as DataTable;
                if (dt is not null)
                {
                    dt.DefaultView.RowFilter = $"Class = '{RestoreClassComboBox.Text}'";
                    ReDrawTable();
                    ClearFilterButton.Enabled = true;
                    Logger.Debug($"Applied filter: \"{$"Class = '{RestoreClassComboBox.Text}'"}\" to restoreDataGridView.");
                }
            }
        }
        private void ResetFilters()
        {
            DataTable? dt = restoreDataGridView.DataSource as DataTable;
            if (dt is not null)
            {
                dt.DefaultView.RowFilter = string.Empty;
                Logger.Debug($"Cleared filters from restoreDataGridView.");
            }
        }
        private void ClearFilterButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("ClearFilterButton clicked.");
            ResetFilters();
            ResetCharacterRestoreTab();
        }
        private void ResetCharacterRestoreTab()
        {
            RestoreDeleteRecordButton.Enabled = false;
            RestoreButton.Enabled = false;
            RestoreRealmComboBox.ResetText();
            RestoreClassComboBox.Items.Clear();
            RestoreClassComboBox.ResetText();
            RestoreClassComboBox.Enabled = false;
        }
        private void RestoreButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("RestoreButton clicked.");
            DataGridViewSelectedRowCollection rowsToRestore = restoreDataGridView.SelectedRows;
            Logger.Debug($"There are {rowsToRestore.Count} selected rows.");
            List<CharacterRecord>? characters = CharacterRepository?.Characters;
            foreach (DataGridViewRow row in rowsToRestore)
            {
                if (row is not null)
                {
                    DataGridViewCell cell = row.Cells["Index"];
                    if (cell is not null)
                    {
                        string? strIndex = cell.Value?.ToString();
                        if (int.TryParse(strIndex, out int index))
                        {
                            CharacterRecord? characterToRestore = characters?.Where(x => x.Index == index).First();
                            CharacterINI? iniToWrite = characterToRestore?.CharacterINI;
                            if (iniToWrite is not null)
                            {
                                WriteCharacterIni.WriteCharacter(iniToWrite.FilePath, iniToWrite);
                                Logger.Debug($"Successfully wrote to {iniToWrite.FilePath}");
                            }
                        }
                    }
                }
            }
            ResetFilters();
            ResetCharacterRestoreTab();
        }
        private void RestoreDeleteRecordButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("RestoreDeleteRecordButton clicked.");
            DataGridViewSelectedRowCollection rowsToDelete = restoreDataGridView.SelectedRows;
            Logger.Debug($"There are {rowsToDelete.Count} selected rows.");
            List<CharacterRecord>? characters = (CharacterRepository?.Characters) ?? throw new NullReferenceException("CharacterRepository does not contain any records.");
            foreach (DataGridViewRow row in rowsToDelete)
            {
                if (row is not null)
                {
                    DataGridViewCell cell = row.Cells["Index"];
                    if (cell is not null)
                    {
                        string? strIndex = cell.Value.ToString();
                        if (int.TryParse(strIndex, out int index))
                        {
                            CharacterRecord? characterToDelete = characters?.Where(x => x.Index == index).First();
                            if (characterToDelete is not null)
                            {
                                _ = (characters?.Remove(characterToDelete));
                                Logger.Debug($"Successfully removed to {characterToDelete.Name} (Index:{characterToDelete.Index}) from the respository.");
                            }
                        }
                    }
                }
            }

            //ReIndex remaining character backup records.
            for (int index = 0; index < characters.Count; index++)
            {
                characters[index].Index = index;
            }

            if (CharacterRepository is null)
            {
                CharacterRepository = new()
                {
                    Characters = characters
                };
            }
            else
            {
                CharacterRepository.Characters = characters;
            }

            //Save remaining backups to disk
            string json = SerializeRepository(CharacterRepository);
            WriteRepository(json);
            Logger.Debug($"Successfully wrote {characters.Count} characters to {RepositoryFullPath}.");

            CreateDataTable();
            ReDrawTable();
            ResetFilters();
            ResetCharacterRestoreTab();
        }
        #endregion

        #region RecordRepository
        private static readonly object repositoryLock = new();
        private static RecordRepository LoadCharacterRepository()
        {
            RecordRepository characterRepository = new();
            if (File.Exists(RepositoryFullPath))
            {
                try
                {
                    string json = ReadRepository();
                    characterRepository = DeserializeRepository(json);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
            characterRepository.Characters ??= new List<CharacterRecord>();
            Logger.Debug($"Successfully loaded character repository from disk. {characterRepository.Count} characters in Repository.");
            return characterRepository;
        }
        private static void WriteRepository(string json)
        {
            lock (repositoryLock)
            {
                try
                {
                    File.WriteAllText(RepositoryFullPath, json);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }
        private static string ReadRepository()
        {
            lock (repositoryLock)
            {
                try
                {
                    string json = File.ReadAllText(RepositoryFullPath);
                    return json;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return "{}";
                }
            }
        }
        private static string SerializeRepository(RecordRepository characterRepository)
        {
            try
            {
                string output = JsonConvert.SerializeObject(characterRepository);
                return output;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return "{}";
            }
        }
        private static RecordRepository DeserializeRepository(string json)
        {
            try
            {
                RecordRepository input = JsonConvert.DeserializeObject<RecordRepository>(json) ?? new();
                return input;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new();
            }
        }
        #endregion
    }
}