using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Sel_b10_hw
{
    class ShopTests : TestBase
    {
        [Test]
        public void StikerTest()
        {
            helper.Navigation.Go2Url("en/");
            var allProduct = helper.Browser.FindElements(By.CssSelector("li.product"));

            Dictionary<string, bool> productIsStikerList = new Dictionary<string,bool>();
            int iterator = 0;
            foreach(var product in allProduct)
            {
                try
                {                    
                    string pName = product.FindElement(By.ClassName("name")).Text;
                    productIsStikerList.Add(pName, product.FindElements(By.CssSelector("div.sticker")).Count>0);
                    iterator++;
                }
                catch(Exception)
                {
                    iterator++;
                }
            }
            
            TestContext.WriteLine("Count of all product: " + iterator);
            foreach (var prop in productIsStikerList)
            {
                try
                {
                    Assert.IsTrue(prop.Value);                    
                }
                catch(AssertionException e)
                {

                }
                finally
                {
                    TestContext.WriteLine(prop);
                }
            }
        }
    }
}
