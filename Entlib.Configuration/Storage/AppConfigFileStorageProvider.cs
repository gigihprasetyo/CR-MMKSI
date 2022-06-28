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
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Storage;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage
{
    /// <summary>
    /// <para>
    /// Represents a storage provider for configuration data that saves the configuration data to the <see cref="AppDomain"/> configuration file.
    /// </para>
    /// </summary>
    public class AppConfigFileStorageProvider : StorageProvider, IStorageProviderWriter
    {
        private RuntimeConfigurationView runtimeConfigurationView;
        private const string configSectionsXPath = "//configuration/configSections";
        private const string configSectionsElement = "configSections";
        private const string configSectionElement = "section";
        private const string nameAttribute = "name";
        private const string typeAttribute = "type";
        private const string rootNode = "configuration";

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="AppConfigFileStorageProvider"/> class.</para>
        /// </summary>
        public AppConfigFileStorageProvider()
        {
        }

        /// <summary>
        /// <para>When overriden by a derived clas, reads the configuration data from storage.</para>
        /// </summary>
        /// <returns>
        /// <para>The configuration data.</para>
        /// </returns>
		public override object Read()
		{
			XmlDocument xmlApplicationDocument = OpenConfigurationFile();
			if (xmlApplicationDocument == null)
			{
				throw new ConfigurationException(SR.ExceptionXmlStorageSectionNotFoundError(CurrentSectionName, GetAppDomainConfigurationFile()));
			}
			XmlNode sectionNode = GetConfigurationSectionNode(xmlApplicationDocument);
			if (sectionNode == null)
			{
				throw new ConfigurationException(SR.ExceptionXmlStorageSectionNotFoundError(CurrentSectionName, GetAppDomainConfigurationFile()));
			}

			return sectionNode.FirstChild;
		}

        /// <summary>
        /// <para>Writes the configuration data to storage to the application configuration file.</para>
        /// </summary>
        /// <param name="value">
        /// <para>The value to write to storage.</para>
        /// </param>
        public void Write(object value)
        {
            ArgumentValidation.CheckForNullReference(value, "value");
            ArgumentValidation.CheckExpectedType(value, typeof(XmlNode));

            XmlNode valueNode = (XmlNode)value;
            AppendSection(valueNode);
        }

        /// <summary>
        /// <para>When overriden by a derived class, creates an <see cref="IConfigurationChangeWatcher"/> for the storage.</para>
        /// </summary>
        /// <returns>
        /// <para>An <see cref="IConfigurationChangeWatcher"/> for the storage.</para>
        /// </returns>
        public override IConfigurationChangeWatcher CreateConfigurationChangeWatcher()
        {
            return new ConfigurationChangeFileWatcher(GetAppDomainConfigurationFile(), CurrentSectionName);
        }

        /// <summary>
        /// <para>Initializes this provider to the correct state and context used by the factory creating it.</para>
        /// </summary>
        /// <param name="configurationView">
        /// <para>The cursor to use to get the data specific for the storage provider.</para>
        /// </param>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="configurationView"/> must be of type <see cref="RuntimeConfigurationView"/>.</para>
        /// </exception>
        public override void Initialize(ConfigurationView configurationView)
        {
            ArgumentValidation.CheckForNullReference(configurationView, "configurationView");

            GetStorageCursor(configurationView);
        }

        private void GetStorageCursor(ConfigurationView configurationView)
        {
            ArgumentValidation.CheckExpectedType(configurationView, typeof(RuntimeConfigurationView));

            runtimeConfigurationView = (RuntimeConfigurationView)configurationView;
        }

        /// <devdoc>
        /// Append a new section to the configSections of the configuration file given the sectionName and typeName.  
        /// </devdoc>        
        private void AppendSection(XmlNode objectData)
        {
            XmlDocument xmlDocument = OpenConfigurationFile();
            XmlNode configSections = GetAppConfigurationSections(xmlDocument);

            if (!SectionExists(configSections, CurrentSectionName))
            {
                XmlElement element = configSections.OwnerDocument.CreateElement(configSectionElement);
                element.SetAttribute(nameAttribute, CurrentSectionName);
                element.SetAttribute(typeAttribute, typeof(IgnoreSectionHandler).AssemblyQualifiedName);
                configSections.AppendChild(element);
            }
            AppendSectionData(xmlDocument, objectData);
            SaveDocument(xmlDocument);
        }

        private static XmlNode GetAppConfigurationSections(XmlDocument doc)
        {
            XmlNode configSections = doc.DocumentElement.SelectSingleNode(configSectionsXPath);
            if (configSections == null)
            {
                if (doc.DocumentElement.ChildNodes.Count == 0)
                {
                    configSections = doc.DocumentElement.AppendChild(doc.CreateElement(configSectionsElement));
                }
                else
                {
                    configSections = doc.DocumentElement.InsertBefore(doc.CreateElement(configSectionsElement), doc.DocumentElement.FirstChild);
                }

            }
            return configSections;
        }

        private void AppendSectionData(XmlDocument xmlDocument, XmlNode objectData)
        {
            XmlNode root = xmlDocument.SelectSingleNode(string.Concat("//", rootNode));
            using (ConfigurationProtector protector = this.runtimeConfigurationView.GetConfigurationProtector(CurrentSectionName))
            {
                XmlNode sectionNode = root.SelectSingleNode(string.Concat("//", CurrentSectionName));
                if (sectionNode != null)
                {
                    if (protector.Encrypted)
                    {
                        if (sectionNode.FirstChild == null) return;

                        XmlNode node = GetEncryptedNodeData(xmlDocument, objectData, protector);
                        sectionNode.RemoveChild(sectionNode.FirstChild);
                        sectionNode.AppendChild(node);
                        //xmlDocument.ReplaceChild(node, sectionNode.FirstChild);
                    }
                    else
                    {
                        XmlNode oldNode = sectionNode.FirstChild;
                        XmlNode newNode = xmlDocument.ImportNode(objectData, true);
                        sectionNode.ReplaceChild(newNode, oldNode);    
                    }
                }
                else
                {
                    XmlNode newSectionNode = xmlDocument.CreateElement(CurrentSectionName);
                    if (protector.Encrypted)
                    {
                        XmlNode node = GetEncryptedNodeData(xmlDocument, objectData, protector);
                        newSectionNode.AppendChild(node);
                    }
                    else
                    {
                        XmlNode newNode = xmlDocument.ImportNode(objectData, true);
                        newSectionNode.AppendChild(newNode);
                    }
                    root.AppendChild(newSectionNode);
                }
            }
        }

        private static XmlNode GetEncryptedNodeData(XmlDocument xmlDocument, XmlNode dataNode, ConfigurationProtector protector)
        {
            byte[] xmlBytes = GetEncoding(xmlDocument).GetBytes(dataNode.OuterXml);
            xmlBytes = protector.Encrypt(xmlBytes);    
            XmlNode node = xmlDocument.CreateNode(XmlNodeType.CDATA, string.Empty, xmlDocument.NamespaceURI);
            node.Value = Convert.ToBase64String(xmlBytes);
            return node;
        }

        private static Encoding GetEncoding(XmlDocument xmlDocument)
        {
            if (xmlDocument.HasChildNodes)
            {
                XmlDeclaration dec = xmlDocument.FirstChild as XmlDeclaration;
                if (null != dec)
                {
                    return Encoding.GetEncoding(dec.Encoding);
                }
            }
            return new UTF8Encoding(false, true);
        }
       
        private static bool SectionExists(XmlNode configSections, string sectionName)
        {
            return (GetNamedSectionNode(configSections, sectionName) != null);
        }

        private static XmlNode GetNamedSectionNode(XmlNode configSections, string sectionName)
        {
            XmlNode sectionNode = configSections.SelectSingleNode(string.Concat("//section[@name='", sectionName, "']"));
            return sectionNode;
        }

        private void SaveDocument(XmlDocument doc)
        {
            string configurationFile = GetAppDomainConfigurationFile();
            doc.Save(configurationFile);
        }

        private string GetAppDomainConfigurationFile()
        {
            AppConfigFileStorageProviderData storageProviderData = (AppConfigFileStorageProviderData)runtimeConfigurationView.GetStorageProviderData(CurrentSectionName);
            return storageProviderData.ConfigurationFile;
        }

        private XmlDocument OpenConfigurationFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(GetAppDomainConfigurationFile());
            return doc;
        }

        private XmlNode GetConfigurationSectionNode(XmlDocument doc)
        {
            XmlNode root = doc.SelectSingleNode(string.Concat("//", rootNode));
            XmlNode sectionNode = root.SelectSingleNode(string.Concat("//", CurrentSectionName));
            if (sectionNode == null || sectionNode.FirstChild == null || sectionNode.FirstChild.Value == null) return sectionNode;

            using (ConfigurationProtector protector = this.runtimeConfigurationView.GetConfigurationProtector(CurrentSectionName))
            {
                if (protector.Encrypted)
                {
                    string nodeValue = string.Empty;
                    
                    byte[] nodeData = protector.Decrypt(Convert.FromBase64String(sectionNode.FirstChild.Value));
                    using(StreamReader reader = new StreamReader(new MemoryStream(nodeData)))
                    {
                        nodeValue = reader.ReadToEnd();
                        XmlDocument tempDocument = new XmlDocument();
                        tempDocument.LoadXml(nodeValue);
                        XmlNode dataNode = tempDocument.DocumentElement;
                        XmlNode newNode = doc.ImportNode(dataNode, true);
                        sectionNode.ReplaceChild(newNode, sectionNode.FirstChild);
                    }
                }
            }
            return sectionNode;
        }
    }
}
