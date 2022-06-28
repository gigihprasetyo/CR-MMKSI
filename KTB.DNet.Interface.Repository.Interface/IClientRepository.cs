using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IClientRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Get client permission
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        List<APIEndpointPermission> GetClientPermission(Guid clientId);

        /// <summary>
        /// Get client role
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        List<APIRole> GetClientRole(Guid clientId);

        /// <summary>
        /// Get user client
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<APIClient> GetUserClient(APIUser user);


        /// <summary>
        /// Get By App Id
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<APIClient> GetByAppId(Guid appId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listOfSelectedRoleId"></param>
        /// <param name="listOfSelectedPermissionId"></param>
        /// <returns></returns>
        ResponseMessage Create(APIClient entity, List<int> listOfSelectedRoleId, List<int> listOfSelectedPermissionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listOfSelectedRoleId"></param>
        /// <param name="listOfSelectedPermissionId"></param>
        /// <returns></returns>
        ResponseMessage Update(APIClient entity, List<int> listOfSelectedRoleId, List<int> listOfSelectedPermissionId);

        int GetTotalClient(bool isDMSAdmin, int userId);

        /// <summary>
        /// Get By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        APIClient GetByName(string name);

        List<APIClient> GetListById(List<string> clientIdList);
    }
}
