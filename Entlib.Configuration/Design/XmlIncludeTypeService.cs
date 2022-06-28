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
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Transformer;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design
{
    /// <devdoc>
    /// Provides a service for registering Type objects that will be included for an XmlSerializerTransformer for a section.
    /// </devdoc>
    internal class XmlIncludeTypeService : IXmlIncludeTypeService
    {
        private Hashtable includeTypesBySection;
        
        public XmlIncludeTypeService()
        {
            includeTypesBySection = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
        }

        /// <devdoc>
        /// Gets the types for the section.
        /// </devdoc>
        public Type[] GetXmlIncludeTypes(string sectionName)
        {
            if (!includeTypesBySection.Contains(sectionName))
            {
                return null;
            }
            Hashtable list = (Hashtable)includeTypesBySection[sectionName];
            Type[] types = new Type[list.Count];
            list.Values.CopyTo(types, 0);
            return types;
        }

        /// <devdoc>
        /// Add a type for a section.
        /// </devdoc>
        public void AddXmlIncludeType(string sectionName, Type type)
        {
            if (sectionName == null)
            {
                throw new ArgumentNullException("sectionName");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (!includeTypesBySection.Contains(sectionName))
            {
                includeTypesBySection[sectionName] = new Hashtable(5);
            }
            Hashtable table = (Hashtable)includeTypesBySection[sectionName];
            table[type.FullName] = type;
        }

        internal static Type[] GetXmlIncludeTypes(IServiceProvider serviceProvider, string sectionName, IUIHierarchy hierarchy)
        {
            Type[] includeTypes = null;
            
            IXmlIncludeTypeService service = ServiceHelper.GetXmlIncludeTypeService(serviceProvider);
            
            includeTypes = service.GetXmlIncludeTypes(sectionName);
            if (includeTypes == null) return includeTypes;

            INodeCreationService nodeCreationService = ServiceHelper.GetNodeCreationService(serviceProvider);

            // make sure that we actually used these xml include types in the application
            ArrayList usedTypes = new ArrayList();
            foreach (Type includeType in includeTypes)
            {
                Type nodeType = nodeCreationService.GetNodeType(includeType);
                if (nodeType == null) continue;
                if (hierarchy.ContainsNodeType(nodeType))
                {
                    usedTypes.Add(includeType);    
                }
            }
            includeTypes = new Type[usedTypes.Count];
            usedTypes.CopyTo(includeTypes);
            return includeTypes;
        }
    }
}