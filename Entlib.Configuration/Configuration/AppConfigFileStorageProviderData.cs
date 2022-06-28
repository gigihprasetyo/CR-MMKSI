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
    /// <para>Represents a storage provider to read and write data to an <see cref="AppDomain"/> configuration file.</para>
    /// </summary>    	
    [XmlRoot("storageProvider", Namespace=ConfigurationSettings.ConfigurationNamespace)]
    public class AppConfigFileStorageProviderData : StorageProviderData
    {
        private string configurationFile;

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="AppConfigFileStorageProviderData"/> class.</para>
        /// </summary>
        public AppConfigFileStorageProviderData() : this(string.Empty)
        {
        }

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="AppConfigFileStorageProviderData"/> class with a name.</para>
        /// </summary>
        /// <param name="name">
        /// <para>The name of the provider.</para>
        /// </param>
        public AppConfigFileStorageProviderData(string name) : base(name)
        {
            configurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
        }

        /// <summary>
        /// <para>Gets the current <see cref="AppDomain"/> configuraiton file.</para>
        /// </summary>
        /// <value>
        /// <para>The current <see cref="AppDomain"/> configuraiton file.</para>
        /// </value>
        [XmlIgnore]
        public string ConfigurationFile
        {
            get { return this.configurationFile; }
            set { this.configurationFile = value; }
        }

        /// <summary>
        /// <para>Gets the fully qualified assembly name for a <see cref="XmlFileStorageProvider"/>.</para>
        /// </summary>
        /// <value>
        /// <para>The fully qualified assembly name for a <see cref="XmlFileStorageProvider"/>.</para>
        /// </value>
        [XmlIgnore]
        public override string TypeName
        {
            get { return typeof(AppConfigFileStorageProvider).AssemblyQualifiedName; }
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
        /// <remarks>
        /// <para>This clone does a deep copy.</para>
        /// </remarks>
        public override object Clone()
        {
            return new AppConfigFileStorageProviderData(Name);
        }
    }
}