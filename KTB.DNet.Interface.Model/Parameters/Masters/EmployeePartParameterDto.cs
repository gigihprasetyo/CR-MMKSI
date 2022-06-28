#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EmployeePartParameterDto  class
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
    public class EmployeePartParameterDto : ParameterDtoBase, IValidatableObject
    {
        [DefaultValue(0)]
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DealerBranchCode { get; set; }

        [AntiXss]
        public string SalesmanCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(60, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(60, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string PlaceOfBirth { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime DateOfBirth { get; set; }

        [DefaultValue(0)]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public byte Gender { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(200, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string CityCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int SalesmanCategoryLevel { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int SalesmanParentCategoryLevel { get; set; }

        [DefaultValue(0)]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int MarriedStatus { get; set; }

        public DateTime ResignDate { get; set; }

        [StringLength(700, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ResignReason { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(2, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ProvinceCode { get; set; }

        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Pendidikan { get; set; }

        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Email { get; set; }

        [StringLength(16, MinimumLength = 16, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgKTPLengthInvalid")]
        [AntiXss]
        public string NoKTP { get; set; }

        [AntiXss]
        public string No_HP { get; set; }

        [AntiXss]
        public string ReligionID { get; set; }

        [DefaultValue(0)]
        public decimal Salary { get; set; }

        public AttachmentParameterDto Image { get; set; }

        public int ResignType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Gender < 1) { results.Add(new ValidationResult(ErrorCode.DataRequiredField + "#" + string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.Gender))); }
            if (MarriedStatus < 1) { results.Add(new ValidationResult(ErrorCode.DataRequiredField + "#" + string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.MarriedStatus))); }

            if (Gender < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.Gender))); }
            if (MarriedStatus < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifInteger, FieldResource.MarriedStatus))); }

            if (HireDate > DateTime.Now)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgComparisonGreaterThanToday, FieldResource.HireDate, FieldResource.TodayDate)));
            }

            if (DateOfBirth >= HireDate)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgComparisonLessOrEqual, FieldResource.HireDate, FieldResource.DateOfBirth)));
            }

            if (!string.IsNullOrEmpty(No_HP))
            {
                if (No_HP.Length < 10)
                {
                    results.Add(new ValidationResult(MessageResource.ErrorMsgHpNoMinimum));
                }

                if (No_HP.Substring(0, 2) != "08")
                {
                    results.Add(new ValidationResult(MessageResource.ErrorMsgHpNoFormat));
                }
            }

            if (!Utils.IsEmailValid(Email)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.EMAIL))); }
            if (ResignType < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResignType))); }

            return results;
        }
    }

    public class EmployeePartUpdtaeParameterDto : EmployeePartParameterDto
    {

    }
}
