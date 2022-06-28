#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Dashboard controller class
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
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebUI.Helper;
using KTB.DNet.Interface.WebUI.Models;
using MvcBreadCrumbs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class DashboardController : BaseController
    {
        private IRoleRepository<APIRole, int> _roleRepo;
        private IUserRepository<APIUser, int> _userRepo;
        private IDealerRepository<Dealer, int> _dealerRepo;
        private IEndpointPermissionRepository<APIEndpointPermission, int> _permissionRepo;
        private ITransactionLogRepository<TransactionLog, long> _transactionLogRepo;
        private IClientRepository<APIClient, Guid> _clientRepo;
        private DateTime DATETIME_DEFAULT_VALUE = new DateTime(1900, 1, 1);
        private int TopWidgetRow;
        private int BottomWidgetRow;
        private IApplicationConfigRepository<ApplicationConfig, long> _appConfigRepo;
        private string ApiRoot = ConfigurationManager.AppSettings["ApiRoot"];
        private string TokenIssuer = ConfigurationManager.AppSettings["TokenIssuer"];

        public DashboardController(
            IRoleRepository<APIRole, int> roleRepo,
            IUserRepository<APIUser, int> userRepo,
            IDealerRepository<Dealer, int> dealerRepo,
            IEndpointPermissionRepository<APIEndpointPermission, int> permissionRepo,
            ITransactionLogRepository<TransactionLog, long> transRepo,
            IClientRepository<APIClient, Guid> clientRepo,
            IApplicationConfigRepository<ApplicationConfig, long> appConfigRepo
            )
        {
            _roleRepo = roleRepo;
            _userRepo = userRepo;
            _dealerRepo = dealerRepo;
            _permissionRepo = permissionRepo;
            _clientRepo = clientRepo;
            _transactionLogRepo = transRepo;
            _appConfigRepo = appConfigRepo;
        }

        [BreadCrumb(Clear = true, Label = "Dashboard", Order = 1)]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_App_Access)]
        [HttpGet]
        public IHttpActionResult GetInfoboxes()
        {
            try
            {
                // populate all dashboard data

                HomeViewModel homeViewModel = new HomeViewModel();
                homeViewModel.RoleCount = _roleRepo.GetTotalRoles(this.UserId);
                homeViewModel.UserCount = _userRepo.GetUserCount(this.UserId, this.DealerId);
                homeViewModel.PermissionCount = _permissionRepo.GetPermissionCount(this.UserId);
                homeViewModel.DealerCount = _dealerRepo.GetDealerCount(this.UserId);
                homeViewModel.ClientCount = _clientRepo.GetTotalClient(this.IsDMSAdmin, this.UserId);
                homeViewModel.RoleName = GetDefaultRoleName(this.UserName);
                homeViewModel.IsDMSAdmin = IsDMSAdmin;


                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Data = homeViewModel
                }
                );
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

        [BreadCrumb(Clear = true, Label = "Dashboard", Order = 1)]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_App_Access)]
        [HttpGet]
        public IHttpActionResult GetTopRankedApi()
        {
            try
            {
                SetWidgets();

                // load data
                List<TransactionLog> listOfTopRankedApi = _transactionLogRepo.GetTopRankedApi(TopWidgetRow);

                //topApiList.Select(x => { x.EndPoint = Utils.GetShortEndpoint(x.EndPoint); return x; }).ToList();

                return Json(new
                {
                    Records = listOfTopRankedApi,
                    TotalRecord = listOfTopRankedApi.Count()
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

        [BreadCrumb(Clear = true, Label = "Dashboard", Order = 1)]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_App_Access)]
        [HttpGet]
        public IHttpActionResult GetLatestTransactionList()
        {
            try
            {
                SetWidgets();
                // load data

                string dealerCode = string.Empty;
                if (!IsDMSAdmin)
                {
                    dealerCode = this.DealerCode;
                }
                List<TransactionLog> list = _transactionLogRepo.GetLatestTransaction(dealerCode, BottomWidgetRow);
                List<TransModel> result = new List<TransModel>();
                foreach (var item in list)
                {
                    string apiName = string.Empty;
                    string[] apiNames = item.Endpoint != null ? item.Endpoint.Split('/') : new string[0];
                    if (apiNames.Length > 2)
                    {
                        apiName = string.Empty;
                        for (int i = apiNames.Length - 2; i < apiNames.Length; i++)
                        {
                            if (string.IsNullOrEmpty(apiName))
                                apiName = apiNames[i];
                            else
                                apiName += "/" + apiNames[i];
                        }
                    }

                    string time = item.CreatedTime.Value.ToString("yyyy/MM/dd HH:mm:ss");

                    result.Add(new TransModel { ID = item.Id, CreatedBy = item.CreatedBy, Endpoint = apiName, CreatedTime = time, Status = item.Status ? "Success" : "Failed", IsResolved = item.IsResolved });
                }


                return Json(new
                {
                    Records = result,
                    TotalRecord = result.Count
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

        [BreadCrumb(Clear = true, Label = "Dashboard", Order = 1)]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_App_Access)]
        [HttpGet]
        public IHttpActionResult GetLatestErrorList()
        {
            try
            {
                SetWidgets();

                // load data
                List<ErrorModel> latestErrors = GetLatestErrors();

                return Json(new
                {
                    Records = latestErrors,
                    TotalRecord = latestErrors.Count
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

        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_App_Access)]
        [HttpGet]
        public IHttpActionResult GetFailedTransactionSummaryPerDealer()
        {
            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = _transactionLogRepo.GetFailedTransactionSummaryPerDealer(DateTime.Now, IsDMSAdmin ? string.Empty : DealerCode)
            });
        }

        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_App_Access)]
        [HttpGet]
        public IHttpActionResult GetTransactionSummary()
        {
            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = _transactionLogRepo.GetTransactionSummary(DateTime.Now, IsDMSAdmin ? string.Empty : DealerCode)
            });
        }

        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_App_Access)]
        [HttpGet]
        public IHttpActionResult GetTransactionSummaryPerDealer()
        {
            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = _transactionLogRepo.GetTransactionSummaryPerDealer(DateTime.Now)
            });
        }

        #region Private Methods
        private string GetDefaultRoleName(string username)
        {
            // invalid, now user has more then one role
            return string.Empty;
        }

        private List<ErrorModel> GetLatestErrors()
        {
            string dealerCode = string.Empty;
            if (!IsDMSAdmin)
            {
                dealerCode = this.DealerCode;
            }
            var list = _transactionLogRepo.GetLatestErrorTransaction(dealerCode, TopWidgetRow);
            List<ErrorModel> result = new List<ErrorModel>();
            try
            {
                foreach (var item in list)
                {
                    string apiName = string.Empty;
                    string[] apiNames = item.Endpoint != null ? item.Endpoint.Split('/') : new string[0];
                    if (apiNames.Length > 2)
                    {
                        apiName = string.Empty;
                        for (int i = apiNames.Length - 2; i < apiNames.Length; i++)
                        {
                            if (string.IsNullOrEmpty(apiName))
                                apiName = apiNames[i];
                            else
                                apiName += "/" + apiNames[i];
                        }
                    }

                    string time = item.CreatedTime.Value.ToString("yyyy/MM/dd HH:mm:ss");

                    result.Add(new ErrorModel { ID = item.Id, Endpoint = apiName, ErrorTime = time, ErrorMessage = string.Empty, IsResolved = item.IsResolved });
                }
            }
            catch
            {
                return result;
            }

            return result;
        }

        private void SetWidgets()
        {
            // get the configuration setting first
            TopWidgetRow = _appConfigRepo.GetConfigValue<int>(Constants.ConfigKey.WebUI_TopWidgetRow);
            BottomWidgetRow = _appConfigRepo.GetConfigValue<int>(Constants.ConfigKey.WebUI_BottomWidgetRow);
            if (TopWidgetRow == 0)
                TopWidgetRow = 5;
            if (BottomWidgetRow == 0)
                BottomWidgetRow = 5;
        }
        #endregion
    }
}