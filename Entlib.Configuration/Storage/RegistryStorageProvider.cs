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
using System.Security.Permissions;
using System.Text;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Cryptography;
using Microsoft.Win32;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage
{
    /// <summary>
    /// <para>
    /// Represents a storage provider for configuration data that saves the configuration data to the registry.
    /// </para>
    /// </summary>
	[System.Security.Permissions.RegistryPermission(SecurityAction.Demand, Unrestricted=true)]
	public class RegistryStorageProvider : StorageProvider, IStorageProviderWriter
    {
        private RuntimeConfigurationView runtimeConfigurationView;

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="RegistryStorageProvider"/> class.</para>
        /// </summary>
        public RegistryStorageProvider()
        {
        }

        /// <summary>
        /// <para>Creates a new <see cref="ConfigurationChangedRegistryWatcher"/> for this configuration storage.</para>
        /// </summary>
        /// <returns>
        /// <para>An <see cref="ConfigurationChangedRegistryWatcher"/> instance.</para>
        /// </returns>
        public override IConfigurationChangeWatcher CreateConfigurationChangeWatcher()
        {
            return new ConfigurationChangedRegistryWatcher(RegistryRoot, RegistrySubkey, CurrentSectionName);
        }

        /// <summary>
        /// <para>Initializes this provider to the correct state and context used by the factory creating it.</para>
        /// </summary>
        /// <param name="configurationView">
        /// <para>The cursor to use to get the data specific for the transformer.</para>
        /// </param>
        public override void Initialize(ConfigurationView configurationView)
        {
            ArgumentValidation.CheckForNullReference(configurationView, "configurationView");

            GetStorageCursor(configurationView);
        }

        /// <summary>
        /// <para>Reads the configuration from storage. The data will come back as an <see cref="XmlNode"/>.</para>
        /// </summary>        
        /// <returns>
        /// <para>The configuration data for the section.</para>
        /// </returns>
        public override object Read()
        {
            XmlDocument xmlApplicationDocument = null;
            using (RegistryKey keyParent = GetRegistrySubKey(RegistryRoot, RegistrySubkey, false))
            {
                if (keyParent == null)
                {
                    throw new ConfigurationException(SR.ExceptionConfigurationRegistryKeyInvalid(String.Format("{0}\\{1}", RegistryRoot, RegistrySubkey)));
                }

                using (RegistryKey keySection = keyParent.OpenSubKey(CurrentSectionName))
                {
                    if (keySection == null)
                    {
                        throw new ConfigurationException(SR.ExceptionConfigurationRegistryKeyInvalid(CurrentSectionName));
                    }

                    xmlApplicationDocument = LoadData(keySection);
                }
            }

			if (xmlApplicationDocument == null)
			{
				throw new ConfigurationException(SR.ExceptionRegistryStorageSectionNotFoundError(CurrentSectionName, RegistrySubkey));
			}
			XmlNode sectionNode = xmlApplicationDocument.SelectSingleNode(CurrentSectionName);
			if (sectionNode == null)
			{
				throw new ConfigurationException(SR.ExceptionRegistryStorageSectionNotFoundError(CurrentSectionName, RegistrySubkey));
			}
			sectionNode = sectionNode.CloneNode(true);
			// since we wrapped the section name here in the file storage provider
			return sectionNode.FirstChild;

        }

        /// <summary>
        /// Writes the configuration data to storage.
        /// </summary>
        /// <param name="value">
        /// <para>The value to write to storage. The type must be <see cref="XmlNode"/></para>
        /// </param>
        public void Write(object value)
        {
            ArgumentValidation.CheckForNullReference(value, "value");
            ArgumentValidation.CheckExpectedType(value, typeof(XmlNode));

            XmlNode valueNode = (XmlNode)value;
            XmlDocument xmlDoc = new XmlDocument();
            CreateXmlDeclaration(xmlDoc);
            XmlNode sectionNode = xmlDoc.CreateElement(CurrentSectionName);
            xmlDoc.AppendChild(sectionNode);
            CloneNode(valueNode, sectionNode);
            SaveData(xmlDoc);
        }

        private string RegistrySubkey
        {
            get
            {
                RegistryStorageProviderData registryStorageProviderData = GetRegistryStorageProviderData();
                return registryStorageProviderData.RegistrySubKey;
            }
        }

        private AllowedRegistryHive RegistryRoot
        {
            get
            {
                RegistryStorageProviderData registryStorageProviderData = GetRegistryStorageProviderData();
                return registryStorageProviderData.RegistryRoot;
            }
        }

        private static void CreateXmlDeclaration(XmlDocument xmlDoc)
        {
            if (!(xmlDoc.FirstChild is XmlDeclaration))
            {
                XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                if (xmlDoc.HasChildNodes)
                {
                    xmlDoc.InsertBefore(declaration, xmlDoc.ChildNodes[0]);
                }
                else
                {
                    xmlDoc.AppendChild(declaration);
                }

            }
        }

        private static void CloneNode(XmlNode value, XmlNode sectionNode)
        {
            XmlNode cloneNode = sectionNode.OwnerDocument.ImportNode(value, true);
            sectionNode.AppendChild(cloneNode);
        }

        private XmlDocument LoadData(RegistryKey regKey)
        {
            XmlDocument doc = new XmlDocument();

            string strDocData = (string)regKey.GetValue("value");

            try
            {
                using (ConfigurationProtector protector = runtimeConfigurationView.GetConfigurationProtector(CurrentSectionName))
                {
                    if (protector.Encrypted)
                    {
                        byte[] decryptedBytes = null;
                        byte[] encryptedBytes = null;
                        try
                        {
                            encryptedBytes = Convert.FromBase64String(strDocData);
                            decryptedBytes = protector.Decrypt(encryptedBytes);
                            strDocData = Encoding.UTF8.GetString(decryptedBytes);

                        }
                        finally
                        {
                            CryptographyUtility.ZeroOutBytes(decryptedBytes);
                            CryptographyUtility.ZeroOutBytes(encryptedBytes);
                        }
                    }

                    if (strDocData.Length > 0)
                    {
                        doc.LoadXml(strDocData);
                    }
                }
            }
            catch (XmlException e)
            {
                throw new ConfigurationException(SR.ExceptionConfigurationRegistryKeyInvalid(regKey.Name), e);
            }

            return doc;
        }

        private void SaveData(XmlDocument xmlDoc)
        {
            using (ConfigurationProtector protector = this.runtimeConfigurationView.GetConfigurationProtector(CurrentSectionName))
            {
                string paramSignature = "";
                string paramValue = xmlDoc.OuterXml;

                if (protector.Encrypted)
                {
                    byte[] decryptedBytes = null;
                    byte[] encryptedBytes = null;
                    try
                    {
                        decryptedBytes = Encoding.UTF8.GetBytes(paramValue);
                        encryptedBytes = protector.Encrypt(decryptedBytes);
                        paramValue = Convert.ToBase64String(encryptedBytes);
                    }
                    finally
                    {
                        CryptographyUtility.ZeroOutBytes(decryptedBytes);
                        CryptographyUtility.ZeroOutBytes(encryptedBytes);
                    }
                }

                using (RegistryKey keyParent = GetRegistrySubKey(RegistryRoot, RegistrySubkey, true))
                {
                    RegistryKey keySection = keyParent.OpenSubKey(CurrentSectionName, true);
                
                    if (keySection == null)
                    {
                        keySection = keyParent.CreateSubKey(CurrentSectionName);
                    }

                    using (keySection)
                    {
                        keySection.SetValue("value", paramValue);
                        keySection.SetValue("signature", paramSignature);
                    }
                }
            }
        }

        private RegistryKey GetRegistrySubKey(AllowedRegistryHive root, string subKey, bool writable)
        {
            RegistryKey subKeyObject = null;
            switch (root)
            {
                case AllowedRegistryHive.CurrentUser:
                    subKeyObject = Registry.CurrentUser.OpenSubKey(subKey, writable);
                    if (subKeyObject == null && writable)
                    {
                        subKeyObject = Registry.CurrentUser.CreateSubKey(subKey);
                    }
                    break;
                case AllowedRegistryHive.LocalMachine:
                    subKeyObject = Registry.LocalMachine.OpenSubKey(subKey, writable);
                    if (subKeyObject == null && writable)
                    {
                        subKeyObject = Registry.LocalMachine.CreateSubKey(subKey);
                    }
                    break;
                case AllowedRegistryHive.Users:
                    subKeyObject = Registry.Users.OpenSubKey(subKey, writable);
                    if (subKeyObject == null && writable)
                    {
                        subKeyObject = Registry.Users.CreateSubKey(subKey);
                    }
                    break;
            }

            return subKeyObject;
        }

        private RegistryStorageProviderData GetRegistryStorageProviderData()
        {
            StorageProviderData storageProviderData = runtimeConfigurationView.GetStorageProviderData(CurrentSectionName);
            ArgumentValidation.CheckExpectedType(storageProviderData, typeof(RegistryStorageProviderData));
            return (RegistryStorageProviderData)storageProviderData;
        }

        private void GetStorageCursor(ConfigurationView configurationView)
        {
            ArgumentValidation.CheckExpectedType(configurationView, typeof(RuntimeConfigurationView));

            runtimeConfigurationView = (RuntimeConfigurationView)configurationView;
        }

    }
}