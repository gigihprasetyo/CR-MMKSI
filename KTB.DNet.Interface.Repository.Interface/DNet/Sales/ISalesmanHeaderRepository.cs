using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ISalesmanHeaderRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> GetByCodesAndDealerAsync(List<string> listOfCodes, int dealerID);
        List<TEntity> GetByCodesAndDealer(List<string> listOfCodes, int dealerID);
    }
}
