using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Automation.WebDriver
{
	public class WebDriverFactory
	{
		private string _key;

		private IWebDriver _webDriver;

		public IWebDriver Create(string key)
		{
			if (_key == key && _webDriver != null) return _webDriver;

			_key = key;
			_webDriver = DoCreate(key);
			return _webDriver;
		}

		private IWebDriver DoCreate(string key)
		{
			switch (key)
			{
				case "Chrome":
					var chromeOptions = new ChromeOptions();
					chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
					chromeOptions.AddExcludedArgument("ignore-certificate-errors");
					chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
					chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
					return new ChromeDriver(chromeOptions);
			}
			throw new ArgumentException(@"Invalid browser key", nameof(key));
		}
	}
}