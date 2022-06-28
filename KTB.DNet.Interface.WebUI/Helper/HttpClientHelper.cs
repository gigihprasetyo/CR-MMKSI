#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : HttpClientHelper.cs class
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
using KTB.DNet.Interface.WebUI.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
#endregion

namespace KTB.DNet.Interface.WebUI.Helper
{
    /// <summary>
    /// Helper class to handle resend feature using httpclient
    /// </summary>
    public static class HttpClientHelper
    {

        /// <summary>
        /// Re-Send the input to API
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        public static void SendRequest(APIUser user, ref TransactionLogViewModel model)
        {
            // replace with localhost address
            string endPoint = model.Endpoint.Replace("qa-interface.mitsubishi-motors.co.id", "localhost");

            // extract the end point
            string baseEndPoint = endPoint.Substring(0, endPoint.IndexOf("WebApi/") + "WebApi/".Length);
            string moduleEndPoint = endPoint.Substring(baseEndPoint.Length);

            // get Client User
            //APIClientUser clientUser = _clientUserRepository.

            // initialize the httpclient
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseEndPoint);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + user.);

            try
            {
                // convert the iput
                var content = new StringContent(model.Input, Encoding.UTF8, "application/json");

                // Accept all server certificate
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                // call the API
                HttpResponseMessage response = client.PostAsync(moduleEndPoint, content).Result;

                // check the result
                if (response.IsSuccessStatusCode)
                {
                    model.Output = response.Content.ReadAsStringAsync().Result;
                    model.Status = true;
                }
                else
                {
                    model.Output = response.Content.ReadAsStringAsync().Result;
                    model.Status = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                model.Output = string.IsNullOrEmpty(ex.Message) ? ex.InnerException.Message : ex.Message; ;
                model.Status = false;
            }
        }
    }
}