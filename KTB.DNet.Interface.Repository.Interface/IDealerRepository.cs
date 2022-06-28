using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IDealerRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(int id);

        /// <summary>
        /// Get dealer by code
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        TEntity GetByCode(string dealerCode);

        /// <summary>
        /// Get dealer by dealer group id
        /// </summary>
        /// <param name="dealerGroupId"></param>
        /// <returns></returns>
        List<TEntity> GetAllByGroupId(int dealerGroupId);

        /// <summary>
        ///     Gets all 'Dealer' entities as an IQueryable.
        /// </summary>
        /// <returns>An IQueryable of all 'Dealer' entities.</returns>
        List<TEntity> GetActiveDealers();

        /// <summary>
        ///     Gets all 'Dealer' entities as an IQueryable.
        /// </summary>
        /// <returns>An IQueryable of all 'Dealer' entities.</returns>
        List<TEntity> GetAll();

        /// <summary>
        /// Get dealer count
        /// </summary>
        /// <returns></returns>
        int GetDealerCount(int userId);
    }
}
