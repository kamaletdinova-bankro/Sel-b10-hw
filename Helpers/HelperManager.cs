using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Sel_b10_hw
{
    public class HelperManager : BrowserList
    {
        protected IWebDriver _browser;
        protected NavigationHelper _navigation;
        protected WebDriverWait _wait;

        public HelperManager()
        {
            _browser = GetDriver("firefox");
            _navigation = new NavigationHelper(_browser);
            _wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(10));
        }
        
        ~HelperManager()
        {
            try
            {
                _browser.Quit();
                _browser = null;
            }
            catch (Exception)
            {

            }
        }
        public IWebDriver Browser => _browser;
        public WebDriverWait Wait => _wait;
        public NavigationHelper Navigation => _navigation;
    }
}
