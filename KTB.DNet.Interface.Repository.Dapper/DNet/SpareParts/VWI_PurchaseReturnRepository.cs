using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.VWI_PurchaseReturn;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_PurchaseReturnRepository : BaseDNetRepository<VWI_PurchaseReturn>, IVWI_PurchaseReturnRepository<VWI_PurchaseReturn, int>
    {
        public VWI_PurchaseReturnRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_PurchaseReturn> Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_PurchaseReturn> result = SearchFetchPaging<VWI_PurchaseReturn>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_PurchaseReturn>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_PurchaseReturnQuery.SelectQuery, criteria == null ? string.Empty : criteria.ToString())
                , "VWI_PurchaseReturn.ID", null, orderBy, out filteredResultsCount, page, pageSize);  

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_PurchaseReturn>();
            }
        }

        #region Not Implemented
        public void SetUserLogin(string username)
        {
            throw new NotImplementedException();
        }

        public VWI_PurchaseReturn Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(VWI_PurchaseReturn entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(VWI_PurchaseReturn entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_PurchaseReturn> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
