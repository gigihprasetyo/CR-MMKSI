using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class paramUpdateCase
    {
        public paramUpdateCase()
        {
            Confirm_Service_Booking_Request_Date__c = string.Empty;
            Confirm_Test_Drive_Request_Date__c = string.Empty;
            Dealer_Respons__c = string.Empty;
        }
        public const String SObjectTypeName = "paramUpdateCase";

        public String id { get; set; }
        public String status { get; set; }
        public String Status_By_Dealer__c { get; set; }
        public String comment { get; set; }
        public String Confirm_Service_Booking_Request_Date__c { get; set; }
        public String Confirm_Test_Drive_Request_Date__c { get; set; }
        public String Dealer_Respons__c { get; set; }
    }
}