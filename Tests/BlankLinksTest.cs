using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Sel_b10_hw
{
    [TestFixture]
    public class BlankLinksTest
    {
        private IWebDriver driver;

        [SetUp]
        public void CreateDriver()
        {
            driver = new ChromeDriver();
        }
        [TearDown]
        public void StopDriver()
        {
            driver.Quit();
        }
        [Test]
        public void ContriesLinks()
        {
            driver.Url = @"http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.Url = @"http://localhost/litecart/admin/?app=countries&doc=countries";

            driver.FindElement(By.ClassName("button")).Click();

            string mainTab = driver.CurrentWindowHandle;
            HashSet<string> htabs = new HashSet<string>();
            
            var links = driver.FindElements(By.CssSelector("form [target='_blank']"));
            foreach(var link in links)
            {
                link.Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                
                ICollection<string> tabs = driver.WindowHandles;
                foreach (var tab in tabs)
                {                    
                    htabs.Add(tab);
                }
                htabs.Remove(mainTab);
                
                if(htabs.Count<2)
                {
                    try
                    {
                        foreach (var htab in htabs)
                        {
                            driver.SwitchTo().Window(htab);
                            htabs.Clear();
                            driver.Close();
                            driver.SwitchTo().Window(mainTab);
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                else
                {
                    TestContext.WriteLine("С вкладками что-то пошло не так!");
                    break;
                }
            }
        }
    }
}
