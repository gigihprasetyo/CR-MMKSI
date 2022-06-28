using KTB.DNet.WebAPI.SMSGetway.Helpers.Attributes;
using KTB.DNet.WebAPI.SMSGetway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Models
{
    [ChannelNames("whatsapp")]
    public class WhatsAppChannel1_6
    {
        public WhatsAppChannel1_6()
        {
            this.BackupExp = string.Empty;
            this.BackupOn = string.Empty;
            this.Attachment = string.Empty;
            this.Header = string.Empty;
            this.Type = string.Empty;
            this.SosialID = string.Empty;
        }
        
       

        [ChannelField(1, "social_id", false)]
        public string SosialID { get; set; }

        [ChannelField(2, "msisdn", false)]
        public string MSISDN { get; set; }

        [ChannelField(3, "type")]
        public string Type { get; set; }

        [ChannelField(4, "template_id", false)]
        public string TemplateID { get; set; }

        [ChannelField(5, "header", false)]
        public string Header { get; set; }

        [ChannelField(6, "message")]
        public string Message { get; set; }

        [ChannelField(7, "attachment", false)]
        public string Attachment { get; set; }

        [ChannelField(8, "backup_on", false)]
        public string BackupOn { get; set; }

        [ChannelField(9, "backup_exp", false)]
        public string BackupExp { get; set; }


    }
}