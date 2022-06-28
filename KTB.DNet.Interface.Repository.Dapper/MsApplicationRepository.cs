#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MsApplication repository class
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
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.MsApplication;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class MsApplicationRepository : BaseRepository<MsApplication>, IMsApplicationRepository<MsApplication, Guid>
    {
        #region Constructor
        public MsApplicationRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Method Create
        /// <summary>
        /// Create MsApplication
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listOfSelectedPermissionId"></param>
        /// <returns></returns>
        public ResponseMessage Create(MsApplication entity, List<int> listOfSelectedPermissionId)
        {
            var responseMessage = new ResponseMessage();

            try
            {
                MsApplicationPermissionRepository msApplicationPermissionRepo = new MsApplicationPermissionRepository(this._connectionString);

                MsApplication application = Connection.Query<MsApplication>(MsApplicationQuery.GetApplicationByName, new { Name = entity.Name }).SingleOrDefault();
                if (application == null)
                {
                    MsApplication msApplication = new MsApplication
                    {
                        AppId = Guid.NewGuid(),
                        Name = entity.Name,
                        DeploymentJenkinsJobName = entity.DeploymentJenkinsJobName,
                        DeploymentBackupFolder = entity.DeploymentBackupFolder
                    };

                    SetCreatedLog(msApplication);

                    #region List of MsApplication Permission
                    var listOfMsApplicationPermission = new List<MsApplicationPermission>();
                    if (listOfSelectedPermissionId != null && listOfSelectedPermissionId.Count() > 0)
                    {
                        foreach (var permissionId in listOfSelectedPermissionId)
                        {
                            var msApplicationPermission = new MsApplicationPermission
                            {
                                AppId = msApplication.AppId,
                                PermissionId = permissionId
                            };

                            SetCreatedLog(msApplicationPermission);
                            listOfMsApplicationPermission.Add(msApplicationPermission);
                        }
                    }
                    #endregion

                    var result = ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        connection.Execute(MsApplicationQuery.InsertApplication, msApplication, transaction);
                        msApplicationPermissionRepo.AddApplicationPermission(connection, transaction, listOfMsApplicationPermission);
                        return msApplication.AppId;
                    });

                    responseMessage = new ResponseMessage
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Message = "New Application has been successfully created.",
                        Data = entity
                    };

                }
                else
                {
                    responseMessage = new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Warning,
                        Message = string.Format("MsApplication with name {0} has already exist.", entity.Name),
                        Data = entity
                    };
                }

            }
            catch (Exception ex)
            {
                responseMessage = new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Failed to create new client : " + GetInnerException(ex).Message
                };
            }

            return responseMessage;

        }
        #endregion

        #region Method Update
        /// <summary>
        /// Update MsApplication
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listOfSelectedPermissionId"></param>
        /// <returns></returns>
        public ResponseMessage Update(MsApplication entity, List<int> listOfSelectedPermissionId)
        {
            var responseMessage = new ResponseMessage();

            try
            {
                if (entity != null)
                {
                    var existingApplication = Get(entity.AppId);

                    if (existingApplication != null)
                    {
                        existingApplication.Name = entity.Name;
                        existingApplication.DeploymentBackupFolder = entity.DeploymentBackupFolder;
                        existingApplication.DeploymentJenkinsJobName = entity.DeploymentJenkinsJobName;
                        SetLastModifiedLog(existingApplication);

                        if (!(listOfSelectedPermissionId != null && listOfSelectedPermissionId.Count() > 0))
                        {
                            responseMessage = new ResponseMessage
                            {
                                Success = false,
                                Status = ResponseStatus.Warning,
                                Message = "Please select at least one permission."
                            };
                            return responseMessage;
                        }

                        MsApplicationPermissionRepository msApplicationPermissionRepo = new MsApplicationPermissionRepository(this._connectionString);
                        msApplicationPermissionRepo.SetUserLogin(this.UserLogin);

                        List<MsApplicationPermission> listOfExistingAppPermission = msApplicationPermissionRepo.GetByAppId(entity.AppId);
                        List<int> listOfExistingPermissionId = listOfExistingAppPermission.Select(p => p.PermissionId).ToList();

                        List<MsApplicationPermission> listOfNewAppPermission = listOfSelectedPermissionId.Where(permissionId => !listOfExistingPermissionId.Contains(permissionId))
                                                                                .Select(permissionId =>
                                                                                {
                                                                                    MsApplicationPermission appPermission = new MsApplicationPermission() { AppId = existingApplication.AppId, PermissionId = permissionId };
                                                                                    SetCreatedLog(appPermission);
                                                                                    return appPermission;
                                                                                }).ToList();
                        List<MsApplicationPermission> listOfRemovedAppPermission = listOfExistingAppPermission.Where(p => !listOfSelectedPermissionId.Contains(p.PermissionId)).ToList();

                        ClientPermissionRepository clientPermissionRepo = new ClientPermissionRepository(this._connectionString);
                        clientPermissionRepo.SetUserLogin(this.UserLogin);

                        ClientRepository clientRepo = new ClientRepository(this._connectionString);
                        clientRepo.SetUserLogin(this.UserLogin);

                        List<APIClient> listOfAppClient = clientRepo.GetByAppId(existingApplication.AppId);

                        bool isDMSAdminApp = existingApplication.AppId.ToString().ToLower().Trim() == AppConfigs.GetString("DMSAdminAppId").ToLower().Trim();
                        if (isDMSAdminApp && listOfRemovedAppPermission != null && listOfRemovedAppPermission.Count() > 0)
                        {
                            return new ResponseMessage
                            {
                                Success = false,
                                Status = ResponseStatus.Warning,
                                Message = "Administrator application permission is restricted to be deleted."
                            };
                        }

                        var result = ExecuteTransaction(Connection, (connection, transaction) =>
                        {
                            msApplicationPermissionRepo.AddApplicationPermission(connection, transaction, listOfNewAppPermission);

                            if (listOfRemovedAppPermission != null && listOfRemovedAppPermission.Count() > 0)
                            {
                                msApplicationPermissionRepo.RemoveApplicationPermission(connection, transaction, listOfRemovedAppPermission.Select(p => p.Id).ToList());

                                foreach (APIClient client in listOfAppClient)
                                {
                                    clientPermissionRepo.RemoveListOfClientPermission(connection, transaction, client.ClientId, listOfRemovedAppPermission.Select(p => p.PermissionId).ToList(), true);
                                }
                            }

                            connection.Execute(MsApplicationQuery.UpdateApplication, existingApplication, transaction);

                            return existingApplication.AppId;

                        });

                        responseMessage.Success = true;
                        responseMessage.Status = ResponseStatus.Success;
                        responseMessage.Message = string.Format("The Application with name {0} has been successfully updated", entity.Name);
                        responseMessage.Data = entity;

                    }
                    else
                    {
                        responseMessage = new ResponseMessage()
                        {
                            Success = false,
                            Status = ResponseStatus.Warning,
                            Message = string.Format("The Application with id {0} does not exist", entity.AppId)
                        };
                    }
                }
                else
                {
                    responseMessage = new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Warning,
                        Message = "The Application cannot be null"
                    };
                }
            }
            catch (Exception ex)
            {

                responseMessage = new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Failed to update client : " + GetInnerException(ex).Message
                };
            }

            return responseMessage;
        }
        #endregion

        #region Method Get Permission By App Id
        /// <summary>
        /// Get Permission By App Id
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> GetPermission(Guid appId)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<APIEndpointPermission>(MsApplicationQuery.GetPermissionByAppId, new { AppId = appId }).ToList();
                }
            }
            catch (Exception)
            {
                return new List<APIEndpointPermission>();
            }
        }
        #endregion

        #region Method Get By Id
        /// <summary>
        /// Get App by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsApplication Get(Guid id)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<MsApplication>(MsApplicationQuery.GetApplicationById, new { Id = id }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method Create
        /// <summary>
        /// Create App
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(MsApplication entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Method Update
        /// <summary>
        /// Update App
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(MsApplication entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Method Delete
        /// <summary>
        /// Delete App
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(Guid id)
        {
            var responseMessage = new ResponseMessage();
            if (id != null)
            {
                if (id.ToString().ToLower() == AppConfigs.GetString("DMSAdminAppId").ToLower().Trim())
                    responseMessage = new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Warning,
                        Message = "Administrator application is restricted to be deleted."
                    };
            }

            var existingApplication = Get(id);
            if (existingApplication != null)
            {
                try
                {
                    var result = ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(MsApplicationQuery.DeleteApplication, new { @AppId = existingApplication.AppId }, transaction);
                    });

                    responseMessage = new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Message = string.Format("The Application with name {0} has successfully deleted", existingApplication.Name)
                    };

                }
                catch (Exception ex)
                {
                    responseMessage = new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Error,
                        Message = GetInnerException(ex).Message
                    };
                }


            }
            else
            {
                responseMessage = new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = string.Format("Failed to delete Application with id {0}. The application is not exist", id)
                };
            }

            return responseMessage;
        }
        #endregion

        #region Method Get All
        /// <summary>
        /// Get All Applications
        /// </summary>
        /// <returns></returns>
        public List<MsApplication> GetAll()
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<MsApplication>(MsApplicationQuery.GetAllApplication).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<MsApplication>();
            }
        }
        #endregion

        #region Method Search
        /// <summary>
        /// Search Ms Application
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<MsApplication> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "CreatedTime DESC", out keyword, out orderBy);
                List<MsApplication> result = Search<MsApplication>((connection, query, sqlParams) =>
                {
                    return connection.Query<MsApplication>(query, sqlParams).ToList();
                },
                    Connection,
                    MsApplicationQuery.SearchApplication,
                    "AppId",
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
                return new List<MsApplication>();
            }
        }
        #endregion

        #region Method Get List of App Permissions
        /// <summary>
        /// Get List of App Permissions by App Id
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<MsApplicationPermission> GetListOfMsAppPermissionByAppId(Guid appId)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<MsApplicationPermission>(MsApplicationQuery.GetApplicationPermissionByAppId, new { AppId = appId }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<MsApplicationPermission>();
            }
        }
        #endregion

        #region Not Implemented
        public List<MsApplication> GetListOfApplication(Guid clientId, bool isDMSAdmin)
        {
            try
            {
                if (isDMSAdmin)
                {
                    using (var connection = Connection)
                    {
                        return connection.Query<MsApplication>(MsApplicationQuery.GetAllApplication).ToList();
                    }
                }
                else
                {
                    using (var connection = Connection)
                    {
                        return connection.Query<MsApplication>(MsApplicationQuery.GetByClientId, new { clientId = clientId }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<MsApplication>();
            }

        }
        #endregion
    }
}
