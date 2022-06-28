
using KTB.DNet.Domain.Search;
using System.Collections.Generic;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ISparePartPOEstimateRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(string query, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);

        List<TEntity> Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
    }
}
