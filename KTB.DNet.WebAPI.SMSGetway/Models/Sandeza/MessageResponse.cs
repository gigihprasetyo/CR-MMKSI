using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Models.Sandeza
{
    public class MessageResponse
    {
        public string rc { get; set; }
        public string ref_id { get; set; }
        public string code_sms { get; set; }
    }
}