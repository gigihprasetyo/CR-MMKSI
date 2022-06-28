using KTB.DNet.Domain;
using KTB.DNet.Interface.Framework.CustomAttribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{

    public class DSFCeilingDto : ReadDtoBase
    {
        public string CreditAccount { get; set; }
        public string DealerName { get; set; }
        public decimal StandardCeiling { get; set; }
        public decimal FactoringCeiling { get; set; }
        public decimal Outstanding { get; set; }
        public decimal AvailableCeiling { get; set; }
        public string Status { get; set; }
    }

    public class DSFCeilingUpdateDto : DtoBase
    {

        public int ID { get; set; }
        public string CreditAccount { get; set; }
        public string DealerNAme { get; set; }
        public decimal FactoringCeiling { get; set; }
        public decimal OutstandingBilling { get; set; }
        public decimal AvailableCeiling { get; set; }
        public string Status { get; set; }
        public DateTime ValidUntil { get; set; }
        public decimal POAmount { get; set; }
        //public DateTime EffectiveDate { get; set; }
        //public decimal TotalCeiling { get; set; }
    }

    
}
