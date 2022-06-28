
using System.Collections.Generic;
using System.Threading.Tasks;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IAssistPartSalesRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        Task<bool> BulkInsertAsync(List<TEntity> data);

        bool BulkInsert(List<TEntity> data);
    }
}
