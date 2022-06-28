#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Throttler controller class
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
using KTB.DNet.Interface.Framework.Helper;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebUI.Helper;
using KTB.DNet.Interface.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class ThrottlerController : BaseController
    {
        #region Initialize
        private IThrottleRepository<APIThrottle, int> _throttleRepo;
        private IEndpointPermissionRepository<APIEndpointPermission, int> _endpointPermissionRepo;
        #endregion

        #region Constructor
        public ThrottlerController(
            IThrottleRepository<APIThrottle, int> throttleRepo,
            IEndpointPermissionRepository<APIEndpointPermission, int> endpointPermissionRepo)
        {
            _throttleRepo = throttleRepo;
            _endpointPermissionRepo = endpointPermissionRepo;

            _throttleRepo.SetUserLogin(UserName);
            _endpointPermissionRepo.SetUserLogin(UserName);
        }
        #endregion

        #region Method Get
        /// <summary>
        /// Get Throttler By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Throttle_Read)]
        public IHttpActionResult Get(int id)
        {
            APIThrottle throttle = _throttleRepo.Get(id);

            ThrottleViewModel viewModel = throttle.ConvertObject<ThrottleViewModel>();

            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = viewModel
            });
        }
        #endregion

        #region Method Create
        ///// <summary>
        ///// Create throttler
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Throttle_Create)]
        public IHttpActionResult Create(ThrottleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var endpoint = _endpointPermissionRepo.Get(model.EndpointId);
                    if (endpoint != null)
                    {
                        if (string.IsNullOrEmpty(endpoint.URI))
                        {
                            return Json(new ResponseMessage()
                            {
                                Success = false,
                                Status = ResponseStatus.Error,
                                Message = "Please check the endpoint's URI. The URI is null"
                            });
                        }

                        model.Endpoint = endpoint.ConvertObject<EndpointPermissionViewModel>();
                    }
                    else
                    {
                        return Json(new ResponseMessage()
                        {
                            Success = false,
                            Status = ResponseStatus.Error,
                            Message = "Invalid Endpoint"
                        });
                    }

                    ResponseMessage result = _throttleRepo.Create(model.ConvertObject<APIThrottle>());

                    if (result.Success)
                    {
                        APIThrottle newThrottle = (APIThrottle)result.Data;
                        result.Data = newThrottle.ConvertObject<ThrottleViewModel>();

                        if (newThrottle != null)
                        {
                            // Save new throttle on configuration file
                            ThrottleViewModel saveModel = (ThrottleViewModel)result.Data;

                            try
                            {
                                if (ThrottleHelper.IsThrottleConfigXmlExist())
                                {
                                    ThrottleHelper.Save(saveModel);
                                }
                                else
                                {
                                    List<ThrottleViewModel> throttleViewModels = GetAllThrottles();

                                    if (ThrottleHelper.Export(throttleViewModels))
                                    {
                                        return Json(new ResponseMessage()
                                        {
                                            Success = true,
                                            Status = ResponseStatus.Success,
                                            Message = result.Message
                                        });
                                    }
                                    else
                                    {
                                        return Json(new ResponseMessage()
                                        {
                                            Success = false,
                                            Status = ResponseStatus.Error,
                                            Message = result.Message
                                        });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                return Json(new ResponseMessage()
                                {
                                    Success = false,
                                    Status = ResponseStatus.Error,
                                    Message = string.Format("{0}, but {1}", result.Message, ex.Message)
                                });
                            }
                        }

                        return Json(result);

                    }

                    return Json(result);

                }
                else
                {
                    return Json(new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Warning,
                        Message = "Failed to create throttler! Model is not valid!",
                        ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                });
            }
        }
        #endregion

        #region Method Delete
        ///// <summary>
        ///// Delete Throttler - DELETE
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpDelete]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Throttle_Delete)]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ThrottleViewModel model = new ThrottleViewModel();
                APIThrottle throttle = _throttleRepo.Get(id);
                model = throttle.ConvertObject<ThrottleViewModel>();

                
                ResponseMessage responseMessage = _throttleRepo.Delete(id);

                if (responseMessage.Success)
                {
                    // Remove throtle on configuration file
                    if (ThrottleHelper.IsThrottleConfigXmlExist())
                    {
                        ThrottleHelper.Delete(model);
                    }
                    else
                    {
                        List<ThrottleViewModel> throttleViewModels = GetAllThrottles();
                        if (ThrottleHelper.Export(throttleViewModels))
                        {
                            return Json(new ResponseMessage()
                            {
                                Success = true,
                                Status = ResponseStatus.Success,
                                Message = responseMessage.Message
                            });
                        }

                        else
                        {
                            return Json(new ResponseMessage()
                            {
                                Success = false,
                                Status = ResponseStatus.Error,
                                Message = responseMessage.Message
                            });
                        }
                    }
                }
                return Json(responseMessage);
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                });
            }

        }
        #endregion

        #region Method Update
        /// <summary>
        /// Edit Throttler 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Throttle_Update)]
        public IHttpActionResult Update(ThrottleViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ResponseMessage result = _throttleRepo.Update(model.ConvertObject<APIThrottle>());

                    if (result.Success)
                    {
                        //comment for now to avoid writing throttle configuration to the server 
                        APIThrottle updatedThrottle = (APIThrottle)result.Data;
                        result.Data = updatedThrottle.ConvertObject<ThrottleViewModel>();

                        if (updatedThrottle != null)
                        {
                            // Save new throttle on configuration file
                            ThrottleViewModel saveModel = updatedThrottle.ConvertObject<ThrottleViewModel>();

                            try
                            {
                                ThrottleHelper.Save(saveModel);
                            }
                            catch (Exception ex)
                            {
                                return Json(new ResponseMessage()
                                {
                                    Success = false,
                                    Status = ResponseStatus.Error,
                                    Message = string.Format("{0}, but {1}", result.Message, ex.Message)
                                });
                            }
                        }

                        return Json(result);

                    }

                    return Json(result);

                }
                else
                {
                    return Json(new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Warning,
                        Message = "Failed to create throttler! Model is not valid!",
                        ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                });
            }
        }
        #endregion

        #region Method Search
        ///// <summary>
        ///// Search
        ///// </summary>
        ///// <param name="postModel"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Throttle_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<APIThrottle> listOfThrottle = _throttleRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);
                List<ThrottleViewModel> dataList = new List<ThrottleViewModel>();

                foreach (APIThrottle throttle in listOfThrottle)
                {
                    ThrottleViewModel throttleViewModel = new ThrottleViewModel()
                    {
                        CreatedBy = throttle.CreatedBy,
                        CreatedTime = throttle.CreatedTime,
                        Enable = throttle.Enable,
                        Endpoint = throttle.Endpoint.ConvertObject<EndpointPermissionViewModel>() != null ? throttle.Endpoint.ConvertObject<EndpointPermissionViewModel>() : null,
                        EndpointId = throttle.EndpointId,
                        Id = throttle.Id,
                        UpdatedTime = throttle.UpdatedTime,
                        RequestLimit = throttle.RequestLimit,
                        TimeInSeconds = throttle.TimeInSeconds,
                        UpdatedBy = throttle.UpdatedBy
                    };

                    dataList.Add(throttleViewModel);
                }

                return Json(new
                {
                    Records = dataList,
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    Records = new List<APIThrottle>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Method Get Options
        /// <summary>
        /// Get Endpoint List - POST
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Throttle_Read)]
        public IHttpActionResult GetOptions()
        {
            try
            {
                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Data = new { EndpointOptions = _endpointPermissionRepo.GetEndpointWithNoThrottler() }
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                });
            }
        }
        #endregion

        #region Method Export
        ///// <summary>
        ///// Export from database to configuration file
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Throttle_Export)]
        public IHttpActionResult Export()
        {
            try
            {
                List<ThrottleViewModel> throttleViewModels = GetAllThrottles();
                if (ThrottleHelper.Export(throttleViewModels))
                {
                    return Json(new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Message = "Data exported successfully!"
                    });
                }

                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Failed to export data"
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                });
            }

        }
        #endregion

        #region private Get All Throttles Method
        private List<ThrottleViewModel> GetAllThrottles()
        {
            List<APIThrottle> allThrottles = _throttleRepo.GetAll();

            List<ThrottleViewModel> throttleViewModels = new List<ThrottleViewModel>();

            if (allThrottles != null)
            {
                // throttleViewModels = allThrottles.ConvertList<InterfaceThrottle, ThrottleViewModel>();
                for (var i = 0; i < allThrottles.Count; i++)
                {
                    ThrottleViewModel throttleViewModel = new ThrottleViewModel();
                    throttleViewModel = allThrottles[i].ConvertObject<ThrottleViewModel>();
                    throttleViewModels.Add(throttleViewModel);
                }
            }

            return throttleViewModels;

        }
        #endregion

        #region Method SetUserModifier
        /// <summary>
        /// Set modifier for Created By and Updated By
        /// </summary>
        /// <param name="username"></param>
        public override void SetUserModifier(string username)
        {
            _throttleRepo.SetUserLogin(username);
            _endpointPermissionRepo.SetUserLogin(username);
        }
        #endregion
    }
}