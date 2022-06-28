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

using Microsoft.Practices.EnterpriseLibrary.Configuration.Storage;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration
{
    /// <summary>
    /// <para>Represents an <see cref="IConfigurationChangeWatcher"/> that does nothing.</para>
    /// </summary>
    public sealed class NullConfigurationChangeWatcher : IConfigurationChangeWatcher
    {
        private readonly string configurationSectionName;

        /// <summary>
        /// <para>Initialize a new <see cref="NullConfigurationChangeWatcher"/> class with the name of the section</para>
        /// </summary>
        /// <param name="configurationSectionName">
        /// <para>The name of the configuration section to watch.</para>
        /// </param>
        public NullConfigurationChangeWatcher(string configurationSectionName)
        {
            this.configurationSectionName = configurationSectionName;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose(){}

        /// <summary>
        /// <para>Gets the name of the configuration section being watched.</para>
        /// </summary>
        /// <value>
        /// <para>The name of the configuration section being watched.</para>
        /// </value>
        public string SectionName
        {
            get { return configurationSectionName; }
        }

        /// <summary>
        /// <para>Event raised when the underlying persistence mechanism for configuration notices that the persistent representation of configuration information has changed.</para>
        /// </summary>
        public event ConfigurationChangedEventHandler ConfigurationChanged;

        /// <summary>
        /// <para>Starts watching for configuration changes.</para>
        /// </summary>
        public void StartWatching(){}

        /// <summary>
        /// <para>Stops watching for configuration changes.</para>
        /// </summary>
        public void StopWatching(){}
    }
}
