
using System.Collections.Generic;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ISparePartPOEstimateDetailRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> GetByListOfPOEstimateId(List<int> listOfPOEstimateId);
    }
}
