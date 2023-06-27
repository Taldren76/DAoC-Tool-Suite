using Dapper;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace SQLLibrary
{
    public class DateQuery
    {
        public int Index { get; set; }
        public DateTime DateMax { get; set; }

    }

    public class SqliteDataAccess
    {   //Environment.SpecialFolder.ApplicationData
        private static readonly string DataBasePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Taldren, Inc\\DAoC Tool Suite\\";
        private static readonly string ExecutionPath = Directory.GetCurrentDirectory();
        private static readonly string DataBaseFileName = "CharacterDB.db";
        public static string DataBaseLocation => Path.Combine(DataBasePath, DataBaseFileName);
        private static string CleanDataBaseLocation => Path.Combine(ExecutionPath, DataBaseFileName);

        private static readonly object thisLock = new();
        private static readonly string CharactersColumnNames = "Date,Account,WebID,FirstName,Name,Realm,Class,Server,TotalRealmPoints,TotalSoloKills,TotalDeathBlows,TotalKills,TotalDeaths,Level,Race,BountyPoints,MasterLevel_Name,Masterlevel_Level,Guild_WebID,Alchemy,Armorcraft,Fletching,Siegecraft,Spellcrafting,Tailoring,Weaponcraft,Albion_SoloKills,Albion_DeathBlows,Albion_Kills,Albion_Deaths,Hibernia_SoloKills,Hibernia_DeathBlows,Hibernia_Kills,Hibernia_Deaths,Midgard_SoloKills,Midgard_DeathBlows,Midgard_Kills,Midgard_Deaths";
        private static string CharactersColumnValues => $"@{CharactersColumnNames.Replace(",", ",@")}";

        public static void TraceLog(string? message)
        {
            string toWrite = $"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: {message ?? ""}";
            Trace.WriteLine(toWrite);
        }


        #region LaunchModel
        //Character names can change, but the WebID is unique to that character.
        public static AHKModel? LoadAHKModelByWebID(string webID, string account)
        {
            lock (thisLock)
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                string launchModelQuery = $"SELECT [AHKScriptPath],[Version] FROM [AHK] WHERE [WebID] = '{webID}'AND [Account] = '{account}'";
                List<AHKModel> characters = conn.Query<AHKModel>(launchModelQuery, new DynamicParameters()).ToList();
                return characters?.FirstOrDefault();
            }
        }

        public static void DelAHK(string webID, string accountName)
        {
            lock (thisLock)
            {
                string tableName = "AHK";
                string deleteAccountQuery = $"Delete From [{tableName}] Where [WebID] = '{webID}' AND [Account] = '{accountName}'";
                string countQuery = $"Select Count([WebID]) from [{tableName}] Where [Account] = '{accountName}'";
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                if (count > 0)
                {
                    _ = conn.Query(deleteAccountQuery, new DynamicParameters());
                }
            }
        }

        public static void AddAHKModel(string webID, string account, string? ahkScriptPath = null, int version = 1)
        {
            AHKModel model = new()
            {
                WebID = webID,
                AHKScriptPath = ahkScriptPath,
                Account = account,
                Version = version
            };
            AddAHKModel(model);
        }

        public static void AddAHKModel(AHKModel model)
        {
            lock (thisLock)
            {
                string columnNames = "WebID,Account,AHKScriptPath";
                string values = $"@{columnNames.Replace(",", ",@")}";
                string tableName = "AHK";
                string writeQuery = $"Insert into [{tableName}] ({columnNames}) values ({values})";
                string countQuery = $"Select Count([AHKScriptPath]) from [{tableName}] Where [WebID] = '{model.WebID}' AND [Account] = '{model.Account}'";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());

                int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                if (count > 0)
                {
                    return;
                }

                _ = conn.Execute(writeQuery, model);
            }
        }
        public static void UpdateAHKScriptByWEBID(string webID, string account, string AHKScriptPath)
        {
            lock (thisLock)
            {
                try
                {
                    using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                    string updateQuery = $"UPDATE [Launcher] SET [AHKScriptPath] = '{AHKScriptPath}' WHERE [WebID] = '{webID}' AND [Account] = '{account}'";
                    _ = conn.Execute(updateQuery, new DynamicParameters());
                }
                catch (Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                }
            }
        }
        #endregion

        #region CharacterModel
        public static List<CharacterModel> LoadCharacters()
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            string characterLoadQuery = $"Select {CharactersColumnNames} from [Characters]";
            List<CharacterModel> characters = conn.Query<CharacterModel>(characterLoadQuery, new DynamicParameters()).ToList();
            return characters;
        }

        public static CharacterModel? LoadCharacterByFirstName(string characterName)
        {
            lock (thisLock)
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                string characterLoadQuery = $"Select {CharactersColumnNames} from [Characters] Where [FirstName] = '{characterName}'";
                List<CharacterModel> characters = conn.Query<CharacterModel>(characterLoadQuery, new DynamicParameters()).ToList();
                return characters?.OrderByDescending(y => y.DateTime)?.FirstOrDefault();
            }
        }

        public static CharacterModel? LoadCharacterByWebID(string webID)
        {
            lock (thisLock)
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                string characterLoadQuery = $"Select {CharactersColumnNames} from [Characters] Where [WebID] = '{webID}'";
                List<CharacterModel> characters = conn.Query<CharacterModel>(characterLoadQuery, new DynamicParameters()).ToList();
                return characters?.OrderByDescending(y => y.DateTime)?.FirstOrDefault();
            }
        }

        public static void AddCharacter(CharacterModel character, DateTime date, string accountName)
        {
            lock (thisLock)
            {
                string tableName = "Characters";
                string writeQuery = $"Insert into [{tableName}] ({CharactersColumnNames}) values ({CharactersColumnValues})";
                string exactCountQuery = $"Select Count([WebID]) from [{tableName}] Where [WebID] = '{character.WebID}' And [Name] = '{character.Name}'";
                string countQuery = $"Select Count([WebID]) from [{tableName}] Where [WebID] = '{character.WebID}'";
                string minDateIndexQuery = $"Select [index], min([Date]) from [{tableName}] Where [WebID] = '{character.WebID}'";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());

                //Check if character name changed
                int exactCount = conn.QueryFirst<int>(exactCountQuery, new DynamicParameters());
                int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                if (exactCount < 1 && count > 0)
                {
                    //Update existing names before continueing
                    string updateQuery = $"Update [{tableName}] Set [Name] = '{character.Name}' Where [WebID] = '{character.WebID}'";
                    _ = conn.Execute(updateQuery, new DynamicParameters());

                }

                int maxSQLEnteriesPerCharacter = Properties.Settings.Default.MaxSQLEntriesPerCharacter;
                DateTime endTime = DateTime.Now.AddSeconds(60);
                while (endTime > DateTime.Now && maxSQLEnteriesPerCharacter > 0 && count > maxSQLEnteriesPerCharacter - 1)
                {
                    DateQuery minDateQuery = conn.QueryFirst<DateQuery>(minDateIndexQuery, new DynamicParameters());
                    string deleteQuery = $"Delete From [{tableName}] Where [index] = '{minDateQuery.Index}'";
                    _ = conn.Query(deleteQuery, new DynamicParameters());
                    count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                }
                character.Date = date.ToString("yyyy-MM-ddTHH:mm:ss");
                character.Account = accountName;
                _ = conn.Execute(writeQuery, character);
            }
        }
        public static void AddCharacters(List<CharacterModel> characters, DateTime date, string accountName)
        {
            lock (thisLock)
            {
                string tableName = "Characters";
                string writeQuery = $"Insert into [{tableName}] ({CharactersColumnNames}) values ({CharactersColumnValues})";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                foreach (CharacterModel character in characters)
                {
                    string exactCountQuery = $"Select Count([WebID]) from [{tableName}] Where [WebID] = '{character.WebID}' And [Name] = '{character.Name}'";
                    string countQuery = $"Select Count([WebID]) from [{tableName}] Where [WebID] = '{character.WebID}'";
                    string minDateIndexQuery = $"Select [index], min([Date]) from [{tableName}] Where [WebID] = '{character.WebID}'";
                    //Check if character name changed
                    int exactCount = conn.QueryFirst<int>(exactCountQuery, new DynamicParameters());
                    int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                    if (exactCount < 1 && count > 0)
                    {
                        //Update existing names before continueing
                        string updateQuery = $"Update [{tableName}] Set [Name] = '{character.Name}' Where [WebID] = '{character.WebID}'";
                        _ = conn.Execute(updateQuery, new DynamicParameters());
                    }

                    int maxSQLEnteriesPerCharacter = Properties.Settings.Default.MaxSQLEntriesPerCharacter;
                    DateTime endTime = DateTime.Now.AddSeconds(10);
                    while (endTime > DateTime.Now && maxSQLEnteriesPerCharacter > 0 && count > maxSQLEnteriesPerCharacter - 1)
                    {
                        DateQuery minDateQuery = conn.QueryFirst<DateQuery>(minDateIndexQuery, new DynamicParameters());
                        string deleteQuery = $"Delete From [{tableName}] Where [index] = {minDateQuery.Index}";
                        _ = conn.Query(deleteQuery, new DynamicParameters());
                        count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                    }
                    character.Date = date.ToString("yyyy-MM-ddTHH:mm:ss");
                    character.Account = accountName;
                    _ = conn.Execute(writeQuery, character);
                }
            }
        }

        public static void DeleteCharacter(string webID)
        {
            lock (thisLock)
            {
                string tableName = "Characters";
                string deleteQuery = $"Delete From [{tableName}] Where [WebID] = '{webID}'";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                _ = conn.Query(deleteQuery, new DynamicParameters());
            }
        }
        public static void DeleteCharacterWithAccount(string accountName)
        {
            lock (thisLock)
            {
                string tableName = "Characters";
                string deleteAccountQuery = $"Delete From [{tableName}] Where [Account] = '{accountName}'";
                string countQuery = $"Select Count([Account]) from [{tableName}] Where [Account] = '{accountName}'";


                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                if (count > 0)
                {
                    _ = conn.Query(deleteAccountQuery, new DynamicParameters());
                }
            }
        }
        #endregion

        #region AccountModel
        public static List<AccountModel> LoadAccounts()
        {

            lock (thisLock)
            {
                try
                {
                    using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                    string columnNames = "[Account], [index]";
                    string queyString = $"Select {columnNames} from [Accounts]";
                    List<AccountModel> query = conn.Query<AccountModel>(queyString, new DynamicParameters()).ToList();
                    return query;
                }
                catch (Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                    return new();
                }
            }
        }

        public static List<CredentialModel> LoadAccountCredentials(string accountName)
        {

            lock (thisLock)
            {
                try
                {
                    using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                    string columnNames = "[Login], [Password]";
                    string queryString = $"Select {columnNames} from [Accounts] Where [Account] = '{accountName}'";
                    List<CredentialModel> query = conn.Query<CredentialModel>(queryString, new DynamicParameters()).ToList();
                    return query;
                }
                catch (Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                    return new();
                }
            }
        }

        public static List<CredentialModel> AddAccountCredentials(string accountName, string? login, string? passWord)
        {
            lock (thisLock)
            {
                try
                {
                    using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                    string columnNames = $"[Login] = '{login}', [Password] = '{passWord}'";
                    List<CredentialModel> query = conn.Query<CredentialModel>($"UPDATE [Accounts] SET {columnNames} Where [Account] = '{accountName}'", new DynamicParameters()).ToList();
                    return query;
                }
                catch (Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                    return new();
                }
            }
        }

        public static void AddAccount(string accountName)
        {
            lock (thisLock)
            {
                string columnNames = "Account";
                string values = "@Account";
                string tableName = "Accounts";
                string writeQuery = $"Insert into [{tableName}] ({columnNames}) values ({values})";
                string countQuery = $"Select Count([Account]) from [{tableName}] Where [Account] = '{accountName}'";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());

                int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                if (count > 0)
                {
                    return;
                }

                AccountModel accountModel = new()
                {
                    Account = accountName
                };
                _ = conn.Execute(writeQuery, accountModel);
            }
        }

        public static void RenameAccount(string accountNameOld, string accountNameNew)
        {
            string tableName = "Accounts";
            _ = $"Select [index] from [{tableName}] Where [Account] = '{accountNameOld}'";
            string renameAccountQuery = $"UPDATE [{tableName}] SET [Account] = '{accountNameNew}' Where [Account] = '{accountNameOld}'";
            string countOldQuery = $"Select Count([Account]) from [{tableName}] Where [Account] = '{accountNameOld}'";
            string countNewQuery = $"Select Count([Account]) from [{tableName}] Where [Account] = '{accountNameNew}'";

            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());

            int countNew = conn.QueryFirst<int>(countNewQuery, new DynamicParameters());
            if (countNew > 0)
            {
                TraceLog($"There is already an account named {accountNameNew} in the database. Aborting.");
                return;
            }

            int countOld = conn.QueryFirst<int>(countOldQuery, new DynamicParameters());
            if (countOld > 0)
            {
                lock (thisLock)
                {
                    _ = conn.Query(renameAccountQuery, new DynamicParameters());
                }
                RenameAccountInCharacterTable(accountNameOld, accountNameNew);
            }
            else
            {
                TraceLog($"There is no account named {accountNameOld} in the database. Aborting.");
                return;
            }

        }

        public static void RenameAccountInCharacterTable(string accountNameOld, string accountNameNew)
        {
            string tableName = "Characters";
            string renameAccountQuery = $"UPDATE [{tableName}] SET [Account] = '{accountNameNew}' Where [Account] = '{accountNameOld}'";
            string countQuery = $"Select Count([Account]) from [{tableName}] Where [Account] = '{accountNameOld}'";

            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
            if (count > 0)
            {
                lock (thisLock)
                {
                    _ = conn.Query(renameAccountQuery, new DynamicParameters());
                }
            }
            else
            {
                TraceLog($"There is no characters associated with account {accountNameOld} in the database. Aborting.");
                return;
            }

        }

        public static void RemoveAccount(string accountName)
        {
            string tableName = "Accounts";
            string indexQuery = $"Select [index] from [{tableName}] Where [Account] = '{accountName}'";
            string deleteAccountQuery = $"Delete From [{tableName}] Where [Account] = '{accountName}'";
            string countQuery = $"Select Count([Account]) from [{tableName}] Where [Account] = '{accountName}'";

            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            int index = conn.QueryFirst<int>(indexQuery, new DynamicParameters());
            if (index == 1)
            {
                TraceLog("Attempted to delete the Default account index. Aborting.");
                return;
            }
            int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
            if (count > 0)
            {
                lock (thisLock)
                {
                    _ = conn.Query(deleteAccountQuery, new DynamicParameters());
                }
                DeleteCharacterWithAccount(accountName);
            }
        }
        #endregion

        #region GuildModel
        public static List<GuildModel> LoadGuilds()
        {

            lock (thisLock)
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                List<GuildModel> query = conn.Query<GuildModel>("Select * from [Guilds]", new DynamicParameters()).ToList();
                return query;
            }
        }
        public static void AddGuild(GuildModel guild)
        {
            lock (thisLock)
            {
                if (!guild.IsValid)
                {
                    return;
                }

                string columnNames = "WebID,Name";
                string values = "@WebID,@Name";
                string tableName = "Guilds";
                string writeQuery = $"Insert into [{tableName}] ({columnNames}) values ({values})";
                string exactCountQuery = $"Select Count([WebID]) from [{tableName}] Where [WebID] = '{guild.WebID}' and [Name] = '{guild.Name}'";
                string countQuery = $"Select Count([WebID]) from [{tableName}] Where [WebID] = '{guild.WebID}'";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                //Check if Guild entry is already present.
                int exactCount = conn.QueryFirst<int>(exactCountQuery, new DynamicParameters());
                if (exactCount > 0)
                {
                    return; //Already exists
                }

                //Check for guild name change
                int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                if (count > 0)
                {
                    string updateQuery = $"Update [{tableName}] SET [Name] = '{guild.Name}' Where [WebID] = '{guild.WebID}'";
                    _ = conn.Query(updateQuery, new DynamicParameters());
                    return; //Entry Updated
                }

                //Add new guild entry
                _ = conn.Execute(writeQuery, guild);
            }
        }
        public static void AddGuilds(List<GuildModel> guilds)
        {
            lock (thisLock)
            {
                string columnNames = "WebID,Name";
                string values = "@WebID,@Name";
                string tableName = "Guilds";
                string writeQuery = $"Insert into [{tableName}] ({columnNames}) values ({values})";
                //string exactCountQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{guilds.WebID}\" and Name = \"{guilds.Name}\"";
                //string countQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{guilds.WebID}\"";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                foreach (GuildModel guild in guilds)
                {
                    if (!guild.IsValid)
                    {
                        continue;
                    }
                    string exactCountQuery = $"Select Count([WebID]) from [{tableName}] Where [WebID] = '{guild.WebID}' and [Name] = '{guild.Name}'";
                    string countQuery = $"Select Count([WebID]) from [{tableName}] Where [WebID] = '{guild.WebID}'";
                    //Check if Guild entry is already present.
                    int exactCount = conn.QueryFirst<int>(exactCountQuery, new DynamicParameters());
                    if (exactCount > 0)
                    {
                        return; //Already exists
                    }

                    //Check for guild name change
                    int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                    if (count > 0)
                    {
                        string updateQuery = $"Update [{tableName}] SET [Name] = '{guild.Name}' Where [WebID] = '{guild.WebID}'";
                        _ = conn.Query(updateQuery, new DynamicParameters());
                        return; //Entry Updated
                    }

                    //Add new guild entry
                    _ = conn.Execute(writeQuery, guilds);
                }
            }
        }
        public static void RemoveGuild(string webID)
        {

            string tableName = "Guilds";
            string deleteAccountQuery = $"Delete From [{tableName}] Where [WebID] = '{webID}'";
            string countQuery = $"Select Count([WebID]) from [{tableName}] Where [WebID] = '{webID}'";

            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
            if (count > 0)
            {
                lock (thisLock)
                {
                    _ = conn.Query(deleteAccountQuery, new DynamicParameters());
                }
                DeleteCharacterWithAccount(webID);
            }

        }
        #endregion

        #region SettingsBackUpModel
        public static List<SettingsBackUpModel> LoadSettingBackUps()
        {
            lock (thisLock)
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                List<SettingsBackUpModel> query = conn.Query<SettingsBackUpModel>("Select * from [SettingsBackup]", new DynamicParameters()).ToList();
                return query;
            }
        }

        public static List<SettingsBackUpModel> LoadSettingBackUps(string firstName, string realm, string _class)
        {
            lock (thisLock)
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                List<SettingsBackUpModel> query = conn.Query<SettingsBackUpModel>($"SELECT * FROM [SettingsBackup] WHERE [FirstName] = '{firstName}' AND [Realm] = '{realm}' AND [Class] = '{_class}'", new DynamicParameters()).ToList();
                return query;
            }
        }

        public static List<SettingsBackUpModel> LoadSettingBackUps(string characterFirstName)
        {
            lock (thisLock)
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                List<SettingsBackUpModel> query = conn.Query<SettingsBackUpModel>($"Select * from [SettingsBackup] Where [FirstName] = '{characterFirstName}'", new DynamicParameters()).ToList();
                return query;
            }
        }
        public static SettingsBackUpModel LoadSettingByIndex(int index)
        {
            lock (thisLock)
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                List<SettingsBackUpModel> query = conn.Query<SettingsBackUpModel>($"Select * from [SettingsBackup] Where [index] = {index}", new DynamicParameters()).ToList();
                return query.First();
            }
        }
        public static void AddSettingBackup(SettingsBackUpModel settingsBackUpModel, DateTime date)
        {
            lock (thisLock)
            {
                string tableName = "SettingsBackup";
                string settingsBackupColumnNames = "Date,FirstName,Realm,Class,Description,Path,INIFileName,INIData,IGNFileName,IGNData";
                string settingsBackupColumnValues = $"@{settingsBackupColumnNames.Replace(",", ",@")}";
                string writeQuery = $"Insert into [{tableName}] ({settingsBackupColumnNames}) values ({settingsBackupColumnValues})";
                settingsBackUpModel.Date = date.ToString("yyyy-MM-ddTHH:mm:ss");
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                _ = conn.Execute(writeQuery, settingsBackUpModel);
            }
        }
        public static void DeleteSettingBackupByFirstName(string firstName)
        {
            lock (thisLock)
            {
                string tableName = "SettingsBackup";
                string deleteAccountQuery = $"Delete From [{tableName}] Where [FirstName] = '{firstName}'";
                string countQuery = $"Select Count(FirstName) from [{tableName}] Where [FirstName] = '{firstName}'";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                if (count > 0)
                {
                    _ = conn.Query(deleteAccountQuery, new DynamicParameters());
                }
            }
        }
        public static void DeleteSettingBackupByIndex(int index)
        {
            lock (thisLock)
            {
                string tableName = "SettingsBackup";
                string deleteBackupQuery = $"Delete From [{tableName}] Where [index] = {index}";
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                _ = conn.Query(deleteBackupQuery, new DynamicParameters());
            }
        }

        public static void UpdateEntryByIndex(int index, SettingsBackUpModel settingsBackUpModel)
        {
            lock (thisLock)
            {
                string tableName = "SettingsBackup";
                string updateBackupQuery = $"UPDATE [{tableName}] SET [Realm] = '{settingsBackUpModel.Realm}', [Class] = '{settingsBackUpModel.Class}', [Description] = '{settingsBackUpModel.Description}' WHERE [index] = {index}";
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                _ = conn.Query(updateBackupQuery, new DynamicParameters());
            }
        }
        #endregion

        private static bool ConnectionStringLoaded = false;
        private static string LoadConnectionString(string id = "Default")
        {
            EnsureDataBaseLocation();

            //new SQLiteConnection("Data Source=C:\SQLITEDATABASES\SQLITEDB1.sqlite;Version=3;");
            string connectionString = id switch
            {
                "Default" => $"Data Source={DataBaseLocation};Version=3;",
                _ => Properties.Settings.Default.Properties[id].ToString() ?? Properties.Settings.Default.ConnectionString,
            };
            if (!ConnectionStringLoaded)
            {
                TraceLog($"Using connection string {id}:{connectionString}");
                ConnectionStringLoaded = true;
            }
            return connectionString;//ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        private static void EnsureDataBaseLocation()
        {
            if (!Directory.Exists(DataBaseLocation))
            {
                _ = Directory.CreateDirectory(DataBasePath);
            }
            if (!File.Exists(DataBaseLocation))
            {
                File.Copy(CleanDataBaseLocation, DataBaseLocation);
            }
        }

        public static void ReIndexTables()
        {
            lock (thisLock)
            {
                List<string> tables = new() { "Accounts", "Characters", "Guilds" };
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                foreach (string tableName in tables)
                {
                    string reindexQuery = $"Reindex {tableName}";
                    _ = conn.Query(reindexQuery, new DynamicParameters());
                }
            }
        }

        public static void ResetSettingsBackup()
        {
            lock (thisLock)
            {
                List<string> tables = new() { "SettingsBackup" };
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                foreach (string tableName in tables)
                {
                    string deleteQuery = $"Delete from {tableName}";
                    string resetSequence = $"Update [sqlite_sequence] SET [seq] = 0 WHERE [name] = '{tableName}'";
                    string reindexQuery = $"Reindex {tableName}";
                    _ = conn.Query(deleteQuery, new DynamicParameters());
                    _ = conn.Query(resetSequence, new DynamicParameters());
                    _ = conn.Query(reindexQuery, new DynamicParameters());
                }
            }
        }

        public static void ResetTables()
        {
            lock (thisLock)
            {
                List<string> tables = new() { "Accounts", "Characters", "Guilds", "AHK" };
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                foreach (string tableName in tables)
                {
                    string deleteQuery = $"Delete from {tableName}";
                    string resetSequence = $"Update [sqlite_sequence] SET [seq] = 0 WHERE [name] = '{tableName}'";
                    string reindexQuery = $"Reindex {tableName}";
                    _ = conn.Query(deleteQuery, new DynamicParameters());
                    _ = conn.Query(resetSequence, new DynamicParameters());
                    _ = conn.Query(reindexQuery, new DynamicParameters());
                }
            }
        }

        public static bool BackupDB(string srcFullPath, string destFullPath)
        {
            lock (thisLock)
            {
                try
                {
                    if (File.Exists(destFullPath))
                    {
                        File.Delete(destFullPath);
                    }

                    if (!File.Exists(srcFullPath))
                    {
                        TraceLog($"Could not locate file {srcFullPath}");
                        return false;
                    }
                    File.Copy(srcFullPath, destFullPath, true);
                    return true;
                }
                catch (Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                    return false;
                }
            }
        }

        public static bool RestoreDB(string srcFullPath, string destFullPath, bool IsCopy = false)
        {
            lock (thisLock)
            {
                try
                {
                    if (!File.Exists(srcFullPath))
                    {
                        TraceLog($"Could not locate file {srcFullPath}");
                        return false;
                    }

                    if (File.Exists(destFullPath))
                    {
                        File.Delete(destFullPath);
                    }

                    if (IsCopy)
                    {
                        _ = BackupDB(srcFullPath, destFullPath);
                    }
                    else
                    {
                        File.Move(srcFullPath, destFullPath);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    TraceLog(ex.Message);
                    TraceLog(ex.StackTrace);
                    return false;
                }
            }
        }

    }
}
