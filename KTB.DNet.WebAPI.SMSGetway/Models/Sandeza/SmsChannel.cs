using KTB.DNet.WebAPI.SMSGetway.Helpers.Attributes;
using KTB.DNet.WebAPI.SMSGetway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Models
{
    [ChannelNames("sms")]
    public class SmsChannel 
    {
        public SmsChannel()
        {
            this.BackupExpired = string.Empty;
            this.BackupOn = string.Empty;
        }

        [ChannelField(1, "msisdn")]
        public string DestinationNo { get; set; }

        [ChannelField(2, "message")]
        public string Message { get; set; }

        [ChannelField(3, "backup_on")]
        public string BackupOn { get; set; }

        [ChannelField(4, "backup_exp")]
        public string BackupExpired { get; set; }

    }

}