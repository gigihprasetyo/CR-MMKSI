using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Models
{
    public class MessageServicesResult
    {
        public string Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}