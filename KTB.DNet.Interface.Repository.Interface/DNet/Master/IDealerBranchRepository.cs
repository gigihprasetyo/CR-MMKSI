
using System.Collections.Generic;
using System.Threading.Tasks;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IDealerBranchRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> GetDealerBranchByDealerIDAsync(int dealerID);
        List<TEntity> GetDealerBranchByDealerID(int dealerID);
        Task<List<TEntity>> GetAllByCodesAsync(List<string> listOfCodes);
        List<TEntity> GetAllByCodes(List<string> listOfCodes);
    }
}
