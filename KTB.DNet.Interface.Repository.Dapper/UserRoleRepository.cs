#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : UserRole repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 3/12/2018 8:29
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.UserRole;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class UserRoleRepository : BaseRepository<APIUserRole>, IUserRoleRepository<APIUserRole, int>
    {
        #region Constructor
        public UserRoleRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Save User Role with return response message
        /// <summary>
        /// Save User Role with return response message
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listOfRoleId"></param>
        /// <returns></returns>
        public ResponseMessage SaveUserRole(int userId, List<int> listOfRoleId)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            if (listOfRoleId != null && listOfRoleId.Count > 0)
            {
                try
                {
                    List<APIUserRole> listOfUserRole = new List<APIUserRole>();

                    foreach (int roleId in listOfRoleId)
                    {
                        listOfUserRole.Add(new APIUserRole() { UserId = userId, RoleId = roleId });
                    }

                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        SaveUserRole(connection, transaction, userId, listOfUserRole, null);
                        return 1;
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("User's roles for userId [{0}] has been successully saved", userId);
                }
                catch (Exception ex)
                {
                    responseMessage.Status = ResponseStatus.Error;
                    responseMessage.Message = "Failed to save user's roles " + GetInnerException(ex).Message;
                }
            }
            else
            {
                responseMessage.Status = ResponseStatus.Warning;
                responseMessage.Message = "Role has not been selected or is not valid";
            }

            return responseMessage;
        }
        #endregion

        #region SaveUserRole with no return
        /// <summary>
        /// Save user role together with User, Clients, and Permissions
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        /// <param name="listOfUserRole"></param>
        /// <param name="listOfClientUser"></param>
        public void SaveUserRole(IDbConnection connection, IDbTransaction transaction, int userId, List<APIUserRole> listOfUserRole, List<APIClientUser> listOfClientUser)
        {
            try
            {
                ClientUserRepository clientUserRepository = new ClientUserRepository(this._connectionString);
                clientUserRepository.SetUserLogin(this.UserLogin);

                if (listOfClientUser == null || listOfClientUser.Count() == 0)
                {
                    listOfClientUser = clientUserRepository.GetByUserId(userId);
                }
                else
                {
                    foreach (APIClientUser clientUser in listOfClientUser)
                    {
                        if (clientUser.UserId != userId)
                        {
                            throw new Exception("Client User and User does not match");
                        }
                    }
                }

                List<APIUserRole> listOfExistingUserRole = GetByUserId(userId);
                List<int> listOfExistingRoleId = listOfExistingUserRole.Select(userRole => userRole.RoleId).ToList();

                // filter new user role
                List<APIUserRole> listOfNewUserRole = listOfUserRole.Where(userRole => !listOfExistingRoleId.Contains(userRole.RoleId))
                    .Select(ur =>
                    {
                        ur.UserId = userId;
                        return ur;
                    }).ToList();

                DateTime today = DateTime.Now;
                foreach (APIUserRole ur in listOfNewUserRole)
                {
                    ur.CreatedBy = this.UserLogin;
                    ur.CreatedTime = today;
                    ur.UpdatedBy = this.UserLogin;
                    ur.UpdatedTime = today;

                    connection.Execute(UserRoleQuery.InsertUserRole, ur, transaction);
                }

                // add user permission
                AddPermissionBasedOnAdditionalRole(connection, transaction, listOfClientUser, listOfNewUserRole, userId);

                if (userId != AppConfigs.GetInt("DMSAdminUserId"))
                {
                    List<int> listOfSelectedRoleId = listOfUserRole.Select(r => r.RoleId).ToList();
                    List<APIUserRole> removedUserRole = listOfExistingUserRole.Where(r => !listOfSelectedRoleId.Contains(r.RoleId)).ToList();

                    RemoveUserRole(connection, transaction, removedUserRole, listOfSelectedRoleId, listOfClientUser);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Get by User id
        /// <summary>
        /// Get by User id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<APIUserRole> GetByUserId(int userId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIUserRole>(UserRoleQuery.GetUserRoleByUserId, new { UserId = userId }).ToList();
                }
            }
            catch (Exception)
            {
                return new List<APIUserRole>();
            }
        }
        #endregion

        #region Get By Role Id
        public List<APIUserRole> GetByRoleId(int roleId)
        {
            try
            {
                var results = new List<APIUserRole>();

                using (var connection = Connection)
                {
                    results = connection.Query<APIUserRole>(UserRoleQuery.GetUserRoleByRoleId, new { @RoleId = roleId }).ToList();
                }

                return results;
            }
            catch (Exception)
            {
                return new List<APIUserRole>();
            }
        }
        #endregion

        #region AddPermissionBasedOnAdditionalRole
        /// <summary>
        /// AddPermissionBasedOnAdditionalRole
        /// </summary>
        /// <param name="listOfNewClientUser"></param>
        private void AddPermissionBasedOnAdditionalRole(IDbConnection connection, IDbTransaction transaction, List<APIClientUser> listOfClientUser, List<APIUserRole> listOfNewUserRole, int userId)
        {
            if (listOfNewUserRole == null ||
                listOfClientUser == null ||
                listOfNewUserRole.Count < 1 ||
                listOfClientUser.Count < 1)
            { return; }

            UserPermissionRepository userPermissionRepo = new UserPermissionRepository(this._connectionString);
            RolePermissionRepository rolePermissionRepo = new RolePermissionRepository(this._connectionString);

            userPermissionRepo.SetUserLogin(this.UserLogin);
            rolePermissionRepo.SetUserLogin(this.UserLogin);

            List<APIUserPermission> listOfUserPermission = new List<APIUserPermission>();
            List<int> listOfNewUserRoleId = listOfNewUserRole.Select(userRole => userRole.RoleId).ToList();

            listOfClientUser.ForEach(clientUser =>
            {
                List<APIUserPermission> existingUserPermission = userPermissionRepo.GetUserPermissionByClientUserId(clientUser.Id);
                List<int> listOfExistingPermissionId = existingUserPermission.Select(up => up.PermissionId).ToList();

                // get distinct permission from role permission based on list of client role
                List<int> listOfNewPermissionId = rolePermissionRepo.
                    GetNewPermissionForUserFromNewRole(clientUser.ClientId, listOfNewUserRoleId, listOfExistingPermissionId);


                listOfNewPermissionId.ForEach(p =>
                {
                    APIUserPermission newUserPermission = new APIUserPermission() { PermissionId = p, ClientUserId = clientUser.Id };
                    SetCreatedLog(newUserPermission);
                    listOfUserPermission.Add(newUserPermission);
                });
            });

            foreach (APIUserPermission userPermission in listOfUserPermission)
            {
                userPermissionRepo.Create(connection, transaction, userPermission);
            }
        }
        #endregion

        #region RemovePermissionBasedOnRemovedRole
        /// <summary>
        /// RemovePermissionBasedOnRemovedRole
        /// </summary>
        /// <param name="listOfRemovedUserRole"></param>
        public void RemovePermissionBasedOnRemovedRole(IDbConnection connection, IDbTransaction transaction, List<int> listOfUnRemovedRoleId, List<APIClientUser> listOfExistingClientUser)
        {
            // remove all user permission based on role permission except the special permission (custom or dismantled)
            UserPermissionRepository userPermissionRepo = new UserPermissionRepository(this._connectionString);

            listOfExistingClientUser.ForEach(clientUser =>
            {

                userPermissionRepo
                    .RemovedPermissionButExcludePermissionFromUnremovedRole(
                    connection, transaction, clientUser.Id, listOfUnRemovedRoleId);

            });
        }
        #endregion

        #region Remove User Role
        /// <summary>
        /// Remove User Role
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="listOfRemovedUserRole"></param>
        /// <param name="listOfUnRemovedRoleId"></param>
        /// <param name="listOfExistingClientUser"></param>
        private void RemoveUserRole(IDbConnection connection, IDbTransaction transaction, List<APIUserRole> listOfRemovedUserRole, List<int> listOfUnRemovedRoleId, List<APIClientUser> listOfExistingClientUser)
        {
            // remove user permission
            if (listOfRemovedUserRole != null && listOfRemovedUserRole.Count() > 0)
            {
                RemovePermissionBasedOnRemovedRole(connection, transaction, listOfUnRemovedRoleId, listOfExistingClientUser);
            }
            // remove client user
            foreach (APIUserRole userRole in listOfRemovedUserRole)
            {
                connection.Execute(UserRoleQuery.DeleteUserRole, userRole, transaction);
            }
        }
        #endregion

        #region Not Implemented
        public ResponseMessage Create(APIUserRole entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(APIUserRole entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<APIUserRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<APIUserRole> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        public APIUserRole Get(int id)
        {
            throw new NotImplementedException();
        }
        #endregion



    }
}
