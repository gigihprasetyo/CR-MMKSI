namespace KTB.DNet.Security.Database.Configuration.Design
{
    using KTB.DNet.Security.Database.Authorization;
    using KTB.DNet.Security.Database.Authorization.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
    using System;
    using System.Diagnostics;

    public class DBRulesAuthConfigurationDesignManager : IConfigurationDesignManager
    {
        public void BuildContext(IServiceProvider serviceProvider, ConfigurationDictionary configurationDictionary)
        {
        }

        public void Open(IServiceProvider serviceProvider)
        {
        }

        public void Register(IServiceProvider serviceProvider)
        {
            RegisterNodeTypes(serviceProvider);
            RegisterXmlIncludeTypes(serviceProvider);
        }

        private static void RegisterNodeTypes(IServiceProvider serviceProvider)
        {
            INodeCreationService nodeCreationService = ServiceHelper.GetNodeCreationService(serviceProvider);
            Type nodeType = typeof(DbRulesAuthorizationProviderNode);
            NodeCreationEntry nodeCreationEntry = NodeCreationEntry.CreateNodeCreationEntryWithMultiples(new AddChildNodeCommand(serviceProvider, nodeType), nodeType, typeof(DbRulesAuthorizationProviderData), "Database Authorization Provider");
            nodeCreationService.AddNodeCreationEntry(nodeCreationEntry);
        }

        private static void RegisterXmlIncludeTypes(IServiceProvider serviceProvider)
        {
            IXmlIncludeTypeService service = serviceProvider.GetService(typeof(IXmlIncludeTypeService)) as IXmlIncludeTypeService;
            Debug.Assert(service != null, "Could not find the IXmlIncludeTypeService");
            service.AddXmlIncludeType("securityConfiguration", typeof(DbRulesAuthorizationProviderData));
        }

        public void Save(IServiceProvider serviceProvider)
        {
        }
    }
}

