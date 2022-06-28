using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IPartShopRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> GetByCodesAsync(List<string> codes);
        List<TEntity> GetByCodes(List<string> codes);
    }
}
