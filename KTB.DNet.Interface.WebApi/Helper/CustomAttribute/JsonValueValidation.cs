#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : JsonValueValidation class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespaces
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.WebApi.Controllers;
using KTB.DNet.Interface.WebApi.Helper.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper.CustomAttribute
{
    /// <summary>
    /// Author          : Muhamad Ridwan
    /// Created On      : April, 2018
    /// 
    /// PT Mitrais
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class JsonValueValidationAttribute : ActionFilterAttribute
    {
        #region private property
        private Type _objectType;
        #endregion

        #region constructor
        public JsonValueValidationAttribute(Type objectType)
        {
            this._objectType = objectType;
        }
        #endregion

        #region public method
        /// <summary>
        /// Override the on action event
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // error messages
            List<MessageBase> errorList = new List<MessageBase>();
            string dataOnrequestBody = string.Empty;
            JToken jToken = null;

            try
            {
                dataOnrequestBody = GetDataOnRequestBody(actionContext);

                // parse data to json
                jToken = JToken.Parse(dataOnrequestBody);

                SetOriginalJson(jToken, actionContext);

                // validate json
                ValidateJSON(jToken, this._objectType, errorList);

                // send response item if the json is invalid
                if (errorList.Any())
                {
                    SetJsonValidationErrorResponse(errorList, actionContext, jToken);
                }
            }
            catch (JsonReaderException ex)
            {
                object input;
                if (jToken != null)
                {
                    input = jToken;
                }
                else
                {
                    try
                    {
                        input = Regex.Unescape(dataOnrequestBody);
                    }
                    catch
                    {
                        input = ex.Message;
                    }
                }

                // add the error
                errorList.Add(new MessageBase(ErrorCode.DataJsonInvalidFormat, "JSON Error: " + ex.Message));

                // send it to response
                SetJsonValidationErrorResponse(errorList, actionContext, input);
            }
            catch (Exception ex)
            {
                SetJsonValidationErrorResponse(new List<MessageBase>() { new MessageBase { ErrorCode = Model.ErrorCode.UnhandledException, ErrorMessage = "JSON Error: " + ex.Message } }, actionContext, dataOnrequestBody);
            }
        }
        #endregion

        #region private method
        /// <summary>
        /// GetDataOnRequestBody
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private string GetDataOnRequestBody(HttpActionContext actionContext)
        {
            BaseController bc = (BaseController)actionContext.ControllerContext.Controller;
            return bc.DataOnRequestBody;
        }

        /// <summary>
        /// Set Original Json
        /// </summary>
        /// <param name="jtoken"></param>
        private void SetOriginalJson(JToken jtoken, HttpActionContext actionContext)
        {
            BaseController bc = (BaseController)actionContext.ControllerContext.Controller;
            bc.SetOriginalRequestData(jtoken, this._objectType);
        }

        /// <summary>
        /// Set JSON validation error response
        /// </summary>
        /// <param name="errorList"></param>
        /// <param name="actionContext"></param>
        /// <param name="input"></param>        
        private void SetJsonValidationErrorResponse(List<MessageBase> errorList, HttpActionContext actionContext, object requestData)
        {
            // populate response json
            ResponseBase<object> responseJson = PopulateValidationError<object>(errorList, null);
            var messageJson = JsonConvert.SerializeObject(responseJson);

            // get http error code
            HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(responseJson.messages);

            BaseController bc = (BaseController)actionContext.ControllerContext.Controller;
            var logId = bc.LogActionFilterError(requestData, this._objectType, responseJson);

            // create response
            actionContext.Response = actionContext.Request.CreateErrorResponse(httpStatusCode, MessageResource.ErrorMsgDataFormat);
            actionContext.Response.RequestMessage = actionContext.Request;

            // set response content
            actionContext.Response.Content = new StringContent(messageJson, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Validate json
        /// </summary>
        /// <param name="JTokenObj"></param>
        /// <param name="type"></param>
        /// <param name="errorList"></param>
        /// <returns></returns>
        private bool ValidateJSON(JToken JTokenObj, Type type, List<MessageBase> errorList)
        {
            if (type.IsArray || (type.Name.ToUpper().Contains("LIST") && type.GenericTypeArguments.Count() > 0))
            {
                // validate json list
                ValidateList(JTokenObj, type.GenericTypeArguments[0], errorList);
            }
            else
            {
                // validate json object
                ValidateObject(JTokenObj, type, errorList);
            }

            return errorList.Count == 0;
        }

        /// <summary>
        /// Validate JSON List
        /// </summary>
        /// <param name="JTokenList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool ValidateList(JToken JTokenList, Type type, List<MessageBase> errorList)
        {
            // is it list of composite object
            bool isObject = type.IsClass && type != typeof(string);

            // parse json array
            JArray JArray = (JArray)JTokenList;

            // validate each object
            foreach (JToken obj in JArray.Children<JToken>())
            {
                if (isObject)
                {
                    // validate json object
                    ValidateObject(obj, type, errorList);
                }
                else
                {
                    if (obj != null)
                    {
                        // validate json value
                        MessageBase validationError = FormatValidation(obj, type, null, null);
                        if (validationError != null)
                        {
                            errorList.Add(validationError);
                        }
                    }
                }
            }

            return errorList.Count == 0;
        }

        /// <summary>
        /// Validate JSON object
        /// </summary>
        /// <param name="JTokenObj"></param>
        /// <param name="type"></param>
        /// <param name="errorList"></param>
        /// <returns></returns>
        private List<MessageBase> ValidateObject(JToken JTokenObj, Type type, List<MessageBase> errorList)
        {
            // parse json object
            JObject JObj = (JObject)JTokenObj;

            // get object properties
            PropertyInfo[] sourceProperties = type.GetProperties();

            // check all property on destination object
            foreach (PropertyInfo property in sourceProperties)
            {
                // get json property value based on property on destination object
                JToken propVal = JObj.GetValue(property.Name, StringComparison.OrdinalIgnoreCase);

                // is it a composite object
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    if (propVal == null || propVal.Type == JTokenType.Null)
                    {
                        MessageBase validationError = FormatValidation(propVal, property.PropertyType, property, JObj);
                        if (validationError != null)
                        {
                            errorList.Add(validationError);
                        }
                    }
                    else
                    {
                        ValidateJSON(propVal, property.PropertyType, errorList);
                    }
                }
                else
                {
                    // validate json value
                    MessageBase validationError = FormatValidation(propVal, property.PropertyType, property, JObj);
                    if (validationError != null)
                    {
                        errorList.Add(validationError);
                    }
                }
            }

            return errorList;
        }

        /// <summary>
        /// Validate json value
        /// </summary>
        /// <param name="JData"></param>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <param name="JObj"></param>
        /// <returns></returns>
        private MessageBase FormatValidation(JToken JData, Type type, PropertyInfo property, JToken JObj)
        {
            // init result
            MessageBase result = null;

            // is the data null
            bool isJDataNull = JData == null ? true : JData.Type == JTokenType.Null;

            // get the display name for error message purpose
            string displayName = property != null ? GetDisplayName(property) : "input";
            string propertyName = FieldResource.ResourceManager.GetString(displayName);
            propertyName = string.IsNullOrEmpty(propertyName) ? displayName : propertyName;

            // check if it is mandatory
            if (property.CustomAttributes.Where(att => att.AttributeType == typeof(RequiredAttribute)).Count() > 0)
            {
                // recheck for undefined type, user send the parameter without any value or BLANK
                if (!isJDataNull &&
                    (((JData as JValue).Value == null && JData.Type == JTokenType.Undefined) ||
                    ((JData as JValue).Value != null && (JData as JValue).Value.ToString() == string.Empty && JData.Type == JTokenType.String)))
                {
                    isJDataNull = true;
                }

                if (isJDataNull)
                {
                    // send error if it is mandatory but the data is null
                    return new MessageBase(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, propertyName));
                }

                // check data with type Guid
                if (!isJDataNull && (JData as JValue).Value.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    // get the proper type
                    Type prType = Nullable.GetUnderlyingType(type) ?? type;
                    if (prType.Name == "Guid")
                    {
                        string ErrorMssageGuid = string.Format(MessageResource.ErrorMsgDataRequired, propertyName);
                        ErrorMssageGuid = ErrorMssageGuid + " Tidak dapat menggunakan '00000000-0000-0000-0000-000000000000'";
                        return new MessageBase(ErrorCode.DataRequiredField, ErrorMssageGuid);
                    }
                }
            }

            // return if null
            if (isJDataNull) { return result; }

            // get the proper type
            Type propertyType = Nullable.GetUnderlyingType(type) ?? type;

            // validate the data type
            switch (propertyType.Name)
            {
                case "Int16":
                    result = ValidateDataType<short>(JData, JData.Type != JTokenType.Integer, propertyName, propertyType);
                    break;
                case "UInt16":
                    result = ValidateDataType<ushort>(JData, JData.Type != JTokenType.Integer, propertyName, propertyType);
                    break;
                case "Int32":
                    result = ValidateDataType<int>(JData, JData.Type != JTokenType.Integer, propertyName, propertyType);
                    break;
                case "UInt32":
                    result = ValidateDataType<uint>(JData, JData.Type != JTokenType.Integer, propertyName, propertyType);
                    break;
                case "Int64":
                    result = ValidateDataType<long>(JData, JData.Type != JTokenType.Integer, propertyName, propertyType);
                    break;
                case "UInt64":
                    result = ValidateDataType<ulong>(JData, JData.Type != JTokenType.Integer, propertyName, propertyType);
                    break;
                case "Byte":
                    result = ValidateDataType<byte>(JData, JData.Type != JTokenType.Integer, propertyName, propertyType);
                    break;
                case "SByte":
                    result = ValidateDataType<sbyte>(JData, JData.Type != JTokenType.Integer, propertyName, propertyType);
                    break;
                case "Double":
                    result = ValidateDataType<double>(JData, !(JData.Type == JTokenType.Float || JData.Type == JTokenType.Integer), propertyName, propertyType);
                    break;
                case "Single":
                    result = ValidateDataType<float>(JData, !(JData.Type == JTokenType.Float || JData.Type == JTokenType.Integer), propertyName, propertyType);
                    break;
                case "Decimal":
                    result = ValidateDataType<decimal>(JData, !(JData.Type == JTokenType.Float || JData.Type == JTokenType.Integer), propertyName, propertyType);
                    break;
                case "Boolean":
                    result = ValidateDataType<bool>(JData, JData.Type != JTokenType.Boolean, propertyName, propertyType);
                    break;
                case "DateTime":
                    if (JData.Type != JTokenType.Date && JData.Type != JTokenType.Null)
                    {
                        // ignore validation if passed value is null or empty string
                        if ((JData as JValue).Value.Equals(null) || (JData as JValue).Value.ToString().Equals(string.Empty))
                        {
                            //if ((JData as JValue).Value.ToString().Equals(string.Empty))
                            //{
                            //    string ErrorMssageGuid = string.Format(MessageResource.ErrorMsgDataType, propertyName);
                            //    ErrorMssageGuid = ErrorMssageGuid + " dapat menggunakan null jika data ingin dikosongkan";
                            //    return new MessageBase(ErrorCode.DataRequiredField, ErrorMssageGuid);
                            //}
                            //else
                            //{
                                return result;
                            //}
                        }

                        string dateFormat = GetDateFormat(property);

                        if (JData.Type == JTokenType.String)
                        {
                            DateTime dt;
                            string[] formats = { dateFormat };
                            if (DateTime.TryParseExact(JData.ToString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                            {
                                return result;
                            }
                        }

                        return new MessageBase(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgInvalidDataTypeFormat, propertyName, "Date (" + dateFormat + ")"));
                    }
                    break;
                case "String":
                    result = ValidateDataType<string>(JData, JData.Type != JTokenType.String, propertyName, propertyType);
                    break;
                case "Char":
                    result = ValidateDataType<char>(JData, JData.Type != JTokenType.String, propertyName, propertyType);
                    break;
                case "Guid":
                    Guid x;
                    bool isGuid = Guid.TryParse(JData.ToString(), out x);

                    if (isGuid)
                    {
                        result = null;
                    }
                    else
                    {
                        result = ValidateDataType<Guid>(JData, isGuid, propertyName, propertyType);
                    }
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Validate data type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JData"></param>
        /// <param name="isJDataTypNotMatch"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        private MessageBase ValidateDataType<T>(JToken JData, bool isJDataTypNotMatch, string propertyName, Type propertyType)
        {
            if (isJDataTypNotMatch && JData.Type != JTokenType.Null)
            {
                return new MessageBase(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(MessageResource.ErrorMsgInvalidDataTypeFormat, propertyName, propertyType.Name));
            }
            else
            {
                try
                {
                    T val = JData.Value<T>();
                    return null;
                }
                catch
                {
                    return new MessageBase(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(MessageResource.ErrorMsgInvalidDataTypeFormat, propertyName, propertyType.Name));
                }
            }
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

        /// <summary>
        /// Get DisplayName from a property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private string GetDisplayName(PropertyInfo property)
        {
            // return empty if the property is null
            if (property == null)
            {
                return string.Empty;
            }

            try
            {
                // get display attribute
                var atts = property.GetCustomAttributes(
                typeof(DisplayAttribute), true);

                // return property name if display attribute is not exist
                if (atts.Length == 0)
                { return property.Name; }

                // return display attribute name
                return (atts[0] as DisplayAttribute).Name;
            }
            catch
            {
                // return property name
                return property.Name;
            }
        }

        /// <summary>
        /// Get date format from a property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private string GetDateFormat(PropertyInfo property)
        {
            // return empty if the property is null
            if (property == null)
            {
                return Constants.DATE_DEFAULT_FORMAT;
            }

            try
            {
                // get display attribute
                var atts = property.GetCustomAttributes(typeof(DisplayFormatAttribute), true);

                // return property name if display attribute is not exist
                if (atts.Length == 0) { return Constants.DATE_DEFAULT_FORMAT; }

                // return display attribute name
                return (atts[0] as DisplayFormatAttribute).DataFormatString;
            }
            catch
            {
                // return property name
                return Constants.DATE_DEFAULT_FORMAT;
            }
        }
        #endregion
    }
}

