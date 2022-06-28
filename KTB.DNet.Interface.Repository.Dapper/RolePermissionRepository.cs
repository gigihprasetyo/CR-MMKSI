#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : RolePermission repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 6/12/2018 15:18
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.RolePermission;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class RolePermissionRepository : BaseRepository<APIRolePermission>, IRolePermissionRepository<APIRolePermission, int>
    {
        #region Constructor
        public RolePermissionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region GetByClientRoleId
        /// <summary>
        /// Get Role Permission By Client Role Id
        /// </summary>
        /// <param name="clientRoleId"></param>
        /// <returns></returns>
        public List<APIRolePermission> GetByClientRoleId(int clientRoleId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIRolePermission>(
                        RolePermissionQuery.GetByClientRoleId,
                        new { ClientRoleId = clientRoleId }
                    ).AsList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region GetByClientRoleIds
        /// <summary>
        /// GetByClientRoleIds
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<APIRolePermission> GetByClientRoleIds(List<int> ids)
        {
            try
            {
                if (ids != null && ids.Count() > 0)
                {
                    using (var connection = Connection)
                    {
                        return connection.Query<APIRolePermission>(
                            RolePermissionQuery.GetByClientRoleIds,
                            new { @ClientRoleIds = ids }
                            ).ToList();
                    }
                }

                return new List<APIRolePermission>();

            }
            catch (Exception e)
            {
                return new List<APIRolePermission>();
            }
        }
        #endregion

        #region AddListOfRolePermission
        /// <summary>
        /// AddListOfRolePermission
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="rolePermissions"></param>
        public void AddListOfRolePermission(IDbConnection connection, IDbTransaction transaction, List<APIRolePermission> rolePermissions)
        {
            if (rolePermissions != null && rolePermissions.Count() > 0)
            {
                connection.Execute(RolePermissionQuery.InsertClientRolePermission, rolePermissions, transaction);
            }
        }
        #endregion

        #region RemoveListOfRolePermission
        /// <summary>
        /// RemoveListOfRolePermission
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="listOfRolePermissionId"></param>
        public void RemoveListOfRolePermission(IDbConnection connection, IDbTransaction transaction, List<int> listOfRolePermissionId)
        {
            if (listOfRolePermissionId != null && listOfRolePermissionId.Count() > 0)
            {
                connection.Execute(RolePermissionQuery.DeleteByListOfId, new { listOfId = listOfRolePermissionId }, transaction);
            }
        }
        #endregion

        #region RemoveBasedOnRemovedClientRole
        /// <summary>
        /// RemoveBasedOnRemovedClientRole
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="listOfRemovedClientRole"></param>
        public void RemoveBasedOnRemovedClientRole(IDbConnection connection, IDbTransaction transaction, List<APIClientRole> listOfRemovedClientRole)
        {
            if (listOfRemovedClientRole != null && listOfRemovedClientRole.Count() > 0)
            {
                // remove client user
                connection.Execute(RolePermissionQuery.RemoveBasedOnRemovedClientRole, new
                {
                    ListOfClientRoleId = listOfRemovedClientRole.Select(cr => cr.Id)
                }, transaction);
            }
        }
        #endregion

        #region RemovedPermissionByRemovedClientPermission
        /// <summary>
        /// RemovedPermissionByRemovedClientPermission
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="clientId"></param>
        /// <param name="listOfRemovedPermissionId"></param>
        public void RemovedPermissionByRemovedClientPermission(IDbConnection connection, IDbTransaction transaction, Guid clientId, List<int> listOfRemovedPermissionId)
        {
            if (listOfRemovedPermissionId != null && listOfRemovedPermissionId.Count() > 0)
            {
                connection.Execute(RolePermissionQuery.RemovedListOfPermissionByRemovedClientPermission, new { ClientId = clientId, ListOfRemovedPermissionId = listOfRemovedPermissionId }, transaction);
            }
        }
        #endregion

        #region Get new permission for user from new role
        /// <summary>
        /// Get new permission for user from new role
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="listOfNewRoleId"></param>
        /// <param name="listOfExistingPermissionId"></param>
        /// <returns></returns>
        public List<int> GetNewPermissionForUserFromNewRole(Guid clientId, List<int> listOfNewRoleId, List<int> listOfExistingPermissionId)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return connection.Query<int>(
                        RolePermissionQuery.GetNewPermissionForUserFromNewRole,
                        new
                        {
                            ClientId = clientId,
                            ListOfNewRoleId = listOfNewRoleId,
                            ListOfExistingPermissionId = listOfExistingPermissionId
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<int>();
            }
        }
        #endregion

        #region not implemented
        public List<APIRolePermission> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public APIRolePermission Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(APIRolePermission entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(APIRolePermission entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<APIRolePermission> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
