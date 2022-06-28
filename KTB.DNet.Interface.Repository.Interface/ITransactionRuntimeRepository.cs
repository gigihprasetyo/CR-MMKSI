using KTB.DNet.Interface.Domain;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ITransactionRuntimeRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
    }
}
