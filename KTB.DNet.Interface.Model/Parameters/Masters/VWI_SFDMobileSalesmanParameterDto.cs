
#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_SFDMobileSalesmanParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string DealerCity { get; set; }
        public string DealerGroup { get; set; }
        public string DealerArea { get; set; }
        public string DealerBranchCode { get; set; }
        public string DealerBranchName { get; set; }
        public string SalesmanCode { get; set; }
        public string SalesmanName { get; set; }
        public DateTime SalesmanHireDate { get; set; }
           [AntiXss]
        public string JobDescription { get; set; }
        public string LevelDescription { get; set; }
        public string SuperiorName { get; set; }
        public string SuperiorCode { get; set; }

        public string SalesmanEmail { get; set; }
        public string SalesmanHandphone { get; set; }
        public string SalesmanTeamCategory { get; set; }
        public string SalesmanStatus { get; set; }


      
       

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
