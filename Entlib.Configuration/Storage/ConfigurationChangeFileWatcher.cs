//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Configuration Application Block
//===============================================================================
// Copyright ? Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.IO;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage
{
	/// <summary>
	/// <para>Represents an <see cref="IConfigurationChangeWatcher"/> that watches a file.</para>
	/// </summary>
	public class ConfigurationChangeFileWatcher : ConfigurationChangeWatcher, IConfigurationChangeWatcher
	{
		private static readonly string eventSourceName = SR.FileWatcherEventSource;
		private string configurationSectionName;
		private string configFilePath;

		/// <summary>
		/// <para>Initialize a new <see cref="ConfigurationChangeFileWatcher"/> class with the path to the configuration file and the name of the section</para>
		/// </summary>
		/// <param name="configFilePath">
		/// <para>The full path to the configuration file.</para>
		/// </param>
		/// <param name="configurationSectionName">
		/// <para>The name of the configuration section to watch.</para>
		/// </param>
		public ConfigurationChangeFileWatcher(string configFilePath, string configurationSectionName)
		{
			// TODO check this for EntLib 1.1
			//ArgumentValidation.CheckForNullReference(configFilePath, "configFilePath");
			//ArgumentValidation.CheckForNullReference(configurationSectionName, "configurationSectionName");

			this.configurationSectionName = configurationSectionName;
			this.configFilePath = configFilePath;
		}

		/// <summary>
		/// <para>Allows an <see cref="ConfigurationChangeFileWatcher"/> to attempt to free resources and perform other cleanup operations before the <see cref="ConfigurationChangeFileWatcher"/> is reclaimed by garbage collection.</para>
		/// </summary>
		~ConfigurationChangeFileWatcher()
		{
			Disposing(false);
		}

		/// <summary>
		/// <para>Gets the name of the configuration section being watched.</para>
		/// </summary>
		/// <value>
		/// <para>The name of the configuration section being watched.</para>
		/// </value>
		public override string SectionName
		{
			get { return configurationSectionName; }
		}

		/// <summary>
		/// <para>Returns the <see cref="DateTime"/> of the last change of the information watched</para>
		/// <para>The information is retrieved using the watched file modification timestamp</para>
		/// </summary>
		/// <returns>The <see cref="DateTime"/> of the last modificaiton, or <code>DateTime.MinValue</code> if the information can't be retrieved</returns>
		protected override DateTime GetCurrentLastWriteTime()
		{
			if (File.Exists(configFilePath) == true)
			{
				return File.GetLastWriteTime(configFilePath);
			}
			else
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>
		/// Returns the string that should be assigned to the thread used by the watcher
		/// </summary>
		/// <returns>The name for the thread</returns>
		protected override string BuildThreadName()
		{
			return "_ConfigurationFileWatherThread : " + configFilePath;
		}

		/// <summary>
		/// Builds the change event data, including the full path of the watched file
		/// </summary>
		/// <returns>The change event information</returns>
		protected override ConfigurationChangedEventArgs BuildEventData()
		{
			return new ConfigurationChangedEventArgs(Path.GetFullPath(configFilePath), configurationSectionName);
		}

		/// <summary>
		/// Returns the source name to use when logging events
		/// </summary>
		/// <returns>The event source name</returns>
		protected override string GetEventSourceName()
		{
			return eventSourceName;
		}
	}
}