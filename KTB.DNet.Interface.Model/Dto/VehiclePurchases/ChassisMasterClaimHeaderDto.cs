#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterClaimHeader  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 10/09/2020 3:32
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Model
{
    public class ChassisMastertClaimHeaderDto : DtoBase
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string ClaimNumber { get; set; }
        public int StatusID { get; set; }
        public string ChassisNumber { get; set; }
        public string ReporterIssue { get; set; }
        public int ResponClaim { get; set; }
        public int IndicatorID { get; set; }
        public string ChassisNumberReplacement { get; set; }
        public string Remark { get; set; }
        public int StatusProcessRetur { get; set; }
        public DateTime EstimationRepairDate { get; set; }
        public DateTime ActualRepairDate { get; set; }
        public decimal Nominal { get; set; }
        public string SORetur { get; set; }
        public string DORetur { get; set; }
        public string BillingRetur { get; set; }
        public string SONormalRetur { get; set; }
        public string DONormalRetur { get; set; }
        public string BillingNormalRetur { get; set; }
        public DateTime? TransferDate { get; set; }
    }
    public class ChassisMastertClaimHeaderCreateResponseDto
    {
        public int ID { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public List<ChassisMasterClaimDetailDto> ChassisMasterClaimDetails{get;set;}
        public List<ChassisMasterClaimDocumentUploadDto> DocumentUpload{get;set; }
    }

    public class ChassisMastertClaimHeaderUpdateResponseDto
    {
        public int ID { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public List<ChassisMasterClaimDetailDto> ChassisMasterClaimDetails { get; set; }
        public List<ChassisMasterClaimDocumentUploadDto> DocumentUpload { get; set; }
    }
}
