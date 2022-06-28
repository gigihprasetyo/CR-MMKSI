#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : HttpCodeMessage class
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
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Resources;
using System.Web.Mvc;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper.Messages
{
    public static class HttpCodeMessage
    {
        /// <summary>
        /// Get Http status code
        /// </summary>
        /// <param name="messageBases"></param>
        /// <returns></returns>
        public static HttpStatusCode GetHttpStatusCode(List<MessageBase> messageBases)
        {
            // set first error code as html status code
            if (messageBases.Count > 0)
                return GetHttpStatusCode(messageBases[0]);
            else
                return HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Get Http status code
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static HttpStatusCode GetHttpStatusCode(MessageBase message)
        {
            try
            {
                // get dnet error code value
                var value = (int)message.ErrorCode;

                // get html status code name in ErrorCode enum
                string enumName = "Er_" + value.ToString();

                // get html status code value
                int intValue = (int)Enum.Parse(typeof(ErrorCode), enumName);

                // get httpstatus code enum
                HttpStatusCode httpStatusCode = (HttpStatusCode)(intValue);

                // return 
                return httpStatusCode;
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }

        /// <summary>
        /// Set Error result
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static ResponseBase<MessageBase> BuildErrorResult(List<MessageBase> messages)
        {
            var result = new ResponseBase<MessageBase>()
            {
                lst = null,
                success = false,
                messages = messages,
                _id = -1,
                total = 0
            };

            return result;
        }

        /// <summary>
        /// Set Error result
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResponseBase<MessageBase> BuildErrorResult(MessageBase message)
        {
            var result = new ResponseBase<MessageBase>()
            {
                lst = null,
                success = false,
                messages = new List<MessageBase>() { message },
                _id = -1,
                total = 0
            };

            return result;
        }

        /// <summary>
        /// Get error message
        /// </summary>
        /// <param name="ModelState"></param>
        /// <returns></returns>
        internal static List<MessageBase> GetErrorMessage(System.Web.Http.ModelBinding.ModelStateDictionary ModelState)
        {
            List<MessageBase> messages = new List<MessageBase>();

            var errors = ModelState.Values.SelectMany(x => x.Errors);

            if (ModelState == null)
            {
                messages.Add(SetMessage(null));
            }

            foreach (var error in errors.Where(e => !string.IsNullOrEmpty(e.ErrorMessage)))
            {
                messages.Add(SetMessage(error));
            }

            foreach (var exMesssage in errors.Where(e => e.Exception != null))
            {
                bool getEx = false;
                if (exMesssage.Exception.Message.Contains("not a valid integer") || exMesssage.Exception.Message.Contains("Error converting value {null} to type"))
                {
                    ResourceManager rm = new ResourceManager(typeof(FieldResource));
                    string propName = rm.GetString(ModelState.Keys.FirstOrDefault().Split('.').Last());
                    if (propName == null)
                        propName = ModelState.Keys.FirstOrDefault().Split('.').Last();

                    messages.Add(new MessageBase(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(MessageResource.ErrorMsgDataInteger, propName)));
                    getEx = true;
                }
                //this code is commented  since if we put validation on parameter DTO the item will display twice. - wiwin
                else if (messages.Count == 0 && exMesssage.Exception.Message.Contains("Required property"))
                {
                    ResourceManager rm = new ResourceManager(typeof(FieldResource));
                    string propName = rm.GetString(exMesssage.Exception.Message.Split('\'')[1]);

                    messages.Add(new MessageBase(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, propName)));
                    getEx = true;
                }
                if (getEx) break;
            }

            if (messages.Count == 0)
            {
                messages.Add(SetMessage(null));
            }

            return messages;
        }

        /// <summary>
        /// Set Message
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private static MessageBase SetMessage(System.Web.Http.ModelBinding.ModelError error)
        {
            // instantiate
            MessageBase message = new MessageBase();

            try
            {
                // null validation
                if (error == null)
                {
                    // set the error code
                    message.ErrorCode = ErrorCode.DataTypeOrDataFormatInvalid;

                    // set the error emssage
                    message.ErrorMessage = MessageResource.ErrorMsgDataFormat;
                }
                else
                {
                    // split just in case contains error code
                    string[] errors = error.ErrorMessage.Split('#');
                    if (errors.Length > 1)
                    {
                        // set the error code
                        message.ErrorCode = (ErrorCode)Enum.Parse(typeof(ErrorCode), errors[0]);

                        // set the error emssage
                        message.ErrorMessage = errors[1];
                    }
                    else
                    {
                        // set the error code invalid format by default
                        message.ErrorCode = ErrorCode.DataTypeOrDataFormatInvalid;

                        // set the error emssage
                        message.ErrorMessage = error.ErrorMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                // set the error code
                message.ErrorCode = ErrorCode.UnhandledException;

                // set the error emssage
                message.ErrorMessage = ex.Message;
            }

            return message;
        }
    }
}