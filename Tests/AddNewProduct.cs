using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Sel_b10_hw
{
    [TestFixture]
    public class AddNewProduct
    {
        private IWebDriver driver;
        [SetUp]
        public void CreateDriver()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        [TearDown]
        public void StopDriver()
        {
            driver.Quit();
        }
        [Test]
        public void AddProduct()
        {
            driver.Url = @"http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.Url = @"http://localhost/litecart/admin/?app=catalog&doc=catalog";

            driver.FindElement(By.CssSelector(".button:last-child")).Click();

            var tabs = driver.FindElements(By.CssSelector(".index li"));
            Dictionary<string, int> tabsD = new Dictionary<string, int>();
            for (int i = 0; i < tabs.Count; i++)
            {
                tabsD.Add(tabs[i].Text, i);
            }
            var name = Guid.NewGuid();

            tabs[tabsD["General"]].Click();
            var general = driver.FindElement(By.Id("tab-general"));
            var statuses = general.FindElements(By.TagName("label"));
            foreach (var status in statuses)
            {
                if (status.Text == "Enabled")
                {
                    status.FindElement(By.TagName("input")).Click();
                }
            }
            var nameAndCode = general.FindElements(By.CssSelector("input[type=text]"));
            foreach (var item in nameAndCode)
            {
                if (item.GetAttribute("name") == "code")
                {
                    item.SendKeys("En");
                }
                else
                {
                    item.SendKeys(name.ToString());
                }
            }
            var category = general.FindElement(By.CssSelector("input[data-name='Rubber Ducks']"));           
            category.Click();
            var groups = general.FindElements(By.CssSelector("input[name*=product]"));
            if (groups.Count > 0)
            {
                groups[0].Click();
            }
            general.FindElement(By.CssSelector("input[name=quantity]")).SendKeys("20");
            general.FindElement(By.CssSelector("input[type=file]")).SendKeys(Path.GetFullPath(@"Sel-b10-hw\TestData\test-duck.png"));
            var validationDates = general.FindElements(By.CssSelector("input[type=date]"));
            if (validationDates.Count > 0)
            {
                foreach (var validationDate in validationDates)
                {
                    if (validationDate.GetAttribute("name") == "date_valid_from")
                    {
                        validationDate.SendKeys(DateTime.Today.Date.ToString());
                    }
                    else if (validationDate.GetAttribute("name") == "date_valid_to")
                    {
                        validationDate.SendKeys(DateTime.Today.Date.AddYears(1).ToString());
                    }
                }
            }

            tabs = driver.FindElements(By.CssSelector(".index li"));
            tabs[tabsD["Information"]].Click();
            var information = driver.FindElement(By.Id("tab-information"));
            var manufacturer = new SelectElement(information.FindElement(By.CssSelector("select[name=manufacturer_id]")));
            manufacturer.SelectByValue("1");
            information.FindElement(By.CssSelector("input[name=keywords]")).SendKeys("TestDuck");
            information.FindElement(By.CssSelector("input[name*=short]")).SendKeys("It is a test duck");
            information.FindElement(By.TagName("textarea")).SendKeys(@"Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
                    Duis pretium metus at sem sagittis consectetur. 
                    Praesent facilisis diam vel lobortis fermentum.");
            information.FindElement(By.CssSelector("input[name*=head]")).SendKeys("Test duck");
            information.FindElement(By.CssSelector("input[name*=meta]")).SendKeys("Testing");

            tabs = driver.FindElements(By.CssSelector(".index li"));
            tabs[tabsD["Prices"]].Click();
            var prices = driver.FindElement(By.Id("tab-prices"));
            prices.FindElement(By.CssSelector("input[name=purchase_price]")).SendKeys("10");
            var currency = new SelectElement(prices.FindElement(By.CssSelector("select[name=purchase_price_currency_code]")));
            currency.SelectByValue("USD");
            var pricesIncTax = prices.FindElements(By.CssSelector("input[name*=prices]"));
            foreach(var price in pricesIncTax)
            {
                price.SendKeys("20");
            }

            driver.FindElement(By.CssSelector("button[name=save]")).Click();

            var products = driver.FindElements(By.CssSelector("td:nth-child(3)>a"));
            var pnames = new HashSet<string>();
            foreach(var product in products)
            {
                pnames.Add(product.Text);
            }
            Assert.That(pnames.Contains(name.ToString()));
        }
    }
}
