#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : UserPermission repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:42
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.UserPermission;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class UserPermissionRepository : BaseRepository<APIUserPermission>, IUserPermissionRepository<APIUserPermission, int>
    {
        #region Constructor
        public UserPermissionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create User Permission
        /// <summary>
        /// Create user permission
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIUserPermission entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                SetCreatedLog(entity);
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return Create(connection, transaction, entity);
                });

                entity.Id = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New User Permission has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create user permission. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Create user permission with external connection
        /// <summary>
        /// Create user permission with external connection
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="userPermission"></param>
        /// <returns></returns>
        public int Create(IDbConnection connection, IDbTransaction transaction, APIUserPermission userPermission)
        {
            SetCreatedLog(userPermission);
            return connection.ExecuteScalar<int>(UserPermissionQuery.InsertUserPermission, userPermission, transaction);
        }
        #endregion

        #region Update User Permission
        /// <summary>
        /// Update User Permission
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIUserPermission entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                APIUserPermission existingUserPermission = Get(entity.Id);
                if (existingUserPermission != null)
                {
                    existingUserPermission.IsCustomPermission = entity.IsCustomPermission;
                    existingUserPermission.IsDismantledPermission = entity.IsDismantledPermission;

                    SetLastModifiedLog(existingUserPermission);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(UserPermissionQuery.UpdateUserPermission, existingUserPermission, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = "User permission has been successfully updated";
                    responseMessage.Data = existingUserPermission;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "User permission Config does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update user permission " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Get by id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIUserPermission Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIUserPermission>(UserPermissionQuery.GetUserPermissionById, new { Id = id }).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region GetUserPermissionByClientUserId
        /// <summary>
        /// GetUserPermissionByClientUserId
        /// </summary>
        /// <param name="clientUserId"></param>
        /// <returns></returns>
        public List<APIUserPermission> GetUserPermissionByClientUserId(int clientUserId)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return connection.Query<APIUserPermission, APIEndpointPermission, APIUserPermission>(
                        UserPermissionQuery.GetUserPermissionByClientUserId,
                        (userPermission, permission) =>
                        {
                            userPermission.Permission = permission;
                            return userPermission;
                        }, new { ClientUserId = clientUserId }, null, true, "Id,Id").ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIUserPermission>();
            }
        }
        #endregion

        #region SaveUserPermission
        /// <summary>
        /// SaveUserPermission
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listOfSelectedUserPermission"></param>
        /// <returns></returns>
        public ResponseMessage SaveUserPermission(int userId, List<APIUserPermission> listOfSelectedUserPermission)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (listOfSelectedUserPermission != null && listOfSelectedUserPermission.Count() > 0)
                {
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        SaveUserPermission(connection, transaction, userId, listOfSelectedUserPermission);
                        return 1;
                    });
                }

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("User Permission for userId [{0}] has been successully saved", userId);
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update user's permissions " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region SaveUserPermission
        /// <summary>
        /// Save User Permission
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="userId"></param>
        /// <param name="listOfUserPermission"></param>
        public void SaveUserPermission(IDbConnection connection, IDbTransaction transaction, int userId, List<APIUserPermission> listOfUserPermission, bool doCheckExisting = true, bool doNothingIfExist = false)
        {
            #region add/ remove user permission
            if (listOfUserPermission != null && listOfUserPermission.Count() > 0)
            {
                int clientUserId = listOfUserPermission[0].ClientUserId;

                // add new custom permission/ update permission
                listOfUserPermission.ForEach(userPermission =>
                {

                    APIUserPermission existingUserPermission = doCheckExisting ?
                        connection.Query<APIUserPermission>(UserPermissionQuery.GetUserPermissionByClientUserIdAndPermissionId, userPermission, transaction).SingleOrDefault()
                        : null;

                    if (existingUserPermission != null)
                    {
                        if (!doNothingIfExist)
                        {
                            // update
                            SetLastModifiedLog(existingUserPermission);
                            existingUserPermission.IsCustomPermission = userPermission.IsCustomPermission;
                            existingUserPermission.IsDismantledPermission = userPermission.IsDismantledPermission;

                            connection.Execute(UserPermissionQuery.UpdateUserPermission, existingUserPermission, transaction);
                        }
                    }
                    else
                    {
                        // add new
                        SetCreatedLog(userPermission);
                        connection.Execute(UserPermissionQuery.InsertUserPermission, userPermission, transaction);
                    }

                });

                if (userId != AppConfigs.GetInt("DMSAdminUserId"))
                {
                    List<int> listOfSelectedPermissionId = listOfUserPermission.Select(up => up.PermissionId).ToList();
                    connection.Execute(UserPermissionQuery.DeleteUnselectedUserPermission, new { ClientUserId = clientUserId, ListOfSelectedPermissionId = listOfSelectedPermissionId }, transaction);
                }
            }
            #endregion
        }
        #endregion

        #region RemoveBasedOnRemovedClientUser
        /// <summary>
        /// RemoveBasedOnRemovedClientUser
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="listOfRemovedClientUser"></param>
        public void RemoveBasedOnRemovedClientUser(IDbConnection connection, IDbTransaction transaction, List<APIClientUser> listOfRemovedClientUser)
        {
            if (listOfRemovedClientUser != null && listOfRemovedClientUser.Count() > 0)
            {
                connection.Execute(UserPermissionQuery.RemoveUserPermissionBasedOnRemovedClientUser, new { ListOfClientUserId = listOfRemovedClientUser.Select(cu => cu.Id) }, transaction); 
            }
        }
        #endregion

        #region RemovedPermissionButExcludePermissionFromUnremovedRole
        /// <summary>
        /// RemovedPermissionButExcludePermissionFromUnremovedRole
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="clientUserId"></param>
        /// <param name="listOfUnremovedRoleId"></param>
        public void RemovedPermissionButExcludePermissionFromUnremovedRole(IDbConnection connection, IDbTransaction transaction, int clientUserId, List<int> listOfUnremovedRoleId)
        {
            List<int> listOfUnremovedPermissionId = new List<int> { -1 };
            if (listOfUnremovedRoleId != null && listOfUnremovedRoleId.Count() > 0)
            {
                listOfUnremovedPermissionId = connection.Query<int>(UserPermissionQuery.GetUnremovedPermissionIdBasedOnUnremovedRole, new { ClientUserId = clientUserId, ListOfUnremovedRoleId = listOfUnremovedRoleId }, transaction).ToList();
            }
            connection.Execute(UserPermissionQuery.RemovedPermissionButExcludeSomePermission, new { ClientUserId = clientUserId, ListOfUnremovedPermissionId = listOfUnremovedPermissionId }, transaction);
        }
        #endregion

        #region RemovedPermissionButExcludePermissionFromUnremovedRole
        /// <summary>
        /// RemovedPermissionButExcludePermissionFromUnremovedRole
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="clientUserId"></param>
        /// <param name="listOfRemovedPermissionId"></param>
        /// <param name="listOfUnremovedRoleId"></param>
        public void RemovedPermissionButExcludePermissionFromUnremovedRole(IDbConnection connection, IDbTransaction transaction, int clientUserId, List<int> listOfRemovedPermissionId, List<int> listOfUnremovedRoleId)
        {
            List<int> listOfUnremovedPermissionId = new List<int> { -1 };
            if (listOfUnremovedRoleId != null && listOfUnremovedRoleId.Count() > 0)
            {
                listOfUnremovedPermissionId = connection.Query<int>(UserPermissionQuery.GetUnremovedPermissionIdBasedOnUnremovedRole, new { ClientUserId = clientUserId, ListOfUnremovedRoleId = listOfUnremovedRoleId }, transaction).ToList();
            }

            connection.Execute(UserPermissionQuery.RemovedListOfPermissionButExcludeSomePermission, new { ClientUserId = clientUserId, ListOfRemovedPermissionId = listOfRemovedPermissionId, ListOfUnremovedPermissionId = listOfUnremovedPermissionId }, transaction);
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
                connection.Execute(UserPermissionQuery.RemovedListOfPermissionByRemovedClientPermission, new { ClientId = clientId, ListOfRemovedPermissionId = listOfRemovedPermissionId }, transaction); 
            }
        }
        #endregion

        #region InsertUserPermission
        /// <summary>
        /// InsertUserPermission
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="listOfUserPermission"></param>
        public void InsertUserPermission(IDbConnection connection, IDbTransaction transaction, List<APIUserPermission> listOfUserPermission)
        {
            if (listOfUserPermission != null && listOfUserPermission.Count() > 0)
            {
                connection.Execute(UserPermissionQuery.InsertUserPermission, listOfUserPermission, transaction); 
            }
        }
        #endregion

        #region Not Implemented
        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<APIUserPermission> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<APIUserPermission> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
