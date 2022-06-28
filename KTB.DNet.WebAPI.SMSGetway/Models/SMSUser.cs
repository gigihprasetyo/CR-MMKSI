using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Models
{
    public class SMSUser
    {
        //public int ID { get; set; }
        public string ClientID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BodyMessage { get; set; }
        public string DestinationNo { get; set; }
    }
}