using Embarr.WebAPI.AntiXss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model 
{
    public class DMSAnnouncementParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public int ID { get; set; }

        [AntiXss]
        public DateTime StartDate { get; set; }

        [AntiXss]
        public DateTime EndDate { get; set; }

        [AntiXss]
        public string StatusAnnounce { get; set; }

        [AntiXss]
        public string Header { get; set; }

        [AntiXss]
        public string Content { get; set; }

        [AntiXss]
        public string Footer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
