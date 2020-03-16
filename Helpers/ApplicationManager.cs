using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Sel_b10_hw
{
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    [TestFixture(typeof(ChromeDriver))]
    public class ApplicationManager
    {
        protected IWebDriver _browser;       
        protected string _base;
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();
        protected NavigationHelper _navigation;
        protected WaitHelper _waiter;

        protected AdminPageObject _adminPage;
        protected ShopPageObject _mainShopPage;
        protected ProductCardPageObject _productCardPage;
        protected ShopCartPageObject _cartPage;

        public IWebDriver Browser => _browser;
        public WaitHelper Wait => _waiter;
        public NavigationHelper Nav => _navigation;
        
        public AdminPageObject Admin => _adminPage;

        public ApplicationManager()
        {
            _base = @"http://localhost/litecart/";
            _browser = GetDriver("chrome");                        
            _navigation = new NavigationHelper(this, _base);
            _waiter = new WaitHelper(this);

            _adminPage = new AdminPageObject(_browser);
            _mainShopPage = new ShopPageObject(_browser);
            _productCardPage = new ProductCardPageObject(_browser);
            _cartPage = new ShopCartPageObject(_browser);
        }

        public ApplicationManager(Type type)
        {
            _base = @"http://localhost/litecart/";
            _browser = GetDriver(type.Name);
            _navigation = new NavigationHelper(this, _base);
            _waiter = new WaitHelper(this);

            _adminPage = new AdminPageObject(_browser);
            _mainShopPage = new ShopPageObject(_browser);
            _productCardPage = new ProductCardPageObject(_browser);
            _cartPage = new ShopCartPageObject(_browser);
        }

        ~ApplicationManager()
        {
            try
            {
                _browser.Quit();
                _browser = null;
            }
            catch (Exception)
            {

            }
        }
        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                app.Value = newInstance;
            }
            return app.Value;
        }

        public IWebDriver GetDriver(string browser)
        {
            switch (browser)
            {
                case "FirefoxDriver":
                    return new FirefoxDriver();
                case "ChromeDriver":
                    return new ChromeDriver();
                case "InternetExplorerDriver":
                    return new InternetExplorerDriver();
                default:
                    return new FirefoxDriver();
            }            
        }
        
        public void Add3product()
        {
            for(int i=1; i<4; i++)
            {
                _navigation.GoShopHome();
                _mainShopPage.OpenProductCard();
                var CartCountLink = _cartPage.CartCount;
                _productCardPage.ChooseSezeIfPossible();
                _productCardPage.AddProduct.Click();
                _waiter.TextToBePresent(CartCountLink, i.ToString());
            }
            _navigation.GoShopHome();            
        }
        public void Delete3products()
        {
            _cartPage.CartLink.Click();
            _waiter.PauseStart();
            var etalon = _cartPage.OrderSummary;
            var productsCount = _cartPage.AllProducts.Count;
            for (int i = 0; i < productsCount; i++)
            {
                if (_cartPage.AllProducts.Count > 1)
                {
                    _cartPage.AllDifferentProducts[0].Click();
                }

                _cartPage.DeleteProduct.Click();
                if (_cartPage.EmptyCart.Count>0)
                {
                    break;
                }
                _waiter.IsElementChanged(By.CssSelector("table.dataTable tr:last-child"), ref etalon);
            }
            _waiter.PauseStop();
        }
    }
}
