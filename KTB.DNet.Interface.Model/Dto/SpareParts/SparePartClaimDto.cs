using KTB.DNet.Interface.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class SparePartClaimDto : DtoBase
    {
        #region Public Properties
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string ClaimNumber { get; set; }
        public int StatusID { get; set; }
        public int ApprovedQty { get; set; }
        public string SONumber { get; set; }
        public string SORetur { get; set; }
        public DateTime SOReturDate { get; set; }
        public string FakturRetur { get; set; }
        public DateTime FakturReturDate { get; set; }
        public List<SparepartClaimDetailDto> ClaimDetails { get; set; }
        public DateTime LastUpdateTime { get; set; } 
        #endregion

        #region Custom Properties

        #endregion
    }

    public class SparePartClaimResponseDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string ClaimNumber { get; set; }
        public int StatusID { get; set; }
        public int ApprovedQty { get; set; }
        public string SONumber { get; set; }
        public string SORetur { get; set; }
        public DateTime SOReturDate { get; set; }
        public string FakturRetur { get; set; }
        public DateTime FakturReturDate { get; set; }
    }
}
