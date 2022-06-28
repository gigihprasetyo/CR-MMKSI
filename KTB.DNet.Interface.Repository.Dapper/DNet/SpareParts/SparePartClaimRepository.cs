using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Model.Dto;
using KTB.DNet.Interface.Model.Parameters;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.SparePartClaim;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SparePartClaimRepository : BaseDNetRepository<SparePartClaim>, ISparePartClaimRepository<SparePartClaim, int>
    {
        public SparePartClaimRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<List<SparePartClaimDto>> GetDataSparePartClaim(DateTime lastUpdateTime)
        {
            List<SparePartClaimDto> result = new List<SparePartClaimDto>();

            try
            {
                using (var cn = Connection)
                {
                    var data = await cn.QueryAsync<SparePartClaimDto>(SparePartClaimQuery.SparePartClaimQ,
                        new
                        {
                            LastUpdateTime = lastUpdateTime
                        });

                    return data != null ? data.ToList() : result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new List<SparePartClaimDto> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<SparePartClaimDto> result = SearchFetchPaging<SparePartClaimDto>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparePartClaimDto>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartClaimQuery.SparePartClaimQ,
                                                strCriteria,
                                                strInnerCriteria)
                , "SparePartClaimDto", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartClaimQuery.GetTotalSparePartClaimQ,
                                                strCriteria,
                                                strInnerCriteria), "ID");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SparePartClaimDto>();
            }
        }
        public List<SparepartClaimDetailDto> SearchDetail(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
            filteredResultsCount = 0;

            List<SparepartClaimDetailDto> claimdetailRes = SearchFetchPaging<SparepartClaimDetailDto>((connection, query, sqlParams) =>
            {
                return connection.Query<SparepartClaimDetailDto>(query, sqlParams, null, true, Timeout, null).ToList();
            }, Connection, string.Format(SparePartClaimQuery.SparePartClaimDetailQ,
                                                strCriteria,
                                                strInnerCriteria)
                , "SparePartClaimDetailParameterDto", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartClaimQuery.GetTotalSparePartClaimDetailQ,
                                                strCriteria,
                                                strInnerCriteria), "ID");

            totalResultsCount = filteredResultsCount;

            return claimdetailRes;
        }

        public List<SparePartClaim> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<SparePartClaim>();
        }
        #region Not Implemented
        public void SetUserLogin(string username)
        {
            throw new NotImplementedException();
        }

        public SparePartClaim Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(SparePartClaim entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(SparePartClaim entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<SparePartClaim> GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
