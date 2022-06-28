#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PermissionAuthorizeAttribute.cs class
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
using KTB.DNet.Interface.Repository.Dapper;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.WebUI.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
#endregion

namespace KTB.DNet.Interface.WebUI.Helper
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PermissionAuthorizeAttribute : ActionFilterAttribute
    {
        private string _dmsAdminClientId;
        public string PermissionName { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                APIEndpointPermission permission = null;
                var hasPermission = HasPermission(UserName, PermissionName, out permission);
                if (hasPermission)
                {
                    BaseController bc = (BaseController)actionContext.ControllerContext.Controller;
                    bc.CurrentEndpointPermission = permission;
                    return;
                }
            }
            catch
            {
                actionContext.Response = UnAuthorizedResponse(actionContext);
            }

            actionContext.Response = UnAuthorizedResponse(actionContext);
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

        /// <summary>
        /// User ClientId
        /// </summary>
        protected string ClientId
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
                        return clientId.Value.ToLower().Trim();
                    }

                }
                return string.Empty;

            }
        }

        /// <summary>
        /// ClientId of DMS Admin User
        /// </summary>
        protected string DMSAdminClientId
        {
            get
            {

                if (string.IsNullOrEmpty(_dmsAdminClientId))
                {
                    _dmsAdminClientId = AppConfigs.GetString("DMSAdminClientId").ToLower().Trim();
                }

                return _dmsAdminClientId;
            }
        }

        /// <summary>
        /// Is User is a DMS Admin?
        /// </summary>
        protected bool IsDMSAdmin
        {
            get
            {
                return ClientId == DMSAdminClientId;
            }
        }

        /// <summary>
        /// Check User has the permission
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        private bool HasPermission(string userName, string permissionCode, out APIEndpointPermission permission)
        {
            permission = null;

            if (IsPermissionOnlyForDMSAdmin(permissionCode))
            {
                if (!IsDMSAdmin)
                {
                    return false;
                }
            }

            bool hasPermission = false;
            var userRepository = new UserRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
            var permissions = userRepository.GetPermission(userName, new Guid(ClientId));
            var userPermission = permissions.FirstOrDefault(
                x => x.Permission.PermissionCode == permissionCode);
            hasPermission = userPermission != null ? !userPermission.IsDismantledPermission : false;

            if (hasPermission)
            {
                permission = userPermission.Permission;
            }

            return hasPermission;
        }

        /// <summary>
        /// Check if it is a restricted permission
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        private bool IsPermissionOnlyForDMSAdmin(string permissionName)
        {
            return Constants.RestrictedPermissions.Contains(permissionName);
        }

        /// <summary>
        /// Unauthorized response
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage UnAuthorizedResponse(HttpActionContext context)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden);

            response.RequestMessage = context.Request;

            var json = JsonConvert.SerializeObject(
                                        new ResponseMessage()
                                        {
                                            Success = false,
                                            Status = ResponseStatus.Warning,
                                            Message = MessageResource.ErrorMsgAuthPermission
                                        });

            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return response;
        }

    }
}