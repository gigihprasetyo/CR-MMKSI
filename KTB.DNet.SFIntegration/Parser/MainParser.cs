using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;
using KTB.DNet.SFIntegration.Model;
using Salesforce.Common;
using Salesforce.Force;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.SFIntegration.Parser
{
    public class MainParser
    {
        public static string Message { get; set; }
        public static bool IsSuccess { get; set; }

        public static async Task SendData(System.Security.Principal.IPrincipal User, string ObjectTypeName, List<object> parameter)
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
                //String strWebProxy = appConfigFacade.Retrieve("wssf_webproxy").Value;
                String strWebProxy = appConfigFacade.Retrieve("wssf_New_WebProxy").Value;
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

                //IList<object> parameters = new List<object>();
                //parameters.Add(parameter);

                var response = await client.ExecuteRestApiAsync<SalesForceResult>(ObjectTypeName, parameter);
                if (response.Status.Equals("1")) { IsSuccess = false; Message = response.Message; }
                else { IsSuccess = true; Message = response.Message; }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        public static async Task SendData(System.Security.Principal.IPrincipal User, string ObjectTypeName, object parameter)
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
                //String strWebProxy = appConfigFacade.Retrieve("wssf_webproxy").Value;
                String strWebProxy = appConfigFacade.Retrieve("wssf_New_WebProxy").Value;
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

                var response = await client.ExecuteRestApiAsync<SalesForceResult>(ObjectTypeName, parameter);
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
