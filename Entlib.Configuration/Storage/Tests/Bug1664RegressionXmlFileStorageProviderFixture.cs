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
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage.Tests 
{
	[TestFixture]
	public class Bug1664RegressionXmlFileStorageProviderFixture : Bug1664RegressionFixture
	{
		protected override IStorageProviderReader CreateStorageProvider()
		{
			return new XmlFileStorageProvider();
		}

		protected override ConfigurationContext CreateConfigurationContext()
		{
			ConfigurationDictionary dictionary = new ConfigurationDictionary();
			ConfigurationSettings settings = new ConfigurationSettings();
			dictionary.Add(ConfigurationSettings.SectionName, settings);

			settings.ConfigurationSections.Add(new ConfigurationSectionData(voidSectionName, false, 
				new XmlFileStorageProviderData(voidSectionName, voidSectionName + ".config"), 
				new XmlSerializerTransformerData(voidSectionName)));

			settings.ConfigurationSections.Add(new ConfigurationSectionData(emptySectionName, false, 
				new XmlFileStorageProviderData(emptySectionName, emptySectionName + ".config"), 
				new XmlSerializerTransformerData(emptySectionName)));

			settings.ConfigurationSections.Add(new ConfigurationSectionData(wrongSectionName, false, 
				new XmlFileStorageProviderData(wrongSectionName, sectionName + ".config"), 
				new XmlSerializerTransformerData(wrongSectionName)));

			settings.ConfigurationSections.Add(new ConfigurationSectionData(sectionName, false, 
				new XmlFileStorageProviderData(sectionName, sectionName + ".config"), 
				new XmlSerializerTransformerData(sectionName)));

			return new ConfigurationContext(dictionary);
		}

	}
}
#endif