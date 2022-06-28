using Embarr.WebAPI.AntiXss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class SFDContactParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public int ID { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string SalesmanCode { get; set; }
        [AntiXss]
        public string FirstName { get; set; }
        [AntiXss]
        public string LastName { get; set; }
        [AntiXss]
        public string CountryCode{ get; set; }
        [AntiXss]
        public string HPNo { get; set; }
        [AntiXss]
        public int PhoneType { get; set; }
        [AntiXss]
        public string Phone { get; set; }
        [AntiXss]
        public string Email { get; set; }
        [AntiXss]
        public int Gender { get; set; }
        [AntiXss]
        public string Address { get; set; }
        [AntiXss]
        public string CityCode { get; set; }
        [AntiXss]
        public string CustomerType { get; set; }
        [AntiXss]
        public int LeadSource { get; set; }
        [AntiXss]
        public string Notes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
