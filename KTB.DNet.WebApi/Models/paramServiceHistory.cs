using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class paramServiceHistory
    {
        public paramServiceHistory()
        { }
        public const String SObjectTypeName = "paramServiceHistory";
        public String No_Rangka__c { get; set; }
        public String Service_Type__c { get; set; }
        public String Service_Date__c { get; set; }
        public String Dealer_code__c { get; set; }
        public String Odometer__c { get; set; }
        public String MSP_No__c { get; set; }
        public String Dnet_ID__c { get; set; }         
    }
}