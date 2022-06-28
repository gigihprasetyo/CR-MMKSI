#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PartShopParameterDto  class
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
    public class PartShopParameterDto : ParameterDtoBase, IValidatableObject
    {
        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "City")]
        [AntiXss]
        public string CityCode { get; set; }

        [AntiXss]
        public string PartShopCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string Name { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Address")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string Address { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Phone")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string Phone { get; set; }

        [AntiXss]
        public string Fax { get; set; }
        [AntiXss]
        public string Email { get; set; }

        //[DefaultValue(1)]
        //public byte Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            //if (!(Utils.IsOfficeNoValid(Phone) || Utils.IsNoHPValid(Phone))) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.PhoneNumber))); }
            if (!Utils.IsEmailValid(Email)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.EMAIL))); }
            return results;
        }
    }

    public class PartShopUpdateParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string Name { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Address")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string Address { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Phone")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string Phone { get; set; }

        [AntiXss]
        public string Fax { get; set; }
        [AntiXss]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            //if (!(Utils.IsOfficeNoValid(Phone) || !Utils.IsNoHPValid(Phone))) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.PhoneNumber))); }
            if (!Utils.IsEmailValid(Email)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.EMAIL))); }
            return results;
        }
    }
}

