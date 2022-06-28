using KTB.DNet.Interface.Framework;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IUserRoleRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {

        /// <summary>
        /// Save User Role Separately
        /// </summary>
        /// <param name="user"></param>
        /// <param name="listOfUserRole"></param>
        /// <param name="listOfClientUser"></param>
        /// <returns></returns>
        ResponseMessage SaveUserRole(int userId, List<int> listOfRoleId);
    }
}
