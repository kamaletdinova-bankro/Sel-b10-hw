using NUnit.Framework;
using OpenQA.Selenium;

namespace Sel_b10_hw
{
    public class TestBase
    {
        protected HelperManager helper;

        [SetUp]
        public void Start()
        {
            helper = new HelperManager();
        }        

        public void Login(LoginData user)
        {
            helper.Browser.FindElement(By.Name("username")).SendKeys(user.Username);
            helper.Browser.FindElement(By.Name("password")).SendKeys(user.Password);
            helper.Browser.FindElement(By.Name(user.SubmitButton)).Click();
        }
    }
}
