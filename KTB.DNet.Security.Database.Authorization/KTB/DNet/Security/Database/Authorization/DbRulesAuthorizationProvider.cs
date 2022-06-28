namespace KTB.DNet.Security.Database.Authorization
{
    using Microsoft.Practices.EnterpriseLibrary.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Security;
    using Microsoft.Practices.EnterpriseLibrary.Security.Authorization;
    using Microsoft.Practices.EnterpriseLibrary.Security.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Security.Instrumentation;
    using System;
    using System.Security.Principal;

    public class DbRulesAuthorizationProvider : ConfigurationProvider, IAuthorizationProvider, IConfigurationProvider
    {
        private string database;
        private DbRulesManager mgr = null;
        private SecurityConfigurationView securityConfigurationView;

        public bool Authorize(IPrincipal principal, string context)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }
            if ((context == null) || (context.Length == 0))
            {
                throw new ArgumentNullException("context");
            }
            if (this.mgr == null)
            {
                this.mgr = new DbRulesManager(this.database, this.securityConfigurationView.ConfigurationContext);
            }
           // SecurityAuthorizationCheckEvent.Fire(principal.Identity.Name, context);
            //BooleanExpression expression = this.GetParsedExpression(context, this.mgr, int.Parse(principal.Identity.Name.Substring(0, 6)));
            //if (expression == null)
            //{
            //    throw new AuthorizationRuleNotFoundException(string.Format("Authorization Rule {0} not found in the database.", context));
            //}
            //bool flag = expression.Evaluate(principal);
            //if (!flag)
            //{
            //    SecurityAuthorizationFailedEvent.Fire(principal.Identity.Name, context);
            //}

            bool flag = mgr.GetRule(principal.Identity.Name, context); 

            return flag;
        }


        /// <summary>
        /// New Function , Bypass 2 sp
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool Authorize(string principal, string context)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }
            if ((context == null) || (context.Length == 0))
            {
                throw new ArgumentNullException("context");
            }
            if (this.mgr == null)
            {
                this.mgr = new DbRulesManager(this.database, this.securityConfigurationView.ConfigurationContext);
            }
           // SecurityAuthorizationCheckEvent.Fire(principal, context);

            //BooleanExpression expression = this.GetParsedExpression(context, this.mgr, int.Parse(principal.Identity.Name.Substring(0, 6)));
            //if (expression == null)
            //{
            //    throw new AuthorizationRuleNotFoundException(string.Format("Authorization Rule {0} not found in the database.", context));
            //}
            bool flag = mgr.GetRule(principal, context); 
            //if (!flag)
            //{
            //    SecurityAuthorizationFailedEvent.Fire(principal.Identity.Name, context);
            //}
            return flag;
        }


        private BooleanExpression GetParsedExpression(string context, DbRulesManager mgr, int OrganizationID)
        {
            AuthorizationRuleData rule = mgr.GetRule(context, OrganizationID);
            if (rule == null)
            {
                return null;
            }
            Parser parser = new Parser();
            return parser.Parse(rule.Expression);
        }

        public override void Initialize(ConfigurationView configurationView)
        {
            if (configurationView == null)
            {
                throw new ArgumentNullException("configurationView cannot be null");
            }
            this.securityConfigurationView = configurationView as SecurityConfigurationView;
            if (this.securityConfigurationView == null)
            {
                throw new ArgumentException("configurationView is not of the correct type.");
            }
            DbRulesAuthorizationProviderData authorizationProviderData = (DbRulesAuthorizationProviderData) this.securityConfigurationView.GetAuthorizationProviderData(base.ConfigurationName);
            this.database = authorizationProviderData.Database;
        }
    }
}

