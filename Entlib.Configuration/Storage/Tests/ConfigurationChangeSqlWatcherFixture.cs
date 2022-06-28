//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Configuration Application Block
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
using System.Threading;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage.Tests
{
	[TestFixture]
	public class ConfigurationChangeSqlWatcherFixture
	{
		protected string configurationSectionName = "DummySection";
		protected string differentConfigurationSectionName = "DifferentSection";
		protected string connectionString = "server=localhost;database=EntLibExtensions;Integrated Security=true";
		protected string getConfig = "EntLib_GetConfig";
		protected string setConfig = "EntLib_SetConfig";

		[SetUp]
		public void SetUp()
		{
			pollingException = null;
			configurationChangeCounter = 0;

			ResetConfigurationInDatabase(this.configurationSectionName, null);
		}

		[TearDown]
		public void TearDown()
		{
			DeleteConfigurationSectionInDatabase(configurationSectionName);
			DeleteConfigurationSectionInDatabase(differentConfigurationSectionName);
		}

		[Test]
		public void NotifiesNothingIfNoOneListeningAndSourceChanges()
		{
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, configurationSectionName))
			{
				watcher.SetPollDelayInMilliseconds(100);
				watcher.StartWatching();

				ResetConfigurationInDatabase(this.configurationSectionName, DateTime.Now + TimeSpan.FromHours(1.0));
				Thread.Sleep(250);
			}
		}

		[Test]
		public void NotifiesSingleListenerIfSourceChanges()
		{
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, configurationSectionName))
			{
				watcher.SetPollDelayInMilliseconds(100);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.StartWatching();
				Thread.Sleep(100);

				ResetConfigurationInDatabase(this.configurationSectionName, DateTime.Now + TimeSpan.FromHours(1.0));
				Thread.Sleep(250);
			}

			Assert.AreEqual(1, configurationChangeCounter);
			Assert.AreEqual(configurationSectionName, storedEventArgs.SectionName);
		}

		[Test]
		public void NotifiesMultipleListenersIfSourceChanges()
		{
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, configurationSectionName))
			{
				watcher.SetPollDelayInMilliseconds(100);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.StartWatching();
				Thread.Sleep(100);

				ResetConfigurationInDatabase(this.configurationSectionName, DateTime.Now + TimeSpan.FromHours(1.0));
				Thread.Sleep(250);
			}

			Assert.AreEqual(2, configurationChangeCounter);
		}

		[Test]
		public void WillStopNotifyingAfterObjectIsDisposed()
		{
			ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, configurationSectionName);
			watcher.SetPollDelayInMilliseconds(100);
			watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
			watcher.StartWatching();
			Thread.Sleep(100);
			watcher.Dispose();

			ResetConfigurationInDatabase(this.configurationSectionName, DateTime.Now + TimeSpan.FromHours(1.0));
			Thread.Sleep(250);

			Assert.AreEqual(0, configurationChangeCounter);
		}

		[Test]
		public void StopsNotifyingHandlersWhenAnyHandlerThrowsException()
		{
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, configurationSectionName))
			{
				watcher.SetPollDelayInMilliseconds(100);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(ExceptionThrowingCallback);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.StartWatching();
				Thread.Sleep(100);

				ResetConfigurationInDatabase(this.configurationSectionName, DateTime.Now + TimeSpan.FromHours(1.0));
				Thread.Sleep(250);
			}

			Assert.AreEqual(1, configurationChangeCounter);
		}

		[Test]
		public void WillStopCallingHandlerAfterThatHandlerHasBeenRemoved()
		{
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, configurationSectionName))
			{
				watcher.SetPollDelayInMilliseconds(100);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(DifferentSourceChangedCallback);
				watcher.StartWatching();
				Thread.Sleep(100);

				watcher.ConfigurationChanged -= new ConfigurationChangedEventHandler(DifferentSourceChangedCallback);

				ResetConfigurationInDatabase(this.configurationSectionName, DateTime.Now + TimeSpan.FromHours(1.0));
				Thread.Sleep(250);
			}

			Assert.AreEqual(1, configurationChangeCounter);
		}

		[Test]
		public void NoCallbackWillHappenAfterLastEventHandlerIsRemoved()
		{
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, configurationSectionName))
			{
				watcher.SetPollDelayInMilliseconds(100);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(DifferentSourceChangedCallback);
				watcher.StartWatching();
				watcher.ConfigurationChanged -= new ConfigurationChangedEventHandler(DifferentSourceChangedCallback);

				ResetConfigurationInDatabase(this.configurationSectionName, DateTime.Now + TimeSpan.FromHours(1.0));
				Thread.Sleep(250);
			}

			Assert.AreEqual(0, configurationChangeCounter);
		}

		[Test]
		public void NoCallbackWillHappenOnSourceDelete()
		{
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, configurationSectionName))
			{
				watcher.SetPollDelayInMilliseconds(100);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.StartWatching();

				DeleteConfigurationSectionInDatabase(configurationSectionName);

				Thread.Sleep(250);
			}

			Assert.AreEqual(0, configurationChangeCounter);
		}

		[Test]
		public void CreatingNewWatchedSourceDoesNotCauseCallback()
		{
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, differentConfigurationSectionName))
			{
				watcher.SetPollDelayInMilliseconds(100);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.StartWatching();
			
				Thread.Sleep(50);
				ResetConfigurationInDatabase(differentConfigurationSectionName, null);
				Thread.Sleep(250);
			}
			
			Assert.AreEqual(0, configurationChangeCounter);
		}

		[Test]
		public void OverwritingExistingWatchedSourceCausesOnlyOneCallback()
		{
			ResetConfigurationInDatabase(differentConfigurationSectionName, null);

			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, differentConfigurationSectionName))
			{
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.SetPollDelayInMilliseconds(100);
				watcher.StartWatching();
				Thread.Sleep(100);
		
				ChangeSource(differentConfigurationSectionName);
				Thread.Sleep(250);
			}
				
			Assert.AreEqual(1, configurationChangeCounter);
		}

		[Test]
		public void ChangesToNewlyCreatedWatchedSourceDoCauseCallback()
		{
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, differentConfigurationSectionName))
			{
				watcher.SetPollDelayInMilliseconds(100);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.StartWatching();
				Thread.Sleep(100);

				ResetConfigurationInDatabase(this.differentConfigurationSectionName, DateTime.Now + TimeSpan.FromHours(1.0));

				Thread.Sleep(150);

				ResetConfigurationInDatabase(this.differentConfigurationSectionName, DateTime.Now + TimeSpan.FromHours(1.0));

				Thread.Sleep(250);
			}

			//            File.Delete("Different.Test");

			Assert.AreEqual(1, configurationChangeCounter);
		}

		[Test]
		public void NotificationsDoNotAccumulateWhileCallbacksAreHappening()
		{
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, configurationSectionName))
			{
				watcher.SetPollDelayInMilliseconds(300);
				watcher.ConfigurationChanged += new ConfigurationChangedEventHandler(SourceChanged);
				watcher.StartWatching();
				Thread.Sleep(100);

				ChangeSource(configurationSectionName);
				Thread.Sleep(50);

				ChangeSource(configurationSectionName);
				Thread.Sleep(450);
			}

			Assert.AreEqual(1, configurationChangeCounter);
		}

		[Test]
		public void StoppingPollingDoesNotThrowException()
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(PollingThreadExceptionCatcher);
			using (ConfigurationChangeSqlWatcher watcher = new ConfigurationChangeSqlWatcher(connectionString, getConfig, setConfig, configurationSectionName))
			{
				watcher.StartWatching();
				Thread.Sleep(100);
				watcher.StopWatching();
				Thread.Sleep(1000);
			}
			AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(PollingThreadExceptionCatcher);

			Assert.IsNull(pollingException);
		}

		private void PollingThreadExceptionCatcher(object sender, UnhandledExceptionEventArgs e)
		{
			pollingException = (Exception)e.ExceptionObject;
		}

		private void SourceChanged(object sender, ConfigurationChangedEventArgs eventData)
		{
			configurationChangeCounter++;
			storedEventArgs = eventData;
		}

		private void ExceptionThrowingCallback(object sender, ConfigurationChangedEventArgs eventData)
		{
			throw new Exception();
		}

		private void DifferentSourceChangedCallback(object sender, ConfigurationChangedEventArgs eventData)
		{
			configurationChangeCounter++;
		}

		private void ChangeSource(string sourceName)
		{
			ResetConfigurationInDatabase(sourceName, null);
		}

		private void DeleteConfigurationSectionInDatabase(string sectionName)
		{
			SqlConnection connection = new SqlConnection(this.connectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("delete configuration_parameter where section_name = @section_name", connection);
				myCommand.CommandType = CommandType.Text;
		
				SqlParameter parameterSectionName = new SqlParameter(@"@section_name", SqlDbType.NVarChar);
				parameterSectionName.Value = sectionName;
				myCommand.Parameters.Add(parameterSectionName);

				connection.Open();

				myCommand.ExecuteNonQuery();
			}
			finally
			{
				connection.Close();
			}
		}
		private void ResetConfigurationInDatabase(string sectionName, object ignore)
		{
			SqlConnection connection = new SqlConnection(this.connectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand(this.setConfig, connection);
				myCommand.CommandType = CommandType.StoredProcedure;
		
				SqlParameter parameterSectionName = new SqlParameter(@"@section_name", SqlDbType.NVarChar);
				parameterSectionName.Value = sectionName;
				myCommand.Parameters.Add(parameterSectionName);
				SqlParameter parameterSectionValue = new SqlParameter(@"@section_value", SqlDbType.NVarChar);
				parameterSectionValue.Value = string.Empty;
				myCommand.Parameters.Add(parameterSectionValue);

				connection.Open();

				myCommand.ExecuteNonQuery();
			}
			finally
			{
				connection.Close();
			}
		}

		private int configurationChangeCounter;
		private ConfigurationChangedEventArgs storedEventArgs;
		private Exception pollingException;
	}
}

#endif