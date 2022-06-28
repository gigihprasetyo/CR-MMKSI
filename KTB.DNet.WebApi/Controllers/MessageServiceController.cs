using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;
using KTB.DNet.WebApi.Helpers;
using KTB.DNet.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace KTB.DNet.WebApi.Controllers
{
    public class MessageServiceController : BaseApiController
    {
        private MessagerResponse Result;
        public MessageServiceController() { this.Result = new MessagerResponse(); }

        [HttpPost]
        public IDictionary<string, object> Send()
        {
            bool success = false; string message = string.Empty; bool isvalid = false;
            if (!IsLogin()) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your Token is invalid."), null);
            try
            {
                GenericPrincipal userApi = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetApi"), null);
                var jsonBody = Request.Content.ReadAsStringAsync().Result;

                WsLog wLog = new WsLog();
                wLog.Body = "K:MESSAGESERVICE\n" + jsonBody;
                wLog.ErrorMessage = string.Empty;
                wLog.Status = success.ToString();
                wLog.RowStatus = 0;
                wLog.Source = GetClientIpAddress();
                wLog.ID = new WsLogFacade(userApi).Insert(wLog);

                AppConfigFacade funcConfig = new AppConfigFacade(userApi);
                APIHelpers<MessagerResponse> api = new APIHelpers<MessagerResponse>();
                api.JsonContent = jsonBody;
                api.Url = funcConfig.Retrieve("MSApiUrl").Value;// "http://localhost/MMKSI.MessageServices/Api/MessageService/";
                api.ProxyAddress = funcConfig.Retrieve("MSApiProxyAddress").Value;
                api.ProxyPort = funcConfig.Retrieve("MSApiProxyPort").Value;
                Task.Run(() => api.POST()).Wait();
                WsLog wLogUpdate = new WsLogFacade(userApi).Retrieve(wLog.ID);
                if (api.ResultPost != null)
                {
                    
                    wLogUpdate.Status = (api.ResultPost.Status == "success" ? true : false).ToString();
                    wLogUpdate.Message = api.ResultPost.Message == "" ? "success" : api.ResultPost.Message;
                    new WsLogFacade(userApi).Update(wLogUpdate);

                    return this.result(api.ResultPost.Status == "success" ? true : false, "-1", 0,
                        api.ResultPost.Message == "" ? "success" : api.ResultPost.Message, null);
                }
                else
                {
                    wLogUpdate.Message = api.StatusCode;
                    new WsLogFacade(userApi).Update(wLogUpdate);
                    return this.result(success, "-1", 0, api.StatusCode, null); 
                }
            }
            catch (Exception ex)
            {
                success = false; message = ex.Message;
            }
            return this.result(success, "-1", 0, message, null);
        }

        public string GetClientIpAddress(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop;
                prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
    }

}
