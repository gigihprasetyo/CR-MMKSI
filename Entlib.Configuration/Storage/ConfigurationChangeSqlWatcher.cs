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

using System;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage
{
	/// <summary>
	/// <para>Represents a <see cref="IConfigurationChangeWatcher"/> for a <see cref="SqlStorageProvider"/>.</para>
	/// </summary>
	public class ConfigurationChangeSqlWatcher : ConfigurationChangeWatcher, IConfigurationChangeWatcher
	{
		private static readonly string eventSourceName = SR.SqlWatcherEventSource;
		private string configurationSectionName;
		private string connectionString;
		private string getConfig;
		private string setConfig;

		/// <summary>
		/// <para>Initialize a new instance of the <see cref="ConfigurationChangeSqlWatcher"/> class with a connection string, the name of the stored procedure to get the configuration data, the name of the stored procedure to set the configuration data, and the configuration section name.</para>
		/// </summary>
		/// <param name="connectionString">
		/// <para>The connection string for the SQL Server database that is being watched.</para>
		/// </param>
		/// <param name="getStoredProcedure">
		/// <para>The name of the stored procedure to get the configuration data.</para>
		/// </param>
		/// <param name="setStoredProcedure">
		/// <para>The name of the stored procedure to set the configuration data.</para></param>
		/// <param name="configurationSectionName">
		/// <para>The name of the configuration section to watch.</para>
		/// </param>
		public ConfigurationChangeSqlWatcher(string connectionString, string getStoredProcedure, string setStoredProcedure, string configurationSectionName)
			: base()
		{
			this.configurationSectionName = configurationSectionName;
			this.connectionString = connectionString;
			this.getConfig = getStoredProcedure;
			this.setConfig = setStoredProcedure;
		}

		/// <summary>
		/// <para>Releases the unmanaged resources used by the <see cref="ConfigurationChangeSqlWatcher"/> and optionally releases the managed resources.</para>
		/// </summary>
		~ConfigurationChangeSqlWatcher()
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
		/// </summary>
		/// <returns>The <see cref="DateTime"/> of the last modificaiton, or <code>DateTime.MinValue</code> if the information can't be retrieved</returns>
		protected override DateTime GetCurrentLastWriteTime()
		{
			using (SqlConnection myConnection = new SqlConnection(this.connectionString))
			{
				DateTime currentLastWriteTime = DateTime.MinValue;

				SqlCommand myCommand = new SqlCommand(this.getConfig, myConnection);
				myCommand.CommandType = CommandType.StoredProcedure;

				SqlParameter parameterSectionName = new SqlParameter(@"@SectionName", SqlDbType.NVarChar);
				parameterSectionName.Value = this.SectionName;
				myCommand.Parameters.Add(parameterSectionName);

				// Execute the command
				myConnection.Open();

				using( SqlDataReader reader = myCommand.ExecuteReader() )
				{
					if( reader.Read() )
					{
						currentLastWriteTime = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1);
					}
				}
				return currentLastWriteTime;
			}
		}

		/// <summary>
		/// Returns the string that should be assigned to the thread used by the watcher
		/// </summary>
		/// <returns>The name for the thread</returns>
		protected override string BuildThreadName()
		{
			return string.Empty;
		}

		/// <summary>
		/// Builds the change event data, in a suitable way for the specific watcher implementation
		/// </summary>
		/// <returns>The change event information</returns>
		protected override ConfigurationChangedEventArgs BuildEventData()
		{
			return new ConfigurationSqlChangedEventArgs(connectionString,getConfig, setConfig, configurationSectionName);
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