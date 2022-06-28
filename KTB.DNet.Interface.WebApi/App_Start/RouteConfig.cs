#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : RouteConfig class
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
using Swashbuckle.Application;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
#endregion

namespace KTB.DNet.Interface.WebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            if (ConfigurationManager.AppSettings["EnableSwagger"] == "true")
            {
                routes.MapHttpRoute(
                    name: "swagger_root",
                    routeTemplate: "",
                    defaults: null,
                    constraints: null,
                    handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger")
                    );
            }
            else
            {
                routes.IgnoreRoute("");
            }

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}