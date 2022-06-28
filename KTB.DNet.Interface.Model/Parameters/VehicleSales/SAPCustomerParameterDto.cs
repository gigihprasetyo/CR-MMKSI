#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SAPCustomerParameterDto  class
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SAPCustomerParameterDto : ParameterDtoBase, IValidatableObject
    {
        #region Required Data Members

        [Display(ResourceType = typeof(FieldResource), Name = "CustomerName")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(40, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string CustomerName { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PhoneNumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Phone { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CountryCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string CountryCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CustomerPurpose")]
        public short CustomerPurpose { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Sex")]
        public short Sex { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "VehicleType")]
        [AntiXss]
        public string VehicleTypeCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Quantity")]
        [DefaultValue(0)]
        public int Qty { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "InformationType")]
        public short InformationType { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "InformationSource")]
        public short InformationSource { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "SalesmanHeader")]
        //[AntiXss]
        public string SalesmanHeaderCode { get; set; }

        public short Status { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ProspectDate")]
        public DateTime ProspectDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CustomerType")]
        public short CustomerType { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "CampaignName")]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string CampaignCode { get; set; }

        #endregion

        #region Optional Data Members
        [Display(ResourceType = typeof(FieldResource), Name = "AgeSegment")]
        public short? AgeSegment { get; set; }

        [DefaultValue(0)]
        public int ID { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string CustomerAddress { get; set; }

        public int BusinessSectorDetailID { get; set; }

        [StringLength(8, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string CustomerCode { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Email { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string CurrVehicleBrand { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string CurrVehicleType { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string Note { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string WebID { get; set; }

        public DateTime? BirthDate { get; set; }

        public short? StatusCode { get; set; }

        [StringLength(2000, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Description { get; set; }

        [DataMember]
        public DateTime EstimatedCloseDate { get; set; }

        public short? StateCode { get; set; }

        public short? LeadStatus { get; set; }

        #endregion

        #region Ignored Data Members
        [IgnoreDataMember]
        public int DealerID { get; set; }

        [IgnoreDataMember]
        public int SalesmanHeaderID { get; set; }

        [IgnoreDataMember]
        public int VehicleTypeID { get; set; }

        public Guid? OriginatingLeadId { get; set; }

        [IgnoreDataMember]
        public bool IsSPK { get; set; }

        [IgnoreDataMember]
        [StringLength(100, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PreferedVehicleModel { get; set; }

        [AntiXss]
        public string GUIDUpdate { get; set; }

        [AntiXss]
        public string Guid { get; set; }

        [AntiXss]
        public int InterfaceStatus { get; set; }

        [AntiXss]
        public string InterfaceMessage { get; set; }
        #endregion

        /// <summary>
        /// Validation Process
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // string null or unset validation for required fields
            if (Utils.IsDefaultString(CustomerName)) { results.Add(new ValidationResult(ErrorCode.DataRequiredField + "#" + string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.CustomerName))); }
            //if (Utils.IsDefaultString(CustomerAddress)) { results.Add(new ValidationResult(ErrorCode.DataRequiredField + "#" + string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.Address))); }
            if (Utils.IsDefaultString(Phone)) { results.Add(new ValidationResult(ErrorCode.DataRequiredField + "#" + string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.PhoneNumber))); }

            // custom validation
            if (!Utils.IsNOTelpValid(Phone)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.PhoneNumber))); }
            if (!Utils.IsEmailValid(Email)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.EMAIL))); }
            if (!Utils.IsAlphaNumeric(CustomerCode)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.RefCustomerCode))); }
            //if (!Utils.IsAlphanumericForName(CustomerName)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.CustomerName))); }
            //if (!Utils.IsAlphanumericIncludeSpecialCharacter(CustomerAddress)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.Address))); }

            if (Qty < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgQuantity, FieldResource.Quantity))); }

            // validate original id
            if (ID != 0 && !OriginatingLeadId.HasValue) { results.Add(new ValidationResult(ErrorCode.DataRequiredField + "#" + string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.OriginatingLeadId))); }

            CustomerName = CustomerName == null ? CustomerName : CustomerName.ToUpper();
            CustomerAddress = CustomerAddress == null ? CustomerAddress : CustomerAddress.ToUpper();
            if (!string.IsNullOrEmpty(Guid))
            {
                if (InterfaceStatus == 0)
                {
                    results.Add(new ValidationResult("InterfaceStatus tidak boleh kosong"));
                }
                if (string.IsNullOrEmpty(InterfaceMessage))
                {
                    results.Add(new ValidationResult("InterfaceMessage tidak boleh kosong"));
                }
            }
            if (!Utils.IsNumeric(Phone)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.PhoneNumber))); }
            //if (CountryCode.Trim() == "62" && Phone.Substring(0, 1) != "8") { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgInvalidPhoneWithZero, FieldResource.PhoneNumber, "Kode Negara '" + CountryCode.Trim() + "'"))); }

            return results;
        }
    }
    // for sweager purpose
    public class SAPCustomerUpdateParameterDto : SAPCustomerParameterDto
    {
    }
}
