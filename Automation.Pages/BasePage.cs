using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace Automation.Pages.Common
{
	public abstract class BasePage
	{
		protected BasePage(IWebDriver webDriver)
		{
			WebDriver = webDriver;
		}

		protected IWebDriver WebDriver { get; }

		protected IWebElement FindElement(By locator)
		{
			var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(50));
			wait.Until(__driver => __driver.FindElement(locator).Enabled);
			return WebDriver.FindElement(locator);
		}

		protected ReadOnlyCollection<IWebElement> FindElements(By locator)
		{
			var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
			wait.Until(__driver => __driver.FindElements(locator).Count>0);
			return WebDriver.FindElements(locator);
		}

		protected void SelectDropdownByText(By locator, string text)
		{
			var element = FindElement(locator);
			if (element.Enabled == false || new SelectElement(element).SelectedOption.Text == text)
				return;
			new SelectElement(element).SelectByText(text);
		}

	}
}