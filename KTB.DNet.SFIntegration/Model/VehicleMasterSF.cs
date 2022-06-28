using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.SFIntegration.Model
{
    public class VehicleMasterSF
    {
        public const String SObjectTypeName = "paramVehicle";
        public string Name { get; set; }
        public string Code__c { get; set; }
        public string Brand__c { get; set; }
        public string Variant__c { get; set; }
        public string Type__c { get; set; }
        public string Fuel_Type__c { get; set; }
    }
}
