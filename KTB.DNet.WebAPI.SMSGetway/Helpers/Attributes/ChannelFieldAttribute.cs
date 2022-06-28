using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Helpers.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property| AttributeTargets.Field)]
    public class ChannelFieldAttribute : Attribute
    {
        public string FieldName;
        public bool AlwaysAdd;
        public int Order;

        /// <summary>
        /// (orderNumber, FieldName, AlwaysAdd default True)
        /// </summary>
        /// <param name="order"></param>
        /// <param name="name"></param>
        /// <param name="always"></param>
        public ChannelFieldAttribute(int order, string name, bool always = true)
        {
            this.Order = order;
            this.FieldName = name;
            this.AlwaysAdd = always;
        }
    }
}