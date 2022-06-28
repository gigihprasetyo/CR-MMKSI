using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;
using KTB.DNet.Parser;
using Microsoft.ProjectServer.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace KTB.DNet.WebApi.Models.MiddlewareSOA
{
    public static class MiddlewareSOA
    {
        public static string Message = string.Empty;
        public static bool IsSuccess = false;
        public static string token = string.Empty;
        public static string refreshtoken = string.Empty;

        public static void SignIn(System.Security.Principal.IPrincipal User)
        {
            try
            {
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);
                string userName = appConfigFacade.Retrieve("soa_username").Value;
                string password = appConfigFacade.Retrieve("soa_password").Value;
                string url = appConfigFacade.Retrieve("soa_url").Value + "/auth/signin";

                HttpClientHandler handler = new HttpClientHandler();
                String strWebProxy = appConfigFacade.Retrieve("wssf_webproxy").Value;
                if (strWebProxy.Length > 0)
                {
                    handler = new HttpClientHandler
                    {
                        UseDefaultCredentials = false,
                        Proxy = new WebProxy(strWebProxy, true, new string[] { }),
                        UseProxy = true,
                    };
                }

                HttpClient client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(new { username = userName, password = password }), Encoding.UTF8, "application/json");
                content.Headers.Add("company", "bsi");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Message = String.Empty; IsSuccess = false;
                var result = client.PostAsync(url, content).Result;

                if (result.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception(HttpStatusCode.InternalServerError.ToString());

                var response = result.Content.ReadAsStringAsync();
                var obj = JObject.Parse(response.Result);

                if (result.IsSuccessStatusCode)
                {
                    token = obj["id_token"].ToString();
                    var conf = appConfigFacade.Retrieve("soa_token");
                    conf.Value = token;
                    appConfigFacade.Update(conf);

                    refreshtoken = obj["refresh_token"].ToString();
                    conf = appConfigFacade.Retrieve("soa_refreshtoken");
                    conf.Value = refreshtoken;
                    appConfigFacade.Update(conf);

                    IsSuccess = true; Message = "Success Signin";
                }
                else
                {
                    IsSuccess = false; 
                    Message = obj["message"].ToString();
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsSuccess = false;
            }
        }

        public static void ReSignIn(System.Security.Principal.IPrincipal User)
        {
            try
            {
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);
                string userName = appConfigFacade.Retrieve("soa_username").Value;
                string freshToken = appConfigFacade.Retrieve("soa_refreshtoken").Value;
                string url = appConfigFacade.Retrieve("soa_url").Value + "/auth/resignin";

                HttpClientHandler handler = new HttpClientHandler();
                String strWebProxy = appConfigFacade.Retrieve("wssf_webproxy").Value;
                if (strWebProxy.Length > 0)
                {
                    handler = new HttpClientHandler
                    {
                        UseDefaultCredentials = false,
                        Proxy = new WebProxy(strWebProxy, true, new string[] { }),
                        UseProxy = true,
                    };
                }

                HttpClient client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(new { username = userName, refresh_token = freshToken }), Encoding.UTF8, "application/json");
                content.Headers.Add("company", "bsi"); 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Message = String.Empty; IsSuccess = false;
                var result = client.PostAsync(url, content).Result;

                if (result.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception(HttpStatusCode.InternalServerError.ToString());

                var response = result.Content.ReadAsStringAsync();
                var obj = JObject.Parse(response.Result);

                if (result.IsSuccessStatusCode)
                {
                    token = obj["id_token"].ToString();
                    var conf = appConfigFacade.Retrieve("soa_token");
                    conf.Value = token;
                    appConfigFacade.Update(conf);
                    IsSuccess = true; Message = "Success ReSignin";
                }
                else 
                {
                    SignIn(User);
                    if (!IsSuccess)
                        return;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsSuccess = false;
            }
        }

        public static void SendData(SFMasterObject sfMasterObject, System.Security.Principal.IPrincipal User, string ObjectTypeName, object parameter = null, bool isbuildarray = true, List<object> parameters = null)
        {
            try
            {
                AppConfigFacade appConfigFacade = new AppConfigFacade(User);
                string tokenId = string.IsNullOrEmpty(token) ? appConfigFacade.Retrieve("soa_token").Value : token;
                string url = appConfigFacade.Retrieve("soa_url").Value + ObjectTypeName;

                if (string.IsNullOrEmpty(tokenId))
                {
                    SignIn(User);
                    if (!IsSuccess)
                        return;

                    tokenId = token;
                }

                HttpClientHandler handler = new HttpClientHandler();
                String strWebProxy = appConfigFacade.Retrieve("wssf_webproxy").Value;
                if (strWebProxy.Length > 0)
                {
                    handler = new HttpClientHandler
                    {
                        UseDefaultCredentials = false,
                        Proxy = new WebProxy(strWebProxy, true, new string[] { }),
                        UseProxy = true,
                    };
                }

                HttpClient client = new HttpClient(handler);
                var content = new StringContent(isbuildarray ? JsonConvert.SerializeObject(parameters) : JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");
                content.Headers.Add("Authentication", tokenId);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Message = String.Empty; IsSuccess = false;
                var result = client.PostAsync(url, content).Result;

                if (result.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception(HttpStatusCode.InternalServerError.ToString());

                var response = result.Content.ReadAsStringAsync();
                var obj = JObject.Parse(response.Result);

                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (obj["message"].ToString().Equals("Fitur tidak bisa digunakan"))
                    {
                        Message = obj["message"].ToString();
                        IsSuccess = false;
                    }
                    else
                    {
                    ReSignIn(User);
                    if (!IsSuccess)
                        return;

                    SendData(sfMasterObject, User, ObjectTypeName, parameter, false);
                }
                }
                else if (result.IsSuccessStatusCode)
                {
                    //IsSuccess = obj["Status"].ToString().Equals("0");
                    //Message = obj["Message"].ToString();
                    IsSuccess = true;
                    Message = response.Result;
                }
                else
                {
                    IsSuccess = false;
                    Message = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsSuccess = false;
            }
        }
    }
}