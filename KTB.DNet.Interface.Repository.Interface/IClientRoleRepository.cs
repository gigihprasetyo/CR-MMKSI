using KTB.DNet.Interface.Domain;
using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IClientRoleRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<APIClientRole> GetByClientId(Guid clientId);
        List<APIClientRole> GetClientRoleByRoleId(int roleId);
    }
}
