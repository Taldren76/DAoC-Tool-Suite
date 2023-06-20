using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using DAoCToolSuite.ChimpTool.Exceptions;
using DAoCToolSuite.ChimpTool.Extensions;
using DAoCToolSuite.ChimpTool.HeraldAPI;
using DAoCToolSuite.ChimpTool.Json;
using DAoCToolSuite.ChimpTool.Selenium;
using DAoCToolSuite.ChimpTool.Settings;
using Logger;
using Logger.Enums;
using Newtonsoft.Json;
using SQLLibrary;
using SQLLibrary.Enums;

namespace DAoCToolSuite.ChimpTool
{
    public partial class MainForm : Form
    {
        public static System.Windows.Forms.Timer Timer { get; set; } = new();
        internal static WaitCursor WaitCursor { get; set; } = new();
        private static System.Windows.Forms.ToolTip MouseOverTooltip { get; set; } = new();
        private static List<AccountModel> Accounts { get; set; } = new List<AccountModel>();
        private static List<GuildModel> Guilds { get; set; } = new List<GuildModel>();

        #region DataGridView Data
        private static List<CharacterModel> Characters { get; set; } = new List<CharacterModel>();
        private static List<CharacterModel>? CharactersByAccountLastDateUpdated { get; set; } = new List<CharacterModel>();
        private static List<CharacterModel>? CharactersByAccountFirstDateUpdated { get; set; } = new List<CharacterModel>();
        private static List<CharacterModel>? CharactersLastDateOnly { get; set; } = new List<CharacterModel>();
        private BindingSource BindingSource { get; set; } = new();
        #endregion

        #region Logger
        private static Logger.LogManager Logger => LogManager.Instance;
        #endregion

        #region Settings
        private static SettingsManager? _settings = null;
        public static SettingsManager Settings
        {
            get
            {
                _settings ??= new SettingsManager();

                return _settings;

            }
            set => _settings = value;
        }
        private static bool UseSelenium { get; set; } = Settings.UseSelenium;
        private static bool UseAPI { get; set; } = Settings.UseAPI;
        private static string LastAccount
        {
            get => Settings.LastAccount;
            set => Settings.LastAccount = value;
        }
        private static bool AlwaysOnTop
        {
            get => Settings.AlwaysOnTop;
            set => Settings.AlwaysOnTop = value;
        }
        #endregion

        #region MainForm
        public MainForm()
        {
            WaitCursor.Push();
            InitializeComponent();
            LoadAccounts();
            SetToLastAccount();
            SetAutoCompleteCharacterList();
            SearchGridView.DataSource = BindingSource;
            RestoreButton.Enabled = HasBackupChimpRepository();
            SearchButton.Enabled = UseSelenium || UseAPI;
            SearchComboBox.Enabled = UseSelenium || UseAPI;
            Shown += new System.EventHandler(MainForm_Shown!);
            Timer.Tick -= new EventHandler(MainForm_TimerHandler);
            Timer.Tick += new EventHandler(MainForm_TimerHandler);
            Timer.Interval = 1000;
            Timer.Start();
            RefreshAllTimer = DateTime.Now < Properties.Settings.Default.NextRefreshAll;
            RefreshTimer = DateTime.Now < Properties.Settings.Default.NextRefresh;
        }

        private static bool RefreshAllTimer = false;
        private static bool RefreshTimer = false;
        private void MainForm_TimerHandler(object? sender, EventArgs e)
        {
            #region Refresh All Button State
            if (RefreshAllTimer && DateTime.Now >= Properties.Settings.Default.NextRefreshAll)
            {
                RefreshAllTimer = false;
                RefreshAllButton.Text = "Refresh All";
                RefreshAllButton.Enabled = true;
            }
            else if (RefreshAllTimer && DateTime.Now < Properties.Settings.Default.NextRefreshAll)
            {
                TimeSpan Diff = Properties.Settings.Default.NextRefreshAll - DateTime.Now;
                RefreshAllButton.Text = Convert.ToInt32(Diff.TotalSeconds).ToString();
                RefreshAllButton.Enabled = false;
            }
            else if (!RefreshAllTimer && BindingSource.Count > 0)
            {
                RefreshAllButton.Text = "Refresh All";
                RefreshAllButton.Enabled = true;
            }
            else if (!RefreshAllTimer && BindingSource.Count < 1)
            {
                RefreshAllButton.Text = "Refresh All";
                RefreshAllButton.Enabled = false;
            }
            #endregion
            #region Refresh Button State
            if (RefreshTimer && DateTime.Now >= Properties.Settings.Default.NextRefresh)
            {
                RefreshTimer = false;
                RefreshButton.Text = "Refresh";
                RefreshButton.Enabled = true;
            }
            else if (RefreshTimer && DateTime.Now < Properties.Settings.Default.NextRefresh)
            {
                TimeSpan Diff = Properties.Settings.Default.NextRefresh - DateTime.Now;
                RefreshButton.Text = Convert.ToInt32(Diff.TotalSeconds).ToString();
                RefreshButton.Enabled = false;
            }
            else if (!RefreshTimer && BindingSource.Count > 0)
            {
                RefreshButton.Text = "Refresh";
                RefreshButton.Enabled = true;
            }
            else if (!RefreshTimer && BindingSource.Count < 1)
            {
                RefreshButton.Text = "Refresh";
                RefreshButton.Enabled = false;
            }
            #endregion
            #region Debug Log Button State
            switch (Logger.CurrentDebugLevel)
            {
                case DebugLevel.Error:
                    DebugLogButton.BackColor = Color.Red;
                    break;
                case DebugLevel.Warning:
                    DebugLogButton.BackColor = Color.Yellow;
                    break;
                default:
                    break;
            }
            #endregion
        }

