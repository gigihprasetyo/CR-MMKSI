#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : UserActivity repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 4/12/2018 20:18
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.UserActivity;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class UserActivityRepository : BaseRepository<UserActivity>, IUserActivityRepository<UserActivity, long>
    {
        #region Constructor
        public UserActivityRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Method Search
        /// <summary>
        /// Search User Activity
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<UserActivity> Search(string dealerCode, DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "ActivityTime DESC", out keyword, out orderBy);
                List<UserActivity> result = Search<UserActivity>((connection, query, sqlParams) =>
                {
                    return connection.Query<UserActivity>(query, sqlParams).ToList();
                },
                    Connection,
                    UserActivityQuery.SearchUserActivity,
                    "ActivityTime DESC",
                    new
                    {
                        Keyword = keyword,
                        DealerCode = dealerCode ?? string.Empty
                    },
                    orderBy,
                    out filteredResultsCount,
                    model.Start,
                    model.Length);

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<UserActivity>();
            }
        }
        #endregion

        #region Method Get
        /// <summary>
        /// Get User Activity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserActivity Get(long id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<UserActivity>(UserActivityQuery.GetUserActivityById, new { Id = id }).SingleOrDefault();
                }
            }
            catch (Exception)
            {

                return new UserActivity();
            }
        }
        #endregion

        #region Method Create
        /// <summary>
        /// Create User Activity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(UserActivity entity)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                SetCreatedLog(entity);
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar(UserActivityQuery.InsertUserActivity, entity, transaction);
                });

                entity.Id = Convert.ToInt64(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New User Activity has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage = new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Failed to create User Activity. " + GetInnerException(ex).Message
                };
            }
            return responseMessage;
        }
        #endregion

        #region Method Update
        /// <summary>
        /// Update User Activity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(UserActivity entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                UserActivity userActivity = Get(entity.Id);
                if (userActivity == null)
                {
                    responseMessage.Success = false;
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "User Activity does not exist";
                }
                else
                {
                    userActivity.Activity = entity.Activity;
                    userActivity.ActivityDesc = entity.ActivityDesc;
                    userActivity.ActivityTime = entity.ActivityTime;
                    userActivity.AppId = entity.AppId;
                    userActivity.DealerCode = entity.DealerCode;
                    userActivity.Endpoint = entity.Endpoint;
                    userActivity.Username = entity.Username;

                    SetLastModifiedLog(userActivity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(UserActivityQuery.UpdateUserActivity, userActivity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("User activity for user with username {0} has been successfully updated", entity.Username);
                    responseMessage.Data = entity;
                }

            }
            catch (Exception ex)
            {
                responseMessage = new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                };
            }
            return responseMessage;
        }
        #endregion

        #region Method Delete
        /// <summary>
        /// Delete User Activity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(long id)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                UserActivity userActivity = Get(id);

                if (userActivity == null)
                {
                    responseMessage.Success = false;
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "User Activity does not exist";
                }

                else
                {
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(UserActivityQuery.DeleteUserActivity, new
                        {
                            Id = id
                        }, transaction);
                    });
                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("User Activity with id {0} has been successfully deleted", id);
                }
            }
            catch (Exception ex)
            {
                responseMessage = new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Failed to deleted User Activity. " + GetInnerException(ex).Message
                };
            }

            return responseMessage;
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Get All User Activity
        /// </summary>
        /// <returns></returns>
        public List<UserActivity> GetAll()
        {

            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<UserActivity>(UserActivityQuery.GetAllUserActivity).ToList();
                }
            }
            catch (Exception)
            {

                return new List<UserActivity>();
            }
        }
        #endregion

        #region Search (Not Implemented)
        /// <summary>
        /// Search User Activity
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<UserActivity> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
