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

using Microsoft.Win32;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Storage
{
    /// <summary>
    /// <para></para>Represents the possible values for to use with the <see cref="RegistryStorageProvider"/>.
    /// </summary>
    public enum AllowedRegistryHive
    {
        /// <summary>
        /// <para>Represents the HKEY_CURRENT_USER base key.</para>
        /// </summary>
        CurrentUser = RegistryHive.CurrentUser,
        /// <summary>
        /// <para>Represents the HKEY_LOCAL_MACHINE base key.</para>
        /// </summary>
        LocalMachine = RegistryHive.LocalMachine,
        /// <summary>
        /// <para>Represents the HKEY_USERS base key.</para>
        /// </summary>
        Users = RegistryHive.Users
    }
}
