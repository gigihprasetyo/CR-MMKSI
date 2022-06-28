using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.SFIntegration.Model
{
    public class TeleSurveySF : TeleSurveyWithoutCarTypeSF
    {
        public TeleSurveySF()
        {
        }

        public CarType Car_Type__r = new CarType();
        public class CarType { public string Code__c = string.Empty;}
  
    }

    public class TeleSurveyWithoutCarTypeSF
    {
        public TeleSurveyWithoutCarTypeSF()
        {

        }

        public const String SObjectTypeName = "paramInsertCaseTelesurvey";
        public string Caller_type__c = string.Empty;
        //public CarType Car_Type__r = new CarType();
        public DealerType Dealer__r = new DealerType();
        public string SuppliedName = string.Empty;
        public string SuppliedPhone = string.Empty;
        public string Customer_Address__c = string.Empty;
        public string Origin = string.Empty;
        public string Plate_Number__c = string.Empty;
        public string Chassis_Number__c = string.Empty;
        public string Engine_Number__c = string.Empty;
        public string Category__c = string.Empty;
        public string Sub_Category_1__c = string.Empty;
        public string Sub_Category_2__c = string.Empty;
        public string Sub_Category_3__c = string.Empty;
        public string Subject = string.Empty;
        public string Sub_Category_4__c = string.Empty;
        public string Description = string.Empty;

        public class DealerType { public string Code__c = string.Empty;}

        public void Clone(TeleSurveySF dat)
        {
            this.Caller_type__c = dat.Caller_type__c;
            this.Dealer__r = dat.Dealer__r;
            this.SuppliedName = dat.SuppliedName;
            this.SuppliedPhone = dat.SuppliedPhone;
            this.Customer_Address__c = dat.Customer_Address__c;
            this.Origin = dat.Origin;
            this.Plate_Number__c = dat.Plate_Number__c;
            this.Chassis_Number__c = dat.Chassis_Number__c;
            this.Engine_Number__c = dat.Engine_Number__c;
            this.Category__c = dat.Category__c;
            this.Subject = dat.Subject;
            this.Description = dat.Description;
            this.Sub_Category_1__c = dat.Sub_Category_1__c;
            this.Sub_Category_2__c = dat.Sub_Category_2__c;
            this.Sub_Category_3__c = dat.Sub_Category_3__c;
            this.Sub_Category_4__c = dat.Sub_Category_4__c;
        }
    }
}
