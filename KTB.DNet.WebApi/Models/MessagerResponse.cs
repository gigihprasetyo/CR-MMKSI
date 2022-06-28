using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class MessagerResponse
    {
        public MessagerResponse(){}
        public MessagerResponse(string status, string code, string message)
        {
            this.Status = status;
            this.Message = message;
            this.Code = code;
        }

        public string Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}