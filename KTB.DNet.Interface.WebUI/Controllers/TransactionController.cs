#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Transaction controller class
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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class TransactionController : BaseController
    {
        #region Initialize
        private ITransactionLogRepository<TransactionLog, long> _transactionLogRepo;
        private IDealerRepository<Dealer, int> _dealerRepo;
        private IClientUserRepository<APIClientUser, int> _clientUserRepo;
        private IClientRepository<APIClient, Guid> _clientRepo;
        private IUserRepository<APIUser, int> _userRepo;
        private IApplicationConfigRepository<ApplicationConfig, long> _appConfigRepo;
        private IMsApplicationRepository<MsApplication, Guid> _appRepo;
        private int ErrorMessageMaxLength;
        private int PageSize;
        private bool ShowReadLogInFailed;
        #endregion

        #region Constructor
        public TransactionController(
            ITransactionLogRepository<TransactionLog, long> transactionLogRepo,
            IDealerRepository<Dealer, int> dealerRepo,
            IClientUserRepository<APIClientUser, int> clientUserRepo,
            IClientRepository<APIClient, Guid> clientRepo,
            IUserRepository<APIUser, int> userRepo,
            IApplicationConfigRepository<ApplicationConfig, long> appConfigRepo,
            IMsApplicationRepository<MsApplication, Guid> appRepo)
        {
            _transactionLogRepo = transactionLogRepo;
            _dealerRepo = dealerRepo;
            _clientUserRepo = clientUserRepo;
            _clientRepo = clientRepo;
            _userRepo = userRepo;
            _appConfigRepo = appConfigRepo;
            _appRepo = appRepo;
            _transactionLogRepo.SetUserLogin(this.UserName);
        }
        #endregion

        #region Method Get Transaction
        ///// <summary>
        ///// Get transaction log detail
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_TransactionDetail_Read)]
        public IHttpActionResult Get(int id)
        {
            try
            {
                TransactionLog transaction = new TransactionLog();
                List<TransactionLog> resendTrans = new List<TransactionLog>();
                if (id > 0)
                {
                    transaction = _transactionLogRepo.Get(id);

                    resendTrans = _transactionLogRepo.GetResendTransaction(id);
                }

                var model = GetTransactionList(transaction, resendTrans);

                return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = model });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
            }
        }
        #endregion

        #region Method Resend (Get)
        ///// <summary>
        ///// Get failed transaction Log
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_FailedTransactionLog_Resend)]
        public IHttpActionResult SearchResendList(int id)
        {
            try
            {
                TransactionLog transaction = new TransactionLog();

                if (id > 0)
                {
                    transaction = _transactionLogRepo.Get(id);
                }

                // TODO: Replace with Regex instead
                try
                {
                    int firstIndex = transaction.Input.IndexOf("\"UpdatedBy\":");
                    string sub1 = transaction.Input.Substring(firstIndex + "\"UpdatedBy\":".Length);
                    int secondIndex = sub1.IndexOf("\"");
                    int thirdIndex = sub1.Substring(secondIndex + 1).IndexOf("\"");
                    int length = thirdIndex - secondIndex;
                    string username = sub1.Substring(secondIndex + 1, length);
                    if (!transaction.Input.Contains("[resender]"))
                        transaction.Input = transaction.Input.Replace(username, id + "[resender]" + User.Identity.Name + "|" + username);
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                // format the input
                string formattedInput = transaction.Input.Replace(",", "," + Environment.NewLine).Replace("{", "{" + Environment.NewLine).Replace("}", Environment.NewLine + "}");

                var model = new TransactionLogViewModel()
                {
                    Id = transaction.Id,
                    Input = formattedInput,
                    Output = transaction.Output,
                    Endpoint = transaction.Endpoint,
                    Status = transaction.Status,
                    StatusStr = transaction.Status ? "Success" : "Failed",
                    CreatedBy = transaction.CreatedBy,
                    CreatedTime = transaction.CreatedTime,
                    UpdatedBy = transaction.UpdatedBy,
                    UpdatedTime = transaction.UpdatedTime
                };

                return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = model });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
            }
        }
        #endregion

        #region Method Resend (Post)
        /// <summary>
        /// Resend Failed Transaction Log
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_FailedTransactionLog_Resend)]
        public IHttpActionResult Resend(TransactionLogViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // get user
                    APIUser user = _userRepo.GetByName(model.Username);
                    if (user != null)
                    {
                        // Get Client User
                        APIClientUser clientUser = _clientUserRepo.GetByUserIdAndClientId(user.Id, model.ClientId);
                        if (clientUser != null)
                        {
                            if (_clientUserRepo.IsTokenExpired(clientUser, DateTime.UtcNow))
                            {
                                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Token for the original user has been expired.", Data = model });
                            }

                            //todo: add resendby and logid to model input
                            //todo: add modal for error/input
                            JObject initialInput = JObject.Parse(model.Input);
                            if (initialInput["ResendBy"] == null)
                            {
                                initialInput.Add("ResendBy", this.UserName);
                            }
                            else
                            {
                                initialInput["ResendBy"] = this.UserName;
                            }

                            if (initialInput["LogId"] == null)
                            {
                                initialInput.Add("LogId", model.ParentId == null ? model.Id : model.ParentId);
                            }
                            else
                            {
                                initialInput["LogId"] = model.ParentId == null ? model.Id : model.ParentId;
                            }

                            Dictionary<string, string> headers = new Dictionary<string, string>();
                            headers.Add("Authorization", "Bearer " + clientUser.Token);

                            ResponseMessage<JToken> response =
                                HttpRequestHelper.RequestJson(
                                    model.Endpoint, initialInput, HttpRequestHelper.RequestMethod.POST, headers
                                    );

                            if (response.Success)
                            {
                                // get response status
                                JValue status = (JValue)((JObject)response.Data).GetValue("success", StringComparison.OrdinalIgnoreCase);

                                if (!((bool)status.Value))
                                {
                                    response.Success = false;
                                    response.Status = ResponseStatus.Warning;
                                }

                                response.Message = response.Data.ToString();
                            }

                            return Json(response);
                        }

                        return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "User ID " + user.Id + " and ClientID " + model.ClientId + " not found", Data = model });
                    }

                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "User is not valid" });
                }

                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Model is not valid" });
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                model.Output = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message; ;
                model.Status = false;
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
            }
        }
        #endregion

        #region Method Search
        ///// <summary>
        ///// Load transaction log
        ///// </summary>
        ///// <param name="postModel"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_TransactionLog_Read)]
        public IHttpActionResult Search(TransactionDataTablePostModel postModel)
        {
            try
            {
                SetConfiguration(EnumPageType.AuditLog_Transaction);

                int filteredResultsCount;
                int totalResultsCount;

                string dealerCode = IsDMSAdmin ? string.Empty : this.DealerCode;

                postModel.searchParams["beginDate"] = ((DateTime)postModel.searchParams["beginDate"]).ToLocalTime();
                postModel.searchParams["endDate"] = ((DateTime)postModel.searchParams["endDate"]).ToLocalTime();
                if (dealerCode.ToLower() == "ktb" || dealerCode.ToLower() == "mmksi")
                {
                    APIUser user = _userRepo.Get(this.UserId);
                    postModel.searchParams["UserName"] = user.UserName;
                }
                else
                {
                    postModel.dealerCode = dealerCode;
                }
                
                List<TransactionLog> listOfTransactionLog = _transactionLogRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                List<TransactionLogViewModel> result = listOfTransactionLog.ConvertList<TransactionLog, TransactionLogViewModel>();

                return Json(new
                {
                    // this is what datatables wants sending back
                    Records = result,
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    // this is what datatables wants sending back
                    Records = new List<TransactionLog>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Method Search Failed Transaction
        ///// <summary>
        ///// Search failed transaction log
        ///// </summary>
        ///// <param name="postModel"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_FailedTransactionLog_Read)]
        public IHttpActionResult SearchError(TransactionDataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                SetConfiguration(EnumPageType.AuditLog_Failed_Transaction);

                string dealerCode = IsDMSAdmin ? string.Empty : this.DealerCode;

                postModel.searchParams["beginDate"] = ((DateTime)postModel.searchParams["beginDate"]).ToLocalTime();
                postModel.searchParams["endDate"] = ((DateTime)postModel.searchParams["endDate"]).ToLocalTime();
                if (dealerCode.ToLower() == "ktb" || dealerCode.ToLower() == "mmksi")
                {
                    APIUser user = _userRepo.Get(this.UserId);
                    postModel.searchParams["UserName"] = user.UserName;
                }
                else
                {
                    postModel.dealerCode = dealerCode;
                }
                List<TransactionLog> listOfLogs = _transactionLogRepo.Search(postModel, out filteredResultsCount, out totalResultsCount, true, ShowReadLogInFailed);

                List<TransactionLogViewModel> record = listOfLogs.ConvertList<TransactionLog, TransactionLogViewModel>();

                foreach (var item in record)
                {
                    // Get ErrorMessage
                    item.ErrorMessage = Utils.GetErrorMessages(item.Output, ErrorMessageMaxLength);
                }

                return Json(new
                {
                    Records = record,
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    Records = new List<TransactionLog>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion



        #region TOP API List
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_TransactionLog_Read)]
        public IHttpActionResult GetTopRankedApi(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                // load data
                List<TransactionLog> topApiList = _transactionLogRepo.SearchTopRankedApi(postModel, out filteredResultsCount, out totalResultsCount);

                // simplify endpoint
                //topApiList.Select(x => { x.EndPoint = Utils.GetShortEndpoint(x.EndPoint); return x; }).ToList();

                return Json(new
                {
                    Records = topApiList,
                    TotalRecord = filteredResultsCount
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = GetInnerException(ex).ToString()
                }
                );
            }
        }

        #endregion

        #region Method Get Transaction List
        /// <summary>
        /// Populate the list of transactions
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="resendTrans"></param>
        /// <returns></returns>
        private static TransactionLogViewModel GetTransactionList(TransactionLog transaction, List<TransactionLog> resendTrans)
        {
            var model = transaction.ConvertObject<TransactionLogViewModel>();
            model.IsResolved = transaction.Status;
            model.IsParentTransaction = !transaction.ParentId.HasValue || transaction.ParentId.Value.Equals(null);

            if (resendTrans != null && resendTrans.Count > 0)
            {
                model.ResendLog = new List<TransactionLogViewModel>();
                foreach (var item in resendTrans)
                {
                    TransactionLogViewModel resendLog = item.ConvertObject<TransactionLogViewModel>();
                    resendLog.StatusStr = item.Status ? "Success" : "Failed";

                    if (item.Status)
                    {
                        model.IsResolved = item.Status;
                    }

                    model.ResendLog.Add(resendLog);
                }
            }

            model.StatusStr = transaction.Status ? "Success" : "Failed";

            return model;
        }
        #endregion

        /// <summary>
        /// Set configuration
        /// </summary>
        private void SetConfiguration(EnumPageType pageType)
        {
            // get the configuration setting first
            switch (pageType)
            {
                case EnumPageType.Dashboard_Top:
                    break;
                case EnumPageType.Dashboard_Bottom:
                    break;
                case EnumPageType.AuditLog_Transaction:
                    break;
                case EnumPageType.AuditLog_Failed_Transaction:
                    ErrorMessageMaxLength = _appConfigRepo.GetConfigValue<int>(Constants.ConfigKey.WebUI_ErrorMessageMaxLength);
                    if (ErrorMessageMaxLength == 0)
                        ErrorMessageMaxLength = 100;
                    // get from database
                    var config = _appConfigRepo.GetByKey(Constants.ConfigKey.WebUI_ReadLoggingInFailedTransaction_Display);

                    ShowReadLogInFailed = true;

                    // get status value
                    bool isConfigActive = config == null ? false : config.IsActive;

                    if (config != null)
                    {
                        if (isConfigActive)
                        {
                            ShowReadLogInFailed = config.Value.ToBool();
                        }
                    }

                    break;
                case EnumPageType.AuditLog_Error_Log:
                    break;
                case EnumPageType.AuditLog_Runtime_Transaction:
                    break;
                case EnumPageType.AuditLog_User_Activity:
                    break;
                default:
                    ShowReadLogInFailed = true;
                    PageSize = _appConfigRepo.GetConfigValue<int>(Constants.ConfigKey.WebUI_PageSize);
                    if (PageSize == 0)
                        PageSize = 10;
                    break;
            }
        }

        #region GetApplicationList
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_FailedTransactionLog_Read)]
        public IHttpActionResult GetApplicationList()
        {
            List<MsApplication> msApplications = new List<MsApplication>();
            if (this.IsDMSAdmin)
            {
                MsApplication msApplication = new MsApplication();
                msApplication.AppId = Guid.Empty;
                msApplication.Name = "All Applications";
                msApplications.Add(msApplication);
            }

            msApplications.AddRange(_appRepo.GetListOfApplication(new Guid(this.ClientId), this.IsDMSAdmin));

            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = msApplications });
        }
        #endregion

        #region GetClientList
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_FailedTransactionLog_Read)]
        public IHttpActionResult GetClientList()
        {
            List<APIClient> apiClients = new List<APIClient>();

            if (this.IsDMSAdmin)
            {
                APIClient apiClient = new APIClient();
                apiClient.ClientId = Guid.Empty;
                apiClient.Name = "All Clients";

                apiClients.Add(apiClient);
                apiClients.AddRange(_clientRepo.GetAll());
            }

            else
            {
                APIUser user = _userRepo.Get(this.UserId);
                apiClients.AddRange(_clientRepo.GetUserClient(user));
            }


            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = apiClients });
        }
        #endregion

        #region GetDealerList
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_FailedTransactionLog_Read)]
        public IHttpActionResult GetDealerList()
        {
            List<Dealer> dealers = new List<Dealer>();
            if (this.IsDMSAdmin)
            {
                Dealer dealer = new Dealer();
                dealer.DealerCode = "";
                dealer.DealerName = "All Dealers";

                dealers.Add(dealer);
                dealers.AddRange(_dealerRepo.GetActiveDealers());
            }
            else
            {
                Dealer dealer = _dealerRepo.GetByCode(this.DealerCode);
                dealers.Add(dealer);
            }

            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = dealers });
        }
        #endregion


        #region Method Delete Transction Log Within Interval Date
        /// <summary>
        /// Delete transaction log
        /// </summary>
        /// <param name="intervalParam"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Log_Delete)]
        public IHttpActionResult Delete(DeleteLogViewModel intervalParam)
        {
            try
            {
                DateTime retainedLogFromDate = DateTime.Now.AddDays(-AppConfigs.GetInt("RetainTransactionLogDays")).Date;
                DateTime from = intervalParam.From.ToLocalTime().Date;
                DateTime to = intervalParam.To.ToLocalTime().Date;

                if (from > to)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Invalid interval date." });
                }

                if (to >= retainedLogFromDate)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("Retained transaction log. Unable to delete transaction log with date >= {0}", retainedLogFromDate.ToString("yyyy-MM-dd")) });
                }

                string dealerCode = IsDMSAdmin ? string.Empty : this.DealerCode;
                ResponseMessage result = _transactionLogRepo.Delete(from, to, dealerCode);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Failed to delete transaction log. " + GetInnerException(ex).Message });
            }
        }
        #endregion

    }
}