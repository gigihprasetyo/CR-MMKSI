//==============================================================================
// Microsoft patterns & practices Enterprise Library
// 
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Cryptography;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Storage;
using System.Threading;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage
{
    /// <summary>
    /// <para>
    /// Represents a storage provider for configuration data that saves the configuration data to a SQL Server database instance.
    /// </para>
    /// </summary>
    [SqlClientPermission(SecurityAction.Demand, Unrestricted=true)]
	public class SqlStorageProvider : StorageProvider, IStorageProviderWriter
    {
        private RuntimeConfigurationView runtimeConfigurationView;

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="SqlStorageProvider"/> class.</para>
        /// </summary>
        public SqlStorageProvider() : base()
        {
        }

        /// <summary>
        /// <para>Creates a new <see cref="ConfigurationChangeSqlWatcher"/> for this configuration storage.</para>
        /// </summary>
        /// <returns>
        /// <para>An <see cref="ConfigurationChangeSqlWatcher"/> instance.</para>
        /// </returns>
        public override IConfigurationChangeWatcher CreateConfigurationChangeWatcher()
        {
            SqlStorageProviderData sqlStorageProviderData = GetSqlStorageProviderData();
            return new ConfigurationChangeSqlWatcher(sqlStorageProviderData.ConnectionString, sqlStorageProviderData.GetStoredProcedure, sqlStorageProviderData.SetStoredProcedure, CurrentSectionName);
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
			XmlDocument xmlApplicationDocument = LoadData();
			if (xmlApplicationDocument == null)
			{
				throw new ConfigurationException(SR.ExceptionSqlStorageSectionNotFoundError(CurrentSectionName, GetStoredProcedure));
			}
			XmlNode sectionNode = xmlApplicationDocument.SelectSingleNode(CurrentSectionName);
			if (sectionNode == null)
			{
				throw new ConfigurationException(SR.ExceptionSqlStorageSectionNotFoundError(CurrentSectionName, GetStoredProcedure));
			}
			sectionNode = sectionNode.CloneNode(true);
			// since we wrapped the section name here in the storage provider
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

        private XmlDocument LoadData()
        {
            string xmlData;
			
            using (SqlConnection myConnection = new SqlConnection(ConnectionString.ToString()))
            {
                try
                {
                    // Create Instance of Connection and Command Object
                    SqlCommand myCommand = new SqlCommand(GetStoredProcedure, myConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter parameterSectionName = new SqlParameter(@"@SectionName", SqlDbType.NVarChar);
                    parameterSectionName.Value = this.CurrentSectionName;
                    myCommand.Parameters.Add(parameterSectionName);

                    // Execute the command
                    myConnection.Open();
                    using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }
                        xmlData = reader.IsDBNull(0) ? null : reader.GetString(0);
                    }
                }
                catch (ThreadInterruptedException threadEx)
                {
                    System.Diagnostics.Debug.WriteLine(threadEx.Source + " : " + threadEx.Message);
                    throw (threadEx);
                }
                catch (SqlException e)
                {
                    throw new ConfigurationException(SR.ExceptionConfigurationSqlInvalidSection(CurrentSectionName), e);
                }
            }

            if (xmlData == null || xmlData.Trim().Equals(String.Empty))
            {
                throw new ConfigurationException(SR.ExceptionConfigurationSqlInvalidSection(CurrentSectionName));
            }

            return DeserializeDocumentData(xmlData);
        }

        private XmlDocument DeserializeDocumentData(string xmlData)
        {
            XmlDocument xmlDoc = null;
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
                            encryptedBytes = Convert.FromBase64String(xmlData);
                            decryptedBytes = protector.Decrypt(encryptedBytes);
                            xmlData = Encoding.UTF8.GetString(decryptedBytes);

                        }
                        finally
                        {
                            CryptographyUtility.ZeroOutBytes(decryptedBytes);
                            CryptographyUtility.ZeroOutBytes(encryptedBytes);
                        }
                    }

                    if (xmlData.Length > 0)
                    {
                        xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xmlData);
                    }
                }
            }
            catch (XmlException e)
            {
                throw new ConfigurationException(SR.ExceptionConfigurationSqlInvalidSection(CurrentSectionName), e);
            }

            return xmlDoc;
        }

        private string GetStoredProcedure
        {
            get
            {
                SqlStorageProviderData sqlStorageProviderData = GetSqlStorageProviderData();
                return sqlStorageProviderData.GetStoredProcedure;
            }
        }

        private string ConnectionString
        {
            get
            {
                SqlStorageProviderData sqlStorageProviderData = GetSqlStorageProviderData();
                return sqlStorageProviderData.ConnectionString;
            }
        }

        private string SetStoredProcedure
        {
            get
            {
                SqlStorageProviderData sqlStorageProviderData = GetSqlStorageProviderData();
                return sqlStorageProviderData.SetStoredProcedure;
            }
        }

        private SqlStorageProviderData GetSqlStorageProviderData()
        {
            StorageProviderData storageProviderData = runtimeConfigurationView.GetStorageProviderData(CurrentSectionName);
            ArgumentValidation.CheckExpectedType(storageProviderData, typeof(SqlStorageProviderData));
            SqlStorageProviderData sqlStorageProviderData = (SqlStorageProviderData)storageProviderData;
            return sqlStorageProviderData;
        }

        private void GetStorageCursor(ConfigurationView configurationView)
        {
            ArgumentValidation.CheckExpectedType(configurationView, typeof(RuntimeConfigurationView));

            runtimeConfigurationView = (RuntimeConfigurationView)configurationView;
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

        private void SaveData(XmlDocument xmlDoc)
        {
            using (ConfigurationProtector protector = this.runtimeConfigurationView.GetConfigurationProtector(CurrentSectionName))
            {
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

                // Create Instance of Connection and Command Object
                using (SqlConnection myConnection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        SqlCommand myCommand = new SqlCommand(SetStoredProcedure, myConnection);
                        myCommand.CommandType = CommandType.StoredProcedure;
		
                        SqlParameter sectionNameParameter = new SqlParameter( @"@section_name", SqlDbType.NVarChar );
                        sectionNameParameter.Value = CurrentSectionName;
                        myCommand.Parameters.Add(sectionNameParameter);

                        SqlParameter sectionValueParameter = new SqlParameter( @"@section_value", SqlDbType.NText );
                        sectionValueParameter.Value = paramValue;
                        myCommand.Parameters.Add(sectionValueParameter);
		
                        // Execute the command
                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                    }
                    catch( Exception e )
                    {
                        throw new ConfigurationException(SR.ExceptionConfigurationSqlCantSet(CurrentSectionName),e);
                    }
                }
            }
        }
    }
}