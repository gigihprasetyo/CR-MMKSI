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

namespace Microsoft.Practices.EnterpriseLibrary.Configuration
{
    /// <summary>
    /// <para>
    /// Provides a facade to configuration settings from defined storage in a specified configuration section. 
    /// </para>
    /// </summary> 
    public sealed class ConfigurationManager 
    {
        private static volatile ConfigurationManager instance;
        private ConfigurationContext currentContext;
        private static object syncObject = new object();

        /// <summary>
        /// <para>Occurs before configuration is changed.</para>
        /// </summary>
        public static event ConfigurationChangingEventHandler ConfigurationChanging;

        /// <summary>
        /// <para>Occurs after configuration is changed.</para>
        /// </summary>
        public static event ConfigurationChangedEventHandler ConfigurationChanged;

        private ConfigurationManager()
        {
            this.currentContext = new ConfigurationContext(new NonDisposingWrapper(new ConfigurationBuilder())); 
            this.currentContext.ConfigurationChanging += new ConfigurationChangingEventHandler(OnConfigurationChanging);
            this.currentContext.ConfigurationChanged += new ConfigurationChangedEventHandler(OnConfigurationChanged);
        }

        /// <devdoc>
        /// The singleton instance to use with static methods.
        /// </devdoc>
        // see this post (http://blogs.msdn.com/brada/archive/2004/05/12/130935.aspx) on why we do this
        internal static ConfigurationManager Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncObject)
                    {
                        if (instance == null)
                        {
                            instance = new ConfigurationManager();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// <para>
        /// Returns configuration settings for a user-defined configuration section.
        /// </para>
        /// </summary>
        /// <param name="sectionName">
        /// <para>The configuration section to read.</para>
        /// </param>
        /// <returns>
        /// <para>The configuration settings for <paramref name="sectionName"/>.</para>
        /// </returns>
        /// <remarks>
        /// <para>Once a section is read, the data for that section is cached and it will always return the same data.</para>
        /// </remarks>
        public static object GetConfiguration(string sectionName)
        {
            return Current.currentContext.GetConfiguration(sectionName);
        }

        /// <summary>
        /// <para>
        /// Write configuration for a section to storage.
        /// </para>
        /// </summary>
        /// <param name="sectionName">
        /// <para>The name of the section for the configuration data.</para>
        /// </param>
        /// <param name="configValue">
        /// <para>The configuration value to store.</para>
        /// </param>
        /// <exception cref="System.Configuration.ConfigurationException">
        /// <para><paramref name="sectionName"/> is not valid section for this configuration.</para>
        /// <para>- or -</para>
        /// <para>The section data is read only.</para>
        /// <para>- or -</para>
        /// <para>An error occured while reading the configuration to save the data.</para>
        /// </exception>
        public static void WriteConfiguration(string sectionName, object configValue)
        {
            Current.currentContext.WriteConfiguration(sectionName, configValue);
        }

        /// <summary>
        /// <para>Gets the <see cref="ConfigurationContext"/> for the current <see cref="ConfigurationManager"/>.</para>
        /// </summary>
        /// <returns><para>A <see cref="ConfigurationContext"/> object.</para></returns>
        public static ConfigurationContext GetCurrentContext()
        {
            return Current.currentContext;
        }

        /// <summary>
        /// <para>Creates a new instance of the <see cref="ConfigurationContext"/> class with the specified <see cref="ConfigurationDictionary"/>.</para>
        /// </summary>
        /// <param name="dictionary"><para>A <see cref="ConfigurationDictionary"/>.</para></param>
        /// <returns>
        /// <para>A <see cref="ConfigurationContext"/> object.</para>
        /// </returns>
        public static ConfigurationContext CreateContext(ConfigurationDictionary dictionary)
        {
            return new ConfigurationContext(dictionary);
        }

        /// <summary>
        /// <para>Creates a new instance of a <see cref="ConfigurationContext"/> object.</para>
        /// </summary>
        /// <returns>
        /// <para>A <see cref="ConfigurationContext"/> object.</para>
        /// </returns>
        public static ConfigurationContext CreateContext()
        {
            return new ConfigurationContext();
        }

        /// <summary>
        /// <para>Creates a new instance of a <see cref="ConfigurationContext"/> object for the given <paraname ref="configurationFile"/>.</para>
        /// </summary>
        /// <param name="configurationFile">
        /// <para>The configuration file that contains the meta configuration.</para>
        /// </param>
        /// <returns>
        /// <para>A <see cref="ConfigurationContext"/> object.</para>
        /// </returns>
        public static ConfigurationContext CreateContext(string configurationFile)
        {
            return new ConfigurationContext(configurationFile);
        }

        /// <summary>
        /// <para>Removes a section from the internal cache.</para>
        /// </summary>
        /// <param name="section">
        /// <para>The section name to remove.</para>
        /// </param>
        public static void ClearSingletonSectionCache(string section)
        {
            Current.currentContext.ClearSectionCache(section);
        }

        /// <summary>
        /// <para>Removes all sections from the internal cache.</para>
        /// </summary>
        public static void ClearSingletonSectionCache()
        {
            Current.currentContext.ClearSectionCache();
        }

        /// <devdoc>
        /// Raises the ConfigurationChanged event.
        /// </devdoc>
        private void OnConfigurationChanged(object sender, ConfigurationChangedEventArgs args)
        {
            if(ConfigurationChanged != null)
            {
                ConfigurationChanged(sender, args);
            }
        }

        /// <devdoc>
        /// Raises the ConfigurationChanging event.
        /// </devdoc>
        private void OnConfigurationChanging(object sender, ConfigurationChangingEventArgs args)
        {
            if (ConfigurationChanging != null)
            {
                ConfigurationChanging(sender, args);
            }
        }
    }
}