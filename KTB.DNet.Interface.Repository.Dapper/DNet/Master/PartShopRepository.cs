using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.PartShop;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class PartShopRepository : BaseDNetRepository<PartShop>, IPartShopRepository<PartShop, int>
    {
        public PartShopRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<List<PartShop>> GetByCodesAsync(List<string> codes)
        {
            List<PartShop> result = new List<PartShop>();
            try
            {
                if (codes.Any())
                {
                    using (var cn = Connection)
                    {
                        var data = await cn.QueryAsync<PartShop>(PartShopQuery.GetByListOfCode, new { ListOfCode = codes });

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

        public List<PartShop> GetByCodes(List<string> codes)
        {
            List<PartShop> result = new List<PartShop>();
            try
            {
                if (codes.Any())
                {
                    using (var cn = Connection)
                    {
                        return cn.Query<PartShop>(PartShopQuery.GetByListOfCode, new { ListOfCode = codes }).AsList();
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
        public PartShop Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(PartShop entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(PartShop entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<PartShop> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<PartShop> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
