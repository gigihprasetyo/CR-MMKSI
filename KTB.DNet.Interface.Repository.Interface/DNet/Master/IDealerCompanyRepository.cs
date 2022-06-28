
using KTB.DNet.Domain;
using KTB.DNet.Interface.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IDealerCompanyRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(DealerCompanyPostModel model, out int filteredResultsCount, out int totalResultsCount);
        List<DealerGroup> GetAllDealerGroup();
    }
}
