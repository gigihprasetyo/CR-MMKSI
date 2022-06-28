using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class ParamServiceHistoryBookletSF
    {
        public const String SObjectTypeName = "serviceHistory";

        //public String Old__c { get; set; }
        public String MSP_No__c { get; set; }
        public String Dnet_ID__c { get; set; }
        public String Dealer_code__c { get; set; }
        //public String Dealer_Name__c { get; set; }
        public String Service_Start_Date__c { get; set; }
        public String Service_Start_Time__c { get; set; }
        public String Service_End_Date__c { get; set; }
        public String Service_End_Time__c { get; set; }
        public String Mechanic_Name__c { get; set; }
        public String Work_Order_Number__c { get; set; }
        public String No_Rangka__c { get; set; }
        public String Service_Kind__c { get; set; }
        //public String Kind_Code__c { get; set; }
        //public String Kind_Description__c { get; set; }
        public String Odometer__c { get; set; }
        public String Service_Type__c { get; set; }
        public String Stall_Code__c { get; set; }
        public String Booking_Code__c { get; set; }
        public String Mechanic_Notes__c { get; set; }
        public String Status__c { get; set; }
    }

    public class ParamServiceHistoryBookletMMID
    {
        public const String SObjectTypeName = "serviceHistory";

        //public String Old__c { get; set; }
        public String MSP_No__c { get; set; }
        public String Dnet_ID__c { get; set; }
        public String Dealer_code__c { get; set; }
        //public String Dealer_Name__c { get; set; }
        public String Service_Start_Date__c { get; set; }
        public String Service_Start_Time__c { get; set; }
        public String Service_End_Date__c { get; set; }
        public String Service_End_Time__c { get; set; }
        public String Mechanic_Name__c { get; set; }
        public String Work_Order_Number__c { get; set; }
        public String No_Rangka__c { get; set; }
        public String Service_Kind__c { get; set; }
        //public String Kind_Code__c { get; set; }
        //public String Kind_Description__c { get; set; }
        public String Odometer__c { get; set; }
        public String Service_Type__c { get; set; }
        public String Stall_Code__c { get; set; }
        public String Booking_Code__c { get; set; }
        public String Mechanic_Notes__c { get; set; }
        public String Status__c { get; set; }
    }
}