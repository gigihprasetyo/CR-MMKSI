using Embarr.WebAPI.AntiXss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class VWI_BabitMasterRetailTargetParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public int ID { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string DealerBranchCode { get; set; }
        [AntiXss]
        public string SubCategoryVehicle { get; set; }
        [AntiXss]
        public int MonthPeriod { get; set; }
        [AntiXss]
        public int YearPeriod { get; set; }
        [AntiXss]
        public string RetailTarget { get; set; }
        [AntiXss]
        public int Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
