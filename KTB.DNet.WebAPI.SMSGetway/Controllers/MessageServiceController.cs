using KTB.DNet.WebAPI.SMSGetway.Models;
using KTB.DNet.WebAPI.SMSGetway.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KTB.DNet.WebAPI.SMSGetway.Helpers.Interfaces;

namespace KTB.DNet.WebAPI.SMSGetway.Controllers
{
    public class MessageServiceController : ApiController
    {
        [HttpPost]
        public MessageServicesResult Post(MessageServicesUser obj)
        {
            IServiceMessage<MessageServicesResult> MS = new MessageSeviceHelpers(obj);
             MS.SendMessage();

            return MS.GetResponse;
        }
    }
}
