#define VS2003
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
using System.IO;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Storage;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration
{
	/// <devdoc>
	/// Represents a configuration file according to the schema for Fx 1.1
	/// </devdoc>
	internal class ConfigurationFile /*: IDisposable*/
	{
		public const string MetaConfigSectionName = "metaConfig";
		private const string rootNode = "configuration";
#if VS2003
        private const string configSectionsXPath = "//configuration/configSections";
#endif
#if VS2005B2
		private const string configSectionsXPath = "configuration/configSections";
		private const string webNamespace = "http://schemas.microsoft.com/.NetConfiguration/v2.0";
#endif
		private const string configSectionsElement = "configSections";
		private const string configSectionElement = "section";
		private const string nameAttribute = "name";
		private const string typeAttribute = "type";

		private readonly ConfigurationFile parent;

		private string configurationFile;
		private ConfigurationSettings configurationSettings;

		public ConfigurationFile(string configurationFile, ConfigurationFile parent)
		{
			SetConfigurationFileName(configurationFile);
			this.parent = parent;
			this.configurationSettings = null;
		}

#if  UNIT_TESTS || LONG_RUNNING_TESTS
		internal string FileName
		{
			get { return this.configurationFile; }
		}
#endif

		/// <devdoc>
		/// Append a new section to the configSections of the configuration file given the 
		/// sectionName and typeName.  The objectData is
		/// </devdoc>        
		public void AppendSection(string sectionName, string typeName, XmlNode objectData)
		{
			ArgumentValidation.CheckForNullReference(sectionName, "sectionName");
			ArgumentValidation.CheckForNullReference(typeName, "typeName");
			ArgumentValidation.CheckForNullReference(objectData, "objectData ");
			if (this.configurationFile == null)
			{
				throw new InvalidOperationException(SR.ExceptionConfigFileNotLoaded);
			}

			XmlDocument doc = new XmlDocument();
			doc.Load(this.configurationFile);

#if VS2003
            XmlNode configSections = doc.DocumentElement.SelectSingleNode(configSectionsXPath);
#endif

#if VS2005B2
			bool isWeb = (doc.DocumentElement.NamespaceURI == webNamespace);
			// hack because app.config and web.config are different
			XmlNamespaceManager nsmngr = new XmlNamespaceManager(doc.NameTable);
			nsmngr.AddNamespace("netweb", webNamespace);
			XmlNode configSections = doc.DocumentElement.SelectSingleNode(string.Concat("//netweb:", "configuration/netweb:configSections"), nsmngr);
			if (configSections == null)
			{				
				configSections = doc.DocumentElement.SelectSingleNode(string.Concat("//", configSectionsXPath));
			}
#endif

			if (configSections == null)
			{
				if (doc.DocumentElement.ChildNodes.Count == 0)
				{
					configSections = doc.DocumentElement.AppendChild(doc.CreateElement(configSectionsElement));
				}
				else
				{
#if VS2003
					configSections = doc.DocumentElement.InsertBefore(doc.CreateElement(configSectionsElement), doc.DocumentElement.FirstChild);
#endif

#if VS2005B2
					if (isWeb)
					{
						configSections = doc.DocumentElement.InsertBefore(doc.CreateElement(configSectionsElement, webNamespace), doc.DocumentElement.FirstChild);
					}
					else
					{
						configSections = doc.DocumentElement.InsertBefore(doc.CreateElement(configSectionsElement), doc.DocumentElement.FirstChild);
					}
#endif
				}

			}			
#if VS2003
			if (!SectionExists(configSections, sectionName))
			{
				XmlElement element = configSections.OwnerDocument.CreateElement(configSectionElement);
#endif

#if VS2005B2
			if (!SectionExists(configSections, sectionName, isWeb))
			{
				XmlElement element = null;
				if (isWeb)
				{
					element = configSections.OwnerDocument.CreateElement(configSectionElement, webNamespace);
				}
				else
				{
					element = configSections.OwnerDocument.CreateElement(configSectionElement);
				}
#endif
				element.SetAttribute(nameAttribute, sectionName);
				element.SetAttribute(typeAttribute, typeName);
				configSections.AppendChild(element);
			}
			AppendSectionData(sectionName, doc, objectData);
			doc.Save(this.configurationFile);
		}

		public object GetConfig(string configKey)
		{
			if (this.configurationSettings != null)
			{
				return this.configurationSettings;
			}
			ConfigurationSettingsLoader handler = new ConfigurationSettingsLoader();
			this.configurationSettings = handler.Build(this.configurationFile);
			if (this.configurationSettings == null)
			{
				// try parent
				if (parent != null)
				{
					this.configurationSettings = handler.Build(parent.configurationFile);
				}
			}
			return this.configurationSettings;
		}

		public IConfigurationChangeWatcher CreateFileWatcher()
		{
			ConfigurationChangeFileWatcher fileWatcher = new ConfigurationChangeFileWatcher(configurationFile, ConfigurationSettings.SectionName);
			return fileWatcher;
		}

		private void SetConfigurationFileName(string filename)
		{
			Uri uri = new Uri(filename);
			if (uri.Scheme == Uri.UriSchemeFile)
			{
				this.configurationFile = uri.LocalPath;
			}
			else
			{
				this.configurationFile = filename;
			}
		}

		/// <devdoc>
		/// Determines if a section exist in the config sections given the configSections node and the name of the section.
		/// </devdoc>        
#if VS2003
		private static bool SectionExists(XmlNode configSections, string sectionName)
		{
			return (GetSectionNode(configSections, sectionName) != null);
		}

		private static XmlNode GetSectionNode(XmlNode configSections, string sectionName)
		{
			XmlNode sectionNode = configSections.SelectSingleNode(string.Concat("//section[@name='", sectionName, "']"));
			return sectionNode;
		}
#endif

#if VS2005B2
		private static bool SectionExists(XmlNode configSections, string sectionName, bool isWeb)
		{
			return (GetSectionNode(configSections, sectionName, isWeb) != null);
		}

		private static XmlNode GetSectionNode(XmlNode configSections, string sectionName, bool isWeb)
		{
			XmlNode sectionNode = null;
			if (isWeb)
			{
				XmlNamespaceManager nsmngr = new XmlNamespaceManager(configSections.OwnerDocument.NameTable);
				nsmngr.AddNamespace("netweb", webNamespace);
				sectionNode = configSections.SelectSingleNode(string.Concat("//netweb:section[@name='", sectionName, "']"), nsmngr);
			}
			else
			{
				sectionNode = configSections.SelectSingleNode(string.Concat("//section[@name='", sectionName, "']"));
			}
			return sectionNode;
		}
#endif	

		private static void AppendSectionData(string sectionName, XmlDocument doc, XmlNode objectData)
		{
#if VS2003
            XmlNode root = doc.SelectSingleNode(string.Concat("//", rootNode));
#endif

#if VS2005B2
			XmlNode root = doc.SelectSingleNode(string.Concat("//", rootNode));
			if (root == null)
			{
				XmlNamespaceManager netweb = new XmlNamespaceManager(doc.NameTable);
				netweb.AddNamespace("netweb", webNamespace);
				root = doc.SelectSingleNode(string.Concat("//netweb:", rootNode), netweb);
			}

#endif
			XmlNamespaceManager nsmngr = GetNamespaceManager(doc);
			XmlNode oldNode = root.SelectSingleNode(string.Concat("//entlib:", sectionName), nsmngr);
			if (oldNode != null)
			{
				XmlNode newNode = doc.ImportNode(objectData, true);
				oldNode.ParentNode.ReplaceChild(newNode, oldNode);
			}
			else
			{
				XmlNode newNode = doc.ImportNode(objectData, true);
				root.AppendChild(newNode);
			}
		}

		internal static XmlNamespaceManager GetNamespaceManager(XmlDocument doc)
		{
			XmlNamespaceManager nsmngr = new XmlNamespaceManager(doc.NameTable);
			nsmngr.AddNamespace("entlib", ConfigurationSettings.ConfigurationNamespace);
			return nsmngr;
		}
	}
}