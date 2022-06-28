// WARNING:
// This file generated by the Microsoft DataWarehouse String Resource Tool 1.13.5000.0
// from information in SR.strings.   
// DO NO MODIFY THIS FILE'S CONTENTS, THEY WILL BE OVERWRITTEN
//
namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Tests.Extensions
{
	using System;
	using System.Resources;
	using System.Globalization;

	internal class SR
	{
		public static string AppConfigFileStorageProviderNodeName
		{
			get { return Keys.GetString( Keys.AppConfigFileStorageProviderNodeName ); }
		}
		public static string AppConfigFileStorageProviderMenuName
		{
			get { return Keys.GetString( Keys.AppConfigFileStorageProviderMenuName ); }
		}

		internal class Keys 
		{
			static ResourceManager resourceManager = 
				new ResourceManager("Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Tests.Extensions.SR", typeof(Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Tests.Extensions.SR).Module.Assembly );

			public static string GetString( string key )
			{
				return resourceManager.GetString( key, CultureInfo.CurrentUICulture );
			}
			public static string GetString( string key, params object[] args )
			{
				string msg = resourceManager.GetString( key, CultureInfo.CurrentUICulture );
				msg = string.Format( msg, args );
				return msg;
			}

			public const string AppConfigFileStorageProviderNodeName = "AppConfigFileStorageProviderNodeName";
			public const string AppConfigFileStorageProviderMenuName = "AppConfigFileStorageProviderMenuName";
		}


	}
}