#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ThrottleFilterAttribute class
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
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Framework.Helper;
using KTB.DNet.Interface.Framework.Models;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Dapper;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.WebApi.Helper.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

#endregion

namespace KTB.DNet.Interface.WebApi.Helper.Throttle
{
    /// <summary>
    /// THrottle filter attribute
    /// </summary>
    public class ThrottleFilterAttribute : ActionFilterAttribute
    {
        private IThrottleRepository<APIThrottle, int> _throttleRepo;
        private ILoggerService _loggerService;

        private ThrottleIdentifier _identifier;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="identifier"></param>        
        public ThrottleFilterAttribute(ThrottleIdentifier identifier)
        {
            _identifier = identifier;

            _throttleRepo = new ThrottleRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
            _loggerService = new LoggerService();
        }
        #endregion

        /// <summary>
        /// Set action while api hit httpactionresult
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                if (EnableThrottle)
                {
                    // get endpoint url
                    string endpoint = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Replace("~", "");

                    string throttleKey = string.Empty;

                    // get throttle info
                    ThrottleInfo throttleInfo = GetThrottleInfo(endpoint, out throttleKey);

                    if (throttleInfo != null)
                    {
                        throttleInfo.RequestCount++;

                        HttpRuntime.Cache.Add(
                            throttleKey,
                            throttleInfo,
                            null,
                            throttleInfo.ExpiresAt,
                            Cache.NoSlidingExpiration,
                            CacheItemPriority.Normal,
                            null
                            );

                        if (throttleInfo.Enable && throttleInfo.RequestCount > throttleInfo.RequestLimit)
                        {
                            actionContext.Response = GetThrottleErrorResponse(throttleInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                actionContext.Response = GetThrottleErrorResponse(MessageResource.ErrorMsgFailedExecuteThrottle + "," + ex.ToString());
            }

            base.OnActionExecuting(actionContext);
        }

        #region Get Throttle Identifier Value
        /// <summary>
        /// Get Throttle Identifier Value
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        private string GetThrottleIdentifierValue(ThrottleIdentifier identifier)
        {
            string identifierValue = string.Empty;
            switch (identifier)
            {
                case ThrottleIdentifier.IPAddress:
                    identifierValue = HttpContext.Current.Request.UserHostAddress;
                    break;
                case ThrottleIdentifier.Username:
                default:
                    identifierValue = HttpContext.Current.User.Identity.Name;
                    break;

            }

            return identifierValue;
        }
        #endregion

        #region Throttle Error Response

        #region Get Throttle Response Message
        /// <summary>
        /// Get Throttle Response Message
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        private List<MessageBase> GetThrottleResponseMessage(string strMessage)
        {
            List<MessageBase> messages = new List<MessageBase>();
            MessageBase message = new MessageBase();
            message.ErrorCode = ErrorCode.UnhandledException;
            message.ErrorMessage = strMessage;
            messages.Add(message);

            return messages;
        }
        #endregion

        #region Get Throttle Error Response
        /// <summary>
        /// Create response message
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage GetThrottleErrorResponse(ThrottleInfo throttleInfo)
        {
            string errorMessage = string.Format(
                                            MessageResource.ErrorMsgTooManyRequest,
                                            throttleInfo.RequestLimit,
                                            throttleInfo.TimeInSeconds
                                            );

            HttpResponseMessage response = GetThrottleErrorResponse(errorMessage);

            response.Headers.Add("X-RateLimit-Limit", throttleInfo.RequestLimit.ToString());
            response.Headers.Add("X-RateLimit-Remaining", (throttleInfo.RequestLimit - throttleInfo.RequestCount).ToString());
            response.Headers.Add("X-RateLimit-Reset", GetRateLimitReset(throttleInfo.ExpiresAt).ToString());

            return response;
        }
        #endregion

        #region Get Throller Error Response
        /// <summary>
        /// Compose response message for exception
        /// </summary>
        /// <param name="strException"></param>
        /// <returns></returns>
        private HttpResponseMessage GetThrottleErrorResponse(string errorMessage)
        {
            HttpResponseMessage response = new HttpResponseMessage((HttpStatusCode)429);

            List<MessageBase> messages = new List<MessageBase>();
            messages = GetThrottleResponseMessage(errorMessage);

            var errorResult = HttpCodeMessage.BuildErrorResult(messages);
            HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(messages);

            var json = JsonConvert.SerializeObject(errorResult);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            LogError(messages);

            return response;
        }
        #endregion

        #endregion

        #region Get Throttle info
        /// <summary>
        /// Get Throttle Info
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private ThrottleInfo GetThrottleInfo(string uri, out string throttleKey)
        {
            string id = GetThrottleIdentifierValue(_identifier);

            throttleKey = string.Format("{0}-{1}", id, uri);

            // Get Throttle info from cache
            // If there's no throttle info from cache then get it from db or xml
            ThrottleInfo throttleInfo = (ThrottleInfo)HttpRuntime.Cache[throttleKey];

            if (throttleInfo == null)
            {
                if (DoRetrieveThrottleConfigFromDb)
                {
                    #region Get Throttle info from db
                    try
                    {
                        // Get throttle info from db
                        var throttleConfig = _throttleRepo.GetByUri(uri);

                        if (throttleConfig != null)
                        {
                            throttleInfo = new ThrottleInfo()
                            {
                                Enable = throttleConfig.Enable,
                                RequestLimit = throttleConfig.RequestLimit,
                                RequestCount = 0,
                                TimeInSeconds = throttleConfig.TimeInSeconds
                            };

                            throttleInfo.ExpiresAt = DateTime.Now.AddSeconds(throttleInfo.TimeInSeconds);
                            return throttleInfo;
                        }
                    }
                    catch (Exception ex)
                    {
                        _loggerService.Log(ex);
                    }
                    #endregion
                }
                else
                {
                    #region Get Throttle info from xml
                    IThrottleInfo _throttleInfo = ThrottleHelper.GetThrottleInfo<ThrottleInfo>(uri);
                    if (_throttleInfo != null)
                    {
                        throttleInfo = (ThrottleInfo)_throttleInfo;
                        throttleInfo.ExpiresAt = DateTime.Now.AddSeconds(throttleInfo.TimeInSeconds);
                    }
                    #endregion
                }

            }

            return throttleInfo;
        }
        #endregion

        #region Log Error
        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="messages"></param>
        protected void LogError(List<MessageBase> messages)
        {
            Exception ex = new Exception(JsonConvert.SerializeObject(messages));
            var service = new LoggerService();
            if (service != null)
            {
                service.Log(ex);
            }
        }
        #endregion


        /// <summary>
        /// Do Retrieve Throttle Info from DB
        /// </summary>
        private bool DoRetrieveThrottleConfigFromDb
        {
            get
            {
                return AppConfigs.GetBoolean("RetrieveThrottleFromDB");
            }
        }

        /// <summary>
        /// Do Retrieve Throttle Info from DB
        /// </summary>
        private bool EnableThrottle
        {
            get
            {
                return AppConfigs.GetBoolean("EnableThrottle");
            }
        }

        #region Get Rate Limit Reset
        /// <summary>
        /// Set to unix time
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private long GetRateLimitReset(DateTime date)
        {
            var initialDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return Convert.ToInt64((date.ToUniversalTime() - initialDateTime).TotalSeconds);
        }
        #endregion
    }
}