using System;
using System.Configuration;

namespace Automation.Core
{
	public static class ConfigurationManager
	{
		public static T GetSetting<T>(string settingName)
		{
			return GetSettingFromConfigOrEnvironment<T>(settingName);
		}

		private static T GetSettingFromConfigOrEnvironment<T>(string settingName)
		{
			if (string.IsNullOrEmpty(settingName))
				throw new Exception("settingName is required.");

			var appSettingValue = Environment.GetEnvironmentVariable($"{settingName}", EnvironmentVariableTarget.Machine);

			if (!string.IsNullOrEmpty(appSettingValue))
				return ChangeType<T>(appSettingValue);
			
			appSettingValue = ConfigurationSettings.AppSettings[settingName];

			return !string.IsNullOrEmpty(appSettingValue) ? ChangeType<T>(appSettingValue) : default(T);
		}
			

		private static T ChangeType<T>(string appSettingValue)
		{
			try
			{
				return (T)Convert.ChangeType(appSettingValue, typeof(T));
			}
			catch (Exception e)
			{
				throw new Exception("The setting value could not be converted to the expected type.", e);
			}
		}
	}
}