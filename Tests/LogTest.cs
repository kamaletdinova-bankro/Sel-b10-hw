using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Sel_b10_hw
{
    [TestFixture]
    public class LogTest
    {
        private RemoteWebDriver driver;

        [SetUp]
        public void CreateDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            var service = ChromeDriverService.CreateDefaultService();
            service.EnableVerboseLogging = true;
            driver = new ChromeDriver(service, options);
        }
        [TearDown]
        public void StopDriver()
        {
            driver.Quit();
        }
        [Test]
        public void CatalogLog()
        {
            driver.Url = @"http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.Url = @"http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1/";

            var folders = driver.FindElements(By.CssSelector("td>a[href*='doc=catalog']"));
            var foldersLinks = GetLinksList(folders);            
            var log = driver.Manage().Logs.GetLog(LogType.Browser);

            foreach (var folderLink in foldersLinks)
            {
                driver.Url = folderLink;
                folders = driver.FindElements(By.CssSelector("td>a[href*='doc=catalog']"));
                foldersLinks = GetLinksList(folders);

                var products = driver.FindElements(By.CssSelector("td>a[href*=product]"));
                if (products.Count > 0)
                {
                    for(int i=0; i < products.Count; i++)
                    {
                        products[i].Click();
                        Assert.That(log.Count == 0);
                        driver.FindElement(By.CssSelector("button[name=cancel]")).Click();
                        products = driver.FindElements(By.CssSelector("td>a[href*=product]"));
                    }
                }
            } 
        }

        private HashSet<string> GetLinksList(ReadOnlyCollection<IWebElement> elements)
        {
            var links = new HashSet<string>();
            foreach(var element in elements)
            {
                links.Add(element.GetAttribute("href"));
            }
            return links;
        }
    }
}