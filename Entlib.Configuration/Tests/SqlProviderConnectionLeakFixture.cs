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
using System.Threading;
using NUnit.Framework;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Tests;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage.Tests 
{
	[TestFixture]
	public class SqlProviderConnectionLeakFixture 
	{
		private const string sectionName = "SqlTestConfig";
		private const string connectionString = "server=localhost;database=EntLibExtensions;Integrated Security=true";
		private const string getConfig = "EntLib_GetConfig";
		private const string setConfig = "EntLib_SetConfig";

		private ConfigurationBuilder builder;

		[SetUp]
		public void SetUp()
		{
			builder = CreateConfigurationBuilder();
		}

		[TearDown]
		public void TearDown()
		{
			if (builder != null)
			{
				builder.Dispose();
			}
		}

		[Test]
		public void TestLeak()
		{
			StressLoop(new State(0, new ManualResetEvent(false)));
		}

		[Test]
		public void TestLeakMultiThreaded()
		{
			ManualResetEvent[] events = new ManualResetEvent[50];

			for (int i = 0; i < 50; i++)
			{
				events[i] = new ManualResetEvent(false);
				ThreadPool.QueueUserWorkItem(new WaitCallback(StressLoop), new State(i, events[i]));
			}

			//WaitHandle.WaitAll(events);
			Thread.Sleep(50000);
		}

		private void StressLoop(object data)
		{
			State state = (State) data;

			try 
			{
				for (int i = 0; i < 100; i++)
				{
					Console.WriteLine("{0} - {1}", state.index, i);
					MockConfigurationData configData = builder.ReadConfiguration(sectionName) as MockConfigurationData;

					builder.ClearSectionCache(sectionName);
				
					Thread.Sleep(200);
				}
			} 
			finally
			{
				state.manualEvent.Set();
			}
		}

		private ConfigurationBuilder CreateConfigurationBuilder()
		{
			ConfigurationSectionData sectionData = new ConfigurationSectionData(sectionName);
			sectionData.StorageProvider = new SqlStorageProviderData(sectionName, connectionString, getConfig, setConfig);
			sectionData.Transformer = new XmlSerializerTransformerData(sectionName);
			
			ConfigurationSettings settings = new ConfigurationSettings();
			settings.ConfigurationSections.Add(sectionData);

			return new ConfigurationBuilder(settings);
		}

	}

	class State
	{
		public int index;
		public ManualResetEvent manualEvent;

		public State(int index, ManualResetEvent manualEvent)
		{
			this.index = index;
			this.manualEvent = manualEvent;
		}
	}
}
#endif