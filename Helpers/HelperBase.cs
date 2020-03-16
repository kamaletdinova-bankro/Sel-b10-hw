using OpenQA.Selenium;

namespace Sel_b10_hw
{
    public class HelperBase
    {
        protected ApplicationManager app;
        protected IWebDriver driver;

        public HelperBase(ApplicationManager application)
        {
            app = application;
            driver = app.Browser;
        }
    }
}
