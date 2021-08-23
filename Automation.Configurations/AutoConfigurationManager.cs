using System;
using System.Configuration;

namespace Automation.Configuration
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

			appSettingValue = System.Configuration.ConfigurationManager.AppSettings[settingName];

			return !string.IsNullOrEmpty(appSettingValue) ? ChangeType<T>(appSettingValue) : default(T);
		}

		public static string GetConnectionString(string connectionName)
		{
			if (string.IsNullOrEmpty(connectionName))
				throw new Exception("connectionName is required.");

			var connectionStringFromEnvironmentVariable = Environment.GetEnvironmentVariable(
				$"{connectionName}", EnvironmentVariableTarget.Machine);

			if (!string.IsNullOrEmpty(connectionStringFromEnvironmentVariable))
				return connectionStringFromEnvironmentVariable;

			var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName];

			if (connectionString != null && !string.IsNullOrEmpty(connectionString.ConnectionString))
				return connectionString.ConnectionString;

			return null;
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