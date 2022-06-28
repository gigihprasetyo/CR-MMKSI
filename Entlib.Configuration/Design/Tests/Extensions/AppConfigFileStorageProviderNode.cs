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

#if UNIT_TESTS
using Microsoft.Practices.EnterpriseLibrary.Configuration.Tests.Extensions;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Tests.Extensions
{
    /// <summary>
    /// <para>Represents a configuration storage provider that saves configuraiton to the <see cref="AppDomain.SetupInformation.ConfigurationFile"/></para>
    /// </summary>
	public class AppConfigFileStorageProviderNode : StorageProviderNode
	{
        /// <summary>
        /// <para>Initialize a new instance of the <see cref="AppConfigFileStorageProviderNode"/> with an instance <see cref="AppConfigFileStorageProviderData"/>.</para>
        /// </summary>
        /// <param name="data">
        /// <para>The configuration data for the provider.</para>
        /// </param>
		public AppConfigFileStorageProviderNode(AppConfigFileStorageProviderData data) : base(data)
		{
            
		}

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="AppConfigFileStorageProviderNode"/> with an instance <see cref="AppConfigFileStorageProviderData"/>.</para>
        /// </summary>
		public AppConfigFileStorageProviderNode() : this(new AppConfigFileStorageProviderData(SR.AppConfigFileStorageProviderNodeName))
		{
		}

        /// <summary>
        /// <para>Sets the name of node when sited to match the underlying storage name and sets the configuraiton file correctly for the section.</para>
        /// </summary>
        protected override void OnSited()
        {
            base.OnSited ();
            ApplicationConfigurationNode node = (ApplicationConfigurationNode)Hierarchy.RootNode;
            if (Hierarchy.ConfigurationContext == null)
            {
                return;
            }

            // make sure that the section has the correct file in design time mode
            ConfigurationSectionData sectionData = Hierarchy.ConfigurationContext.GetMetaConfiguration(Parent.Name);
            if (sectionData == null)
            {
                return;
            }

            AppConfigFileStorageProviderData providerData = (AppConfigFileStorageProviderData)sectionData.StorageProvider;
            providerData.ConfigurationFile = node.ConfigurationFile;
        }

        /// <summary>
        /// <para>Gets the <see cref="AppConfigFileStorageProviderData"/> for this node.</para>
        /// </summary>
        /// <value>
        /// <para>The <see cref="AppConfigFileStorageProviderData"/> for this node.</para>
        /// </value>
        public override StorageProviderData StorageProvider
        {
            get
            {
                AppConfigFileStorageProviderData data = (AppConfigFileStorageProviderData)base.StorageProvider;
                ApplicationConfigurationNode node = (ApplicationConfigurationNode)Hierarchy.RootNode;
                data.ConfigurationFile = node.ConfigurationFile;
                return base.StorageProvider;
            }
        }
	}
}
#endif