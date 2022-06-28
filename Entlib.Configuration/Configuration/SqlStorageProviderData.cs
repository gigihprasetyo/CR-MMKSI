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
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Storage;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration
{
    /// <summary>
    /// <para>Represents the configuration data for a <see cref="SqlStorageProvider"/>.</para>
    /// </summary>    	
    [XmlRoot("storageProvider", Namespace=ConfigurationSettings.ConfigurationNamespace)]
    public class SqlStorageProviderData : StorageProviderData
    {
        private string connectionString;
		private string getStoredProcedure;
		private string setStoredProcedure;

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="SqlStorageProviderData"/> class.</para>
        /// </summary>
        public SqlStorageProviderData() : this(string.Empty)
        {
        }

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="SqlStorageProviderData"/> class with a name.</para>
        /// </summary>
        /// <param name="name">
        /// <para>The name of the <see cref="SqlStorageProvider"/> for this data.</para>
        /// </param>
        public SqlStorageProviderData(string name) : this(name, string.Empty, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="SqlStorageProviderData"/> class with a name.</para>
        /// </summary>
        /// <param name="name">
        /// <para>The name of the <see cref="SqlStorageProvider"/> for this data.</para>
        /// </param>
        /// <param name="connectionString">
        /// <para>The connection string to the SQL Server database.</para>
        /// </param>
        /// <param name="getStoredProcedure">
        /// <para>The stored procedure name to get the data.</para>
        /// </param>
        /// <param name="setStoredProcedure">
        /// <para>The stored procedure name to set the data.</para>
        /// </param>
        public SqlStorageProviderData(string name, string connectionString, string getStoredProcedure, string setStoredProcedure) : base(name, typeof(SqlStorageProvider).AssemblyQualifiedName)
        {
            this.connectionString = connectionString;
            this.getStoredProcedure = getStoredProcedure;
            this.setStoredProcedure = setStoredProcedure;
        }


        /// <summary>
        /// <para>Gets or sets the connection string to the SQL Server database.</para>
        /// </summary>
        /// <value>
        /// <para>The connection string to the SQL Server database.</para>
        /// </value>
        [XmlAttribute("connectionString")]
        public string ConnectionString
        {
            get { return connectionString; }
			set {connectionString = value;}
        }

        /// <summary>
        /// <para>Gets or sets the stored procedure name to get the data.</para>
        /// </summary>
        /// <value>
        /// <para>The stored procedure name to get the data.</para>
        /// </value>
		[XmlAttribute("getStoredProcedure")]
		public string GetStoredProcedure
		{
			get { return this.getStoredProcedure; }
			set {this.getStoredProcedure = value; }
		}

		/// <summary>
		/// <para>Gets or sets the stored procedure name to set the data.</para>
		/// </summary>
		/// <value>
		/// <para>The stored procedure name to set the data.</para>
		/// </value>
        [XmlAttribute("setStoredProcedure")]
		public string SetStoredProcedure
		{
			get { return this.setStoredProcedure; }
			set {this.setStoredProcedure = value; }
		}

        /// <summary>
        /// <para>Gets or sets the <see cref="System.Type"/> name of the provider.</para>
        /// </summary>
        /// <value>
        /// <para>The type name of the provider. The default is an empty string.</para>
        /// </value>
        /// <remarks>
        /// <para>Always returns <see cref="Type.AssemblyQualifiedName"/> for <see cref="SqlStorageProvider"/>.</para>
        /// </remarks>
		[XmlIgnore]
		public override string TypeName
		{
			get { return typeof(SqlStorageProvider).AssemblyQualifiedName; }
			set
			{
			}
		}

        /// <summary>
        /// <para>Creates a new object that is a copy of the current instance.</para>
        /// </summary>
        /// <returns>
        /// <para>A new object that is a copy of this instance.</para>
        /// </returns>
		public override object Clone()
        {
            return new SqlStorageProviderData(Name, connectionString, getStoredProcedure,  setStoredProcedure);
        }
    }
}