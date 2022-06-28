using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using System.Collections;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IBaseDNetRepository<TEntity, in TKey> where TEntity : class
    {
        void SetUserLogin(string username);

        TEntity Get(TKey id);
        ResponseMessage Create(TEntity entity);
        ResponseMessage Update(TEntity entity);
        ResponseMessage Delete(TKey id);
        List<TEntity> GetAll();
        List<TEntity> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);

    }
}
