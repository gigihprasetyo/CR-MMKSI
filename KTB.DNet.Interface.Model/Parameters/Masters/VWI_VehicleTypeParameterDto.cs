using Embarr.WebAPI.AntiXss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class VWI_VehicleTypeParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [AntiXss]
        public string VehicleType { get; set; }

        [AntiXss]
        public string VehicleDesc { get; set; }

        [AntiXss]
        public string ProductCategory { get; set; }

        [AntiXss]
        public string VehicleModel_S1 { get; set; }

        [AntiXss]
        public string VehicleCategory_S2 { get; set; }

        [AntiXss]
        public string ProductSegment_S3 { get; set; }

        [AntiXss]
        public string DriveSystem_S4 { get; set; }

        public int Status { get; set; }

        [AntiXss]
        public string VariantType { get; set; }

        [AntiXss]
        public string TransmitType { get; set; }

        [AntiXss]
        public string DriveSystemType { get; set; }

        [AntiXss]
        public string SpeedType { get; set; }

        [AntiXss]
        public string FuelType { get; set; }

        [AntiXss]
        public string LastUpdateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
