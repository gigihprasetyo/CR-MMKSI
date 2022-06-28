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
using System.ComponentModel;
using System.Drawing.Design;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Validation;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Storage;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design
{
    /// <summary>
    /// <para>Represents a node to configure a <see cref="SqlStorageProvider"/>.</para>
    /// </summary>
    public class SqlStorageProviderNode : StorageProviderNode
    {
        private SqlStorageProviderData sqlStorageProviderData;

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="SqlStorageProviderNode"/> class.</para>
        /// </summary>
        public SqlStorageProviderNode() : this(new SqlStorageProviderData(SR.SqlStorageProviderNodeName))
        {
        }

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="SqlStorageProviderNode"/> class with a <see cref="SqlStorageProviderData"/> instance.</para>
        /// </summary>
        /// <param name="data">
        /// <para>A <see cref="SqlStorageProviderData"/> instance.</para>
        /// </param>
        public SqlStorageProviderNode(SqlStorageProviderData data) : base(data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            this.sqlStorageProviderData = data;
        }

        /// <summary>
        /// <para>Gets or sets the connection string to the SQL Server database.</para>
        /// </summary>
        /// <value>
        /// <para>The connection string to the SQL Server database.</para>
        /// </value>
        [Required]
        [SRDescription(SR.Keys.SqlStorageProviderNodeConnStringDescription)]
        [SRCategory(SR.Keys.CategoryGeneral)]
        [Editor(typeof(SqlConnectionStringEditor), typeof(UITypeEditor))]
        public string ConnectionString
        {
            get { return this.sqlStorageProviderData.ConnectionString; }
            set { this.sqlStorageProviderData.ConnectionString = value; }
        }

        /// <summary>
        /// <para>Gets or sets the stored procedure name to get the data.</para>
        /// </summary>
        /// <value>
        /// <para>The stored procedure name to get the data.</para>
        /// </value>
        [Required]
        [SRDescription(SR.Keys.SqlStorageProviderNodeGetSPDescription)]
        [SRCategory(SR.Keys.CategoryGeneral)]
        public string GetStoredProcedure
        {
            get { return this.sqlStorageProviderData.GetStoredProcedure; }
            set { this.sqlStorageProviderData.GetStoredProcedure = value; }
        }

        /// <summary>
        /// <para>Gets or sets the stored procedure name to set the data.</para>
        /// </summary>
        /// <value>
        /// <para>The stored procedure name to set the data.</para>
        /// </value>
        [Required]
        [SRDescription(SR.Keys.SqlStorageProviderNodeSetSPDescription)]
        [SRCategory(SR.Keys.CategoryGeneral)]
        public string SetStoredProcedure
        {
            get { return this.sqlStorageProviderData.SetStoredProcedure; }
            set { this.sqlStorageProviderData.SetStoredProcedure = value; }
        }

        /// <summary>
        /// <para>Gets or sets the <see cref="System.Type"/> name of the provider.</para>
        /// </summary>
        /// <value>
        /// <para>The type name of the provider. The default is an empty string.</para>
        /// </value>
        [Required]
        [SRCategory(SR.Keys.CategoryGeneral)]
        [SRDescription(SR.Keys.SqlStorageProviderNodeTypeNameDescription)]
        [ReadOnly(true)]
        public string TypeName
        {
            get { return this.sqlStorageProviderData.TypeName; }
        }
    }
}