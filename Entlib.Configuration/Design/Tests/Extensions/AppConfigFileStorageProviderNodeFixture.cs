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
using System;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Tests.Extensions;
using NUnit.Framework;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Tests.Extensions
{
    [TestFixture]
    public class AppConfigFileStorageProviderNodeFixture : ConfigurationDesignHostTestBase
    {
        public override void SetUp()
        {
            base.SetUp ();
            INodeCreationService service = ServiceHelper.GetNodeCreationService(Host);

            Type nodetype = typeof(AppConfigFileStorageProviderNode);
            service.AddNodeCreationEntry(NodeCreationEntry.CreateNodeCreationEntryNoMultiples(new AddChildNodeCommand(Host, nodetype), nodetype, typeof(AppConfigFileStorageProviderData), SR.AppConfigFileStorageProviderMenuName));

            IXmlIncludeTypeService includeTypeService = ServiceHelper.GetXmlIncludeTypeService(Host);
            includeTypeService.AddXmlIncludeType(ConfigurationSettings.SectionName, typeof(AppConfigFileStorageProviderData));
        }

        [Test]
        public void EnsureTheXmlIncludeTypeIsAddedWhenAddingTheAppFileProvider()
        {
            AddChildNodeCommand cmd = new AddChildNodeCommand(Host, typeof(ConfigurationSectionCollectionNode));
            cmd.Execute(GeneratedApplicationNode);
            ConfigurationSectionCollectionNode sections = (ConfigurationSectionCollectionNode)cmd.ChildNode;
            cmd = new AddChildNodeCommand(Host, typeof(ConfigurationSectionNode));
            cmd.Execute(sections);
            ConfigurationSectionNode section = (ConfigurationSectionNode)cmd.ChildNode;
            cmd = new AddChildNodeCommand(Host, typeof(XmlFileStorageProviderNode));
            cmd.Execute(section);
            ConfigurationNode xmlFileStorageNode = GeneratedHierarchy.FindNodeByType(section, typeof(XmlFileStorageProviderNode));
            Assert.IsNotNull(xmlFileStorageNode);
            Assert.AreEqual(xmlFileStorageNode.GetType(), typeof(XmlFileStorageProviderNode));
            section.Nodes.Remove(xmlFileStorageNode);
            
            cmd = new AddChildNodeCommand(Host, typeof(AppConfigFileStorageProviderNode));
            cmd.Execute(section);
            XmlIncludeTypeNode xmlIncludeNode = GeneratedHierarchy.FindNodeByType(sections, typeof(XmlIncludeTypeNode)) as XmlIncludeTypeNode;
            Assert.IsNotNull(xmlIncludeNode);
            Assert.AreEqual(xmlIncludeNode.GetType(), typeof(XmlIncludeTypeNode));
            xmlIncludeNode.TypeName = typeof(AppConfigFileStorageProvider).AssemblyQualifiedName;
         }
    }
}
#endif