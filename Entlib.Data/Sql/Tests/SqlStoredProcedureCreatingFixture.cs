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
using Microsoft.Practices.EnterpriseLibrary.Data.Tests;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Sql.Tests
{
    [TestFixture]
    public class SqlStoredProcedureCreatingFixture : StoredProcedureCreatingFixture
    {
        [SetUp]
        public override void SetUp()
        {
            using (TestConfigurationContext context = new TestConfigurationContext())
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory(context);
                db = factory.CreateDefaultDatabase();
            }
            base.SetUp();
        }

        protected override void CreateStoredProcedure()
        {
            string storedProcedureCreation = "CREATE procedure [TestProc] " +
                "(@vCount int output, @vCustomerId varchar(15)) AS " +
                "set @vCount = (select count(*) from Orders where CustomerId = @vCustomerId)";

            DBCommandWrapper command = this.db.GetSqlStringCommandWrapper(storedProcedureCreation);
            this.db.ExecuteNonQuery(command);
        }

        protected override void DeleteStoredProcedure()
        {
            string storedProcedureDeletion = "Drop procedure TestProc";
            DBCommandWrapper command = this.db.GetSqlStringCommandWrapper(storedProcedureDeletion);
            this.db.ExecuteNonQuery(command);
        }
    }
}

#endif