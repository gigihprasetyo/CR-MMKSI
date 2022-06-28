using KTB.DNet.Interface.Domain;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IRoleRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        int GetTotalRoles(int userId);
        List<APIRole> GetUserRole(int userId);

    }
}
