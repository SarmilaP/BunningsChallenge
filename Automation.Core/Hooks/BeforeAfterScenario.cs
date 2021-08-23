using BoDi;
using OpenQA.Selenium;
using Automation.Configuration;
using Automation.WebDriver;
using System;
using TechTalk.SpecFlow;

namespace Automation.Core.Hooks
{
	[Binding]
	public class BeforeAfterScenario
	{
		private readonly IObjectContainer _objectContainer;
		private readonly WebDriverFactory _webDriverFactory = new WebDriverFactory();
		private IWebDriver _webDriver;

		public BeforeAfterScenario(IObjectContainer objectContainer, ScenarioContext context)
		{
			switch (Settings.Target)
			{
				case "Chrome":
						_objectContainer = objectContainer;
					break;
				default:
					throw new PlatformNotSupportedException(
						$"Your target '{Settings.Target}' is not supported");
			}
		}

		[BeforeScenario(Order = 0)]
		public void BeforeScenario()
		{
			RegisterOptions();
			RegisterWebDriver();			
		}

		[BeforeScenario("@Pending", Order = 1)]
		public void PendingScenario() => throw new PendingStepException();

		[AfterScenario(Order = 1)]
		public void AfterScenarioCloseWebDriver()
		{
			DisposeWebDriver();
		}


		private void RegisterOptions()
		{
			var options = new Options
			{
				BunningsSiteUri = new Uri(Settings.WebUrl)
			};
			_objectContainer.RegisterInstanceAs(options);
		}

		private void RegisterWebDriver()
		{
			_webDriver = _webDriverFactory.Create(Settings.Target);
			_objectContainer.RegisterInstanceAs(_webDriver);			
		}

		private void DisposeWebDriver()
		{
			_webDriver.Quit();
			_webDriver.Dispose();
		}
	}
}