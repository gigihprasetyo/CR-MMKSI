#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipDetailParameterDto.cs class
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
    public class EstimationEquipDetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        public EstimationEquipDetailParameterDto()
        {
            ConfirmedDate = DateTime.MinValue;
        }

        public int ID { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int EstimationEquipHeaderID { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //public int SparePartMasterID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string PartNumber { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //public string ModelCode { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //public Decimal Harga { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        //public Decimal Discount { get; set; }

        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBePositiveInteger")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int TotalForecast { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeGreaterThanZero")]
        public int EstimationUnit { get; set; }

        //public int Status { get; set; }        
        public DateTime ConfirmedDate { get; set; }

        //public string Remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            return results;
        }
    }
}

