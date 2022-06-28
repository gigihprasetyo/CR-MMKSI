#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AppConfigs  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad Ridwan - Initial
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Framework
{
    public class AppConfigs
    {
        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="configName">Name of the configuration.</param>
        /// <returns></returns>
        public static string GetString(string configName)
        {
            return GetAppSetting(configName);
        }

        /// <summary>
        /// Gets the int.
        /// <param name="configName">Name of the configuration.</param>
        /// <returns></returns>
        public static int GetInt(string configName)
        {
            int appSetting = 0;
            try
            {
                int.TryParse(GetAppSetting(configName), out appSetting);
            }
            catch
            {
                appSetting = 0;
            }

            return appSetting;
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="configName">Name of the configuration.</param>
        /// <returns></returns>
        public static bool GetBoolean(string configName)
        {
            bool boolConfig = false;
            try
            {
                bool.TryParse(GetAppSetting(configName), out boolConfig);
            }
            catch
            {
                boolConfig = false;
            }

            return boolConfig;
        }

        /// <summary>
        /// Gets list of string.
        /// </summary>
        /// <param name="configName">Name of the configuration.</param>
        /// <returns></returns>
        public static List<string> GetList(string configName, char separator)
        {
            string listStr = GetAppSetting(configName);
            if (string.IsNullOrEmpty(listStr))
            {
                return new List<string>();
            }
            else
            {
                return listStr.Split(separator).ToList();
            }
        }

        /// <summary>
        /// Gets list of string from section
        /// </summary>
        /// <param name="configName">Name of the configuration.</param>
        /// <returns></returns>
        public static List<T> GetListFromSection<T>(string sectionName)
        {
            IDictionary dictionaries = GetListFromSection(sectionName);
            List<T> result = new List<T>();

            if (dictionaries == null)
            {
                return new List<T>();
            }


            foreach (string key in dictionaries.Keys)
            {

                result.Add((T)Convert.ChangeType(dictionaries[key], typeof(T)));
            }

            return result;
        }

        /// <summary>
        /// Gets list of string from section
        /// </summary>
        /// <param name="configName">Name of the configuration.</param>
        /// <returns></returns>
        public static IDictionary GetListFromSection(string sectionName)
        {
            return (IDictionary)ConfigurationManager.GetSection(sectionName);
        }

        /// Connections the string.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <returns></returns>
        public static string ConnectionString(string server, string dbName, string username, string password)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                InitialCatalog = dbName,
                UserID = username,
                Password = password
            };

            return connectionStringBuilder.ConnectionString;
        }

        /// Connections the string.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <returns></returns>
        public static string ConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        /// <summary>
        /// Gets the application setting.
        /// </summary>
        /// <param name="configName">Name of the configuration.</param>
        /// <returns></returns>
        private static string GetAppSetting(string configName)
        {
            try
            {
                return ConfigurationManager.AppSettings[configName].ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
