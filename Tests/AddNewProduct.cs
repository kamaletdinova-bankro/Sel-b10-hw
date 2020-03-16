using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Sel_b10_hw.Model;

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
            for(int i=0; i < tabs.Count; i++)
            {
                tabsD.Add(tabs[i].Text, i);
            }

            //Дальше адд
        }
    }
}
