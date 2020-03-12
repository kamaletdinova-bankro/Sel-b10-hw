using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Sel_b10_hw.Model
{
    public class CreateAccount
    {
        public IWebElement FirstName { get; set; }
        public IWebElement LastName { get; set; }
        public IWebElement Address { get; set; }
        public IWebElement Postcode { get; set; }
        public IWebElement City { get; set; }
        public SelectElement Country { get; set; }
        public IWebElement Email { get; set; }
        public IWebElement Phone { get; set; }
        public IWebElement DesiredPassword { get; set; }
        public IWebElement ConfirmPassword { get; set; }

        public string FirstNameValue { get; set; }
        public string LastNameValue { get; set; }
        public string AddressValue { get; set; }
        public string PostcodeValue { get; set; }
        public string CityValue { get; set; }
        public string CountryValue { get; set; }
        public string EmailValue { get; set; }
        public string PhoneValue { get; set; }
        public string PasswordValue { get; set;}

        public CreateAccount(string email)
        {
            FirstNameValue = "Jane "+email;
            LastNameValue = "Dou";
            AddressValue = "Nowhere";
            PostcodeValue = "01234";
            CityValue = "New York";
            CountryValue = "United States";
            EmailValue = email + "@gmail.com";
            PhoneValue = "+12345678900";
            PasswordValue = "qwerty";            
        }
    }
}
