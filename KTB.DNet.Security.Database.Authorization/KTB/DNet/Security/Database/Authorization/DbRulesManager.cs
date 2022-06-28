namespace KTB.DNet.Security.Database.Authorization
{
    using Microsoft.Practices.EnterpriseLibrary.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Security.Configuration;
    using System;
    using System.Data;

    public class DbRulesManager
    {
        private Database dbRules = null;

        public DbRulesManager(string databaseService, ConfigurationContext config)
        {
            this.dbRules = new DatabaseProviderFactory(config).CreateDatabase(databaseService);
        }

        public void DeleteRuleById(int ruleId)
        {
            DBCommandWrapper storedProcCommandWrapper = this.dbRules.GetStoredProcCommandWrapper("dbo.DeleteRuleById");
            storedProcCommandWrapper.AddInParameter("id", DbType.Int32, ruleId);
            this.dbRules.ExecuteNonQuery(storedProcCommandWrapper);
        }

        public DataSet GetAllRules()
        {
            AuthorizationRuleDataCollection datas = new AuthorizationRuleDataCollection();
            DBCommandWrapper storedProcCommandWrapper = this.dbRules.GetStoredProcCommandWrapper("dbo.GetAllRules");
            using (DataSet set = this.dbRules.ExecuteDataSet(storedProcCommandWrapper))
            {
                return set;
            }
        }

        public AuthorizationRuleDataCollection GetAllRulesAsCollection()
        {
            AuthorizationRuleDataCollection datas = new AuthorizationRuleDataCollection();
            DBCommandWrapper storedProcCommandWrapper = this.dbRules.GetStoredProcCommandWrapper("dbo.GetAllRules");
            using (IDataReader reader = this.dbRules.ExecuteReader(storedProcCommandWrapper))
            {
                while (reader.Read())
                {
                    AuthorizationRuleData ruleFromReader = this.GetRuleFromReader(reader);
                    datas.Add(ruleFromReader);
                }
            }
            return datas;
        }

        public AuthorizationRuleData GetRule(string name, int OrganizationID)
        {
            AuthorizationRuleData ruleFromReader = null;
            DBCommandWrapper storedProcCommandWrapper = this.dbRules.GetStoredProcCommandWrapper("dbo.GetRuleByName");
            storedProcCommandWrapper.AddInParameter("Name", DbType.AnsiString, name);
            storedProcCommandWrapper.AddInParameter("OrganizationID", DbType.Int32, OrganizationID);
            using (IDataReader reader = this.dbRules.ExecuteReader(storedProcCommandWrapper))
            {
                if (reader.Read())
                {
                    ruleFromReader = this.GetRuleFromReader(reader);
                }
            }
            return ruleFromReader;
        }


        /// <summary>
        /// Rule baru di coba
        /// </summary>
        /// <param name="name"></param>
        /// <param name="PrivillageName"></param>
        /// <returns></returns>
        public bool GetRule(string name,  string PrivillageName)
        {
           
            DBCommandWrapper storedProcCommandWrapper = this.dbRules.GetStoredProcCommandWrapper("dbo.GetAuthorization");
            storedProcCommandWrapper.AddInParameter("Name", DbType.AnsiString, name);
            storedProcCommandWrapper.AddInParameter("PrivillegeName", DbType.AnsiString, PrivillageName);
            bool Exist = false;
            using (IDataReader reader = this.dbRules.ExecuteReader(storedProcCommandWrapper))
            {
                if (reader.Read())
                {
                    Exist = true;
                }
            }
            return Exist;
        }


        private AuthorizationRuleData GetRuleFromReader(IDataReader reader)
        {
            AuthorizationRuleData data = new AuthorizationRuleData();
            data.Name = reader.GetString(reader.GetOrdinal("Name"));
            data.Expression = reader.GetString(reader.GetOrdinal("Expression"));
            return data;
        }

        public void InsertRule(string name, string expression)
        {
            DBCommandWrapper storedProcCommandWrapper = this.dbRules.GetStoredProcCommandWrapper("dbo.InsertRule");
            storedProcCommandWrapper.AddInParameter("Name", DbType.AnsiString, name);
            storedProcCommandWrapper.AddInParameter("Expression", DbType.AnsiString, expression);
            this.dbRules.ExecuteNonQuery(storedProcCommandWrapper);
        }

        public void UpdateRuleById(int ruleId, string name, string expression)
        {
            DBCommandWrapper storedProcCommandWrapper = this.dbRules.GetStoredProcCommandWrapper("dbo.UpdateRuleById");
            storedProcCommandWrapper.AddInParameter("id", DbType.Int32, ruleId);
            storedProcCommandWrapper.AddInParameter("Name", DbType.AnsiString, name);
            storedProcCommandWrapper.AddInParameter("Expression", DbType.AnsiString, expression);
            this.dbRules.ExecuteNonQuery(storedProcCommandWrapper);
        }
    }
}

