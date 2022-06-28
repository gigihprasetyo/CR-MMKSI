using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IUserActivityRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<UserActivity> Search(string dealerCode, DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount);
    }
}
