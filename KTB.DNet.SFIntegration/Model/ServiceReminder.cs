using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KTB.DNet.SFIntegration.Model
{
    public class ServiceReminder
    {
        public ServiceReminder()
        {
            
        }
        [JsonIgnore]
        public const String SObjectTypeName = "paramServiceReminder";
        [JsonIgnore]
        public int ID { get; set; }

        public string salesforce_id { get; set; }
        public string Chassis_Number__c {get; set; }
        public string Customer_Name__c {get; set; }
        public string Customer_Phone__c { get; set; }
        public DealerSF Dealer__r { get; set; }
        public string Description__c { get; set; }
        public string Engine_Number__c { get; set; }
        public string Service_Status__c { get; set; }
        public string Service_Type__c { get; set; }
        public string Date_Sync_With_Dnet__c { get; set; }
        public Car__r Car__r { get; set; }
    }

    public class DealerSF
    {
        public string Code__c {get; set; }
    }

    public class Car__r
    {
        public string Code__c { get; set; }
    }

    public class chassisHistoricalData
    {
        public int ActualKM { get; set; }
        public int DeltaDays { get; set; }
    }

    public class FSKindByKMDesc
    {
        public int KM { get; set; }
        public KTB.DNet.Domain.FSKind FSKind { get; set; }
    }
}
