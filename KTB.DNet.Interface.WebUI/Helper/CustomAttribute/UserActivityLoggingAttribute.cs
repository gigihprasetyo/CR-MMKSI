#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : UserActivityLoggingAttribute.cs class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Repository.Dapper;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebUI.Controllers;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Http.Filters;
#endregion

namespace KTB.DNet.Interface.WebUI.Helper
{
    public class UserActivityLoggingAttribute : ActionFilterAttribute
    {
        private IUserActivityRepository<UserActivity, long> _userActivityRepo;

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                if (actionExecutedContext.Response.IsSuccessStatusCode)
                {
                    BaseController bc = (BaseController)actionExecutedContext.ActionContext.ControllerContext.Controller;

                    if (bc != null)
                    {
                        APIEndpointPermission permission = bc.CurrentEndpointPermission;
                        if (permission != null)
                        {
                            string endpoint = actionExecutedContext.Request.RequestUri.AbsoluteUri;
                            string requestData = GetRequestData(actionExecutedContext);
                            string responseData = GetResponseData(actionExecutedContext);

                            switch (permission.OperationType)
                            {
                                case OperationType.Create:
                                    LogUserActivity(bc, UserActivityType.Create, endpoint, requestData, responseData);
                                    break;
                                case OperationType.Update:
                                    LogUserActivity(bc, UserActivityType.Update, endpoint, requestData, responseData);
                                    break;
                                case OperationType.Delete:
                                    LogUserActivity(bc, UserActivityType.Delete, endpoint, requestData, responseData);
                                    break;
                                case OperationType.Deploy:
                                    LogUserActivity(bc, UserActivityType.Deploy, endpoint, requestData, responseData);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {

                // do nothing
            }



            base.OnActionExecuted(actionExecutedContext);
        }

        private void LogUserActivity(BaseController bc, UserActivityType activityType, string endpoint, string requestData, string responseData)
        {
            _userActivityRepo = new UserActivityRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.LogConnection));
            _userActivityRepo.SetUserLogin(bc.UserName);

            _userActivityRepo.Create(
                new UserActivity()
                {
                    Username = bc.UserName,
                    Activity = activityType,
                    ActivityDesc = string.Format("activity: {0}, request: {1}, response: {2}", activityType.ToString(), requestData, responseData),
                    ActivityTime = DateTime.Now,
                    Endpoint = endpoint,
                    DealerCode = bc.DealerCode,
                    AppId = new Guid(AppConfigs.GetString("AppId"))
                }
            );
        }

        private string GetRequestData(HttpActionExecutedContext actionExecutedContext)
        {

            string dataOnrequestBody = string.Empty;

            try
            {
                // read data on request body
                using (var stream = new MemoryStream())
                {
                    var context = (HttpContextBase)actionExecutedContext.ActionContext.Request.Properties["MS_HttpContext"];
                    context.Request.InputStream.Seek(0, SeekOrigin.Begin);
                    context.Request.InputStream.CopyTo(stream);
                    dataOnrequestBody = Encoding.UTF8.GetString(stream.ToArray());
                }

                return dataOnrequestBody;
            }
            catch (Exception ex)
            {
                return dataOnrequestBody;
            }
        }

        private string GetResponseData(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response.IsSuccessStatusCode)
            {
                return actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
            }

            return string.Empty;
        }
    }
}
