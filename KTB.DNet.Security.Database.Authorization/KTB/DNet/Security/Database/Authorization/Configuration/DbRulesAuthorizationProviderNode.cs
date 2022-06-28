namespace KTB.DNet.Security.Database.Authorization.Configuration
{
    using KTB.DNet.Security.Database.Authorization;
    using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
    using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Design;
    using Microsoft.Practices.EnterpriseLibrary.Security.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Security.Configuration.Design;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing.Design;

    public class DbRulesAuthorizationProviderNode : AuthorizationProviderNode
    {
        private const string applicationName = "My Application";
        private InstanceNode database;
        private KTB.DNet.Security.Database.Authorization.DbRulesAuthorizationProviderData DbRulesAuthorizationProviderData;
        private ConfigurationNodeChangedEventHandler onDatabaseRemoved;
        private ConfigurationNodeChangedEventHandler onDatabaseRenamed;

        public DbRulesAuthorizationProviderNode() : this(new KTB.DNet.Security.Database.Authorization.DbRulesAuthorizationProviderData("Database Authorization Provider", string.Empty))
        {
        }

        public DbRulesAuthorizationProviderNode(KTB.DNet.Security.Database.Authorization.DbRulesAuthorizationProviderData DbRulesAuthorizationProviderData) : base(DbRulesAuthorizationProviderData)
        {
            this.DbRulesAuthorizationProviderData = DbRulesAuthorizationProviderData;
            this.onDatabaseRemoved = new ConfigurationNodeChangedEventHandler(this.OnDatabaseRemoved);
            this.onDatabaseRenamed = new ConfigurationNodeChangedEventHandler(this.OnDatabaseRenamed);
        }

        protected override void AddDefaultChildNodes()
        {
            base.AddDefaultChildNodes();
            this.CreateDatabaseSettingsNode();
        }

        private void CreateDatabaseSettingsNode()
        {
            if (!this.DatabaseSettingsNodeExists())
            {
                new AddConfigurationSectionCommand(this.Site, typeof(DatabaseSettingsNode), "dataConfiguration").Execute(base.Hierarchy.RootNode);
            }
        }

        private bool DatabaseSettingsNodeExists()
        {
            DatabaseSettingsNode node = base.Hierarchy.FindNodeByType(typeof(DatabaseSettingsNode)) as DatabaseSettingsNode;
            return (node != null);
        }

        private void OnDatabaseRemoved(object sender, ConfigurationNodeChangedEventArgs args)
        {
            this.Database = null;
        }

        private void OnDatabaseRenamed(object sender, ConfigurationNodeChangedEventArgs args)
        {
            this.DbRulesAuthorizationProviderData.Database = args.Node.Name;
        }

        public override void ResolveNodeReferences()
        {
            base.ResolveNodeReferences();
            DatabaseSettingsNode node = base.Hierarchy.FindNodeByType(typeof(DatabaseSettingsNode)) as DatabaseSettingsNode;
            Debug.Assert(node != null, "How is it that the database settings are not there?");
            InstanceCollectionNode parent = base.Hierarchy.FindNodeByType(typeof(InstanceCollectionNode)) as InstanceCollectionNode;
            this.Database = base.Hierarchy.FindNodeByName(parent, this.DbRulesAuthorizationProviderData.Database) as InstanceNode;
        }

        [Browsable(false)]
        public override Microsoft.Practices.EnterpriseLibrary.Security.Configuration.AuthorizationProviderData AuthorizationProviderData
        {
            get
            {
                return base.AuthorizationProviderData;
            }
        }

        [Description("The database instance that will be used to query for rules"), ReferenceType(typeof(InstanceNode)), Editor(typeof(ReferenceEditor), typeof(UITypeEditor)), Required]
        public InstanceNode Database
        {
            get
            {
                return this.database;
            }
            set
            {
                ILinkNodeService service = this.GetService(typeof(ILinkNodeService)) as ILinkNodeService;
                Debug.Assert(service != null, "Could not get the ILinkNodeService");
                this.database = (InstanceNode) service.CreateReference(this.database, value, this.onDatabaseRemoved, this.onDatabaseRenamed);
                this.DbRulesAuthorizationProviderData.Database = string.Empty;
                if (this.database != null)
                {
                    this.DbRulesAuthorizationProviderData.Database = this.database.Name;
                }
            }
        }

        [Browsable(false)]
        public override string TypeName
        {
            get
            {
                return typeof(DbRulesAuthorizationProvider).AssemblyQualifiedName;
            }
        }
    }
}

