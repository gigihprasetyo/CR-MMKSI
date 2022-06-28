
using System.Collections;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IStallWorkingTimeRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        bool BulkInsert(List<TEntity> data);
        bool BulkUpdate(List<TEntity> data);
    }
}
