//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

#if  UNIT_TESTS
using System.Configuration;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Tests
{
    [TestFixture]
    public class DatabaseFactoryFixture
    {
        [Test]
        public void CanCreateDefaultDatabase()
        {
            using (TestConfigurationContext context = new TestConfigurationContext())
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory(context);
                Database db = factory.CreateDefaultDatabase();
                Assert.IsNotNull(db);
            }
        }

        [Test]
        public void CanGetDatabaseByName()
        {
            using (TestConfigurationContext context = new TestConfigurationContext())
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory(context);
                Database db = factory.CreateDatabase("NewDatabase");
                Assert.IsNotNull(db);
            }
        }

        [Test]
        public void CallingTwiceReturnsDifferenceDatabaseInstances()
        {
            using (TestConfigurationContext context = new TestConfigurationContext())
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory(context);
                Database firstDb = factory.CreateDatabase("NewDatabase");
                Database secondDb = factory.CreateDatabase("NewDatabase");

                Assert.IsFalse(firstDb == secondDb);
            }
        }

        [ExpectedException(typeof(ConfigurationException))]
        [Test]
        public void ExceptionThrownWhenAskingForDatabaseWithUnknownName()
        {
            using (TestConfigurationContext context = new TestConfigurationContext())
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory(context);
                Database db = factory.CreateDatabase("ThisIsAnUnknownKey");
                Assert.IsNotNull(db);
            }
        }
    }
}

#endif