using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ITrStatusToLeadStatusCodeToLeadStateCodeRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        TEntity GetByParam(int customerStatus, int leadStatus, int leadState);
    }
}
