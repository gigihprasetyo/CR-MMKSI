using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.VehiclePurchases.SQLQuery.VWI_POReceiptDealer;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.Dealer;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_POReceiptDealerRepository : BaseDNetRepository<VWI_POReceiptDealer>, IVWI_POReceiptDealerRepository<VWI_POReceiptDealer,int>
    {
        public VWI_POReceiptDealerRepository(string connectionString)
           : base(connectionString)
        { }

        public List<VWI_POReceiptDealer> Search(ICriteria criteria, ICriteria criteriaDealer, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                Dealer dealerCode = null;
                List<VWI_POReceiptDealer> result = null;

                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                totalResultsCount = 0;

                using (var cn = Connection)
                {
                    dealerCode = cn.Query<Dealer>(string.Format(VWI_POReceiptDealerQuery.SelectQueryDealer, criteriaDealer.ToString())).FirstOrDefault();
                }

                if (dealerCode != null)
                {
                    result = Search<VWI_POReceiptDealer>((connection, query, sqlparams) =>
                    {
                        return connection.Query<VWI_POReceiptDealer>(query, sqlparams, null, true, 500, null).ToList();
                    }, Connection, string.Format(VWI_POReceiptDealerQuery.SelectQueryResultPOReceiptDealer, dealerCode.ID, criteria == null ? string.Empty : criteria.ToString())
                    , "VWI_POReceiptDealer.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                    string.Format(VWI_POReceiptDealerQuery.SelectQueryPOReceiptDealer, dealerCode.ID, criteria == null ? string.Empty : criteria.ToString()));
                }

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_POReceiptDealer>();
            }
        }


        public ResponseMessage Create(VWI_POReceiptDealer entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public VWI_POReceiptDealer Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_POReceiptDealer> GetAll()
        {
            throw new NotImplementedException();
        }

        

        public ResponseMessage Update(VWI_POReceiptDealer entity)
        {
            throw new NotImplementedException();
        }

        public new List<VWI_POReceiptDealer> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
    }
}
