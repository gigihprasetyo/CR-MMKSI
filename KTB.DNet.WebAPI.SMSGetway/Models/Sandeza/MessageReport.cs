using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Models.Sandeza
{
    public class MessageReport
    {
        public string ref_id { get; set; }
        public string channel { get; set; }
        public string status { get; set; }
        public string time { get; set; }
        public string division_id { get; set; }
    }
}