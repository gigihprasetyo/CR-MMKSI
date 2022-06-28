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
using Microsoft.Practices.EnterpriseLibrary.Configuration;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage
{
    /// <summary>
    /// <para>Provides data for the <seealso cref="ConfigurationManager.ConfigurationChanging"/> and <see cref="ConfigurationContext.ConfigurationChanging"/> event which occur after configuration is changed and committed to storage for the <see cref="SqlStorageProvider"/>.</para>
    /// </summary>
    [Serializable]
    public class ConfigurationSqlChangingEventArgs : ConfigurationChangingEventArgs
    {
		private readonly string connectionString;
		private readonly string getStoredProcedure;
		private readonly string setStoredProcedure;

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="ConfigurationSqlChangingEventArgs"/> class the connection string to the SQL Server database, the stored procedure name to get the data, the stored procedure name to set the data, the configuration section name for this data, the old value and the new value.</para>
        /// </summary>
        /// <param name="connectionString">
        /// <para>The connection string to the SQL Server database.</para>
        /// </param>
        /// <param name="getStoredProcedure">
        /// <para>The stored procedure name to get the data.</para>
        /// </param>
        /// <param name="setStoredProcedure">
        /// <para>The stored procedure name to set the data.</para>
        /// </param>
        /// <param name="sectionName">
        /// <para>The name of the configuration section.</para>
        /// </param>
        /// <param name="oldValue"><para>The old value.</para></param>
        /// <param name="newValue"><para>The new value.</para></param>
		public ConfigurationSqlChangingEventArgs(string connectionString, string getStoredProcedure, string setStoredProcedure, string sectionName, object oldValue, object newValue) : base(String.Empty, sectionName, oldValue, newValue)
        {
			this.connectionString = connectionString;
			this.getStoredProcedure = getStoredProcedure;
			this.setStoredProcedure = setStoredProcedure;
		}

        /// <summary>
        /// <para>Gets the connection string to the SQL Server database.</para>
        /// </summary>
        /// <value>
        /// <para>The connection string to the SQL Server database.</para>
        /// </value>
		public string ConnectionString
		{
			get { return this.connectionString; }
		}

        /// <summary>
        /// <para>Gets the stored procedure name to get the data.</para>
        /// </summary>
        /// <value>
        /// <para>The stored procedure name to get the data.</para>
        /// </value>
		public string GetStoredProcedure
		{
			get { return this.getStoredProcedure; }
		}

        /// <summary>
        /// <para>Gets or sets the stored procedure name to set the data.</para>
        /// </summary>
        /// <value>
        /// <para>The stored procedure name to set the data.</para>
        /// </value>
		public string SetStoredProcedure
		{
			get { return this.setStoredProcedure; }
		}
	}
}