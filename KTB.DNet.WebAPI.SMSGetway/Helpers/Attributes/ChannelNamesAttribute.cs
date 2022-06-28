using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebAPI.SMSGetway.Helpers.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class)]
    public class ChannelNamesAttribute : Attribute
    {
        public string Name;
        public ChannelNamesAttribute(string name)
        {
            this.Name = name;
        }
    }
}