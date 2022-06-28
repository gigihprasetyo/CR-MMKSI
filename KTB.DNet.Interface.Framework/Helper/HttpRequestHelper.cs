#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : HttpRequestHelper  class
// SPECIAL NOTES : DNet WebUI Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad Ridwan - Initial
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace KTB.DNet.Interface.Framework
{
    public static class HttpRequestHelper
    {
        private static int DEFAULT_TIMEOUT = 3600000; // in miliseconds

        /// <summary>
        /// Request Json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ResponseMessage<JToken> RequestJson(string url, JToken data, RequestMethod method, Dictionary<string, string> additionalHeaders = null)
        {
            return RequestJson(url, data, method, DEFAULT_TIMEOUT, additionalHeaders);
        }

        /// <summary>
        /// Request Json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="timeout"></param>
        /// <param name="additionalHeaders"></param>
        /// <returns></returns>
        public static ResponseMessage<JToken> RequestJson(string url, JToken data, RequestMethod method, int timeout, Dictionary<string, string> additionalHeaders = null)
        {
            ResponseMessage<string> response = SendRequest(url, data.ToString(), method, "application/json", timeout, additionalHeaders);
            ResponseMessage<JToken> result = new ResponseMessage<JToken>();
            result.Success = response.Success;
            result.Status = response.Status;
            result.Message = response.Message;

            if (result.Success)
            {
                result.Data = ParseResponseToJson(response.Data);
            }
            return result;
        }

        /// <summary>
        /// Request Xml
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static ResponseMessage<XmlDocument> RequestXml(string url, string data, RequestMethod method, Dictionary<string, string> additionalHeaders = null)
        {
            return RequestXml(url, data, method, DEFAULT_TIMEOUT, additionalHeaders);
        }

        /// <summary>
        /// Request Xml
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="timeout"></param>
        /// <param name="additionalHeaders"></param>
        /// <returns></returns>
        public static ResponseMessage<XmlDocument> RequestXml(string url, string data, RequestMethod method, int timeout, Dictionary<string, string> additionalHeaders = null)
        {
            ResponseMessage<string> response = SendRequest(url, data, method, "application/xml", timeout, additionalHeaders);
            ResponseMessage<XmlDocument> result = new ResponseMessage<XmlDocument>();
            result.Success = response.Success;
            result.Status = response.Status;

            if (result.Success)
            {
                result.Data = ParseResponseToXml(response.Data);
            }
            return result;
        }

        /// <summary>
        /// Send request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        /// <param name="timeout"></param>
        /// <param name="additionalHeaders"></param>
        /// <returns></returns>
        private static ResponseMessage<string> SendRequest(string url, string data, RequestMethod method, string contentType, int timeout = 0, Dictionary<string, string> additionalHeaders = null)
        {
            ResponseMessage<string> response = new ResponseMessage<string>();
            try
            {
                var reqData = Encoding.ASCII.GetBytes(data);

                HttpWebRequest request = HttpWebRequest.CreateHttp(url);
                request.Method = method.ToString();
                request.AllowAutoRedirect = true;
                request.AllowWriteStreamBuffering = true;
                request.ContentLength = reqData.Length;
                request.ContentType = contentType;
                request.Timeout = timeout == 0 ? DEFAULT_TIMEOUT : timeout;

                request = SetAdditionalHeaders(request, additionalHeaders);

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(reqData, 0, reqData.Length);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = webResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream, Encoding.Default))
                        {
                            response.Success = true;
                            response.Status = ResponseStatus.Success;
                            response.Data = reader.ReadToEnd();
                        }
                    }
                }

                return response;
            }
            catch (WebException e)
            {
                try
                {
                    response.Success = false;
                    response.Status = ResponseStatus.Error;
                    if (e.Response == null)
                    {
                        response.Message = string.Format("There's no response from server. {0}", e.Message);
                        return response;
                    }

                    using (WebResponse webResponse = e.Response)
                    {
                        HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
                        using (Stream responseStream = httpWebResponse.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(responseStream, Encoding.Default))
                            {
                                response.Success = true;
                                response.Status = ResponseStatus.Success;
                                response.Data = reader.ReadToEnd();
                            }
                        }
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Status = ResponseStatus.Error;
                    response.Message = string.Format("Failed to read response stream. {0}", e.Message);
                    return response;
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = ResponseStatus.Error;
                response.Message = e.Message;
                return response;
            }
        }

        /// <summary>
        /// Set additional headers
        /// </summary>
        /// <param name="httpWebRequest"></param>
        /// <param name="additionalHeaders"></param>
        /// <returns></returns>
        private static HttpWebRequest SetAdditionalHeaders(HttpWebRequest httpWebRequest, Dictionary<string, string> additionalHeaders)
        {
            if (additionalHeaders != null)
            {
                foreach (KeyValuePair<string, string> header in additionalHeaders)
                {
                    httpWebRequest.Headers[header.Key] = header.Value;
                }
            }

            return httpWebRequest;
        }

        /// <summary>
        /// Parse to Json
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static JToken ParseResponseToJson(string response)
        {
            return JToken.Parse(response);
        }

        /// <summary>
        /// Parse to Xml
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static XmlDocument ParseResponseToXml(string response)
        {
            XmlDocument xmlResponse = new XmlDocument();
            xmlResponse.LoadXml(response);

            return xmlResponse;
        }

        /// <summary>
        /// Request Method
        /// </summary>
        public enum RequestMethod
        {
            POST,
            GET
        }
    }
}
