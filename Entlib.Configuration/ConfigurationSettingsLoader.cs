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
using System.Security.Permissions;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration
{
    /// <devdoc>
    /// Reads the "enterpriselibrary.configurationSettings" section of the configuration file. 
    /// </devdoc>
    internal class ConfigurationSettingsLoader 
    {
        private const string xmlIncludeTypeXPath = "elib:includeType";
        
        public ConfigurationSettingsLoader()
        {
        }

        public ConfigurationSettings Build(string configurationFile)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(configurationFile);    
            }
            catch (XmlException ex)
            {
                throw new ConfigurationException(ex.Message, ex);
            }
            catch (IOException ex)
            {
                throw new ConfigurationException(ex.Message, ex);
            }
            XPathNavigator navigator = doc.CreateNavigator();
            XPathExpression expr = navigator.Compile(string.Concat("//elib:", ConfigurationSettings.SectionName));
            XmlNamespaceManager nsmgr = GetNamespaceManager(navigator);
            expr.SetContext(nsmgr);
            XPathNodeIterator iterator = navigator.Select(expr);
            ConfigurationSettings configurationSettings = null;
            if (iterator.MoveNext())
            {
                configurationSettings = Build(((IHasXmlNode)iterator.Current).GetNode());
            }
            return configurationSettings; 
        }

        public ConfigurationSettings Build(XmlNode node)
        {
            ConfigurationSettings configurationSettings = new ConfigurationSettings();
            if (node == null)
            {
                return configurationSettings;
            }
            
            Type[] types = GetTypes(node.CreateNavigator());

            XmlSerializer xmlSerializer = CreateXmlSerializer(types);
            configurationSettings = (ConfigurationSettings)xmlSerializer.Deserialize(new XmlTextReader(new StringReader(node.OuterXml)));
            return configurationSettings;
        }

        private static XmlSerializer CreateXmlSerializer(Type[] types)
        {
            if ((types != null) && (types.Length > 0))
            {
				return SerializerCache.GetSerializer(typeof(ConfigurationSettings), types);
                //return new XmlSerializer(typeof(ConfigurationSettings), types);
            }
            return new XmlSerializer(typeof(ConfigurationSettings));
        }

        /// <devdoc>
        /// Full demand needed to protect Type.GetType. LinkDemand insufficient because that only
        /// checks permissions of calling code, which would be Create, which has full 
        /// trust
        /// </devdoc>
        [ReflectionPermission(SecurityAction.Demand, TypeInformation = true)]
        private static Type[] GetTypes(XPathNavigator navigator)
        {
            XPathExpression expr = navigator.Compile(string.Concat("//", xmlIncludeTypeXPath));
            XmlNamespaceManager nsmgr = GetNamespaceManager(navigator);
            expr.SetContext(nsmgr);
            XPathNodeIterator iterator = navigator.Select(expr);
            
            Type[] types = null;
            int index = 0;
            Type t = null;
            XmlIncludeTypeData xmlIncludeTypeData = null;
            types = new Type[iterator.Count];
            while (iterator.MoveNext())
            {
                xmlIncludeTypeData = new XmlIncludeTypeData(iterator.Current.GetAttribute("name", string.Empty), 
                    iterator.Current.GetAttribute("type", string.Empty));
                t = Type.GetType(xmlIncludeTypeData.TypeName, true, true);
                types[index++] = t;
            }
            return types;
        }

        private static XmlNamespaceManager GetNamespaceManager(XPathNavigator navigator)
        {
            XmlNamespaceManager nsmngr = new XmlNamespaceManager(navigator.NameTable);
            nsmngr.AddNamespace("elib", ConfigurationSettings.ConfigurationNamespace);
            return nsmngr;
        }
    }
}