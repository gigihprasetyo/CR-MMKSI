using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KTB.DNet.Interface.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Redirect if url does not have a slash
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (string.IsNullOrEmpty(url)) return;

            if (HttpContext.Current.Request.Url.Segments.Length == 2)
            {
                string urlSegment = HttpContext.Current.Request.Url.Segments[1];
                string lastChar = urlSegment[urlSegment.Length - 1].ToString();
                if (lastChar != "/")
                {
                    Response.RedirectToRoute("Default");
                }
            }
        }
    }
}
