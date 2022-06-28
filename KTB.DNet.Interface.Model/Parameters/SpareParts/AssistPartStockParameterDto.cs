#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartStockParameterDto  class
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
    public class AssistPartStockParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Month")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //[Range(1,12, ErrorMessageResourceType= typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgMonthRange")]
        [AntiXss]
        public string Month { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "Year")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string Year { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerCode")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "DealerBranchCode")]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string DealerBranchCode { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "PartNumber")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string NoParts { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "JumlahStokAwal")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Double JumlahStokAwal { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "JumlahDatang")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Double JumlahDatang { get; set; }

        [DefaultValue(0)]
        [Display(ResourceType = typeof(FieldResource), Name = "HargaBeli")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Decimal HargaBeli { get; set; }

        //public int DealerID { get; set; }
        //public int AssistUploadLogID { get; set; }
        //public int SparepartMasterID { get; set; }
        //public int DealerBranchID { get; set; }
        //public string RemarksSystem { get; set; }
        [DefaultValue(1)]
        public int StatusAktif { get; set; }

        [DefaultValue(1)]
        public int ValidateSystemStatus { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Utils.IsNumeric(Month))
            {
                if (!Utils.IsMonthValid(Convert.ToInt16(Month)))
                {
                    results.Add(new ValidationResult(FieldResource.Month + string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Month)));
                }
            }
            else { results.Add(new ValidationResult(FieldResource.Month + string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Month))); }

            if (!Utils.IsNumeric(Year)) { results.Add(new ValidationResult(FieldResource.Year + string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Year))); }

            if (this.HargaBeli < 0)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataPositifIntegerSparePart, string.Format(FieldResource.HargaBeli), this.NoParts)));
            }

            return results;
        }
    }
}