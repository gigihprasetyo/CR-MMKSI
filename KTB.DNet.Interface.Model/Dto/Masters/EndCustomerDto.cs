#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EndCustomerDto  class
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
using KTB.DNet.Interface.Model.CustomAttribute;
using System;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class EndCustomerDto : DtoBase
    {
        public int ID { get; set; }

        public string ProjectIndicator { get; set; }

        public int RefChassisNumberID { get; set; }

        public string Name1 { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime FakturDate { get; set; }

        [DataType(DataType.Date)]
        [DateDisplayFormatAttribute]
        public DateTime OpenFakturDate { get; set; }

        public string FakturNumber { get; set; }

        public string AreaViolationFlag { get; set; }

        public decimal AreaViolationAmount { get; set; }

        public string AreaViolationBankName { get; set; }

        public string AreaViolationGyroNumber { get; set; }

        public string PenaltyFlag { get; set; }

        public decimal PenaltyAmount { get; set; }

        public string PenaltyBankName { get; set; }

        public string PenaltyGyroNumber { get; set; }

        public string ReferenceLetterFlag { get; set; }

        public string ReferenceLetter { get; set; }

        public string SaveBy { get; set; }

        [DateTimeDisplayFormatAttribute]
        public DateTime SaveTime { get; set; }

        public string ValidateBy { get; set; }

        [DateTimeDisplayFormatAttribute]
        public DateTime ValidateTime { get; set; }

        public string ConfirmBy { get; set; }

        public DateTime ConfirmTime { get; set; }

        public string DownloadBy { get; set; }

        [DateTimeDisplayFormatAttribute]
        public DateTime DownloadTime { get; set; }

        public string PrintedBy { get; set; }

        [DateTimeDisplayFormatAttribute]
        public DateTime PrintedTime { get; set; }

        public int CleansingCustomerId { get; set; }

        public short MCPStatus { get; set; }

        public string Remark1 { get; set; }

        public string Remark2 { get; set; }

        public DateTime HandoverDate { get; set; }

        public CustomerDto Customer { get; set; }

        public OwnerAgeDto OwnerAge { get; set; }

        public MainUsageDto MainUsage { get; set; }

        public VehicleBodyShapeDto VehicleBodyShape { get; set; }

        public VehiclePurposeDto VehiclePurpose { get; set; }

        public MainOperationAreaDto MainOperationArea { get; set; }

        public CustomerBusinessDto CustomerBusiness { get; set; }

        public VehicleOwnershipDto VehicleOwnership { get; set; }

        public PaymentTypeDto PaymentType { get; set; }

        public int AreaViolationPaymentMethodID { get; set; }

        public int PenaltyPaymentMethodID { get; set; }

        public CityDto City { get; set; }

        public ChassisMasterDto ChassisMaster { get; set; }

        public SPKFakturDto SPKFaktur { get; set; }

        public MCPHeaderDto MCPHeader { get; set; }

        public LKPPHeaderDto LKPPHeader { get; set; }

        public short LKPPStatus { get; set; }
    }
}
