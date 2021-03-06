//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Configuration Application Block
//===============================================================================
// Copyright ? Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

#if UNIT_TESTS
namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Tests 
{
    public abstract class MyAbstractDerivedNode : StorageProviderNode
    {
        protected MyAbstractDerivedNode(StorageProviderData storageProviderData) : base(storageProviderData)
        {
        }
            
    }
}
#endif