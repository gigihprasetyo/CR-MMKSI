#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleClaimHeaderParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 10/09/2020 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model.CustomAttribute;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace KTB.DNet.Interface.Model
{
    public class ChassisMasterClaimHeaderParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime ClaimDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [AntiXss]
        public string PODestinationCode { get; set; }

        [AntiXss]
        public string DealerPIC { get; set; }

        [AntiXss]
        public string ReporterIssue { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeNumeric")]
        public int StatusID { get; set; }

        public DateTime DateOccur { get; set; }

        [AntiXss]
        public string PlaceOccur { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ChassisNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeNumeric")]
        public short StatusStockDMS { get; set; }

        public List<ChassisMasterClaimDetailParameterDto> ChassisMasterClaimDetails { get; set; }
        public List<ChassisMasterClaimDocumentUploadParameterDto> DocumentUpload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
    public class ChassisMasterClaimHeaderUpdateParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime ClaimDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [AntiXss]
        public string PODestinationCode { get; set; }

        [AntiXss]
        public string DealerPIC { get; set; }
        [AntiXss]
        public string ClaimNumber { get; set; }
        
        [AntiXss]
        public string ReporterIssue { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeNumeric")]
        public int StatusID { get; set; }

        public DateTime DateOccur { get; set; }

        [AntiXss]
        public string PlaceOccur { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ChassisNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeNumeric")]
        public short StatusStockDMS { get; set; }

        public List<ChassisMasterClaimDetailParameterDto> ChassisMasterClaimDetails { get; set; }
        public List<ChassisMasterClaimDocumentUploadParameterDto> DocumentUpload { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

}
