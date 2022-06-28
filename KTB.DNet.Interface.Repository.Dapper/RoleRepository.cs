#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Role repository class
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
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.Role;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class RoleRepository : BaseRepository<APIRole>, IRoleRepository<APIRole, int>
    {
        #region Constructor
        public RoleRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region GetTotalRoles
        /// <summary>
        /// GetTotalRoles
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetTotalRoles(int userId)
        {
            try
            {
                if (userId == AppConfigs.GetInt("DMSAdminUserId"))
                {
                    return GetAll().Count();
                }
                else
                {
                    return GetUserRole(userId).Count();
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        #endregion

        #region GetUserRole
        /// <summary>
        /// GetUserRole
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<APIRole> GetUserRole(int userId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIRole>(RoleQuery.GetRoleByUserId, new { UserId = userId }).AsList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIRole Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIRole>(RoleQuery.GetRoleById, new { Id = id }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIRole entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                SetCreatedLog(entity);

                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(RoleQuery.InsertRole, entity, transaction);
                });

                entity.Id = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Role with name {0} has been successfully created.", entity.Name);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create role. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIRole entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                APIRole existingEntity = Get(entity.Id);
                if (existingEntity != null)
                {
                    existingEntity.Name = entity.Name;
                    existingEntity.Level = entity.Level;

                    SetLastModifiedLog(existingEntity);

                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(RoleQuery.UpdateRole, existingEntity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Role with name {0} has been successfully updated.", entity.Name);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Role does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Role." + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (id > 0)
                {
                    if (id == AppConfigs.GetInt("DMSAdminRoleId"))
                    {
                        throw new Exception("Administrator role is restricted to be deleted.");
                    }

                    // TODO: restrict delete if role is used by other entity
                    ClientRoleRepository clientRoleRepository = new ClientRoleRepository(this._connectionString);
                    var clientRoles = clientRoleRepository.GetClientRoleByRoleId(id);
                    UserRoleRepository userRoleRepository = new UserRoleRepository(this._connectionString);
                    var userRoles = userRoleRepository.GetByRoleId(id);
                    if (clientRoles.Count() > 0 || userRoles.Count() > 0)
                    {
                        throw new Exception("Role that used by other entity is restricted to be deleted.");
                    }

                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(RoleQuery.DeleteRole, new { Id = id }, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Role with id {0} has been successfully deleted", id);
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete role. " + GetInnerException(ex).Message;
            }
            return responseMessage;

        }
        #endregion

        #region GetAll
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public List<APIRole> GetAll()
        {
            using (var connection = Connection)
            {
                return connection.Query<APIRole>(RoleQuery.GetAllRole).ToList();
            }
        }
        #endregion

        #region Search
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APIRole> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "CreatedTime DESC", out keyword, out orderBy);
                List<APIRole> result = Search<APIRole>((connection, query, sqlParams) =>
                {
                    return connection.Query<APIRole>(query, sqlParams).ToList();
                }, Connection, RoleQuery.SearchRole
                , "Id", new { Keyword = keyword }, orderBy, out filteredResultsCount, model.Start, model.Length);

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIRole>();
            }
        }
        #endregion
    }
}

