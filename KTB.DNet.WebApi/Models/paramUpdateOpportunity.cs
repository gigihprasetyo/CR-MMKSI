using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class paramUpdateOpportunity
    {
        public paramUpdateOpportunity()
        { }

        public const String SObjectTypeName = "paramUpdateOpportunity";

        public String id { get; set; }
        public String Dealer_Code__c { get; set; }
        public String Dealer_Name__c { get; set; }
        public String Salesman_Code__c { get; set; }
        public String Salesman_Name__c { get; set; }
        public String Consumen_Type__c { get; set; }
        public String Name { get; set; }
        public String Address__c { get; set; }
        public String Email__c { get; set; }
        public String Mobile_Phone__c { get; set; }
        public String Gender__c { get; set; }
        public String ID_Type__c { get; set; }
        public String ID_Number__c { get; set; }
        public String Information_Type__c { get; set; }
        public String Customer_Purposes__c { get; set; }
        public String LeadSource { get; set; }
        public String Prospect_Date__c { get; set; }
        public String SPK_No__c { get; set; }
        public String Dealer_SPK_No__c { get; set; }
        public String SPK_Status__c { get; set; }
        public String Validation_Key__c { get; set; }
        public String StageName { get; set; }
        public String CloseDate { get; set; }
        public String AccountID { get; set; }
        public String Phone_No__c { get; set; }
        public String Car_Type__c { get; set; }
        public String Car_Code__c { get; set; }
        public String Quantity__c { get; set; }
        public String Is_Valid_To_Send_SMS__c { get; set; }
        public String MMKSI_WEB_ID__c { get; set; }
        public String Rejected_Reason__c { get; set; }
        public String Dealer_SPK_Date__c { get; set; }
        public String Created_Date_SPK__c { get; set; }
        public String Company_Type__c { get; set; }
        public String Postal_Code__c { get; set; }
        public String City__c { get; set; }
        public String Office_No__c { get; set; }
        public String Home_No__c { set; get; }
        public String Age__c { get; set; }
        public String Registration_Code__c { get; set; }
        public String Web_ID_Dealer__c { get; set; }
        public String Current_Vehicle_Brand__c { get; set; }
        public String Current_Vehicle_Type__c { get; set; }
        public String Note__c { get; set; }
        public String Lead_Name__c { get; set; }
        public String Lead_Address__c { get; set; }
        public String Lead_Email__c { get; set; }
        public String Lead_Dealer_Code__c { get; set; }
        public String Lead_Consumen_Type__c { get; set; }
        public String Lead_Phone__c { get; set; }
        public String Lead_Gender__c { get; set; }
        public String Lead_Status__c { get; set; }
        public String Lead_Salesman_Code__c { get; set; }
        public String Lead_Salesman_Name__c { get; set; }

    }

    public class paramUpdateOpportunityVehicle
    {
        public const String SObjectTypeName = "paramOpportunityVehicle";
        public paramUpdateOpportunityVehicle()
        { }
        public String Opportunity__c { get; set; }
        public String Car_Code__c { get; set; }
        public String Additional__c { get; set; }
        public String Color__c { get; set; }
        public String Dnet_ID__c { get; set; }
        public String Quantity__c { get; set; }
        public String Rejected_Reason__c { get; set; }
        public String Remarks__c { get; set; }
        public String Status__c { get; set; }
    }
}