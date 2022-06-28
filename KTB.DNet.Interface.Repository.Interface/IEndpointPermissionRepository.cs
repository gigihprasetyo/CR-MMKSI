using KTB.DNet.Interface.Framework;
using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IEndpointPermissionRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        TEntity GetByName(string name);
        TEntity GetByUri(string uri);
        List<TEntity> GetClientPermission(Guid clientId);
        List<TEntity> SearchByClientRoleId(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, int clientRoleId);
        List<TEntity> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, bool isScheduled);
        List<TEntity> GetSelectedPermission(List<int> ids);
        List<TEntity> GetUnselectedPermission(List<int> ids);
        List<TEntity> GetEndpointWithNoThrottler();
        int GetPermissionCount(int userId);
        List<string> GetUnregisteredPermissionCode(List<string> listOfConstantPermissionCode);
        List<int> GetPermissionByEndpointGroup(int id);
        List<int> GetPermissionByEndpointType(int id);
        List<int> GetPermissionByOperationType(int id);
        List<TEntity> GetAllPermission();
        ResponseMessage UpdateEndpointPermissionGroup(List<int> endpointIdList, int endpointGroupId, string username);
        ResponseMessage UpdateEndpointPermissionType(List<int> endpointIdList, int endpointTypeId, string username);
        ResponseMessage UpdateOperationType(List<int> endpointIdList, int operationTypeId, string username);

    }
}
