//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

#if  UNIT_TESTS
using Microsoft.Practices.EnterpriseLibrary.Data.Tests;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Tests
{
    [TestFixture]
    public class OracleUpdateDataSetWithTransactionsAndParameterDiscovery : UpdateDataSetWithTransactionsAndParameterDiscovery
    {
        [TestFixtureSetUp]
        public void Initialize()
        {
            using (TestConfigurationContext context = new TestConfigurationContext())
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory(context);
                this.db = factory.CreateDatabase("OracleTest");
            }
            try
            {
                DeleteStoredProcedures();
            }
            catch
            {
            }
            CreateStoredProcedures();
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            DeleteStoredProcedures();
        }

        protected override void CreateStoredProcedures()
        {
            OracleDataSetHelper.CreateStoredProcedures(db);
        }

        protected override void DeleteStoredProcedures()
        {
            OracleDataSetHelper.DeleteStoredProcedures(db);
        }

        protected override void CreateDataAdapterCommands()
        {
            OracleDataSetHelper.CreateDataAdapterCommandsDynamically(db, ref insertCommand, ref updateCommand, ref deleteCommand);
        }

        protected override void AddTestData()
        {
            OracleDataSetHelper.AddTestData(db);
        }
    }
}

#endif