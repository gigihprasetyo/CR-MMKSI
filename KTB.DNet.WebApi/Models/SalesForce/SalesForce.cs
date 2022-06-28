using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using Salesforce.Common;
using Salesforce.Force;
using KTB.DNet.BusinessFacade;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;


namespace KTB.DNet.WebApi.Models.SalesForce
{
	public static class SalesForce
    {
        //private static string ConsumerKey = "3MVG99S6MzYiT5k8v_tpj0oztWwvc_19DZl2MhX3sT0njoAKdnN..ot4RlOKz4zeSHM5P0AUZtiZYbRjG9T6a";
        //private static string ConsumerSecret = "4373355308920100566";
        //private static string Username = "dnet@ktb.com.dev";
        //private static string Password = "development1"+"";
        //private static string url = "https://test.salesforce.com/services/oauth2/token";

        public static string Message { get; set; }
        public static bool IsSuccess { get; set; }

        public static string GetJsonStringMethodPost(string objParams, string url, string token)
        {
            string Result = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var data = Encoding.ASCII.GetBytes(objParams);

            request.Method = "POST";
            request.ContentType = "application/raw";
            request.ContentLength = data.Length;

            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                Result = sr.ReadToEnd();
            }
            return Result;
        }



        public static string GetJsonStringMethodGet(string url, string ObjectTypeName, string param, string HeaderSignature)
        {
            string responseString = null;
            try
            {

                url = url + "/" + ObjectTypeName + param;

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";

                if (HeaderSignature != null)
                {
                    request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + HeaderSignature);
                }

                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }

                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    responseString = sr.ReadToEnd();
                    if (responseString == "")
                    {
                        responseString = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                responseString = "Not Found";
            }
            return responseString;
        }

        public static async Task SendDealer(System.Security.Principal.IPrincipal User, string ObjectTypeName, object parameter, List<paramOpportunityVehicle> parameter1, bool isbuildarray = true)
        {
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string ConsumerKey = appConfigFacade.Retrieve("wssf_customerkey").Value;
            string ConsumerSecret = appConfigFacade.Retrieve("wssf_consumersecret").Value;
            string Username = appConfigFacade.Retrieve("wssf_username").Value;
            string Password = appConfigFacade.Retrieve("wssf_password").Value;
            string url = appConfigFacade.Retrieve("wssf_url").Value;
            string strJson = "";

            IsSuccess = false; Message = String.Empty;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

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

                var auth = new AuthenticationClient(new HttpClient(handler));
                await auth.UsernamePasswordAsync(ConsumerKey, ConsumerSecret, Username, Password, url);

                var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion, new HttpClient(handler), new HttpClient(handler));


                StringBuilder builder = new StringBuilder();
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);
                builder.Append(strJson);
                builder.Append("&paramOpportunityVehicle=");

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(parameter1);
                builder.Append(strJson);

                builder.Append("");

                string param = builder.ToString();

                var response = GetJsonStringMethodGet(auth.InstanceUrl.ToString(), ObjectTypeName, param, auth.AccessToken);

                var obj = JObject.Parse(response);
                Message = (string)obj["Message"];
                var Status = (string)obj["Status"];

                //IList<object> parameters = new List<object>();
                //parameters.Add(parameter);
                //parameters.Add(parameter1);

                //var response = await client.<SalesForceResult>(ObjectTypeName, ((isbuildarray) ? parameters : parameter));
                if (Status.Equals("1")) { IsSuccess = false; Message = Message; }
                else { IsSuccess = true; }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }


        public static async Task SendDealerPost(System.Security.Principal.IPrincipal User, string ObjectTypeName, object parameter, bool isbuildarray = true)
        {
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string ConsumerKey = appConfigFacade.Retrieve("wssf_customerkey").Value;
            string ConsumerSecret = appConfigFacade.Retrieve("wssf_consumersecret").Value;
            string Username = appConfigFacade.Retrieve("wssf_username").Value;
            string Password = appConfigFacade.Retrieve("wssf_password").Value;
            string url = appConfigFacade.Retrieve("wssf_url").Value;
            string strJson = "";

            IsSuccess = false; Message = String.Empty;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

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

                var auth = new AuthenticationClient(new HttpClient(handler));
                await auth.UsernamePasswordAsync(ConsumerKey, ConsumerSecret, Username, Password, url);

                var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion, new HttpClient(handler), new HttpClient(handler));

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                var response = GetJsonStringMethodPost(strJson, auth.InstanceUrl + "/" + ObjectTypeName, auth.AccessToken);

                var obj = JObject.Parse(response);
                Message = (string)obj["Message"];
                var Status = (string)obj["Status"];

                if (Status.Equals("1")) { IsSuccess = false; Message = Message; }
                else { IsSuccess = true; }


            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }


        public static async Task Send(System.Security.Principal.IPrincipal User, string ObjectTypeName, object parameter, bool isbuildarray = true)
        {
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string ConsumerKey = appConfigFacade.Retrieve("wssf_customerkey").Value;
            string ConsumerSecret = appConfigFacade.Retrieve("wssf_consumersecret").Value;
            string Username = appConfigFacade.Retrieve("wssf_username").Value;
            string Password = appConfigFacade.Retrieve("wssf_password").Value;
            string url = appConfigFacade.Retrieve("wssf_url").Value;

            IsSuccess = false; Message = String.Empty;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

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

                var auth = new AuthenticationClient(new HttpClient(handler));
                await auth.UsernamePasswordAsync(ConsumerKey, ConsumerSecret, Username, Password, url);

                var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion, new HttpClient(handler), new HttpClient(handler));

                IList<object> parameters = new List<object>();
                parameters.Add(parameter);

                var response = await client.ExecuteRestApiAsync<SalesForceResult>(ObjectTypeName, ((isbuildarray) ? parameters : parameter));
                if (response.Status.Equals("1")) { IsSuccess = false; Message = response.Message; }
                else { IsSuccess = true; }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }




        public static async Task SendData(SFMasterObject sfMasterObject, System.Security.Principal.IPrincipal User, string ObjectTypeName, object parameter, bool isbuildarray = true)
        {
            //SFConfig sfConfig = new SFConfigFacade(User).Retrieve(sfMasterObject.SFConfig.ID);
            //string ConsumerKey = sfConfig.ConsumerKey;
            //string ConsumerSecret = sfConfig.ConsumerSecret;
            //string Username = sfConfig.Username;
            //string Password = sfConfig.Password;
            //string url = sfConfig.Url;

            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string ConsumerKey = appConfigFacade.Retrieve("wssf_customerkey").Value;
            string ConsumerSecret = appConfigFacade.Retrieve("wssf_consumersecret").Value;
            string Username = appConfigFacade.Retrieve("wssf_username").Value;
            string Password = appConfigFacade.Retrieve("wssf_password").Value;
            string url = appConfigFacade.Retrieve("wssf_url").Value;

            IsSuccess = false; Message = String.Empty;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

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

                var auth = new AuthenticationClient(new HttpClient(handler));
                await auth.UsernamePasswordAsync(ConsumerKey, ConsumerSecret, Username, Password, url);

                var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion, new HttpClient(handler), new HttpClient(handler));

                IList<object> parameters = new List<object>();
                parameters.Add(parameter);

                var response = await client.ExecuteRestApiAsync<SalesForceResult>(ObjectTypeName, ((isbuildarray) ? parameters : parameter));
                if (response.Status.Equals("1")) { IsSuccess = false; Message = response.Message; }
                else { IsSuccess = true; Message = response.Message; }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

    }
}