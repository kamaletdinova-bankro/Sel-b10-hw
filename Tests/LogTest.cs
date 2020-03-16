using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Sel_b10_hw
{
    [TestFixture]
    public class LogTest
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
        public void CatalogLog()
        {
            driver.Url = @"http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.Url = @"http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1/";

            driver.FindElement(By.ClassName("button")).Click();

           
        }
    }
}