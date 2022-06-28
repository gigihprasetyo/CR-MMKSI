using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.WebApi.Models
{
    public class paramPurchaseTransaction
    {
        public paramPurchaseTransaction()
        { }
        public const String SObjectTypeName = "paramPurchaseTransaction";
        public String ID_Number__c { get; set; }
        public String Open_Faktur_Date__c { get; set; }
        public String No_Rangka__c { get; set; }
        public String No_Mesin__c { get; set; }
        public String Jenis_Kendaraan__c { get; set; }
        //public String Kode_Tipe_Kendaraan__c { get; set; }
        public String Kode_Dealer_Penjual__c { get; set; }
        public String Nama_Dealer_Penjual__c { get; set; }
        public String Garansi__c { get; set; }
        public String PDI_Date__c { get; set; }
        public String SPK_No__c { get; set; }
        public String Tipe_Kendaraan__c { get; set; }
        public String PKT_Date__c { get; set; } // farid additional 20190206// farid additional 20190206
        public String Match_Date__c { get; set; }
        public String Warranty_Date__c { get; set; }
        public String Sales_Name__c { get; set; }
        public String Sales_Code__c { get; set; }
    }
}