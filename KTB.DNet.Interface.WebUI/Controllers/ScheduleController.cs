#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Schedule controller class
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
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class ScheduleController : BaseController
    {
        #region Initialize
        private IScheduleRepository<APISchedule, int> _scheduleRepo;
        private IDealerRepository<Dealer, int> _dealerRepo;
        #endregion

        #region Constructor
        public ScheduleController(
           IScheduleRepository<APISchedule, int> scheduleRepo,
           IDealerRepository<Dealer, int> dealerRepo)
        {
            _scheduleRepo = scheduleRepo;
            _dealerRepo = dealerRepo;

            _scheduleRepo.SetUserLogin(UserName);
            _dealerRepo.SetUserLogin(UserName);
        }
        #endregion

        #region Method Get
        /// <summary>
        /// Get Schedule by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Schedule_Update)]
        public IHttpActionResult Get(int id)
        {
            APISchedule _schedule = _scheduleRepo.Get(id);
            if (_schedule != null)
            {

                ScheduleViewModel viewModel = new ScheduleViewModel
                {
                    Id = _schedule.Id,
                    Name = _schedule.Name,
                    ScheduleType = _schedule.ScheduleType,
                    ScheduleDay = _schedule.ScheduleDay,
                    MonthDay = _schedule.MonthDay,
                    ScheduleTime = _schedule.ScheduleTime,
                    Interval = _schedule.Interval,
                    DealerCode = _schedule.DealerCode,
                    CreatedBy = _schedule.CreatedBy,
                    CreatedTime = _schedule.CreatedTime,
                    UpdatedBy = _schedule.UpdatedBy,
                    UpdatedTime = _schedule.UpdatedTime
                };
                //PopulateViewBagData(viewModel);
                //GetOptions
                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Data = viewModel
                });
            }
            else
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Id is not valid"
                });
            }
        }
        #endregion

        #region Method Create
        ///// <summary>
        ///// Create schedule
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Schedule_Create)]
        public IHttpActionResult Create(ScheduleViewModel model)
        {
            try
            {
                //GetOptions();
                // TODO: Add insert logic here
                //if (permission.Name == null || permission.Description == null)
                //{
                //    ModelState.AddModelError("Permission Name and Description are required", "Permission Name and Description must be entered");
                //}

                if (ModelState.IsValid)
                {
                    var schedule = new APISchedule()
                    {
                        Name = model.Name,
                        ScheduleType = model.ScheduleType,
                        ScheduleDay = model.ScheduleType == ScheduleType.Weekly ? model.ScheduleDay : null,
                        MonthDay = model.ScheduleType == ScheduleType.Monthly ? model.MonthDay : null,
                        ScheduleTime = model.ScheduleTime,
                        DealerCode = model.DealerCode,
                        Interval = model.Interval,

                    };

                    ResponseMessage result = _scheduleRepo.Create(schedule);
                    if (result.Success)
                        result.Message = string.Format("Schedule with name {0} has been successfully created", schedule.Name);

                    return Json(result);

                }
                else
                {
                    return Json(new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Error,
                        Message = "Model is not valid"
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
        ///// Delete Schedule
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpDelete]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Schedule_Delete)]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ResponseMessage result = _scheduleRepo.Delete(id);
                if (result.Success)
                    result.Message = string.Format("Schedule with id {0} has been successfully deleted", id.ToString());
                return Json(result);
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
        /// Update Schedule
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Schedule_Update)]
        public IHttpActionResult Update(ScheduleViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var entity = new APISchedule()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        ScheduleType = model.ScheduleType,
                        ScheduleDay = model.ScheduleType == ScheduleType.Weekly ? model.ScheduleDay : null,
                        MonthDay = model.ScheduleType == ScheduleType.Monthly ? model.MonthDay : null,
                        ScheduleTime = model.ScheduleTime,
                        Interval = model.Interval,
                        DealerCode = model.DealerCode
                    };

                    ResponseMessage result = _scheduleRepo.Update(entity);
                    return Json(result);

                }
                else
                {
                    return Json(new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Error,
                        Message = "Model is not valid"
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
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Schedule_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<APISchedule> listOfSchedule = _scheduleRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                return Json(
                    new
                    {
                        Records = listOfSchedule.ConvertList<APISchedule, ScheduleViewModel>(),
                        TotalRecord = filteredResultsCount
                    }
                );
            }
            catch (Exception ex)
            {
                return Json(new
                    {
                        Records = new List<ScheduleViewModel>(),
                        TotalRecord = 0
                    }
                );
            }
        }
        #endregion

        #region Method Get Options
        ///// <summary>
        ///// Populate view bag data
        ///// </summary>
        ///// <param name="model"></param>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Schedule_Update)]
        public IHttpActionResult GetOptions()
        {

            List<EnumViewModel> scheduleTypeOptions = Enum.GetValues(typeof(ScheduleType))
               .Cast<ScheduleType>()
               .Select(t => new EnumViewModel
               {
                   Value = (int)t,
                   Text = t.ToString()
               }).ToList();

            List<EnumViewModel> scheduleDayOptions = Enum.GetValues(typeof(ScheduleDay))
               .Cast<ScheduleDay>()
               .Select(t => new EnumViewModel
               {
                   Value = (int)t,
                   Text = t.ToString()
               }).ToList();

            List<Dealer> dealerOptions = _dealerRepo.GetActiveDealers();

            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = new { ScheduleTypeOptions = scheduleTypeOptions, ScheduleDayOptions = scheduleDayOptions, DealerOptions = dealerOptions }
            });
        }
        #endregion

        #region Method SetUserModifier
        /// <summary>
        /// Set modifier for Created By and Updated By
        /// </summary>
        /// <param name="username"></param>
        public override void SetUserModifier(string username)
        {
            _scheduleRepo.SetUserLogin(username);
            _dealerRepo.SetUserLogin(username);
        }
        #endregion
    }
}
