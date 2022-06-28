#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetailParameterDto  class
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
using KTB.DNet.Interface.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SPKDetailParameterDto : DtoBase, IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Category")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string CategoryCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "VehicleColorCode")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(4, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string VehicleColorCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "VehicleTypeCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(4, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string VehicleTypeCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Amount")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int Amount { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "LineItem")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public short LineItem { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "RejectedReason")]
        [StringLength(255, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string RejectedReason { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SPKDetailStatus")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public short Status { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Quantity")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [Range(1, 1000000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgInvalidDetailQty")]
        public int Quantity { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ProfileDetailCode")]
        [AntiXss]
        public string ProfileDetailCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Additional")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public short Additional { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string Remarks { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int SPKHeaderID { get; set; }


        public string CBU_BODYTYPE1 { get; set; }

        public string CBU_BODYTYPELCV1 { get; set; }

        public string CBU_LOADPROFILE1 { get; set; }

        public string CBU_MEDANOPERASI1 { get; set; }

        public string CBU_OWNERSHIP1 { get; set; }

        public string CBU_PURCSTAT { get; set; }

        public string CBU_PURPOSE1 { get; set; }

        public string CBU_USERAGE1 { get; set; }

        public string CBU_WAYPAID1 { get; set; }

        public string CBU_JENISKEND { get; set; }

        public string CBU_MODELKEND { get; set; }

        public string CBU_PURCSTAT2 { get; set; }

        public string CBU_PURPOSE2 { get; set; }

        public string CBU_LEASING { get; set; }

        public string CBU_CARROSSERIE { get; set; }

        public int? EventType { get; set; }

        public string CampaignName { get; set; }

        public SPKDetailCustomerParameterDto SPKDetailCustomer { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Amount < 1)
            {
                results.Add(new ValidationResult(MessageResource.ErrorMsgInvalidAmount));
            }

            return results;
        }

    }
}
