using KTB.DNet.SFIntegration.SchedullingSF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KTB.DNet.Web.Scheduling.Controllers
{
    public class ApiTestController : Controller
    {
        // GET: ApiTest
        public ActionResult Index()
        {
            ServiceHistoryBookletLogic.WSSalesforce_ServiceHistory(true);
            return View();
        }

        [HttpGet]
        [Route("api/TestHit")]
        public void TestHit()
        {
            ServiceHistoryBookletLogic.WSSalesforce_ServiceHistory(true);
        }
    }
}