
using KTB.DNet.Interface.Domain;
using System.Collections.Generic;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IApplicationConfigRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Get App Config By Key
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns></returns>
        ApplicationConfig GetByKey(string configKey);

        /// <summary>
        /// Get App Config Value
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns></returns>
        T GetConfigValue<T>(string configKey);

        /// <summary>
        /// Get unregistered config key
        /// </summary>
        /// <param name="listOfConstantConfigKey"></param>
        /// <returns></returns>
        List<string> GetUnregisteredConfigKey(List<string> listOfConstantConfigKey);
    }
}
