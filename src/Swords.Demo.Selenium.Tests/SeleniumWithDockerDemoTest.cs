﻿namespace Swords.Demo.SeleniumDemo.Tests
{
	using System;

	using Microsoft.Extensions.Configuration;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using OpenQA.Selenium.Remote;

	[TestClass]
	public class SeleniumWithDockerDemoTest : SeleniumDemoTest
	{
		[TestInitialize]
		public override void Setup()
		{
			// Nuget packages:
			// - Microsoft.Extensions.Configuration.Binder
			// - Microsoft.Extensions.Configuration.EnvironmentVariables
			// - Microsoft.Extensions.Configuration.Json
			var configuration =
				new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
										  .AddJsonFile("appsettings.json")
										  .AddEnvironmentVariables()
										  .Build();

			var seleniumOptions = new SeleniumOptions();
			configuration.Bind("SeleniumDockerHost", seleniumOptions);

			this.webDriver = new RemoteWebDriver(
				seleniumOptions.WebDriverUri,
				seleniumOptions.DriverOptions);
		}
	}
}