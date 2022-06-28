
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System;
using System.Collections.Generic;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IUserRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Get by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        TEntity GetByName(string userName);

        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        TEntity GetAuthenticatedUser(string username, string password);

        /// <summary>
        /// Find user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        TEntity GetAuthenticatedUser(string userName, string password, string dealerCode);

        /// <summary>
        /// Get user count
        /// </summary>
        /// <returns></returns>
        int GetUserCount(int userId, int dealerId);

        /// <summary>
        /// Filter user
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<TEntity> Search(DataTablePostModel model, int? dealerId, out int filteredResultsCount, out int totalResultsCount);

        /// <summary>
        /// Create user with all its clients, roles, and permissions
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ResponseMessage CreateWithAllClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission);

        /// <summary>
        /// Update user with all its clients, roles, and permissions
        /// </summary>
        /// <param name="repoModelUser"></param>
        /// <returns></returns>
        ResponseMessage UpdateWithAllClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission);

        /// <summary>
        /// Get List of Permmission by User Name 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<APIUserPermission> GetPermission(string userName);

        /// <summary>
        /// Get List of Permmission by User Name and clientId 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<APIUserPermission> GetPermission(string userName, Guid clientId);

        /// <summary>
        /// Get client user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<APIClientUser> GetClientUser(int id);

        /// <summary>
        /// Create with Separate ClientRolePermissionRepositories
        /// </summary>
        /// <param name="user"></param>
        /// <param name="listOfSelectedClient"></param>
        /// <param name="listOfSelectedRole"></param>
        /// <param name="listOfSelectedPermission"></param>
        /// <returns></returns>
        ResponseMessage CreateWithSeparateClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission);

        /// <summary>
        /// Update with Separe ClientRolePermissionRepositories
        /// </summary>
        /// <param name="user"></param>
        /// <param name="listOfSelectedClient"></param>
        /// <param name="listOfSelectedRole"></param>
        /// <param name="listOfSelectedPermission"></param>
        /// <returns></returns>
        ResponseMessage UpdateWithSeparateClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission);

        List<APIUser> GetUnassignedUsers(Guid clientId);

        List<int> GetUsersByClientId(Guid clientId);
    }
}
