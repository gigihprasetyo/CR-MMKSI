#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Throttler class
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
using KTB.DNet.BusinessLogic;
using KTB.DNet.Interface.Dapper.Repository;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper.Throttle
{
    /// <summary>
    /// Implement throttling for web api request
    /// </summary>
    public class ThrottleHelper
    {
        private string _uri;
        private bool _configFromDB;
        private static ConcurrentDictionary<string, ThrottleInfo> _cache = new ConcurrentDictionary<string, ThrottleInfo>();
        private IThrottleRepository<APIThrottle, int> _repo;
        private ILoggerService _loggerService;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="uri"></param>
        /// <param name="configFromDB"></param>       
        public ThrottleHelper(string key, string uri, bool configFromDB = false)
        {
            ThrottleGroup = key;
            _uri = uri;
            _configFromDB = configFromDB;
            _repo = new ThrottleRepository();
            _loggerService = new LoggerService();
        }
        #endregion

        /// <summary>
        /// Request limitation
        /// </summary>
        public int RequestLimit { get; private set; }

        /// <summary>
        /// Request Remaining
        /// </summary>
        public int RequestRemaining { get; private set; }

        /// <summary>
        /// Request in seconds
        /// </summary>
        public int RequestInSeconds { get; private set; }

        /// <summary>
        /// Reset Date
        /// </summary>
        public DateTime WindowResetDate { get; private set; }

        /// <summary>
        /// Throttle group
        /// </summary>
        public string ThrottleGroup { get; set; }

        /// <summary>
        /// Enable Throttle to be executed
        /// </summary>
        public bool EnableThrottle { get; set; }

        #region Public Method
        /// <summary>
        /// Validate if request will be Throtle
        /// </summary>
        public ThrottleInfo RequestShouldBeThrottled
        {
            get
            {
                ThrottleInfo throttleInfo = GetThrottleInfoFromCache();
                WindowResetDate = throttleInfo.ExpiresAt;
                RequestRemaining = Math.Max(throttleInfo.RequestLimit - throttleInfo.RequestCount, 0);
                if (throttleInfo.RequestCount > throttleInfo.RequestLimit) throttleInfo.IsThrottle = true;

                return throttleInfo;
            }
        }

        /// <summary>
        /// Get Limit headers and store it on Dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetRateLimitHeaders()
        {
            ThrottleInfo throttleInfo = GetThrottleInfoFromCache();

            int requestsRemaining = Math.Max(throttleInfo.RequestLimit - throttleInfo.RequestCount, 0);

            var headers = new Dictionary<string, string>();
            headers.Add("X-RateLimit-Limit", throttleInfo.RequestLimit.ToString());
            headers.Add("X-RateLimit-Remaining", requestsRemaining.ToString());
            headers.Add("X-RateLimit-Reset", ToUnixTime(throttleInfo.ExpiresAt).ToString());

            return headers;
        }

        /// <summary>
        /// Increment request count
        /// </summary>
        public void IncrementRequestCount()
        {
            ThrottleInfo throttleInfo = GetThrottleInfoFromCache();
            throttleInfo.RequestCount++;
            _cache[ThrottleGroup] = throttleInfo;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Read throttle configuration from server
        /// </summary>
        /// <returns></returns>
        private XDocument ReadThrottleConfiguration()
        {
            XDocument xDocument = null;
            bool success = false;

            try
            {
                // set credentials to access repository server
                string user = AppConfigs.GetString("User");
                string password = AppConfigs.GetString("Password");
                string webServer = AppConfigs.GetString("WebServer");
                UserImpersonater imp = new UserImpersonater(user, password, webServer);

                success = imp.Start();
                if (success)
                {
                    var dir = Path.Combine(AppConfigs.GetString("WebServer"), Path.Combine("MDNet", Path.Combine("DNet", "Throttle")));
                    var location = string.Concat(@"\\", dir);
                    string filename = AppConfigs.GetString("ThrottleConfig");
                    var path = Path.Combine(location, filename);

                    xDocument = XDocument.Load(path);
                    imp.Stop();
                    imp.Dispose();
                }

                return xDocument;
            }
            catch
            {
                return xDocument;
            }
        }

        /// <summary>
        /// Get Throttle info From Cache
        /// </summary>
        /// <returns></returns>
        private ThrottleInfo GetThrottleInfoFromCache()
        {
            ThrottleInfo throttleInfo = null;

            if (_cache.ContainsKey(ThrottleGroup))
            {
                throttleInfo = _cache[ThrottleGroup];
            }

            if (throttleInfo == null || throttleInfo.ExpiresAt <= DateTime.Now)
            {
                // get configuration settings
                GetRequestLimitAndTimeout(_uri);

                throttleInfo = new ThrottleInfo
                {
                    ExpiresAt = DateTime.Now.AddSeconds(RequestInSeconds),
                    RequestCount = 0,
                    RequestLimit = RequestLimit,
                    RequestInSeconds = RequestInSeconds,
                    IsThrottle = false,
                    EnableThrottle = EnableThrottle
                };
            };

            return throttleInfo;
        }

        /// <summary>
        /// Get request limit and timeout based on URI
        /// </summary>
        /// <param name="uri"></param>
        private void GetRequestLimitAndTimeout(string uri)
        {
            // uri not null means take from database
            if (_configFromDB)
            {
                try
                {
                    //e.g.: allow request, 5 every 5 seconds
                    var throttle = _repo.GetByUri(uri);

                    if (throttle != null)
                    {
                        RequestLimit = throttle.RequestLimit;
                        RequestInSeconds = throttle.TimeInSeconds;
                        EnableThrottle = throttle.Enable;
                    }
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex);
                }
            }
            else
            {
                try
                {
                    XDocument xDocument = null;
                    xDocument = ReadThrottleConfiguration();

                    if (xDocument != null)
                    {
                        foreach (XElement xe in xDocument.Descendants("EndPoints"))
                        {
                            if (xe.Element("uri").Value == uri)
                            {
                                RequestLimit = int.Parse(xe.Element("limit").Value);
                                RequestInSeconds = int.Parse(xe.Element("time").Value);
                                EnableThrottle = bool.Parse(xe.Element("enable").Value);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _loggerService.Log(ex);
                }
            }
        }

        /// <summary>
        /// Set to unix time
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private long ToUnixTime(DateTime date)
        {
            var initialDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return Convert.ToInt64((date.ToUniversalTime() - initialDateTime).TotalSeconds);
        }
        #endregion

    }
}