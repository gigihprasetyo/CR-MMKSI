using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class VWI_VehicleTypeDto
    {
        public int ID { get; set; }

        public string VehicleType { get; set; }

        public string VehicleDesc { get; set; }

        public string ProductCategory { get; set; }

        public string VehicleModel_S1 { get; set; }

        public string VehicleCategory_S2 { get; set; }

        public string ProductSegment_S3 { get; set; }

        public string DriveSystem_S4 { get; set; }

        public int Status { get; set; }

        public string VariantType { get; set; }

        public string TransmitType { get; set; }

        public string DriveSystemType { get; set; }

        public string SpeedType { get; set; }

        public string FuelType { get; set; }

        public string LastUpdateTime { get; set; }
    }
}
