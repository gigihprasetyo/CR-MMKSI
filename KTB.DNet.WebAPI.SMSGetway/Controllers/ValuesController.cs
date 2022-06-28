using KTB.DNet.WebAPI.SMSGetway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KTB.DNet.WebAPI.SMSGetway.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

          [HttpPost]
        public SMSResult Post(SMSUser obj)
        {
            SMSResult objResult = new SMSResult();
            objResult.Message = "ok";
            return objResult;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
