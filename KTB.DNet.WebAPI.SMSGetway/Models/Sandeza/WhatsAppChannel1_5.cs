using KTB.DNet.WebAPI.SMSGetway.Helpers.Attributes;
using KTB.DNet.WebAPI.SMSGetway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Models
{
    [ChannelNames("whatsapp")]
    public class WhatsAppChannel1_5
    {
        public WhatsAppChannel1_5()
        {
            this.BackupExp = string.Empty;
            this.BackupOn = string.Empty;
            this.Attachment = string.Empty;
            this.SosialID = string.Empty;
        }



        [ChannelField(1, "socialid", false)]
        public string SosialID { get; set; }

        [ChannelField(2, "chat_id", false)]
        public string ChatID { get; set; }

        [ChannelField(3, "templateid", false)]
        public string TemplateID { get; set; }

        [ChannelField(4, "message")]
        public string Message { get; set; }

        [ChannelField(5, "attachment", false)]
        public string Attachment { get; set; }

        [ChannelField(6, "order", false)]
        public string Order { get; set; }

        [ChannelField(7, "backup_on", false)]
        public string BackupOn { get; set; }

        [ChannelField(8, "backup_exp", false)]
        public string BackupExp { get; set; }


    }
}