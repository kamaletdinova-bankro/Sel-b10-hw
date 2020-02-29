using OpenQA.Selenium;

namespace Sel_b10_hw
{
    public class NavigationHelper
    {
        private IWebDriver _driver;

        public NavigationHelper(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Go2Url(string uri)
        {
            _driver.Url = "http://localhost/litecart/" + uri;
        }
    }
}
