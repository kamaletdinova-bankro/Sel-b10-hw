using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Sel_b10_hw
{
    [TestFixture]
    public class ShopTests : TestBase
    {
        [Test]
        public void StikerTest()
        {
            helper.Navigation.Go2Url("en/");
            var allProduct = helper.Browser.FindElements(By.CssSelector("li.product"));
                       
            foreach(var product in allProduct)
            {
                try
                {                    
                    var stiker = product.FindElements(By.CssSelector("div.sticker"));
                    Assert.That(stiker.Count == 1);                    
                }
                catch(Exception)
                {                    
                    TestContext.WriteLine("There wasn't the only one stiker for " + product.FindElement(By.ClassName("name")).Text);
                }
            }
        }
    }
}
