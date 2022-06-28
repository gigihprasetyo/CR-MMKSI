#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : JsonHelper  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad Ridwan - Initial
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Newtonsoft.Json.Linq;
using System;
#endregion


namespace KTB.DNet.Interface.Framework
{
    public static class JsonHelper
    {
        /// <summary>
        /// SetProperty
        /// </summary>
        /// <param name="jToken"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public static void SetProperty(JToken jToken, string propertyName, object propertyValue)
        {
            if (jToken != null)
            {
                if (jToken.Type == JTokenType.Array)
                {
                    SetPropertyOnArrayOfObject(jToken, propertyName, propertyValue);

                }
                else if (jToken.Type == JTokenType.Object)
                {
                    SetPropertyObject(jToken, propertyName, propertyValue);
                }
            }
        }

        /// <summary>
        /// SetPropertyOnArrayOfObject
        /// </summary>
        /// <param name="jToken"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        private static void SetPropertyOnArrayOfObject(JToken jToken, string propertyName, object propertyValue)
        {
            if (jToken != null)
            {
                JArray JArray = (JArray)jToken;

                foreach (JToken obj in JArray.Children<JToken>())
                {
                    if (obj.Type == JTokenType.Array)
                    {
                        SetPropertyOnArrayOfObject(obj, propertyName, propertyValue);
                    }
                    else if (obj.Type == JTokenType.Object)
                    {
                        SetPropertyObject(obj, propertyName, propertyValue);
                    }
                }
            }
        }

        /// <summary>
        /// SetPropertyObject
        /// </summary>
        /// <param name="jToken"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        private static void SetPropertyObject(JToken jToken, string propertyName, object propertyValue)
        {
            if (jToken != null)
            {
                JObject obj = (JObject)jToken;
                foreach (JProperty prop in obj.Properties())
                {
                    if (obj[prop.Name] != null)
                    {
                        if (prop.Name == propertyName)
                        {
                            obj[prop.Name] = GetTokenValue(obj[prop.Name], propertyValue);
                        }
                        else
                        {
                            if (obj[prop.Name].Type == JTokenType.Array)
                            {
                                SetPropertyOnArrayOfObject(obj[prop.Name], propertyName, propertyValue);
                            }
                            else if (obj[prop.Name].Type == JTokenType.Object)
                            {
                                SetPropertyObject(obj[prop.Name], propertyName, propertyValue);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// GetTokenValue
        /// </summary>
        /// <param name="jToken"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        private static JToken GetTokenValue(JToken jToken, object propertyValue)
        {
            if (jToken != null && propertyValue != null)
            {
                try
                {
                    switch (propertyValue.GetType().Name)
                    {
                        case "Int16":
                            return JToken.FromObject((short)Convert.ChangeType(propertyValue, typeof(short)));
                        case "UInt16":
                            return JToken.FromObject((ushort)Convert.ChangeType(propertyValue, typeof(ushort)));
                        case "Int32":
                            return JToken.FromObject((int)Convert.ChangeType(propertyValue, typeof(int)));
                        case "UInt32":
                            return JToken.FromObject((uint)Convert.ChangeType(propertyValue, typeof(uint)));
                        case "Int64":
                            return JToken.FromObject((long)Convert.ChangeType(propertyValue, typeof(long)));
                        case "UInt64":
                            return JToken.FromObject((ulong)Convert.ChangeType(propertyValue, typeof(ulong)));
                        case "Byte":
                            return JToken.FromObject((byte)Convert.ChangeType(propertyValue, typeof(byte)));
                        case "SByte":
                            return JToken.FromObject((sbyte)Convert.ChangeType(propertyValue, typeof(sbyte)));
                        case "Double":
                            return JToken.FromObject((double)Convert.ChangeType(propertyValue, typeof(double)));
                        case "Single":
                            return JToken.FromObject((float)Convert.ChangeType(propertyValue, typeof(float)));
                        case "Decimal":
                            return JToken.FromObject((decimal)Convert.ChangeType(propertyValue, typeof(decimal)));
                        case "Boolean":
                            return JToken.FromObject((bool)Convert.ChangeType(propertyValue, typeof(bool)));
                        case "DateTime":
                            return JToken.FromObject((DateTime)Convert.ChangeType(propertyValue, typeof(DateTime)));
                        case "String":
                            return JToken.FromObject((string)Convert.ChangeType(propertyValue, typeof(string)));
                        case "Char":
                            return JToken.FromObject((char)Convert.ChangeType(propertyValue, typeof(char)));
                        case "Guid":
                            return JToken.FromObject((string)Convert.ChangeType(propertyValue, typeof(string)));
                        default:
                            return JToken.FromObject((string)Convert.ChangeType(propertyValue.ToString(), typeof(string)));
                    }
                }
                catch (Exception ex)
                {
                    // do nothing
                }
            }

            return null;
        }

    }
}
