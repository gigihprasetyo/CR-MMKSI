using KTB.DNet.WebAPI.SMSGetway.Helpers;
using KTB.DNet.WebAPI.SMSGetway.Helpers.Attributes;
using KTB.DNet.WebAPI.SMSGetway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Models
{
    public class MessageServices
    {
        public MessageServices()
        {
            this.Channels = new Dictionary<int,IJsonSerialize>();
        }

        public MessageServices(string usrName)
            : this()
        {
            this.Username = usrName;
        }

        [ChannelField(1, "type")]
        public string Type { get; set; }

        [ChannelField(2, "username")]
        public string Username { get; set; }

        [ChannelField(3, "ref_id")]
        public string RefID { get; set; }

        [ChannelField(4, "time")]
        public long Time { get; set; }

        [ChannelField(5, "signature")]
        public string Signature { get; set; }

        [ChannelField(6, "subject")]
        public string Subject { get; set; }

        [ChannelField(7, "sender_id")]
        public string SenderID { get; set; }

        [ChannelField(8, "channel")]
        public Dictionary<int, IJsonSerialize> Channels;

        
        public void AddChannel<T>(int order, T objChannel) where T :class
        {
            this.Channels.Add(order, new JsonSerialize<T>(objChannel));
        }

        public void AddChannel<T>(Dictionary<int, T> dicChannels) where T : class
        {
            foreach (KeyValuePair<int, T> item in dicChannels.OrderBy(x => x.Key))
            {
                this.Channels.Add(item.Key, new JsonSerialize<T>(item.Value));                
            }
        }

    }
}