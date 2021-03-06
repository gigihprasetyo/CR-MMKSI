#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : JwtAuthenticationAttribute.cs class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad Ridwan
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Framework.Helper;
using KTB.DNet.Interface.Repository.Dapper;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
#endregion

namespace KTB.DNet.Interface.WebUI.Helper
{
    /// </summary>
    /// <summary>
    /// JWT Authenticate
    /// </summary>
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private IClientUserRepository<APIClientUser, int> _clientUserRepo;
        private JWTManager _jwtManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public JwtAuthenticationAttribute()
        {
            _clientUserRepo = new ClientUserRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
            _jwtManager = new JWTManager();
            _jwtManager.ValidateClaimsHandler += ValidateClaims;
        }

        #region Public Methods
        /// <summary>
        /// Get the allow multiple setting
        /// </summary>
        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Authenticate async
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {

            // 1. Look for credentials in the request.
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            // 2. If there are no credentials, do nothing.
            if (authorization == null)
            {
                context.ErrorResult = new AuthenticationFailureResult(MessageResource.ErrorMsgAuthOnRequestHeaderNotFound, request);
                return Task.FromResult<IPrincipal>(null);
            }

            // 3. If there are credentials but the filter does not recognize the authentication scheme, do nothing.
            if (authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult(MessageResource.ErrorMsgAuthRequestHeaderAuthorizationSchemeNotBearer, request);
                return Task.FromResult<IPrincipal>(null);
            }

            // 4. If there are credentials that the filter understands, try to validate them.
            // 5. If the credentials are bad, set the error result.
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult(MessageResource.ErrorMsgMissingJwtToken, request);
                return Task.FromResult<IPrincipal>(null);
            }

            try
            {
                string errorMessage = string.Empty;

                // validate the token
                Task<IPrincipal> authenticationResult = AuthenticateJwtToken(authorization.Parameter, out errorMessage);
                if (authenticationResult.Result == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult(errorMessage, request);

                    // log the failed authorization to elmah
                    LogErrorToElmah(errorMessage, context.Request.ToString(), authorization.Parameter);
                }
                else
                {
                    Thread.CurrentPrincipal = authenticationResult.Result;
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = Thread.CurrentPrincipal;
                        BaseController bc = (BaseController)context.ActionContext.ControllerContext.Controller;
                        bc.SetUserModifier(HttpContext.Current.User.Identity.Name);
                    }
                }

                return authenticationResult;
            }
            catch (Exception e)
            {
                context.ErrorResult = new AuthenticationFailureResult(e.Message, request);

                return Task.FromResult<IPrincipal>(null);
            }
        }

        /// <summary>
        /// Challenge Asynchronus
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Bearer");
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return Task.FromResult(0);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Authenticate token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private Task<IPrincipal> AuthenticateJwtToken(string token, out string errorMessage)
        {
            errorMessage = string.Empty;

            // validate principal
            var principal = _jwtManager.GetPrincipal(token, out errorMessage);
            if (principal == null)
            {
                return Task.FromResult<IPrincipal>(null);
            }

            // validate identity
            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            if (identity == null || !identity.IsAuthenticated)
            {
                errorMessage = MessageResource.ErrorMsgAuthCouldNotGetTheCredentialsFromToken;
                return Task.FromResult<IPrincipal>(null);
            }

            // get username
            Claim username = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            Claim userId = identity.Claims.FirstOrDefault(x => x.Type == "UserId");
            Claim clientId = identity.Claims.FirstOrDefault(x => x.Type == "ClientId");

            if (username == null || string.IsNullOrEmpty(username.Value))
            {
                errorMessage = MessageResource.ErrorMsgAuthCouldNotGetTheCredentialsFromToken;
                return Task.FromResult<IPrincipal>(null);
            }

            if (userId == null || string.IsNullOrEmpty(userId.Value))
            {
                errorMessage = MessageResource.ErrorMsgAuthCouldNotGetTheCredentialsFromToken;
                return Task.FromResult<IPrincipal>(null);
            }

            if (clientId == null || string.IsNullOrEmpty(clientId.Value))
            {
                errorMessage = MessageResource.ErrorMsgAuthCouldNotGetTheCredentialsFromToken;
                return Task.FromResult<IPrincipal>(null);
            }

            //validate user token
            APIClientUser dbUser = _clientUserRepo.GetByUserIdAndClientId(int.Parse(userId.Value), new Guid(clientId.Value));

            if (dbUser.Token.Trim() != token.Trim())
            {
                errorMessage = MessageResource.ErrorMsgAuthTokenIsNotValid;
                return Task.FromResult<IPrincipal>(null);
            }

            // set last activity
            dbUser.LastActivity = DateTime.UtcNow;
            _clientUserRepo.Update(dbUser);

            // setup user
            IPrincipal user = new ClaimsPrincipal(identity);

            // return it
            return Task.FromResult(user);
        }

        /// <summary>
        /// Handling JWTManager ValidateClaims
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="token"></param>
        /// <param name="secretKey"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool ValidateClaims(IEnumerable<Claim> claims, string token, out string secretKey, out string errorMessage)
        {
            secretKey = string.Empty;
            errorMessage = string.Empty;

            try
            {
                var userId = claims.FirstOrDefault(c => c.Type == "UserId");
                var clientId = claims.FirstOrDefault(c => c.Type == "ClientId");

                if (userId == null || clientId == null)
                {
                    errorMessage = MessageResource.ErrorMsgAuthCouldNotGetTheCredentialsFromToken;
                    return false;
                }

                APIClientUser user = _clientUserRepo.GetByUserIdAndClientId(int.Parse(userId.Value), new Guid(clientId.Value));
                secretKey = user.Client.SecretKey.ToString();

                if (_clientUserRepo.IsTokenExpired(user, DateTime.UtcNow))
                {
                    errorMessage = MessageResource.ErrorMsgAuthTokenHasAlreadyExpired;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// LogErrorToElmah
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="httpRequest"></param>
        /// <param name="authHeaderParam"></param>
        private void LogErrorToElmah(string errorMessage, string httpRequest, string authHeaderParam)
        {
            //LoggerService log = new LoggerService();
            ErrorLog.LogError(new Exception("[TOKEN]ERROR: " + errorMessage));
            ErrorLog.LogError(new Exception("[TOKEN]HTTPREQUESTMESSAGE: " + httpRequest));
            ErrorLog.LogError(new Exception("[TOKEN]Authorization.Parameter: " + authHeaderParam));
        }

        #endregion
    }
}