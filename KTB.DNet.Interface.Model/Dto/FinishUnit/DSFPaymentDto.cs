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

    public class DSFPaymentDto : ReadDtoBase
    {
        public string RegPO { get; set; }
        public string SalesOrderNumber { get; set; }
        public string  GiroNumber { get; set; }
        public string  RegNumber { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public Decimal Amount { get; set; }
        public short IsTOP { get; set; }
    }

    public class DSFPaymentUpdateDto : DtoBase
    {

        public int ID { get; set; }
        public string SONumber { get; set; }
        public string SlipNumber { get; set; }
        public decimal Amount { get; set; }
        public short Status { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    
}
