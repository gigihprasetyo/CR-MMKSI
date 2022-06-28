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

#if  LONG_RUNNING_TESTS
using Microsoft.Practices.EnterpriseLibrary.Data.Tests;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Data.DB2.Tests
{
    [TestFixture]
    public class DB2ExecuteNonQueryFixture : ExecuteNonQueryFixture
    {
        [SetUp]
        public void SetUp()
        {
            using (Db2TestConfigurationContext context = new Db2TestConfigurationContext())
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory(context);
                this.db = factory.CreateDatabase("DB2Test");
                this.insertString = "insert into Region values (77, 'Elbonia')";
                this.insertionCommand = this.db.GetSqlStringCommandWrapper(this.insertString);

                this.countQuery = "select count(*) from Region";
                this.countCommand = this.db.GetSqlStringCommandWrapper(this.countQuery);
            }
        }

    }
}

#endif