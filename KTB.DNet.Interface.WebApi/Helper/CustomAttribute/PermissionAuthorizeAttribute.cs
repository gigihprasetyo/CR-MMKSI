#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PermissionAuthorizeAttribute class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Interface.BusinessLogic;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Dapper;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.WebApi.Controllers;
using KTB.DNet.Interface.WebApi.Helper.Messages;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PermissionAuthorizeAttribute : ActionFilterAttribute
    {
        public string PermissionName { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var dealerAuthFailed = String.Empty;
            try
            {
                var hasPermission = HasPermission(actionContext, UserName, PermissionName, DealerCode, out dealerAuthFailed);
                if (hasPermission)
                {
                    return;
                }
                else
                {
                    LogAuthorizationError(actionContext, dealerAuthFailed);
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                }
            }
            catch (SqlException ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

                var message = new MessageBase()
                {
                    ErrorCode = ErrorCode.DBRetrieveFailed,
                    ErrorMessage = "Akses Database gagal! Silakan hubungi Administrator Operasional!"
                };

                var errorResult = HttpCodeMessage.BuildErrorResult(message);
                HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(message);
                var json = JsonConvert.SerializeObject(errorResult);

                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                response.ReasonPhrase = "Akses Database gagal! Silakan hubungi Administrator Operasional!";

                LogAuthorizationError(actionContext, dealerAuthFailed);
                actionContext.Response = response;

            }
            catch (Exception ex)
            {
                LogAuthorizationError(actionContext, dealerAuthFailed);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }

        /// <summary>
        /// Return User Name from Login
        /// </summary>
        protected string UserName
        {
            get
            {
                var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var userName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
                    if (userName != null)
                        return userName.Value;
                }

                return null;
            }
        }

        protected Guid ClientId
        {
            get
            {
                var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var clientId = claims.FirstOrDefault(x => x.Type == "ClientId");
                    if (clientId != null)
                    {
                        return new Guid(clientId.Value);
                    }

                }

                return new Guid();
            }
        }

        protected string DealerCode
        {
            get
            {
                var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var dealerCode = claims.FirstOrDefault(x => x.Type == "DealerCode");
                    if (dealerCode != null)
                    {
                        return dealerCode.Value;
                    }

                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Check User has the permission
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="userName"></param>
        /// <param name="permissionCode"></param>
        /// <param name="dealerId"></param>
        /// <returns></returns>
        private bool HasPermission(HttpActionContext actionContext, string userName, string permissionCode, string dealerCode, out string dealerAuthFailed)
        {
            #region Set database string for DMS Data
            DatabaseSettings settings = (DatabaseSettings)ConfigurationManager.GetConfiguration("dataConfiguration");
            ConnectionStringData connString = settings.ConnectionStrings[Constants.ConnectionStringName.DNetConnection];
            string connectionString = AppConfigs.ConnectionString(
                                        connString.Parameters["server"].Value,
                                        connString.Parameters["database"].Value,
                                        connString.Parameters["uid"].Value,
                                        connString.Parameters["password"].Value
                                        );
            #endregion

            #region Set variables and repositories
            bool hasPermission = false;
            dealerAuthFailed = String.Empty;
            IUserRepository<APIUser, int> userRepository = new UserRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
            IDealerRepository<Dealer, int> dealerRepository = new DealerRepository(connectionString);
            IStandardCodeRepository<StandardCode, int> standardCodeRepository = new StandardCodeRepository(connectionString);
            IAppConfigBL appConfigBL = new AppConfigBL();
            #endregion

            var permissions = userRepository.GetPermission(userName, ClientId);
            var userPermission = permissions.FirstOrDefault(
                x => x.Permission.PermissionCode == permissionCode);

            if (userPermission != null)
            {

                hasPermission = !userPermission.IsDismantledPermission;
                KeepTheEndpoint(actionContext, userPermission.Permission);

                if (hasPermission)
                {
                    #region Additional logic for Dealer Auth
                    var appConfig = appConfigBL.GetConfigByName(Constants.ConfigKey.WebAPI_IsDealerAuthChecked);
                    if (appConfig != null && appConfig.Value.ToBool())
                    {
                        var endpointGroup = userPermission.Permission.EndpointGroup;
                        if (endpointGroup != 0)
                        {
                            var standardCodes = standardCodeRepository.GetByCategory("EndpointGroup");
                            var standardCode = standardCodes.FirstOrDefault(x => x.ValueId == endpointGroup);

                            if (standardCode != null)
                            {
                                var dealer = dealerRepository.GetByCode(dealerCode);
                                var standardCodeValue = standardCode.ValueCode;
                                if (standardCodeValue == Constants.EndpointGroupCode.Sales)
                                {
                                    hasPermission = dealer.SalesUnitFlag.ToBool();
                                    dealerAuthFailed = Constants.EndpointGroupCode.Sales;
                                }
                                else if (standardCodeValue == Constants.EndpointGroupCode.Service)
                                {
                                    hasPermission = dealer.ServiceFlag.ToBool();
                                    dealerAuthFailed = Constants.EndpointGroupCode.Service;
                                }
                                else if (standardCodeValue == Constants.EndpointGroupCode.SparePart)
                                {
                                    hasPermission = dealer.SparepartFlag.ToBool();
                                    dealerAuthFailed = Constants.EndpointGroupCode.SparePart;
                                }
                            }
                        }
                    }
                    #endregion
                }

            }

            return hasPermission;
        }

        /// <summary>
        /// Log Unauthorized Action
        /// </summary>
        /// <param name="context"></param>
        private void LogAuthorizationError(HttpActionContext context, string dealerAuthFailed)
        {
            try
            {
                BaseController bc = (BaseController)context.ControllerContext.Controller;
                MessageBase message = new MessageBase();
                message.ErrorCode = ErrorCode.AuthNoPrivilege;
                message.ErrorMessage = MessageResource.ErrorMsgAuthPermission;

                if (!string.IsNullOrEmpty(dealerAuthFailed))
                {
                    message.ErrorMessage = string.Format(MessageResource.ErrorMsgDealerAuth, dealerAuthFailed);
                }

                List<MessageBase> messages = new List<MessageBase>();
                messages.Add(message);

                var errorResult = HttpCodeMessage.BuildErrorResult(messages);

                var logId = bc.LogActionFilterError(errorResult, false);
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }

        /// <summary>
        /// Keep The Endpoint
        /// </summary>
        /// <param name="context"></param>
        /// <param name="endpoint"></param>
        private void KeepTheEndpoint(HttpActionContext context, APIEndpointPermission endpoint)
        {
            try
            {
                BaseController bc = (BaseController)context.ControllerContext.Controller;
                bc.SetCurrentEndpoint(endpoint);
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }

    }
}