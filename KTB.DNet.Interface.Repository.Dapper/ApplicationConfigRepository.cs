#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ApplicationConfig repository class
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
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.ApplicationConfig;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class ApplicationConfigRepository : BaseRepository<ApplicationConfig>, IApplicationConfigRepository<ApplicationConfig, long>
    {
        #region Constructor
        public ApplicationConfigRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create Application Config
        /// <summary>
        /// Create Application Config
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(ApplicationConfig entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                SetCreatedLog(entity);
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar<long>(ApplicationConfigQuery.InsertApplicationConfig, entity, transaction);
                });

                entity.Id = Convert.ToInt64(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New Application Config has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create application config. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Update Application Config
        /// <summary>
        /// Update Application Config
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(ApplicationConfig entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                ApplicationConfig existingAppConfig = Get((int)entity.Id);
                if (existingAppConfig != null)
                {
                    existingAppConfig.ConfigKey = entity.ConfigKey;
                    existingAppConfig.DataType = entity.DataType;
                    existingAppConfig.Description = entity.Description;
                    existingAppConfig.Id = entity.Id;
                    existingAppConfig.Name = entity.Name;
                    existingAppConfig.Value = entity.Value;
                    SetLastModifiedLog(existingAppConfig);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(ApplicationConfigQuery.UpdateApplicationConfig, existingAppConfig, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Application Config with name {0} has been successfully updated", entity.Name);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Application Config does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update application config. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Delete Application Config
        /// <summary>
        /// Delete Application Config
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(long id)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(ApplicationConfigQuery.DeleteApplicationConfig, new
                    {
                        Id = id
                    }, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Application Config with id {0} has been successfully deleted", id);

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete application config. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Get Application Config By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationConfig Get(long id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<ApplicationConfig>(ApplicationConfigQuery.GetApplicationConfigById, new { Id = id }).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get Application Config by Key
        /// <summary>
        /// Get application config by key
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public ApplicationConfig GetByKey(string configKey)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<ApplicationConfig>(ApplicationConfigQuery.GetApplicationConfigByKey, new { @ConfigKey = configKey }).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All Application Config
        /// <summary>
        /// Get All application config
        /// </summary>
        /// <returns></returns>
        public List<ApplicationConfig> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<ApplicationConfig>(ApplicationConfigQuery.GetAllApplicationConfig).ToList();
                }
            }
            catch (Exception)
            {
                return new List<ApplicationConfig>();
            }
        }
        #endregion

        #region Search Application Config
        public List<ApplicationConfig> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "CreatedTime DESC", out keyword, out orderBy);
                List<ApplicationConfig> result = Search<ApplicationConfig>((connection, query, sqlParams) =>
                {
                    return connection.Query<ApplicationConfig>(query, sqlParams).ToList();
                },
                Connection, // connection 
                ApplicationConfigQuery.SearchApplicationConfig, // query
                "Id",                   // default identifier/sorting 
                new { Keyword = keyword }, // sqlParams
                orderBy,                // sorting by (optional) 
                out filteredResultsCount, // total result 
                model.Start,            // start index 
                model.Length            // length of data will be retrieved
                );

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<ApplicationConfig>();
            }
        }
        #endregion

        #region Get Config Value
        /// <summary>
        /// Get config value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public T GetConfigValue<T>(string configKey)
        {
            T result = default(T);
            Type dataType = typeof(T);
            ApplicationConfig config = GetByKey(configKey);
            if (config != null)
            {
                if (dataType == typeof(int))
                {
                    result = (T)(object)config.Value.ToInt();
                }
                else if (dataType == typeof(string))
                {
                    result = (T)(object)config.Value;
                }
                else if (dataType == typeof(bool))
                {
                    result = (T)(object)config.Value.ToBool();
                }

            }

            return result;
        }
        #endregion

        #region Get unregistered permission code
        /// <summary>
        /// Get unregistered permission code
        /// </summary>
        /// <param name="listOfConstantPermissionCode"></param>
        /// <returns></returns>
        public List<string> GetUnregisteredConfigKey(List<string> listOfConstantConfigKey)
        {
            try
            {
                List<string> unregisteredConfigKey = new List<string>();
                List<string> listOfRegisteredConfigKey = GetAll().Where(c => !string.IsNullOrEmpty(c.ConfigKey)).Select(p => p.ConfigKey).ToList();
                if (listOfRegisteredConfigKey != null && listOfRegisteredConfigKey.Count > 0)
                {
                    unregisteredConfigKey = listOfConstantConfigKey.Where(p => !listOfRegisteredConfigKey.Contains(p)).OrderBy(p => p).ToList();
                }

                return unregisteredConfigKey;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        #endregion
    }
}
