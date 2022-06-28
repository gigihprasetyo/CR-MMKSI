using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.POEstimate;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SparePartPOEstimateRepository : BaseDNetRepository<VWI_POEstimateHaveBillingOne>, ISparePartPOEstimateRepository<VWI_POEstimateHaveBillingOne, int>
    {
        public SparePartPOEstimateRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_POEstimateHaveBillingOne> Search(string query, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            List<VWI_POEstimateHaveBillingOne> result = new List<VWI_POEstimateHaveBillingOne>();
            try
            {
                using (var cn = Connection)
                {
                    var data = cn.Query<VWI_POEstimateHaveBillingOne>(string.Format(POEstimateQuery.SelectQuery, query)).ToList();

                    filteredResultsCount = data.Count;
                    totalResultsCount = filteredResultsCount;
                    if (data.Count > 0)
                    {
                        // calculate the skip 
                        int skip = page < 1 ? 0 : (page - 1) * pageSize;

                        // filter out the data based on the paging
                        result = data.Cast<VWI_POEstimateHaveBillingOne>().OrderBy(x => x.SparePartPOEstimateID).Skip(skip).Take(pageSize).ToList();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_POEstimateHaveBillingOne>();
            }
        }

        public List<VWI_POEstimateHaveBillingOne> Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_POEstimateHaveBillingOne> result = SearchFetchPagingWithoutTotal<VWI_POEstimateHaveBillingOne>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_POEstimateHaveBillingOne>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(POEstimateQuery.SelectQuery, criteria == null ? string.Empty : criteria.ToString())
                , "VWI_POEstimateHaveBilling.SparePartPOEstimateID", null, orderBy, out filteredResultsCount, page, pageSize);

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_POEstimateHaveBillingOne>();
            }
        }

        #region Not Implemented

        public VWI_POEstimateHaveBillingOne Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(VWI_POEstimateHaveBillingOne entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(VWI_POEstimateHaveBillingOne entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_POEstimateHaveBillingOne> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
