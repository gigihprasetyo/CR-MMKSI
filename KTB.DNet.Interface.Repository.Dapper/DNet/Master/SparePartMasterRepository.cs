using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.SparepartMaster;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SparePartMasterRepository : BaseDNetRepository<SparePartMaster>, ISparePartMasterRepository<SparePartMaster, int>
    {
        public SparePartMasterRepository(string connectionString)
            : base(connectionString)
        { }

        public async Task<List<SparePartMaster>> GetByPartNumbersAsync(List<string> listOfPartNumber)
        {
            List<SparePartMaster> result = new List<SparePartMaster>();
            try
            {
                if (listOfPartNumber.Any())
                {
                    using (var cn = Connection)
                    {
                        var data = await cn.QueryAsync<SparePartMaster>(SparepartMasterQuery.GetByListOfPartNumber, new { ListOfPartNumber = listOfPartNumber });

                        return data != null ? data.ToList() : result;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return new List<SparePartMaster>();
            }
        }

        public List<SparePartMaster> GetByPartNumbers(List<string> listOfPartNumber)
        {
            List<SparePartMaster> result = new List<SparePartMaster>();
            try
            {
                if (listOfPartNumber.Any())
                {
                    using (var cn = Connection)
                    {
                        return cn.Query<SparePartMaster>(SparepartMasterQuery.GetByListOfPartNumber, new { ListOfPartNumber = listOfPartNumber }).AsList();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return new List<SparePartMaster>();
            }
        }

        #region Not Implemented
        public SparePartMaster Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(SparePartMaster entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(SparePartMaster entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<SparePartMaster> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<SparePartMaster> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
