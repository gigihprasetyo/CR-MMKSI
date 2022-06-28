using KTB.DNet.Interface.Framework;
using System;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IMsAppVersionRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Get current deployment version
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        TEntity GetCurrentDeploymentVersionApp(Guid appId);

        /// <summary>
        /// Set the version as current deployment on server
        /// </summary>
        /// <param name="appVersion"></param>
        /// <returns></returns>
        ResponseMessage SetAsCurrentDeploymentVersion(TEntity appVersion);
    }
}
