#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Rollback controller class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

#region Namespace Imports
using JenkinsClient;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Framework.Common;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebUI.Helper;
using KTB.DNet.Interface.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class RollbackController : BaseController
    {
        #region Initialize
        private string jenkinsClientUrl = ConfigurationManager.AppSettings["JenkinsClientUrl"];
        private string jenkinsUserName = ConfigurationManager.AppSettings["JenkinsUserName"];
        private string jenkinsApiToken = ConfigurationManager.AppSettings["JenkinsApiToken"];

        private IMsApplicationRepository<MsApplication, Guid> _msApplicationRepo;
        #endregion

        #region Constructor
        public RollbackController(IMsApplicationRepository<MsApplication, Guid> msApplicationRepo)
        {
            _msApplicationRepo = msApplicationRepo;
        }
        #endregion


        #region Method Index
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_JenkinsJob_Read)]
        public async Task<IHttpActionResult> GetRollbackData()
        {
            var viewModel = new List<JenkinsEnvironment>();

            try
            {
                JenkinsClient.Client client = JenkinsClient.Client.Create(
                    jenkinsClientUrl, jenkinsUserName, jenkinsApiToken);

                List<MsApplication> jenkins = _msApplicationRepo.GetAll();

                foreach (var job in jenkins)
                {
                    if (!string.IsNullOrEmpty(job.DeploymentJenkinsJobName) && !job.DeploymentJenkinsJobName.Contains("Rollback"))
                    {
                        var deploymentViewModel = new List<DeploymentViewModel>();
                        string[] words = job.DeploymentJenkinsJobName.Split('-');
                        var category = words[1];
                        var envName = words[0] + '-' + words[1];

                        var appRollbackModel = new JenkinsEnvironment { Category = category, Name = envName, AppRollbackModels = new List<AppRollbackModel>() };

                        var existing = viewModel.FirstOrDefault(w => w.Category == category);

                        if (existing == null)
                        {
                            viewModel.Add(appRollbackModel);
                        }
                    }
                }

                foreach (var item in viewModel)
                {
                    getCategorizedAppRollbackModel(item);
                }

                return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = viewModel });

            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
            }
        }
        #endregion

        #region Method Get App Rollback Model
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listAppRolebackModel"></param>
        /// <param name="path"></param>
        private void getCategorizedAppRollbackModel(JenkinsEnvironment jenkinsEnvironment)
        {
            List<AppRollbackModel> result = new List<AppRollbackModel>();
            string path = string.Format(@"\\{0}\{1}\{2}",
                AppConfigs.GetString("JenkinsServer"),
                AppConfigs.GetString("JenkinsBackupFolder"),
                jenkinsEnvironment.Name);

            CustomUserImpersonater imp = null;
            try
            {
                string user = AppConfigs.GetString("JenkinsServerUser");
                string password = AppConfigs.GetString("JenkinsServerPwd");
                string webServer = AppConfigs.GetString("JenkinsServer");

                imp = new CustomUserImpersonater(user, password, webServer);

                if (imp != null && imp.Start())
                {
                    if (Directory.Exists(path))
                    {
                        List<DirModel> dirListModel = new List<DirModel>();
                        List<FileModel> fileListModel = new List<FileModel>();

                        IEnumerable<string> dirList = Directory.EnumerateDirectories(path);
                        foreach (string dir in dirList)
                        {
                            List<DirModel> dirListModel2 = new List<DirModel>();
                            List<FileModel> fileListModel2 = new List<FileModel>();
                            IEnumerable<string> dirList2 = Directory.EnumerateDirectories(dir);

                            var currentGroup = new ExplorerModel();
                            foreach (string dir2 in dirList2)
                            {
                                string dirData = dir2.Split('\\').Last();
                                string dateF = dirData.Split('_').First();
                                string timeF = dirData.Split('_').Last();
                                int year = int.Parse(dateF.Substring(0, 4));
                                int month = int.Parse(dateF.Substring(4, 2));
                                int day = int.Parse(dateF.Substring(6, 2));

                                int hour = int.Parse(timeF.Substring(0, 2));
                                int minute = int.Parse(timeF.Substring(2, 2));
                                int second = int.Parse(timeF.Substring(4, 2));

                                DateTime date1 = new DateTime(year, month, day, hour, minute, second);
                                string groupName = date1.ToString("MMM", CultureInfo.InvariantCulture) + " " + date1.Year;

                                var folderList = new List<ExplorerModel>();

                                string appName = dir.Split(new string[] { jenkinsEnvironment.Name + '\\' }, StringSplitOptions.None).Last();

                                if (string.IsNullOrEmpty(appName)) appName = "Default";

                                var appRollbackModel = new AppRollbackModel { AppName = appName, FolderList = new List<ExplorerModel>() };

                                var existingAppRollbackModel = jenkinsEnvironment.AppRollbackModels.Where(w => w.AppName == appName).FirstOrDefault();

                                if (existingAppRollbackModel != null)
                                {
                                    appRollbackModel = existingAppRollbackModel;
                                }
                                else
                                {
                                    jenkinsEnvironment.AppRollbackModels.Add(appRollbackModel);
                                }

                                folderList = appRollbackModel.FolderList;

                                currentGroup = folderList.Where(w => w.GroupName == groupName).FirstOrDefault();
                                if (currentGroup == null)
                                {
                                    currentGroup = new ExplorerModel { Date = date1 };
                                    currentGroup.GroupName = groupName;
                                    currentGroup.DirModelList.Add(new DirModel { Date = date1, DirName = dirData, When = date1.ToString("yyyy-MM-ddTHH:mm:ss") });
                                    folderList.Add(currentGroup);
                                }
                                else
                                {
                                    currentGroup.DirModelList.Add(new DirModel { Date = date1, DirName = dirData, When = date1.ToString("yyyy-MM-ddTHH:mm:ss") });
                                }
                            }
                        }
                    }
                    imp.Stop();
                }

                //sort data
                foreach (var item in jenkinsEnvironment.AppRollbackModels)
                {
                    item.FolderList = item.FolderList.OrderByDescending(o => o.Date).ToList();
                    foreach (var fdl in item.FolderList)
                    {
                        fdl.DirModelList = fdl.DirModelList.OrderByDescending(o => o.Date).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (imp != null)
                {
                    imp.Stop();
                    imp.Dispose();
                }
            }
        }
        #endregion

        #region Method Output
        ///// <summary>
        ///// Output
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_JenkinsJob_ViewOutput)]
        public async Task<IHttpActionResult> Output(JenkinsJobViewModel model)
        {
            try
            {
                JenkinsClient.Client client2 = JenkinsClient.Client.Create(jenkinsClientUrl, jenkinsUserName, jenkinsApiToken);
                Job job = client2.GetJob(model.Name);
                string res = await job.GetConsoleLog(model.BuildNumber);

                if (res.Length > 1000000)
                {
                    res = res.Substring(res.Length - 1000000);
                    string more = "...";
                    res = more + res;
                }

                int test = res.Length;

                return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = res });
            }
            catch (Exception e)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = e.Message });
            }
        }
        #endregion

        #region Method Run
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_JenkinsJob_Deploy)]
        public async Task<IHttpActionResult> Run(JenkinsJobViewModel model)
        {
            JenkinsClient.Client client2 = JenkinsClient.Client.Create(jenkinsClientUrl, jenkinsUserName, jenkinsApiToken);
            int nextBuildNumber = 0;
            string nextTimestamp = "";
            string jobName = model.Name + "-Rollback";
            //jobName = "Testing-Rollback";

            try
            {
                var job = client2.GetJob(jobName);

                var item = await job.BuildAsync(model.Parameter);

                nextBuildNumber = item.number;

                await item.WaitForBuildStart();
                await item.WaitForBuildEnd();

                if (job.lastFailedBuild != null)
                {
                    nextTimestamp = UnixTimeStampToDateTime(job.lastFailedBuild.timestamp);
                }

                if (item.result == "SUCCESS")
                {
                    return Json(new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Message = "Rolled back Successfully",
                        Data = new
                        {
                            success = true,
                            responseText = "Rolled back successfully",
                            timestamp = UnixTimeStampToDateTime(item.timestamp),
                            number = item.number
                        }
                    });

                }
                else
                {
                    return Json(new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Error,
                        Message = "Rollback Failed",
                        Data = new
                        {
                            success = false,
                            responseText = "Rollback Failed",
                            timestamp = UnixTimeStampToDateTime(item.timestamp),
                            number = item.number
                        }
                    });

                }
            }
            catch (Exception e)
            {
                var job = client2.GetJob(jobName);
                if (job.lastFailedBuild != null)
                {
                    if (job.lastFailedBuild.number > nextBuildNumber)
                    {
                        return Json(new ResponseMessage()
                        {
                            Success = false,
                            Status = ResponseStatus.Error,
                            Message = "Rollback Failed",
                            Data = new
                            {
                                success = false,
                                timestamp = UnixTimeStampToDateTime(job.lastFailedBuild.timestamp),
                                number = job.lastFailedBuild.number,
                                responseText = e.Message
                            }
                        });

                    }
                    else
                    {
                        return Json(new ResponseMessage()
                        {
                            Success = false,
                            Status = ResponseStatus.Error,
                            Message = "Rollback Failed",
                            Data = new
                            {
                                success = false,
                                timestamp = nextTimestamp,
                                number = nextBuildNumber,
                                responseText = e.Message
                            }
                        });

                    }
                }
                else
                {
                    return Json(new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Error,
                        Message = "Rollback Failed",
                        Data = new
                        {
                            success = false,
                            responseText = e.Message
                        }
                    });
                }
            }
        }
        #endregion

        #region Method Convert Unix Time to Date Time
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static string UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp / 1000).ToLocalTime();
            return dtDateTime.ToString();
        }
        #endregion

        //public ActionResult Index(string path)
        //{
        //    if (path == null)
        //    {
        //        path = string.Empty;
        //    }

        //    string realPath;
        //    realPath = @"P:\" + ConfigurationManager.AppSettings["JenkinsBackupFolder"] + "\\" + path;
        //    // or realPath = "FullPath of the folder on server" 

        //    if (Directory.Exists(realPath))
        //    {
        //        Uri url = Request.Url;
        //        //Every path needs to end with slash
        //        if (url.ToString().Last() != '/')
        //        {
        //            Response.Redirect(url + path + "/");
        //        }

        //        List<DirModel> dirListModel = new List<DirModel>();
        //        List<FileModel> fileListModel = new List<FileModel>();

        //        IEnumerable<string> dirList = Directory.EnumerateDirectories(realPath);
        //        foreach (string dir in dirList)
        //        {
        //            DirectoryInfo d = new DirectoryInfo(dir);

        //            DirModel dirModel = new DirModel();

        //            dirModel.DirName = Path.GetFileName(dir);

        //            IEnumerable<string> subfolders = Directory.EnumerateDirectories(realPath + dirModel.DirName);
        //            dirModel.IsHasSubDir = subfolders != null && subfolders.Any();

        //            if (!dirModel.IsHasSubDir)
        //            {
        //                IEnumerable<string> fileList = Directory.EnumerateFiles(realPath);
        //                foreach (string file in fileList)
        //                {
        //                    FileInfo f = new FileInfo(file);

        //                    FileModel fileModel = new FileModel();

        //                    if (f.Extension.ToLower() == "zip")
        //                    {
        //                        fileModel.FileName = Path.GetFileName(file);

        //                        fileListModel.Add(fileModel);
        //                    }
        //                }
        //            }

        //            dirListModel.Add(dirModel);
        //        }


        //        ExplorerModel explorerModel = new ExplorerModel(dirListModel, fileListModel);

        //        return View(explorerModel);
        //    }
        //    else
        //    {
        //        return Content(path + " is not a valid file or directory.");
        //    }
        //}
    }

}