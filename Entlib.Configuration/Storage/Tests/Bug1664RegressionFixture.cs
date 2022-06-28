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
using Microsoft.Practices.EnterpriseLibrary.Configuration.Transformer;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage.Tests 
{
    [TestFixture]
    public abstract class Bug1664RegressionFixture 
    {
		protected const string voidSectionName = "Bug1664Void";
		protected const string emptySectionName = "Bug1664Empty";
		protected const string wrongSectionName = "Bug1664Wrong";
		protected const string sectionName = "Bug1664Ok";

		[Test]
		[ExpectedException(typeof(System.Configuration.ConfigurationException))]
		public void ReadConfigWhenVoidStoreShouldThrowException()
		{
			IStorageProviderReader reader = CreateStorageProvider();
			reader.CurrentSectionName = voidSectionName;
			reader.Initialize(new RuntimeConfigurationView(this.CreateConfigurationContext()));

			reader.Read();
		}

		[Test]
		[ExpectedException(typeof(System.Configuration.ConfigurationException))]
		public void ReadConfigWhenEmptyStoreShouldThrowException()
		{
			IStorageProviderReader reader = CreateStorageProvider();
			reader.CurrentSectionName = emptySectionName;
			reader.Initialize(new RuntimeConfigurationView(this.CreateConfigurationContext()));

			reader.Read();
		}

		[Test]
		[ExpectedException(typeof(System.Configuration.ConfigurationException))]
		public void ReadConfigWhenSectionNotInStoreShouldThrowException()
		{
			IStorageProviderReader reader = CreateStorageProvider();
			reader.CurrentSectionName = emptySectionName;
			reader.Initialize(new RuntimeConfigurationView(this.CreateConfigurationContext()));

			reader.Read();
		}

		[Test]
		public void ReadConfigWhenOkStoreShouldReturnNonNullValue()
		{
			IStorageProviderReader reader = CreateStorageProvider();
			reader.CurrentSectionName = sectionName;
			reader.Initialize(new RuntimeConfigurationView(this.CreateConfigurationContext()));

			object rawSectionData = reader.Read();

			Assert.IsNotNull(rawSectionData);
		}

		protected abstract IStorageProviderReader CreateStorageProvider();
		protected abstract ConfigurationContext CreateConfigurationContext();
    }
}
#endif