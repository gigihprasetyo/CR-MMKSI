using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.ServiceHistory;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class ServiceHistoryRepository : BaseDNetRepository<VWI_ServiceHistory>, IServiceHistoryRepository<VWI_ServiceHistory, int>
    {
        public ServiceHistoryRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_ServiceHistory> Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_ServiceHistory> result = SearchFetchPaging<VWI_ServiceHistory>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_ServiceHistory>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(ServiceHistoryQuery.SelectQuery, criteria == null ? string.Empty : criteria.ToString())
                , "VWI_ServiceHistory.ID", null, orderBy, out filteredResultsCount, page, pageSize); 
                
                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_ServiceHistory>();
            }
        }

        #region Not Implemented
        public void SetUserLogin(string username)
        {
            throw new NotImplementedException();
        }

        public VWI_ServiceHistory Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(VWI_ServiceHistory entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(VWI_ServiceHistory entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_ServiceHistory> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
