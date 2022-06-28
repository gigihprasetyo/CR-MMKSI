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
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Tests;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Transformer;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage.Tests 
{
    [TestFixture]
    public class SqlStorageProviderFixture 
    {
        private const string sectionName = "SqlTestConfig";
        private const string color = "Red";
        private const int size = 5;
        private const string text = "Some random text";

        private ConfigurationData data;
        
        [SetUp]
        public void SetUp()
        {
            data = new ConfigurationData();;
            data.Color = color;
            data.Size = size;
            data.SomeText = text;
        }


        [Test]
        public void WriteConfigurationDataTest()
        {
            SqlStorageProvider provider = new SqlStorageProvider();
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
    }
}
#endif