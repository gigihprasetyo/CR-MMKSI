#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Utils class
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
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
#endregion

namespace KTB.DNet.Interface.WebUI.Helper
{
    public static class Utils
    {
        #region Method Unix Time to Date Time
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static string UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string dateString = dtDateTime.AddSeconds(unixTimeStamp / 1000).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss");
            return dateString;
            //return dtDateTime.ToLongDateString() + " " + dtDateTime.ToLongTimeString();
        }
        #endregion

        /// <summary>
        /// Format Endpoint uri
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static string GetShortEndpoint(string endpoint)
        {
            try
            {
                string result = endpoint;
                if (endpoint.Contains(".mitsubishi-motors.co.id"))
                {

                    string server = "";
                    if (endpoint.Substring(0, 8) == "https://")
                    {
                        server = endpoint.Substring("https://".Length, endpoint.IndexOf(".mitsubishi-motors.co.id") - "https://".Length);
                    }
                    else if (endpoint.Substring(0, 7) == "http://")
                    {
                        server = endpoint.Substring("http://".Length, endpoint.IndexOf(".mitsubishi-motors.co.id") - "http://".Length);
                    }

                    if (endpoint.Contains("WebApi"))
                    {
                        endpoint = endpoint.Substring(endpoint.IndexOf("/WebApi/") + "/WebApi/".Length);
                        result = "[" + server + "] - " + endpoint;
                    }
                    else if (endpoint.Contains("WebAPI"))
                    {
                        endpoint = endpoint.Substring(endpoint.IndexOf("/WebAPI/") + "/WebAPI/".Length);
                        result = "[" + server + "] - " + endpoint;
                    }
                    else if (endpoint.Contains("DMSApi"))
                    {
                        endpoint = endpoint.Substring(endpoint.IndexOf("/DMSApi/") + "/DMSApi/".Length);
                        result = "[" + server + "-DMS] - " + endpoint;
                    }
                    else if (endpoint.Contains("UATApi"))
                    {
                        endpoint = endpoint.Substring(endpoint.IndexOf("/UATApi/") + "/UATApi/".Length);
                        result = "[" + server + "-UAT] - " + endpoint;
                    }
                }
                else if (endpoint.Contains("localhost"))
                {
                    endpoint = endpoint.Substring(endpoint.IndexOf("/MMKSIWebApi/") + "/MMKSIWebApi/".Length);
                    result = "[localhost] - " + endpoint;
                }

                return result;
            }
            catch
            {
                return endpoint;
            }
        }

        /// <summary>
        /// Extract ouput to get the error message
        /// </summary>
        /// <param name="output"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string GetErrorMessages(string output, int maxLength)
        {
            try
            {
                // define the list
                List<string> outputMessages = new List<string>();

                // split the output just in case it has multiple error messages
                string[] outputs = output.Split(new string[] { "},{" }, StringSplitOptions.None);

                // proceed each output
                foreach (var item in outputs)
                {
                    // set the regex
                    var match = Regex.Match(item, "\"ErrorMessage\":(.*),\"ErrorCode\"");
                    if (match.Success)
                        outputMessages.Add(match.Groups[1].Value);
                    else
                        outputMessages.Add(item);
                }

                // populate the messages
                string errorMessages = String.Join(", ", outputMessages.ToArray());

                // substring the error message based on its max length config
                errorMessages = errorMessages.Length <= maxLength ? errorMessages : errorMessages.Substring(0, maxLength) + "...";

                return errorMessages;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}