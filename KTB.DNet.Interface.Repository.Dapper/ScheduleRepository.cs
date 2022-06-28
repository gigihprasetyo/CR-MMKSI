#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Schedule repository class
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
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.Schedule;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class ScheduleRepository : BaseRepository<APISchedule>, IScheduleRepository<APISchedule, int>
    {
        #region Constructor
        public ScheduleRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Get
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APISchedule Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APISchedule>(ScheduleQuery.GetScheduleById, new { Id = id }).SingleOrDefault();
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
        public ResponseMessage Create(APISchedule entity)
        {

            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                SetCreatedLog(entity);
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar(ScheduleQuery.InsertSchedule, entity, transaction);
                });

                entity.Id = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Schedule with name {0} has been successfully created.", entity.Name);
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Schedule. " + GetInnerException(ex).Message;
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
        public ResponseMessage Update(APISchedule entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                APISchedule existingSchedule = Get(entity.Id);
                if (existingSchedule != null)
                {
                    existingSchedule.DealerCode = entity.DealerCode;
                    existingSchedule.Interval = entity.Interval;
                    existingSchedule.MonthDay = entity.MonthDay;
                    existingSchedule.Name = entity.Name;
                    existingSchedule.ScheduleDay = entity.ScheduleDay;
                    existingSchedule.ScheduleTime = entity.ScheduleTime;
                    existingSchedule.ScheduleType = entity.ScheduleType;

                    SetLastModifiedLog(existingSchedule);

                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(ScheduleQuery.UpdateSchedule, existingSchedule, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Schedule with name {0} has been successfully updated.", entity.Name);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Schedule does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Schedule." + GetInnerException(ex).Message;
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
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(ScheduleQuery.DeleteSchedule, new
                    {
                        Id = id
                    }, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Schedule with id {0} has been successfully deleted", id);

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete schedule. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region GetAll
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public List<APISchedule> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APISchedule>(ScheduleQuery.GetAllSchedule).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APISchedule>();
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
        public List<APISchedule> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "CreatedTime DESC", out keyword, out orderBy);
                List<APISchedule> result = Search<APISchedule>((connection, query, sqlParams) =>
                {
                    return connection.Query<APISchedule>(query, sqlParams).ToList();
                }, Connection, ScheduleQuery.SearchSchedule
                , "Id", new { Keyword = keyword }, orderBy, out filteredResultsCount, model.Start, model.Length);

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APISchedule>();
            }
        }
        #endregion
    }
}
