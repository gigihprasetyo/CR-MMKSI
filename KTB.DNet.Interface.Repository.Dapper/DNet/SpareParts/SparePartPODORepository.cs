using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.PODO;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SparePartPODORepository : BaseDNetRepository<VWI_SparePartPODOHaveBillingOne>, ISparePartPODORepository<VWI_SparePartPODOHaveBillingOne, int>
    {
        public SparePartPODORepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_SparePartPODOHaveBillingOne> Search(string query, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            List<VWI_SparePartPODOHaveBillingOne> result = new List<VWI_SparePartPODOHaveBillingOne>();
            try
            {
                using (var cn = Connection)
                {
                    var data = cn.Query<VWI_SparePartPODOHaveBillingOne>(query, null, null, true, Timeout, null).ToList();

                    filteredResultsCount = data.Count;
                    totalResultsCount = filteredResultsCount;

                    if (data.Count > 0)
                    {
                        // calculate the skip 
                        int skip = page < 1 ? 0 : (page - 1) * pageSize;

                        // filter out the data based on the paging                                            
                        if (sortColumns != null && sortColumns.Count > 0)
                            result = data.Skip(skip).Take(pageSize).ToList();
                        else
                            result = data.OrderBy(x => x.ID).Skip(skip).Take(pageSize).ToList();
                    }

                }

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_SparePartPODOHaveBillingOne>();
            }
        }

        public List<VWI_SparePartPODOHaveBillingOne> Search(ICriteria criteria, ICriteria innerQueryCriteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                int? timeout = AppConfigs.GetInt(Constants.DBTimeoutConfigName);
                timeout = timeout.Value == 0 ? null : timeout;

                List<VWI_SparePartPODOHaveBillingOne> result = SearchFetchPagingWithoutTotal<VWI_SparePartPODOHaveBillingOne>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_SparePartPODOHaveBillingOne>(query, sqlParams, null, true, timeout, null).ToList();
                }, Connection, string.Format(PODOQuery.SelectPurchaseReceipt,
                                                innerQueryCriteria == null ? string.Empty : innerQueryCriteria.ToString(),
                                                criteria == null ? string.Empty : criteria.ToString())
                , "VWI_PODOHaveBilling.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(PODOQuery.GetTotalQuery,
                                                innerQueryCriteria == null ? string.Empty : innerQueryCriteria.ToString(),
                                                criteria == null ? string.Empty : criteria.ToString()));

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_SparePartPODOHaveBillingOne>();
            }
        }

        #region Not Implemented

        public List<VWI_SparePartPODOHaveBillingOne> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public VWI_SparePartPODOHaveBillingOne Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(VWI_SparePartPODOHaveBillingOne entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(VWI_SparePartPODOHaveBillingOne entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_SparePartPODOHaveBillingOne> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
