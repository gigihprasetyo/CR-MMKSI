using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.PRHistoryPOStatusCancel;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class PRHistoryPOStatusCancelRepository : BaseDNetRepository<VWI_PRHistoryPOStatusCancel>, IPRHistoryPOStatusCancelRepository<VWI_PRHistoryPOStatusCancel, int>
    {
        public PRHistoryPOStatusCancelRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_PRHistoryPOStatusCancel> Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;


                List<VWI_PRHistoryPOStatusCancel> result = SearchFetchPaging<VWI_PRHistoryPOStatusCancel>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_PRHistoryPOStatusCancel>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(PRHistoryPOStatusCancelQuery.SelectQuery, criteria == null ? string.Empty : criteria.ToString())
                , "VWI_PRHistoryPOStatusCancel.ID", null, orderBy, out filteredResultsCount, page, pageSize);  

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_PRHistoryPOStatusCancel>();
            }
        }

        #region Not Implemented
        public void SetUserLogin(string username)
        {
            throw new NotImplementedException();
        }

        public VWI_PRHistoryPOStatusCancel Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(VWI_PRHistoryPOStatusCancel entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(VWI_PRHistoryPOStatusCancel entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_PRHistoryPOStatusCancel> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
