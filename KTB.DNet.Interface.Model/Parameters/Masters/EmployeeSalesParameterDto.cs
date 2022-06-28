#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EmployeeSalesParameterDto  class
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
    public class EmployeeSalesParameterDto : ParameterDtoBase, IValidatableObject
    {
        [DefaultValue(0)]
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(60, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss] #request by halimi 20200805
        public string Name { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Gender")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public byte Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DealerCode { get; set; }

        [StringLength(10, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string DealerBranchCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Status")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(1, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Status { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PlaceOfBirth")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string PlaceOfBirth { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DateOfBirth")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime DateOfBirth { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Address")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(200, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        //[AntiXss] #request by halimi 20200805
        public string Address { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string CityCode { get; set; }
        public int IsOtherCity { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Category")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string JobPositionCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "HireDate")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public DateTime HireDate { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "ResignDate")]
        public DateTime ResignDate { get; set; }

        [StringLength(700, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string ResignReason { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "MarriedStatus")]
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [StringLength(1, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string MarriedStatus { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string SalesmanAreaCode { get; set; }

        public int SalesmanLevelID { get; set; }

        public string LeaderCode { get; set; }

        [AntiXss]
        public string RefSalesmanCode { get; set; }

        public AttachmentParameterDto Image { get; set; }

        #region ProfileValue
        [Display(ResourceType = typeof(FieldResource), Name = "NOKTP")]
        [StringLength(16, MinimumLength = 16, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgKTPLengthInvalid")]
        [AntiXss]
        public string NoKTP { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PENDIDIKAN")]
        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Pendidikan { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "EMAIL")]
        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Email { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "HpNo")]
        [StringLength(250, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string No_HP { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "TeamCategory")]
        [AntiXss]
        public int Kategori_Tim { get; set; }
        #endregion

        public int ResignType { get; set; }

        //[AntiXss]
        //public int? ResignReasonID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // custom validation
            if (!Utils.ExcludedRegexIsNotExist(Name, @"\<\>\?\*\%\$\;")) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Name))); }
            if (!Utils.ExcludedRegexIsNotExist(Address, @"\<\>\?\*\%\$\;")) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Address))); }
            if (!Utils.ExcludedRegexIsNotExist(PlaceOfBirth, @"\<\>\?\*\%\$\;")) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.PlaceOfBirth))); }

            if (HireDate <= DateOfBirth) { results.Add(new ValidationResult(MessageResource.ErrorMsgHireDateLessThanBirthOfDate)); }

            if (!Utils.IsEmailValid(Email)) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.EMAIL))); }

            if (!Utils.IsNoHPValid(No_HP)) { results.Add(new ValidationResult(MessageResource.ErrorMsgHpNoFormat)); }

            if (ResignType < 0) { results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResignType))); }

            return results;
        }
    }

    // for sweager purpose
    public class UpdateEmployeeSalesParameterDto : EmployeeSalesParameterDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public new int ID { get { return base.ID; } set { base.ID = value; } }
    }

    // for sweager purpose
    public class CreateEmployeeSalesParameterDto : EmployeeSalesParameterDto
    {
    }
}
