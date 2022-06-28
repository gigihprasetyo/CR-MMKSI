using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System.Collections;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IVWI_EmployeePartsRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(ICriteria criteria, ICriteria innerQueryCriteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);

        List<VWI_EmployeeResign> SearchResign(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
    }
}