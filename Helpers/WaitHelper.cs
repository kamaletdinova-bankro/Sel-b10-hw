using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Sel_b10_hw
{
    public class WaitHelper: HelperBase
    {        
        protected WebDriverWait wait;

        public WaitHelper(ApplicationManager app)
            : base(app)
        {
            this.driver = app.Browser;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void TextToBePresent(IWebElement element, string text)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, text));
        }

        public void PauseStart()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void PauseStop()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public bool IsElementChanged(By locator, ref IWebElement etalon)
        {
            try
            {
                PauseStart();
                if (etalon.Equals(driver.FindElement(locator)))
                {
                    return false;
                }
                etalon = driver.FindElement(locator);
                return true;
            }
            finally
            {
                PauseStop();
            }
        }
    }
}
