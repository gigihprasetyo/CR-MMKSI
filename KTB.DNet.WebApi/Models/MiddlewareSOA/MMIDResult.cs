using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models.MiddlewareSOA
{
    public class MMIDResult
    {
        public bool error { get; set; }
        public AlertMMID alerts { get; set; }
        public DataMMID data { get; set; }
    }

    public class AlertMMID
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class DataMMID
    {
        public int received { get; set; }
        public int processed { get; set; }
        public int insert_count { get; set; }
        public int update_count { get; set; }
        public int failed_count { get; set; }
        public string status { get; set; }
        public FailedDetailMMID details { get; set; }
        public List<FailedDetailMMID> failed_details { get; set; }
    }

    public class FailedDetailMMID
    {
        public int index { get; set; }
        public string reason { get; set; }
        public object data { get; set; }
    }
}