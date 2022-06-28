#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ActionWebApiFilterAttribute class
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
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper.Messages;
using KTB.DNet.Interface.Resources;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper.CustomAttribute
{
    public class ActionWebApiFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Check pre-processing action result
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            object id = "";
            if (actionContext.ActionArguments.TryGetValue("id", out id))
            {
                // split the condition because id is object
                if (id == null)
                {
                    this.SetNullResponse(actionContext);
                }
            }
        }

        /// <summary>
        /// set response content
        /// </summary>
        /// <param name="actionContext"></param>
        private void SetNullResponse(HttpActionContext actionContext)
        {
            List<MessageBase> messages = new List<MessageBase>();
            MessageBase message = new MessageBase();
            message.ErrorCode = ErrorCode.DataTypeOrDataFormatInvalid;
            message.ErrorMessage = MessageResource.ErrorMsgDataFormat;
            messages.Add(message);

            var result = HttpCodeMessage.BuildErrorResult(messages);
            HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(messages);

            var jsonResult = JsonConvert.SerializeObject(result);

            var response = new HttpResponseMessage
            {
                StatusCode = httpStatusCode,
                ReasonPhrase = MessageResource.ErrorMsgDataFormat,
                Content = new StringContent(jsonResult, Encoding.UTF8, "application/json")
            };

            actionContext.Response = response;
        }
    }
}