using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace KTB.DNet.Interface.Repository
{
    public class MsApplicationRepository : BaseRepository, IMsApplicationRepository<MsApplication, Guid>
    {

        #region Method Get
        /// <summary>
        /// Get MsApplication by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MsApplication Get(Guid id)
        {
            try
            {
                if (id != null)
                {
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        MsApplication msApplication = db.MsApplications.FirstOrDefault(x => x.AppId == id);
                        return msApplication;
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

        #region Method Create
        public ResponseMessage Create(MsApplication entity, List<int> listOfSelectedPermissionId)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var isExist = db.MsApplications.Any(x => x.Name == entity.Name);

                    if (!isExist)
                    {

                        if (!(listOfSelectedPermissionId != null && listOfSelectedPermissionId.Count() > 0))
                        {
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Please select at least one permission." };
                        }

                        MsApplication msApplication = new MsApplication();
                        msApplication.AppId = Guid.NewGuid();
                        msApplication.Name = entity.Name;
                        msApplication.DeploymentJenkinsJobName = entity.DeploymentJenkinsJobName;
                        msApplication.DeploymentBackupFolder = entity.DeploymentBackupFolder;

                        SetCreatedLog(msApplication);

                        db.MsApplications.Add(msApplication);

                        #region add permission
                        List<APIEndpointPermission> selectedPermissions = db.EndpointPermissions.Where(p => listOfSelectedPermissionId.Contains(p.Id)).ToList();

                        List<MsApplicationPermission> msApplicationPermissions = selectedPermissions.Select(permission =>
                        {
                            MsApplicationPermission msApplicationPermission =
                              new MsApplicationPermission
                                  {
                                      PermissionId = permission.Id,
                                      Permission = permission,
                                      MsApplication = msApplication,
                                      AppId = msApplication.AppId,

                                  };
                            SetCreatedLog(msApplicationPermission);
                            return msApplicationPermission;
                        }).ToList();
                        db.MsApplicationPermissions.AddRange(msApplicationPermissions);
                        #endregion

                        db.SaveChanges();

                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New MsApplication has been successfully created.", Data = entity };
                    }

                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("MsApplication with name {0} has already exist.", entity.Name), Data = entity };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Failed to create new client : " + GetInnerException(ex).Message };
            }

        }
        #endregion

        #region Method Update
        public ResponseMessage Update(MsApplication entity, List<int> listOfSelectedPermissionId)
        {
            try
            {
                if (entity != null)
                {

                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        var isExist = db.MsApplications.Any(x => x.AppId == entity.AppId);

                        if (isExist)
                        {
                            var existingEntity = db.MsApplications.FirstOrDefault(x => x.AppId == entity.AppId);

                            existingEntity.Name = entity.Name;
                            existingEntity.DeploymentJenkinsJobName = entity.DeploymentJenkinsJobName;
                            existingEntity.DeploymentBackupFolder = entity.DeploymentBackupFolder;

                            SetLastModifiedLog(existingEntity);

                            if (!(listOfSelectedPermissionId != null && listOfSelectedPermissionId.Count() > 0))
                            {
                                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Please select at least one permission." };
                            }

                            #region Update Client Permission
                            List<APIEndpointPermission> selectedPermissions = db.EndpointPermissions.Where(p => listOfSelectedPermissionId.Contains(p.Id)).ToList();
                            List<MsApplicationPermission> existingPermissions = db.MsApplicationPermissions.Where(p => p.AppId == existingEntity.AppId).ToList();

                            if (listOfSelectedPermissionId != null && listOfSelectedPermissionId.Count() > 0)
                            {
                                List<int> existingPermissionIds = existingPermissions.Select(cp => cp.PermissionId).ToList();

                                if (existingPermissionIds.Count() > 0)
                                {
                                    var additems = selectedPermissions.Where(permission => !existingPermissionIds.Contains(permission.Id)).ToList();
                                    if (additems.Count > 0)
                                    {
                                        //bulk add
                                        db.MsApplicationPermissions.AddRange(additems.Select(permission =>
                                        {
                                            MsApplicationPermission msApplicationPermission = new MsApplicationPermission
                                        {
                                            PermissionId = permission.Id,
                                            Permission = permission,
                                            MsApplication = existingEntity,
                                            AppId = existingEntity.AppId,
                                        };

                                            SetCreatedLog(msApplicationPermission);
                                            return msApplicationPermission;
                                        }));
                                    }

                                    var deleteitems = existingPermissions.Where(permission => !listOfSelectedPermissionId.Contains(permission.PermissionId)).ToList();
                                    if (deleteitems.Count > 0 &&
                                            entity.AppId.ToString().ToLower() != AppConfigs.GetString("DMSAdminAppId").ToLower().Trim())
                                    {
                                        DeleteApplicationPermission(db, deleteitems, existingEntity.AppId);
                                    }

                                }
                                else
                                {
                                    db.MsApplicationPermissions.AddRange(selectedPermissions.Select(permission => new MsApplicationPermission
                                    {
                                        MsApplication = existingEntity,
                                        AppId = existingEntity.AppId,
                                        Permission = permission,
                                        PermissionId = permission.Id,
                                        CreatedBy = existingEntity.CreatedBy,
                                        CreatedTime = existingEntity.CreatedTime,

                                    }));
                                }
                            }
                            else
                            {

                                if (existingPermissions != null &&
                                        existingPermissions.Count() > 0 &&
                                        entity.AppId.ToString().ToLower() != AppConfigs.GetString("DMSAdminAppId").ToLower().Trim())
                                {
                                    DeleteApplicationPermission(db, existingPermissions, existingEntity.AppId);
                                }

                            }
                            #endregion

                            existingEntity.UpdatedBy = entity.UpdatedBy;
                            existingEntity.UpdatedTime = DateTime.Now;

                            db.SaveChanges();

                            entity.AppId = existingEntity.AppId;

                            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("The Application with name {0} has successfully updated", entity.Name), Data = entity };

                        }

                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("The Application with id {0} does not exist", entity.AppId) };
                    }


                }

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "The Application cannot be null" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }
        #endregion

        #region Method Delete
        /// <summary>
        /// Delete Application
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(Guid id)
        {
            if (id != null)
            {
                if (id.ToString().ToLower() == AppConfigs.GetString("DMSAdminAppId").ToLower().Trim())
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Administrator application is restricted to be deleted." };
                }

                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {

                    var existingEntity = db.MsApplications.FirstOrDefault(x => x.AppId == id);

                    if (existingEntity != null)
                    {
                        string applicationName = existingEntity.Name;

                        var existInUser = db.MsApplications.Any(x => x.AppId == id);
                        if (existInUser)
                        {
                            db.MsApplications.RemoveRange(db.MsApplications.Where(cu => cu.AppId == existingEntity.AppId));
                        }


                        DeleteApplicationPermission(db, db.MsApplicationPermissions.Where(cr => cr.AppId == existingEntity.AppId).ToList(), existingEntity.AppId);
                        db.MsApplications.Remove(existingEntity);

                        db.SaveChanges();

                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("The Application with name {0} has successfully deleted", applicationName) };
                    }
                    else
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = string.Format("Failed to delete Application with id {0}. The application is not exist", id) };
                    }
                }

            }
            else
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Application Id could not be null" };
            }
        }
        #endregion

        #region Method Get All
        public List<MsApplication> GetAll()
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.MsApplications.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<MsApplication>();
            }
        }
        #endregion

        #region Method Search
        public List<MsApplication> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            string keyword;
            PropertyInfo orderedProperty = null;
            int take, skip;
            bool orderDir;

            GetPostModelData<MsApplication>(model, out keyword, "Name", out orderedProperty, out orderDir, out take, out skip);

            // search the dbase taking into consideration table sorting and paging
            var result = Filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<MsApplication>();
            }

            return result;
        }
        #endregion

        #region Method Delete Application Permission
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="listOfApplicationPermission"></param>
        /// <param name="clientId"></param>
        private void DeleteApplicationPermission(DNETInterfaceDBContext db, List<MsApplicationPermission> listOfApplicationPermission, Guid clientId)
        {
            if (clientId.ToString().ToLower() == AppConfigs.GetString("DMSAdminAppId").ToLower().Trim())
            {
                throw new Exception("Administrator client permission is restricted to be updated.");
            }

            // delete all role permission and user permission who has permission from the permission which will be deleted

            List<int> listOfPermissionId = listOfApplicationPermission.Select(cp => cp.PermissionId).ToList();


            db.MsApplicationPermissions.RemoveRange(listOfApplicationPermission);
        }
        #endregion

        #region Method Filter
        /// <summary>
        /// Filter 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        private List<MsApplication> Filter(string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount)
        {

            List<MsApplication> result = new List<MsApplication>();

            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                IQueryable<MsApplication> queryablePermission = db.MsApplications;

                result = queryablePermission.AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return !keywordExists ||
                                                (keywordExists && (u.Name != null ? u.Name.ToUpper().Contains(keyword) : false));
                                        }
                                     )
                               .OrderByWithDirection(u => orderedProperty.GetValue(u, null), !orderDir)
                               .ToList();

                filteredResultsCount = result.Count();

                totalResultsCount = queryablePermission.Count();

                result = result
                           .Skip(skip)
                           .Take(take)
                           .ToList();
            }

            return result;
        }
        #endregion

        #region Method Create
        public ResponseMessage Create(MsApplication entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Method Update
        public ResponseMessage Update(MsApplication entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Method GetPermission
        public List<APIEndpointPermission> GetPermission(Guid appId)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.MsApplicationPermissions.Include(p => p.Permission).Where(p => p.AppId == appId).Select(p => p.Permission).OrderBy(p => p.PermissionCode).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIEndpointPermission>();
            }
        }
        #endregion

        public List<MsApplicationPermission> GetListOfMsAppPermissionByAppId(Guid appId)
        {
            throw new NotImplementedException();
        }


        #region Method Get Application List
        public List<MsApplication> GetApplicationList(List<APIClient> listOfClients, bool isDMSAdmin)
        {
            try
            {
                List<MsApplication> msApplicationList = new List<MsApplication>();

                if (!isDMSAdmin)
                {
                    foreach (APIClient client in listOfClients)
                    {
                        using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                        {
                            MsApplication msApplication = (db.Clients.Include(x => x.MsApplication).First(x => x.ClientId == client.ClientId)).MsApplication;
                            msApplicationList.Add(msApplication);
                        }
                    }
                }

                else
                {
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        msApplicationList = db.MsApplications.ToList();
                    }
                }
                return msApplicationList;
            }
            catch (Exception ex)
            {

                return new List<MsApplication>();
            }
        }
        #endregion
    }
}
