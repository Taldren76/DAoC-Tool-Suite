using System.Data;
using System.Diagnostics;
using System.IO;
using DAoCToolSuite.ChimpTool.Enums;
using DAoCToolSuite.ChimpTool.Exception;
using DAoCToolSuite.ChimpTool.Extensions;
using DAoCToolSuite.ChimpTool.HeraldAPI;
using DAoCToolSuite.ChimpTool.Json;
using DAoCToolSuite.ChimpTool.Logging;
using DAoCToolSuite.ChimpTool.Selenium;
using DAoCToolSuite.ChimpTool.Settings;
using Newtonsoft.Json;
using SQLLibrary;
using SQLLibrary.Enums;

namespace DAoCToolSuite.ChimpTool
{
    public partial class MainForm : Form
    {
        internal static WaitCursor WaitCursor = new();
        private static readonly System.Windows.Forms.ToolTip MouseOverTooltip = new();
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
        private static Logger? _Logger = null;
        public static Logger Logger
        {
            get
            {
                _Logger ??= new Logger();

                return _Logger;

            }
            set => _Logger = value;
        }
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
            //EnableSelectIndexChangedEvent = false;
            //Settings = new SettingsManager();
            WaitCursor.Push();
            InitializeComponent();
            LinkLabelLinkToAFile();
            LoadAccounts();
            SetToLastAccount();
            SetAutoCompleteCharacterList();
            SearchGridView.DataSource = BindingSource;
            RestoreButton.Enabled = HasBackupChimpRepository();
            SearchButton.Enabled = UseSelenium || UseAPI;
            SearchComboBox.Enabled = UseSelenium || UseAPI;
            Shown += new System.EventHandler(MainForm_Shown!);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            bool check = AlwaysOnTop;
            OnTopCheckBox.Checked = check;
            GridPanel.Visible = true;
            LoadCharacters();
            CalculateRPTotals();
            WaitCursor.PopAll();
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
        private void MainForm_Closing(object sender, EventArgs e)
        {
            if (sender is not MainForm form)
            {
                return;
            }

            Properties.Settings.Default.WindowLocation = form.Location;
            Properties.Settings.Default.Save();
            Logger.Debug($"Shutting down.");
            CamelotHerald.Quit();
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
            UpdateDebugLinkBackColor();
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
        public static DebugLevel CurrentDebugLevel { get; set; } = DebugLevel.Debug;
        public void LinkLabelLinkToAFile()
        {
            string? path = Path.GetDirectoryName(Application.ExecutablePath); //System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            linkLabel1.BorderStyle = BorderStyle.None;
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
            _ = linkLabel1.Links.Add(0, linkLabel1.Text.ToString().Length, path + "\\DAoCTools.log");
        }

        public static void UpdateDataLinkColor(string level, string message)
        {
            switch (level.ToLower())
            {
                case "error":
                    if (DebugLevel.Error >= CurrentDebugLevel)
                    {
                        CurrentDebugLevel = DebugLevel.Error;
                    }

                    break;
                case "warn":
                    if (DebugLevel.Warning >= CurrentDebugLevel)
                    {
                        CurrentDebugLevel = DebugLevel.Warning;
                    }

                    break;
                default:
                    if (DebugLevel.Debug >= CurrentDebugLevel)
                    {
                        CurrentDebugLevel = DebugLevel.Debug;
                    }

                    break;
            }

            Debug.WriteLine($"NLog | {level} | {message}");
        }

        private void DebugLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            LinkLabel lnk = (LinkLabel)sender;
            if (e.Link?.LinkData?.ToString() == null)
            {
                return;
            }
            //_ = new LinkLabel();
            string path = e.Link.LinkData.ToString()!;
            lnk.Links[lnk.Links.IndexOf(e.Link)].Visited = true;

            //
            //File.SetAttributes(path, attributes | FileAttributes.ReadOnly);
            //_ = System.Diagnostics.Process.Start(path);
            //File.SetAttributes(path, attributes);

            FileAttributes attributes = File.GetAttributes(path);
            string contents = string.Empty;
            using (FileStream fs = new(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using StreamReader sr = new(fs);
                contents = sr.ReadToEnd();
            }
            File.SetAttributes(path, attributes);

            LogViewer logViewer = new();

            logViewer.textBox1.Text = contents;
            logViewer.textBox1.Refresh();
            logViewer.Show();
            logViewer.textBox1.SelectionStart = logViewer.textBox1.TextLength;
            logViewer.textBox1.ScrollToCaret();
        }

        private void UpdateDebugLinkBackColor()
        {
            switch (CurrentDebugLevel)
            {
                case DebugLevel.Error:
                    linkLabel1.BackColor = Color.Red;
                    break;
                case DebugLevel.Warning:
                    linkLabel1.BackColor = Color.Yellow;
                    break;
                default:
                    break;
            }
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

            WaitCursor.Push();
            Logger.Debug("Backup button has been pressed.");
            LoadCharacters();
            BackupToChimpRepository();
            Logger.Debug($"Backup created at {BackupRepositoryFullPath}");
            RestoreButton.Enabled = File.Exists(BackupRepositoryFullPath);
            UpdateDebugLinkBackColor();
            WaitCursor.Pop();

        }
        private void RestoreButton_Click(object sender, EventArgs e)
        {
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
            UpdateDebugLinkBackColor();
            WaitCursor.Pop();
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
            UpdateDebugLinkBackColor();
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
        }

        private void AttachCharacters()
        {
            BindingSource.DataSource = CharactersByAccountLastDateUpdated?.ToChimpJsonList() ?? new();
            if (BindingSource.Count > 0)
            {
                RefreshButton.Enabled = true;
                RefreshAllButton.Enabled = true;
            }
            else
            {
                RefreshButton.Enabled = false;
                RefreshAllButton.Enabled = false;
            }
            //SearchGridView.DataSource = null;
            //SearchGridView.DataSource = CharactersByAccountLastDateUpdated_Json;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
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
                UpdateDebugLinkBackColor();
            }
        }

        private void RefreshAllButton_Click(object sender, EventArgs e)
        {
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
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
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
                UpdateDebugLinkBackColor();
                WaitCursor.Pop();
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
            }

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
                UpdateDebugLinkBackColor();
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

            AddNewCharacter();

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
    }
}
