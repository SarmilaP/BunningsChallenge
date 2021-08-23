namespace Automation.Configuration
{
	public class Settings
	{
		public static string Target => ConfigurationManager.GetSetting<string>("Target");
		public static string SiteUrl => ConfigurationManager.GetSetting<string>("Web.Url");
	}
}