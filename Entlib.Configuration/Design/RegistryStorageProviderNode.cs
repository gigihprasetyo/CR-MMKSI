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
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Storage;
using Microsoft.Win32;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Validation;


namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design
{
    /// <summary>
    /// <para>Represents the node to configure a <see cref="RegistryStorageProvider"/> instance.</para>
    /// </summary>
    public class RegistryStorageProviderNode : StorageProviderNode
    {
        private RegistryStorageProviderData registryStorageProviderData;

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="RegistryStorageProviderNode"/> class.</para>
        /// </summary>
        public RegistryStorageProviderNode() : this(new RegistryStorageProviderData(SR.RegistryStorageProviderNodeName))
        {
        }

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="RegistryStorageProviderNode"/> class with a <see cref="RegistryStorageProviderData"/> instance.</para>
        /// </summary>
        /// <param name="data">
        /// <para>A <see cref="RegistryStorageProviderData"/> instance.</para>
        /// </param>
        public RegistryStorageProviderNode(RegistryStorageProviderData data) : base(data)
        {
            this.registryStorageProviderData = data;
        }

        /// <summary>
        /// <para>Gets or sets the <see cref="RegistryHive"/> where the data is to be stored.</para>
        /// </summary>
        /// <value>
        /// <para>The <see cref="RegistryHive"/> where the data is to be stored. The default is <see cref="RegistryHive.CurrentUser"/></para>
        /// </value>
        [Required]
        [SRDescription(SR.Keys.RegistryStorageProviderNodeHiveDescription)]
        [SRCategory(SR.Keys.CategoryGeneral)]
        public AllowedRegistryHive RegistryRoot
        {
            get { return this.registryStorageProviderData.RegistryRoot; }
            set { this.registryStorageProviderData.RegistryRoot = value; }
        }

        /// <summary>
        /// <para>Gets or sets the sub key where the data is to be stored.</para>
        /// </summary>
        /// <value>
        /// <para>The sub key where the data is to be stored.</para>
        /// </value>
		[Required]
		[SRDescription(SR.Keys.RegistryStorageProviderNodeKeyDescription)]
		[SRCategory(SR.Keys.CategoryGeneral)]
		public string RegistrySubKey
		{
			get { return this.registryStorageProviderData.RegistrySubKey; }
			set { this.registryStorageProviderData.RegistrySubKey = value; }
		}

        /// <summary>
        /// <para>Gets or sets the <see cref="System.Type"/> name of the provider.</para>
        /// </summary>
        /// <value>
        /// <para>The type name of the provider. The default is an empty string.</para>
        /// </value>
        /// <remarks>
        /// <para>Always returns <see cref="Type.AssemblyQualifiedName"/> for <see cref="RegistryStorageProvider"/>.</para>
        /// </remarks>
		[Required]
        [SRCategory(SR.Keys.CategoryGeneral)]
        [SRDescription(SR.Keys.RegistryStorageProviderNodeTypeNameDescription)]
        [ReadOnly(true)]
        public string TypeName
        {
            get { return this.registryStorageProviderData.TypeName; }
        }
    }
}