using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.AssistChassisMaster;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class AssistChassisMasterRepository : BaseDNetRepository<AssistChassisMaster>, IAssistChassisMasterRepository<AssistChassisMaster, int>
    {
        public AssistChassisMasterRepository(string connectionString)
            : base(connectionString)
        { }

        public List<AssistChassisMaster> Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<AssistChassisMaster> result = SearchFetchPaging<AssistChassisMaster>((connection, query, sqlParams) =>
                {
                    return connection.Query<AssistChassisMaster>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(AssistChassisMasterQuery.SelectQuery, criteria == null ? string.Empty : criteria.ToString())
                , "AssistChassisMaster.ID", null, orderBy, out filteredResultsCount, page, pageSize);  

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<AssistChassisMaster>();
            }
        }

        #region Not Implemented
        public void SetUserLogin(string username)
        {
            throw new NotImplementedException();
        }

        public AssistChassisMaster Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(AssistChassisMaster entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(AssistChassisMaster entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AssistChassisMaster> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
