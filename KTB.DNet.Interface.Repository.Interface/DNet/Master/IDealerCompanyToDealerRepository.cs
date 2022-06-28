
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IDealerCompanyToDealerRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<Dealer> GetAllDealer(int dealerCompanyId);
    }
}