        private void MainForm_Shown(object? sender, EventArgs e)
        {
            bool check = AlwaysOnTop;
            //OnTopCheckBox.Checked = check;
            GridPanel.Visible = true;
            LoadCharacters();
            CalculateRPTotals();
            WaitCursor.PopAll();
        }

        private void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            FormClosing -= new FormClosingEventHandler(MainForm_FormClosing);
            Thread.Sleep(1000);
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Logger.Debug($"Shutting down.");
            Timer.Stop();
            Shown -= new System.EventHandler(MainForm_Shown);
            Timer.Tick -= new EventHandler(MainForm_TimerHandler);

            if (sender is not MainForm form)
            {
                return;
            }
            form.Hide();
            Properties.Settings.Default.WindowLocation = form.Location;
            Properties.Settings.Default.Save();
            CamelotHerald.Quit();

            ShutDown sd = new()
            {
                StartPosition = FormStartPosition.Manual,
                Owner = form,
                Enabled = true,
                TopMost = true
            };
            sd.SetLocation();
            sd.Show();
            sd.Refresh();
        }

        private void SearchGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private AccountModel AccountComboBoxAccount => Accounts?.Where(x => x.Account == AccountComboBox.Text)?.FirstOrDefault() ?? new AccountModel();
        private int AccountComboBoxIndex => AccountComboBoxAccount == null ? -1 : AccountComboBox.Items.IndexOf(AccountComboBoxAccount);

        private void SetToLastAccount()
        {
            int indexLast = 0;
            AccountModel? accountLast = Accounts?.Where(x => x.Account == LastAccount)?.FirstOrDefault();
            if (accountLast is not null)
            {
                indexLast = AccountComboBox.Items.IndexOf(accountLast);
            }

            Logger.Debug($"Startup Account:{AccountComboBoxAccount?.Account ?? "null"}({AccountComboBoxIndex}) => LastAccount: {LastAccount}({indexLast})");

            AccountComboBox.SelectedIndex = indexLast;

            if (AccountComboBox.SelectedIndex == 0)  //If last and startup are both 0, IndexChange  
            {                                       //event isn't launched and these never get called.
                LoadCharacters();                   //Results in table with no color/formatting.
                CalculateRPTotals();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            long currentCount = ChimpTool.Properties.Settings.Default.LoadCount;
            if (currentCount > 0)
            {
                Location = ChimpTool.Properties.Settings.Default.WindowLocation;
            }
            else
            {
                //ScreenCentered by Default
                ChimpTool.Properties.Settings.Default.WindowLocation = Location;
            }
            if (currentCount != long.MaxValue)
            {
                ChimpTool.Properties.Settings.Default.LoadCount = currentCount + 1;
            }
            ChimpTool.Properties.Settings.Default.Save();
        }
        private void OnTopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is not CheckBox checkbox)
            {
                return;
            }

