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
using Microsoft.Practices.EnterpriseLibrary.Configuration.Storage;
using Microsoft.Win32;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration
{
	/// <summary>
	/// <para>Represents the configuration data for a <see cref="RegistryStorageProvider"/>.</para>
	/// </summary>    	
	[XmlRoot("storageProvider", Namespace=ConfigurationSettings.ConfigurationNamespace)]
	public class RegistryStorageProviderData : StorageProviderData
	{
		private string registrySubkey;
		private AllowedRegistryHive registryRoot;

		/// <summary>
		/// <para>Initialize a new instance of the <see cref="RegistryStorageProviderData"/></para>
		/// </summary>
		public RegistryStorageProviderData() : this(string.Empty)
		{
		}

		/// <summary>
		/// <para>Initialize a new instance of the <see cref="RegistryStorageProviderData"/> with a name.</para>
		/// </summary>
		/// <param name="name">
		/// <para>The name of the <see cref="RegistryStorageProvider"/> for this configuration data.</para>
		/// </param>
		public RegistryStorageProviderData(string name) : this(name, string.Empty)
		{
		}

		/// <summary>
		/// <para>Initialize a new instance of the <see cref="RegistryStorageProviderData"/> with a name and the sub key in the registry the data is to be stored.</para>
		/// </summary>
		/// <param name="name">
		/// <para>The name of the <see cref="RegistryStorageProvider"/> for this configuration data.</para>
		/// </param>
		/// <param name="registrySubKey">
		/// <para>The sub key where the data is to be stored.</para>
		/// </param>
		public RegistryStorageProviderData(string name, string registrySubKey) : this(name, registrySubKey, AllowedRegistryHive.CurrentUser)
		{
		}

		/// <summary>
		/// <para>Initialize a new instance of the <see cref="RegistryStorageProviderData"/> with a name and the sub key and the <see cref="RegistryHive"/> where the data is to be stored.</para>
		/// </summary>
		/// <param name="name">
		/// <para>The name of the <see cref="RegistryStorageProvider"/> for this configuration data.</para>
		/// </param>
		/// <param name="registrySubKey">
		/// <para>The sub key where the data is to be stored.</para>
		/// </param>
		/// <param name="registryRoot">
		/// <para>The <see cref="AllowedRegistryHive"/> where the data is to be stored.</para>
		/// </param>
		public RegistryStorageProviderData(string name, string registrySubKey, AllowedRegistryHive registryRoot) : base(name, typeof(RegistryStorageProvider).AssemblyQualifiedName)
		{
			ValidateHiveValue(registryRoot);
			this.registrySubkey = registrySubKey;
			this.registryRoot = registryRoot;
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
		[XmlIgnore]
		public override string TypeName
		{
			get { return typeof(RegistryStorageProvider).AssemblyQualifiedName; }
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
			return new RegistryStorageProviderData(Name, this.registrySubkey, this.registryRoot);
		}

		/// <summary>
		/// <para>Gets or sets the <see cref="RegistryHive"/> where the data is to be stored.</para>
		/// </summary>
		/// <value>
		/// <para>The <see cref="RegistryHive"/> where the data is to be stored. The default is <see cref="RegistryHive.CurrentUser"/></para>
		/// </value>
		[XmlAttribute("registryRoot")]
		public AllowedRegistryHive RegistryRoot
		{
			get { return this.registryRoot; }
			set
			{
				ValidateHiveValue(value);
				this.registryRoot = value;
			}
		}

		/// <summary>
		/// <para>Gets or sets the sub key where the data is to be stored.</para>
		/// </summary>
		/// <value>
		/// <para>The sub key where the data is to be stored.</para>
		/// </value>
		[XmlAttribute("registrySubKey")]
		public string RegistrySubKey
		{
			get { return this.registrySubkey; }
			set { this.registrySubkey = value; }
		}

		internal static void ValidateHiveValue(AllowedRegistryHive value)
		{
			if (!Enum.IsDefined(typeof(AllowedRegistryHive), value))
			{
				throw new ArgumentOutOfRangeException("RegistryRoot");
			}
		}
	}
}