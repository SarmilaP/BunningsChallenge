using OpenQA.Selenium;

namespace Automation.Core
{
	public abstract class BaseSteps : TechTalk.SpecFlow.Steps
	{
		protected BaseSteps(IWebDriver webDriver, Options options)
		{
			WebDriver = webDriver;
			Options = options;
		}

		protected IWebDriver WebDriver { get; }

		protected Options Options { get; }

		protected string TrimStepArgument(string argument)
		{
			return argument.Replace("'", string.Empty);
		}
	}
}
