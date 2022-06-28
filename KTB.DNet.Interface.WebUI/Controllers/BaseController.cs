#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Base controller class
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
using KTB.DNet.Interface.WebUI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    [JwtAuthentication]
    [UserActivityLogging]
    public class BaseController : ApiController
    {
        protected int? _userId;
        protected string _username;
        protected List<string> _roles;
        protected int? _dealerId;
        protected string _dealerCode;
        protected string _clientId;
        protected int _dmsAdminUserId;
        protected string _dmsAdminClientId;

        public APIEndpointPermission CurrentEndpointPermission { get; set; }

        /// <summary>
        /// Return User Name from Login
        /// </summary>
        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(_username))
                {
                    var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        var userName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
                        if (userName != null)
                        {
                            _username = userName.Value;
                            return _username;
                        }
                    }

                    return "-";
                }

                return _username;
            }
        }

        /// <summary>
        /// List of User Roles
        /// </summary>
        protected List<string> Roles
        {
            get
            {
                if (!(_roles != null && _roles.Count() > 0))
                {
                    var identity = this.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        var roles = claims.FirstOrDefault(x => x.Type == "Roles");
                        if (roles != null)
                        {
                            _roles = roles.Value.ToString().Trim().ToLower().Split('-').ToList();
                        }
                    }
                }

                return _roles;
            }
        }

        /// <summary>
        /// User UserId
        /// </summary>
        protected int UserId
        {
            get
            {
                if (!_userId.HasValue)
                {
                    var identity = this.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        var userId = claims.FirstOrDefault(x => x.Type == "UserId");
                        if (userId != null)
                        {
                            _userId = Convert.ToInt32(userId.Value);
                            return _userId.Value;
                        }
                    }

                    return -1;
                }

                return _userId.Value;
            }
        }

        /// <summary>
        /// User ClientId
        /// </summary>
        protected string ClientId
        {
            get
            {
                if (string.IsNullOrEmpty(_clientId))
                {
                    var identity = this.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        var clientId = claims.FirstOrDefault(x => x.Type == "ClientId");
                        if (clientId != null)
                        {
                            _clientId = clientId.Value.ToLower().Trim();
                            return _clientId;
                        }
                    }

                    return null;
                }

                return _clientId;
            }
        }

        /// <summary>
        /// DealerCode
        /// </summary>
        public string DealerCode
        {
            get
            {
                if (string.IsNullOrEmpty(_dealerCode))
                {
                    var identity = this.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        var dealerCode = claims.FirstOrDefault(x => x.Type == "DealerCode");
                        if (dealerCode != null)
                        {
                            _dealerCode = dealerCode.Value.ToLower().Trim();
                            return _dealerCode;
                        }
                    }

                    return null;
                }

                return _dealerCode;
            }
        }

        /// <summary>
        /// User DealerId
        /// </summary>
        protected int DealerId
        {
            get
            {
                if (!_dealerId.HasValue)
                {
                    var identity = this.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        var dealerId = claims.FirstOrDefault(x => x.Type == "DealerId");
                        if (dealerId != null)
                        {
                            if (!string.IsNullOrEmpty(dealerId.Value))
                            {
                                _dealerId = Convert.ToInt32(dealerId.Value);
                                return _dealerId.Value;
                            }
                        }
                    }
                    return -1;
                }

                return _dealerId.Value;
            }
        }

        /// <summary>
        /// Id of DMS Admin User
        /// </summary>
        protected int DMSAdminUserId
        {
            get
            {
                if (_dmsAdminUserId == 0)
                {
                    _dmsAdminUserId = AppConfigs.GetInt("DMSAdminUserId");
                }

                return _dmsAdminUserId;
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
        /// change async method to sync method
        /// </summary>
        protected TaskFactory _taskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        /// <summary>
        /// Get Inner Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected Exception GetInnerException(Exception ex)
        {
            Exception innerEx = ex;
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }

            return innerEx;
        }

        public virtual void SetUserModifier(string username)
        {
            //do nothing
        }
    }
}