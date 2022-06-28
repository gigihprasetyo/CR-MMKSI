using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade;
using KTB.DNet.BusinessFacade.General;
using System.Security.Principal;


namespace KTB.DNet.WebApi.Controllers
{
    public class ApiTestController : ApiController
    {

        [HttpGet]
        public string IsOK()
        {

            return "ok";
        }


        [HttpPost]
        public List<AppConfig> GetAppConfig()
        {

            List<AppConfig> app = new List<AppConfig>();
            //app.Add(new AppConfig());

            System.Security.Principal.GenericPrincipal GpUser = new GenericPrincipal(new GenericIdentity("Ahoy"), null);

            try
            {
                AppConfigFacade ApF = new AppConfigFacade(GpUser);
                var ObjData = ApF.RetrieveAll();

                return ObjData.Cast<AppConfig>().ToList<AppConfig>();

            }
            catch (Exception x)
            {

                return app;
            }
            return app;
        }


    }
}
