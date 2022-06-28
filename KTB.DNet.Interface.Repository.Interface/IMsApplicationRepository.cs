using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IMsApplicationRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        ResponseMessage Create(MsApplication entity, List<int> listOfSelectedPermissionId);
        ResponseMessage Update(MsApplication entity, List<int> listOfSelectedPermissionId);
        List<APIEndpointPermission> GetPermission(Guid appId);
        List<MsApplicationPermission> GetListOfMsAppPermissionByAppId(Guid appId);
        List<MsApplication> GetListOfApplication(Guid clientId, bool isDMSAdmin);
    }
}
