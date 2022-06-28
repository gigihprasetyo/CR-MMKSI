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
using System.Data.SqlClient;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage.Tests 
{
	[TestFixture]
	public class Bug1664RegressiosSqlStorageProviderFixture : Bug1664RegressionFixture
	{
		private const string ConnectionString = "server=localhost;database=EntLibExtensions;Integrated Security=true";
		private const string GetConfiguration = "EntLib_GetConfig";
		private const string SetConfiguration = "EntLib_SetConfig";
		private const string SectionDataTemplate = "<?xml version=\"1.0\" encoding=\"utf-8\"?><{0}><xmlSerializerSection type=\"Microsoft.Practices.EnterpriseLibrary.Configuration.Tests.ConfigurationData, Microsoft.Practices.EnterpriseLibrary.Configuration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\"><ConfigurationData xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><Color>Red</Color><SomeText>Some random text</SomeText><Size>5</Size></ConfigurationData></xmlSerializerSection></{0}>";

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
			
				SqlCommand command = null;
				SqlParameter sectionNameParameter = null;
				SqlParameter sectionValueParameter = null;
				
				command = new SqlCommand(string.Format("delete Configuration_Parameter where section_name = '{0}'", voidSectionName), connection);
				command.CommandType = CommandType.Text;
				command.ExecuteNonQuery();

				command = new SqlCommand(SetConfiguration, connection);
				command.CommandType = CommandType.StoredProcedure;
				sectionNameParameter = new SqlParameter( @"@section_name", SqlDbType.NVarChar );
				sectionNameParameter.Value = emptySectionName;
				command.Parameters.Add(sectionNameParameter);
				sectionValueParameter = new SqlParameter( @"@section_value", SqlDbType.NText );
				sectionValueParameter.Value = "";
				command.Parameters.Add(sectionValueParameter);
				command.ExecuteNonQuery();

				command = new SqlCommand(SetConfiguration, connection);
				command.CommandType = CommandType.StoredProcedure;
				sectionNameParameter = new SqlParameter( @"@section_name", SqlDbType.NVarChar );
				sectionNameParameter.Value = wrongSectionName;
				command.Parameters.Add(sectionNameParameter);
				sectionValueParameter = new SqlParameter( @"@section_value", SqlDbType.NText );
				sectionValueParameter.Value = string.Format(SectionDataTemplate, sectionName);
				command.Parameters.Add(sectionValueParameter);
				command.ExecuteNonQuery();

				command = new SqlCommand(SetConfiguration, connection);
				command.CommandType = CommandType.StoredProcedure;
				sectionNameParameter = new SqlParameter( @"@section_name", SqlDbType.NVarChar );
				sectionNameParameter.Value = sectionName;
				command.Parameters.Add(sectionNameParameter);
				sectionValueParameter = new SqlParameter( @"@section_value", SqlDbType.NText );
				sectionValueParameter.Value = string.Format(SectionDataTemplate, sectionName);
				command.Parameters.Add(sectionValueParameter);
				command.ExecuteNonQuery();
			} 
		}

		[TestFixtureTearDown]
		public void FixtureTearDown()
		{
		}

		protected override IStorageProviderReader CreateStorageProvider()
		{
			return new SqlStorageProvider();
		}

		protected override ConfigurationContext CreateConfigurationContext()
		{
			ConfigurationDictionary dictionary = new ConfigurationDictionary();
			ConfigurationSettings settings = new ConfigurationSettings();
			dictionary.Add(ConfigurationSettings.SectionName, settings);

			settings.ConfigurationSections.Add(new ConfigurationSectionData(voidSectionName, false, 
				new SqlStorageProviderData(voidSectionName, ConnectionString, GetConfiguration, SetConfiguration), 
				new XmlSerializerTransformerData(voidSectionName)));

			settings.ConfigurationSections.Add(new ConfigurationSectionData(emptySectionName, false, 
				new SqlStorageProviderData(emptySectionName, ConnectionString, GetConfiguration, SetConfiguration), 
				new XmlSerializerTransformerData(emptySectionName)));

			settings.ConfigurationSections.Add(new ConfigurationSectionData(wrongSectionName, false, 
				new SqlStorageProviderData(wrongSectionName, ConnectionString, GetConfiguration, SetConfiguration), 
				new XmlSerializerTransformerData(wrongSectionName)));

			settings.ConfigurationSections.Add(new ConfigurationSectionData(sectionName, false, 
				new SqlStorageProviderData(sectionName, ConnectionString, GetConfiguration, SetConfiguration), 
				new XmlSerializerTransformerData(sectionName)));

			return new ConfigurationContext(dictionary);
		}

	}
}
#endif