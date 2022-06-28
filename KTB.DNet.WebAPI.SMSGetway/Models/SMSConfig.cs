using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Models
{
    public class SMSConfig
    {
        public string APIUrl { get; set; }
        public string APIUser { get; set; }
        public string APIPassword { get; set; }
        public string DestinationNo { get; set; }
        public string BodyMassage { get; set; }
        public bool IsUsingModem { get; set; }
        public bool IsActive { get; set; }
        public string Trx_id { get; set; }
        public string Status { get; set; }
        public string StatusNotification { get; set; }
    }
}