            MainForm.ActiveForm.TopMost = checkbox.Checked;
            AlwaysOnTop = checkbox.Checked;
            Logger.Debug($"AlwaysOnTop.Checked = {checkbox.Checked}");
            Logger.Debug($"Settings.AlwaysOnTop = {AlwaysOnTop}");
        }
        #endregion

        #region RP Totals
        private static int AlbionRpTotal { get; set; } = 0;
        private static int HiberniaRpTotal { get; set; } = 0;
        private static int MidgardRpTotal { get; set; } = 0;
        private void CalculateRPTotals()
        {
            WaitCursor.Push();
            Logger.Debug("Calculating RP totals");
            AlbionRpTotal = 0;
            HiberniaRpTotal = 0;
            MidgardRpTotal = 0;
            if (CharactersByAccountLastDateUpdated is null)
            {
                return;
            }

            List<CharacterModel> characters = CharactersByAccountLastDateUpdated;
            foreach (CharacterModel character in characters)
            {

                if (character.Realm == "Albion")
                {
                    AlbionRpTotal += character.TotalRealmPoints ?? 0;
                }

                if (character.Realm == "Hibernia")
                {
                    HiberniaRpTotal += character.TotalRealmPoints ?? 0;
                }

                if (character.Realm == "Midgard")
                {
                    MidgardRpTotal += character.TotalRealmPoints ?? 0;
                }

            }
            AlbionTotalsTextBox.Text = AlbionRpTotal.ToString("N0");
            HiberniaTotalsTextBox.Text = HiberniaRpTotal.ToString("N0");
            MidgardTotalsTextBox.Text = MidgardRpTotal.ToString("N0");
            TotalRPTextBox.Text = (AlbionRpTotal + HiberniaRpTotal + MidgardRpTotal).ToString("N0");
            WaitCursor.Pop();
        }
        #endregion

        #region SearchBox Autocomplete
        private static Dictionary<string, int>? _characterList = null;
        private static Dictionary<string, int> CharacterList
        {
            get
            {
                _characterList ??= new ParseDirectory(DefaultLocation()).Characters;

                return _characterList;
            }
            set => _characterList = value;
        }
        private static string DefaultLocation()
        {
            string sPath = Settings.DAoCCharacterFileDirectory;
            return sPath;
        }
        private void SetAutoCompleteCharacterList()
        {

            SearchComboBox.AutoCompleteCustomSource.AddRange(CharacterList.Keys.ToArray());
            SearchComboBox.Items.AddRange(CharacterList.Keys.ToArray());
        }
        #endregion

        #region Debug.Log DataLink

        private static readonly object thisLock = new();

        private void DebugLog_Click(object sender, EventArgs e)
        {
            DebugLogButton.Enabled = false;
            try
            {
                string contents;
                lock (thisLock)
                {
                    FileAttributes attributes = File.GetAttributes(Logger.PATH);
                    using (FileStream fs = new(Logger.PATH, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using StreamReader sr = new(fs);
                        contents = sr.ReadToEnd();
                    }
                    File.SetAttributes(Logger.PATH, attributes);
                }

                using LogViewer logViewer = new();
                logViewer.LogViewerTextBox.Text = contents;
                logViewer.Location = Location;
                logViewer.Size = Size;
                logViewer.LogViewerTextBox.SelectionStart = logViewer.LogViewerTextBox.TextLength;
                logViewer.LogViewerTextBox.ScrollToCaret();
                _ = logViewer.ShowDialog();
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
            }
            DebugLogButton.Enabled = true;
        }
        #endregion

        #region Json Backup and Restore to DB
        private static readonly string BackupRepositoryFullPath = Settings.JsonBackupFileFullPath;
        private static readonly object repositoryLock = new();
        private static ChimpRepository LoadChimpRepository()
        {
            WaitCursor.Push();
            Logger.Debug("Loading repository from disk.");
            ChimpRepository chimpRepository = new();
            if (File.Exists(BackupRepositoryFullPath))
            {
                try
                {
                    string json = ReadRepository();
                    chimpRepository = DeserializeRepository(json);
                    if (chimpRepository.AccountCount > 0)
                    {
                        Logger.Debug($"Successfully loaded {chimpRepository.AccountCount} accounts and {chimpRepository.CharacterCount} characters from disk.");
                    }
                }
                catch (System.Exception e)
                {
                    Logger.Error(e);
                }
            }
            else
            {
                Logger.Warn($"No valid character repository file found. A new file will be created.");
                chimpRepository = new ChimpRepository();
            }
            WaitCursor.Pop();
            return chimpRepository;
        }
        private static ChimpRepository CreateChimpRepository()
        {
            ChimpRepository chimps = new();
            foreach (AccountModel account in Accounts)
            {
                string? accountName = account.Account;
                List<ChimpJson>? characters = CharactersLastDateOnly?.Where(x => x.Account == account.Account)?.Select(x => x.CovertToChimp())?.ToList();
                if (accountName is null || characters is null)
                {
                    continue;
                }

                chimps.Add(accountName, characters);
            }
            return chimps;
        }
        private static void BackupToChimpRepository()
        {
            WaitCursor.Push();
            string json = SerializeRepository(CreateChimpRepository());
            WriteRepository(json);
            WaitCursor.Pop();
        }
        private static bool HasBackupChimpRepository()
        {
            return File.Exists(BackupRepositoryFullPath);
        }
        private static void RestoreFromChimpRepository()
        {
            Logger.Debug($"Restoring from backup file {BackupRepositoryFullPath}");
            WaitCursor.Push();
            if (!HasBackupChimpRepository())
            {
                return;
            }

            try
            {
                SqliteDataAccess.ResetTables();

                ChimpRepository chimpRepository = LoadChimpRepository();
                foreach (KeyValuePair<string, List<ChimpJson>> account in chimpRepository.Chimps)
                {
                    SqliteDataAccess.AddAccount(account.Key);
                    foreach (ChimpJson chimp in account.Value)
                    {
                        SqliteDataAccess.AddCharacter(chimp.ConvertToCharacterModel(), DateTime.Now, account.Key);
                        SqliteDataAccess.AddGuild(chimp.ConvertToGuildModel());
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
            }
            WaitCursor.Pop();
        }
        private static void WriteRepository(string json)
        {
            lock (repositoryLock)
            {
                try
                {
                    Logger.Debug($"Writting backup to {BackupRepositoryFullPath}");
                    File.WriteAllText(BackupRepositoryFullPath, json);
                }
                catch (System.Exception ex)
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
                    string json = File.ReadAllText(BackupRepositoryFullPath);
                    return json;
                }
                catch (System.Exception ex)
                {
                    Logger.Error(ex);
                    return "{}";
                }
            }
        }
        private static string SerializeRepository(ChimpRepository chimpRepository)
        {
            try
            {
                string output = JsonConvert.SerializeObject(chimpRepository);
                return output;
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
                return "{}";
            }
        }
        private static ChimpRepository DeserializeRepository(string json)
        {
            try
            {
                ChimpRepository input = JsonConvert.DeserializeObject<ChimpRepository>(json) ?? new ChimpRepository();
                return input;
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
                return new ChimpRepository();
            }

        }
        private void BackupButton_Click(object sender, EventArgs e)
        {
            BackupButton.Enabled = false;
            WaitCursor.Push();
            Logger.Debug("Backup button has been pressed.");
            LoadCharacters();
            BackupToChimpRepository();
            Logger.Debug($"Backup created at {BackupRepositoryFullPath}");
            RestoreButton.Enabled = File.Exists(BackupRepositoryFullPath);
            WaitCursor.Pop();
            BackupButton.Enabled = true;

        }
        private void RestoreButton_Click(object sender, EventArgs e)
        {
            RestoreButton.Enabled = false;
            WaitCursor.Push();
            Logger.Debug("Restore button has been pressed.");
            SearchGridView.Visible = false;
            LoadingTabelLabel.Visible = true;
            GridPanel.Refresh();
            if (File.Exists(BackupRepositoryFullPath))
            {
                EnableSelectIndexChangedEvent = false;
                RestoreFromChimpRepository();
                Logger.Debug($"Backup restored to {BackupRepositoryFullPath}");
                LoadAccounts();
                EnableSelectIndexChangedEvent = true;
                LoadCharacters();
                CalculateRPTotals();
            }
            else
            {
                Logger.Debug($"No backup chimp repository found at {BackupRepositoryFullPath}");
                RestoreButton.Enabled = false;
            }
            WaitCursor.Pop();
            RestoreButton.Enabled = true;
        }
        #endregion

        #region DataGridView
        private void UpdateCharacterLists()
        {
            string account;
            if (Characters.Count > 0)
            {
                account = AccountComboBox.Items.Count > 0 && AccountComboBox.Text != "SQLLibrary.AccountModel"
                    ? AccountComboBox.Text.ToString()
                    : "Default";
                List<CharacterModel> charactersLastDateOnly = Characters.GroupBy(x => x.WebID)
                                                                        .Select(x => x.OrderByDescending(y => y.DateTime))
                                                                        .Select(x => x.First())
                                                                        .OrderBy(x => x.Realm)
                                                                        .ThenByDescending(x => x.TotalRealmPoints)
                                                                        .ThenByDescending(x => x.Name)
                                                                        .ToList();


                List<CharacterModel> charactersByAccountLastDateUpdated = Characters.Where(x => x.Account == account)
                                                                                    .GroupBy(x => x.WebID)
                                                                                    .Select(x => x.OrderByDescending(y => y.DateTime))
                                                                                    .Select(x => x.First())
                                                                                    .OrderBy(x => x.Realm)
                                                                                    .ThenByDescending(x => x.TotalRealmPoints)
                                                                                    .ThenByDescending(x => x.Name)
                                                                                    .ToList();


                List<CharacterModel> charactersByAccountFirstDateUpdated = Characters.Where(x => x.Account == account)
                                                                                     .GroupBy(x => x.WebID)
                                                                                     .Select(x => x.OrderByDescending(y => y.DateTime))
                                                                                     .Select(x => x.Last())
                                                                                     .OrderBy(x => x.Realm)
                                                                                     .ThenByDescending(x => x.TotalRealmPoints)
                                                                                     .ThenByDescending(x => x.Name)
                                                                                     .ToList();
                foreach (CharacterModel character in charactersByAccountLastDateUpdated)
                {
                    if (character?.WebID is null)
                    {
                        continue;
                    }

                    string webID = character.WebID;
                    int latestRP = charactersByAccountLastDateUpdated.Where(x => x.WebID == webID).Select(x => x.TotalRealmPoints ?? 0).First();
                    int earliestRP = charactersByAccountFirstDateUpdated.Where(x => x.WebID == webID).Select(x => x.TotalRealmPoints ?? 0).First();
                    character.RPLastUpdate = latestRP - earliestRP;
                    //if (character.RPLastUpdate > 0)
                    //    Thread.Sleep(1);
                }
                CharactersLastDateOnly = null;
                CharactersLastDateOnly = charactersLastDateOnly;
                CharactersByAccountLastDateUpdated = null;
                CharactersByAccountLastDateUpdated = charactersByAccountLastDateUpdated;
                CharactersByAccountFirstDateUpdated = null;
                CharactersByAccountFirstDateUpdated = charactersByAccountFirstDateUpdated;


                //DataView view = charactersByAccountLastDateUpdated.ToDataTable().DefaultView;
                //view.Sort = "Name ASC, TotalRealmPoints DESC";
                //BindingSource = view;

            }
            else
            {
                CharactersLastDateOnly = null;
                CharactersLastDateOnly = new List<CharacterModel>();
                CharactersByAccountLastDateUpdated = null;
                CharactersByAccountLastDateUpdated = new List<CharacterModel>();
                CharactersByAccountFirstDateUpdated = null;
                CharactersByAccountFirstDateUpdated = new List<CharacterModel>();
            }
        }
        private void SearchGridView_CellToolTipTextNeeded(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is not DataGridView dataGridView || CharactersByAccountLastDateUpdated is null)
            {
                return;
            }

            DataGridViewColumn column = dataGridView.Columns[e.ColumnIndex];
            if (e.RowIndex != -1 && column.Visible == true)
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                CharacterModel character = CharactersByAccountLastDateUpdated[e.RowIndex]; //.Where(x => x.WebID == webID).First();
                string columnName = column.Name;
                string? realm = row?.Cells["Realm"]?.Value?.ToString()?.ToLower();
                if (realm is null)
                {
                    return;
                }

                string? guild = Guilds.Where(x => x.WebID == character.Guild_WebID).Select(x => x.Name).FirstOrDefault();
                string albKills = character.Albion_Kills?.ToString("N0") ?? "0";
                string midKills = character.Midgard_Kills?.ToString("N0") ?? "0";
                string hibKills = character.Hibernia_Kills?.ToString("N0") ?? "0";

                string toShow = string.Empty;
                switch (columnName)
                {
                    case "Name":
                        toShow = $"Level {character.Level} {character.Race}";
                        if (!string.IsNullOrEmpty(guild))
                        {
                            toShow += $" - {guild}";
                        }

                        break;
                    case "TotalSoloKills":
                        switch (realm)
                        {
                            case "albion":
                                toShow += $"Hibernia:{character.Hibernia_SoloKills?.ToString("N0") ?? "0"} Midgard:{character.Midgard_SoloKills?.ToString("N0") ?? "0"}";
                                break;
                            case "midgard":
                                toShow += $"Albion:{character.Albion_SoloKills?.ToString("N0") ?? "0"} Hibernia:{character.Hibernia_SoloKills?.ToString("N0") ?? "0"}";
                                break;
                            case "hibernia":
                                toShow += $"Albion:{character.Albion_SoloKills?.ToString("N0") ?? "0"} Midgard:{character.Midgard_SoloKills?.ToString("N0") ?? "0"}";
                                break;
                            default:
                                break;
                        }
                        break;
                    case "TotalDeathBlows":
                        switch (realm)
                        {
                            case "albion":
                                toShow += $"Hibernia:{character.Hibernia_DeathBlows?.ToString("N0") ?? "0"} Midgard:{character.Midgard_DeathBlows?.ToString("N0") ?? "0"}";
                                break;
                            case "midgard":
                                toShow += $"Albion:{character.Albion_DeathBlows?.ToString("N0") ?? "0"} Hibernia:{character.Hibernia_DeathBlows?.ToString("N0") ?? "0"}";
                                break;
                            case "hibernia":
                                toShow += $"Albion:{character.Albion_DeathBlows?.ToString("N0") ?? "0"} Midgard:{character.Midgard_DeathBlows?.ToString("N0") ?? "0"}";
                                break;
                            default:
                                break;
                        }
                        break;
                    case "TotalKills":
                        switch (realm)
                        {
                            case "albion":
                                toShow += $"Hibernia:{character.Hibernia_Kills?.ToString("N0") ?? "0"} Midgard:{character.Midgard_Kills?.ToString("N0") ?? "0"}";
                                break;
                            case "midgard":
                                toShow += $"Albion:{character.Albion_Kills?.ToString("N0") ?? "0"} Hibernia:{character.Hibernia_Kills?.ToString("N0") ?? "0"}";
                                break;
                            case "hibernia":
                                toShow += $"Albion:{character.Albion_Kills?.ToString("N0") ?? "0"} Midgard:{character.Midgard_Kills?.ToString("N0") ?? "0"}";
                                break;
                            default:
                                break;
                        }
                        break;
                }
                // get x and y position of cell		response	error CS0103: 

                Rectangle rec = dataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Point p = rec.Location;
                // add offset to move tip up
                p.Offset(0, -40);
                // changed dataGridView1 to panel1, use offset cell position, and timeout

                MouseOverTooltip.BackColor = Color.White;
                MouseOverTooltip.ForeColor = Color.Black;
                MouseOverTooltip.Show(toShow, GridPanel, p, 10000); // show tool tip
            }
        }

        private void SearchGridView_DataSourceChanged(object sender, EventArgs e)
        {
            UpdateCharacterLists();
            DataGridView thisSearchGrid = (DataGridView)sender;
            thisSearchGrid.FormatTable();
            if (thisSearchGrid.Rows.Count > 0)
            {
                RefreshButton.Enabled = true;
                RefreshAllButton.Enabled = true;
            }
            else
            {
                RefreshButton.Enabled = false;
                RefreshAllButton.Enabled = false;
            }

        }
        #endregion

        #region Characters (Add, Remove, Refresh)
        private void LoadCharacters()
        {
            SearchGridView.Visible = false;
            LoadingTabelLabel.Visible = true;
            GridPanel.Refresh();

            Characters = SqliteDataAccess.LoadCharacters();
            Guilds = SqliteDataAccess.LoadGuilds();

            UpdateCharacterLists();
            AttachCharacters();
            SearchGridView.FormatTable(SearchProgressBar);
            LoadingTabelLabel.Visible = false;
            SearchGridView.Visible = true;
            GridPanel.Refresh();

            if ((CharactersByAccountLastDateUpdated is null || CharactersByAccountLastDateUpdated.Count == 0) && DateTime.Now > Properties.Settings.Default.NextLoadAll)
            {
                LoadAllButton.Visible = true;
            }
            else
            {
                LoadAllButton.Visible = false;
            }
        }

        private void AttachCharacters()
        {
            BindingSource.DataSource = CharactersByAccountLastDateUpdated?.ToChimpJsonList() ?? new();
        }
        private void LoadAllButton_Click(object sender, EventArgs e)
        {
            LoadAllButton.Enabled = false;
            DialogResult dlg = MessageBox.Show($"WARNING: This can only be used once per 24 hours.\nDo you wish to proceed?", "Load All Characters", MessageBoxButtons.YesNo);
            if (dlg == DialogResult.No)
            {
                LoadAllButton.Enabled = true;
                return;
            }
            Properties.Settings.Default.NextLoadAll = DateTime.Now.AddDays(1);
            Properties.Settings.Default.Save();

            LoadAllButton.Visible = false;

            SearchProgressBar.Minimum = 0;
            SearchProgressBar.Maximum = CharacterList.Count;
            SearchProgressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
            SearchProgressBar.Value = 0;
            SearchProgressBar.CustomText = "Loading Characters";
            SearchProgressBar.Visible = true;
            SearchProgressBar.Refresh();
            var useSelenium = UseSelenium;
            UseSelenium = false;
            try
            {
                foreach (var character in CharacterList)
                {
                    SearchProgressBar.Value += 1;
                    AddCharacter(character.Key);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            UseSelenium = useSelenium;
            SearchProgressBar.Visible = false;
            LoadAllButton.Enabled = false;

            LoadCharacters();
            CalculateRPTotals();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshButton.Enabled = false;
            Logger.Debug("Refresh button clicked.");
            WaitCursor.Push();
            DataGridViewSelectedRowCollection rowsToRefresh = SearchGridView.SelectedRows;
            Logger.Debug($"There are {rowsToRefresh.Count} selected rows.");
            if (CharactersByAccountLastDateUpdated is null)
            {
                return;
            }

            if (CharactersByAccountLastDateUpdated.Count < 1)
            {
                Logger.Debug("Database does not contain any records.");
                return;
            }
            List<ChimpJson> chimpsToBeRefreshed = new();
            DateTime date = DateTime.Now;
            foreach (DataGridViewRow row in rowsToRefresh)
            {
                string webID = row.Cells["WebID"]?.Value?.ToString() ?? "";

                if (string.IsNullOrEmpty(webID))
                {
                    continue;
                }

                chimpsToBeRefreshed.Add(new ChimpJson() { WebID = webID });
            }
            RefreshChimps(chimpsToBeRefreshed, date);
            RefreshTimer = true;
            Properties.Settings.Default.NextRefresh = DateTime.Now.AddSeconds(10);
            Properties.Settings.Default.Save();
        }

        private void RefreshChimps(List<ChimpJson> chimpsToBeRefreshed, DateTime date)
        {
            if (CharactersByAccountLastDateUpdated is null)
            {
                return;
            }

            try
            {
                List<ChimpJson> chimpRefreshResults = new();
                Stopwatch stopWatch = Stopwatch.StartNew();
                if (UseAPI)
                {
                    chimpRefreshResults = CamelotHeraldAPI.GetChimps(chimpsToBeRefreshed, SearchProgressBar);
                    if (UseSelenium && chimpRefreshResults.Where(x => !x.IsValid()).Any())
                    {

                        List<ChimpJson> goodChimps = chimpRefreshResults.Where(x => x.IsValid()).ToList();
                        List<ChimpJson> badChimps = chimpRefreshResults.Where(x => !x.IsValid()).ToList();
                        Logger.Debug($"There were {badChimps.Count} characters that could not be refreshed via the API. Attempting via CamelotHerald scrape.");
                        chimpRefreshResults = CamelotHerald.GetChimps(badChimps, SearchProgressBar).Where(x => x.IsValid()).ToList();
                        chimpRefreshResults.AddRange(goodChimps);
                    }
                }
                else if (UseSelenium)
                {
                    chimpRefreshResults = CamelotHerald.GetChimps(chimpsToBeRefreshed, SearchProgressBar).Where(x => x.IsValid()).ToList();
                }

                int refreshed = chimpRefreshResults.Count;
                int failed = CharactersByAccountLastDateUpdated.Count - refreshed;
                Logger.Debug($"Success:{refreshed} Failure:{failed} in {stopWatch.Elapsed:c})");

                SearchProgressBar.Minimum = 0;
                SearchProgressBar.Maximum = chimpRefreshResults.Count;
                SearchProgressBar.Value = 0;
                SearchProgressBar.CustomText = "Updating Database";
                SearchProgressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
                SearchProgressBar.Visible = true;
                GridPanel.Refresh();

                foreach (ChimpJson result in chimpRefreshResults)
                {
                    SearchProgressBar.Value++;
                    SqliteDataAccess.AddCharacter(result.ConvertToCharacterModel(), date, AccountComboBox.Text);
                    SqliteDataAccess.AddGuild(result.ConvertToGuildModel());
                }
                SearchProgressBar.Visible = false;
                GridPanel.Refresh();

                LoadCharacters();
                CalculateRPTotals();

            }
            catch (MaintenanceException)
            {
                UseAPI = false;
                RefreshChimps(chimpsToBeRefreshed, date);
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                WaitCursor.Pop();
                SearchProgressBar.Visible = false;
                SearchProgressBar.CustomText = "";
                SearchProgressBar.VisualMode = ProgressBarDisplayMode.Percentage;
                SearchProgressBar.Refresh();
            }
        }

        private void RefreshAllButton_Click(object sender, EventArgs e)
        {
            RefreshAllButton.Enabled = false;
            if (CharactersByAccountLastDateUpdated is null)
            {
                return;
            }

            DateTime date = DateTime.Now;
            Logger.Debug("RefreshAll button clicked.");
            Logger.Debug($"There are {CharactersByAccountLastDateUpdated.Count} characters in the current table for {AccountComboBox.Text}.");

            WaitCursor.Push();
            List<ChimpJson> chimpsToBeRefreshed = CharactersByAccountLastDateUpdated.Select(x => x.CovertToChimp()).ToList();
            RefreshChimps(chimpsToBeRefreshed, date);

            Properties.Settings.Default.NextRefreshAll = DateTime.Now.AddMinutes(1);
            Properties.Settings.Default.Save();
            RefreshAllTimer = true;
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            RemoveButton.Enabled = false;
            try
            {
                WaitCursor.Push();
                Logger.Debug("Remove button clicked.");
                DataGridViewSelectedRowCollection rowsToDelete = SearchGridView.SelectedRows;
                List<string> webIDs = new();
                foreach (DataGridViewRow row in rowsToDelete)
                {
                    string? webIDValue = row?.Cells["WebID"]?.Value?.ToString();
                    if (webIDValue is not null)
                    {
                        webIDs.Add(webIDValue);
                    }
                    else
                    {
                        Logger.Warn("Row entry indicates its null.");
                    }
                }
                Logger.Debug($"There are {webIDs.Count} selected rows.");
                if (CharactersByAccountLastDateUpdated is null)
                {
                    Logger.Error("CharactersByAccountLastDateUpdated is null.");
                    return;
                }
                if (CharactersByAccountLastDateUpdated.Count > 0)
                {

                    foreach (string _webID in webIDs)
                    {
                        SqliteDataAccess.DeleteCharacter(_webID);
                    }
                }
                LoadCharacters();
                CalculateRPTotals();
                WaitCursor.Pop();
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
            }

            RemoveButton.Enabled = true;

        }

        private void AddCharacter(string name)
        {
            WaitCursor.Push();
            try
            {
                ChimpJson chimp = new();
                if (UseAPI)
                {
                    chimp = CamelotHeraldAPI.GetChimp(name, ServerCluster.Ywain);
                }
                if (chimp.IsValid())
                {
                    SqliteDataAccess.AddCharacter(chimp.ConvertToCharacterModel(), DateTime.Now, AccountComboBox.Text);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            WaitCursor.Pop();
        }

        private void AddNewCharacter()
        {
            WaitCursor.Push();
            Logger.Debug("Search button clicked.");
            try
            {
                if (SearchComboBox.Text != "")
                {
                    WaitCursor.Push();
                    string name = SearchComboBox.Text;
                    ChimpJson chimp = new();
                    if (UseAPI)
                    {
                        chimp = CamelotHeraldAPI.GetChimp(name, ServerCluster.Ywain);
                    }

                    if (!chimp.IsValid() && UseSelenium)
                    {
                        if (UseAPI)
                        {
                            Logger.Debug($"Could not find a character named {name} via API.");
                        }
                        Logger.Debug($"Attempting to find {name} via Selenium Webdriver");

                        try
                        {
                            chimp = CamelotHerald.GetChimp(name, ServerCluster.Ywain, 3);
                        }
                        catch (System.Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }

                    if (!chimp.IsValid())
                    {
                        _ = MessageBox.Show($"Could not find a character named \"{name}\"\non server cluster \"{ServerCluster.Ywain}\".", "Character Not Found", MessageBoxButtons.OK);
                        Logger.Debug($"Could not find a character named {name} on server {ServerCluster.Ywain}.");
                        Logger.Debug($"Returned result: {chimp}");
                    }
                    else
                    {
                        SqliteDataAccess.AddCharacter(chimp.ConvertToCharacterModel(), DateTime.Now, AccountComboBox.Text);
                        Logger.Debug($"Successfully added {chimp.Name} to the repository.");
                        LoadCharacters();
                        CalculateRPTotals();
                    }
                }
                SearchComboBox.Text = string.Empty;
                WaitCursor.Pop();
            }
            catch (MaintenanceException)
            {
                UseAPI = false;
                AddNewCharacter();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchButton.Enabled = false;
            AddNewCharacter();
            SearchButton.Enabled = true;
        }

        private void SearchComboBox_CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar is ((char)Keys.Return) or ((char)Keys.Enter) or ((char)13))
            {
                AddNewCharacter();
            }
        }
        #endregion

        #region Accounts (Add, Remove)
        private void LoadAccounts()
        {
            Accounts = SqliteDataAccess.LoadAccounts();
            AttachAccountList();
        }

        private void AttachAccountList()
        {
            AccountComboBox.DataSource = null;
            AccountComboBox.DataSource = Accounts;
            AccountComboBox.DisplayMember = "Account";
            AccountComboBox.Refresh();
        }

        private void AddAccount()
        {
            string currentAccount = AccountComboBoxAccount?.Account ?? "/\\/073QU4|"; //Some invalid name that //shouldn't// match anything is the database.
            string newText = AccountComboBox.Text;
            Logger.Debug($"Adding account {newText}");
            if (!string.IsNullOrEmpty(newText) && !newText.Equals(currentAccount))
            {
                SqliteDataAccess.AddAccount(newText);
                EnableSelectIndexChangedEvent = false;
                LoadAccounts();
                EnableSelectIndexChangedEvent = true;
                AccountComboBox.SelectedIndex = AccountComboBox.Items.IndexOf(Accounts.Where(x => x?.Account?.Equals(newText) ?? false).First());
            }
        }

        private void AddAccountButton_Click(object sender, EventArgs e)
        {
            AddAccountButton.Enabled = false;
            AddAccount();
            AddAccountButton.Enabled = true;
        }

        private void AccountComboBox_CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                AddAccount();
            }
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            DeleteAccountButton.Enabled = false;
            int index = AccountComboBoxIndex;
            if (index < 1)
            {
                return;
            }

            SqliteDataAccess.RemoveAccount(AccountComboBox.Text);
            EnableSelectIndexChangedEvent = false;  //Turn off index Change Event to keep it from drawing multiple times
            LoadAccounts();                         //Refresh combobox item list
            EnableSelectIndexChangedEvent = true;   //Turn on index Change Event.
            AccountComboBox.SelectedIndex = index - 1; //Index Change will cause table to rebuild. (hopefully only once)
            //AccountComboBox.SelectedIndex = AccountComboBox.Items.IndexOf(Accounts.Where(x => x?.Account?.Equals(newText) ?? false).First());
            if (index == 1)
            {
                LoadCharacters();
                CalculateRPTotals();
            }

        }

        private bool EnableSelectIndexChangedEvent = true;

        private void AccountComboBox_TextChanged(object sender, EventArgs e)
        {
            AccountModel? account = Accounts.Where(x => x.Account == AccountComboBox.Text).FirstOrDefault();
            if (EnableSelectIndexChangedEvent && account is not null)
            {
                LoadCharacters();
                CalculateRPTotals();
                int index = AccountComboBox.Items.IndexOf(account);
                LastAccount = account?.Account ?? "Default";
                Logger.Debug($"Settings.LastAccount = {LastAccount}");
            }
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private CredentialModel? GetCredentials(bool force=false)
        {
            List<CredentialModel> credentials = SqliteDataAccess.LoadAccountCredentials(AccountComboBox.Text);
            if (force || string.IsNullOrEmpty(Properties.Settings.Default.GameDllLocation) || credentials is null || credentials.Count == 0 || string.IsNullOrEmpty(credentials.First().Login) || string.IsNullOrEmpty(credentials.First().Password))
            {
                CredentialForm form = new CredentialForm()
                {
                    Owner = this,
                    StartPosition = FormStartPosition.Manual,
                    AccountName = AccountComboBox.Text
                };
                form.SetLocation();
                form.ShowDialog();
                credentials = SqliteDataAccess.LoadAccountCredentials(AccountComboBox.Text);
                if (credentials is null || credentials.Count == 0 || string.IsNullOrEmpty(credentials.First().Login) || string.IsNullOrEmpty(credentials.First().Password))
                    return null;
            }
            return credentials.FirstOrDefault();
        }

        private static string ReadServerList()
        {
            lock (repositoryLock)
            {
                try
                {
                    string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\ServerConfig.json";
                    string json = File.ReadAllText(path);
                    return json;
                }
                catch (System.Exception ex)
                {
                    Logger.Error(ex);
                    return "{}";
                }
            }
        }

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            CredentialModel? credentials; 
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                credentials = GetCredentials(true);
            }
            else
            {
                credentials = GetCredentials();
            }
            
            if (credentials is null)
                return;

            var login = credentials.Login;
            var password = credentials.Password;

            var row = SearchGridView.SelectedRows[0];
            var charName = row.Cells["Name"]?.Value?.ToString()?.Split(' ').First();
            var serverName = row.Cells["Server"].Value.ToString();
            var realm = row.Cells["Realm"].Value.ToString();

            int realmIndex = 0;
            switch (realm)
            {
                case "Albion":
                    realmIndex = 1;
                    break;
                case "Midgard":
                    realmIndex = 2;
                    break;
                default: //Hibernia
                    realmIndex = 3;
                    break;
            }


            string json = ReadServerList();
            ServerListINI SLI = JsonConvert.DeserializeObject<ServerListINI>(json) ?? new ServerListINI();
            Servers? servers = SLI.Servers;
            List<Server>? serversList = servers?.Server;
            Server? server = serversList?.Where(x => x is not null && x.Name is not null && x.Name.ToLower().Equals(serverName?.ToLower())).FirstOrDefault();
            if (server is null) return;

            var index = server.Index;
            var port = server.Port;
            var ip = server.IP;

            var path = Properties.Settings.Default.GameDllLocation;
            if (path == null) return;

            //string connectionString = $"{path}\\game.dll {ip} {port} {index} {login} {password} {charName} {realmIndex}";
            try
            {
                var p = new System.Diagnostics.Process();
                p.StartInfo.FileName = $"{path}\\game.dll";
                p.StartInfo.Arguments = $" {ip} {port} {index} {login} {password} {charName} {realmIndex} ";
                p.StartInfo.WorkingDirectory = path;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                SqliteDataAccess.AddAccountCredentials(AccountComboBox.Text, null, null);
            }
        }

        private void SearchGridView_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CredentialModel? credentials = GetCredentials();
        }
    }
}
