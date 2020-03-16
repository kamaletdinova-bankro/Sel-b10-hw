using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Sel_b10_hw
{
    public class AdminPageObject: Page
    {
        public AdminPageObject(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public void Login(LoginData user)
        {
            driver.FindElement(By.Name("username")).SendKeys(user.Username);
            driver.FindElement(By.Name("password")).SendKeys(user.Password);
            driver.FindElement(By.Name(user.SubmitButton)).Click();
        }
    }
}
