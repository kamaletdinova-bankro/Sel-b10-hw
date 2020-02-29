using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using Sel_b10_hw;

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

        [Test]
        [Description("Exercise nine")]
        public void Countries()
        {
            helper.Navigation.Go2Url("admin/?app=countries&doc=countries");
            Login(admin);

            var countriesTable = helper.Browser.FindElements(By.ClassName("row"));
            var contries = new List<Countries>();
            
            foreach(var row in countriesTable)
            {                
                var countriesColumn = row.FindElements(By.TagName("td"));

                var currentLink = countriesColumn[4].FindElement(By.TagName("a"));

                contries.Add(new Countries() { 
                    Id = int.Parse(countriesColumn[2].Text),
                    Code = countriesColumn[3].Text,
                    Name = countriesColumn[4].Text,
                    Link = currentLink.GetAttribute("href"),
                    Zones = int.Parse(countriesColumn[5].Text)});                
            }

            var etalon = new List<string>();
            var bigContries = new List<Countries>();

            foreach (var contry in contries)
            {
                etalon.Add(contry.Name);
                if(contry.Zones>0)
                {
                    bigContries.Add(contry);
                }
            }

            Assert.Multiple(() =>
            {
                Assert.That(etalon, Is.Ordered.Ascending); 
                
                foreach(var contry in bigContries)
                {
                    helper.Navigation.Go2Url(contry.Link, true);
                    var zonesTable = helper.Browser.FindElements(By.CssSelector("table#table-zones tr"));
                    var contriesZones = new List<CountryZones>();

                    for(int i=1; i<(zonesTable.Count-1);i++)
                    {
                        var zonesColumn = zonesTable[i].FindElements(By.TagName("td"));

                        contriesZones.Add(new CountryZones() { 
                        Id=int.Parse(zonesColumn[0].Text),
                        Code = zonesColumn[1].Text,
                        Name = zonesColumn[2].Text});
                    }

                    etalon.Clear();
                    foreach(var zone in contriesZones)
                    {
                        etalon.Add(zone.Name);
                    }

                    Assert.That(etalon, Is.Ordered.Ascending);
                }
            });            
        }
    }
}
