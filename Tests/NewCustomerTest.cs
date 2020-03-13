using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Sel_b10_hw.Model;

namespace Sel_b10_hw
{
    [TestFixture]
    public class NewCustomer
    {
        private IWebDriver driver;
        [SetUp]
        public void CreateDriver()
        {
            driver = new ChromeDriver();
        }
        [TearDown]
        public void StopDriver()
        {
            driver.Quit();
        }
        [Test]
        public void CreateNewone()
        {
            Guid emailName = Guid.NewGuid();
            var customer = new CreateAccount(emailName.ToString());

            driver.Url = @"http://localhost/litecart/en/create_account";
            
            customer.FirstName = driver.FindElement(By.CssSelector("input[name=firstname]"));            
            customer.FirstName.SendKeys(customer.FirstNameValue);

            customer.LastName = driver.FindElement(By.CssSelector("input[name=lastname]"));
            customer.LastName.SendKeys(customer.LastNameValue);

            customer.Address = driver.FindElement(By.CssSelector("input[name=address1]"));
            customer.Address.SendKeys(customer.AddressValue);
            
            customer.Country = new SelectElement(driver.FindElement(By.CssSelector("select[name=country_code]")));
            customer.Country.SelectByText(customer.CountryValue);

            customer.Postcode = driver.FindElement(By.CssSelector("input[name=postcode]"));
            customer.Postcode.SendKeys(customer.PostcodeValue);

            customer.City = driver.FindElement(By.CssSelector("input[name=city]"));
            customer.City.SendKeys(customer.CityValue);

            customer.Email = driver.FindElement(By.CssSelector("input[name=email]"));
            customer.Email.SendKeys(customer.EmailValue);

            customer.Phone = driver.FindElement(By.CssSelector("input[name=phone]"));
            customer.Phone.SendKeys(customer.PhoneValue);

            customer.DesiredPassword = driver.FindElement(By.CssSelector("input[name=password]"));
            customer.DesiredPassword.SendKeys(customer.PasswordValue);

            customer.ConfirmPassword = driver.FindElement(By.CssSelector("input[name=confirmed_password]"));
            customer.ConfirmPassword.SendKeys(customer.PasswordValue);

            driver.FindElement(By.CssSelector("button[name=create_account]")).Click();

            customer.DesiredPassword = driver.FindElement(By.CssSelector("input[name=password]"));
            customer.DesiredPassword.SendKeys(customer.PasswordValue);

            customer.ConfirmPassword = driver.FindElement(By.CssSelector("input[name=confirmed_password]"));
            customer.ConfirmPassword.SendKeys(customer.PasswordValue);

            driver.FindElement(By.CssSelector("button[name=create_account]")).Click();

            var accountLinks = driver.FindElements(By.CssSelector("div#box-account ul.list-vertical li a"));
            foreach(var link in accountLinks)
            {
                if(link.Text.ToLower()== "logout")
                {
                    link.Click();
                }
            }

            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(customer.EmailValue);
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(customer.PasswordValue);
            driver.FindElement(By.CssSelector("button[name=login]")).Click();

            accountLinks = driver.FindElements(By.CssSelector("div#box-account ul.list-vertical li a"));
            foreach (var link in accountLinks)
            {
                if (link.Text.ToLower() == "logout")
                {
                    link.Click();
                }
            }
        }
    }
}
