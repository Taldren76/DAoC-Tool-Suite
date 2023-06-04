using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using DAoCToolSuite.ChimpTool.Json;
using DAoCToolSuite.ChimpTool.Logging;
using DAoCToolSuite.ChimpTool.Exception;
using Newtonsoft.Json;
using SQLLibrary.Enums;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Intrinsics.X86;
using System.Windows.Documents;

namespace DAoCToolSuite.ChimpTool.HeraldAPI
{
    public static class CamelotHeraldAPI
    {
        private static bool AttemptMaintenanceBypass { get; set; } = false;
        private static readonly Logger Logger = new();
        private const string searchBase = "https://api.camelotherald.com/character/search";
        private const string searchBase2 = "http://api.camelotherald.com/characters/search";
        private const string infoBase = "https://api.camelotherald.com/character/info";
        private const string infoBase2 = "http://api.camelotherald.com/character/info";
        public static CharacterInfoResult CharacterInfo(string webID)
        {
            if (webID == null)
            {
                return new CharacterInfoResult();
            }

            string urlParameters = $"";
            HttpClient client = new()
            {
                BaseAddress = new Uri(infoBase + $"/{webID}")
            };

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            CharacterInfoResult? result = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<CharacterInfoResult>(json);
                    if (result is null)
                    {
                        return new();
                    }

                    result.IsValid = true;
                }
                else
                {
                    Logger.Debug($"{(int)response.StatusCode} ({response.ReasonPhrase})");
                }
            }
            catch(AggregateException ex)
            {
                if (!AttemptMaintenanceBypass && ex?.InnerException?.Message is not null && ex.InnerException.Message.Equals("The SSL connection could not be established, see inner exception."))
                {
                    DialogResult del = MessageBox.Show($"Herald is in Maintenance. Attempt bypass?\nNote: Doing this may upset Broadsword.", "Maintenance", MessageBoxButtons.YesNo);
                    if (del == DialogResult.Yes)
                    {
                        AttemptMaintenanceBypass = true;
                        return CharacterInfo2(webID);
                    }
                    throw new MaintenanceException("Herald is in maintenance", ex);
                }
                else if(AttemptMaintenanceBypass)
                {
                    return CharacterInfo2(webID);
                }
            }
            catch (System.Exception e)
            {
                Logger.Error(e);
                return new();
            }

            client.Dispose();

            if (result is null)
                return new();

