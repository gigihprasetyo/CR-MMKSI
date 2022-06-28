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
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Tests;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Transformer;
using Microsoft.Win32;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage.Tests  
{
    [TestFixture]
    public class RegistryStorageProviderFixture 
    {
        private const string sectionName = "RegistryTestConfig";
        private const string encryptedSectionName = "EncryptedRegistryTestConfig";
        private const string color = "Red";
        private const int size = 5;
        private const string text = "Some random text";

        private int changedEventCount;
        private ConfigurationData data;
        
        [TestFixtureSetUp]
        public void ShortenPollDelay()
        {
            ConfigurationChangedRegistryWatcher.SetDefaultPollDelayInMilliseconds(250);
        }

        [TestFixtureTearDown]
        public void ResetPollDelay()
        {
            ConfigurationChangedRegistryWatcher.ResetDefaultPollDelay();
        }

        [SetUp]
        public void SetUp()
        {
            data = new ConfigurationData();;
            data.Color = color;
            data.Size = size;
            data.SomeText = text;
            changedEventCount = 0;
        }

        [Test]
        public void WriteConfigurationDataTest()
        {
            DoWrite(sectionName);
        }
        

        [Test]
        public void WriteEncryptedConfigurationDataTest()
        {
            DoWrite(encryptedSectionName);
        }

        [Test]
        public void ValidateThatTheChangeEventFires()
        {
			using (ConfigurationContext context = new ConfigurationContext(GetRegistryStorageConfig()))
			{
				context.WriteConfiguration(sectionName, data);
			
				context.ConfigurationChanged += new ConfigurationChangedEventHandler(OnConfigurationChanged);
				Thread.Sleep(500);
				OpenAndWriteToRegistryKey(sectionName);
				Thread.Sleep(1000);
				context.ConfigurationChanged -= new ConfigurationChangedEventHandler(OnConfigurationChanged);
				Assert.IsTrue(changedEventCount > 0);
			}
        }

    	private void WriteValue()
    	{
			Thread.Sleep(500);
    		OpenAndWriteToRegistryKey(sectionName);
    		Debug.WriteLine("Written");
    	}

    	private static string GetRegistryStorageConfig()
		{
			return Path.GetFullPath("registrystorage.config");
		}

        private void OpenAndWriteToRegistryKey(string sectionName)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\EnterpriseLibrary\" + sectionName, true))
            {
                key.SetValue("value", "foobar" + DateTime.Now.Ticks.ToString());
            }
        }

        private void DoWrite(string sectionName)
        {
            RegistryStorageProvider provider = new RegistryStorageProvider();
            provider.CurrentSectionName = sectionName;
            provider.Initialize(new RuntimeConfigurationView(ConfigurationManager.GetCurrentContext()));
            XmlSerializerTransformer transformer = new XmlSerializerTransformer();
            object serializedData = transformer.Serialize(data);
            provider.Write(serializedData);
            ValidateReadData();
        }

        private void ValidateReadData()
        {
            ConfigurationData readData = (ConfigurationData)ConfigurationManager.GetConfiguration(sectionName);
            Assert.IsNotNull(readData);
            Assert.AreEqual(data.Color, readData.Color);
            Assert.AreEqual(data.Size, readData.Size);
            Assert.AreEqual(data.SomeText, readData.SomeText);
        }

        private void OnConfigurationChanged(object sender, ConfigurationChangedEventArgs e)
        {
            changedEventCount++;
        }
    }
}
#endif