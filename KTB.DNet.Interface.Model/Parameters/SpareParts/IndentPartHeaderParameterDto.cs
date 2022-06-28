#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IndentPartHeaderParameterDto  class
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
#endregion

namespace KTB.DNet.Interface.Model
{
    public class IndentPartHeaderParameterDto : ParameterDtoBase, IValidatableObject
    {
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int ID { get; set; }

        [StringLength(13, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string RequestNo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime RequestDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "MaterialType")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int MaterialType { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public byte Status { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public byte StatusKTB { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string SubmitFile { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public byte PaymentType { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Price")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime KTBConfirmedDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DescID")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public byte DescID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ChassisNumber")]
        [StringLength(20, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ChassisNumber { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DMSPRNo { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int DealerID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public List<IndentPartDetailParameterDto> IndentPartDetail { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }

    public class IndentPartHeaderCreateParameterDto : ParameterDtoBase, IValidatableObject
    {
        [DefaultValue(0)]
        public int ID { get; set; }

        [AntiXss]
        public string RequestNo { get; set; }

        public DateTime RequestDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "MaterialType")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int MaterialType { get; set; }

        [DefaultValue(0)]
        public byte Status { get; set; }

        [DefaultValue(0)]
        public byte StatusKTB { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string SubmitFile { get; set; }

        [DefaultValue(0)]
        public byte PaymentType { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Price")]
        [DefaultValue(0.0)]
        public decimal Price { get; set; }

        public DateTime KTBConfirmedDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DescID")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public byte DescID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ChassisNumber")]
        [StringLength(20, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ChassisNumber { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DMSPRNo { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public List<IndentPartDetailCreateParameterDto> IndentPartDetail { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (ID != 0)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgCreateID, ID)));
            }

            List<int> validType = new List<int>(new[] { 1, 3 }); ;
            bool isMaterialTypeInRange = validType.IndexOf(MaterialType) != -1;
            if (!isMaterialTypeInRange) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.MaterialType) + ValidationResource.ValidationIndentPartHeaderAccesorries)); }

            List<int> validDescID = new List<int>(new[] { 1, 2 }); ;
            bool isDescIDInRange = validDescID.IndexOf(DescID) != -1;
            if (!isDescIDInRange) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.DescID) + ValidationResource.ValidationIndentPartHeaderStamping)); }

            return results;
        }
    }

    public class IndentPartHeaderUpdateParameterDto : ParameterDtoBase, IValidatableObject
    {
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int ID { get; set; }

        [AntiXss]
        public string RequestNo { get; set; }

        public DateTime RequestDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "MaterialType")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int MaterialType { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public byte Status { get; set; }

        [DefaultValue(0)]
        public byte StatusKTB { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string SubmitFile { get; set; }

        [DefaultValue(0)]
        public byte PaymentType { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Price")]
        [DefaultValue(0.0)]
        public decimal Price { get; set; }

        public DateTime KTBConfirmedDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DescID")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public byte DescID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ChassisNumber")]
        [StringLength(20, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ChassisNumber { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DMSPRNo { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public List<IndentPartDetailParameterDto> IndentPartDetail { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (ID == 0 || string.IsNullOrEmpty(RequestNo)) { results.Add(new ValidationResult(string.Format(ValidationResource.ValidationIndentPartDataID))); }
            if (MaterialType < 1) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataMustBeGreaterThanZero, FieldResource.MaterialType))); }

            List<int> validType = new List<int>(new[] { 1, 3 }); ;
            bool isMaterialTypeInRange = validType.IndexOf(MaterialType) != -1;
            if (!isMaterialTypeInRange) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.MaterialType) + ValidationResource.ValidationIndentPartHeaderAccesorries)); }

            List<int> validDescID = new List<int>(new[] { 1, 2 }); ;
            bool isDescIDInRange = validDescID.IndexOf(DescID) != -1;
            if (!isDescIDInRange) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.DescID) + ValidationResource.ValidationIndentPartHeaderStamping)); }

            return results;
        }
    }
}
