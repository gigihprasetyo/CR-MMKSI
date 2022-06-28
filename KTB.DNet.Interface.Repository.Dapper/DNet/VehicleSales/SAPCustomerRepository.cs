using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.SAPCustomer;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SAPCustomerRepository : BaseDNetRepository<VWI_LeadCustomerSalesForce>, ISAPCustomerRepository<VWI_LeadCustomerSalesForce, int>
    {
        public SAPCustomerRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_LeadCustomerSalesForce> Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_LeadCustomerSalesForce> result = SearchFetchPaging<VWI_LeadCustomerSalesForce>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_LeadCustomerSalesForce>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SAPCustomerQuery.SelectQuery, criteria == null ? string.Empty : criteria.ToString())
                , "VWI_LeadCustomerSalesForce.DNetID", null, orderBy, out filteredResultsCount, page, pageSize,string.Format(SAPCustomerQuery.GetTotalQuery, criteria == null ? string.Empty : criteria.ToString()));

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_LeadCustomerSalesForce>();
            }
        }

        #region Not Implemented
        public void SetUserLogin(string username)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        VWI_LeadCustomerSalesForce IBaseDNetRepository<VWI_LeadCustomerSalesForce, int>.Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(VWI_LeadCustomerSalesForce entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(VWI_LeadCustomerSalesForce entity)
        {
            throw new NotImplementedException();
        }

        List<VWI_LeadCustomerSalesForce> IBaseDNetRepository<VWI_LeadCustomerSalesForce, int>.GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
