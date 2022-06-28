using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.ServiceModel.Channels;
using KTB.DNet.WebAPI.SMSGetway.Models;
using KTB.DNet.WebAPI.SMSGetway.Helpers.Interfaces;
using KTB.DNet.WebAPI.SMSGetway.Helpers;
using KTB.DNet.WebAPI.SMSGetway.Models.Sandeza;


namespace KTB.DNet.WebAPI.SMSGetway.Controllers
{
    public class ReportController : ApiController
    {
        private string ClientIP;
        public ReportController(){}

        [HttpPost]
        public MessageServicesResult Post(MessageReport obj)
        {
            this.ClientIP = GetClientIpAddress();
            IReport<MessageServicesResult> RS = new ReportHelpers(obj, this.ClientIP);
            RS.SetReport();

            return RS.Result;
        }

        private string GetClientIpAddress(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey( RemoteEndpointMessageProperty.Name))
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
