#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Throttle repository class
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
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.Throttle;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class ThrottleRepository : BaseRepository<APIThrottle>, IThrottleRepository<APIThrottle, int>
    {
        #region Constructor
        public ThrottleRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Method Get Throttle by Id
        /// <summary>
        /// Get Throttle By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIThrottle Get(int id)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<APIThrottle, APIEndpointPermission, APIThrottle>(ThrottleQuery.GetThrottleById,
                        map: (apiThrottle, endpoint) =>
                        {
                            endpoint.Id = apiThrottle.EndpointId;
                            apiThrottle.Endpoint = endpoint;
                            return apiThrottle;
                        },
                        splitOn: "Name",
                        param: new { Id = id }).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method Throttle by URI
        /// <summary>
        /// Get Throttle By URI 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public APIThrottle GetByUri(string uri)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<APIThrottle, APIEndpointPermission, APIThrottle>(ThrottleQuery.GetThrottleByUri,
                        map: (apiThrottle, endpoint) =>
                    {
                        endpoint.Id = apiThrottle.EndpointId;
                        apiThrottle.Endpoint = endpoint;
                        return apiThrottle;
                    },
                        splitOn: "Name",
                        param: new { Uri = uri }).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method Create Throttle
        /// <summary>
        /// Create Throttle
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIThrottle entity)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                SetCreatedLog(entity);
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.ExecuteScalar<int>(ThrottleQuery.InsertThrottle, entity, transaction);
                    });

                entity.Id = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New Throttle has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Success = false;
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create throttle. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Method Update Throttle
        /// <summary>
        /// Update Throttle
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIThrottle entity)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                APIThrottle existingThrottle = Get((int)entity.Id);
                if (existingThrottle != null)
                {
                    entity.CreatedBy = existingThrottle.CreatedBy;
                    entity.CreatedTime = existingThrottle.CreatedTime;

                    SetLastModifiedLog(entity);

                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(ThrottleQuery.UpdateThrottle, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Throttle with id {0} has been successfully updated", entity.Id);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Success = false;
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Throttle does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update throttle. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Method Delete Throttle
        /// <summary>
        /// Delete Throttle
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
                                return connection.Execute(ThrottleQuery.DeleteThrottle, new
                                        {
                                            Id = id
                                        }, transaction);
                            });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Throttle with id {0} has been successfully deleted", id);

            }
            catch (Exception ex)
            {
                responseMessage.Success = false;
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete throttle. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Method Get All Throttle
        /// <summary>
        /// Get All Throttle
        /// </summary>
        /// <returns></returns>
        public List<APIThrottle> GetAll()
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<APIThrottle, APIEndpointPermission, APIThrottle>(ThrottleQuery.GetAllThrottle,
                        map: (apiThrottle, endpoint) =>
                        {
                            endpoint.Id = apiThrottle.EndpointId;
                            apiThrottle.Endpoint = endpoint;
                            return apiThrottle;
                        },
                        splitOn: "Name").ToList();
                }
            }
            catch (Exception)
            {
                return new List<APIThrottle>();
            }
        }
        #endregion

        #region Method Search
        /// <summary>
        /// Search Throttle
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APIThrottle> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "APIThrottle.CreatedTime DESC", out keyword, out orderBy);
                List<APIThrottle> result = Search<APIThrottle>((connection, query, sqlParams) =>
                    {
                        return connection.Query<APIThrottle, APIEndpointPermission, APIThrottle>(query,
                            map: (apiThrottle, endpoint) =>
                        {
                            endpoint.Id = apiThrottle.EndpointId;
                            apiThrottle.Endpoint = endpoint;
                            return apiThrottle;
                        },
                            splitOn: "Name",
                            param: sqlParams).ToList();
                    },
                    Connection,
                    ThrottleQuery.SearchThrottle,
                    "APIThrottle.Id",
                    new { Keyword = keyword },
                    orderBy,
                    out filteredResultsCount,
                    model.Start,
                    model.Length);

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIThrottle>();
            }
        }
        #endregion

    }
}
