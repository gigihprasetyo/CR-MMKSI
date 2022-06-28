#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Framework.CustomAttribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class DSFLeasingClaimParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public int ID { get; set; }
        [AntiXss]
        public string AgreementNo { get; set; }
        [AntiXss]
        public int AssetSeqNo { get; set; }
        [AntiXss]
        public decimal ATPMSubsidy { get; set; }
        [AntiXss]
        public BenefitClaimHeader BenefitClaimHeader { get; set; }
        [AntiXss]
        public int ChassisMasterID { get; set; }
        [AntiXss]
        public ChassisMaster ChassisMaster { get; set; }
        [AntiXss]
        public string ChassisNumber { get; set; }
        [AntiXss]
        public DateTime ClaimDate { get; set; }
        [AntiXss]
        public string CollectionPeriod { get; set; }
        [AntiXss]
        public byte CollectionPeriodMonth { get; set; }
        [AntiXss]
        public short CollectionPeriodYear { get; set; }
        [AntiXss]
        public string CustomerName { get; set; }
        [AntiXss]
        public int DealerID { get; set; }
        [AntiXss]
        public Dealer Dealer { get; set; }
        [AntiXss]
        public string EngineNumber { get; set; }
        [AntiXss]
        public DateTime GoLiveDate { get; set; }
        [AntiXss]
        public string Insurance { get; set; }
        [AntiXss]
        public decimal InterestLease { get; set; }
        [AntiXss]
        public string ObjectLease { get; set; }
        [AntiXss]
        public int PeriodLease { get; set; }
        [AntiXss]
        public string ProgramName { get; set; }
        [AntiXss]
        public string RegNumber { get; set; }
        [AntiXss]
        public DateTime SKDApprovalDate { get; set; }
        [AntiXss]
        public DateTime SKDDate { get; set; }
        [AntiXss]
        public string SKDNumber { get; set; }
        [AntiXss]
        public short Status { get; set; }
        [AntiXss]
        public string SupplierName { get; set; }
        [AntiXss]
        public decimal TotalAmountLease { get; set; }
        [AntiXss]
        public decimal TotalDP { get; set; }
        [AntiXss]
        public string TypeInsurance { get; set; }
        [AntiXss]
        public int Unit { get; set; }
        [AntiXss]
        public string RemarkByDealer { get; set; }
        [AntiXss]
        public string RemarkByDSF { get; set; }
        [AntiXss]
        public string ValidatingRemark { get; set; }
        public int ValidatingResult { get; set; }
        public int ValidatingResultCode { get; set; }
        [Ignore]
        public bool IsNotChange { get; set; }
        [Ignore]
        public string ErrorMessage { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class DSFLeasingClaimDocumentParameterDto : ParameterDtoBase, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class DSFLeasingClaimCreateParameterDto : DSFLeasingClaimParameterDto
    {

    }

    public class DSFLeasingClaimCreateParameter
    {
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public int AssetSeqNo { get; set; }
        public string AgreementNumber { get; set; }
        public string SKDNumber { get; set; }
        public DateTime SKDDate { get; set; }
        public DateTime SKDApprovalDate { get; set; }
        public DateTime GoLiveDate { get; set; }
        public decimal ATPMSubsidy { get; set; }
        public string SupplierName { get; set; }
        public string ProgramName { get; set; }
        public int CollectionPeriodMonth { get; set; }
        public int CollectionPeriodYear { get; set; }
        public decimal TotalDownPayment { get; set; }
        public decimal TotalAmountLease { get; set; }
        public int PeriodLease { get; set; }
        public decimal InterestLease { get; set; }
        public string Insurance { get; set; }
        public string TypeInsurance { get; set; }
    }

    public class DSFLeasingClaimCreateResponse
    {
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string RegNumber { get; set; }
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public int AssetSeqNo { get; set; }
        public DateTime ClaimDate { get; set; }
        public string AgreementNumber { get; set; }
        public string CustomerName { get; set; }
        public int Unit { get; set; }
        public string ObjectLease { get; set; }
        public string ProductionYear { get; set; }
        public string RemarksCode { get; set; }
        public string RemarksDescription { get; set; }
    }

    public class DSFLeasingClaimGetFileParameter
    {
        public string regnumber { get; set; }
        public string path { get; set; }
    }

    public class ResubmitClaimParamater
    {
        public string RegNumber { get; set; }
        public int Status { get; set; }
        public string RemarkByDSF { get; set; }
        public List<ResubmitClaimEvidenceFileParamater> EvidenceFiles { get; set; }
    }

    public class ResubmitClaimEvidenceFileParamater
    {
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public string Base64OfStreamFile { get; set; }
    }

    public class ResubmitClaimResponse
    {
        public string RegNumber { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
    }

    public class DSFLeasingClaimDAPPParameter
    {
        public string Status { get; set; }
        public string SourceData { get; set; }
        public string LastUpdateTime { get; set; }
    }
}



