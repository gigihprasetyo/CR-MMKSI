
using KTB.DNet.Interface.Domain;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IThrottleRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        APIThrottle GetByUri(string uri);
    }
}
