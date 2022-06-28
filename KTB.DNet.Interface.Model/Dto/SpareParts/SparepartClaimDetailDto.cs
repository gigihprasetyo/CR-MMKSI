using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model.Dto
{
    public class SparepartClaimDetailDto : DtoBase
    {
        public int ID { get; set; }
        public string NoBarang { get; set; }
        public int QtyClaim { get; set; }
        public byte StatusDetail { get; set; }
        public byte StatusDetailKTB { get; set; }
        public int ClaimGoodConditionId { get; set; }
        public string Keterangan { get; set; }
        public string UpdatedBy { get; set; }
    }
}
