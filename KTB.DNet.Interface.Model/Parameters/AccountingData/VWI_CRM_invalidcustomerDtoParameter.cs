#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_invalidcustomer  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 8:22:47
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model 
{
    public class VWI_CRM_invalidcustomerDtoParameter : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public Guid Id { get; set; }
        [AntiXss]
        public bool InvalidIDCard { get; set; }
        [AntiXss]
        public bool InvalidIDCard_Length { get; set; }
        [AntiXss]
        public bool InvalidIDCard_Format { get; set; }
        [AntiXss]
        public bool InvalidIDCard_Duplicate { get; set; }
        [AntiXss]
        public bool InvalidIDCard_Dummy { get; set; }
        [AntiXss]
        public bool InvalidMobile { get; set; }
        [AntiXss]
        public bool InvalidMobile_Blank { get; set; }
        [AntiXss]
        public bool InvalidMobile_Length { get; set; }
        [AntiXss]
        public bool InvalidMobile_Format { get; set; }
        [AntiXss]
        public bool InvalidMobile_Duplicate { get; set; }
        [AntiXss]
        public bool InvalidMobile_Dummy { get; set; }
        [AntiXss]
        public bool InvalidBirthDate { get; set; }
        [AntiXss]
        public bool InvalidBirthDate_Blank { get; set; }
        [AntiXss]
        public bool InvalidBirthDate_Range { get; set; }
        [AntiXss]
        public bool InvalidEmail { get; set; }
        [AntiXss]
        public bool InvalidEmail_Blank { get; set; }
        [AntiXss]
        public bool InvalidEmail_Length { get; set; }
        [AntiXss]
        public bool InvalidEmail_Format { get; set; }
        [AntiXss]
        public bool InvalidEmail_Duplicate { get; set; }
        [AntiXss]
        public bool InvalidCustClass { get; set; }
        [AntiXss]
        public bool InvalidCustClass_Blank { get; set; }
        [AntiXss]
        public bool InvalidCustClass_Data { get; set; }
        [AntiXss]
        public string ID_Card_Error_Reason { get; set; }
        [AntiXss]
        public string Mobile_Nbr_Error_Reason { get; set; }
        [AntiXss]
        public string Birth_Date_Error_Reason { get; set; }
        [AntiXss]
        public string Email_Address_Error_Reason { get; set; }
        [AntiXss]
        public string Customer_Class_Error_Reason { get; set; }
        [AntiXss]
        public DateTime LastCheckedTime { get; set; }
        [AntiXss]
        public string ktb_dealercode { get; set; }
        [AntiXss]
        public string ktb_bucompany { get; set; }
        [AntiXss]
        public bool NGData { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
