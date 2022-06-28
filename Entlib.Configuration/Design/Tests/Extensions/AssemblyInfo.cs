//===============================================================================
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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Tests.Extensions;

#if UNIT_TESTS
[assembly : ConfigurationDesignManager(typeof(ExtensionsConfigurationDesignManager))]
#endif

[assembly: ReflectionPermission(SecurityAction.RequestMinimum, MemberAccess=true)]

[assembly: AssemblyTitle("Enterprise Library Configuration Extensions Design Tests")]
[assembly: AssemblyDescription("Enterprise Library Configuration Extensions Design Tests")]
[assembly : AssemblyVersion("1.0.0.0")]

