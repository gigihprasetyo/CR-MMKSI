 #region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EndpointSchedule controller class
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
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebUI.Helper;
using KTB.DNet.Interface.WebUI.Models;
using KTB.DNet.Interface.WebUI.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class EndpointScheduleController : BaseController
    {
        #region Initialize
        private IEndpointScheduleRepository<APIEndpointSchedule, int> _endpointScheduleRepo;
        private IScheduleRepository<APISchedule, int> _scheduleRepo;
        private IEndpointPermissionRepository<APIEndpointPermission, int> _permissionRepo;
        #endregion

        #region Constructor
        public EndpointScheduleController(
            IEndpointScheduleRepository<APIEndpointSchedule, int> endpointScheduleRepo,
            IScheduleRepository<APISchedule, int> scheduleRepo,
            IEndpointPermissionRepository<APIEndpointPermission, int> permissionRepo)
        {
            _endpointScheduleRepo = endpointScheduleRepo;
            _scheduleRepo = scheduleRepo;
            _permissionRepo = permissionRepo;

            _endpointScheduleRepo.SetUserLogin(this.UserName);
            _scheduleRepo.SetUserLogin(this.UserName);
            _permissionRepo.SetUserLogin(this.UserName);
        }
        #endregion

        #region Get Schedule
        /// <summary>
        /// Get List of Endpoint Schedule by Endpoint Id
        /// </summary>
        /// <param name="endpointId"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_EndpointSchedule_Read)]
        public IHttpActionResult GetUnassignedSchedule(int endpointId)
        {
            try
            {
                List<APIEndpointSchedule> listOfData = _endpointScheduleRepo.GetByEndpointId(endpointId);

                List<int> selectedIds = listOfData.Select(s => s.ScheduleId).ToList();

                List<APISchedule> schedules = _scheduleRepo.GetAll().Where(w => !selectedIds.Contains(w.Id)).OrderBy(o => o.Name).ToList();
                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Data = schedules
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Message = GetInnerException(ex).Message
                });
            }
        }
        #endregion

        #region Search Scheduled/// <summary>
        /// SearchScheduled
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_EndpointSchedule_Read)]
        public IHttpActionResult SearchScheduled(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<APIEndpointPermission> listOfPermission = _permissionRepo.Search(postModel, out filteredResultsCount, out totalResultsCount, true);

                return Json(new
                {
                    Records = listOfPermission,
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    Records = new List<APIEndpointPermission>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Add Bulk Endpoint Schedule
        ///// <summary>
        ///// Add Endpoint Schedule in Bulk
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_EndpointSchedule_Create)]
        public IHttpActionResult AddBulkEndpointSchedule(EndpointScheduleViewModel model)
        {
            try
            {
                if (model.ListOfSelectedScheduleId.Any())
                {
                    List<APIEndpointSchedule> list = new List<APIEndpointSchedule>();
                    foreach (var item in model.ListOfSelectedScheduleId)
                    {
                        list.Add(new APIEndpointSchedule { ScheduleId = item, EndpointId = model.EndpointPermissionId });
                    }

                    ResponseMessage result = _endpointScheduleRepo.AddEndpointSchedule(list);
                    return Json(result);
                }

                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Endpoint Schedule model is not valid!",
                    ModelState = model
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = GetInnerException(ex).Message
                });
            }
        }
        #endregion

        #region Method Delete
        [HttpDelete]
        //[ValidateAntiForgeryToken]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_EndpointSchedule_Delete)]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ResponseMessage result = _endpointScheduleRepo.Delete(id);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = GetInnerException(ex).Message
                });
            }
        }
        #endregion

        #region Method Search
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_EndpointSchedule_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel, int endpointId)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<APIEndpointSchedule> listOfData = _endpointScheduleRepo.SearchByEndpointId(postModel, out filteredResultsCount, out totalResultsCount, endpointId);

                return Json(new
                {
                    // this is what datatables wants sending back
                    recordsTotal = totalResultsCount,
                    recordsFiltered = filteredResultsCount,
                    Records = listOfData,
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    // this is what datatables wants sending back
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    Records = new List<APIEndpointSchedule>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Method Get Options
        /// <summary>
        /// Get options for Endpoint Schedule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_EndpointSchedule_Read)]
        public IHttpActionResult GetOptions()
        {

            List<EnumViewModel> transactionType = Enum.GetValues(typeof(TransactionType))
               .Cast<TransactionType>()
               .Select(t => new EnumViewModel
               {
                   Value = (int)t,
                   Text = t.ToString()
               }).ToList();

            List<EnumViewModel> operationType = Enum.GetValues(typeof(OperationType))
               .Cast<OperationType>()
               .Select(t => new EnumViewModel
               {
                   Value = (int)t,
                   Text = t.ToString()
               }).ToList();

            List<EnumViewModel> scheduleType = Enum.GetValues(typeof(ScheduleType))
               .Cast<ScheduleType>()
               .Select(t => new EnumViewModel
               {
                   Value = (int)t,
                   Text = t.ToString()
               }).ToList();

            List<EnumViewModel> scheduleDay = Enum.GetValues(typeof(ScheduleDay))
               .Cast<ScheduleDay>()
               .Select(t => new EnumViewModel
               {
                   Value = (int)t,
                   Text = t.ToString()
               }).ToList();

            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = new
                {
                    ListOfTransactionTypeOptions = transactionType,
                    ListOfOperationTypeOptions = operationType,
                    ListOfScheduleTypeOptions = scheduleType,
                    ListOfScheduleDayOptions = scheduleDay
                }
            });
        }
        #endregion

        #region Method SetUserModifier
        /// <summary>
        /// Set modifier for Created By and Updated By
        /// </summary>
        public override void SetUserModifier(string username)
        {
            _endpointScheduleRepo.SetUserLogin(username);
            _scheduleRepo.SetUserLogin(username);
        }
        #endregion
    }
}