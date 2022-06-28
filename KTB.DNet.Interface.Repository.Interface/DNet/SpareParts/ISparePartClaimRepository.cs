using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ISparePartClaimRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        //Task<bool> BulkInsertAsync(TEntity data);
        Task<List<SparePartClaimDto>> GetDataSparePartClaim(DateTime lastUpdateTime);
        List<SparePartClaimDto> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
        List<SparepartClaimDetailDto> SearchDetail(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
    }
}
