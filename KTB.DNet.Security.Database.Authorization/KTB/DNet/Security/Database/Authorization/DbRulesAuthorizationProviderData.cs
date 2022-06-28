namespace KTB.DNet.Security.Database.Authorization
{
    using Microsoft.Practices.EnterpriseLibrary.Security.Configuration;
    using System;
    using System.Xml.Serialization;

    [XmlRoot("authorizationProvider", Namespace="http://www.microsoft.com/practices/enterpriselibrary/08-31-2004/security")]
    public class DbRulesAuthorizationProviderData : AuthorizationProviderData
    {
        private string database;

        public DbRulesAuthorizationProviderData()
        {
        }

        public DbRulesAuthorizationProviderData(string name) : this(name, string.Empty)
        {
        }

        public DbRulesAuthorizationProviderData(string name, string database) : base(name)
        {
            this.database = database;
        }

        [XmlAttribute(AttributeName="database")]
        public string Database
        {
            get
            {
                return this.database;
            }
            set
            {
                this.database = value;
            }
        }

        [XmlIgnore]
        public override string TypeName
        {
            get
            {
                return typeof(DbRulesAuthorizationProvider).AssemblyQualifiedName;
            }
            set
            {
            }
        }
    }
}

