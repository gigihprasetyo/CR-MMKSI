using System;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Tests
{
#if UNIT_TESTS
	/// <summary>
	/// Summary description for MockStorageProvider.
	/// </summary>
	public class MockStorageProvider : StorageProvider
	{
		public MockStorageProvider()
		{
		}

		public override object Read()
		{
			return null;
		}

		public override Microsoft.Practices.EnterpriseLibrary.Configuration.Storage.IConfigurationChangeWatcher CreateConfigurationChangeWatcher()
		{
			return new NullConfigurationChangeWatcher(base.CurrentSectionName);
		}

		public override void Initialize(ConfigurationView configurationView)
		{
        }
	}
#endif
}
