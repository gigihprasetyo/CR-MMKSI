using System;
using System.Collections.Generic;
using KTB.DNet.Domain.Search;
using System.Collections;

/// <summary>
/// Interface Repository DNET
/// </summary>
namespace KTB.DNet.Interface.Repository.Interface
{
    /// <summary></summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="KTB.DNet.Interface.Repository.Interface.IBaseDNetRepository{TEntity, TKey}" />
    public interface IVWI_POReceiptDealerRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(ICriteria criteria, ICriteria criteriaDealer, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
    }
}
