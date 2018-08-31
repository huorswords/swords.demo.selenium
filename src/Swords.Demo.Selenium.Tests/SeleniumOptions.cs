namespace Swords.Demo.SeleniumDemo.Tests
{
	using System;

	using OpenQA.Selenium;

	internal class SeleniumOptions
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public string BrowserName { get; set; }

		public Uri WebDriverUri
		{
			get
			{
				string url = $"http://{this.Host}:{this.Port}/wd/hub";
				if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
				{					
					return new Uri(url);
				}

				return null;
			}
		}

		public DriverOptions DriverOptions
		{
			get
			{
				if (!string.IsNullOrEmpty(this.BrowserName))
				{
					switch (this.BrowserName.ToLower())
					{
						case "firefox":
							return new OpenQA.Selenium.Firefox.FirefoxOptions();

						case "chrome":
							return new OpenQA.Selenium.Chrome.ChromeOptions();

						default:
							throw new ArgumentOutOfRangeException(nameof(this.BrowserName));
					}
				}

				return null;
			}
		}
	}
}