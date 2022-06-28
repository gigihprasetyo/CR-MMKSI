using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class ServiceReminderUpdate
    {
        public string SalesForceID { get; set; }
        public byte Status { get; set; }
    }
}