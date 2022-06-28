#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Deployment controller class
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
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebUI.Helper;
using KTB.DNet.Interface.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class DeploymentController : BaseController
    {
        #region Initialize
        private IMsApplicationRepository<MsApplication, Guid> _msApplicationRepo;

        private string jenkinsClientUrl = ConfigurationManager.AppSettings["JenkinsClientUrl"];
        private string jenkinsUserName = ConfigurationManager.AppSettings["JenkinsUserName"];
        private string jenkinsApiToken = ConfigurationManager.AppSettings["JenkinsApiToken"];
        #endregion

        #region Constructor
        public DeploymentController(IMsApplicationRepository<MsApplication, Guid> msApplicationRepo)
        {
            _msApplicationRepo = msApplicationRepo;
        }

        #endregion

        #region Method Get Deployment Model
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_JenkinsJob_Read)]
        public async Task<IHttpActionResult> GetJenkinsJobs()
        {
            var categorizedJenkinsJobViewModels = new List<CategorizedJenkinsJobViewModel>();
            try
            {
                JenkinsClient.Client client = JenkinsClient.Client.Create(
                    jenkinsClientUrl, jenkinsUserName, jenkinsApiToken);

                var jenkins = await client.GetJobsAsync();
                var msApplicationList = _msApplicationRepo.GetAll();

                foreach (var job in msApplicationList.Where(w => !string.IsNullOrEmpty(w.DeploymentJenkinsJobName)))
                {
                    var jenkinsJob = jenkins.Where(w => w.name == job.DeploymentJenkinsJobName).FirstOrDefault();
                    if (jenkinsJob != null && !jenkinsJob.name.Contains("Rollback"))
                    {
                        var deploymentViewModel = new List<DeploymentViewModel>();
                        string[] words = jenkinsJob.name.Split('-');
                        var category = words[1];

                        var categorizedJenkinsJobViewModel = new CategorizedJenkinsJobViewModel { Name = category, JenkinsJobs = new List<JenkinsJobViewModel>() };

                        var existing = categorizedJenkinsJobViewModels.FirstOrDefault(w => w.Name == category);

                        if (existing != null)
                        {
                            categorizedJenkinsJobViewModel = existing;
                        }
                        else
                        {
                            categorizedJenkinsJobViewModels.Add(categorizedJenkinsJobViewModel);
                        }

                        var jjob = new JenkinsJobViewModel();
                        jjob.Name = jenkinsJob.name;
                        var lastSuccessfulBuild = jenkinsJob.lastSuccessfulBuild;
                        if (lastSuccessfulBuild != null)
                        {
                            jjob.LastSuccessfulBuildNumber = lastSuccessfulBuild.number;
                            jjob.LastSuccessfulBuildTimestamp = Utils.UnixTimeStampToDateTime(jenkinsJob.lastSuccessfulBuild.timestamp);
                        }

                        var lastFailedBuild = jenkinsJob.lastFailedBuild;
                        if (lastFailedBuild != null)
                        {
                            jjob.LastFailedBuildNumber = lastFailedBuild.number;
                            jjob.LastFailedBuildTimestamp = Utils.UnixTimeStampToDateTime(jenkinsJob.lastFailedBuild.timestamp);
                        }

                        categorizedJenkinsJobViewModel.JenkinsJobs.Add(jjob);


                    }
                }
                return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = categorizedJenkinsJobViewModels });

            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
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
                var client2 = JenkinsClient.Client.Create(jenkinsClientUrl, jenkinsUserName, jenkinsApiToken);
                var job = client2.GetJob(model.Name);
                var res = await job.GetConsoleLog(model.BuildNumber);

                if (res.Length > 1000000)
                {
                    res = res.Substring(res.Length - 1000000);
                    string more = "...";
                    res = more + res;
                }

                var test = res.Length;


                return Json(new { Success = true, Status = ResponseStatus.Success, Output = res });
            }
            catch (Exception e)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = e.Message });
            }
        }
        #endregion

        #region Method Deploy
        /// <summary>
        /// Deploy
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_JenkinsJob_Deploy)]
        public async Task<IHttpActionResult> Deploy(JenkinsJobViewModel model)
        {
            var client2 = JenkinsClient.Client.Create(jenkinsClientUrl, jenkinsUserName, jenkinsApiToken);
            int nextBuildNumber = 0;
            string nextTimestamp = "";
            try
            {
                var job = client2.GetJob(model.Name);

                var item = await job.BuildAsync(model.Parameter.Count > 0 ? model.Parameter : null);

                nextBuildNumber = item.number;

                await item.WaitForBuildStart();
                await item.WaitForBuildEnd();

                if (job.lastFailedBuild != null)
                {
                    nextTimestamp = Utils.UnixTimeStampToDateTime(job.lastFailedBuild.timestamp);
                }

                if (item.result == "SUCCESS")
                {
                    return Json(new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Message = "Deployed Successfully",
                        Data = new
                        {
                            success = true,
                            responseText = "Deployed successfully",
                            timestamp = Utils.UnixTimeStampToDateTime(item.timestamp),
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
                        Message = "Deployment Failed",
                        Data = new
                        {
                            success = false,
                            responseText = "Deployment Failed",
                            timestamp = Utils.UnixTimeStampToDateTime(item.timestamp),
                            number = item.number
                        }
                    });

                }
            }
            catch (Exception e)
            {
                var job = client2.GetJob(model.Name);
                if (job.lastFailedBuild != null)
                {
                    if (job.lastFailedBuild.number > nextBuildNumber)
                    {
                        return Json(new ResponseMessage()
                        {
                            Success = false,
                            Status = ResponseStatus.Error,
                            Message = "Deployment Failed",
                            Data = new
                            {
                                success = false,
                                timestamp = Utils.UnixTimeStampToDateTime(job.lastFailedBuild.timestamp),
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
                            Message = "Deployment Failed",
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
                        Message = "Deployment Failed",
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

        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_JenkinsJob_Deploy)]
        public async Task<IHttpActionResult> RestartJenkins()
        {
            try
            {
                JenkinsClient.Client client = JenkinsClient.Client.Create(
                    jenkinsClientUrl, jenkinsUserName, jenkinsApiToken);

                var result = await client.RestartJenkinsAsync();

                if (result)
                {
                    return Json(new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Message = "Jenkins Server Restarted Successfully",
                        Data = new
                        {
                            success = true,
                            responseText = "Jenkins Server Restarted Successfully"
                        }
                    });
                }
                else
                {
                    return Json(new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Error,
                        Message = "Jenkins Server Restart Failed",
                        Data = new
                        {
                            success = false,
                            responseText = "Jenkins Server Restart Failed"
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
            }
        }
    }

}