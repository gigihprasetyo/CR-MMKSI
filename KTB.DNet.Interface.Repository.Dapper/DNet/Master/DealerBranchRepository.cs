using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.DealerBranch;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class DealerBranchRepository : BaseDNetRepository<DealerBranch>, IDealerBranchRepository<DealerBranch, int>
    {
        public DealerBranchRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<List<DealerBranch>> GetDealerBranchByDealerIDAsync(int dealerID)
        {
            try
            {
                using (var connection = Connection)
                {
                    var data = await connection.QueryAsync<DealerBranch>(DealerBranchQuery.GetByDealerId, new { DealerID = dealerID });

                    return data != null ? data.ToList() : new List<DealerBranch>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DealerBranch> GetDealerBranchByDealerID(int dealerID)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<DealerBranch>(DealerBranchQuery.GetByDealerId, new { DealerID = dealerID }).AsList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DealerBranch>> GetAllByCodesAsync(List<string> listOfCodes)
        {
            List<DealerBranch> result = new List<DealerBranch>();
            try
            {
                if (listOfCodes.Any())
                {
                    using (var cn = Connection)
                    {
                        var data = await cn.QueryAsync<DealerBranch>(DealerBranchQuery.GetByListOfCode, new { ListOfCode = listOfCodes });

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

        public List<DealerBranch> GetAllByCodes(List<string> listOfCodes)
        {
            List<DealerBranch> result = new List<DealerBranch>();
            try
            {
                if (listOfCodes.Any())
                {
                    using (var cn = Connection)
                    {
                        return cn.Query<DealerBranch>(DealerBranchQuery.GetByListOfCode, new { ListOfCode = listOfCodes }).AsList();
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
        public DealerBranch Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(DealerBranch entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(DealerBranch entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DealerBranch> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DealerBranch> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
