#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EndCustomerParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class EndCustomerParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        [AntiXss]
        public string ProjectIndicator { get; set; }
        public int RefChassisNumberID { get; set; }
        public int CustomerID { get; set; }
        [AntiXss]
        public string Name1 { get; set; }
        public DateTime FakturDate { get; set; }
        public DateTime OpenFakturDate { get; set; }
        [AntiXss]
        public string FakturNumber { get; set; }
        [AntiXss]
        public string AreaViolationFlag { get; set; }
        public byte AreaViolationPaymentMethodID { get; set; }
        public Decimal AreaViolationyAmount { get; set; }
        [AntiXss]
        public string AreaViolationBankName { get; set; }
        [AntiXss]
        public string AreaViolationGyroNumber { get; set; }
        [AntiXss]
        public string PenaltyFlag { get; set; }
        public byte PenaltyPaymentMethodID { get; set; }
        public Decimal PenaltyAmount { get; set; }
        [AntiXss]
        public string PenaltyBankName { get; set; }
        [AntiXss]
        public string PenaltyGyroNumber { get; set; }
        [AntiXss]
        public string ReferenceLetterFlag { get; set; }
        [AntiXss]
        public string ReferenceLetter { get; set; }
        [AntiXss]
        public string SaveBy { get; set; }
        public DateTime SaveTime { get; set; }
        [AntiXss]
        public string ValidateBy { get; set; }
        public DateTime ValidateTime { get; set; }
        [AntiXss]
        public string ConfirmBy { get; set; }
        public DateTime ConfirmTime { get; set; }
        [AntiXss]
        public string DownloadBy { get; set; }
        public DateTime DownloadTime { get; set; }
        [AntiXss]
        public string PrintedBy { get; set; }
        public DateTime PrintedTime { get; set; }
        public int CleansingCustomerID { get; set; }
        public int MCPHeaderID { get; set; }
        public int MCPStatus { get; set; }
        public int LKPPHeaderID { get; set; }
        public int LKPPStatus { get; set; }
        [AntiXss]
        public string Remark1 { get; set; }
        [AntiXss]
        public string Remark2 { get; set; }
        public DateTime HandoverDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
