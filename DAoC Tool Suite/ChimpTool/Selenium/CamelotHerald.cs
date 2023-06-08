using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using CefSharp;
using CefSharp.OffScreen;
using DAoCToolSuite.ChimpTool.Extensions;
using DAoCToolSuite.ChimpTool.Json;
using Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SQLLibrary.Enums;

namespace DAoCToolSuite.ChimpTool.Selenium
{
    internal static class CamelotHerald
    {

        internal static Logger.LogManager Logger => LogManager.Instance;
        internal static ChromiumWebBrowser? Browser { get; set; }
        internal static bool IsInitialized { get; set; } = false;
        internal static string? ExePath = Path.GetDirectoryName(Application.ExecutablePath);
        internal static string AssemblyPath => ExePath ?? throw new NullReferenceException("The path to the assembly can not be determined and returned null");
        public static void Initialize()
        {
            Stopwatch stopwatch = new();
            try
            {
                #region Cef Settings
                if (!IsInitialized)
                {
                    CefSettings settings = new()
                    {

                        RemoteDebuggingPort = 9222,
                        UserDataPath = AssemblyPath + "\\Temp",
                        WindowlessRenderingEnabled = true
                    };
                    if (Cef.Initialize(settings))
                    {
                        IsInitialized = true;
                    }
                    else
                    {
                        Logger.Error("Failed to initialize Cef");
                    }
                }
                #endregion
                #region Browser
                DateTime endTime;
                if (!(Browser?.IsBrowserInitialized ?? false))
                {
                    Logger.Debug($"Assembly path has been set to {AssemblyPath}");
                    stopwatch.Restart();
                    Browser = new ChromiumWebBrowser("data:,");
                    endTime = DateTime.Now.AddSeconds(10);
                    while (Browser.IsLoading || !Browser.IsBrowserInitialized || DateTime.Now > endTime)
                    {
                        Thread.Sleep(100);
                    }
                    Thread.Sleep(100);
                    Logger.Debug($"CEFSharp Browser.IsBrowserInitialized = {Browser?.IsBrowserInitialized.ToString() ?? "null"} after {stopwatch.Elapsed:c}");
                }
                stopwatch.Restart();
                #endregion
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
            }
        }
        public static void Quit()
        {
            try
            {
                Logger.Debug($"Disposing CEFSharp Browser");
                Browser?.Dispose();
                Browser = null;
            }
            catch { }
        }
        internal static ChimpJson GetChimp(string playerName, ServerCluster cluster, int secondsToTry = 3)
        {
            if (!IsInitialized || Browser == null || !Browser.IsBrowserInitialized)
            {
                Initialize();
            }

            ChromeOptions options = new()
            {
                DebuggerAddress = "127.0.0.1:9222",
                AcceptInsecureCertificates = true
            };
            //options.AddArgument("--remote-debugging-port=9222");
            //options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-breakpad");
            Logger.Debug($"Initializing selenium chromedriver to search for {playerName} on {cluster}");

            using ChromeDriverService chromeDrvService = ChromeDriverService.CreateDefaultService(AssemblyPath + "\\Selenium");
            chromeDrvService.HideCommandPromptWindow = true;
            using ChromeDriver driver = new(chromeDrvService, options);
            try
            {
                string url = $"https://search.camelotherald.com/#/search/c/{cluster}/{playerName}";

                Logger.Debug($"Navigating to {url}");
                driver.Navigate().GoToUrl(new Uri(url));
                Logger.Debug($"Navigation complete. Url={driver.Url}");
                IWebElement? characterNameLink = null; // driver.GetIfExists(By.XPath("//*[@id='main-group-inner']/div[2]/div[2]/div/table/tbody/tr[2]/td[2]/a"));

                if (driver.GetIfExists(By.XPath("//*[@id='main-group-inner']/div[2]/div[2]/div/table/tbody/tr[2]/td[2]/a"), secondsToTry * 1000, 100) is null) //Delays until a result exists in the table;
                {
                    Logger.Debug($"Could not find {"//*[@id='main-group-inner']/div[2]/div[2]/div/table/tbody/tr[2]/td[2]/a"} in\n{driver.PageSource}");
                    return new(true);
                }

                ReadOnlyCollection<IWebElement> playerSearchResultsTable = driver.FindElements(By.XPath("//*[@id='main-group-inner']/div[2]/div[2]/div/table/tbody//tr//td//a"));
                foreach (IWebElement cell in playerSearchResultsTable)
                {
                    string? cellText = cell?.Text?.Split(' ')?.FirstOrDefault()?.Trim().ToLower();


                    if (cellText != null && cellText.Equals(playerName.ToLower()))
                    {
                        characterNameLink = cell;
                        break;
                    }
                }

                if (characterNameLink is null)
                {
                    return new(true);
                }

                characterNameLink?.Click();



                return driver.ScrapeCharacterInfo();
            }
            catch
            {
                return new(true);
            }
        }
        internal static ChimpJson GetChimp(ChimpJson player)
        {
            if (!IsInitialized || Browser == null || !Browser.IsBrowserInitialized)
            {
                Initialize();
            }

            ChromeOptions options = new()
            {
                DebuggerAddress = "127.0.0.1:9222",
                AcceptInsecureCertificates = true
            };
            //options.AddArgument("--remote-debugging-port=9222");
            //options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-breakpad");
            using ChromeDriverService chromeDrvService = ChromeDriverService.CreateDefaultService(AssemblyPath + "\\Selenium");
            chromeDrvService.HideCommandPromptWindow = true;
            using ChromeDriver driver = new(chromeDrvService, options);
            try
            {
                string url = $"https://search.camelotherald.com/#/character/{player.WebID}";
                Logger.Debug($"Navigating to {url}");
                driver.Navigate().GoToUrl(new Uri(url));
                Logger.Debug($"Navigation complete. Url={driver.Url}");
                return driver.ScrapeCharacterInfo();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ChimpJson(true);
            }
        }
        internal static List<ChimpJson> GetChimps(List<string> playerNames, ServerCluster cluster, TextProgressBar progressBar, int secondsToTry = 5)
        {
            if (!IsInitialized || Browser == null || !Browser.IsBrowserInitialized)
            {
                Initialize();
            }

            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Maximum = playerNames.Count;
            progressBar.Minimum = 0;
            progressBar.CustomText = "Retrieving Character Data";
            progressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
            progressBar.Refresh();

            List<ChimpJson> results = new();
            ChromeOptions options = new()
            {
                DebuggerAddress = "127.0.0.1:9222",
                AcceptInsecureCertificates = true
            };
            //options.AddArgument("--remote-debugging-port=9222");
            //options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-breakpad");
            using ChromeDriverService chromeDrvService = ChromeDriverService.CreateDefaultService(AssemblyPath + "\\Selenium");
            chromeDrvService.HideCommandPromptWindow = true;
            using (ChromeDriver driver = new(chromeDrvService, options))
            {
                foreach (string playerName in playerNames)
                {
                    #region ProgressBar
                    progressBar.Value += 1;
                    progressBar.Update();
                    #endregion

                    try
                    {
                        string url = $"https://search.camelotherald.com/#/search/c/{cluster}/{playerName}";
                        Logger.Debug($"Navigating to {url}");
                        driver.Navigate().GoToUrl(new Uri(url));
                        Logger.Debug($"Navigation complete. Url={driver.Url}");
                        WebDriverWait wait = new(driver, timeout: TimeSpan.FromSeconds(secondsToTry))
                        {
                            PollingInterval = TimeSpan.FromMilliseconds(100),
                        };
                        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                        IWebElement characterNameLink = wait.Until(drv => drv.FindElement(By.XPath("//*[@id='main-group-inner']/div[2]/div[2]/div/table/tbody/tr[2]/td[2]/a")));
                        characterNameLink.Click();
                        ChimpJson result = driver.ScrapeCharacterInfo();
                        results.Add(result);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
            }

            progressBar.Visible = false;
            progressBar.CustomText = "";
            progressBar.VisualMode = ProgressBarDisplayMode.Percentage;
            progressBar.Refresh();
            return results;
        }

        internal static List<ChimpJson> GetChimps(List<ChimpJson> chimps, TextProgressBar progressBar)
        {
            if (!IsInitialized || Browser == null || !Browser.IsBrowserInitialized)
            {
                Initialize();
            }

            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Maximum = chimps.Count;
            progressBar.Minimum = 0;
            progressBar.CustomText = "Retrieving Character Data";
            progressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
            progressBar.Refresh();

            List<ChimpJson> results = new();
            ChromeOptions options = new()
            {
                DebuggerAddress = "127.0.0.1:9222",
                AcceptInsecureCertificates = true
            };
            //options.AddArgument("--remote-debugging-port=9222");
            //options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-breakpad");
            using ChromeDriverService chromeDrvService = ChromeDriverService.CreateDefaultService(AssemblyPath + "\\Selenium");
            chromeDrvService.HideCommandPromptWindow = true;
            using (ChromeDriver driver = new(chromeDrvService, options))
            {
                Stopwatch resultWatch = new();
                foreach (ChimpJson chimp in chimps)
                {
                    resultWatch.Restart();
                    #region ProgressBar
                    progressBar.Value += 1;
                    progressBar.Update();
                    #endregion
                    Logger.Debug($"Progressbar updated in {resultWatch.Elapsed:c}");
                    if (chimp?.WebID is null)
                    {
                        continue;
                    }

                    string webID = chimp.WebID;
                    try
                    {
                        string url = $"https://search.camelotherald.com/#/character/{webID}";
                        Logger.Debug($"Navigating to {url}");
                        driver.Navigate().GoToUrl(new Uri(url));
                        Logger.Debug($"Navigation to {driver.Url} complete in {resultWatch.Elapsed:c}.");
                        ChimpJson result = driver.ScrapeCharacterInfo();
                        Logger.Debug($"Result returned in {resultWatch.Elapsed:c}");
                        results.Add(result);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Error(ex);
                        Logger.Debug($"Exception after {resultWatch.Elapsed:c}");
                        results.Add(new() { WebID = webID });
                    }
                }
            }
            progressBar.Visible = false;
            progressBar.CustomText = "";
            progressBar.VisualMode = ProgressBarDisplayMode.Percentage;
            progressBar.Refresh();
            return results;
        }

        internal static ChimpJson ScrapeCharacterInfo(this IWebDriver driver)
        {
            ChimpJson chimp = new();
            try
            {
                chimp.WebID = driver.Url.Split('/').Last();

                WebDriverWait wait = new(driver, timeout: TimeSpan.FromSeconds(10))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(100),
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

                //Pause until webpage is loaded and name is displayed.
                bool hasName(IWebDriver d)
                {
                    try
                    {
                        string? _name = d.FindElement(By.XPath("//*[@id='char-info']/div[2]/h2"))?.Text;
                        return !string.IsNullOrEmpty(_name);
                    }
                    catch (NotFoundException)
                    {
                        return false;
                    }
                };
                _ = wait.Until(hasName);

                string masterPath(IWebDriver d)
                {
                    try
                    {
                        ////*[@id="char-info"]/div[4]/p[1]/span
                        IWebElement? element = d.GetIfExists(By.XPath("//*[@id='char-info']/div[4]/p[1]/span"), 1000, 100);
                        return element?.Text ?? "";
                    }
                    catch
                    {
                        return "";
                    }
                };

                Regex mp = new(@"(\d+)\s-\s\((\w+)\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match? matches = mp.Match(masterPath(driver));
                string strMasterPathLevel = matches?.Groups[1]?.Value ?? "0";
                string strMasterPathName = matches?.Groups[2]?.Value ?? "";
                int intMasterPathLevel = int.TryParse(strMasterPathLevel, out intMasterPathLevel) ? intMasterPathLevel : 0;

                Stopwatch stopwatch = new();
                stopwatch.Start();
                DateTime endTime = DateTime.Now.AddSeconds(5);
                List<string> strTableElements = new();
                Match? rx = null;
                while (DateTime.Now < endTime)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> tableElements = driver.FindElements(By.XPath("//div[@id='char-info']//*[@class='ng-binding']"));
                    strTableElements = tableElements.Select(x => x.Text).ToList();
                    string pattern = @"Level\s*(\d+)\s*(\w+)\s*(\w+)";
                    rx = Regex.Match(strTableElements[1], pattern);
                    if (!rx.Success)
                    {
                        continue;
                    }

                    chimp.Name = strTableElements[0].ToString().ToTitleCase();
                    chimp.Level = rx.Groups[1].ToString();
                    chimp.Race = rx.Groups[2].ToString();
                    chimp.Class = rx.Groups[3].ToString();
                    chimp.RealmRank = strTableElements[3].ToString().Replace("Realm Rank ", "");
                    chimp.GuildName = strTableElements.Count > 8 ? strTableElements[4].ToString() : "";
                    chimp.Server = strTableElements.Count > 8 ? strTableElements[6].ToString().Split('-').Last().Replace(" ", "") : strTableElements[5].ToString().Split('-').Last().Replace(" ", "");
                    chimp.Realm = strTableElements.Count > 8 ? strTableElements[7].ToString() : strTableElements[6].ToString();
                    chimp.MasterLevel_Name = strMasterPathName;
                    chimp.MasterLevel_Level = intMasterPathLevel.ToString("N0") ?? "0";

                    // Table XPath: //table[1]//tr//td
                    /* ==== REALM VS REALM STATS ===
                     * [00] BLANK
                     * [01] PvP Deaths
                     * [02] PvP Kills
                     * [03] Solo Kills
                     * [04] Death Blows
                     * [05] Realm Points
                     * [06] IRS
                     * [07] Lifetime
                     * [08] <PVP Deaths> 
                     * [09] <PvP Kills> 
                     * [10] <Solo Kills> 
                     * [11] <Death Blows>
                     * [12] <Realm Points>
                     * [13] <IRS>
                     * [14]     <REALM1NAME>
                     * [15] <PvP Kills>
                     * [16] <Solo Kills>
                     * [17] <Death Blows>
                     * [18]     <REALM2NAME>
                     * [19] <PvP Kills>
                     * [20] <Solo Kills>
                     * [21] <Death Blows>
                     * [22] BLANK
                     * [23] BLANK
                     * [24] BLANK
                     */

                    ReadOnlyCollection<IWebElement> RvRStatsCells = driver.FindElements(By.XPath("//table[1]//tr//td"));
                    List<string> rvrStatsValues = new();
                    foreach (IWebElement cell in RvRStatsCells)
                    {
                        if (cell != null)
                        {
                            rvrStatsValues.Add(cell.Text);
                        }
                    }

                    chimp.TotalDeaths = rvrStatsValues[8];
                    chimp.TotalKills = rvrStatsValues[9];
                    chimp.TotalSoloKills = rvrStatsValues[10];
                    chimp.TotalDeathBlows = rvrStatsValues[11];
                    chimp.TotalRealmPoints = rvrStatsValues[12];
                    chimp.IRS = rvrStatsValues[13];
                    string realm1 = rvrStatsValues[14].Replace(" ", "").ToLower();
                    switch (realm1)
                    {
                        case "albion":
                            chimp.Albion_Kills = rvrStatsValues[15];
                            chimp.Albion_SoloKills = rvrStatsValues[16];
                            chimp.Albion_DeathBlows = rvrStatsValues[17];
                            break;
                        case "midgard":
                            chimp.Midgard_Kills = rvrStatsValues[15];
                            chimp.Midgard_SoloKills = rvrStatsValues[16];
                            chimp.Midgard_DeathBlows = rvrStatsValues[17];
                            break;
                        case "hibernia":
                            chimp.Hibernia_Kills = rvrStatsValues[15];
                            chimp.Hibernia_SoloKills = rvrStatsValues[16];
                            chimp.Hibernia_DeathBlows = rvrStatsValues[17];
                            break;
                    }
                    string realm2 = rvrStatsValues[18].Replace(" ", "").ToLower();
                    switch (realm2)
                    {
                        case "albion":
                            chimp.Albion_Kills = rvrStatsValues[19];
                            chimp.Albion_SoloKills = rvrStatsValues[20];
                            chimp.Albion_DeathBlows = rvrStatsValues[21];
                            break;
                        case "midgard":
                            chimp.Midgard_Kills = rvrStatsValues[19];
                            chimp.Midgard_SoloKills = rvrStatsValues[20];
                            chimp.Midgard_DeathBlows = rvrStatsValues[21];
                            break;
                        case "hibernia":
                            chimp.Hibernia_Kills = rvrStatsValues[19];
                            chimp.Hibernia_SoloKills = rvrStatsValues[20];
                            chimp.Hibernia_DeathBlows = rvrStatsValues[21];
                            break;
                    }

                    //Table XPath: //table[2]//tr//td
                    /* === TRADESKILL INFO ===
                     * [00] Spellcrafting
                     * [01] Alchemy
                     * [02] Armorcraft
                     * [03] Fletching
                     * [04] Tailoring
                     * [05] Weaponcraft
                     * [06] Siegecraft
                     * [07] <Spellcrafting>
                     * [08] <Alchemy>
                     * [09] <Armorcraft>
                     * [10] <Fletching>
                     * [11] <Tailoring>
                     * [12] <Weaponcraft>
                     * [13] <Siegecraft>
                     */

                    ReadOnlyCollection<IWebElement> TradeSkillInfoCells = driver.FindElements(By.XPath("//table[2]//tr//td"));
                    List<string> tradeSkillInfoValues = new();
                    foreach (IWebElement cell in TradeSkillInfoCells)
                    {
                        if (cell != null)
                        {
                            tradeSkillInfoValues.Add(cell.Text);
                        }
                    }

                    chimp.Spellcrafting = tradeSkillInfoValues[7];
                    chimp.Alchemy = tradeSkillInfoValues[8];
                    chimp.Armorcraft = tradeSkillInfoValues[9];
                    chimp.Fletching = tradeSkillInfoValues[10];
                    chimp.Tailoring = tradeSkillInfoValues[11];
                    chimp.Weaponcraft = tradeSkillInfoValues[12];
                    chimp.Siegecraft = tradeSkillInfoValues[13];

                    if (chimp.IsValid())
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
                Logger.Debug($"Took {stopwatch.Elapsed:c} to scape data for {chimp.Name}");
                return chimp;
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex);
                return new ChimpJson(true);
            }
        }
    }
}
