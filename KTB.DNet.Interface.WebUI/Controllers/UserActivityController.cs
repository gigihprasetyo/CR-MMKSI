#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : UserActivity controller class
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
    public class UserActivityController : BaseController
    {
        #region Initialize
        private IUserActivityRepository<UserActivity, long> _userActivityRepo;
        #endregion

        #region Constructor
        public UserActivityController(
            IUserActivityRepository<UserActivity, long> userActivityRepo)
        {
            _userActivityRepo = userActivityRepo;

            _userActivityRepo.SetUserLogin(this.UserName);
        }
        #endregion

        #region Method Search
        ///// <summary>
        ///// Load client application
        ///// </summary>
        ///// <param name="postModel"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Activity_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;
                string dealerCode = IsDMSAdmin ? string.Empty : this.DealerCode;

                List<UserActivity> listUserActivity = _userActivityRepo.Search(dealerCode, postModel, out filteredResultsCount, out totalResultsCount);

                return Json(new
                {
                    Records = listUserActivity.ConvertList<UserActivity, UserActivityViewModel>(),
                    TotalRecord = filteredResultsCount
                });

            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Records = new List<UserActivityViewModel>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Method GetOptions
        ///// <summary>
        ///// Set option data
        ///// </summary>
        ///// <param name="endpoint"></param>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Activity_Read)]
        public IHttpActionResult GetOptions()
        {
            List<EnumViewModel> userActivityTypeOptions = Enum.GetValues(typeof(UserActivityType))
               .Cast<UserActivityType>()
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
                    ListOfUserActivityTypeOptions = userActivityTypeOptions
                }
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
            _userActivityRepo.SetUserLogin(username);
        }
        #endregion

    }
}
