using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using Dapper;

namespace SQLLibrary
{
    public class DateQuery
    {
        public int Index { get; set; }
        public DateTime DateMax { get; set; }

    }

    public class SqliteDataAccess
    {
        private static readonly object thisLock = new();
        private static readonly string CharactersColumnNames = "Date,Account,WebID,FirstName,Name,Realm,Class,Server,TotalRealmPoints,TotalSoloKills,TotalDeathBlows,TotalKills,TotalDeaths,Level,Race,BountyPoints,MasterLevel_Name,Masterlevel_Level,Guild_WebID,Alchemy,Armorcraft,Fletching,Siegecraft,Spellcrafting,Tailoring,Weaponcraft,Albion_SoloKills,Albion_DeathBlows,Albion_Kills,Albion_Deaths,Hibernia_SoloKills,Hibernia_DeathBlows,Hibernia_Kills,Hibernia_Deaths,Midgard_SoloKills,Midgard_DeathBlows,Midgard_Kills,Midgard_Deaths";
        private static string CharactersColumnValues => $"@{CharactersColumnNames.Replace(",", ",@")}";
        public static void TraceLog(string? message)
        {
            string toWrite = $"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: {message ?? ""}";
            Trace.WriteLine(toWrite);
        }

        #region CharacterModel
        public static List<CharacterModel> LoadCharacters()
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            string characterLoadQuery = $"Select {CharactersColumnNames} from Characters";
            List<CharacterModel> characters = conn.Query<CharacterModel>(characterLoadQuery, new DynamicParameters()).ToList();
            return characters;
        }
        public static CharacterModel? LoadCharacterByFirstName(string characterName)
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            string characterLoadQuery = $"Select {CharactersColumnNames} from Characters Where \"FirstName\" = \"{characterName}\"";
            List<CharacterModel> characters = conn.Query<CharacterModel>(characterLoadQuery, new DynamicParameters()).ToList();
            return characters?.OrderByDescending(y => y.DateTime)?.FirstOrDefault();
        }
        public static CharacterModel? LoadCharacterByWebID(string webID)
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            string characterLoadQuery = $"Select {CharactersColumnNames} from Characters Where \"WebID\" = \"{webID}\"";
            List<CharacterModel> characters = conn.Query<CharacterModel>(characterLoadQuery, new DynamicParameters()).ToList();
            return characters?.OrderByDescending(y => y.DateTime)?.FirstOrDefault();
        }
        public static void AddCharacter(CharacterModel character, DateTime date, string accountName)
        {
            lock (thisLock)
            {
                string tableName = "Characters";
                string writeQuery = $"Insert into {tableName} ({CharactersColumnNames}) values ({CharactersColumnValues})";
                string exactCountQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{character.WebID}\" And Name = \"{character.Name}\"";
                string countQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{character.WebID}\"";
                string minDateIndexQuery = $"Select \"index\", min(Date) from {tableName} Where WebID = \"{character.WebID}\"";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                //Check if character name changed
                int exactCount = conn.QueryFirst<int>(exactCountQuery, new DynamicParameters());
                int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                if (exactCount < 1 && count > 0)
                {
                    //Update existing names before continueing
                    string updateQuery = $"Update {tableName} Set Name = \"{character.Name}\" Where WebID = \"{character.WebID}\"";
                    _ = conn.Execute(updateQuery, new DynamicParameters());
                }

                int maxSQLEnteriesPerCharacter = Properties.Settings.Default.MaxSQLEntriesPerCharacter; //ConfigurationManager.AppSettings["MaxSQLEntriesPerCharacter"] ?? "2";
                //int maxCount = int.TryParse(maxSQLEnteriesPerCharacter, out maxCount) ? maxCount - 1 : 1;
                DateTime endTime = DateTime.Now.AddSeconds(10);
                while (endTime > DateTime.Now && maxSQLEnteriesPerCharacter > 0 && count > maxSQLEnteriesPerCharacter)
                {
                    DateQuery minDateQuery = conn.QueryFirst<DateQuery>(minDateIndexQuery, new DynamicParameters());
                    string deleteQuery = $"Delete From {tableName} Where \"index\" = {minDateQuery.Index}";
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
                string writeQuery = $"Insert into {tableName} ({CharactersColumnNames}) values ({CharactersColumnValues})";
                //string exactCountQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{character.WebID}\" And Name = \"{character.Name}\"";
                //string countQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{character.WebID}\"";
                //string minDateIndexQuery = $"Select \"index\", min(Date) from {tableName} Where WebID = \"{character.WebID}\"";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                foreach (CharacterModel character in characters)
                {
                    string exactCountQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{character.WebID}\" And Name = \"{character.Name}\"";
                    string countQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{character.WebID}\"";
                    string minDateIndexQuery = $"Select \"index\", min(Date) from {tableName} Where WebID = \"{character.WebID}\"";
                    //Check if character name changed
                    int exactCount = conn.QueryFirst<int>(exactCountQuery, new DynamicParameters());
                    int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                    if (exactCount < 1 && count > 0)
                    {
                        //Update existing names before continueing
                        string updateQuery = $"Update {tableName} Set Name = \"{character.Name}\" Where WebID = \"{character.WebID}\"";
                        _ = conn.Execute(updateQuery, new DynamicParameters());
                    }

                    int maxSQLEnteriesPerCharacter = Properties.Settings.Default.MaxSQLEntriesPerCharacter; //ConfigurationManager.AppSettings["MaxSQLEntriesPerCharacter"] ?? "2";
                    //int maxCount = int.TryParse(maxSQLEnteriesPerCharacter, out maxCount) ? maxCount - 1 : 1;
                    DateTime endTime = DateTime.Now.AddSeconds(10);
                    while (endTime > DateTime.Now && maxSQLEnteriesPerCharacter > 0 && count > maxSQLEnteriesPerCharacter)
                    {
                        DateQuery minDateQuery = conn.QueryFirst<DateQuery>(minDateIndexQuery, new DynamicParameters());
                        string deleteQuery = $"Delete From {tableName} Where \"index\" = {minDateQuery.Index}";
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
                string deleteQuery = $"Delete From {tableName} Where WebID = \"{webID}\"";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                _ = conn.Query(deleteQuery, new DynamicParameters());
            }
        }
        public static void DeleteCharacterWithAccount(string accountName)
        {
            lock (thisLock)
            {
                string tableName = "Characters";
                string deleteAccountQuery = $"Delete From {tableName} Where Account = \"{accountName}\"";
                string countQuery = $"Select Count(Account) from {tableName} Where Account = \"{accountName}\"";


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
            try
            {
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                List<AccountModel> query = conn.Query<AccountModel>("Select * from Accounts", new DynamicParameters()).ToList();
                return query;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new();
            }
        }
        public static void AddAccount(string accountName)
        {
            lock (thisLock)
            {
                string columnNames = "Account";
                string values = "@Account";
                string tableName = "Accounts";
                string writeQuery = $"Insert into {tableName} ({columnNames}) values ({values})";
                string countQuery = $"Select Count(Account) from {tableName} Where Account = \"{accountName}\"";

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
        public static void RemoveAccount(string accountName)
        {
            lock (thisLock)
            {
                if (accountName == "Default")
                {
                    return;
                }

                string tableName = "Accounts";
                string deleteAccountQuery = $"Delete From {tableName} Where Account = \"{accountName}\"";
                string countQuery = $"Select Count(Account) from {tableName} Where Account = \"{accountName}\"";


                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                if (count > 0)
                {
                    _ = conn.Query(deleteAccountQuery, new DynamicParameters());
                    DeleteCharacterWithAccount(accountName);
                }
            }
        }
        #endregion

        #region GuildModel
        public static List<GuildModel> LoadGuilds()
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            List<GuildModel> query = conn.Query<GuildModel>("Select * from Guilds", new DynamicParameters()).ToList();
            return query;
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
                string writeQuery = $"Insert into {tableName} ({columnNames}) values ({values})";
                string exactCountQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{guild.WebID}\" and Name = \"{guild.Name}\"";
                string countQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{guild.WebID}\"";

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
                    string updateQuery = $"Update {tableName} SET Name = {guild.Name} Where WebID = {guild.WebID}";
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
                string writeQuery = $"Insert into {tableName} ({columnNames}) values ({values})";
                //string exactCountQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{guilds.WebID}\" and Name = \"{guilds.Name}\"";
                //string countQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{guilds.WebID}\"";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                foreach (GuildModel guild in guilds)
                {
                    if (!guild.IsValid)
                    {
                        continue;
                    }
                    string exactCountQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{guild.WebID}\" and Name = \"{guild.Name}\"";
                    string countQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{guild.WebID}\"";
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
                        string updateQuery = $"Update {tableName} SET Name = {guild.Name} Where WebID = {guild.WebID}";
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
            lock (thisLock)
            {
                string tableName = "Guilds";
                string deleteAccountQuery = $"Delete From {tableName} Where WebID = \"{webID}\"";
                string countQuery = $"Select Count(WebID) from {tableName} Where WebID = \"{webID}\"";

                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                int count = conn.QueryFirst<int>(countQuery, new DynamicParameters());
                if (count > 0)
                {
                    _ = conn.Query(deleteAccountQuery, new DynamicParameters());
                    DeleteCharacterWithAccount(webID);
                }
            }
        }
        #endregion

        #region SettingsBackUpModel
        public static List<SettingsBackUpModel> LoadSettingBackUps()
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            List<SettingsBackUpModel> query = conn.Query<SettingsBackUpModel>("Select * from SettingsBackup", new DynamicParameters()).ToList();
            return query;
        }
        public static List<SettingsBackUpModel> LoadSettingBackUps(string characterFirstName)
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            List<SettingsBackUpModel> query = conn.Query<SettingsBackUpModel>($"Select * from SettingsBackup Where \"FirstName\" = \"{characterFirstName}\"", new DynamicParameters()).ToList();
            return query;
        }
        public static SettingsBackUpModel LoadSettingByIndex(int index)
        {
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            List<SettingsBackUpModel> query = conn.Query<SettingsBackUpModel>($"Select * from SettingsBackup Where \"index\" = {index}", new DynamicParameters()).ToList();
            return query.First();
        }
        public static void AddSettingBackup(SettingsBackUpModel settingsBackUpModel, DateTime date)
        {
            lock (thisLock)
            {
                string tableName = "SettingsBackup";
                string settingsBackupColumnNames = "Date,FirstName,Realm,Class,Description,Path,INIFileName,INIData,IGNFileName,IGNData";
                string settingsBackupColumnValues = $"@{settingsBackupColumnNames.Replace(",", ",@")}";
                string writeQuery = $"Insert into {tableName} ({settingsBackupColumnNames}) values ({settingsBackupColumnValues})";
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
                string deleteAccountQuery = $"Delete From {tableName} Where FirstName = \"{firstName}\"";
                string countQuery = $"Select Count(FirstName) from {tableName} Where FirstName = \"{firstName}\"";

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
                string deleteBackupQuery = $"Delete From {tableName} Where \"index\" = {index}";
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                _ = conn.Query(deleteBackupQuery, new DynamicParameters());
            }
        }

        public static void UpdateEntryByIndex(int index, SettingsBackUpModel settingsBackUpModel)
        {
            string tableName = "SettingsBackup";
            string updateBackupQuery = $"UPDATE {tableName} SET Realm = '{settingsBackUpModel.Realm}', Class = '{settingsBackUpModel.Class}', Description = '{settingsBackUpModel.Description}' WHERE \"index\" = {index}";
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            _ = conn.Query(updateBackupQuery, new DynamicParameters());
        }
        #endregion

        private static bool ConnectionStringLoaded = false;
        private static string LoadConnectionString(string id = "Default")
        {
            string connectionString = id switch
            {
                "Default" => Properties.Settings.Default.ConnectionString,
                _ => Properties.Settings.Default.Properties[id].ToString() ?? Properties.Settings.Default.ConnectionString,
            };
            if (!ConnectionStringLoaded)
            {
                TraceLog($"Using connection string {id}:{connectionString}");
                ConnectionStringLoaded = true;
            }
            return connectionString;//ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static void ReIndexTables()
        {
            List<string> tables = new() { "Accounts", "Characters", "Guilds" };
            using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
            foreach (string tableName in tables)
            {
                string reindexQuery = $"Reindex {tableName}";
                _ = conn.Query(reindexQuery, new DynamicParameters());
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
                    string resetSequence = $"Update sqlite_sequence SET seq = 0 WHERE name = \"{tableName}\"";
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
                List<string> tables = new() { "Accounts", "Characters", "Guilds" };
                using IDbConnection conn = new SQLiteConnection(LoadConnectionString());
                foreach (string tableName in tables)
                {
                    string deleteQuery = $"Delete from {tableName}";
                    string resetSequence = $"Update sqlite_sequence SET seq = 0 WHERE name = \"{tableName}\"";
                    string reindexQuery = $"Reindex {tableName}";
                    _ = conn.Query(deleteQuery, new DynamicParameters());
                    _ = conn.Query(resetSequence, new DynamicParameters());
                    _ = conn.Query(reindexQuery, new DynamicParameters());
                }
            }
        }
    }
}
