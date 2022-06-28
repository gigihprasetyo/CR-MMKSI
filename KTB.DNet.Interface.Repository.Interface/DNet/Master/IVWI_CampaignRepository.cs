using KTB.DNet.Domain.Search;
using System.Collections;
using System.Collections.Generic;


namespace KTB.DNet.Interface.Repository.Interface
{
    /// <summary></summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="KTB.DNet.Interface.Repository.Interface.IBaseDNetRepository{TEntity, TKey}" />
    public interface IVWI_CampaignRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>Searches the specified criteria.</summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="dealerCode">The dealer code.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="filteredResultsCount">The filtered results count.</param>
        /// <param name="totalResultsCount">The total results count.</param>
        /// <returns></returns>
        List<TEntity> Search(ICriteria criteria, string dealerCode, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
    }
}
