using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IClientUserRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Save Client User Separately
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listOfClientId"></param>
        /// <returns></returns>
        ResponseMessage SaveClientUser(int userId, List<Guid> listOfClientId);

        /// <summary>
        /// Get Client User By User Id and Client Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        TEntity GetByUserIdAndClientId(int userId, Guid clientId);

        /// <summary>
        /// check if token has already expired
        /// </summary>
        /// <param name="user"></param>
        /// <param name="today"></param>
        /// <returns></returns>
        bool IsTokenExpired(APIClientUser user, DateTime today);

        /// <summary>
        /// Get List of Client User by User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<TEntity> GetByUserId(int userId);

        /// <summary>
        /// Get by userid and appname
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        List<TEntity> GetByUserIdAndAppName(int userId, string name);

        /// <summary>
        /// Save User Clients in Bulk
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listOfSelectedUserPermission"></param>
        /// <returns></returns>
        ResponseMessage SaveClientUserInBulk(Guid clientId, List<APIUser> listOfSelectedUsers);
    }
}
