//===============================================================================
// Microsoft patterns & practices Enterprise Library
// 
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

#if UNIT_TESTS
using System;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Tests.Extensions;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Tests.Extensions
{
    /// <summary>
    /// <para>Represents the <see cref="IConfigurationDesignManager"/> for the extenstios for Enterprise Library.</para>
    /// </summary>
	public class ExtensionsConfigurationDesignManager : IConfigurationDesignManager
	{
        /// <summary>
        /// <para>Initialize a new instance of the <see cref="ExtensionsConfigurationDesignManager"/> class.</para>
        /// </summary>
		public ExtensionsConfigurationDesignManager()
		{
		}

        /// <summary>
        /// <para>
        /// Allows the registration of configuration nodes and commands into the configuration tree.
        /// </para>
        /// </summary>
        /// <param name="serviceProvider">
        /// <para>The a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</para>
        /// </param>
		public void Register(IServiceProvider serviceProvider)
		{
			INodeCreationService service = ServiceHelper.GetNodeCreationService(serviceProvider);

			Type nodetype = typeof(AppConfigFileStorageProviderNode);
			service.AddNodeCreationEntry(NodeCreationEntry.CreateNodeCreationEntryNoMultiples(new AddChildNodeCommand(serviceProvider, nodetype), nodetype, typeof(AppConfigFileStorageProviderData), SR.AppConfigFileStorageProviderMenuName));

			IXmlIncludeTypeService includeTypeService = ServiceHelper.GetXmlIncludeTypeService(serviceProvider);
			includeTypeService.AddXmlIncludeType(ConfigurationSettings.SectionName, typeof(AppConfigFileStorageProviderData));
		}

        /// <summary>
        /// <para>Saves the configuration data for the implementer.</para>
        /// </summary>
        /// <param name="serviceProvider">
        /// <para>The a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</para>
        /// </param>
		public void Save(IServiceProvider serviceProvider)
		{
		}

        /// <summary>
        /// <para>Opens the configuration for the application.</para>
        /// </summary>
        /// <param name="serviceProvider">
        /// <para>The a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</para>
        /// </param>
		public void Open(IServiceProvider serviceProvider)
		{
		}

        /// <summary>
        /// <para>Adds the configuration data for the current implementer to the <see cref="ConfigurationDictionary"/>.</para>
        /// </summary>
        /// <param name="serviceProvider">
        /// <para>The a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</para>
        /// </param>
		public void BuildContext(IServiceProvider serviceProvider, ConfigurationDictionary configurationDictionary)
		{
		}
	}
}
#endif
