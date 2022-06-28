using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_Campaign;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_CampaignRepository : BaseDNetRepository<VWI_Campaign>, IVWI_CampaignRepository<VWI_Campaign, int>
    {
        /// <summary>Initializes a new instance of the <see cref="VWI_CampaignRepository"/> class.</summary>
        /// <param name="connectionString">The connection string.</param>
        public VWI_CampaignRepository(string connectionString)
           : base(connectionString)
        { }

        /// <summary>Searches the specified criteria.</summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="dealerCode">The dealer code.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="filteredResultsCount">The filtered results count.</param>
        /// <param name="totalResultsCount">The total results count.</param>
        /// <returns></returns>
        public List<VWI_Campaign> Search(ICriteria criteria, string dealerCode, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                string defauldOrder = string.Empty;
                if (string.IsNullOrEmpty(orderBy))
                    orderBy = "VWI_Campaign.DealerCode asc drop table #tempBabitHeader drop table #tempBabitEventProposalHeader drop table #tempNationalEvent";
                else
                    orderBy = orderBy + " drop table #tempBabitHeader drop table #tempBabitEventProposalHeader";
                filteredResultsCount = 0;

                string sql = string.Format(VWI_CampaignQuery.SelectQuery, "WHERE DealerCode = '" + dealerCode + "'", criteria == null ? string.Empty : criteria.ToString());
                string sqlTotal = string.Format(VWI_CampaignQuery.SelectTotalQuery, "WHERE DealerCode = '" + dealerCode + "'", criteria == null ? string.Empty : criteria.ToString());

                List<VWI_Campaign> result = Search<VWI_Campaign>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_Campaign>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, sql, "VWI_Campaign.ID", null, orderBy, out filteredResultsCount, page, pageSize, sqlTotal, "ID");

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_Campaign>();
            }
        }

        public ResponseMessage Create(VWI_Campaign entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public VWI_Campaign Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_Campaign> GetAll()
        {
            throw new NotImplementedException();
        }

        public new List<VWI_Campaign> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(VWI_Campaign entity)
        {
            throw new NotImplementedException();
        }
    }
}
