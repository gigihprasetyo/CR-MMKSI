using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Routing;

namespace KTB.DNet.WebAPI.SMSGetway
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {    //// Web API routes
            config.MapHttpAttributeRoutes(); //Don't miss this

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );
        }
    }
}
