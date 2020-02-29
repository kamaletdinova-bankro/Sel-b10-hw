using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Sel_b10_hw
{
    public class BrowserList
    {
        private static FirefoxDriver firefox;
        private static ChromeDriver chrome;
        private static InternetExplorerDriver ie;

        protected Dictionary<string, IWebDriver> driverList = new Dictionary<string, IWebDriver> {
            {"firefox", firefox},
            {"chrome", chrome},
            {"ie", ie}
        };

        public IWebDriver GetDriver(string browser)
        {
            switch(browser)
            {
                case "firefox":
                    driverList[browser] = new FirefoxDriver();
                    break;
                case "chrome":
                    driverList[browser] = new ChromeDriver();
                    break;
                case "ie":
                    driverList[browser] = new InternetExplorerDriver();
                    break;
            }
            return driverList[browser];
        }

    }
}
