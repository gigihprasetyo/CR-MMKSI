//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Configuration Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

#if UNIT_TESTS
using System;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Storage;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Tests
{
	/// <summary>
	/// Summary description for FileWatcherFixture.
	/// </summary>
	[TestFixture]
	public class FileWatcherCountFixture
	{
		static int count;
		static ItemDetails defaultConfigData;

        [TestFixtureSetUp]
        public void ShortenDefaultTimeouts()
        {
        	ConfigurationChangedRegistryWatcher.SetDefaultPollDelayInMilliseconds(250);
            ConfigurationChangeSqlWatcher.SetDefaultPollDelayInMilliseconds(250);
            ConfigurationChangeFileWatcher.SetDefaultPollDelayInMilliseconds(250);
        }

        [TestFixtureTearDown]
        public void ResetDefaultTimeouts()
        {
            ConfigurationChangedRegistryWatcher.ResetDefaultPollDelay();   
            ConfigurationChangeSqlWatcher.ResetDefaultPollDelay();
            ConfigurationChangeFileWatcher.ResetDefaultPollDelay();
        }

		[SetUp]
		public void SetUp()
		{
			defaultConfigData = new ItemDetails();

			defaultConfigData.Name = "Toy";
			defaultConfigData.Price = 5.44M;
			defaultConfigData.Quantity = 3;

			ConfigurationManager.WriteConfiguration("RegistryWatcherTest", defaultConfigData);
			ConfigurationManager.WriteConfiguration("XmlWatcherTest", defaultConfigData);
			ConfigurationManager.WriteConfiguration("SqlWatcherTest", defaultConfigData);
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));

			count = 0;

			ConfigurationManager.ConfigurationChanged += new ConfigurationChangedEventHandler(OnConfigurationChanged); 
		}

		[TearDown]
		public void TearDown()
		{
			ConfigurationManager.ConfigurationChanged -= new ConfigurationChangedEventHandler(OnConfigurationChanged); 

			count = 0;
		}

		[Test]
		public void WriteRegistryWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("RegistryWatcherTest",configData);
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(1, count);
		}

		[Test]
		public void ReadRegistryWatch()
		{
			ItemDetails configData = new ItemDetails();
					
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("RegistryWatcherTest");
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(0, count);
		}

		[Test]
		public void WriteReadRegistryWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("RegistryWatcherTest",configData);
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("RegistryWatcherTest");
			
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(1, count);
		}

		[Test]
		public void ReadWriteRegistryWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("RegistryWatcherTest");
			
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("RegistryWatcherTest",configData);
			
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(1, count);
		}

   		[Test]
		public void ReadTwiceRegistryWatch()
		{
			ItemDetails configData = new ItemDetails();
			
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("RegistryWatcherTest");
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("RegistryWatcherTest");
			
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(0, count);
		}

        [Test]
		public void WriteTwiceRegistryWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("RegistryWatcherTest",configData);
						
			configData.Name="VSTO";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("RegistryWatcherTest",configData);

            Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(2, count);
		}

		[Test]
		public void WriteXmlWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("XmlWatcherTest",configData);
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(1, count);
		}

		[Test]
		public void ReadXmlWatch()
		{
			ItemDetails configData = new ItemDetails();
					
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("XmlWatcherTest");
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(0, count);
		}

		[Test]
		public void WriteReadXmlWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("XmlWatcherTest",configData);
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("XmlWatcherTest");
			
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(1, count);
		}

		[Test]
		public void ReadWriteXmlWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("XmlWatcherTest");
			
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("XmlWatcherTest",configData);
			
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(1, count);
		}

		[Test]
		public void ReadTwiceXmlWatch()
		{
			ItemDetails configData = new ItemDetails();
			
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("XmlWatcherTest");
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("XmlWatcherTest");
			
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(0, count);
		}

		[Test]
		public void WriteTwiceXmlWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("XmlWatcherTest",configData);
						
			configData.Name="VSTO";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("XmlWatcherTest",configData);

			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(2, count);
		}
  
		[Test]
		public void WriteSqlWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("SqlWatcherTest",configData);
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(1, count);
		}
		
		[Test]
		public void ReadSqlWatch()
		{
			ItemDetails configData = new ItemDetails();
					
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("SqlWatcherTest");
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(0, count);
		}

		[Test]
		public void WriteReadSqlWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("SqlWatcherTest",configData);
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("SqlWatcherTest");
			
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(1, count);
		}

		[Test]
		public void ReadWriteSqlWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("SqlWatcherTest");
			
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("SqlWatcherTest",configData);
			
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(1, count);
		}

 		[Test]
		public void ReadTwiceSqlWatch()
		{
			ItemDetails configData = new ItemDetails();
			
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("SqlWatcherTest");
			configData = (ItemDetails) ConfigurationManager.GetConfiguration("SqlWatcherTest");
			
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(0, count);
		}
 
		[Test]
		public void WriteTwiceSqlWatch()
		{
			ItemDetails configData = new ItemDetails();
			configData.Name="Whidbey Beta 2";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("SqlWatcherTest",configData);
						
			configData.Name="VSTO";
			configData.Price=0;
			configData.Quantity= 4;

			ConfigurationManager.WriteConfiguration("SqlWatcherTest",configData);

			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			Assert.AreEqual(2, count);
		}

		[Test]
		public void UnregisterEventCheckFileWatcherNotWorkingTest()
		{
			ConfigurationManager.ConfigurationChanged -= new ConfigurationChangedEventHandler(OnConfigurationChanged); 

			ItemDetails configData = new ItemDetails();
			configData.Name = "Toy";
			configData.Price = 5.44M;
			configData.Quantity = 3;

			ConfigurationManager.WriteConfiguration("RegistryWatcherTest",configData);
			Thread.Sleep(new TimeSpan(0, 0, 0, 1, 0));
			
			// reset config in file
			ConfigurationManager.WriteConfiguration("RegistryWatcherTest",defaultConfigData);
		}

		private void OnConfigurationChanged(object sender, ConfigurationChangedEventArgs args)
		{
			++count;
		}

		[Serializable]
		public class ItemDetails
		{
			public string Name;
			public decimal Price;
			public int Quantity;
		}
	}
}
#endif
