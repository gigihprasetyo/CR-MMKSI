#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : OCRIdentity business logic class
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
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class OCRIdentityBL : AbstractBusinessLogic, IOCRIdentityBL
    {
        #region Variables
        string proxyAddress;
        string proxyPort;
        string ocrServer;
        bool isProxyUsed;
        #endregion

        #region Constructor
        public OCRIdentityBL()
        {
            // get the config
            proxyAddress = ConfigurationManager.AppSettings["ProxyAddress"];
            proxyPort = ConfigurationManager.AppSettings["ProxyPort"];
            ocrServer = ConfigurationManager.AppSettings["OCRServer"];
            isProxyUsed = ConvertToBoolean(ConfigurationManager.AppSettings["IsProxyUsed"]);

            // set to false
            ServicePointManager.Expect100Continue = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Upload OCR File to server
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        public ResponseBase<OCRIdentityKTPDto> UploadKTP(HttpPostedFile postedFile)
        {
            // define result
            var result = new ResponseBase<OCRIdentityKTPDto>();

            try
            {
                // get server 
                string uploadServer = ocrServer + "/Ktp/upload";

                // save the posted file
                string imagePath = FileUtility.SaveOCRFileToLocal(postedFile);
                if (string.IsNullOrEmpty(imagePath))
                {
                    result.messages = new List<MessageBase> { new MessageBase { ErrorCode = ErrorCode.DBSaveFailed, ErrorMessage = MessageResource.ErrorMsgSavePostedFileFailed } };
                    return result;
                }

                // convert 
                using (WebClient client = new WebClient())
                {
                    // set the proxy
                    if (isProxyUsed)
                        client.Proxy = new WebProxy(proxyAddress, int.Parse(proxyPort));

                    // set the header
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");

                    // call the API
                    byte[] value = client.UploadFile(uploadServer, imagePath);

                    // convert to json
                    String textjson = System.Text.Encoding.ASCII.GetString(value);

                    // convert to dto object
                    OCRKTPUploadDto data = JsonConvert.DeserializeObject<OCRKTPUploadDto>(textjson);

                    // return
                    result.success = data.success;
                    result.lst = data.data;
                }

                // delete the image file
                FileUtility.DeleteOCRLocalFile(imagePath);
            }
            catch (Exception ex)
            {
                result.messages = new List<MessageBase> { new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = ex.Message } };
            }

            return result;
        }

        /// <summary>
        /// Get KTP OCR progress status
        /// </summary>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        public ResponseBase<OCRResultValueDto> ProgressKTP(string uploadID)
        {
            // define result
            var result = new ResponseBase<OCRResultValueDto>();

            try
            {
                // get server 
                string progressServer = ocrServer + "/Ktp/" + uploadID + "/progress";

                // convert 
                using (WebClient client = new WebClient())
                {
                    // set the proxy
                    if (isProxyUsed)
                        client.Proxy = new WebProxy(proxyAddress, int.Parse(proxyPort));

                    // set the header
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");

                    // call the API
                    Stream streamValue = client.OpenRead(progressServer);
                    StreamReader reader = new StreamReader(streamValue);
                    string textjson = reader.ReadToEnd();

                    // convert to dto object
                    OCRResultValueDto data = JsonConvert.DeserializeObject<OCRResultValueDto>(textjson);

                    // return
                    result.success = data.Value.Equals("100");
                    result.lst = data;
                }
            }
            catch
            {
                result.messages = new List<MessageBase> { new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = string.Format(MessageResource.ErrorMsgUploadIDInvalid, uploadID) } };
            }

            return result;
        }

        /// <summary>
        /// Get KTP OCR Data
        /// </summary>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        public ResponseBase<OCRKTPDataDto> DataKTP(string uploadID)
        {
            var result = new ResponseBase<OCRKTPDataDto>();

            try
            {
                // get server 
                string progressServer = ocrServer + "/Ktp/" + uploadID + "/data";

                // convert 
                using (WebClient client = new WebClient())
                {
                    // set the proxy
                    if (isProxyUsed)
                        client.Proxy = new WebProxy(proxyAddress, int.Parse(proxyPort));

                    // set the header
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");

                    // call the API
                    Stream streamValue = client.OpenRead(progressServer);
                    StreamReader reader = new StreamReader(streamValue);
                    string textjson = reader.ReadToEnd();

                    // convert to dto object
                    OCRKTPDataDto data = JsonConvert.DeserializeObject<OCRKTPDataDto>(textjson);

                    // return
                    result.success = true;
                    result.lst = data;
                }
            }
            catch
            {
                result.messages = new List<MessageBase> { new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = string.Format(MessageResource.ErrorMsgUploadIDInvalid, uploadID) } };
            }

            return result;
        }

        /// <summary>
        /// Upload SIM OCR to server
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        public ResponseBase<OCRIdentitySIMDto> UploadSIM(HttpPostedFile postedFile)
        {
            // define result
            var result = new ResponseBase<OCRIdentitySIMDto>();

            try
            {
                // get server 
                string uploadServer = ocrServer + "/Sim/upload";

                // save the posted file
                string imagePath = FileUtility.SaveOCRFileToLocal(postedFile);
                if (string.IsNullOrEmpty(imagePath))
                {
                    result.messages = new List<MessageBase> { new MessageBase { ErrorCode = ErrorCode.DBSaveFailed, ErrorMessage = MessageResource.ErrorMsgSavePostedFileFailed } };
                    return result;
                }

                // convert 
                using (WebClient client = new WebClient())
                {
                    // set the proxy
                    if (isProxyUsed)
                        client.Proxy = new WebProxy(proxyAddress, int.Parse(proxyPort));

                    // set the header
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");

                    // call the API
                    byte[] value = client.UploadFile(uploadServer, imagePath);

                    // convert to json
                    String textjson = System.Text.Encoding.ASCII.GetString(value);

                    // convert to dto object
                    OCRSIMUploadDto data = JsonConvert.DeserializeObject<OCRSIMUploadDto>(textjson);

                    // return
                    result.success = data.success;
                    result.lst = data.data;
                }

                // delete the image file
                FileUtility.DeleteOCRLocalFile(imagePath);
            }
            catch (Exception ex)
            {
                result.messages = new List<MessageBase> { new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = ex.Message } };
            }

            return result;
        }

        /// <summary>
        /// Check SIM OCR Status
        /// </summary>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        public ResponseBase<OCRResultValueDto> ProgressSIM(string uploadID)
        {
            var result = new ResponseBase<OCRResultValueDto>();

            try
            {
                // get server 
                string progressServer = ocrServer + "/Sim/" + uploadID + "/progress";

                // convert 
                using (WebClient client = new WebClient())
                {
                    // set the proxy
                    if (isProxyUsed)
                        client.Proxy = new WebProxy(proxyAddress, int.Parse(proxyPort));

                    // set the header
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");

                    // call the API
                    Stream streamValue = client.OpenRead(progressServer);
                    StreamReader reader = new StreamReader(streamValue);
                    string textjson = reader.ReadToEnd();

                    // convert to dto object
                    OCRResultValueDto data = JsonConvert.DeserializeObject<OCRResultValueDto>(textjson);

                    // return
                    result.success = data.Value.Equals("100");
                    result.lst = data;
                }
            }
            catch
            {
                result.messages = new List<MessageBase> { new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = string.Format(MessageResource.ErrorMsgUploadIDInvalid, uploadID) } };
            }

            return result;
        }

        /// <summary>
        /// Get SIM OCR Data
        /// </summary>
        /// <param name="uploadID"></param>
        /// <returns></returns>
        public ResponseBase<OCRSIMDataDto> DataSIM(string uploadID)
        {
            var result = new ResponseBase<OCRSIMDataDto>();

            try
            {
                // get server 
                string progressServer = ocrServer + "/Sim/" + uploadID + "/data";

                // convert 
                using (WebClient client = new WebClient())
                {
                    // set the proxy
                    if (isProxyUsed)
                        client.Proxy = new WebProxy(proxyAddress, int.Parse(proxyPort));

                    // set the header
                    client.Headers.Add(HttpRequestHeader.Accept, "application/json");

                    // call the API
                    Stream streamValue = client.OpenRead(progressServer);
                    StreamReader reader = new StreamReader(streamValue);
                    string textjson = reader.ReadToEnd();

                    // convert to dto object
                    OCRSIMDataDto data = JsonConvert.DeserializeObject<OCRSIMDataDto>(textjson);

                    // return
                    result.success = true;
                    result.lst = data;
                }
            }
            catch
            {
                result.messages = new List<MessageBase> { new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = string.Format(MessageResource.ErrorMsgUploadIDInvalid, uploadID) } };
            }

            return result;
        }

        #endregion

        #region Private Method
        /// <summary>
        /// Convert to Boolean
        /// </summary>
        /// <param name="boolean"></param>
        /// <returns></returns>
        private bool ConvertToBoolean(string boolean)
        {
            return boolean.Equals("true", StringComparison.OrdinalIgnoreCase);
        }
        #endregion
    }
}

