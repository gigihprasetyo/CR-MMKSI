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
using Microsoft.Win32;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage
{
    /// <summary>
    /// <para>Provides data for the <seealso cref="ConfigurationManager.ConfigurationChanging"/> and <see cref="ConfigurationContext.ConfigurationChanging"/> event which occur after configuration is changed and committed to storage for the <see cref="RegistryStorageProvider"/>.</para>
    /// </summary>
    [Serializable]
    public class ConfigurationRegistryChangingEventArgs : ConfigurationChangingEventArgs
    {
		private readonly string registrySubKey;
		private readonly AllowedRegistryHive registryRoot;

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="ConfigurationRegistryChangedEventArgs"/> class with the root <see cref="RegistryHive"/> and the registry sub key where the configuration data is stored, the configuration section name for this data, the old value and the new value.</para>
        /// </summary>
        /// <param name="registryRoot">
        /// <para>The root <see cref="RegistryHive"/>.</para>
        /// </param>
        /// <param name="registrySubKey">
        /// <para>The registry sub key where the configuration data is stored.</para>
        /// </param>
        /// <param name="sectionName">
        /// <para>The name of the configuration section.</para>
        /// </param>
        /// <param name="oldValue"><para>The old value.</para></param>
        /// <param name="newValue"><para>The new value.</para></param>
        public ConfigurationRegistryChangingEventArgs(AllowedRegistryHive registryRoot, string registrySubKey, string sectionName, object oldValue, object newValue) : base(String.Empty, sectionName, oldValue, newValue)
        {
            RegistryStorageProviderData.ValidateHiveValue(registryRoot);

			this.registryRoot = registryRoot;
			this.registrySubKey = registrySubKey;
        }

        /// <summary>
        /// <para>Gets the root <see cref="AllowedRegistryHive"/> for the configuration data.</para>
        /// </summary>
        /// <value>
        /// <para>The root <see cref="RegistryHive"/> for the configuration data.</para>
        /// </value>
		public AllowedRegistryHive RegistryRoot
		{
			get { return registryRoot; }
		}


        /// <summary>
        /// <para>Gets the registry sub key where the configuration data is stored.</para>
        /// </summary>
        /// <value>
        /// <para>The registry sub key where the configuration data is stored.</para>
        /// </value>
		public string RegistrySubKey
		{
			get { return registrySubKey; }
		}
	}
}