            result.IsValid = true;
            return result;
        }

        public static CharacterInfoResult CharacterInfo2(string webID)
        {
            if (webID == null)
            {
                return new CharacterInfoResult();
            }

            string urlParameters = $"";
            HttpClient client = new()
            {
                BaseAddress = new Uri(infoBase2 + $"/{webID}")
            };

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            CharacterInfoResult? result = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<CharacterInfoResult>(json);
                    if (result is null)
                    {
                        return new();
                    }

                    result.IsValid = true;
                }
                else
                {
                    Logger.Debug($"{(int)response.StatusCode} ({response.ReasonPhrase})");
                }
            }
            catch(MaintenanceException ex)
            {
                throw ex;
            }
            catch (System.Exception e)
            {
                Logger.Error(e);
                return new();
            }

            client.Dispose();
            result.IsValid = true;
            return result;
        }

        public static CharacterSearchResult CharacterSearch(string characterName, ServerCluster cluster)
        {
            string urlParameters = $"?name={characterName}&cluster={cluster}";
            HttpClient client = new()
            {
                BaseAddress = new Uri(searchBase)
            };

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            CharacterSearchResult? result = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<CharacterSearchResult>(json);
                    if (result?.Results is null || result.Results.Count == 0)
                    {
                        return new();
                    }

                    result.IsValid = true;
                }
                else
                {
                    Logger.Debug($"{(int)response.StatusCode} ({response.ReasonPhrase})");
                }
            }
            catch (AggregateException ex)
            {
                if (!AttemptMaintenanceBypass && ex?.InnerException?.Message is not null && ex.InnerException.Message.Equals("The SSL connection could not be established, see inner exception."))
                {
                    DialogResult del = MessageBox.Show($"Herald is in Maintenance. Attempt bypass?\nNote: Doing this may upset Broadsword.", "Maintenance", MessageBoxButtons.YesNo);
                    if (del == DialogResult.Yes)
                    {
                        AttemptMaintenanceBypass = true;
                        return CharacterSearch2(characterName,cluster);
                    }
                    throw new MaintenanceException("Herald is in maintenance", ex);
                }
                else if (AttemptMaintenanceBypass)
                {
                    return CharacterSearch2(characterName,cluster);
                }
            }
            catch (System.Exception e)
            {
                Logger.Error(e);
                return new();
            }

            client.Dispose();

            if (result is null)
                return new();

            result.IsValid = true;
            return result;
        }

        public static CharacterSearchResult CharacterSearch2(string characterName, ServerCluster cluster)
        {
            string urlParameters = $"?name={characterName}&cluster={cluster}";
            HttpClient client = new()
            {
                BaseAddress = new Uri(searchBase2)
            };

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            CharacterSearchResult? result = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<CharacterSearchResult>(json);
                    if (result?.Results is null || result.Results.Count == 0)
                    {
                        return new();
                    }

                    result.IsValid = true;
                }
                else
                {
                    Logger.Debug($"{(int)response.StatusCode} ({response.ReasonPhrase})");
                }
            }
            catch(MaintenanceException ex)
            {
                throw ex;
            }
            catch (System.Exception e)
            {
                Logger.Error(e);
                return new();
            }

            client.Dispose();
            result.IsValid = true;
            return result;
        }

        public static ChimpJson GetChimp(string name, ServerCluster cluster)
        {
            CharacterSearchResult? searchResult = CharacterSearch(name, cluster);
            if (searchResult is null || searchResult.Results is null)
            {
                return new(true);
            }

            try
            {
                string? webid = searchResult?.Results[0]?.CharacterWebId;
                return webid is null ? new() : GetChimp(webid);
            }
            catch(MaintenanceException ex)
            {
                throw ex;
            }
            catch (System.Exception e)
            {
                Logger.Error(e);
                return new(true);
            }
        }
        public static ChimpJson GetChimp(string webId)
        {
            CharacterInfoResult result = CharacterInfo(webId);
            try
            {
                if (result?.Realm is not null && result.IsValid)
                {
                    int deaths = result.RealmWarStats?.Current?.PlayerKills?.Total?.Deaths ?? 0;
                    int rps = result.RealmWarStats?.Current?.RealmPoints ?? 0;
                    int irs = deaths > 0 ? rps / deaths : rps;
                    int realm = result.Realm - 1;

                    ChimpJson chimp = new()
                    {
                        WebID = result?.CharacterWebId,
                        Name = result?.Name,
                        GuildName = result?.GuildInfo?.GuildName ?? "",
                        Guild_WebID = result?.GuildInfo?.GuildWebId ?? "",
                        Level = result?.Level.ToString(),
                        Race = result?.Race,
                        Class = result?.ClassName,
                        Server = result?.ServerName,
                        Realm = new List<string>() { "Albion", "Midgard", "Hibernia", "NewJersey" }[realm],

                        MasterLevel_Level = result?.MasterLevel?.Level.ToString("N0") ?? "0",
                        MasterLevel_Name = result?.MasterLevel?.Path ?? "",

                        BountyPoints = result?.RealmWarStats?.Current?.BountyPoints.ToString("N0") ?? "0",

                        Alchemy = result?.Crafting?.Alchemy.ToString("N0") ?? "0",
                        Armorcraft = result?.Crafting?.Armorcraft.ToString("N0") ?? "0",
                        Fletching = result?.Crafting?.Fletching.ToString("N0") ?? "0",
                        Siegecraft = result?.Crafting?.Siegecraft.ToString("N0") ?? "0",
                        Spellcrafting = result?.Crafting?.Spellcraft.ToString("N0") ?? "0",
                        Tailoring = result?.Crafting?.Tailoring.ToString("N0") ?? "0",
                        Weaponcraft = result?.Crafting?.Weaponcraft.ToString("N0") ?? "0",

                        TotalKills = result?.RealmWarStats?.Current?.PlayerKills?.Total?.Kills.ToString("N0") ?? "0",
                        TotalSoloKills = result?.RealmWarStats?.Current?.PlayerKills?.Total?.SoloKills.ToString("N0") ?? "0",
                        TotalDeathBlows = result?.RealmWarStats?.Current?.PlayerKills?.Total?.DeathBlows.ToString("N0") ?? "0",
                        TotalDeaths = deaths.ToString("N0") ?? "0",
                        Albion_SoloKills = result?.RealmWarStats?.Current?.PlayerKills?.Albion?.SoloKills.ToString("N0") ?? "0",
                        Albion_DeathBlows = result?.RealmWarStats?.Current?.PlayerKills?.Albion?.DeathBlows.ToString("N0") ?? "0",
                        Albion_Kills = result?.RealmWarStats?.Current?.PlayerKills?.Albion?.Kills.ToString("N0") ?? "0",
                        Albion_Deaths = result?.RealmWarStats?.Current?.PlayerKills?.Albion?.Deaths.ToString("N0") ?? "0",
                        Hibernia_SoloKills = result?.RealmWarStats?.Current?.PlayerKills?.Hibernia?.SoloKills.ToString("N0") ?? "0",
                        Hibernia_DeathBlows = result?.RealmWarStats?.Current?.PlayerKills?.Hibernia?.DeathBlows.ToString("N0") ?? "0",
                        Hibernia_Kills = result?.RealmWarStats?.Current?.PlayerKills?.Hibernia?.Kills.ToString("N0") ?? "0",
                        Hibernia_Deaths = result?.RealmWarStats?.Current?.PlayerKills?.Hibernia?.Deaths.ToString("N0") ?? "0",
                        Midgard_SoloKills = result?.RealmWarStats?.Current?.PlayerKills?.Midgard?.SoloKills.ToString("N0") ?? "0",
                        Midgard_DeathBlows = result?.RealmWarStats?.Current?.PlayerKills?.Midgard?.DeathBlows.ToString("N0") ?? "0",
                        Midgard_Kills = result?.RealmWarStats?.Current?.PlayerKills?.Midgard?.Kills.ToString("N0") ?? "0",
                        Midgard_Deaths = result?.RealmWarStats?.Current?.PlayerKills?.Midgard?.Deaths.ToString("N0") ?? "0",
                        TotalRealmPoints = rps.ToString("N0") ?? "0",
                        IRS = irs.ToString("N0") ?? "0",

                        RealmRank = CalculateRealmRank(rps).ToString("0.0").Replace('.', 'L')
                    };
                    return chimp;
                }
                return new ChimpJson();
            }
            catch(MaintenanceException ex)
            {
                throw ex;
            }
            catch (System.Exception e)
            {
                Logger.Error(e);
                return new ChimpJson();
            }

        }
        public static List<ChimpJson> GetChimps(List<ChimpJson> chimps, TextProgressBar progressBar)
        {

            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Maximum = chimps.Count;
            progressBar.Minimum = 0;
            progressBar.CustomText = "Retrieving Character Data";
            progressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
            progressBar.Refresh();

            List<ChimpJson> results = new();
            foreach (ChimpJson chimp in chimps)
            {
                #region ProgressBar
                progressBar.Value += 1;
                progressBar.Update();
                #endregion

                if (chimp?.WebID is null)
                {
                    continue;
                }

                string webID = chimp.WebID;

                CharacterInfoResult infoResult = CharacterInfo(webID);
                try
                {
                    if (infoResult?.Realm is not null && infoResult.IsValid)
                    {
                        int deaths = infoResult.RealmWarStats?.Current?.PlayerKills?.Total?.Deaths ?? 0;
                        int rps = infoResult.RealmWarStats?.Current?.RealmPoints ?? 0;
                        int irs = deaths > 0 ? rps / deaths : rps;
                        int realm = infoResult.Realm - 1;

                        ChimpJson result = new()
                        {
                            WebID = infoResult?.CharacterWebId,
                            Name = infoResult?.Name,
                            GuildName = infoResult?.GuildInfo?.GuildName ?? "",
                            Guild_WebID = infoResult?.GuildInfo?.GuildWebId ?? "",
                            Level = infoResult?.Level.ToString(),
                            Race = infoResult?.Race,
                            Class = infoResult?.ClassName,
                            Server = infoResult?.ServerName,
                            Realm = new List<string>() { "Albion", "Midgard", "Hibernia", "NewJersey" }[realm],

                            MasterLevel_Level = infoResult?.MasterLevel?.Level.ToString("N0") ?? "0",
                            MasterLevel_Name = infoResult?.MasterLevel?.Path ?? "",

                            BountyPoints = infoResult?.RealmWarStats?.Current?.BountyPoints.ToString("N0") ?? "0",

                            Alchemy = infoResult?.Crafting?.Alchemy.ToString("N0") ?? "0",
                            Armorcraft = infoResult?.Crafting?.Armorcraft.ToString("N0") ?? "0",
                            Fletching = infoResult?.Crafting?.Fletching.ToString("N0") ?? "0",
                            Siegecraft = infoResult?.Crafting?.Siegecraft.ToString("N0") ?? "0",
                            Spellcrafting = infoResult?.Crafting?.Spellcraft.ToString("N0") ?? "0",
                            Tailoring = infoResult?.Crafting?.Tailoring.ToString("N0") ?? "0",
                            Weaponcraft = infoResult?.Crafting?.Weaponcraft.ToString("N0") ?? "0",

                            TotalKills = infoResult?.RealmWarStats?.Current?.PlayerKills?.Total?.Kills.ToString("N0") ?? "0",
                            TotalSoloKills = infoResult?.RealmWarStats?.Current?.PlayerKills?.Total?.SoloKills.ToString("N0") ?? "0",
                            TotalDeathBlows = infoResult?.RealmWarStats?.Current?.PlayerKills?.Total?.DeathBlows.ToString("N0") ?? "0",
                            TotalDeaths = deaths.ToString("N0") ?? "0",
                            Albion_SoloKills = infoResult?.RealmWarStats?.Current?.PlayerKills?.Albion?.SoloKills.ToString("N0") ?? "0",
                            Albion_DeathBlows = infoResult?.RealmWarStats?.Current?.PlayerKills?.Albion?.DeathBlows.ToString("N0") ?? "0",
                            Albion_Kills = infoResult?.RealmWarStats?.Current?.PlayerKills?.Albion?.Kills.ToString("N0") ?? "0",
                            Albion_Deaths = infoResult?.RealmWarStats?.Current?.PlayerKills?.Albion?.Deaths.ToString("N0") ?? "0",
                            Hibernia_SoloKills = infoResult?.RealmWarStats?.Current?.PlayerKills?.Hibernia?.SoloKills.ToString("N0") ?? "0",
                            Hibernia_DeathBlows = infoResult?.RealmWarStats?.Current?.PlayerKills?.Hibernia?.DeathBlows.ToString("N0") ?? "0",
                            Hibernia_Kills = infoResult?.RealmWarStats?.Current?.PlayerKills?.Hibernia?.Kills.ToString("N0") ?? "0",
                            Hibernia_Deaths = infoResult?.RealmWarStats?.Current?.PlayerKills?.Hibernia?.Deaths.ToString("N0") ?? "0",
                            Midgard_SoloKills = infoResult?.RealmWarStats?.Current?.PlayerKills?.Midgard?.SoloKills.ToString("N0") ?? "0",
                            Midgard_DeathBlows = infoResult?.RealmWarStats?.Current?.PlayerKills?.Midgard?.DeathBlows.ToString("N0") ?? "0",
                            Midgard_Kills = infoResult?.RealmWarStats?.Current?.PlayerKills?.Midgard?.Kills.ToString("N0") ?? "0",
                            Midgard_Deaths = infoResult?.RealmWarStats?.Current?.PlayerKills?.Midgard?.Deaths.ToString("N0") ?? "0",
                            TotalRealmPoints = rps.ToString("N0") ?? "0",
                            IRS = irs.ToString("N0") ?? "0",

                            RealmRank = CalculateRealmRank(rps).ToString("0.0").Replace('.', 'L')
                        };
                        results.Add(result);
                    }
                    else
                    {
                        Logger.Debug($"Invalid return for WebID:{webID}");
                        results.Add(new() { WebID = webID });
                    }
                    continue;
                }
                catch (System.Exception e)
                {
                    Logger.Error(e);
                    continue;
                }
            }
            progressBar.Visible = false;
            progressBar.CustomText = "";
            progressBar.VisualMode = ProgressBarDisplayMode.Percentage;
            progressBar.Refresh();
            return results;
        }

        public static double CalculateRealmRank(int realmPoints)
        {
            Dictionary<int, double> realmRanks = new();
            for (int rr = 0; rr < 100; rr++)
            {
                realmRanks.Add(rr + 11, ((50 * Math.Pow(rr, 3)) + (75 * Math.Pow(rr, 2)) + (25 * rr)) / 6);
            }
            realmRanks.Add(111, 9111713);
            realmRanks.Add(112, 10114001);
            realmRanks.Add(113, 11226541);
            realmRanks.Add(114, 12461460);
            realmRanks.Add(115, 13832221);
            realmRanks.Add(116, 15353765);
            realmRanks.Add(117, 17042680);
            realmRanks.Add(118, 18917374);
            realmRanks.Add(119, 20998286);
            realmRanks.Add(120, 23308097);
            realmRanks.Add(121, 25871988);
            realmRanks.Add(122, 28717906);
            realmRanks.Add(123, 31876876);
            realmRanks.Add(124, 35383333);
            realmRanks.Add(125, 39275499);
            realmRanks.Add(126, 43595804);
            realmRanks.Add(127, 48391343);
            realmRanks.Add(128, 53714390);
            realmRanks.Add(129, 59622973);
            realmRanks.Add(130, 66181501);
            realmRanks.Add(131, 73461466);
            realmRanks.Add(132, 81542227);
            realmRanks.Add(133, 90511872);
            realmRanks.Add(134, 100468178);
            realmRanks.Add(135, 111519678);
            realmRanks.Add(136, 123786843);
            realmRanks.Add(137, 137403395);
            realmRanks.Add(138, 152517769);
            realmRanks.Add(139, 169294723);
            realmRanks.Add(140, 187917143);

            try
            {
                IEnumerable<KeyValuePair<int, double>> test = realmRanks.Where(x => x.Value <= realmPoints);
                double test2 = test.Select(x => x.Key).Last() / 10.0;
                return test2;
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
                return -1.0;
            }

        }

        //public static GuildInfo GetGuildInfo(string webID)
        //{

        //}
    }


}
