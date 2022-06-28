//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Reflection;
using System.Security.Permissions;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Design;

[assembly : ConfigurationDesignManager(typeof(DataConfigurationDesignManager))]

[assembly : ReflectionPermission(SecurityAction.RequestMinimum, MemberAccess=true)]

[assembly : AssemblyTitle("Enterprise Library Data Access Application Block Design")]
[assembly : AssemblyDescription("Enterprise Library Data Access Application Block Design")]
[assembly : AssemblyVersion("1.1.0.0")]

[assembly : AssemblyDelaySign(false)]
[assembly : AssemblyKeyFile("")]
[assembly : AssemblyKeyName("")]