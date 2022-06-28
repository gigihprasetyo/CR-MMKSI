#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomJsonFormatter class
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper
{
    /// <summary>
    /// Author          : Muhamad Ridwan
    /// Created On      : April, 2018
    /// 
    /// PT Mitrais
    /// 
    /// </summary>
    public class CustomJsonFormatter : JsonMediaTypeFormatter
    {
        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomJsonFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("json/application"));
        }
        #endregion

        /// <summary>
        /// Can write type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override bool CanWriteType(Type type)
        {
            // don't serialize JsonValue structure use default for that
            if (type == typeof(JObject) || type == typeof(JValue) || type == typeof(JArray))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Can read type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override bool CanReadType(Type type)
        {
            // default : allow any type
            // modify if only specified type allowed
            return true;
        }

        /// <summary>
        /// Read from stream async
        /// </summary>
        /// <param name="type"></param>
        /// <param name="readStream"></param>
        /// <param name="content"></param>
        /// <param name="formatterLogger"></param>
        /// <returns></returns>
        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var task = Task<object>.Factory.StartNew(() =>
            {
                try
                {
                    var ser = new JavaScriptSerializer();
                    string json;

                    // read data from stream request
                    using (var sr = new StreamReader(readStream))
                    {
                        json = sr.ReadToEnd();
                    }

                    // parse data to json
                    JToken jToken = JToken.Parse(json);

                    // set json default value
                    SetJsonDefaultValue(jToken, type);

                    // deserialize object
                    // get method to deserialize object from JsonConvert
                    var methods = from m in typeof(JsonConvert).GetMethods()
                                  where m.Name == "DeserializeObject"
                                  && m.IsGenericMethodDefinition
                                  let normalParams = m.GetParameters()
                                  where normalParams.Length == 1
                                  select m;

                    MethodInfo mi = methods.Single();
                    MethodInfo miConstructed = mi.MakeGenericMethod(type);
                    object[] args = { jToken.ToString() };

                    // deserialize object
                    var val = miConstructed.Invoke(null, args);
                    return val;
                }
                catch
                {
                    return base.ReadFromStreamAsync(type, readStream, content, formatterLogger).Result;
                }
            });

            return task;
        }

        /// <summary>
        /// Set json default value
        /// </summary>
        private void SetJsonDefaultValue(JToken jToken, Type type)
        {
            if (jToken.Type == JTokenType.Array)
            {
                // set default value of json array
                SetDefaultPropertyValueOnList(jToken, type);
            }
            else
            {
                // set default value of json object
                SetDefaultPropertyValueOnObject(jToken, type);
            }
        }

        /// <summary>
        /// Set default property value on list
        /// </summary>
        /// <param name="JTokenList"></param>
        /// <param name="type"></param>
        private void SetDefaultPropertyValueOnList(JToken JTokenList, Type type)
        {
            bool isObject = type.IsClass && type != typeof(string);

            // get the underlying type 
            var propType = type.GetProperty("Item");
            if (propType != null)
                type = propType.PropertyType;

            // parse json array
            JArray JArray = (JArray)JTokenList;

            foreach (JToken obj in JArray.Children<JToken>())
            {
                if (isObject)
                {
                    // default of the object
                    SetDefaultPropertyValueOnObject(obj, type);
                }
                else
                {
                    if (obj != null)
                    {
                        // set default value
                        SetDefaultPropertyValue(obj, type, null, null);
                    }
                }
            }
        }

        /// <summary>
        /// Set default property value on object
        /// </summary>
        /// <param name="JTokenObj"></param>
        /// <param name="type"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        private void SetDefaultPropertyValueOnObject(JToken JTokenObj, Type type)
        {
            // cast jtoken to jobject
            JObject JObj = (JObject)JTokenObj;

            // get properties
            PropertyInfo[] sourceProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in sourceProperties)
            {
                // get property value from json
                JToken propVal = JObj.GetValue(property.Name, StringComparison.OrdinalIgnoreCase);
                try
                {
                    // check if the property is object
                    if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    {
                        if (propVal == null || propVal.Type == JTokenType.Null)
                        {
                            // set default property value
                            SetDefaultPropertyValue(propVal, property.PropertyType, property, JObj);
                        }
                        else
                        {
                            string validationMessage = string.Empty;
                            if (property.PropertyType.IsArray || property.PropertyType.Name.ToUpper().Contains("LIST"))
                            {
                                // set default property value on list
                                SetDefaultPropertyValueOnList(propVal, property.PropertyType.GenericTypeArguments[0]);
                            }
                            else
                            {
                                // set default property value on object
                                SetDefaultPropertyValueOnObject(propVal, property.PropertyType);
                            }
                        }
                    }
                    else
                    {
                        // set default property value
                        SetDefaultPropertyValue(propVal, property.PropertyType, property, JObj);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

            }

        }

        /// <summary>
        /// Set default property value
        /// </summary>
        /// <param name="JData"></param>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <param name="JObj"></param>
        private void SetDefaultPropertyValue(JToken JData, Type type, PropertyInfo property, JToken JObj)
        {
            bool isJDataNull = JData == null ? true : JData.Type == JTokenType.Null;

            Type underlyingTypeOfNullableType = Nullable.GetUnderlyingType(type);
            bool isNullableType = underlyingTypeOfNullableType != null;
            Type propertyType = isNullableType ? underlyingTypeOfNullableType : type;

            if (isJDataNull)
            {
                if (!isNullableType)
                {
                    if (JObj != null)
                    {
                        switch (propertyType.Name)
                        {
                            case "Int16":
                                SetDefaultValue<short>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "UInt16":
                                SetDefaultValue<ushort>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "Int32":
                                SetDefaultValue<int>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "UInt32":
                                SetDefaultValue<uint>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "Int64":
                                SetDefaultValue<long>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "UInt64":
                                SetDefaultValue<ulong>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "Byte":
                                SetDefaultValue<byte>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "SByte":
                                SetDefaultValue<sbyte>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "Double":
                                SetDefaultValue<double>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "Single":
                                SetDefaultValue<float>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "Decimal":
                                SetDefaultValue<decimal>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.NUMBER_DEFAULT_VALUE),
                                    Constants.NUMBER_DEFAULT_VALUE
                                );
                                break;
                            case "Boolean":
                                SetDefaultValue<bool>(
                                    JObj,
                                    property,
                                    (bool)GetDefaultPropertyValue(property, Constants.BOOL_DEFAULT_VALUE),
                                    Constants.BOOL_DEFAULT_VALUE
                                );
                                break;
                            case "DateTime":
                                JObj[property.Name] = Constants.DATETIME_DEFAULT_VALUE.ToString(Constants.DATE_DEFAULT_FORMAT);
                                break;
                            case "String":
                                SetDefaultValue<string>(
                                    JObj,
                                    property,
                                    GetDefaultPropertyValue(property, Constants.STRING_DEFAULT_VALUE),
                                    Constants.STRING_DEFAULT_VALUE
                                );
                                break;
                            case "Char":
                                JObj[property.Name] = default(char);
                                break;
                            case "Guid":
                                JObj[property.Name] = default(Guid);
                                break;
                            default:
                                if (isJDataNull && JObj != null)
                                {
                                    JObj[property.Name] = null;
                                }
                                break;
                        }
                    }
                }
            }
            else
            {
                if (propertyType.Name == "DateTime")
                {
                    if (JData.Type != JTokenType.Date && JData.Type != JTokenType.Null)
                    {
                        string dateFormat = GetDateFormat(property);

                        if (JData.Type == JTokenType.String)
                        {
                            DateTime dt;
                            string[] formats = { dateFormat };
                            if (DateTime.TryParseExact(JData.ToString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                            {
                                // if the date < default value, set the date with default value
                                dt = dt < Constants.DATETIME_DEFAULT_VALUE ? Constants.DATETIME_DEFAULT_VALUE : dt;

                                JObj[property.Name] = dt.ToString(Constants.JSON_DATE_DEFAULT_FORMAT);
                            }
                            else
                            {
                                JObj[property.Name] = Constants.DATETIME_DEFAULT_VALUE.ToString(Constants.JSON_DATE_DEFAULT_FORMAT);
                            }
                        }
                        else
                        {
                            JObj[property.Name] = Constants.DATETIME_DEFAULT_VALUE.ToString(Constants.JSON_DATE_DEFAULT_FORMAT);
                        }
                    }
                }
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
                var atts = property.GetCustomAttributes(
                typeof(DisplayFormatAttribute), true);

                // return property name if display attribute is not exist
                if (atts.Length == 0)
                { return Constants.DATE_DEFAULT_FORMAT; }

                // return display attribute name
                return (atts[0] as DisplayFormatAttribute).DataFormatString;
            }
            catch
            {
                // return property name
                return Constants.DATE_DEFAULT_FORMAT;
            }
        }

        /// <summary>
        /// Get default property value
        /// </summary>
        /// <param name="property"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private object GetDefaultPropertyValue(PropertyInfo property, object defaultValue)
        {
            // return empty if the property is null
            if (property == null)
            {
                return defaultValue;
            }

            try
            {
                // get default value attribute
                var atts = property.GetCustomAttributes(
                typeof(DefaultValueAttribute), true);

                // return property name if display attribute is not exist
                if (atts.Length == 0)
                { return defaultValue; }

                DefaultValueAttribute defaultValAttr = atts[0] as DefaultValueAttribute;

                // return display attribute name
                return (atts[0] as DefaultValueAttribute).Value;
            }
            catch
            {
                // return property name
                return defaultValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JObj"></param>
        /// <param name="property"></param>
        /// <param name="defaultValue"></param>
        /// <param name="appDefaultValue"></param>
        private void SetDefaultValue<T>(JToken JObj, PropertyInfo property, object defaultValue, object appDefaultValue)
        {
            try
            {
                // try to get default value from default value
                JObj[property.Name] = defaultValue == null ? null : JToken.FromObject((T)Convert.ChangeType(defaultValue, typeof(T)));
            }
            catch
            {
                try
                {
                    // try to get default value from application default value
                    JObj[property.Name] = appDefaultValue == null ? null : JToken.FromObject((T)Convert.ChangeType(appDefaultValue, typeof(T)));
                }
                catch
                {
                    // set value with default value of type
                    JObj[property.Name] = default(T) == null ? null : JToken.FromObject(default(T));
                }
            }
        }
    }
}