using AutoHotkey.Interop;
using DAoCToolSuite.ChimpTool.Exceptions;
using DAoCToolSuite.ChimpTool.Extensions;
using DAoCToolSuite.ChimpTool.HeraldAPI;
using DAoCToolSuite.ChimpTool.Json;
using DAoCToolSuite.ChimpTool.Selenium;
using Logger;
using Logger.Enums;
using Newtonsoft.Json;
using SQLLibrary;
using SQLLibrary.Enums;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DAoCToolSuite.ChimpTool
{
    public partial class MainForm : Form
    {
        private static readonly List<LaunchedCharacter> LaunchedCharacters = new();
        private static bool RefreshAllTimer = false;
        private static bool RefreshTimer = false;
        private static DateTime GameRestartTimer0 = DateTime.Now;
        private static DateTime GameRestartTimer1 = DateTime.Now;
        private static readonly object thisLock = new();
        private static readonly object repositoryLock = new();
        private static bool EnableSelectIndexChangedEvent = true;
        private static AutoHotkeyEngine? _AHK = null;
        private static Dictionary<string, int>? _characterList = null;
        public static AutoHotkeyEngine AHK
        {
            get
            {
                _AHK ??= GetAHKEngine();
                return _AHK;
            }
            set => _AHK = value;
        }
        private static Dictionary<string, int> CharacterList
        {
            get
            {
                _characterList ??= new ParseDirectory(DefaultLocation()).Characters;

                return _characterList;
            }
            set => _characterList = value;
        }
        private static int AlbionRpTotal { get; set; } = 0;
        private static int HiberniaRpTotal { get; set; } = 0;
        private static int MidgardRpTotal { get; set; } = 0;
        private static AutoHotkeyEngine GetAHKEngine(int version = 1)
        {
            return version == 2 ? new AutoHotkeyEngine(AutoHotKeyVersion.v2) : new AutoHotkeyEngine(AutoHotKeyVersion.v1);
        }
        private AccountModel AccountComboBoxAccount => Accounts?.Where(x => x.Account == AccountComboBox.Text)?.FirstOrDefault() ?? new AccountModel();
        private int AccountComboBoxIndex => AccountComboBoxAccount == null ? -1 : AccountComboBox.Items.IndexOf(AccountComboBoxAccount);
        public static System.Windows.Forms.Timer Timer { get; set; } = new();
        internal static WaitCursor WaitCursor { get; set; } = new();
        private static System.Windows.Forms.ToolTip MouseOverTooltip { get; set; } = new();
        private static List<AccountModel> Accounts { get; set; } = new List<AccountModel>();
        private static List<GuildModel> Guilds { get; set; } = new List<GuildModel>();

        #region ServerList
        private static List<Server>? _Servers = null;
        private static List<Server> Servers
        {
            get
            {
                if (_Servers is null)
                    _Servers = GetServerList();
                return _Servers;
            }
        }
        private static List<Server> GetServerList()
        {
            string json = Properties.Settings.Default.ServerList;
            if (string.IsNullOrEmpty(json) || json.Equals("{}"))
            {
                Logger.Error("ServerList is invalid.");
                return new();
            }

            ServerListINI SLI = JsonConvert.DeserializeObject<ServerListINI>(json) ?? new ServerListINI();
            if (SLI is null)
            {
                Logger.Error("Deserialization of ServerListINI from json is invalid.");
                return new();
            }

            Servers? servers = SLI.Servers;

            List<Server>? serversList = servers?.Server;

            if (serversList is null)
                return new();

            return serversList;
        }
        #endregion

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
        private static bool UseSelenium
        {
            get => Properties.Settings.Default.UseSelenium;
            set
            {
                Properties.Settings.Default.UseSelenium = value;
                Properties.Settings.Default.Save();
            }
        }
        private static bool UseAPI
        {
            get => Properties.Settings.Default.UseAPI;
            set
            {
                Properties.Settings.Default.UseAPI = value;
                Properties.Settings.Default.Save();
            }
        }
        private static string LastAccount
        {
            get => Properties.Settings.Default.LastAccount;
            set
            {
                Properties.Settings.Default.LastAccount = value;
                Properties.Settings.Default.Save();
            }
        }
        private static string DAoCCharacterFileDirectory
        {
            get => Environment.ExpandEnvironmentVariables(Properties.Settings.Default.DAoCCharacterFileDirectory);
            set
            {
                Properties.Settings.Default.DAoCCharacterFileDirectory = value;
                Properties.Settings.Default.Save();
            }
        }
        private static string BackupRepositoryFullPath
        {
            get => Environment.ExpandEnvironmentVariables(Properties.Settings.Default.JsonBackupFileFullPath);
            set
            {
                Properties.Settings.Default.JsonBackupFileFullPath = value;
                Properties.Settings.Default.Save();
            }
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
            SearchButton.Enabled = UseSelenium || UseAPI;
            SearchComboBox.Enabled = UseSelenium || UseAPI;
            Shown += new System.EventHandler(MainForm_Shown!);
            Timer.Tick -= new EventHandler(MainForm_TimerHandler);
            Timer.Tick += new EventHandler(MainForm_TimerHandler);
            Timer.Interval = 500;
            Timer.Start();
            RefreshAllTimer = DateTime.Now < Properties.Settings.Default.NextRefreshAll;
            RefreshTimer = DateTime.Now < Properties.Settings.Default.NextRefresh;
        }
        private void MainForm_TimerHandler(object? sender, EventArgs e)
        {
            #region Game0 Restart Timer
            if (GameRestartTimer0 > DateTime.Now)
            {
                if (!TimerLabel0.Visible)
                {
                    TimerLabel0.Visible = true;
                }

                TimeSpan diff = GameRestartTimer0 - DateTime.Now;
                TimerLabel0.Text = diff.ToString(@"mm\:ss");
            }
            else
            {
                if (TimerLabel0.Visible)
                {
                    TimerLabel0.Visible = false;
                    TimerLabel0.Text = "";
                }
            }
            #endregion
            #region Game1 Restart Timer
            if (GameRestartTimer1 > DateTime.Now)
            {
                if (!TimerLabel1.Visible)
                {
                    TimerLabel1.Visible = true;
                }

                TimeSpan diff = GameRestartTimer1 - DateTime.Now;
                TimerLabel1.Text = diff.ToString(@"mm\:ss");
            }
            else
            {
                if (TimerLabel1.Visible)
                {
                    TimerLabel1.Visible = false;
                    TimerLabel1.Text = "";
                }
            }
            #endregion
            #region Refresh All Button State
            if (RefreshAllTimer && DateTime.Now >= Properties.Settings.Default.NextRefreshAll)
            {
                RefreshAllTimer = false;
                RefreshAllButton.Text = "Refresh All";
                RefreshAllButton.Enabled = true;
                RefreshAllToolStripMenuItem.Enabled = true;
            }
            else if (RefreshAllTimer && DateTime.Now < Properties.Settings.Default.NextRefreshAll)
            {
                TimeSpan Diff = Properties.Settings.Default.NextRefreshAll - DateTime.Now;
                RefreshAllButton.Text = Convert.ToInt32(Diff.TotalSeconds).ToString();
                RefreshAllButton.Enabled = false;
                RefreshAllToolStripMenuItem.Enabled = false;
            }
            else if (!RefreshAllTimer && BindingSource.Count > 0)
            {
                RefreshAllButton.Text = "Refresh All";
                RefreshAllButton.Enabled = true;
                RefreshAllToolStripMenuItem.Enabled = true;
            }
            else if (!RefreshAllTimer && BindingSource.Count < 1)
            {
                RefreshAllButton.Text = "Refresh All";
                RefreshAllButton.Enabled = false;
                RefreshAllToolStripMenuItem.Enabled = false;
            }
            #endregion
            #region Refresh Button State
            if (RefreshTimer && DateTime.Now >= Properties.Settings.Default.NextRefresh)
            {
                RefreshTimer = false;
                RefreshButton.Text = "Refresh";
                RefreshButton.Enabled = true;
                RefreshToolStripMenuItem.Enabled = true;
            }
            else if (RefreshTimer && DateTime.Now < Properties.Settings.Default.NextRefresh)
            {
                TimeSpan Diff = Properties.Settings.Default.NextRefresh - DateTime.Now;
                RefreshButton.Text = Convert.ToInt32(Diff.TotalSeconds).ToString();
                RefreshButton.Enabled = false;
                RefreshToolStripMenuItem.Enabled = false;
            }
            else if (!RefreshTimer && BindingSource.Count > 0)
            {
                RefreshButton.Text = "Refresh";
                RefreshButton.Enabled = true;
                RefreshToolStripMenuItem.Enabled = true;
            }
            else if (!RefreshTimer && BindingSource.Count < 1)
            {
                RefreshButton.Text = "Refresh";
                RefreshButton.Enabled = false;
                RefreshToolStripMenuItem.Enabled = false;
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
        #endregion

        #region RP Totals
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
            AlbionTotalsTextBox.Text = AlbionRpTotal.ToString("N0", System.Globalization.CultureInfo.CurrentCulture);
            HiberniaTotalsTextBox.Text = HiberniaRpTotal.ToString("N0", System.Globalization.CultureInfo.CurrentCulture);
            MidgardTotalsTextBox.Text = MidgardRpTotal.ToString("N0", System.Globalization.CultureInfo.CurrentCulture);
            TotalRPTextBox.Text = (AlbionRpTotal + HiberniaRpTotal + MidgardRpTotal).ToString("N0", System.Globalization.CultureInfo.CurrentCulture);
            WaitCursor.Pop();
        }
        #endregion

        #region SearchBox Autocomplete
        private static string DefaultLocation()
        {
            string sPath = DAoCCharacterFileDirectory;
            return sPath;
        }
        private void SetAutoCompleteCharacterList()
        {

            SearchComboBox.AutoCompleteCustomSource.AddRange(CharacterList.Keys.ToArray());
            SearchComboBox.Items.AddRange(CharacterList.Keys.ToArray());
        }
        #endregion

        #region Debug.Log DataLink
        private void ProduceDebugLog()
        {
            try
            {
                string contents;
                lock (thisLock)
                {
                    FileAttributes attributes = File.GetAttributes(Logger.LogFileLocation);
                    using (FileStream fs = new(Logger.LogFileLocation, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using StreamReader sr = new(fs);
                        contents = sr.ReadToEnd();
                    }
                    File.SetAttributes(Logger.LogFileLocation, attributes);
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
        }
        private void DebugLog_Click(object sender, EventArgs e)
        {
            DebugLogButton.Enabled = false;
            ProduceDebugLog();
            DebugLogButton.Enabled = true;
        }
        #endregion

        #region Json Backup and Restore to DB
        private static ChimpRepository? LoadChimpRepository()
        {
            WaitCursor.Push();
            Logger.Debug("Loading repository from disk.");
            ChimpRepository chimpRepository = new();
            try
            {
                string json = ReadRepository();
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
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
        private static bool RestoreFromChimpRepository()
        {
            Logger.Debug($"Restoring from backup file {BackupRepositoryFullPath}");
            WaitCursor.Push();
            try
            {
                ChimpRepository? chimpRepository = LoadChimpRepository();
                if (chimpRepository is null)
                {
                    return false;
                }
                SqliteDataAccess.ResetTables();
                foreach (KeyValuePair<string, List<ChimpJson>> account in chimpRepository.Chimps)
                {
                    SqliteDataAccess.AddAccount(account.Key);
                    foreach (ChimpJson chimp in account.Value)
                    {
                        SqliteDataAccess.AddCharacter(chimp.ConvertToCharacterModel(), DateTime.Now, account.Key);
                        SqliteDataAccess.AddGuild(chimp.ConvertToGuildModel());
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
            finally
            {
                WaitCursor.Pop();
            }
        }
        private static void WriteRepository(string json)
        {
            string fileName;
            SaveFileDialog saveFileDialog = new()
            {
                InitialDirectory = Path.GetDirectoryName(BackupRepositoryFullPath),
                //FileName = Path.GetFileName(BackupRepositoryFullPath),
                Filter = "json (*.json)|*.json|All files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            lock (repositoryLock)
            {
                try
                {
                    Logger.Debug($"Writting backup to {fileName}");
                    BackupRepositoryFullPath = fileName;
                    File.WriteAllText(fileName, json);
                }
                catch (System.Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }
        private static string ReadRepository()
        {
            string fileName;
            OpenFileDialog openFileDialog = new()
            {
                InitialDirectory = Path.GetDirectoryName(BackupRepositoryFullPath),
                Filter = "json (*.json)|*.json|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
            }
            else
            {
                return "";
            }

            lock (repositoryLock)
            {
                try
                {
                    string json = File.ReadAllText(fileName);
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
        private void PerformDBRestore()
        {
            SearchGridView.Visible = false;
            LoadingTabelLabel.Visible = true;
            GridPanel.Refresh();
            if (File.Exists(BackupRepositoryFullPath))
            {
                EnableSelectIndexChangedEvent = false;
                if (RestoreFromChimpRepository())
                {
                    Logger.Debug($"Backup restored to {BackupRepositoryFullPath}");
                    LoadAccounts();
                    EnableSelectIndexChangedEvent = true;
                    LoadCharacters();
                    CalculateRPTotals();
                }
                else
                {
                    Logger.Debug($"Aborted");
                }
            }
            else
            {
                Logger.Debug($"No backup chimp repository found at {BackupRepositoryFullPath}");
            }
            SearchGridView.Visible = true;
            LoadingTabelLabel.Visible = false;
            GridPanel.Refresh();
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
            try
            {
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
                    string albKills = character.Albion_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
                    string midKills = character.Midgard_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
                    string hibKills = character.Hibernia_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";

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
                                    toShow += $"Hibernia:{character.Hibernia_SoloKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"} Midgard:{character.Midgard_SoloKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"}";
                                    break;
                                case "midgard":
                                    toShow += $"Albion:{character.Albion_SoloKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"} Hibernia:{character.Hibernia_SoloKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"}";
                                    break;
                                case "hibernia":
                                    toShow += $"Albion:{character.Albion_SoloKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"} Midgard:{character.Midgard_SoloKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"}";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "TotalDeathBlows":
                            switch (realm)
                            {
                                case "albion":
                                    toShow += $"Hibernia:{character.Hibernia_DeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"} Midgard:{character.Midgard_DeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"}";
                                    break;
                                case "midgard":
                                    toShow += $"Albion:{character.Albion_DeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"} Hibernia:{character.Hibernia_DeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"}";
                                    break;
                                case "hibernia":
                                    toShow += $"Albion:{character.Albion_DeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"} Midgard:{character.Midgard_DeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"}";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "TotalKills":
                            switch (realm)
                            {
                                case "albion":
                                    toShow += $"Hibernia:{character.Hibernia_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"} Midgard:{character.Midgard_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"}";
                                    break;
                                case "midgard":
                                    toShow += $"Albion:{character.Albion_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"} Hibernia:{character.Hibernia_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"}";
                                    break;
                                case "hibernia":
                                    toShow += $"Albion:{character.Albion_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"} Midgard:{character.Midgard_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0"}";
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
            catch (Exception ex)
            {
                Logger.Error(ex);
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
        private void SearchGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow? row = SearchGridView.Rows[e.RowIndex];
            if (row != null)
            {
                string? characterName = row.Cells["Name"]?.Value?.ToString();
                Logger.Debug($"Doubleclick detected on row containing {characterName ?? "null"}");
                PerformLaunch();
            }
        }

        private void SearchGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            SearchGridView.ClearSelection();
            int rowSelected = e.RowIndex;
            if (e.RowIndex != -1)
            {
                SearchGridView.Rows[rowSelected].Selected = true;
            }
            e.ContextMenuStrip = contextMenuStrip1;
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

            addAllCharactersToolStripMenuItem.Enabled = (CharactersByAccountLastDateUpdated is null || CharactersByAccountLastDateUpdated.Count == 0) && DateTime.Now > Properties.Settings.Default.NextLoadAll;
        }
        private void AttachCharacters()
        {
            BindingSource.DataSource = CharactersByAccountLastDateUpdated?.ToChimpJsonList() ?? new();
        }
        private void PerformRefresh()
        {
            WaitCursor.Push();
            DataGridViewSelectedRowCollection rowsToRefresh = SearchGridView.SelectedRows;
            Logger.Debug($"There are {rowsToRefresh.Count} selected rows.");

            if (CharactersByAccountLastDateUpdated is null)
            {
                Logger.Warn("CharactersByAccountLastDateUpdated is null @ PerformRefresh()");
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
        private void PerformRefresh(string? webID, string account)
        {
            if (webID is null)
            {
                Logger.Error("WebID null @ PerformRefresh(webID)");
                return;
            }
            WaitCursor.Push();
            if (CharactersByAccountLastDateUpdated is null)
            {
                Logger.Warn("CharactersByAccountLastDateUpdated is null @ PerformRefresh(webID)");
                return;
            }
            if (CharactersByAccountLastDateUpdated.Count < 1)
            {
                Logger.Debug("Database does not contain any records.");
                return;
            }
            RefreshChimp(webID, account, DateTime.Now);
            RefreshTimer = true;
            Properties.Settings.Default.NextRefresh = DateTime.Now.AddSeconds(10);
            Properties.Settings.Default.Save();
            WaitCursor.Pop();
        }
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("Refresh button clicked.");
            RefreshButton.Enabled = false;
            PerformRefresh();
        }
        private void RefreshChimp(string webID, string account, DateTime date)
        {
            if (CharactersByAccountLastDateUpdated is null)
            {
                return;
            }

            try
            {
                ChimpJson chimpToBeRefreshed = new() { WebID = webID };
                ChimpJson chimpRefreshResult = new();
                Stopwatch stopWatch = Stopwatch.StartNew();
                if (UseAPI)
                {
                    chimpRefreshResult = CamelotHeraldAPI.GetChimp(webID);
                    if (UseSelenium && !chimpRefreshResult.IsValid())
                    {
                        chimpRefreshResult = CamelotHerald.GetChimp(webID);
                    }
                }
                else if (UseSelenium)
                {
                    chimpRefreshResult = CamelotHerald.GetChimp(webID);
                }


                if (!chimpRefreshResult.IsValid())
                {
                    return;
                }
                SqliteDataAccess.AddCharacter(chimpRefreshResult.ConvertToCharacterModel(), date, account);
                SqliteDataAccess.AddGuild(chimpRefreshResult.ConvertToGuildModel());
                LoadCharacters();
                CalculateRPTotals();
            }
            catch (MaintenanceException)
            {
                UseAPI = false;
                RefreshChimp(webID, account, date);
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                WaitCursor.Pop();
            }
        }
        private void RefreshChimps(List<ChimpJson> chimpsToBeRefreshed, DateTime date)
        {
            if (CharactersByAccountLastDateUpdated is null)
            {
                Logger.Error("CharactersByAccountLastDateUpdated is null @ RefreshChimps()");
                return;
            }

            try
            {
                List<ChimpJson> chimpRefreshResults = new();
                Stopwatch stopWatch = Stopwatch.StartNew();
                if (UseAPI)
                {
                    Logger.Debug("Attempting RefreshAll via HeraldAPI");
                    chimpRefreshResults = CamelotHeraldAPI.GetChimps(chimpsToBeRefreshed, SearchProgressBar);
                    if (UseSelenium && chimpRefreshResults.Where(x => !x.IsValid()).Any())
                    {
                        List<ChimpJson> goodChimps = chimpRefreshResults.Where(x => x.IsValid()).ToList();
                        List<ChimpJson> badChimps = chimpRefreshResults.Where(x => !x.IsValid()).ToList();
                        foreach (var bad in badChimps)
                        {
                            if (bad?.Name is not null)
                            {
                                Logger.Debug($"Failed to update chimp for: {bad.Name}");
                            }
                        }
                        Logger.Debug($"There were {badChimps.Count} characters that could not be refreshed via the API. Attempting via CamelotHerald scrape.");
                        chimpRefreshResults = CamelotHerald.GetChimps(badChimps, SearchProgressBar);
                        goodChimps.AddRange(chimpRefreshResults.Where(x => x.IsValid()).ToList());
                        badChimps.AddRange(chimpRefreshResults.Where(x => x.IsValid()).ToList());
                        Logger.Debug($"{goodChimps.Count} characters were refreshed with {badChimps.Count} failures.");
                        foreach (var bad in badChimps)
                        {
                            if (bad?.Name is not null)
                            {
                                Logger.Debug($"Failed to update chimp for: {bad.Name}");
                            }
                        }
                        chimpRefreshResults = goodChimps;
                    }
                }
                else if (UseSelenium)
                {
                    chimpRefreshResults = CamelotHerald.GetChimps(chimpsToBeRefreshed, SearchProgressBar).Where(x => x.IsValid()).ToList();
                    List<ChimpJson> goodChimps = chimpRefreshResults.Where(x => x.IsValid()).ToList();
                    List<ChimpJson> badChimps = chimpRefreshResults.Where(x => !x.IsValid()).ToList();
                    Logger.Debug($"{goodChimps.Count} characters were refreshed with {badChimps.Count} failures.");
                    foreach (var bad in badChimps)
                    {
                        if (bad?.Name is not null)
                        {
                            Logger.Debug($"Failed to update chimp for: {bad.Name}");
                        }
                    }
                }

                int refreshed = chimpRefreshResults.Count;
                int failed = chimpsToBeRefreshed.Count - refreshed;
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
                    SearchProgressBar.Value += 1;
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
        private void PerformRefreshAll()
        {
            if (CharactersByAccountLastDateUpdated is null)
            {
                Logger.Error("CharactersByAccountLastDateUpdated is null @ PerformRefreshAll().");
                return;
            }

            DateTime date = DateTime.Now;
            Logger.Debug($"There are {CharactersByAccountLastDateUpdated.Count} characters in the current table for {AccountComboBox.Text}.");

            WaitCursor.Push();
            List<ChimpJson> chimpsToBeRefreshed = CharactersByAccountLastDateUpdated.Select(x => x.CovertToChimp()).ToList();
            RefreshChimps(chimpsToBeRefreshed, date);

            Properties.Settings.Default.NextRefreshAll = DateTime.Now.AddMinutes(1);
            Properties.Settings.Default.Save();
            RefreshAllTimer = true;
        }
        private void RefreshAllButton_Click(object sender, EventArgs e)
        {
            Logger.Debug("RefreshAll button clicked.");
            RefreshAllButton.Enabled = false;
            PerformRefreshAll();
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
        private void AddCharacter(string name, ServerCluster server = ServerCluster.Ywain)
        {
            WaitCursor.Push();
            try
            {
                ChimpJson chimp = new();
                if (UseAPI)
                {
                    Logger.Debug($"Searching for character {name} on {server}.");
                    chimp = CamelotHeraldAPI.GetChimp(name, server);
                }
                if (chimp.IsValid())
                {
                    Logger.Debug($"Character found. Adding to database.");
                    SqliteDataAccess.AddCharacter(chimp.ConvertToCharacterModel(), DateTime.Now, AccountComboBox.Text);
                }
                else
                {
                    Logger.Debug($"Could not find a character {name} on {server}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            WaitCursor.Pop();
        }
        private void AddNewCharacter(ServerCluster server = ServerCluster.Ywain)
        {
            WaitCursor.Push();
            Logger.Debug($"Searching for character {SearchComboBox.Text} on {server}");
            try
            {
                if (SearchComboBox.Text != "")
                {
                    WaitCursor.Push();
                    string name = SearchComboBox.Text;
                    ChimpJson chimp = new();
                    if (UseAPI)
                    {
                        chimp = CamelotHeraldAPI.GetChimp(name, server);
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
                            chimp = CamelotHerald.GetChimp(name, server, 3);
                        }
                        catch (System.Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }

                    if (!chimp.IsValid())
                    {
                        _ = MessageBox.Show($"Could not find a character named \"{name}\"\non server cluster \"{server}\".", "Character Not Found", MessageBoxButtons.OK);
                        Logger.Debug($"Could not find a character named {name} on server {server}.");
                        Logger.Debug($"Returned result: {chimp}");
                    }
                    else
                    {
                        SqliteDataAccess.AddCharacter(chimp.ConvertToCharacterModel(), DateTime.Now, AccountComboBox.Text);
                        Logger.Debug($"Successfully added {chimp.Name} to the database.");
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
            Logger.Debug("Search button clicked.");
            SearchButton.Enabled = false;
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                Logger.Debug("Shift key detected.");
                if (Properties.Settings.Default.CharacterShiftLeftClick.Equals("Gaheris"))
                    AddNewCharacter(ServerCluster.Gaheris);
                else
                    AddNewCharacter(ServerCluster.Ywain);
            }
            else
            {
                if (Properties.Settings.Default.CharacterLeftClick.Equals("Ywain"))
                    AddNewCharacter(ServerCluster.Ywain);
                else
                    AddNewCharacter(ServerCluster.Gaheris);
            }
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
            AddAccountButton.Enabled = false;
        }
        private void AddAccount()
        {
            string currentAccount = AccountComboBoxAccount?.Account ?? "/\\/073QU4|"; //Some invalid name that //shouldn't// match anything is the database.
            string newText = AccountComboBox.Text;
            Logger.Debug($"Adding chimp page {newText}");
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
                DeleteAccountButton.Enabled = true;
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
            DeleteAccountButton.Enabled = true;
        }
        private void AccountComboBox_TextChanged(object sender, EventArgs e)
        {
            AccountModel? account = Accounts.Where(x => x.Account == AccountComboBox.Text).FirstOrDefault();
            if (EnableSelectIndexChangedEvent && account is not null)
            {
                AddAccountButton.Enabled = false;
                LoadCharacters();
                CalculateRPTotals();
                int index = AccountComboBox.Items.IndexOf(account);
                LastAccount = account?.Account ?? "Default";
                Logger.Debug($"Settings.LastAccount = {LastAccount}");
            }
            else
            {
                AddAccountButton.Enabled = true;
            }
        }
        #endregion

        #region Launch Character
        private CredentialModel? GetCredentials(bool force = false)
        {
            List<CredentialModel> credentials = SqliteDataAccess.LoadAccountCredentials(AccountComboBox.Text);
            if (force || string.IsNullOrEmpty(Properties.Settings.Default.GameDllLocation) || !File.Exists(Properties.Settings.Default.GameDllLocation + "\\game.dll")
                || credentials is null || credentials.Count == 0 || string.IsNullOrEmpty(credentials.First().Login) || string.IsNullOrEmpty(credentials.First().Password))
            {
                CredentialForm form = new()
                {
                    Owner = this,
                    StartPosition = FormStartPosition.Manual,
                    AccountName = AccountComboBox.Text
                };
                form.SetLocation();
                _ = form.ShowDialog();
                credentials = SqliteDataAccess.LoadAccountCredentials(AccountComboBox.Text);
                if (credentials is null || credentials.Count == 0 || string.IsNullOrEmpty(credentials.First().Login) || string.IsNullOrEmpty(credentials.First().Password))
                {
                    return null;
                }
            }
            return credentials.FirstOrDefault();
        }
        private static string HidePassword(string? password)
        {
            if (password == null)
            {
                return "null";
            }

            string hidden = "";
            for (int index = 0; index < password.Length; index++)
            {
                hidden += "*";
            }
            return hidden;
        }
        private static string ObscureUserName(string? userName)
        {
            if (userName == null)
            {
                return "null";
            }

            string obscured = userName[..2];
            for (int index = 2; index < userName.Length; index++)
            {
                obscured += "*";
            }
            return obscured;
        }
        private void PerformLaunch()
        {
            #region DAoC Credentials
            CredentialModel? credentials;
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                Logger.Debug("Shift key detected.");
                credentials = GetCredentials(true);
            }
            else
            {
                credentials = GetCredentials();
            }


            if (credentials is null)
            {
                Logger.Warn("Credentials returned from GetCredentials() are invalid.");
                LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
                launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
                launchToolStripMenuItem1.Enabled = LaunchedCharacters.Count <= 2;
                return;
            }

            string? login = credentials.Login;
            string? password = credentials.Password;
            #endregion

            DataGridViewRow row = SearchGridView.SelectedRows[0];
            string? charName = row.Cells["Name"]?.Value?.ToString()?.Split(' ').First();
            string? serverName = row.Cells["Server"].Value.ToString();
            string? realm = row.Cells["Realm"].Value.ToString();
            string? webID = row.Cells["WebID"].Value.ToString();
            string account = AccountComboBox.Text;

            Logger.Debug($"Information read from the selected item Character:{charName ?? "null"} Server:{serverName ?? "null"} Realm:{realm ?? "null"}");

            if (webID is null)
            {
                Logger.Error("Could not determine character's WebID");
                LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
                launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
                launchToolStripMenuItem1.Enabled = LaunchedCharacters.Count <= 2;
                return;
            }
            LaunchedCharacter launchedCharacter = new();

            #region AHK
            try
            {
                if (!LaunchedCharacters.Where(x => x.AttacheAHK).Any()) //Check if another character had already launched w/AHK script associated.
                {
                    AHKModel? model = SqliteDataAccess.LoadAHKModelByWebID(webID, account);
                    if (model is not null)
                    {
                        AHK = GetAHKEngine(model.Version ?? 1);
                        Logger.Debug($"Loading {model.AHKScriptPath}");
                        AHK.LoadFile(model.AHKScriptPath);
                        launchedCharacter.AttacheAHK = true;
                    }
                }
                else
                {
                    Logger.Debug($"Only one AHK Script can be active at one time. I think.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            #endregion

            #region Launch Character
            int realmIndex = 0;
            realmIndex = realm switch
            {
                "Albion" => 1,
                "Midgard" => 2,
                //Hibernia
                _ => 3,
            };

            //string json = Properties.Settings.Default.ServerList;
            //if (string.IsNullOrEmpty(json) || json.Equals("{}"))
            //{
            //    Logger.Error("ServerList is invalid.");
            //    LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
            //    launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
            //    return;
            //}

            //ServerListINI SLI = JsonConvert.DeserializeObject<ServerListINI>(json) ?? new ServerListINI();
            //if (SLI is null)
            //{
            //    Logger.Error("Deserialization of ServerListINI from json is invalid.");
            //    LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
            //    launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
            //    return;
            //}
            //Servers? servers = SLI.Servers;
            //List<Server>? serversList = servers?.Server;
            Server? server = Servers?.Where(x => x is not null && x.Name is not null && x.Name.ToLower().Equals(serverName?.ToLower())).FirstOrDefault();
            if (server is null)
            {
                Logger.Error($"Could not find Server {server} in the ServerList");
                LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
                launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
                return;
            }

            int? index = server.Index;
            int? port = server.Port;
            string? ip = server.IP;

            string path = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.GameDllLocation);
            if (path == null)
            {
                Logger.Error("Game.dll location is not set.");
                LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
                launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
                return;
            }

            Logger.Debug($"Executing: {path}\\game.dll {ip} {port} {index} {ObscureUserName(login)} {HidePassword(password)} {charName} {realmIndex}");

            int launchIndex = LaunchedCharacters.Count;

            if (launchIndex < 2)
            {
                launchedCharacter.WebID = webID;
                launchedCharacter.Account = account;
                launchedCharacter.ProcessIndex = launchIndex;
            }
            else
            {
                _ = MessageBox.Show("You may not launch more than 2 characters.", "You shall not pass");
                LaunchButton.Enabled = launchIndex <= 2;
                launchToolStripMenuItem.Enabled = launchIndex <= 2;
                return;
            }

            try
            {
                Process p = new();
                p.StartInfo.FileName = $"{path}\\game.dll";
                p.StartInfo.Arguments = $" {ip} {port} {index} {login} {password} {charName} {realmIndex} ";
                p.StartInfo.WorkingDirectory = path;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.EnableRaisingEvents = true;
                if (launchIndex == 0)
                {
                    p.Exited -= GameOver0;
                    p.Exited += GameOver0;
                }
                else
                {
                    p.Exited -= GameOver1;
                    p.Exited += GameOver1;
                }
                Logger.Debug("Starting game.dll process");
                _ = p.Start();
                LaunchedCharacters.Add(launchedCharacter);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                _ = SqliteDataAccess.AddAccountCredentials(AccountComboBox.Text, login, null);
            }
            #endregion

            LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
            launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
            launchToolStripMenuItem1.Enabled = LaunchedCharacters.Count <= 2;
        }
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            LaunchButton.Enabled = false;
            launchToolStripMenuItem.Enabled = false;
            launchToolStripMenuItem1.Enabled = false;
            PerformLaunch();
        }
        private void GameOver0(object? sender, EventArgs e)
        {
            try
            {
                Logger.Debug("game.dll process ended");
                if (LaunchButton.InvokeRequired)
                {
                    Logger.Debug("Invoked Was Required");
                    LaunchButton.Invoke(PostGame0);
                }
                else
                {
                    Logger.Debug("Invoked Was NOT Required");
                    PostGame0();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
            launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
            launchToolStripMenuItem1.Enabled = LaunchedCharacters.Count <= 2;
        }
        private void GameOver1(object? sender, EventArgs e)
        {
            try
            {
                Logger.Debug("game.dll process ended");
                if (LaunchButton.InvokeRequired)
                {
                    Logger.Debug("Invoked Was Required");
                    LaunchButton.Invoke(PostGame1);
                }
                else
                {
                    Logger.Debug("Invoked Was NOT Required");
                    PostGame1();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
            launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
            launchToolStripMenuItem1.Enabled = LaunchedCharacters.Count <= 2;
        }
        private void PostGame0()
        {
            GameRestartTimer0 = DateTime.Now.AddMinutes(2);

            LaunchedCharacter launchedCharacter = LaunchedCharacters.Where(x => x.ProcessIndex == 0).First();

            //Remove AHK
            if (launchedCharacter.AttacheAHK)
            {
                AHK.Terminate();
                _AHK = null;
            }

            //Refresh the launched character's chimp inf
            PerformRefresh(launchedCharacter.WebID, launchedCharacter.Account);

            //Freeup Process Slot
            _ = LaunchedCharacters.Remove(launchedCharacter);

            //Re-Enable Buttons/MenuItems
            LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
            launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
            launchToolStripMenuItem1.Enabled = LaunchedCharacters.Count <= 2;
        }
        private void PostGame1()
        {
            GameRestartTimer1 = DateTime.Now.AddMinutes(2);

            LaunchedCharacter launchedCharacter = LaunchedCharacters.Where(x => x.ProcessIndex == 1).First();

            //Remove AHK
            if (launchedCharacter.AttacheAHK)
            {
                AHK.Terminate();
                _AHK = null;
            }

            //Refresh the launched character's chimp inf
            PerformRefresh(launchedCharacter.WebID, launchedCharacter.Account);

            //Freeup Process Slot
            _ = LaunchedCharacters.Remove(launchedCharacter);

            //Re-Enable Buttons/MenuItems
            LaunchButton.Enabled = LaunchedCharacters.Count <= 2;
            launchToolStripMenuItem.Enabled = LaunchedCharacters.Count <= 2;
            launchToolStripMenuItem1.Enabled = LaunchedCharacters.Count <= 2;
        }
        #endregion

        #region Menu
        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem menuItem)
            {
                return;
            }

            menuItem.Enabled = false;
            Logger.Debug("Refresh MenuItem clicked.");
            if (Properties.Settings.Default.NextRefresh > DateTime.Now)
            {
                _ = MessageBox.Show($"Refresh can only be done every 10 seconds.\nPlease try again later.", "Refresh Character", MessageBoxButtons.OK);
                menuItem.Enabled = true;
                return;
            }
            PerformRefresh();
            menuItem.Enabled = true;
        }
        private void BackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName;
            SaveFileDialog saveFileDialog = new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                FileName = "CharacterDB_Backup.db",
                Filter = "SQLite Database (*.db)|*.db|All files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            string srcFullPath = SqliteDataAccess.DataBaseLocation;
            string destFileName = fileName;
            if (!SqliteDataAccess.BackupDB(srcFullPath, destFileName))
            {
                Logger.Error("Failed to backup the DB. See ChimpTool.log for more details.");
            }
        }
        private void RestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName;
            OpenFileDialog openFileDialog = new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                //FileName = "CharacterDB_Backup.db",
                Filter = "SQLite Database (*.db)|*.db|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            EnableSelectIndexChangedEvent = false;
            string srcFullPath = fileName;
            string destFileName = SqliteDataAccess.DataBaseLocation;
            if (SqliteDataAccess.RestoreDB(srcFullPath, destFileName, true))
            {
                LoadAccounts();
                LoadCharacters();
                CalculateRPTotals();
            }
            else
            {
                Logger.Error("Failed to restore the DB. See ChimpTool.log for more details.");
            }
            EnableSelectIndexChangedEvent = true;
        }
        private void ImportJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importJsonToolStripMenuItem.Enabled = false;
            WaitCursor.Push();
            Logger.Debug("Restore MenuItem has been pressed.");
            try
            {
                PerformDBRestore();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                WaitCursor.Pop();
                importJsonToolStripMenuItem.Enabled = true;
            }
        }
        private void ExportJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportJsonToolStripMenuItem.Enabled = false;
            WaitCursor.Push();
            Logger.Debug("Backup MenuItem has been pressed.");
            try
            {
                LoadCharacters();
                BackupToChimpRepository();
                Logger.Debug($"Backup created at {BackupRepositoryFullPath}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                importJsonToolStripMenuItem.Enabled = File.Exists(BackupRepositoryFullPath);
                WaitCursor.Pop();
                exportJsonToolStripMenuItem.Enabled = true;
            }
        }
        private void LaunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchButton.Enabled = false;
            launchToolStripMenuItem.Enabled = false;
            launchToolStripMenuItem1.Enabled = false;
            PerformLaunch();
        }
        private void DAoCCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CredentialForm form = new()
            {
                Owner = this,
                StartPosition = FormStartPosition.Manual,
                AccountName = AccountComboBox.Text
            };
            form.SetLocation();
            _ = form.ShowDialog();
        }
        private void ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationForm form = new()
            {
                Owner = this,
                StartPosition = FormStartPosition.Manual
            };
            form.SetLocation();
            form.ShowDialog();
        }
        private void AssociateAHKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SearchGridView.SelectedRows.Count < 1)
                return;
            DataGridViewRow row = SearchGridView.SelectedRows[0];
            string? charName = row.Cells["Name"]?.Value?.ToString()?.Split(' ').First();
            string? serverName = row.Cells["Server"].Value.ToString();
            string? realm = row.Cells["Realm"].Value.ToString();
            string? webID = row.Cells["WebID"].Value.ToString();
            if (string.IsNullOrEmpty(charName) || string.IsNullOrEmpty(serverName) || string.IsNullOrEmpty(realm) || string.IsNullOrEmpty(webID))
            {
                return;
            }

            AHKForm form = new(charName, realm, serverName, webID, AccountComboBox.Text)
            {
                Owner = this,
                StartPosition = FormStartPosition.Manual
            };
            form.SetLocation();
            _ = form.ShowDialog();

        }
        private void LogViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logViewerToolStripMenuItem.Enabled = false;
            ProduceDebugLog();
            logViewerToolStripMenuItem.Enabled = true;
        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new()
            {
                Owner = this,
                StartPosition = FormStartPosition.Manual
            };
            about.SetLocation();
            _ = about.ShowDialog();
        }
        private void AddAllCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addAllCharactersToolStripMenuItem.Enabled = false;
            DialogResult dlg = MessageBox.Show($"WARNING: This can only be used once per 24 hours.\nDo you wish to proceed?", "Load All Characters", MessageBoxButtons.YesNo);
            if (dlg == DialogResult.No)
            {
                addAllCharactersToolStripMenuItem.Enabled = true;
                return;
            }
            Properties.Settings.Default.NextLoadAll = DateTime.Now.AddDays(1);
            Properties.Settings.Default.Save();

            SearchProgressBar.Minimum = 0;
            SearchProgressBar.Maximum = CharacterList.Count;
            SearchProgressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
            SearchProgressBar.Value = 0;
            SearchProgressBar.CustomText = "Loading Characters";
            SearchProgressBar.Visible = true;
            SearchProgressBar.Refresh();
            bool useSelenium = UseSelenium;
            UseSelenium = false;
            try
            {
                foreach (KeyValuePair<string, int> character in CharacterList)
                {
                    string key = character.Key.Split('(').First().Trim();
                    int value = character.Value;

                    SearchProgressBar.Value += 1;
                    ServerCluster server = ServerCluster.Ywain;
                    var serverName = Servers.Where(x => x.Index == value).FirstOrDefault()?.Name;
                    if (serverName is not null)
                    {
                        if (serverName.Contains("Ywain", StringComparison.OrdinalIgnoreCase))
                            serverName = "Ywain";

                        if (!Enum.TryParse(serverName, out server))
                        {
                            Logger.Warn($"Unknown server {server} with index {value}.");
                            continue;
                        }
                    }
                    else
                    {
                        Logger.Debug($"Could not determine server cluster for server index {value}.");
                        continue;
                    }
                    AddCharacter(key, server);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            UseSelenium = useSelenium;
            SearchProgressBar.Visible = false;

            LoadCharacters();
            CalculateRPTotals();
        }
        private void RefreshAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshAllToolStripMenuItem.Enabled = false;
            PerformRefreshAll();
        }
        private void ManageAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountManagerForm form = new()
            {
                Owner = this,
                StartPosition = FormStartPosition.Manual,
            };
            form.SetLocation();
            _ = form.ShowDialog();
            LoadAccounts();
        }
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Restore Settings ToolStrip Context MenuItem Clicked");
            settingsToolStripMenuItem.Enabled = false;
            PerformRestoreSettings();
            settingsToolStripMenuItem.Enabled = true;
        }
        #endregion

        #region Right Click Menu
        private void RC_LaunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchButton.Enabled = false;
            launchToolStripMenuItem.Enabled = false;
            launchToolStripMenuItem1.Enabled = false;
            PerformLaunch();
        }
        private void RC_RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem menuItem)
            {
                return;
            }

            menuItem.Enabled = false;
            Logger.Debug("Refresh MenuItem clicked.");
            if (Properties.Settings.Default.NextRefresh > DateTime.Now)
            {
                _ = MessageBox.Show($"Refresh can only be done every 10 seconds.\nPlease try again later.", "Refresh Character", MessageBoxButtons.OK);
                menuItem.Enabled = true;
                return;
            }
            PerformRefresh();
            menuItem.Enabled = true;
        }
        private void RC_DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteToolStripMenuItem.Enabled = false;
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

            deleteToolStripMenuItem.Enabled = true;
        }
        private void RC_AssociateAHKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AHKForm form = new(SearchGridView.SelectedRows[0], AccountComboBox.Text)
            {
                Owner = this,
                StartPosition = FormStartPosition.Manual
            };
            form.SetLocation();
            _ = form.ShowDialog();
        }
        private void SettingsRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Debug("Restore Settings ToolStrip MenuItem Clicked");
            settingsRestoreToolStripMenuItem.Enabled = false;
            PerformRestoreSettings();
            settingsRestoreToolStripMenuItem.Enabled = true;
        }
        private void MoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = SearchGridView.SelectedRows[0];
            string? webID = row.Cells["WebID"].Value.ToString();
            string? account = AccountComboBox.Text.ToString();
            if (string.IsNullOrEmpty(webID) || string.IsNullOrEmpty(account))
            {
                return;
            }
            MoveAccountForm form = new(webID, account) { Owner = this, StartPosition = FormStartPosition.Manual };
            form.SetLocation();
            form.ShowDialog();
            LoadCharacters();
        }
        #endregion

        private void PerformRestoreSettings()
        {
            var firstName = SearchGridView.SelectedRows[0].Cells["Name"].Value?.ToString()?.Split(' ').First();
            var realm = SearchGridView.SelectedRows[0].Cells["Realm"].Value?.ToString();
            var _class = SearchGridView.SelectedRows[0].Cells["Class"].Value?.ToString();
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(realm) || string.IsNullOrEmpty(_class)) { return; }
            SettingsManagerForm form = new(firstName, realm, _class)
            {
                Owner = this,
                StartPosition = FormStartPosition.Manual
            };
            form.SetLocation();
            form.ShowDialog(this);
        }



    }
}
