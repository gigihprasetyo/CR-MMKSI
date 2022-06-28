#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Client controller class
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
using KTB.DNet.Interface.WebUI.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class EndpointGroupController : BaseController
    {
        #region Initialize
        private IEndpointPermissionRepository<APIEndpointPermission, int> _endpointPermissionRepo;
        private IStandardCodeRepository<StandardCode, int> _standardCodeRepo;
        #endregion

        #region Constructor
        public EndpointGroupController(IEndpointPermissionRepository<APIEndpointPermission, int> endpointPermissionRepo,
                                        IStandardCodeRepository<StandardCode, int> standardCodeRepo)
        {
            _endpointPermissionRepo = endpointPermissionRepo;
            _standardCodeRepo = standardCodeRepo;
        }
        #endregion

        #region Method Get Endpoint Group Options
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Permission_Read)]
        public IHttpActionResult GetEndpointGroupOptions()
        {
            List<StandardCode> endpointGroups = _standardCodeRepo.GetByCategory("EndpointGroup");
            List<EnumViewModel> listOfEndpointGroupOptions = null;
            if (endpointGroups.Count > 0)
            {
                listOfEndpointGroupOptions = endpointGroups.Select(s => new EnumViewModel() { Value = s.ValueId, Text = s.ValueDesc }).ToList();
            }

            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = new
                {
                    ListOfEndpointGroupOptions = listOfEndpointGroupOptions
                }
            });
        }
        #endregion

        #region Method Endpoint Permission's Group in Bulk
        /// <summary>
        /// Create client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Client_Create)]
        public IHttpActionResult SaveEndpointPermissionGroup(EndpointPermissionGroupBulkViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResponseMessage result = _endpointPermissionRepo.UpdateEndpointPermissionGroup(model.EndpointIds, model.EndpointGroupId, this.UserName);
                    return Json(result);
                }

                catch (Exception ex)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
                }
            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to create client! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
        }
        #endregion

    }
}
