using Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace DAoCToolSuite.ChimpTool.Extensions
{
    internal static class SeleniumExtensions
    {
        private static LogManager Logger => LogManager.Instance;
        internal static IWebElement? GetIfExists(this IWebDriver driver, By strategy, int timeout = 3000, int frequency = 100)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Logger.Debug($"Searching for Element {strategy}");
            try
            {
                WebDriverWait wait = new(driver, TimeSpan.FromMilliseconds(timeout))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(frequency)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

                return wait.Until(d =>
                {
                    IWebElement element = d.FindElement(strategy);
                    Logger.Debug($"Element found in {stopwatch.Elapsed:c}");
                    return element;
                });
            }
            catch
            {
                Logger.Debug($"Element was not found after {stopwatch.Elapsed:c}");
                return null;
            }
        }
    }
}

