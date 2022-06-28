using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.ClientPermission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class ClientPermissionRepository : BaseRepository<APIClientPermission>
    {
        #region Constructor
        public ClientPermissionRepository(string connectionString)
            : base(connectionString) { }

        #endregion

        #region Remove Client Permission By ClientId
        /// <summary>
        /// RemoveByClientId
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="clientId"></param>
        public void RemoveByClientId(IDbConnection connection, IDbTransaction transaction, Guid clientId)
        {
            connection.Execute(ClientPermissionQuery.RemoveByClientId, new { clientId = clientId }, transaction);
        }
        #endregion

        #region Remove List Of Client Permission
        /// <summary>
        /// RemoveListOfClientPermission
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="clientId"></param>
        /// <param name="listOfPermissionId"></param>
        public void RemoveListOfClientPermission(IDbConnection connection, IDbTransaction transaction, Guid clientId, List<int> listOfPermissionId, bool withRemovingRoleAndUserPermission = false)
        {
            if (listOfPermissionId != null && listOfPermissionId.Count() > 0)
            {
                if (withRemovingRoleAndUserPermission)
                {
                    UserPermissionRepository userPermissionRepo = new UserPermissionRepository(this._connectionString);
                    RolePermissionRepository rolePermissionRepo = new RolePermissionRepository(this._connectionString);

                    userPermissionRepo.RemovedPermissionByRemovedClientPermission(connection, transaction, clientId, listOfPermissionId);
                    rolePermissionRepo.RemovedPermissionByRemovedClientPermission(connection, transaction, clientId, listOfPermissionId);
                }
                connection.Execute(ClientPermissionQuery.RemoveListOfClientPermission, new { clientId = clientId, listOfPermissionId = listOfPermissionId }, transaction);
            }
        }
        #endregion

        #region AddClientPermission
        /// <summary>
        /// AddClientPermission
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="listOfClientPermission"></param>
        public void AddClientPermission(IDbConnection connection, IDbTransaction transaction, List<APIClientPermission> listOfClientPermission)
        {
            if (listOfClientPermission != null && listOfClientPermission.Count() > 0)
            {
                connection.Execute(ClientPermissionQuery.InsertClientPermission, listOfClientPermission, transaction);
            }
        }
        #endregion

        #region GetByClientId
        /// <summary>
        /// GetByClientId
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<APIClientPermission> GetByClientId(Guid clientId)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return connection.Query<APIClientPermission>(ClientPermissionQuery.GetByClientId, new { clientId = clientId }).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<APIClientPermission>();
            }
        }
        #endregion
    }
}
