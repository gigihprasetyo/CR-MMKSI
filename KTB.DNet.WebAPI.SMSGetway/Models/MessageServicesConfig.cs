using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Models
{
    public class MessageServicesConfig
    {
        public string APIUrl { get; set; }
        public string APIUser { get; set; }
        public string APIPassword { get; set; }
        public string DestinationNo { get; set; }
        public string BodyMassage { get; set; }
        public string WhatsAppHeader { get; set; }
        public string Subject { get; set; }
        public string UserName { get; set; }
        public string SenderID { get; set; }
        public string Channels { get; set; }
        public string TemplateID { get; set; }
        public string TemplateValue { get; set; }
        public string TemplateType { get; set; }
        public string BackOn { get; set; }
        public string BackExp { get; set; }
        public bool IsUsingModem { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
    }
}