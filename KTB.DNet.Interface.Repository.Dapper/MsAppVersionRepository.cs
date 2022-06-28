#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MsAppVersion repository class
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
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.MsAppVersion;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class MsAppVersionRepository : BaseRepository<MsAppVersion>, IMsAppVersionRepository<MsAppVersion, int>
    {
        #region Constructor
        public MsAppVersionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create Ms Application Version
        /// <summary>
        /// Create Ms Application Version
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(MsAppVersion entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                SetCreatedLog(entity);
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    if (entity.IsCurrentDeployment)
                    {
                        // if current version is current deployment,
                        // un-flag another version as current deployment
                        connection.Execute(MsAppVersionQuery.RemoveCurrentDeploymentFlag, entity, transaction);
                    }
                    return connection.ExecuteScalar<int>(MsAppVersionQuery.InsertMsAppVersion, entity, transaction);
                });

                entity.VersionId = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New Ms Application Version has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Ms Application Version. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Update Ms Application Version
        /// <summary>
        /// Update Ms Application Version
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(MsAppVersion entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                MsAppVersion existingMsAppVersion = Get((int)entity.VersionId);
                if (existingMsAppVersion != null)
                {
                    SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        if (entity.IsCurrentDeployment)
                        {
                            // if current version is current deployment,
                            // un-flag another version as current deployment
                            connection.Execute(MsAppVersionQuery.RemoveCurrentDeploymentFlag, entity, transaction);
                        }

                        return connection.Execute(MsAppVersionQuery.UpdateMsAppVersion, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Ms Application Version {0} has been successfully updated", entity.Version);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Ms Application Version does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Ms Application Version. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Delete Ms Application Version
        /// <summary>
        /// Delete Ms Application Version
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
                    return connection.Execute(MsAppVersionQuery.DeleteMsAppVersion, new
                    {
                        VersionId = id
                    }, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Ms Application Version with id {0} has been successfully deleted", id);

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete Ms Application Version. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Get Ms Application Version By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsAppVersion Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<MsAppVersion, MsApplication, MsAppVersion>(
                        MsAppVersionQuery.GetMsAppVersionById,
                        (appVersion, app) =>
                        {
                            appVersion.MsApplication = app;
                            appVersion.AppId = app.AppId;
                            return appVersion;
                        },
                        new { VersionId = id }
                        , null, true, "AppId").SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All Ms Application Version
        /// <summary>
        /// Get All Ms Application Version
        /// </summary>
        /// <returns></returns>
        public List<MsAppVersion> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<MsAppVersion>(MsAppVersionQuery.GetAllMsAppVersion).ToList();
                }
            }
            catch (Exception)
            {
                return new List<MsAppVersion>();
            }
        }
        #endregion

        #region Search Ms Application Version
        public List<MsAppVersion> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "MsAppVersion.CreatedTime DESC", out keyword, out orderBy);
                List<MsAppVersion> result = Search<MsAppVersion>((connection, query, sqlParams) =>
                {
                    return connection.Query<MsAppVersion, MsApplication, MsAppVersion>(
                        query,
                        (appVersion, app) =>
                        {
                            appVersion.MsApplication = app;
                            appVersion.AppId = app.AppId;
                            return appVersion;
                        },
                        sqlParams
                        , null, true, "AppId").ToList();
                }, Connection, MsAppVersionQuery.SearchMsAppVersion
                , "MsAppVersion.CreatedTime DESC", new { Keyword = keyword }, orderBy, out filteredResultsCount, model.Start, model.Length);

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<MsAppVersion>();
            }
        }
        #endregion

        #region Get current deployment version app
        /// <summary>
        /// Get current deployment version app
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public MsAppVersion GetCurrentDeploymentVersionApp(Guid appId)
        {
            try
            {
                using (var cn = Connection)
                {
                    MsAppVersion msAppVersion = cn.Query<MsAppVersion, MsApplication, MsAppVersion>(
                        MsAppVersionQuery.GetCurrentDeploymentVersionApp,
                        (appVersion, app) =>
                        {
                            appVersion.MsApplication = app;
                            appVersion.AppId = app.AppId;
                            return appVersion;
                        },
                        new { AppId = appId }
                        , null, true, "AppId").SingleOrDefault();
                    return msAppVersion;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Set As Current Deployment Version
        /// <summary>
        /// Set As Current Deployment Version
        /// </summary>
        /// <param name="appVersion"></param>
        /// <returns></returns>
        public ResponseMessage SetAsCurrentDeploymentVersion(MsAppVersion appVersion)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                MsAppVersion existingMsAppVersion = Get((int)appVersion.VersionId);
                if (existingMsAppVersion != null)
                {
                    SetLastModifiedLog(appVersion);
                    appVersion.IsCurrentDeployment = true;
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        connection.Execute(MsAppVersionQuery.RemoveCurrentDeploymentFlag, appVersion, transaction);
                        return connection.Execute(MsAppVersionQuery.UpdateMsAppVersion, appVersion, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Ms Application Version {0} has been set as current deployment", appVersion.Version);
                    responseMessage.Data = appVersion;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Ms Application Version does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed the Ms Application Version as current deployment. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion
    }
}
