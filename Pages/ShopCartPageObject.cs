using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Sel_b10_hw
{
    public class ShopCartPageObject : Page
    {
        public ShopCartPageObject(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "span.quantity")]
        internal IWebElement CartCount;

        [FindsBy(How = How.CssSelector, Using = "#cart .link")]
        internal IWebElement CartLink;

        [FindsBy(How = How.CssSelector, Using = "table.dataTable tr:last-child")]
        internal IWebElement OrderSummary;

        [FindsBy(How = How.CssSelector, Using = "li.item")]
        internal IList<IWebElement> AllProducts;

        [FindsBy(How = How.CssSelector, Using = "li.shortcut")]
        internal IList<IWebElement> AllDifferentProducts;

        [FindsBy(How = How.CssSelector, Using = "button[name=remove_cart_item]")]
        internal IWebElement DeleteProduct;

        [FindsBy(How =How.TagName, Using ="em")]
        internal IList<IWebElement> EmptyCart;
    }
}
