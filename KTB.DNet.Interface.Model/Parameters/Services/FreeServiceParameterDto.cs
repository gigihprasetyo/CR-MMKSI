#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FreeServiceParameterDto  class
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace KTB.DNet.Interface.Model
{
    public class FreeServiceParameterDto : ParameterDtoBase, IValidatableObject
    {
        public FreeServiceParameterDto()
        {
        }

        public int ID { get; set; }

        [StringLength(1, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgLengthCannotExceed")]
        [AntiXss]
        public string Status { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string ChassisNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string EngineNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string FSKindCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string VisitType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeNumeric")]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataMustBeGreaterThanZero")]
        public int MileAge { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [DisplayFormat(DataFormatString = "ddMMyyyy")]
        public DateTime ServiceDate { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Wajib Mengisikan Tanggal Service")]
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string DealerBranchCode { get; set; }
        [DisplayFormat(DataFormatString = "ddMMyyyy")]
        public DateTime SoldDate { get; set; }
        [AntiXss]
        [DefaultValue("0")]
        public string NotificationNumber { get; set; }
        [AntiXss]
        [DefaultValue("")]
        public string NotificationType { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal LabourAmount { get; set; }
        public Decimal PartAmount { get; set; }
        public Decimal PPNAmount { get; set; }
        public Decimal PPHAmount { get; set; }
        [AntiXss]
        [DefaultValue("")]
        public string Reject { get; set; }
        public int Reason { get; set; }
        [AntiXss]
        [DefaultValue("")]
        public string ReleaseBy { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int FleetRequestID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string WorkOrderNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public Boolean isBB { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string FileName { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        [AntiXss]
        public string Base64OfStream { get; set; }


        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Utils.IsAlphaNumeric(ChassisNumber)) { results.Add(new ValidationResult(FieldResource.ChassisNumber + string.Format(MessageResource.ErrorMsgDataInvalid, ChassisNumber))); }

            if (ServiceDate == Constants.DATETIME_DEFAULT_VALUE)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ServiceDate)));
            }

            if (!isBB && SoldDate == Constants.DATETIME_DEFAULT_VALUE)
            {
                results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.SoldDate)));
            }

            //if (SoldDate < Constants.DATETIME_DEFAULT_VALUE || SoldDate >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value)
            //{
            //    results.Add(new ValidationResult(string.Format(MessageResource.ErrorMsgDataFormatField, FieldResource.SoldDate)));
            //}


            return results;
        }
    }
}

