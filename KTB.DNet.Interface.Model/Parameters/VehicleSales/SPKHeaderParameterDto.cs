#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKHeaderParameterDto  class
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SPKHeaderParameterDto : ParameterDtoBase
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SPKDealerNumber")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerSPKNumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SPKReferenceNumber")]
        [AntiXss]
        public string SPKReferenceNumber { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Salesman")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string SalesmanCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerSPKDate")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime DealerSPKDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SPKStatus")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int Status { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SPKEvidence")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public AttachmentParameterDto EvidenceFile { get; set; }

        [AntiXss]
        public string RejectedReason { get; set; }

        public int? EventType { get; set; }

        public string CampaignName { get; set; }

        public OCRKKParameterDto OCRFamilyIdentity { get; set; }

        public SPKCustomerParameterDto SPKCustomer { get; set; }
        public List<SPKDetailParameterDto> SPKDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // custom validation
            if (!Utils.IsAlphaNumericPlusUniv(DealerSPKNumber)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.SPKDealerNumber))); }

            // Return if any errors
            if (results.Count > 0)
            { return results; }

            // validate SPK Customer
            Validator.TryValidateObject(SPKCustomer, new ValidationContext(SPKCustomer, null, null), results);

            // Return if any errors
            if (results.Count > 0)
            { return results; }

            // validate SPK Details
            foreach (SPKDetailParameterDto detail in SPKDetails)
            {
                Validator.TryValidateObject(detail, new ValidationContext(detail, null, null), results);
            }

            return results;
        }
    }

    // for sweager purpose
    public class SPKHeaderUpdateParameterDto : SPKHeaderParameterDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new int ID { get { return base.ID; } set { base.ID = value; } }
    }

    // for sweager purpose
    public class SPKHeaderCreateParameterDto : SPKHeaderParameterDto
    {
    }
}
