//===============================================================================
// Microsoft patterns & practices Enterprise Library
// XXXXX Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

#if UNIT_TESTS
using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Tests 
{
	[TestFixture]
	public class OracleBugFixingRegressionFixture
	{
		const string OracleTestStoredProcedureInPackageWithTranslation = "TESTPACKAGETOTRANSLATEGETCUSTOMERDETAILS";
		// TODO verify the expected behavior for this feature
		// const string OracleTestTranslatedStoredProcedureInPackageWithTranslation = "TESTPACKAGE.GETCUSTOMERDETAILS";
		const string OracleTestTranslatedStoredProcedureInPackageWithTranslation = "TESTPACKAGE.TESTPACKAGETOTRANSLATEGETCUSTOMERDETAILS";
		const string OracleTestStoredProcedureInPackageWithoutTranslation = "TESTPACKAGETOKEEPGETCUSTOMERDETAILS";
		const string OracleTestPackage1Prefix = "TESTPACKAGETOTRANSLATE";
		const string OracleTestPackage1Name = "TESTPACKAGE";
		const string OracleTestPackage2Prefix = "TESTPACKAGETOTRANSLATE2";
		const string OracleTestPackage2Name = "TESTPACKAGE2";

		private Database db;

		[SetUp]
		public void SetUp()
		{
			using (ConfigurationContext context = new ConfigurationContext(GenerateConfigurationDictionary()))
			{
				DatabaseProviderFactory factory = new DatabaseProviderFactory(context);
				this.db = factory.CreateDatabase("OracleTest");
			}
		}

		[Test]
		public void CommandTextWithConfiguredPackageTranslationsShouldBeTranslatedToTheCorrectPackageBug1572() 
		{
			DBCommandWrapper dBCommandWrapper = this.db.GetStoredProcCommandWrapper(OracleTestStoredProcedureInPackageWithTranslation);

			Assert.AreEqual(OracleTestTranslatedStoredProcedureInPackageWithTranslation, dBCommandWrapper.Command.CommandText);
		}

		[Test]
		public void CommandTextWithoutConfiguredPackageTranslationsShouldNotBeTranslatedBug1572() 
		{
			DBCommandWrapper dBCommandWrapper = this.db.GetStoredProcCommandWrapper(OracleTestStoredProcedureInPackageWithoutTranslation);

			Assert.AreEqual(OracleTestStoredProcedureInPackageWithoutTranslation, dBCommandWrapper.Command.CommandText);
		}

		[Test]
		public void RountripGuidParametersKeepTheCorrectTypeBug1574()
		{
			string parameterName = "dummyParameter";
			byte[] guidBytes = new byte[16];

			DBCommandWrapper dBCommandWrapper = this.db.GetStoredProcCommandWrapper("IGNORED");
			dBCommandWrapper.AddInParameter(parameterName, DbType.Guid);
			dBCommandWrapper.SetParameterValue(parameterName, new Guid(guidBytes));
			object paramValue = dBCommandWrapper.GetParameterValue(parameterName);

			Assert.IsNotNull(paramValue);
			Assert.AreSame(typeof(Guid), paramValue.GetType());
		}

		[Test]
		public void RountripToDatabaseGuidParametersKeepTheCorrectTypeBug1574()
		{
			Guid guid = new Guid(new byte[16]);
			object outputParamValue = null;

			DBCommandWrapper dBCommandWrapper = this.db.GetStoredProcCommandWrapper("SetAndGetGuid");
			dBCommandWrapper.AddOutParameter("outputGuid", DbType.Guid, 0);
			dBCommandWrapper.AddInParameter("inputGuid", DbType.Guid);
			dBCommandWrapper.SetParameterValue("inputGuid", guid);

			try
			{
				CreateStoredProcedure();
				db.ExecuteNonQuery(dBCommandWrapper);
				outputParamValue = dBCommandWrapper.GetParameterValue("outputGuid");
			}
			finally
			{
				DeleteStoredProcedure();
			}

			Assert.IsNotNull(outputParamValue);
			Assert.IsFalse(outputParamValue == DBNull.Value);
			Assert.AreSame(typeof(Guid), outputParamValue.GetType());
			Assert.AreEqual(guid, outputParamValue);
		}

		#region private setup methods

		private static ConfigurationDictionary GenerateConfigurationDictionary()
		{
			ConfigurationDictionary dictionary = new ConfigurationDictionary();
			dictionary.Add(DatabaseSettings.SectionName, GenerateDataSettings());

			return dictionary;
		}

		private static DatabaseSettings GenerateDataSettings()
		{
			DatabaseSettings settings = new DatabaseSettings();

			settings.DefaultInstance = "OracleTest";

			settings.DatabaseTypes.Add( new DatabaseTypeData("Oracle", typeof(OracleDatabase).AssemblyQualifiedName));

			OracleConnectionStringData data = new OracleConnectionStringData("OracleTest");
			data.Parameters.Add(new ParameterData("server","entlib"));
			data.Parameters.Add(new ParameterData("user id","testuser"));
			data.Parameters.Add(new ParameterData("password","testuser"));
			data.OraclePackages.Add(new OraclePackageData(OracleTestPackage1Name, OracleTestPackage1Prefix));
			data.OraclePackages.Add(new OraclePackageData(OracleTestPackage2Name, OracleTestPackage2Prefix));
			settings.ConnectionStrings.Add(data);

			settings.Instances.Add( new InstanceData("OracleTest", "Oracle", "OracleTest"));

			return settings;
		}

		private void CreateStoredProcedure()
		{
			string storedProcedureCreation = "CREATE OR REPLACE PROCEDURE SetAndGetGuid(outputGuid OUT RAW, inputGuid IN RAW) AS " +
				"BEGIN SELECT inputGuid INTO outputGuid FROM DUAL; END;";

			DBCommandWrapper command = this.db.GetSqlStringCommandWrapper(storedProcedureCreation);
			this.db.ExecuteNonQuery(command);
		}

		private void DeleteStoredProcedure()
		{
			string storedProcedureDeletion = "Drop procedure SetAndGetGuid";
			DBCommandWrapper command = this.db.GetSqlStringCommandWrapper(storedProcedureDeletion);
			this.db.ExecuteNonQuery(command);
		}


		#endregion
	}
}
#endif