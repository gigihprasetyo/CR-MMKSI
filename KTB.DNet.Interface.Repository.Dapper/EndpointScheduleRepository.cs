#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EndpointSchedule repository class
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
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.EndpointSchedule;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class EndpointScheduleRepository : BaseRepository<APIEndpointSchedule>, IEndpointScheduleRepository<APIEndpointSchedule, int>
    {
        #region Constructor
        public EndpointScheduleRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region AddEndpointSchedule
        /// <summary>
        /// Add Endpoint Schedule 
        /// </summary>
        /// <param name="listOfEndpointSchedule"></param>
        /// <returns></returns>
        public ResponseMessage AddEndpointSchedule(List<APIEndpointSchedule> listOfEndpointSchedule)
        {
            try
            {
                if (listOfEndpointSchedule != null && listOfEndpointSchedule.Count > 0)
                {
                    foreach (APIEndpointSchedule endpointSchedule in listOfEndpointSchedule)
                    {
                        SetCreatedLog(endpointSchedule);
                    }

                    var result = ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.ExecuteScalar(EndpointScheduleQuery.InsertEndpointSchedule, listOfEndpointSchedule, transaction);
                    });

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New Endpoint Schedules have been successfully created." };
                }

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "No Endpoint Schedules selected" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Failed to create endpoint schedule. " + GetInnerException(ex).Message };
            }
        }
        #endregion

        #region GetByEndpointId
        /// <summary>
        /// Get Endpoint Schedule by Endpoint Id
        /// </summary>
        /// <param name="endpointId"></param>
        /// <returns></returns>
        public List<APIEndpointSchedule> GetByEndpointId(int endpointId)
        {
            List<APIEndpointSchedule> result = new List<APIEndpointSchedule>();
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIEndpointSchedule, APISchedule, APIEndpointSchedule>(
                        EndpointScheduleQuery.GetEndpointScheduleByEndpointId,
                        (endpointSchedule, schedule) =>
                        {
                            endpointSchedule.Schedule = schedule;
                            return endpointSchedule;
                        },
                            new { EndpointId = endpointId },
                            splitOn: "Id,Id"
                        ).AsList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region GetByEndpointUrl
        /// <summary>
        /// Get Endpoint Schedule by Url Endpoint
        /// </summary>
        /// <param name="endpointUrl"></param>
        /// <returns></returns>
        public List<APIEndpointSchedule> GetByEndpointUrl(string endpointUrl)
        {
            var result = new List<APIEndpointSchedule>();
            try
            {
                if (!String.IsNullOrEmpty(endpointUrl))
                {
                    using (var cn = Connection)
                    {
                        return cn.Query<APIEndpointSchedule, APIEndpointPermission, APIEndpointSchedule>
                            (
                                EndpointScheduleQuery.GetEndpointScheduleByEndpointUrl,
                                (apiSchedule, apiEndPoint) =>
                                {
                                    apiSchedule.Endpoint = apiEndPoint;
                                    return apiSchedule;
                                },
                                new { EndpointUrl = endpointUrl },
                                splitOn: "Id,Id"
                            )
                            .Distinct()
                            .AsList();
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return result;
        }
        #endregion

        #region SearchByEndpointId
        /// <summary>
        /// Search By EndPoint ID
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <param name="endPointId"></param>
        /// <returns></returns>
        public List<APIEndpointSchedule> SearchByEndpointId(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, int endpointId)
        {
            try
            {
                return Search(model, out filteredResultsCount, out totalResultsCount, endpointId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// Get EndpointSchedule by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIEndpointSchedule Get(int id)
        {
            try
            {
                if (id != 0)
                {
                    using (var cn = Connection)
                    {
                        return cn.Query<APIEndpointSchedule>(EndpointScheduleQuery.GetEndpointScheduleById, new { Id = id }).FirstOrDefault();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create Endpoint Schedule
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIEndpointSchedule entity)
        {
            try
            {
                SetCreatedLog(entity);

                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(EndpointScheduleQuery.InsertEndpointSchedule, entity, transaction);
                });

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New Endpoint Schedule has been successfully created.", Data = entity };

            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Update Endpoint Schedule
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIEndpointSchedule entity)
        {
            try
            {
                APIEndpointSchedule existingData = Get(entity.Id);
                if (existingData != null)
                {
                    existingData.EndpointId = entity.EndpointId;
                    existingData.ScheduleId = entity.ScheduleId;
                    existingData.Endpoint = entity.Endpoint;
                    existingData.Schedule = entity.Schedule;
                    SetLastModifiedLog(existingData);

                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(EndpointScheduleQuery.UpdateEndpointSchedule, existingData, transaction);
                    });

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Endpoint Schedule has been successfully updated.", Data = existingData };
                }

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Endpoint Schedule not found in database." };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id"></param>
        public ResponseMessage Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(EndpointScheduleQuery.DeleteEndpointSchedule, new { Id = id }, transaction);
                    });

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Endpoint Schedule has been successfully deleted." };
                }
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Endpoint Schedule not found in database." };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Get All Endpoint Schedule
        /// </summary>
        /// <returns></returns>
        public List<APIEndpointSchedule> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIEndpointSchedule>(EndpointScheduleQuery.GetAllEndpointSchedule).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// <param name="endpointId"></param>
        /// <returns></returns>
        public List<APIEndpointSchedule> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, int endpointId = 0)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "APISchedule.CreatedTime DESC", out keyword, out orderBy);
                List<APIEndpointSchedule> result = Search<APIEndpointSchedule>(
                    (connection, sql, sqlParams) =>
                    {
                        return connection.Query<APIEndpointSchedule, APISchedule, APIEndpointSchedule>(
                            sql,
                            (endpointSchedule, schedule) =>
                            {
                                endpointSchedule.Schedule = schedule;
                                return endpointSchedule;
                            },
                                sqlParams,
                                splitOn: "ScheduleId"
                            ).AsList();
                    },
                    Connection, EndpointScheduleQuery.SearchEndpointSchedule,
                    "ScheduleId",
                    new { Keyword = keyword, EndpointId = endpointId },
                    orderBy,
                    out filteredResultsCount,
                    model.Start,
                    model.Length
                );

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIEndpointSchedule>();
            }

        }
        #endregion

        #region not implemented

        public List<APIEndpointSchedule> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
