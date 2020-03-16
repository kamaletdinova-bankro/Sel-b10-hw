using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Sel_b10_hw
{
    [TestFixture]
    public class WorkWithCartTest : TestBase
    {        
        [Test]
        public void AddAndDeleteProductsInCart()
        {
            for(int i=1; i<4; i++)
            {
                app.Nav.GoShopHome();
                app.Browser.FindElement(By.CssSelector("li.product")).Click();
                
                var cartCount = app.Browser.FindElement(By.CssSelector("span.quantity"));
                var selection = app.Browser.FindElements(By.CssSelector("select[required=required]"));
                if (selection.Count>0)
                {
                    var size = new SelectElement(selection[0]);
                    size.SelectByText("Small");
                }

                app.Browser.FindElement(By.CssSelector("button[name=add_cart_product]")).Click();
                app.Wait.TextToBePresent(cartCount, i.ToString());
            }

            app.Wait.PauseStart();


            app.Browser.FindElement(By.CssSelector("#cart .link")).Click();            
            
            var order = app.Browser.FindElement(By.CssSelector("table.dataTable tr:last-child"));
            
            var products = app.Browser.FindElements(By.CssSelector("li.item"));
            var productsCount = products.Count;
            app.Wait.PauseStop();

            for (int i = 0; i < productsCount; i++)
            {
                if (products.Count > 1)
                {
                    app.Browser.FindElements(By.CssSelector("li.shortcut"))[0].Click();
                }

                app.Browser.FindElement(By.CssSelector("button[name=remove_cart_item]")).Click();
                app.Wait.IsElementChanged(By.CssSelector("table.dataTable tr:last-child"), ref order);

                if (app.Browser.FindElements(By.TagName("em")).Count > 0)
                {
                    break;
                }

                products = app.Browser.FindElements(By.CssSelector("li.item"));
            }
        }        

        [Test]
        public void NewAddAndDeleteProductsInCart()
        {
            app.Add3product();
            app.Delete3products();
        }
    }
}
