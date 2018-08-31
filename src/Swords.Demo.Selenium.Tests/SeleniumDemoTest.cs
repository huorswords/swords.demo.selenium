namespace Swords.Demo.SeleniumDemo.Tests
{
	using System;
	using System.Linq;
	using System.Threading;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using OpenQA.Selenium;
	using OpenQA.Selenium.Chrome;

	[TestClass]
	public class SeleniumDemoTest
	{
		protected IWebDriver webDriver;

		[TestInitialize]
		public virtual void Setup() 
			=> this.webDriver = new ChromeDriver(Environment.CurrentDirectory);

		[TestCleanup]
		public virtual void Cleanup()
			=> this.webDriver?.Close();

		[TestMethod]
		public void Google_Should_ShowMyWebPageFirst_When_LookingForSpecificUrl()
		{
			const string ExpectedWebTitle = "Soy Ángel.G";
			const string TextToSearch = "huorswords.github.io";

			// 1. Navigate and search on Google
			this.SearchOnGoogle(TextToSearch);

			// 2. Analyze search results
			var searchFirstResult = this.webDriver.FindElement(By.Id("search"))
												  .FindElements(By.ClassName("g"))
												  .First()
												  .FindElement(By.ClassName("r"));

			Assert.AreEqual(ExpectedWebTitle, searchFirstResult.Text);

			// 3. Navigate (by click) to first Google search result.
			searchFirstResult.FindElement(By.CssSelector("a"))
							 .Click();

			StringAssert.Contains(this.webDriver.Title, ExpectedWebTitle);

			// TOO FAST? Uncomment next line to wait for 5 seconds...
			// Thread.Sleep(5000);
		}

		private void SearchOnGoogle(string TextToSearch)
		{
			this.webDriver.Navigate()
						  .GoToUrl("http://www.google.com");

			IWebElement searchBox = this.webDriver.FindElement(By.Name("q"));
			searchBox.SendKeys(TextToSearch);
			searchBox.Submit();
		}
	}
}