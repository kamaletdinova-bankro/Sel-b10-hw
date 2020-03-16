using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Sel_b10_hw
{
    public class Page
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public Page(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
    }
}
