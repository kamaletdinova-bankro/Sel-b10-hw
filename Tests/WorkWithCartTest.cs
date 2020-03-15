using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Sel_b10_hw
{
    [TestFixture]
    public class WorkWithCartTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        
        [SetUp]
        public void CreateDriver()
        {            
            driver = new ChromeDriver();            
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [TearDown]
        public void StopDriver()
        {
            driver.Quit();
        }
        [Test]
        public void AddAndDeleteProductsInCart()
        {
            for(int i=1; i<4; i++)
            {
                driver.Url = @"http://localhost/litecart/en/";
                driver.FindElement(By.CssSelector("li.product")).Click();
                
                var cartCount = driver.FindElement(By.CssSelector("span.quantity"));
                var selection = driver.FindElements(By.CssSelector("select[required=required]"));
                if (selection.Count>0)
                {
                    var size = new SelectElement(selection[0]);
                    size.SelectByText("Small");
                }

                driver.FindElement(By.CssSelector("button[name=add_cart_product]")).Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(cartCount,i.ToString()));
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.CssSelector("#cart .link")).Click();            
            var order = driver.FindElement(By.CssSelector("table.dataTable tr:last-child"));
            var products = driver.FindElements(By.CssSelector("li.item"));
            var productsCount = products.Count;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            for (int i = 0; i < productsCount; i++)
            {
                if (products.Count > 1)
                {
                    driver.FindElements(By.CssSelector("li.shortcut"))[0].Click();
                }
                
                driver.FindElement(By.CssSelector("button[name=remove_cart_item]")).Click();
                isElementChanged(driver, By.CssSelector("table.dataTable tr:last-child"), ref order);

                if (driver.FindElements(By.TagName("em")).Count > 0)
                {
                    break;
                }

                
               
                products = driver.FindElements(By.CssSelector("li.item"));
            }
        }

        public bool isElementChanged(IWebDriver driver, By locator, ref IWebElement etalon)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                if (etalon.Equals(driver.FindElement(locator)))
                {
                    return false;
                }
                etalon = driver.FindElement(locator);
                return true;                
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            }            
        }
    }
}
