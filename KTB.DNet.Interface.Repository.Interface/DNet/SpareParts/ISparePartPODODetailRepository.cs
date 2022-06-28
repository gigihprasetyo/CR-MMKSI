using KTB.DNet.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ISparePartPODODetailRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> GetByListOfSparePartDOId(List<int> listOfId);

        List<VWI_SparePartPODOHaveBillingHeaderDetail> GetByQuery(string query);

        SparePartBillingDetail GetSparePartBillingDetailByQuery(string query);
    }
}
