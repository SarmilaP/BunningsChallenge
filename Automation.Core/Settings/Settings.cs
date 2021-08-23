namespace Automation.Core
{
	public class Settings
	{
		public static string Target => ConfigurationManager.GetSetting<string>("Target");
		public static string WebUrl => ConfigurationManager.GetSetting<string>("Web.Url");
		
	}
}