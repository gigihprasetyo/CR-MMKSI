using KTB.DNet.Domain.Search;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IVWI_OpenFakturRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(ICriteria criteria, ICriteria chassisQueryCriteria, ICriteria lastUpdateQueryCriteria, ICriteria createdTimeQueryCriteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
    }
}
