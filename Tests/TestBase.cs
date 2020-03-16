using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Sel_b10_hw
{
    [TestFixture]
    public class TestBase
    {
        protected ApplicationManager app;

        [OneTimeSetUp]
        public void Start()
        {
            app = new ApplicationManager((typeof(ChromeDriver)));
        }        
    }
}
