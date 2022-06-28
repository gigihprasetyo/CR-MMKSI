using KTB.DNet.Interface.Domain;

using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IRolePermissionRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<APIRolePermission> GetByClientRoleId(int clientRoleId);
    }
}
