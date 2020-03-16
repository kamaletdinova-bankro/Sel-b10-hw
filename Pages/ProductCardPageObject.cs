using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Sel_b10_hw
{
    public class ProductCardPageObject : Page
    {
        public ProductCardPageObject(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "button[name=add_cart_product]")]
        internal IWebElement AddProduct;

        public void ChooseSezeIfPossible()
        {
            var selection = driver.FindElements(By.CssSelector("select[required=required]"));
            if (selection.Count > 0)
            {
                var size = new SelectElement(selection[0]);
                size.SelectByText("Small");
            }
        }
    }
}
