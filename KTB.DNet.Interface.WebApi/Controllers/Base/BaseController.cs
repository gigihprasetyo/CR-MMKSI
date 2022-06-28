#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Base Controller
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
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Dapper;
using KTB.DNet.Interface.Repository.Dapper.DNet;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Helper.Messages;
using KTB.DNet.Interface.WebApi.Models;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using KTB.DNet.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Runtime.Caching;
using System.Security.Claims;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// Base Controller 
    /// </summary>
    [JwtAuthentication]
    public class BaseController : ApiController
    {
        #region Attribute
        private ILoggerService _loggerService;
        private string _dataOnRequestBody;
        private object _originalRequestData;
        private bool _isOrginalRequestDataJson;
        private Type _requestDataType;
        private string _currentToken;
        private string _senderIP;
        private Guid _appId;
        protected Guid _clientId;
        private long? _currentLogId;
        private APIEndpointPermission _endpoint;
        private IEndpointPermissionRepository<APIEndpointPermission, int> _endpointRepo;
        #endregion

        #region Constructor
        public BaseController()
        {
            DatabaseSettings settings = (DatabaseSettings)ConfigurationManager.GetConfiguration("dataConfiguration");
            ConnectionStringData connString = settings.ConnectionStrings[Constants.ConnectionStringName.DNetConnection];
            string connectionString = AppConfigs.ConnectionString(
                                        connString.Parameters["server"].Value,
                                        connString.Parameters["database"].Value,
                                        connString.Parameters["uid"].Value,
                                        connString.Parameters["password"].Value
                                        );
            _repoDealerCompany = new DealerCompanyRepository(connectionString);
            _repoDealerCompanyToDealer = new DealerCompanyToDealerRepository(connectionString);

            _endpointRepo = new EndpointPermissionRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
            _applicationConfigRepo = new ApplicationConfigRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.LogConnection));
            _repoUser = new UserRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
            _repoDealer = new DealerRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection));
            _loggerService = new LoggerService();
        }
        #endregion

        #region Property
        MemoryCache memCache = MemoryCache.Default;
        private static readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        private IDealerRepository<KTB.DNet.Interface.Domain.Dealer, int> _repoDealer;
        private IApplicationConfigRepository<ApplicationConfig, long> _applicationConfigRepo;
        private IUserRepository<APIUser,int> _repoUser;
        private IDealerCompanyRepository<DealerCompany,int> _repoDealerCompany;
        private IDealerCompanyToDealerRepository<DealerCompanyToDealer, int> _repoDealerCompanyToDealer;
        /// <summary>
        /// IsLoggingOnlyForFailedTransaction
        /// </summary>
        protected bool IsLoggingOnlyForFailedTransaction
        {
            get { return GetLoggingConfig(Constants.ConfigKey.WebAPI_LoggingOnlyForFailedTransaction_Enable, false); }
        }

        /// <summary>
        /// Endpoint
        /// </summary>
        protected APIEndpointPermission Endpoint
        {
            get
            {
                if (_endpoint == null)
                {
                    try
                    {
                        string uri = Request.RequestUri.AbsolutePath.Replace(this.RequestContext.VirtualPathRoot, string.Empty);
                        _endpoint = _endpointRepo.GetByUri(uri);
                    }
                    catch (Exception ex)
                    {
                        // do nothing
                    }
                }

                return _endpoint;
            }
        }

        /// <summary>
        /// Is Endpoint Logging Enabled
        /// </summary>
        protected bool IsEndpointLoggingEnabled
        {
            get
            {
                if (Endpoint != null)
                {
                    return Endpoint.IsLogEnabled;
                }
                return true;
            }
        }

        /// <summary>
        /// Is Endpoint Logging Enabled
        /// </summary>
        protected bool IsEndpointRuntimeLoggingEnabled
        {
            get
            {
                if (Endpoint != null)
                {
                    return Endpoint.IsRuntimeLogEnabled;
                }
                return true;
            }
        }

        /// <summary>
        /// AppId 
        /// </summary>
        protected Guid AppId
        {
            get
            {
                if (_appId == Guid.Empty)
                {
                    _appId = new Guid(AppConfigs.GetString("AppId"));
                }

                return _appId;
            }
        }

        /// <summary>
        /// Return Dealer Code from Login 
        /// </summary>
        public string DealerCode
        {
            get
            {
                var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var dealerCode = claims.FirstOrDefault(x => x.Type == "DealerCode");
                    if (dealerCode != null)
                        return dealerCode.Value;
                }

                return null;
            }
        }

        /// <summary>
        /// Return Dealer CompanyName from Login 
        /// </summary>
        public string DealerCompanyCode
        {
            get
            {
                string dealerCompanyCode = string.Empty;
                if (DealerCode.ToUpper() == "MKS")
                {
                    var apiUser = _repoUser.GetByName(this.UserName);
                    if (apiUser != null)
                    {
                        var dealerCompany = _repoDealerCompany.Get((int)apiUser.DealerCompanyId);
                        dealerCompanyCode = dealerCompany.DealerCompanyCode;
                    }
                }

                return dealerCompanyCode;
            }
        }

        /// <summary>
        /// Return List Dealer Code from Login 
        /// </summary>
        public List<Domain.Dealer> ListDealer
        {
            get
            {
                if (this.DealerCode.ToUpper() == "MKS")
                {
                    var list = new List<KTB.DNet.Interface.Domain.Dealer>();                    
                    var apiUser = _repoUser.GetByName(this.UserName);
                    if(apiUser != null)
                    {
                        if (apiUser.GroupDealerId != null && apiUser.DealerCompanyId != null)
                        {
                            var dealerCompany = _repoDealerCompany.Get((int)apiUser.DealerCompanyId);
                            list = _repoDealerCompanyToDealer.GetAllDealer(dealerCompany.ID);
                        }
                        else if (apiUser.GroupDealerId != null)
                        {
                            list = _repoDealer.GetAllByGroupId((int)apiUser.GroupDealerId);                           
                        }
                    }
                    
                    return list;
                }

                return null;
            }
        }

        /// <summary>
        /// DataOnRequestBody
        /// </summary>
        public string DataOnRequestBody
        {
            get
            {
                if (string.IsNullOrEmpty(_dataOnRequestBody))
                {
                    // read data on request body
                    try
                    {
                        using (var stream = new MemoryStream())
                        {
                            var context = (HttpContextBase)this.ActionContext.Request.Properties["MS_HttpContext"];
                            context.Request.InputStream.Seek(0, SeekOrigin.Begin);
                            context.Request.InputStream.CopyTo(stream);
                            _dataOnRequestBody = Encoding.UTF8.GetString(stream.ToArray());
                        }
                    }
                    catch (Exception)
                    {
                        _dataOnRequestBody = string.Empty;
                    }
                }

                return _dataOnRequestBody;
            }
        }
        /// <summary>
        /// User ClientId
        /// </summary>
        protected Guid ClientId
        {
            get
            {
                if (Guid.Empty == _clientId)
                {
                    var identity = this.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        var clientId = claims.FirstOrDefault(x => x.Type == "ClientId");
                        if (clientId != null)
                        {
                            _clientId = new Guid(clientId.Value);
                            return _clientId;
                        }
                    }
                }

                return _clientId;
            }
        }

        /// <summary>
        /// Get page size configuration
        /// </summary>
        protected int PageSize
        {
            get
            {
                return Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);
            }
        }

        /// <summary>
        /// Get controller action name
        /// </summary>
        protected string ControllerActionName
        {
            get
            {
                // get the controller name
                string controllerName = this.GetType().Name.Replace("Controller", "");
                string controllerActionName = (this as ApiController).ActionContext.ActionDescriptor.ActionName + controllerName;
                return controllerActionName;
            }
        }

        /// <summary>
        /// Get Method Name 
        /// </summary>
        protected string MethodName
        {
            get
            {
                return MethodBase.GetCurrentMethod().Name;
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

        /// <summary>
        /// IsReadURL
        /// </summary>
        protected bool IsReadURL
        {
            get
            {
                bool isReadUrl = false;
                // check if it is read or not

                if (Endpoint != null)
                {
                    isReadUrl = Endpoint.OperationType == OperationType.Read;
                    try
                    {
                        if (Request.RequestUri.Segments[2].ToLower().Contains("dmsdata") || Request.RequestUri.Segments[2].ToLower().Contains("accountingreport"))
                        {
                            isReadUrl = true;
                        }
                        else
                        {
                            isReadUrl = false;
                        }
                    }
                    catch
                    {
                        // do nothing
                    }
                }
                else
                {
                    try
                    {
                        //if (Request.RequestUri.Segments.Last().Equals("Read") ||
                        //    Request.RequestUri.Segments.Last().Equals("GetPRHistorySO") ||
                        //    Request.RequestUri.Segments.Last().Equals("PRHistoryIndentStatusCancel"))
                        if (Request.RequestUri.Segments[2].ToLower().Contains("dmsdata") || Request.RequestUri.Segments[2].ToLower().Contains("accountingreport"))
                        {
                            isReadUrl = true;
                        }
                    }
                    catch
                    {
                        //do nothing
                    }
                }

                return isReadUrl;
            }
        }
        #endregion

        #region Called by Action Filter Such as Json Validation/ Permission / JWTAuth
        /// <summary>
        /// SetOriginalRequestData
        /// </summary>
        /// <param name="originalRequestData"></param>
        /// <param name="requestDataType"></param>
        [NonAction]
        public void SetOriginalRequestData(object originalRequestData, Type requestDataType)
        {
            JToken jsonData = null;
            _isOrginalRequestDataJson = IsJsonData(originalRequestData, out jsonData);
            _originalRequestData = _isOrginalRequestDataJson ? jsonData : originalRequestData;
            _requestDataType = requestDataType;
        }

        /// <summary>
        /// Set current token
        /// </summary>
        /// <param name="token"></param>
        [NonAction]
        public void SetCurrentToken(string token)
        {
            _currentToken = token;
        }

        /// <summary>
        /// Set Current Endpoint
        /// </summary>
        /// <param name="endpoint"></param>
        [NonAction]
        public void SetCurrentEndpoint(APIEndpointPermission endpoint)
        {
            _endpoint = endpoint;
        }

        /// <summary>
        /// Log Transaction from Action Filter
        /// </summary>
        /// <param name="responseData"></param>
        /// <param name="status"></param>
        /// <param name="isJsonObject"></param>
        /// <returns></returns>
        [NonAction]
        public long LogActionFilterError(object responseData, bool status)
        {
            return LogActionFilterError(_originalRequestData, _requestDataType, responseData);
        }

        /// <summary>
        /// Log Transaction from Action Filter
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="requestDataType"></param>
        /// <param name="responseData"></param>
        /// <param name="status"></param>
        /// <param name="isJsonObject"></param>
        /// <returns></returns>
        [NonAction]
        public long LogActionFilterError(object requestData, Type requestDataType, object responseData)
        {
            SetOriginalRequestData(requestData, requestDataType);

            try
            {
                return LogTransaction(responseData, false);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return -1;
            }
        }

        #region LogError (Exception)

        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="error">Exception Error</param>
        [NonAction]
        public void LogError(Exception error)
        {
            LogError(error, ErrorCode.UnhandledException);
        }

        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="error"></param>
        /// <param name="errorCode"></param>
        [NonAction]
        public void LogError(Exception error, ErrorCode errorCode)
        {
            try
            {
                List<MessageBase> errorList = new List<MessageBase>();
                errorList.Add(new MessageBase(errorCode, error.Message));
                ResponseBase<object> responseMessage = PopulateValidationError<object>(errorList, null);

                LogActionFilterError(_originalRequestData, _requestDataType, responseMessage);

            }
            catch (Exception ex)
            {
                // do nothing
            }

            try
            {
                _loggerService.Log(error);
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }
        #endregion

        #endregion

        #region Protected Methods
        /// <summary>
        /// InvalidModelState
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        protected IHttpActionResult InvalidModelState(JsonMediaTypeFormatter json)
        {
            List<MessageBase> messages = HttpCodeMessage.GetErrorMessage(ModelState);

            var result = HttpCodeMessage.BuildErrorResult(messages);

            HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(messages);

            try
            {
                if (IsEndpointLoggingEnabled)
                {
                    LogTransaction(result, false);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }


            return Content(httpStatusCode, result, json);
        }

        /// <summary>
        /// InvalidDealerCode
        /// </summary>
        /// <param name="json"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        protected IHttpActionResult InvalidDealerCode(JsonMediaTypeFormatter json, string dealerCode)
        {
            List<MessageBase> messages = new List<MessageBase>();
            messages.Add(new MessageBase(ErrorCode.DataReferenceNotMatch, FieldResource.DealerCode + " " + string.Format(MessageResource.ErrorMsgDealerCode, dealerCode)));

            var result = HttpCodeMessage.BuildErrorResult(messages);
            HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(messages);

            try
            {
                if (IsEndpointLoggingEnabled)
                {
                    LogTransaction(result, false);
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }

            return Content(httpStatusCode, result, json);
        }

        /// <summary>
        /// Get http code message
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        protected HttpStatusCode GetHttpCodeMsg(List<MessageBase> messages)
        {
            return HttpCodeMessage.GetHttpStatusCode(messages);
        }

        /// <summary>
        /// Get unhandle message
        /// </summary>
        /// <param name="message"></param>
        protected MessageBase GetUnhandledExceptionMsg(string message)
        {
            return new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = String.Format(MessageResource.ErrorMsgPRGUnhandle, message) };
        }

        /// <summary>
        /// Get valid username
        /// </summary>
        /// <param name="resendBy"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        protected string GetUsername(string resendBy, long? logId)
        {
            _currentLogId = logId;
            return string.IsNullOrEmpty(resendBy) ? GetUpdatedBy() : resendBy;
        }
        #endregion

        #region Logging

        #region LogTransaction
        /// <summary>
        /// LogTransaction
        /// </summary>
        /// <param name="responseData"></param>
        /// <param name="success"></param>
        /// <returns></returns>
        protected long LogTransaction(object responseData, bool success)
        {
            long? parentId;

            string updatedBy = GetUpdatedBy();

            bool isResend = IsResendTransaction(ref updatedBy, out parentId);

            var logModel = ConstructDataLogData(responseData, success, isResend, parentId, updatedBy);

            _loggerService.SetUserModifier(updatedBy ?? UserName);

            return _loggerService.LogTransaction(logModel, DealerCode);
        }
        #endregion

        #region LogError (List<MessageBase>)
        /// <summary>
        /// Log Error
        /// </summary>
        protected void LogError(List<MessageBase> messages)
        {
            try
            {
                ResponseBase<object> responseMessage = PopulateValidationError<object>(messages, null);

                LogActionFilterError(_originalRequestData, _requestDataType, responseMessage);

            }
            catch (Exception ex)
            {
                // do nothing
            }

            try
            {
                Exception ex = new Exception(JsonConvert.SerializeObject(messages));
                _loggerService.Log(ex);
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }
        #endregion

        #region LogTransactionRuntime
        /// <summary>
        /// LogTransactionRuntime
        /// </summary>
        /// <param name="transactionRuntime"></param>
        /// <param name="responseData"></param>
        /// <param name="status"></param>
        /// <param name="updatedBy"></param>
        protected void LogTransactionRuntime(TransactionRuntime transactionRuntime, object responseData, bool status, string updatedBy = null)
        {
            try
            {
                if ((IsEndpointLoggingEnabled && (!status || !IsLoggingOnlyForFailedTransaction)) || IsEndpointRuntimeLoggingEnabled)
                {
                    long logId = LogTransaction(responseData, status);

                    if (IsEndpointRuntimeLoggingEnabled)
                    {
                        transactionRuntime.TransactionLogId = logId;

                        if (transactionRuntime.TransactionLogId != -1)
                        {
                            transactionRuntime.Id = _loggerService.LogTransactionRuntime(transactionRuntime);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        #endregion

        #endregion

        #region Private Methods
        /// <summary>
        /// Check if it is a resend transaction
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="updateBy"></param>
        /// <param name="parentId"></param>
        /// <param name="isJsonObject"></param>
        /// <returns></returns>
        private bool IsResendTransaction(ref string updateBy, out long? parentId)
        {
            try
            {
                bool isResend = false;
                parentId = 0;
                if (_isOrginalRequestDataJson)
                {
                    JToken jToken = (JToken)_originalRequestData;
                    if (jToken is JObject)
                    {
                        var param = (JObject)jToken;
                        isResend = param["ResendBy"] == null ? false : true;
                        if (isResend)
                        {
                            updateBy = param["ResendBy"].ToString();
                            parentId = (long)param["LogId"];
                        }
                    }
                }

                return isResend;
            }
            catch (Exception ex)
            {
                parentId = 0;
                return false;
            }
        }

        /// <summary>
        /// Construct data log
        /// </summary>
        /// <param name="responseData"></param>
        /// <param name="success"></param>
        /// <param name="isResend"></param>
        /// <param name="parentId"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        private DataLogModel ConstructDataLogData(object responseData, bool success, bool isResend = false, long? parentId = null, string updatedBy = null)
        {

            if (_isOrginalRequestDataJson)
            {
                if (AppConfigs.GetBoolean(Constants.AppConfig.RemoveImageFromInputDataLog))
                {
                    JsonHelper.SetProperty((JToken)_originalRequestData, Constants.TransactionLogConfig.ReplacedImagePropertyName, Constants.TransactionLogConfig.ReplacedImagePropertyValue);
                }
            }

            object output = responseData;

            if (IsReadURL)
            {
                JToken jsonOutput = null;

                if (IsJsonData(responseData, out jsonOutput))
                {
                    output = jsonOutput;
                    if (AppConfigs.GetBoolean(Constants.AppConfig.RemoveOutputFromOutputDataLog))
                    {
                        JsonHelper.SetProperty(jsonOutput, Constants.TransactionLogConfig.ReplacedOutputPropertyName, Constants.TransactionLogConfig.ReplacedOutputPropertyValue);
                    }
                }
            }


            DataLogModel transactionLog = new DataLogModel()
            {
                AppId = AppId,
                ClientId = ClientId,
                Username = UserName,
                DealerCode = DealerCode,
                Token = _currentToken,
                SenderIP = SenderIP,
                ResponseData = output,
                RequestData = _originalRequestData,
                Succeed = success,
                IsResend = isResend
            };

            if (isResend)
            {
                transactionLog.ParentId = parentId;
                transactionLog.UpdatedBy = updatedBy;
            }
            return transactionLog;
        }

        /// <summary>
        /// Get sender IP
        /// </summary>
        private string SenderIP
        {
            get
            {
                if (string.IsNullOrEmpty(_senderIP))
                {
                    if (HttpContext.Current != null)
                    {
                        _senderIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    }

                    if (string.IsNullOrEmpty(_senderIP))
                    {
                        if (Request.Properties.ContainsKey("MS_HttpContext"))
                        {
                            _senderIP = ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                        }
                        else if (Request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                        {
                            RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)Request.Properties[RemoteEndpointMessageProperty.Name];
                            _senderIP = prop.Address;
                        }
                        else if (HttpContext.Current != null)
                        {
                            _senderIP = HttpContext.Current.Request.UserHostAddress;
                        }
                        else
                        {
                            _senderIP = string.Empty;
                        }
                    }
                }
                return _senderIP;
            }
        }

        /// <summary>
        /// GetUpdatedBy
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        private string GetUpdatedBy()
        {
            if (_isOrginalRequestDataJson)
            {
                return GetUpdatedByFromJson((JToken)_originalRequestData);
            }

            return UserName;
        }

        /// <summary>
        /// Check if the request data is json data
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsJsonData(object requestData, out JToken data)
        {
            try
            {
                data = JToken.FromObject(requestData);
                return true;
            }
            catch (Exception ex)
            {
                data = null;
                return false;
            }
        }

        /// <summary>
        /// GetUpdatedByFromJson
        /// </summary>
        /// <param name="jToken"></param>
        /// <returns></returns>
        private string GetUpdatedByFromJson(JToken jToken)
        {
            try
            {
                if (jToken is JArray)
                {
                    JArray data = (JArray)jToken;
                    return
                        data[0] == null ? UserName : (
                                                        data[0]["UpdatedBy"] == null ?
                                                        UserName : data["UpdatedBy"].ToString()
                                                       );

                }
                else if (jToken is JObject)
                {
                    JObject data = (JObject)jToken;
                    return data["UpdatedBy"] == null ? UserName : data["UpdatedBy"].ToString();
                }
                else
                {
                    return UserName;
                }
            }
            catch (Exception ex)
            {
                return UserName;
            }
        }

        /// <summary>
        /// GetLoggingConfig
        /// </summary>
        /// <param name="configKey"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private bool GetLoggingConfig(string configKey, bool defaultValue)
        {
            // dafault value, enable logging if there's no configuration
            bool result = defaultValue;

            // read lock
            cacheLock.EnterReadLock();

            try
            {
                // get from memory cache if any
                CacheItem cacheItem = memCache.GetCacheItem(configKey);
                if (cacheItem != null)
                {
                    // convert to bool
                    bool.TryParse(cacheItem.Value.ToString(), out result);

                    return result;
                }
            }
            finally
            {
                cacheLock.ExitReadLock();
            }

            // update lock
            cacheLock.EnterWriteLock();

            try
            {
                // get from database
                var config = _applicationConfigRepo.GetByKey(configKey);

                // get status value
                bool isConfigActive = config == null ? false : config.IsActive;

                if (isConfigActive)
                {
                    result = config.Value.ToBool();
                }

                // define cache policy
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.UtcNow.AddHours(1);

                // add to memory cache
                memCache.Add(configKey, result, cacheItemPolicy);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }

            return result;
        }

        /// <summary>
        /// Populate validation message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errorList"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private ResponseBase<T> PopulateValidationError<T>(List<MessageBase> errorList, T result)
        {
            // init response
            ResponseBase<T> response = new ResponseBase<T>();

            // set response as error response
            response.success = false;
            response._id = -1;
            response.total = 0;
            response.lst = result;

            // populate the list
            foreach (MessageBase item in errorList)
            {
                response.messages.Add(new MessageBase(item.ErrorCode, item.ErrorMessage));
            }

            return response;
        }

        #endregion
    }
}