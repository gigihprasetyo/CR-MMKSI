using KTB.DNet.WebApi.Models.MiddlewareSOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class ParamSparepartHistorySF
    {
        public ParamSparepartHistorySF()
        { }
        public const String SObjectTypeName = "sparepartSalesHistory";
        //public String Assist_Part_Sales_ID__c { get; set; }
        public String SalesforceID { get; set; }
        public String Transaction_Date__c { get; set; }
        //public String Dealer_Code__c { get; set; }
        //public String No_Work_Order__c { get; set; }
        public String Parts_Code__c { get; set; }
        public String Parts_Name__c { get; set; }
        public String Quantity__c { get; set; }
        public String Sales_Price__c { get; set; }
        public String Is_Campaign__c { get; set; }
        public String Campaign_No__c { get; set; }
        public String Campaign_Description__c { get; set; }
        public String Status__c { get; set; }
        public String Dnet_ID__c { get; set; }
    }

    public class ParamSparepartHistoryMMID
    {
        public ParamSparepartHistoryMMID()
        { }
        public const String SObjectTypeName = "sparepartList";
        //public String Assist_Part_Sales_ID__c { get; set; }
        public String Dnet_Sparepart_ID__c { get; set; }
        public String Transaction_Date__c { get; set; }
        //public String Dealer_Code__c { get; set; }
        //public String No_Work_Order__c { get; set; }
        public String Parts_Code__c { get; set; }
        public String Parts_Name__c { get; set; }
        public String Quantity__c { get; set; }
        public String Sales_Price__c { get; set; }
        public String Is_Campaign__c { get; set; }
        public String Campaign_No__c { get; set; }
        public String Campaign_Description__c { get; set; }
        public String Status__c { get; set; }
        public String Dnet_ID__c { get; set; }
    }
}