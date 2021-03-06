//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright ? Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Validation;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Design
{
    /// <summary>
    /// <para>
    /// Represents a database instance in the data settings for an application.
    /// </para>
    /// </summary>
    [Image(typeof(InstanceNode))]
    [ServiceDependency(typeof(ILinkNodeService))]
    public class InstanceNode : ConfigurationNode
    {
        private DatabaseTypeNode databaseTypeNode;
        private ConnectionStringNode connectionStringNode;
        private InstanceData instanceData;
        private ConfigurationNodeChangedEventHandler databaseTypeRemovedHandler;
        private ConfigurationNodeChangedEventHandler databaseTypeRenamedHandler;
        private ConfigurationNodeChangedEventHandler connectionStringRemovedHandler;
        private ConfigurationNodeChangedEventHandler connectionStringRenamedHandler;

        /// <summary>
        /// <para>
        /// Initialize a new instance of the <see cref="InstanceNode"/> class.
        /// </para>
        /// </summary>
        public InstanceNode() : this(new InstanceData(SR.DefaultInstanceNodeName))
        {
        }

        /// <summary>
        /// <para>
        /// Initialize a new instance of the <see cref="InstanceNode"/> class with a <see cref="InstanceData"/> object.
        /// </para>
        /// </summary>
        /// <param name="instance">
        /// <para>The <see cref="InstanceData"/> runtime configuration.</para>
        /// </param>         
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="instance"/> is a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </exception>
        public InstanceNode(InstanceData instance) : base()
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            this.instanceData = instance;
            this.databaseTypeRemovedHandler = new ConfigurationNodeChangedEventHandler(OnDatabaseTypeNodeRemoved);
            this.databaseTypeRenamedHandler = new ConfigurationNodeChangedEventHandler(OnDatabaseTypeNodeRenamed);
            this.connectionStringRemovedHandler = new ConfigurationNodeChangedEventHandler(OnConnectionStringNodeRemoved);
            this.connectionStringRenamedHandler = new ConfigurationNodeChangedEventHandler(OnConnectionStringNodeRenamed);
        }

        /// <summary>
        /// <para>
        /// Gets or sets the <see cref="DatabaseTypeNode"/> for the instance.
        /// </para>
        /// </summary>
        /// <value>
        /// <para>
        /// The <see cref="DatabaseTypeNode"/> for the instance.
        /// </para>
        /// </value>
        [Required]
        [Editor(typeof(ReferenceEditor), typeof(UITypeEditor))]
        [ReferenceType(typeof(DatabaseTypeNode))]
        [SRCategory(SR.Keys.CategoryGeneral)]
        [SRDescription(SR.Keys.InstanceDatabaseTypeDescription)]
        public DatabaseTypeNode DatabaseTypeNode
        {
            get { return this.databaseTypeNode; }
            set
            {
                ILinkNodeService service = ServiceHelper.GetLinkService(Site);
                this.databaseTypeNode = (DatabaseTypeNode)service.CreateReference(databaseTypeNode, value, databaseTypeRemovedHandler, databaseTypeRenamedHandler);
                DatabaseTypeName = string.Empty;
                if (this.databaseTypeNode != null)
                {
                    DatabaseTypeName = this.databaseTypeNode.Name;
                }
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets the <see cref="ConnectionStringNode"/> for the instance.
        /// </para>
        /// </summary>
        /// <value>
        /// <para>
        /// The <see cref="ConnectionStringNode"/> for the instance.
        /// </para>
        /// </value>
        [SRCategory(SR.Keys.CategoryGeneral)]
        [Editor(typeof(ReferenceEditor), typeof(UITypeEditor))]
        [ReferenceType(typeof(ConnectionStringNode))]
        [Required]
        [SRDescription(SR.Keys.InstanceDatabaseTypeDescription)]
        public ConnectionStringNode ConnectionStringNode
        {
            get { return this.connectionStringNode; }
            set
            {
                ILinkNodeService service = ServiceHelper.GetLinkService(Site);
                this.connectionStringNode = (ConnectionStringNode)service.CreateReference(connectionStringNode, value, connectionStringRemovedHandler, connectionStringRenamedHandler);
                ConnectionString = string.Empty;
                if (this.connectionStringNode != null)
                {
                    ConnectionString = this.connectionStringNode.Name;
                }
            }
        }

        /// <summary>
        /// <para>
        /// Gets or sets the fully qualified type name of the instance.
        /// </para>
        /// </summary>
        /// <value>
        /// <para>
        /// The name of the connection string.
        /// </para>
        /// </value>
        [Browsable(false)]
        public string DatabaseTypeName
        {
            get { return this.instanceData.DatabaseTypeName; }
            set { this.instanceData.DatabaseTypeName = value; }
        }

        /// <summary>
        /// <para>
        /// Gets or sets the connection string for the instance.
        /// </para>
        /// </summary>
        /// <value>
        /// <para>
        /// The connection string for the instance.
        /// </para>
        /// </value>
        [Browsable(false)]
        public string ConnectionString
        {
            get { return this.instanceData.ConnectionString; }
            set { this.instanceData.ConnectionString = value; }
        }

        /// <summary>
        /// <para>Gets the <see cref="InstanceData"/> configuration.</para>
        /// </summary>
        /// <value>
        /// <para>The <see cref="InstanceData"/> configuration.</para>
        /// </value>
        [Browsable(false)]
        public virtual InstanceData InstanceData
        {
            get { return this.instanceData; }
        }

        /// <summary>
        /// <para>After the node is loaded, allows child nodes to resolve references to sibling nodes in configuration.</para>
        /// </summary>
        public override void ResolveNodeReferences()
        {
            base.ResolveNodeReferences();
            ConnectionStringCollectionNode connectionStringCollectionNode = Hierarchy.FindNodeByType(Parent.Parent, typeof(ConnectionStringCollectionNode)) as ConnectionStringCollectionNode;
            ConnectionStringNode = Hierarchy.FindNodeByName(connectionStringCollectionNode, instanceData.ConnectionString) as ConnectionStringNode;
            if (connectionStringNode == null)
            {
                throw new InvalidOperationException(SR.ExceptionConnectionStringNodeNotFound(this.instanceData.ConnectionString, this.instanceData.Name));
            }
            DatabaseTypeCollectionNode databaseTypeCollectionNode = Hierarchy.FindNodeByType(Parent.Parent, typeof(DatabaseTypeCollectionNode)) as DatabaseTypeCollectionNode;
            DatabaseTypeNode = Hierarchy.FindNodeByName(databaseTypeCollectionNode, instanceData.DatabaseTypeName) as DatabaseTypeNode;
            if (databaseTypeNode == null)
            {
                throw new InvalidOperationException(SR.ExceptionDatabaseTypeNodeNotFound(this.instanceData.DatabaseTypeName, this.instanceData.Name));
            }
        }

        /// <summary>
        /// <para>Sets the <seeals cref="ConfigurationNode.Name"/> to the <seealso cref="ParameterData.Name"/></para>
        /// </summary>
        protected override void OnSited()
        {
            base.OnSited();
            Site.Name = instanceData.Name;
        }

        /// <summary>
        /// <para>Sets the <seeals cref="ParameterData.Name"/> to the <seealso cref="ConfigurationNode.Name"/></para>
        /// </summary>
        /// <param name="e"><para>A <see cref="ConfigurationNodeChangedEventArgs"/> that contains the event data.</para></param>
        protected override void OnRenamed(ConfigurationNodeChangedEventArgs e)
        {
            base.OnRenamed(e);
            instanceData.Name = e.Node.Name;
        }

        /// <devdoc>
        /// Handles the removal of the connection string node.
        /// </devdoc>                
        private void OnConnectionStringNodeRemoved(object sender, ConfigurationNodeChangedEventArgs e)
        {
            this.connectionStringNode = null;
            ConnectionString = string.Empty;
        }

        /// <devdoc>
        /// Handles the renaming of the connection string node.
        /// </devdoc>                
        private void OnConnectionStringNodeRenamed(object sender, ConfigurationNodeChangedEventArgs e)
        {
            ConnectionString = e.Node.Name;
        }

        /// <devdoc>
        /// Handles the remove of a database node.
        /// </devdoc>                
        private void OnDatabaseTypeNodeRemoved(object sender, ConfigurationNodeChangedEventArgs e)
        {
            this.databaseTypeNode = null;
            DatabaseTypeName = string.Empty;
        }

        /// <devdoc>
        /// Handles the renaming of a database node.
        /// </devdoc>                
        private void OnDatabaseTypeNodeRenamed(object sender, ConfigurationNodeChangedEventArgs e)
        {
            DatabaseTypeName = e.Node.Name;
        }
    }
}