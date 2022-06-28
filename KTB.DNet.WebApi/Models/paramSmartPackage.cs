using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class paramSmartPackage
    {
        public paramSmartPackage()
        { }
        public const String SObjectTypeName = "paramSmartPackage";
        public String Name { get; set; }
        public String Chassis_Number__c { get; set; }
        public String Durasi_MSP__c { get; set; }
        public String Expired_Date__c { get; set; }
        public String ID_Number__c { get; set; }
        public String Join_MSP_Date__c { get; set; }
        public String KM_MSP__c { get; set; }
        public String Nama__c { get; set; }
        public String Phone__c { get; set; }
        public String Tipe_MSP__c { get; set; }
        public String Upgrade_Date__c { get; set; }        
        public String Dnet_ID__c { get; set; }  
    }
}