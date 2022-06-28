using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IUserPermissionRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Get User Permission By Client User Id
        /// </summary>
        /// <param name="clientUserId"></param>
        /// <returns></returns>
        List<APIUserPermission> GetUserPermissionByClientUserId(int clientUserId);

        /// <summary>
        /// Save User Permission Separately
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listOfSelectedUserPermission"></param>
        /// <returns></returns>
        ResponseMessage SaveUserPermission(int userId, List<APIUserPermission> listOfSelectedUserPermission);
    }
}
