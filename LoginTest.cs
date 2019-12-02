using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace Sel_b10_hw
{
    [TestFixture]
    class LoginTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [Test]
        public void LoginAdmin()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }
        [TearDown]
        public void Stop()
        {
            try
            {
                driver.Quit();
                driver = null;
            }
            catch(Exception)
            {

            }
        }
    }
}
