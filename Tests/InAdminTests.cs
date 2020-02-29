using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Sel_b10_hw
{
    [TestFixture]
    class InAdminTests : TestBase
    {
        readonly LoginData admin = new LoginData("admin", "admin", "login");

        [Test]
        public void LoginAdmin()
        {
            helper.Navigation.Go2Url("admin/");
            Login(admin);
        }

        [Test]
        [Description("Exercise seven")]
        public void LeftMenuTest()
        {
            helper.Navigation.Go2Url("admin/");
            Login(admin);
            
            var leftsideBar = helper.Browser.FindElements(By.CssSelector("ul#box-apps-menu li"));

            List<string> menu = new List<string>();
            foreach (var item in leftsideBar)
            {
                menu.Add(item.Text);
            }            

            for(int menuItem=0; menuItem< menu.Count; menuItem++)
            {                
                foreach(var item in leftsideBar)
                {
                    if (item.Text == menu[menuItem])
                    {
                        item.Click();
                        break;
                    }
                }
                
                Assert.IsNotNull(helper.Browser.FindElements(By.CssSelector("h1")));

                leftsideBar = helper.Browser.FindElements(By.CssSelector("ul#box-apps-menu li"));
                var newMenu = new List<string>();
                int nextItem=0;
                foreach (var item in leftsideBar)
                {
                    newMenu.Add(item.Text);
                    if(item.Text== menu[menuItem])
                    {
                        nextItem= newMenu.Count;
                    }
                }
                
                try
                {
                    if (newMenu[nextItem] != menu[menuItem + 1])
                    {
                        for (int newMenuItem = 0; newMenuItem < newMenu.Count; newMenuItem++)
                        {
                            if (newMenu[newMenuItem].IndexOf("\r") > 0)
                            {
                                newMenu[newMenuItem] = newMenu[newMenuItem].Substring(0, newMenu[newMenuItem].IndexOf("\r"));
                            }
                        }

                        int newItemIdStart = newMenu.IndexOf(menu[menuItem]);
                        int newItemIdEnd = newMenu.IndexOf(menu[menuItem + 1]);
                        menu.InsertRange(menuItem + 1, newMenu.GetRange(newItemIdStart+1, newItemIdEnd - newItemIdStart-1));
                        menuItem++;                        
                    }
                }
                catch (Exception e)
                {

                }
            }
            TestContext.WriteLine("All menu item count: "+menu.Count);
            foreach (var s in menu)
            {
                TestContext.WriteLine(s);
            }
        }
    }
}
