using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ITrLeadStatusToStatusRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        TEntity GetByCustomerStatusAndNextLeadStatus(int customerStatus, int nextLeadStatus);
        TEntity GetByParam(int customerStatus, int nextLeadStatus, int leadStatus);
    }
}
