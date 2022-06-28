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
    public class DSFLeasingClaimDto : DtoBase
    {
        
        public string AgreementNo { get; set; }
        public int AssetSeqNo { get; set; }
        public decimal ATPMSubsidy { get; set; }
        public BenefitClaimHeader BenefitClaimHeader { get; set; }
        public ChassisMaster ChassisMaster { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime ClaimDate { get; set; }
        public string CollectionPeriod { get; set; }
        public byte CollectionPeriodMonth { get; set; }
        public short CollectionPeriodYear { get; set; }
        public string CustomerName { get; set; }
        public Dealer Dealer { get; set; }
        public string EngineNumber { get; set; }
        public DateTime GoLiveDate { get; set; }
        public int ID { get; set; }
        public string Insurance { get; set; }
        public decimal InterestLease { get; set; }
        public string ObjectLease { get; set; }
        public int PeriodLease { get; set; }
        public string ProgramName { get; set; }
        public string RegNumber { get; set; }
        public DateTime SKDApprovalDate { get; set; }
        public DateTime SKDDate { get; set; }
        public string SKDNumber { get; set; }
        public short Status { get; set; }
        public string SupplierName { get; set; }
        public decimal TotalAmountLease { get; set; }
        public decimal TotalDP { get; set; }
        public string TypeInsurance { get; set; }
        public int Unit { get; set; }
        public string RemarkByDealer {get;set;}
        public string RemarkByDSF{get;set;}
        [Ignore]
        public bool IsNotChange { get; set; }
        [Ignore]
        public string ErrorMessage {get;set;}
        public string ValidatingRemark { get; set; }
        [Ignore]
        public int ValidatingResult { get; set; }
        [Ignore]
        public int ValidatingResultCode { get; set; }
        public enum ValidateResult
        {
            NotValid = 0,
            Valid = 1,
        }
        public enum ValidateResultCode
        {
            OK = 0,
            ChassisNotValid = 1,
            EngineNotValid = 2,
            ChassisAndEngineNotValid = 3,
            AlreadyClaimByDealer = 4,
            BenefitNotValid = 5,
            DataDoubleUpload = 6,
            ClaimSudahPernahDiupload = 7,
        }
    }

    public class DSFLeasingClaimDocumentDto : DtoBase { }
}
