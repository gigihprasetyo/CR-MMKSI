#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_LeadCustomerSalesForceParameterDto  class
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
    public class VWI_LeadCustomerSalesForceParameterDto : DtoBase, IValidatableObject
    {
        public Int64 ID { get; set; }
        public int DNetID { get; set; }
        [AntiXss]
        public string SumberData { get; set; }
        [AntiXss]
        public string CreateDate { get; set; }
        [AntiXss]
        public string CreateDate_YYYYMMDD { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string DealerName { get; set; }
        [AntiXss]
        public string CustomerTypeID { get; set; }
        [AntiXss]
        public string CustomerType { get; set; }
        [AntiXss]
        public string SalesmanCode { get; set; }
        [AntiXss]
        public string Name { get; set; }
        [AntiXss]
        public string CustomerCode { get; set; }
        [AntiXss]
        public string CustomerName { get; set; }
        [AntiXss]
        public string CustomerAddress { get; set; }
        [AntiXss]
        public string Phone { get; set; }
        [AntiXss]
        public string Email { get; set; }
        [AntiXss]
        public string SexID { get; set; }
        [AntiXss]
        public string Sex { get; set; }
        [AntiXss]
        public string AgeSegmentID { get; set; }
        [AntiXss]
        public string AgeSegment { get; set; }
        [AntiXss]
        public string CustomerStatusID { get; set; }
        [AntiXss]
        public string CustomerStatus { get; set; }
        [AntiXss]
        public string InformationTypeID { get; set; }
        [AntiXss]
        public string InformationType { get; set; }
        [AntiXss]
        public string InformationSourceID { get; set; }
        [AntiXss]
        public string InformationSource { get; set; }
        [AntiXss]
        public string CustomerPurposeID { get; set; }
        [AntiXss]
        public string CustomerPurpose { get; set; }
        [AntiXss]
        public string ProspectDate { get; set; }
        [AntiXss]
        public string ProspectDate_YYYYMMDD { get; set; }
        [AntiXss]
        public string VechileTypeCode { get; set; }
        [AntiXss]
        public string Description { get; set; }
        [AntiXss]
        public string CurrVehicleType { get; set; }
        [AntiXss]
        public string CurrVehicleBrand { get; set; }
        [AntiXss]
        public string Note { get; set; }
        [AntiXss]
        public string Keterangan { get; set; }
        [AntiXss]
        public string StatusResponseID { get; set; }
        [AntiXss]
        public string StatusResponse { get; set; }
        [AntiXss]
        public string WebID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
