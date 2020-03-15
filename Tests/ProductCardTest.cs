using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Sel_b10_hw.Model;

namespace Sel_b10_hw
{
	[TestFixture(typeof(FirefoxDriver))]
	[TestFixture(typeof(InternetExplorerDriver))]
	[TestFixture(typeof(ChromeDriver))]
	internal class Tests<TWebDriver> where TWebDriver : IWebDriver, new()
	{
		private IWebDriver driver;
		[SetUp]
		public void CreateDriver()
		{
			this.driver = new TWebDriver();
		}
		[TearDown]
		public void StopDriver()
		{
			driver.Quit();
		}
		[Test]
		public void ProductCardCheck()
		{
			driver.Url = "http://localhost/litecart/en/";
			var product = driver.FindElement(By.CssSelector("div#box-campaigns li.product"));

			var littleCard = new ProductCard();
			littleCard.ProductName = product.FindElement(By.ClassName("name")).Text;
			GetPrice(product,ref littleCard);			

			product.Click();

			product = driver.FindElement(By.Id("box-product"));

			var bigCard = new ProductCard();
			bigCard.ProductName = product.FindElement(By.TagName("h1")).Text;
			GetPrice(product, ref bigCard);

			Assert.Multiple(()=> 
			{
				Assert.AreEqual(littleCard.ProductName, bigCard.ProductName);
				Assert.AreEqual(littleCard.MainPrice, bigCard.MainPrice);
				Assert.AreEqual(littleCard.SalePrice, bigCard.SalePrice);

				StringAssert.Contains("line-through", littleCard.MainPriceType);
				Assert.IsTrue(IsItGray(FromString(littleCard.MainPriceColor)));
				Assert.AreEqual("strong", littleCard.SalePriceType);
				Assert.IsTrue(IsItRed(FromString(littleCard.SalePriceColor)));
				Assert.IsTrue(IsItSmaller(littleCard.MainPriceSize, littleCard.SalePriceSize));

				StringAssert.Contains("line-through", bigCard.MainPriceType);
				Assert.IsTrue(IsItGray(FromString(bigCard.MainPriceColor)));
				Assert.AreEqual("strong", bigCard.SalePriceType);
				Assert.IsTrue(IsItRed(FromString(bigCard.SalePriceColor)));
				Assert.IsTrue(IsItSmaller(bigCard.MainPriceSize, bigCard.SalePriceSize));
			});			
		}
		private void GetPrice(IWebElement product, ref ProductCard card)
		{
			var regularPrice = product.FindElement(By.ClassName("regular-price"));
			card.MainPrice = regularPrice.Text;
			card.MainPriceColor = regularPrice.GetCssValue("color");
			card.MainPriceSize = regularPrice.GetCssValue("font-size");
			card.MainPriceType = regularPrice.GetCssValue("text-decoration");

			var salePrice = product.FindElement(By.ClassName("campaign-price"));
			card.SalePrice = salePrice.Text;
			card.SalePriceColor = salePrice.GetCssValue("color");
			card.SalePriceSize = salePrice.GetCssValue("font-size");
			card.SalePriceType = salePrice.TagName;
		}
		private Color FromString(string stringColor)
		{
			var start = stringColor.IndexOf("(")+1;
			var length = stringColor.Length - 1 - start;
			var rgba = stringColor.Substring(start, length);			
			var rgb = rgba.Split(',');
			var color = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
			return color;
		}

		private bool IsItGray(Color color)
		{
			return color.R == color.G && color.R == color.B && color.G == color.B;
		}

		private bool IsItRed(Color color)
		{
			return color.G == 0 && color.B == 0;
		}
		private bool IsItSmaller(string little, string big)
		{
			var l = double.Parse(little.Remove(little.IndexOf("p"), 2).Replace(".", ","));
			var b = double.Parse(big.Remove(big.IndexOf("p"), 2).Replace(".", ","));
			return l < b;
		}
	}
}