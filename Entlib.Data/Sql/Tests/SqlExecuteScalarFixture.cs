//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright ? Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

#if  UNIT_TESTS
using Microsoft.Practices.EnterpriseLibrary.Data.Tests;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Sql.Tests
{
    [TestFixture]
    public class SqlExecuteScalarFixture : ExecuteScalarFixture
    {
        [SetUp]
        public void SetUp()
        {
            using (TestConfigurationContext context = new TestConfigurationContext())
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory(context);
                this.db = factory.CreateDefaultDatabase();
            }

            this.sqlCommand = "Select count(*) from region";
            this.command = this.db.GetSqlStringCommandWrapper(this.sqlCommand);
        }
    }
}

#endif