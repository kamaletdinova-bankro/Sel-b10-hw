using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Sel_b10_hw
{
    public class ShopPageObject : Page
    {
        public ShopPageObject(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.CssSelector, Using = "li.product")]
        public IWebElement FirstRandomProduct;

        public void OpenProductCard()
        {
            FirstRandomProduct.Click();
        }
    }
}
