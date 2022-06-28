using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class MessageServiceUser
    {
        public string ClientID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TypeMessage { get; set; }
        public string BodyMessage { get; set; }
        public string DestinationNo { get; set; }
        public string Email { get; set; }
        public string FID { get; set; }
    }
}