using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Sales.SqlQuery;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SalesmanHeaderRepository : BaseDNetRepository<SalesmanHeader>, ISalesmanHeaderRepository<SalesmanHeader, int>
    {
        public SalesmanHeaderRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<List<SalesmanHeader>> GetByCodesAndDealerAsync(List<string> listOfCodes, int dealerID)
        {
            List<SalesmanHeader> result = new List<SalesmanHeader>();
            try
            {
                if (listOfCodes.Any())
                {
                    using (var connection = Connection)
                    {
                        var data = await connection.QueryAsync<SalesmanHeader>(SalesmanHeaderQuery.GetSalesmanByCodeAndDealer, new { DealerID = dealerID, SalesmanCode = listOfCodes });

                        return data != null ? data.ToList() : result;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SalesmanHeader> GetByCodesAndDealer(List<string> listOfCodes, int dealerID)
        {
            List<SalesmanHeader> result = new List<SalesmanHeader>();
            try
            {
                if (listOfCodes.Any())
                {
                    using (var cn = Connection)
                    {
                        return cn.Query<SalesmanHeader>(SalesmanHeaderQuery.GetSalesmanByCodeAndDealer, new { DealerID = dealerID, SalesmanCode = listOfCodes }).AsList();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Not Implemented

        public SalesmanHeader Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(SalesmanHeader entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(SalesmanHeader entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<SalesmanHeader> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<SalesmanHeader> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
