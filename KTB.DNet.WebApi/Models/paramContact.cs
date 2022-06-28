using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class paramContact
    {
        public paramContact()
        { }
        public const String SObjectTypeName = "paramContactDnet";
        public String LastName { get; set; }
        public String Gender__c { get; set; }
        public String MobilePhone { get; set; }
        public String Email { get; set; }
        public String Address__c { get; set; }
        public String Kabupaten_Kota__c { get; set; }
        public String Provinsi__c { get; set; }
        public String Kelurahan__c { get; set; }
        public String ID_Type__c { get; set; }
        public String ID_Number__c { get; set; }
        public String LeadSource { get; set; }        
    }
}